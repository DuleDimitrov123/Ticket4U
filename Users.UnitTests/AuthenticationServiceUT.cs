namespace Users.UnitTests;

public class AuthenticationServiceUT
{
    //[Theory]
    //[AutoMoqData]
    //public async Task AuthenticateAsync_UserDoesntExist(
    //    [Frozen] Mock<UserManager<User>> userManagerMock,
    //    [Frozen] Mock<IOptions<JwtSettings>> jwtSettingsMock
    //    //[Frozen] Mock<SignInManager<User>> signInManagerMock
    //    //AuthenticationService authenticationService
    //    )
    //{
    //    userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

    //    //var signInManagerMock = new Mock<SignInManager<User>>();

    //    var authenticationService = new AuthenticationService(userManagerMock.Object, jwtSettingsMock.Object, null);

    //    await Assert.ThrowsAsync<Exception>(() => authenticationService.AuthenticateAsync(new AuthenticateRequest()));
    //}
}
