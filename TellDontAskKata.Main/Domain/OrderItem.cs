using TellDontAskKata.Main.Commands;

namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public decimal TaxedAmount { get; }
        public decimal Tax { get; }

        private OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            Tax = (product.UnitaryTax() * quantity).Round();
            TaxedAmount = (product.UnitaryTaxedAmount() * quantity).Round();
        }


        public static OrderItem New(CreateOrderItem itemRequest, Product product)
            => new(product, itemRequest.Quantity);
    }
}