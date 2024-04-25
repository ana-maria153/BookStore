namespace BookStore.BL.Interfaces;

public interface IOrderService
{
    public void PlaceOrder(Guid ID, string Name, string Surename, string Address, string Phone, List<Tuple<string, int>> OrderItems);
}
