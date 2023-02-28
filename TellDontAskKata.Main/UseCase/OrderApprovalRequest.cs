namespace TellDontAskKata.Main.UseCase
{
    public class OrderApprovalRequest
    {
        public int OrderId { get; init; }
        public bool Approved { get; init; }
    }
}
