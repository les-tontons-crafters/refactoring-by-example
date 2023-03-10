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

        public static OrderItem NewOrderItem(IProductCatalog productCatalog, CreateOrderItem itemRequest)
        {
            var product = productCatalog.GetByName(itemRequest.Name);

            if (product == null)
            {
                throw new UnknownProductException();
            }

            return new OrderItem(product, itemRequest.Quantity);
        }

        public static Either<UnknownProductException, OrderItem> NewOrderItemWithEither(
            IProductCatalog productCatalog,
            CreateOrderItem itemRequest)
        {
            var product = productCatalog.GetByName(itemRequest.Name);

            return product == null
                ? Left(new UnknownProductException())
                : new OrderItem(product, itemRequest.Quantity);
        }
    }
}