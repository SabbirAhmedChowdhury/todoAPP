using AuthenticationAPI.Definitions;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValidUser = await _authService.IsValidUser(user);

            if(isValidUser)
            {
                var token = _tokenService.GenerateToken();
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }                     
        }
    }
}
