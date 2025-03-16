namespace User_Order.Model.DTO
{
    public class UserDTO
    {

        public string UserName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public long UserPhoneNumber { get; set; }

        public string UserAddress { get; set; } = null!;

        public List<int> OrderIds { get; set; } = [];
    }
}
