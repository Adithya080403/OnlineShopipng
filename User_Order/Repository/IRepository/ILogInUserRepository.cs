using User_Order.Model;
using User_Order.Model.DTO;

namespace User_Order.Repository.IRepository
{
    public interface ILogInUserRepository
    {
        Task<bool> IsUniqueUser(string UserName);

        Task<LogInUserDetail> AddAsync(LogInUserDetail logInUserDetails);

        Task<LogInUserDetail?> GetAsync(LogInRequestDTO loginRequestDTO);

        Task<IEnumerable<LogInUserDetail>> GetAllAsync();
    }
}
