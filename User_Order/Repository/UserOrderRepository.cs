using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Repository.IRepository;

namespace User_Order.Repository
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public UserOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserOrder userOrder)
        {
            await _context.UserOrders.AddAsync(userOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserOrder>> GetAllAsync()
        {
            //var userOrders =  await _context.UserOrders
            //                .Include(uo => uo.User)
            //                .Include(uo => uo.Order)
            //                .ToListAsync();
            var userOrders= await _context.UserOrders.ToListAsync();
            return userOrders;
        }
    }
}
