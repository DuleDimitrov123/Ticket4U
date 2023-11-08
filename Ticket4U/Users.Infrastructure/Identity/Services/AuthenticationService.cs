using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Application.Exceptions;
using Shared.Infrastructure.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Application.Contracts.Identity;
using Users.Application.Features.Users.Commands.AuthenticateUser;
using Users.Application.Features.Users.Commands.RegistrateUser;
using Users.Application.Models.Identity;
using Users.Domain.Users;

namespace Users.Infrastructure.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(UserManager<User> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateUserCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user == null)
        {
            throw new NotFoundException("User", command.Email);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, command.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new Exception($"Credentials for '{command.Email} aren't valid!'");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthenticateResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName,
        };

        return response;
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim("roles", roles[i]));
        }

        if (!user.IsAdmin)//If it is admin, automatically is added roles: Admin
        {
            roleClaims.Add(new Claim("roles", "User"));
        }

        var claims = new[]
        {
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

    public async Task<RegistrationResponse> RegistrateAsync(RegistrateUserCommand command)
    {
        var existingUser = await _userManager.FindByNameAsync(command.UserName);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(command.UserName, "username");
        }

        var existingEmail = await _userManager.FindByEmailAsync(command.Email);

        if (existingEmail != null)
        {
            throw new UserAlreadyExistsException(command.Email, "email");
        }

        var user = User.Create(command.Email, command.UserName, command.FirstName, command.LastName, true, command.IsAdmin);

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new Exception($"{result.Errors}");
        }

        if (command.IsAdmin)
        {
            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

            if (!roleResult.Succeeded)
            {
                throw new Exception($"{roleResult.Errors}");
            }
        }

        return new RegistrationResponse() { UserId = user.Id };
    }
}
