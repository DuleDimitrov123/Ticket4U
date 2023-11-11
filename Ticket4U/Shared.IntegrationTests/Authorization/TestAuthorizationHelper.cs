using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.IntegrationTests.Authorization;

public static class TestAuthorizationHelper
{
    public static string CreateBasicToken()
    {
        return CreateTokenWithClaims(new List<Claim>());
    }

    public static string CreateAdminToken()
    {
        var claims = new List<Claim>()
        {
            new Claim("roles", "Admin")
        };

        return CreateTokenWithClaims(claims);
    }

    private static string CreateTokenWithClaims(List<Claim> claims)
    {
        var secret = "superSecretKeyForUsersApi";
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

        var testIssuer = "Ticket4UIdentity";
        var testAudience = "Ticket4UIdentityUser";

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = testIssuer,
            Audience = testAudience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
