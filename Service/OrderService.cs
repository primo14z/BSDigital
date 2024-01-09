using BSDigital.Models;
using Newtonsoft.Json;

namespace BSDigital.Service
{
    public class OrderService : IOrderService
    {
        public List<Order> GenerateOrder(OrderInput orderInput)
        {
            List<Order> outputOrders = new List<Order>();

            OrderBook orderBooks = JsonConvert.DeserializeObject<OrderBook>(File.ReadAllText("orderBooks.json"));
            BalanceConstraint balanceConstraint = JsonConvert.DeserializeObject<BalanceConstraint>(File.ReadAllText("balanceConstraint.json"));

            if(orderBooks == null || balanceConstraint == null)
                throw new Exception("Json files are empty");
            
            var feasibleOrderBooks = new List<Order>();

            // Sort the order books by balance constraints so the most "optimal" is taken first
            if (orderInput.Type == "Buy")
            {
                feasibleOrderBooks = orderBooks.Bids.Where(x => x.Price <= balanceConstraint.EUR).OrderBy(x => x.Price).ToList();
            }
            else if (orderInput.Type == "Sell")
            {
                feasibleOrderBooks = orderBooks.Asks.Where(x => x.Price <= balanceConstraint.EUR).OrderBy(x => x.Price).ToList();
            }

            // Analyze feasible orders and generate output orders
            foreach (var orderBook in feasibleOrderBooks)
            {
                // Determine feasible amount based on balance constraints and order input
                double feasibleAmount = (orderInput.Type == "Buy") ? Math.Min(balanceConstraint.EUR / orderBook.Price, orderInput.Amount) :
                                                                      Math.Min(balanceConstraint.BTC, orderInput.Amount);

                if (feasibleAmount > 0)
                {
                    // Generate output order
                    outputOrders.Add(new Order
                    {
                        Type = orderInput.Type,
                        Amount = feasibleAmount,
                        Price = (orderInput.Type == "Buy") ? orderBook.Price : orderBook.Price
                    });

                    // Update balance constraints based on executed order
                    balanceConstraint.EUR -= (orderInput.Type == "Buy") ? feasibleAmount * orderBook.Price : 0;
                    balanceConstraint.BTC -= (orderInput.Type == "Sell") ? feasibleAmount : 0;

                    // Update remaining amount based on executed order
                    orderInput.Amount -= feasibleAmount;

                    // Break the loop if the order input is fully executed
                    if (orderInput.Amount <= 0) break;
                }
            }

            return outputOrders;
        }
    }
}