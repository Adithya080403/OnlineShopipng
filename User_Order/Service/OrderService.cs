using User_Order.Model.DTO;
using User_Order.Model;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Service
{
    public class OrderService :IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> AddOrderAsync(OrderDTO OrderDTO)
        {
            var order = new Order
            {
              OrderName=OrderDTO.OrderName,
              OrderPrice=OrderDTO.OrderPrice,
              OrderStatus=OrderDTO.OrderStatus,
              OrderQuantity=OrderDTO.OrderQuantity
            };
            var orderDto=await _orderRepository.AddAsync(order);
            return orderDto;

        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders ?? [];  //An empty list([])(if repository returns null)
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order;
        }

        public async Task<Order?> UpdateOrderByIdAsync(int id, OrderDTO orderDTO)
        {
            var order= await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return null;

            order.OrderName = orderDTO.OrderName;
            order.OrderPrice = orderDTO.OrderPrice;
            order.OrderStatus = orderDTO.OrderStatus;
            order.OrderQuantity = orderDTO.OrderQuantity;
            await _orderRepository.UpdateByIdAsync(order);

            return order;
        }


        public async Task<bool> DeleteOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return false;
            await _orderRepository.DeleteByIdAsync(order);
            return true;
        }

    }
}
