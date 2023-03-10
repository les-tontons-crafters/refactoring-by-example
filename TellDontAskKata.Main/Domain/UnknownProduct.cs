namespace TellDontAskKata.Main.Domain
{
    public record UnknownProduct(string Product) : DomainError;
}