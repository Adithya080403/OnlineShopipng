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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuditLogService _auditLogService;

        public OrderController(IOrderService orderService, IAuditLogService auditLogService)
        {
            _orderService = orderService;
            _auditLogService = auditLogService;
        }

        [HttpPost]
        //[Authorize(Roles ="Level1")]
        public async Task<IActionResult> AddOrder([FromBody]OrderDTO orderDTO)
        {
            if (orderDTO == null)
                return BadRequest(new { error = "The Order data is Invalid Or Empty" });
            var order=await _orderService.AddOrderAsync(orderDTO);
            if (order == null)
                return BadRequest(new { error = "Failed to create order. Please try again later." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", order.OrderId.ToString(), "AddOrder", userId, userName, "Completed the AddOrder Operation On Order");

            return Ok(order);
        }

        [HttpGet]
        //[Authorize(Roles = "Level1")]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _orderService.GetAllOrderAsync();
            if (orders == null || !orders.Any())
                return NotFound("No Order data found");

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", "Get", "GetAllOrder", userId, userName, "Completed the GetAllOrder Operation On Order");

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        //[Authorize(Roles = "Level2")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid Order Id" });
                var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"{nameof(Order)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "GetOrderById", userId, userName, "Completed the GetOrderById Operation On Order");

            return Ok(order);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = "AdminOwner")]
        public async Task<IActionResult> UpdateOrderById(int id , [FromBody]OrderDTO orderDTO)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid Order ID." });

            if (orderDTO == null)
                return BadRequest(new { error = "Invalid Order data provided." });
            var order = await _orderService.UpdateOrderByIdAsync(id , orderDTO);
            if (order == null)
                return NotFound(new { message = $"{nameof(Order)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "UpdateOrderById", userId, userName, "Completed the UpdateOrderById Operation On Order");

            return Ok(new { message = "Order updated successfully.", updatedOrder = order });
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "AdminOwner")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            if (id <= 0)
                return BadRequest(new { error = "Invalid Order ID." });

            var isDeleted = await _orderService.DeleteOrderByIdAsync(id);

            if (!isDeleted)
                return NotFound(new { message = $"{nameof(Order)} not found with ID {id}." });

            //Log tha Audit trail
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            await _auditLogService.AddAuditLogAsync("User", id.ToString(), "DeleteOrderById", userId, userName, "Completed the DeleteOrderById Operation On Order");


            return Ok(new { message = "Order deleted successfully." });
        }
    }
}

