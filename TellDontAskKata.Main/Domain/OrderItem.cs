using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;

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

        public static OrderItem NewOrderItem(IProductCatalog productCatalog, CreateOrderItem itemRequest)
        {
            var product = productCatalog.GetByName(itemRequest.Name);

            if (product == null)
            {
                throw new UnknownProductException();
            }

            return new OrderItem(product, itemRequest.Quantity);
        }
    }
}