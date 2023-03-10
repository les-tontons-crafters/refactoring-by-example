using System;
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

            var unitaryTax = Round((product.Price / 100m) * product.Category.TaxPercentage);
            var unitaryTaxedAmount = Round(product.Price + unitaryTax);

            Tax = Round(unitaryTax * quantity);
            TaxedAmount = Round(unitaryTaxedAmount * quantity);
        }


        public static OrderItem New(CreateOrderItem itemRequest, Product product)
            => new(product, itemRequest.Quantity);

        private static decimal Round(decimal amount) => decimal.Round(amount, 2, MidpointRounding.ToPositiveInfinity);
    }
}