using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Model.DTO;
using User_Order.Repository.IRepository;

namespace User_Order.Repository
{
    public class LogInUserRepository : ILogInUserRepository
    {

        private readonly ApplicationDbContext _context;

        public LogInUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LogInUserDetail> AddAsync(LogInUserDetail logInUserDetails)
        {
            await _context.LogInUserDetails.AddAsync(logInUserDetails);
            await _context.SaveChangesAsync();
            return logInUserDetails;
        }

        public async Task<IEnumerable<LogInUserDetail>> GetAllAsync()
        {
            var logInUserDetail = await _context.LogInUserDetails.ToListAsync();
            return logInUserDetail;
        }

        public Task<LogInUserDetail?> GetAsync(LogInRequestDTO loginRequestDTO)
        {
            var logInUserDetail = _context.LogInUserDetails.FirstOrDefaultAsync(u => u.UserName == loginRequestDTO.UserName && u.Password == loginRequestDTO.Password);
            return logInUserDetail;
        }

        public  async Task<bool> IsUniqueUser(string userName)
        {
             var user=await _context.LogInUserDetails.FirstOrDefaultAsync(u => u.UserName == userName);
            return user == null;
          
        }
    }
}
