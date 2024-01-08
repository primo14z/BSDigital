using BSDigital.Models;

namespace BSDigital.Service
{
    public interface IOrderService
    {
        public List<Order> GenerateOrder(OrderInput orderInput);
    }
}