using System.Collections.Generic;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; }
        public IList<OrderItem> Items { get; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; init; }

        public Order()
        {
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
            Currency = "EUR";
            Total = 0m;
            Tax = 0m;
        }

        public static Order New() => new();
    }
}