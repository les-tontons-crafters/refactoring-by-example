using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

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

        public void Run(HashSet<CreateOrderItem> items)
        {
            var order = Order.New();

            foreach (var itemRequest in items)
            {
                var orderItem = ToOrderItem(itemRequest);

                order.Items.Add(orderItem);
                order.Total += orderItem.TaxedAmount;
                order.Tax += orderItem.Tax;
            }

            _orderRepository.Save(order);
        }

        private OrderItem ToOrderItem(CreateOrderItem itemRequest)
        {
            var product = _productCatalog.GetByName(itemRequest.Name);

            if (product == null)
            {
                throw new UnknownProductException();
            }

            var unitaryTax = Round((product.Price / 100m) * product.Category.TaxPercentage);
            var unitaryTaxedAmount = Round(product.Price + unitaryTax);
            var taxedAmount = Round(unitaryTaxedAmount * itemRequest.Quantity);
            var taxAmount = Round(unitaryTax * itemRequest.Quantity);

            var orderItem = new OrderItem
            {
                Product = product,
                Quantity = itemRequest.Quantity,
                Tax = taxAmount,
                TaxedAmount = taxedAmount
            };
            return orderItem;
        }

        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }

    /// <summary>
    /// Represents an item dedicated to order creation.
    /// </summary>
    /// <param name="Name">The name of the item.</param>
    /// <param name="Quantity">The quantity of the item.</param>
    public record CreateOrderItem(string Name, int Quantity);
}