using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Repository;
using static LanguageExt.Prelude;
using static TellDontAskKata.Main.Domain.OrderItem;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        private Seq<OrderItem> _items;
        public decimal Total { get; private set; }
        public string Currency { get; }
        public Seq<OrderItem> Items => _items;
        public decimal Tax { get; private set; }
        public OrderStatus Status { get; set; }
        public int Id { get; init; }

        // Should be private
        public Order()
        {
            Status = OrderStatus.Created;
            _items = new Seq<OrderItem>();
            Currency = "EUR";
            Total = 0m;
            Tax = 0m;
        }

        // Does it make sense to be able to create an empty Order?
        public static Either<UnknownProducts, Order> New(
            IProductCatalog productCatalog,
            IEnumerable<CreateOrderItem> items)
        {
            var orderItems =
                items.Map(createOrderItem => NewOrderItem(productCatalog, createOrderItem)).ToArray();

            return ContainsFailure(orderItems)
                ? ToFailure(orderItems.Lefts())
                : ToSuccess(orderItems.Rights());
        }

        private static bool ContainsFailure(IEnumerable<Either<UnknownProduct, OrderItem>> orderItems) =>
            orderItems.Lefts().Any();

        private static EitherLeft<UnknownProducts> ToFailure(IEnumerable<UnknownProduct> unknownProducts) =>
            Left(new UnknownProducts(unknownProducts));

        private static Order ToSuccess(IEnumerable<OrderItem> orderItems)
            => orderItems.Fold(new Order(), (order, item) => order.AddItem(item));

        private Order AddItem(OrderItem orderItem)
        {
            _items = _items.Add(orderItem);
            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;

            return this;
        }
    }
}