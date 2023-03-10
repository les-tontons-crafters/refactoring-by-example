using LanguageExt;
using TellDontAskKata.Main.Commands;
using TellDontAskKata.Main.Repository;
using static LanguageExt.Prelude;

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

        public static Either<UnknownProduct, OrderItem> NewOrderItem(
            IProductCatalog productCatalog,
            CreateOrderItem itemRequest)
        {
            var product = productCatalog.GetByName(itemRequest.Name);

            return product == null
                ? Left(new UnknownProduct(itemRequest.Name))
                : new OrderItem(product, itemRequest.Quantity);
        }
    }
}