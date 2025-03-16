using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Order.Model;
using User_Order.Model.DTO;
using User_Order.Service;
using User_Order.Service.IService;

namespace User_Order.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditLogService;

        public UserController(IUserService userService , IAuditLogService auditLogService)
        {
            _userService = userService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        //[Authorize(Roles = "Level1")]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest(new { error = "The User data is invalid or empty." });

            try
            {
                var user = await _userService.AddUserAsync(userDTO);
                if (user == null)
                    return StatusCode(500, new { error = "Failed to create user. Please try again later." });

                //Log tha Audit trail
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = User.Identity.Name;
                await _auditLogService.AddAuditLogAsync("User" , user.UserId.ToString() , "Create" , userId , userName ,"Completed the Create Operation On Users");

                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Level1")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUserAsync();
            if (users == null || !users.Any())
                return NotFound("No User data found");

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", "Get", "GetAllUsers", userId, userName, "Completed the  Fetching All Users from Users");

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Level2")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid User Id" });
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"{nameof(User)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "GetUserById", userId, userName, "Completed the GetUserById Operation On Users");

            return Ok(user);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = "AdminOwner")]
        public async Task<IActionResult> UpdateUserById(int id, [FromBody] UserDTO userDTO)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid User ID." });

            if (userDTO == null)
                return BadRequest(new { error = "Invalid User data provided." });
            var order = await _userService.UpdateUserByIdAsync(id, userDTO);
            if (order == null)
                return NotFound(new { message = $"{nameof(Order)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "UpdateUserById", userId, userName, "Completed the UpdateUserById Operation On Users");

            return Ok(new { message = "User updated successfully.", updatedOrder = order });
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "AdminOwner")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid User ID." });

            var isDeleted = await _userService.DeleteUserByIdAsync(id);

            if (!isDeleted)
                return NotFound(new { message = $"{nameof(Order)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "DeleteUserById", userId, userName, "Completed the DeleteUserById Operation On Users");


            return Ok(new { message = "User deleted successfully." });
        }
    }
}
