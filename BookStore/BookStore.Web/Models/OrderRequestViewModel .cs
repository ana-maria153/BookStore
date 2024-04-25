namespace BookStore.Web.Models;

public class OrderRequestViewModel
{
    public Guid OrderID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public List<OrderProductModel> OrderProducts { get; set; }
}
