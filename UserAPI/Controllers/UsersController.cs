using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Application.DTO;
using UserApi.Application.Interfaces;

namespace UserAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService service, IGroupService groupService) : ControllerBase
    {
        private readonly IUserService _service = service;
        private readonly IGroupService _groupService = groupService;    

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _service.GetAllUsersAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _service.GetUserAsync(id);
            return result.Success ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto dto)
        {
            var result = await _service.AddUserAsync(dto);
            return result.Success
                ? CreatedAtAction(nameof(GetUserById), new { id = result.Data!.Id }, result.Data)
                : BadRequest(result.ErrorMessage);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var result = await _service.UpdateUserAsync(dto);
            return result.Success ? NoContent() : NotFound(result.ErrorMessage);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _service.DeleteUserAsync(id);
            return result.Success ? NoContent() : NotFound(result.ErrorMessage);
        }

        [HttpGet("get-user-count")]
        public async Task<IActionResult> GetUserCount()
        {
            var result = await _service.GetUserCountAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("get-user-counts-per-group")]
        public async Task<IActionResult> GetUserCountsPerGroup()
        {
            var result = await _service.GetUserCountsPerGroupAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("get-all-groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _groupService.GetAllGroupsAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}