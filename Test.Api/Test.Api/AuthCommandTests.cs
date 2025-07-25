namespace Test.Api
{
    using Authentication.Api.Controllers;
    using Authentication.Application.Commands.Auth;
    using Authentication.Application.DTOs;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class AuthCommandTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public AuthCommandTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithAuthResponseDTO()
        {
            // Arrange
            var command = new AuthCommand { UserName = "testuser", Password = "password" };
            var expectedResponse = new AuthResponseDTO
            {
                UserId = "1",
                Name = "Test User",
                Token = "jwt-token"
            };
            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthResponseDTO>(okResult.Value);
            Assert.Equal(expectedResponse.UserId, response.UserId);
            Assert.Equal(expectedResponse.Name, response.Name);
            Assert.Equal(expectedResponse.Token, response.Token);
        }

        [Fact]
        public async Task Login_CallsMediatorSendOnce()
        {
            // Arrange
            var command = new AuthCommand { UserName = "user", Password = "pass" };
            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthResponseDTO());

            // Act
            await _controller.Login(command);

            // Assert
            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}