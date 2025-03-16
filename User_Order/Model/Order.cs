using System.Text.Json.Serialization;

namespace User_Order.Model
{
    public class Order
    {

        public int OrderId { get; set; }

        public string OrderName { get; set; } = null!;

        public int OrderPrice { get; set; }

        public int OrderQuantity { get; set; }

        public string OrderStatus { get; set; } = null!;

        [JsonIgnore]
        public ICollection<UserOrder> UserOrder { get; set; } = [];
    }
}
