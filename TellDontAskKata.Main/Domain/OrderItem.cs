using System;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public Product Product { get; private init; }
        public int Quantity { get; private init; }
        public decimal TaxedAmount { get; private init; }
        public decimal Tax { get; private init; }

        public static OrderItem New(CreateOrderItem itemRequest, Product product)
        {
            var unitaryTax = Round((product.Price / 100m) * product.Category.TaxPercentage);
            var unitaryTaxedAmount = Round(product.Price + unitaryTax);

            return new OrderItem
            {
                Product = product,
                Quantity = itemRequest.Quantity,
                Tax = Round(unitaryTax * itemRequest.Quantity),
                TaxedAmount = Round(unitaryTaxedAmount * itemRequest.Quantity)
            };
        }

        private static decimal Round(decimal amount) => decimal.Round(amount, 2, MidpointRounding.ToPositiveInfinity);
    }
}