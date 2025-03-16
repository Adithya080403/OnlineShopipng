using User_Order.Model;
using User_Order.Model.DTO;
using User_Order.Repository;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOrderService _userOrderService;

        public UserService(IUserRepository userRepository , IUserOrderService userOrderService)
        {
            _userRepository = userRepository;
            _userOrderService = userOrderService;

        }
        public async Task<User> AddUserAsync(UserDTO userDTO)
        {
            var user = new User
            {
                UserName = userDTO.UserName,
                UserAddress=userDTO.UserAddress,
                UserEmail = userDTO.UserEmail,
                UserPhoneNumber=userDTO.UserPhoneNumber
            };

            // Save user first
            var savedUser = await _userRepository.AddSync(user);

            // Ensure user is saved before adding orders
            if (savedUser == null || savedUser.UserId <= 0)
            {
                throw new Exception("Failed to create user.");
            }

            // Add orders if provided
            if (userDTO.OrderIds != null && userDTO.OrderIds.Any())
            {
                foreach (var orderId in userDTO.OrderIds)
                {
                    var userOrderDTO = new UserOrderDTO
                    {
                        UserId = savedUser.UserId,
                        OrderId = orderId
                    };

                    try
                    {
                        await _userOrderService.AddUserOrderAsync(userOrderDTO);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to add UserOrder for OrderId {orderId}: {ex.Message}");
                        throw;
                    }
                }
            }

            return savedUser;
        
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var users=await _userRepository.GetAllSync();
            return users ?? [];
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<User?> UpdateUserByIdAsync(int id, UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            user.UserName = userDTO.UserName;
            user.UserPhoneNumber = userDTO.UserPhoneNumber;
            user.UserEmail = userDTO.UserEmail;
            user.UserAddress = userDTO.UserAddress;
            await _userRepository.UpdateByIdAsync(user);

            return user;
        }


        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;
            await _userRepository.DeleteByIdAsync(user);
            return true;
        }

    }
}
