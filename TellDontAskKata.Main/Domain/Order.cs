using System.Collections.Generic;

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

        public static Order New() => new();

        public void AddItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;
        }
    }
}