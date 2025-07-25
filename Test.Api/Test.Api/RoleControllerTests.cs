namespace Test.Api
{
    using Authentication.Api.Controllers;
    using Authentication.Application.Commands.Role.Create;
    using Authentication.Application.Commands.Role.Update;
    using Authentication.Application.DTOs;
    using Authentication.Application.Queries.Role;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    public class RoleControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RoleController _controller;

        public RoleControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RoleController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateRoleAsync_ReturnsOkResult()
        {
            var command = new RoleCreateCommand { RoleName = "test" };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.CreateRoleAsync(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task GetRoleAsync_ReturnsOkResultWithList()
        {
            var roles = new List<RoleResponseDTO> { new RoleResponseDTO() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRoleQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(roles);

            var result = await _controller.GetRoleAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(roles, okResult.Value);
        }

        [Fact]
        public async Task GetRoleByIdAsync_ReturnsOkResult()
        {
            var role = new RoleResponseDTO();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRoleByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(role);

            var result = await _controller.GetRoleByIdAsync("id");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(role, okResult.Value);
        }

        [Fact]
        public async Task DeleteRoleAsync_ReturnsOkResult()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<Authentication.Application.Commands.Role.Delete.DeleteRoleCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteRoleAsync("id");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task EditRole_IdMatches_ReturnsOkResult()
        {
            var command = new UpdateRoleCommand { Id = "id", RoleName = "name" };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.EditRole("id", command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task EditRole_IdDoesNotMatch_ReturnsBadRequest()
        {
            var command = new UpdateRoleCommand { Id = "id1", RoleName = "name" };

            var result = await _controller.EditRole("id2", command);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}