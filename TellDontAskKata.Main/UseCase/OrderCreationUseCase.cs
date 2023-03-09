using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using static TellDontAskKata.Main.Domain.OrderStatus;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;

        public OrderCreationUseCase(
            IOrderRepository orderRepository,
            IProductCatalog productCatalog)
        {
            _orderRepository = orderRepository;
            _productCatalog = productCatalog;
        }

        public void Run(Dictionary<string, int> items)
        {
            var order = new Order
            {
                Status = Created,
                Items = new List<OrderItem>(),
                Currency = "EUR",
                Total = 0m,
                Tax = 0m
            };

            foreach (var itemRequest in items)
            {
                var product = _productCatalog.GetByName(itemRequest.Key);

                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else
                {
                    var unitaryTax = Round((product.Price / 100m) * product.Category.TaxPercentage);
                    var unitaryTaxedAmount = Round(product.Price + unitaryTax);
                    var taxedAmount = Round(unitaryTaxedAmount * itemRequest.Value);
                    var taxAmount = Round(unitaryTax * itemRequest.Value);

                    var orderItem = new OrderItem
                    {
                        Product = product,
                        Quantity = itemRequest.Value,
                        Tax = taxAmount,
                        TaxedAmount = taxedAmount
                    };
                    order.Items.Add(orderItem);
                    order.Total += taxedAmount;
                    order.Tax += taxAmount;
                }
            }

            _orderRepository.Save(order);
        }

        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }
}