using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using User_Order.Model;
using User_Order.Model.DTO;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Service
{
    public class LogInUserService : ILogInUserService
    {
        private readonly ILogInUserRepository _logInUserRepository;
        private string? SecretKey;

        public LogInUserService(ILogInUserRepository logInUserRepository , IConfiguration configuration)
        {
            _logInUserRepository = logInUserRepository;
            SecretKey = configuration.GetValue<string>("ApiSettings:secret");
        }

       

        public async Task<bool> IsUniqueUser(string userName)
        {
            bool user= await _logInUserRepository.IsUniqueUser(userName);
            return user;
        }

        public async Task<LogInUserDetail> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            var logInUserDetail = new LogInUserDetail
            {
                LogInUserName = registrationRequestDTO.LogInUserName,
                UserName = registrationRequestDTO.UserName,
                Password = registrationRequestDTO.Password,
                Role = registrationRequestDTO.Role
            };
            var logInUserDetails=await _logInUserRepository.AddAsync(logInUserDetail);
            return logInUserDetails;
        }


        public async Task<IEnumerable<LogInUserDetail>> GetAllLogInUsers()
        {
            var users = await _logInUserRepository.GetAllAsync();
            return users ?? [];
        }

        public async Task<LogInResponseDTO> LogIn(LogInRequestDTO logInRequestDTO)
        {
            var user = await _logInUserRepository.GetAsync(logInRequestDTO);
            if(user==null)
            {
                return new LogInResponseDTO
                {
                    LogInUserDetail = null,
                    Token = ""
                };
            }

            //if User is Found
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey ?? "defaultKey");

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.LogInUserId.ToString()),
                    new Claim(ClaimTypes.Name , user.LogInUserName??"Unknown"),
                    new Claim(ClaimTypes.Role ,user.Role??"User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature )
            };

            var token = TokenHandler.CreateToken(tokenDescription);
            LogInResponseDTO logInResponseDTO = new LogInResponseDTO
            {
                LogInUserDetail = user,
                Token = TokenHandler.WriteToken(token),
            };
            return logInResponseDTO;
        }

        

    }
}
