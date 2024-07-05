using Entity.Concrete.Identity;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.AuthDTOs;
using ExamManagement.Business.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, Role role)
        {
            var token = await _authService.LoginAsync(loginDTO, role.ToString());

            return Ok(token);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefleshTokenLogin(string refleshToken)
        {
            var accessToken = await _authService.RefleshTokenLoginAsync(refleshToken);

            return Ok(accessToken);
        }
    }
}
