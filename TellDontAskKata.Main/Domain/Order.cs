using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Repository;
using static TellDontAskKata.Main.Domain.OrderItem;

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

        // Should be private
        public Order()
        {
            Status = OrderStatus.Created;
            _items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0m;
            Tax = 0m;
        }

        // Does it make sense to be able to create an empty Order?
        public static Order New(IProductCatalog productCatalog, IEnumerable<CreateOrderItem> items)
        {
            var order = new Order();

            items
                .Select(createOrderItem => NewOrderItem(productCatalog, createOrderItem))
                .ToList()
                .ForEach(item => order.AddItem(item));

            return order;
        }

        private void AddItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;
        }
    }
}