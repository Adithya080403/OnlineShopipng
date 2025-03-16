using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Repository.IRepository;

namespace User_Order.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<User> AddSync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        

        public async Task<IEnumerable<User>> GetAllSync()
        {
            var users = await _context.Users
                .Include(u => u.UserOrder)
                .ThenInclude(uo => uo.Order)
                .ToListAsync();

            foreach(var user in users)
            {
                user.UserOrder = user.UserOrder
                    .GroupBy(uo => uo.OrderId)
                    .Select(g => g.First())
                    .ToList();
            }

            return users;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u=>u.UserOrder)
                .ThenInclude(uo=>uo.Order)
                .FirstOrDefaultAsync(o => o.UserId == id);

            if(user==null)
                return null;
         
            user.UserOrder = user.UserOrder
                    .GroupBy(uo => uo.OrderId)
                    .Select(g => g.First())
                    .ToList();
            return user;
        }

        public async Task UpdateByIdAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
