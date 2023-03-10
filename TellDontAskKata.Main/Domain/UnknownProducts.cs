using System.Collections.Generic;

namespace TellDontAskKata.Main.Domain
{
    public record UnknownProducts(string Message) : DomainError
    {
        public UnknownProducts(IEnumerable<UnknownProduct> products)
            : this($"Missing product(s): {string.Join(",", products.Map(p => p.Product))}")
        {
        }
    }
}