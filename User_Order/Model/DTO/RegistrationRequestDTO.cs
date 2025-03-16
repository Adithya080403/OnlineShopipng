namespace User_Order.Model.DTO
{
    public class RegistrationRequestDTO
    {
        public string LogInUserName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
