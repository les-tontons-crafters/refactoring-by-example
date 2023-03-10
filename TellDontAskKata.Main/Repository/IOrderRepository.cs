using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Repository
{
    public interface IOrderRepository
    {
        Order Save(Order order);

        Order GetById(int orderId);
    }
}