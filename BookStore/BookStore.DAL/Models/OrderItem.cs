using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models;

public class OrderItem
{
    [Key]
    public Guid OrderItemID { get; set; }

    public Guid OrderItemBookID { get; set; }
    public Book? OrderItemBook { get; set; }

    public int Quantity { get; set; }
}
