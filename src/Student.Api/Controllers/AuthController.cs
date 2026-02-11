using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Repositories;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null || user.Password != dto.Password)
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(user.Username, user.Role);

            return Ok(new { token });
        }
    }

}
