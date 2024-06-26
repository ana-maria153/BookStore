﻿using BookStore.BL.Interfaces;
using BookStore.DAL;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BL.Implementations;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _appDBContext;
    private readonly IBookService _bookService;

    public OrderService(ApplicationDbContext appDBContext, IBookService bookService)
    {
        _appDBContext = appDBContext;
        _bookService = bookService;
    }

    public void PlaceOrder(Guid ID, string Name, string Surename, string Address, string Phone, List<Tuple<string, int>> OrderItems)
    {
        var Order = new Order()
        {
            ID = ID,
            Name = Name,
            Surename = Surename,
            Addres = Address,
            Phone = Phone,
            OrderItems = OrderItems.Select(x => new OrderItem()
            {
                OrderItemID = Guid.NewGuid(),
                OrderItemBookID = _bookService.GetBook(x.Item1).ID,
                Quantity = x.Item2
            }).ToList()
        };
        _appDBContext.Orders.Add(Order);
        _appDBContext.SaveChanges();
    }

    public List<Order> GetOrders()
    {
        return _appDBContext.Orders.Include(x=>x.OrderItems).ToList();
    }
    public Order GetOrder(Guid ID)
    {
        return _appDBContext.Orders.Include(x => x.OrderItems).ThenInclude(x => x.OrderItemBook).FirstOrDefault(x => x.ID == ID);
    }
}
