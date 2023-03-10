using System.Collections.Generic;
using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        private readonly List<OrderItem> _items;
        public decimal Total { get; private set; }
        public string Currency { get; }
        public IReadOnlyList<OrderItem> Items => _items.ToArray();
        public decimal Tax { get; private set; }
        public OrderStatus Status { get; set; }
        public int Id { get; init; }

        public Order()
        {
            Status = OrderStatus.Created;
            _items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0m;
            Tax = 0m;
        }

        public static Order New(IProductCatalog productCatalog, HashSet<CreateOrderItem> items)
        {
            var order = new Order();

            foreach (var itemRequest in items)
            {
                order.AddItem(ToOrderItem(productCatalog, itemRequest));
            }

            return order;
        }

        private static OrderItem ToOrderItem(IProductCatalog productCatalog, CreateOrderItem itemRequest)
        {
            var product = productCatalog.GetByName(itemRequest.Name);

            if (product == null)
            {
                throw new UnknownProductException();
            }

            return OrderItem.New(itemRequest, product);
        }

        private void AddItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;
        }
    }
}