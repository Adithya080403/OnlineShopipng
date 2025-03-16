namespace User_Order.Model.DTO
{
    public class OrderDTO
    {
        public string OrderName { get; set; } = null!;

        public int OrderPrice { get; set; }

        public int OrderQuantity { get; set; }

        public string OrderStatus { get; set; } = null!;
    }
}
