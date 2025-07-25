namespace Test.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using Authentication.Application.Commands.Auth;
    using Authentication.Application.Common.Exceptions;
    using Authentication.Application.Common.Interfaces;
    using Moq;
    using Xunit;

    public class AuthCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCredentials_ReturnsAuthResponseDTO()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();
            var tokenGeneratorMock = new Mock<ITokenGenerator>();

            var userId = "user123";
            var userName = "testuser";
            var fullName = "Test User";
            var email = "test@example.com";
            List<string> roles = new List<string> { "Admin" };
            var token = "jwt-token";

            identityServiceMock.Setup(x => x.SigninUserAsync(userName, "password"))
                .ReturnsAsync(true);
            identityServiceMock.Setup(x => x.GetUserIdAsync(userName))
                .ReturnsAsync(userId);
            identityServiceMock.Setup(x => x.GetUserDetailsAsync(userId))
                .ReturnsAsync((userId, fullName, userName, email, (IList<string>)roles));
            tokenGeneratorMock.Setup(x => x.GenerateJWTToken(new(userId, userName, roles)))
                .Returns(token);

            var handler = new AuthCommandHandler(identityServiceMock.Object, tokenGeneratorMock.Object);
            var command = new AuthCommand { UserName = userName, Password = "password" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(userName, result.Name);
            Assert.Equal(token, result.Token);
        }

        [Fact]
        public async Task Handle_InvalidCredentials_ThrowsBadRequestException()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();
            var tokenGeneratorMock = new Mock<ITokenGenerator>();

            identityServiceMock.Setup(x => x.SigninUserAsync("wronguser", "wrongpass"))
                .ReturnsAsync(false);

            var handler = new AuthCommandHandler(identityServiceMock.Object, tokenGeneratorMock.Object);
            var command = new AuthCommand { UserName = "wronguser", Password = "wrongpass" };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}