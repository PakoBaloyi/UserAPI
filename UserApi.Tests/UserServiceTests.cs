using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using UserApi.Application.DTO;
using UserApi.Application.Mappings;
using UserApi.Application.Services;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Interfaces;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace UserApi.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IGroupRepository> _groupRepoMock;
        private readonly IMapper _mapper;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _groupRepoMock = new Mock<IGroupRepository>();
            var services = new ServiceCollection();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

            var provider = services.BuildServiceProvider();
            _mapper = provider.GetRequiredService<IMapper>();

            _service = new UserService(_userRepoMock.Object, _mapper, _groupRepoMock.Object);
        }

        [Fact]
        public async Task GetUserAsync_ReturnsUser_WhenUserExists()
        {
            var user = new User
            {
                Id = 1,
                Username = "testuser",
                Name = "John",
                LastName = "Doe",
                Email = "john@example.com",
                IsActive = true,
                Groups = new List<Group> { new Group { Id = 1, Name = "Admin" } }
            };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var result = await _service.GetUserAsync(1);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Id.Should().Be(1);
            result.Data.FullName.Should().Be("John Doe");
            result.Data.GroupIds.Should().Contain(1);
        }

        [Fact]
        public async Task GetUserAsync_ReturnsFail_WhenUserNotFound()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            var result = await _service.GetUserAsync(1);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("User not found");
        }


        [Fact]
        public async Task UpdateUserAsync_ReturnsFail_WhenUserNotFound()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            var result = await _service.UpdateUserAsync(new UpdateUserDto { Id = 1 });

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("User not found");
        }

        [Fact]
        public async Task DeleteUserAsync_SetsIsActiveFalse()
        {
            var user = new User { Id = 1, IsActive = true };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.UpdateAsync(user)).Returns(Task.CompletedTask);

            var result = await _service.DeleteUserAsync(1);

            result.Success.Should().BeTrue();
            user.IsActive.Should().BeFalse();
            _userRepoMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ReturnsFail_WhenUserNotFound()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((User?)null);

            var result = await _service.DeleteUserAsync(1);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("User not found");
        }

        [Fact]
        public async Task GetUserCountAsync_ReturnsCount()
        {
            _userRepoMock.Setup(r => r.GetUserCountAsync()).ReturnsAsync(5);

            var result = await _service.GetUserCountAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().Be(5);
        }

        [Fact]
        public async Task GetUserCountsPerGroupAsync_ReturnsCounts()
        {
            var counts = new Dictionary<string, int>
            {
                { "Admin", 2 },
                { "User", 3 }
            };
            _userRepoMock.Setup(r => r.GetUserCountsPerGroupAsync()).ReturnsAsync(counts);

            var result = await _service.GetUserCountsPerGroupAsync();

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCount(2);
            result.Data["Admin"].Should().Be(2);
            result.Data["User"].Should().Be(3);
        }
    }
}