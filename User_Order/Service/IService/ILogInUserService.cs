using User_Order.Model;
using User_Order.Model.DTO;

namespace User_Order.Service.IService
{
    public interface ILogInUserService
    {
        Task<bool> IsUniqueUser(string userName);

        Task<IEnumerable<LogInUserDetail>> GetAllLogInUsers();

        Task<LogInUserDetail> Register(RegistrationRequestDTO registrationRequestDTO);

        Task<LogInResponseDTO> LogIn(LogInRequestDTO logInRequestDTO);

        
    }
}
