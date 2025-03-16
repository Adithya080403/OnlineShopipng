namespace User_Order.Model.DTO
{
    public class LogInResponseDTO
    {
        public LogInUserDetail LogInUserDetail { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
