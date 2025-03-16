namespace User_Order.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public long UserPhoneNumber { get; set; }

        public string UserAddress { get; set; } = null!;

        public ICollection<UserOrder> UserOrder { get; set; } = [];
    }
}
