using Microsoft.AspNetCore.Mvc;
using UserApi.Application.Services;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(TokenService tokenService) : ControllerBase
    {
        private readonly TokenService _tokenService = tokenService;

        [HttpGet("generate")]
        public IActionResult GenerateToken()
        {
            var token = _tokenService.GenerateToken("test-user");
            return Ok(new { token });
        }
    }
}
