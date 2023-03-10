using System.Collections.Generic;
using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using static TellDontAskKata.Main.Domain.OrderItem;

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

            return New(itemRequest, product);
        }
    }
}