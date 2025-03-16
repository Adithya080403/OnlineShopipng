using System.ComponentModel.DataAnnotations;

namespace User_Order.Model
{
    public class LogInUserDetail
    {
        [Key]
        public int LogInUserId { get; set; }

        public string LogInUserName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
