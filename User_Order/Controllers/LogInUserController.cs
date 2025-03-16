//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using User_Order.Model.DTO;
using User_Order.Service.IService;

namespace User_Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInUserController : ControllerBase
    {
        private readonly ILogInUserService _logInUserService;

        public LogInUserController(ILogInUserService logInUserService)
        {
            _logInUserService = logInUserService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateLogInUser([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            if (registrationRequestDTO == null)
                return BadRequest(new { error = "Invalid request data" });

            bool isUniqueUser = await _logInUserService.IsUniqueUser(registrationRequestDTO.UserName);

            if (!isUniqueUser)
                return BadRequest(new { error = "The username already exists" });

            var user = await _logInUserService.Register(registrationRequestDTO);

            if (user == null)
                return BadRequest(new { error = "An error occurred while registering" });

            return Ok(user);

        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInRequestDTO logInRequestDTO)
        {
            if (logInRequestDTO == null)
                return BadRequest(new { error = "Invalid request data" });

            var loginResponse = await _logInUserService.LogIn(logInRequestDTO);

            if (loginResponse == null || loginResponse.LogInUserDetail == null)
                return BadRequest(new { error = "Invalid username or password" });

            return Ok(loginResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogInUsers()
        {
            var users = await _logInUserService.GetAllLogInUsers();
            if (users == null || !users.Any())
                return NotFound(new { error = "No User FOund" });
            return Ok(users);
        }
    }
}
