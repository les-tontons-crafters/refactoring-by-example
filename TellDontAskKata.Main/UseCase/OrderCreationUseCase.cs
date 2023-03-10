using System.Collections.Generic;
using LanguageExt;
using TellDontAskKata.Main.Commands;
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

        public void Run(IEnumerable<CreateOrderItem> items)
            => _orderRepository.Save
            (
                Order.New(_productCatalog, items)
            );

        public Either<UnknownProductException, Order> RunWithEither(IEnumerable<CreateOrderItem> items)
            => Order.NewWithEither(_productCatalog, items)
                .Map(order => _orderRepository.Save(order));
    }
}