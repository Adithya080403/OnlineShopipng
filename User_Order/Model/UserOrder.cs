using System.Text.Json.Serialization;

namespace User_Order.Model
{
    public class UserOrder
    {
       
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; } = null!;
     
        public int OrderId { get; set; }

        public Order Order { get; set; } = null!;
    }
}
