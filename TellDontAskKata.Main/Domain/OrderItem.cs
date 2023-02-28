namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public Product Product { get; init; }
        public int Quantity { get; init; }
        public decimal TaxedAmount { get; init; }
        public decimal Tax { get; init; }
    }
}
