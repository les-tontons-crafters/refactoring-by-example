using System.Collections.Generic;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total { get; set; }
        public string Currency { get; init; }
        public IList<OrderItem> Items { get; init; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; init; }
    }
}
