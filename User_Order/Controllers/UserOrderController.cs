
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using User_Order.Service.IService;

namespace User_Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService _userOrderService;

        public UserOrderController(IUserOrderService userOrderService)
        {
             _userOrderService= userOrderService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUserOrder()
        {
            var userOrders = await _userOrderService.GetAllUserOrderAsync();
            if (userOrders == null || !userOrders.Any())
                return NotFound("No UserOrder data found");
            return Ok(userOrders);
        }
        
    }
}
