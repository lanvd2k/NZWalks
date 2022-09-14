using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            
            // Check if user is authenticated and check username and password
            var user = await _userRepository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (user != null)
            {
                // Generate JWT token
                var token = await _tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("UserName or Password is incorrect!");
        }
    }
}
