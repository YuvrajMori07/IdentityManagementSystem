namespace Test.Api
{
    using Authentication.Api.Controllers;
    using Authentication.Application.Commands.User.Create;
    using Authentication.Application.Commands.User.Delete;
    using Authentication.Application.Commands.User.Update;
    using Authentication.Application.DTOs;
    using Authentication.Application.Queries.User;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidCommand_ReturnsOk()
        {
            var command = new CreateUserCommand { UserName = "test", Password = "pass", Email = "test@test.com", FullName = "Test", Roles = new List<string>() };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(1);

            var result = await _controller.CreateUser(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task CreateUser_MediatorThrows_ReturnsException()
        {
            var command = new CreateUserCommand();
            _mediatorMock.Setup(m => m.Send(command, default)).ThrowsAsync(new Exception("fail"));

            await Assert.ThrowsAsync<Exception>(() => _controller.CreateUser(command));
        }

        [Fact]
        public async Task GetAllUserAsync_UsersExist_ReturnsOk()
        {
            var users = new List<UserResponseDTO> { new UserResponseDTO() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), default)).ReturnsAsync(users);

            var result = await _controller.GetAllUserAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetAllUserAsync_NoUsers_ReturnsOkWithEmptyList()
        {
            var users = new List<UserResponseDTO>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), default)).ReturnsAsync(users);

            var result = await _controller.GetAllUserAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Empty((List<UserResponseDTO>)okResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ValidUserId_ReturnsOk()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), default)).ReturnsAsync(1);

            var result = await _controller.DeleteUser("userId");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task GetUserDetails_ValidUserId_ReturnsOk()
        {
            var userDetails = new UserDetailsResponseDTO();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserDetailsQuery>(), default)).ReturnsAsync(userDetails);

            var result = await _controller.GetUserDetails("userId");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDetails, okResult.Value);
        }

        [Fact]
        public async Task GetUserDetailsByUserName_ValidUserName_ReturnsOk()
        {
            var userDetails = new UserDetailsResponseDTO();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserDetailsByUserNameQuery>(), default)).ReturnsAsync(userDetails);

            var result = await _controller.GetUserDetailsByUserName("userName");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDetails, okResult.Value);
        }

        [Fact]
        public async Task AssignRoles_ValidCommand_ReturnsOk()
        {
            var command = new AssignUsersRoleCommand { UserName = "user", Roles = new List<string>() };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(1);

            var result = await _controller.AssignRoles(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task EditUserRoles_ValidCommand_ReturnsOk()
        {
            var command = new UpdateUserRolesCommand { userName = "user", Roles = new List<string>() };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(1);

            var result = await _controller.EditUserRoles(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task GetAllUserDetails_UsersExist_ReturnsOk()
        {
            var userDetailsList = new List<UserDetailsResponseDTO> { new UserDetailsResponseDTO() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersDetailsQuery>(), default)).ReturnsAsync(userDetailsList);

            var result = await _controller.GetAllUserDetails();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(userDetailsList, okResult.Value);
        }

        [Fact]
        public async Task EditUserProfile_IdMatches_ReturnsOk()
        {
            var command = new EditUserProfileCommand { Id = "id", FullName = "Test", Email = "test@test.com", Roles = new List<string>() };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(1);

            var result = await _controller.EditUserProfile("id", command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task EditUserProfile_IdDoesNotMatch_ReturnsBadRequest()
        {
            var command = new EditUserProfileCommand { Id = "id1" };

            var result = await _controller.EditUserProfile("id2", command);

            Assert.IsType<BadRequestResult>(result);
        }
    }

}