namespace BookStore.DAL.Models;

public class Order
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Surename { get; set; }
    public string Addres { get; set; }
    public string Phone { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
