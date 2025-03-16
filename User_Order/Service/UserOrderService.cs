using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Model.DTO;
using User_Order.Repository;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Service
{
    public class UserOrderService : IUserOrderService
    {
        private readonly IUserOrderRepository _userOrderRepository;
        private readonly ApplicationDbContext _context;

        public UserOrderService(IUserOrderRepository userOrderRepository, ApplicationDbContext context)
        {
            _userOrderRepository = userOrderRepository;
            _context = context;
        }

        public async Task<bool> AddUserOrderAsync(UserOrderDTO userOrderDTO)
        {
            // Check if User exists
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userOrderDTO.UserId);
            if (!userExists)
            {
                throw new InvalidOperationException($"User with ID {userOrderDTO.UserId} does not exist.");
            }

            // Check if Order exists
            var orderExists = await _context.Orders.AnyAsync(o => o.OrderId == userOrderDTO.OrderId);
            if (!orderExists)
            {
                throw new InvalidOperationException($"Order with ID {userOrderDTO.OrderId} does not exist.");
            }

            var userOrder = new UserOrder
            {
                UserId = userOrderDTO.UserId,
                OrderId = userOrderDTO.OrderId,
            };

            await _userOrderRepository.AddAsync(userOrder);
            return true;
        }

        public async Task<IEnumerable<UserOrder>> GetAllUserOrderAsync()
        {
            var userOrder=await _userOrderRepository.GetAllAsync();
            return  userOrder ?? []; //An empty list([])(if repository returns null)
        }
    }
}
