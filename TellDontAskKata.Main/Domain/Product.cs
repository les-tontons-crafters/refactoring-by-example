namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public Category Category { get; init; }

        public decimal UnitaryTax() => ((Price / 100m) * Category.TaxPercentage).Round();
        public decimal UnitaryTaxedAmount() => (Price + UnitaryTax()).Round();
    }
}