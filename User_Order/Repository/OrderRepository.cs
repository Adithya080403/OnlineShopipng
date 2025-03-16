using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Repository.IRepository;

namespace User_Order.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var orders = await _context.Orders.FirstOrDefaultAsync(o=>o.OrderId==id);
            return orders;
        }

        public async  Task UpdateByIdAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteByIdAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}
