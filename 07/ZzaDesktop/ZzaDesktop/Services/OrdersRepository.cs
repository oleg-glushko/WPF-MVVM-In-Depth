﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Zza.Data;

namespace ZzaDesktop.Services;

public class OrdersRepository : IOrdersRepository
{
    private readonly ZzaDbContext _context;

    public OrdersRepository(ZzaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<List<Order>> GetOrdersForCustomersAsync(Guid customerId)
    {
        return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        if (!_context.Orders.Local.Any(o => o.Id == order.Id))
        {
            _context.Orders.Attach(order);
        }
        _context.Entry(order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        using TransactionScope scope = new();
        var order = _context.Orders.Include("OrderItems").Include("OrderItems.OrderItemOptions").FirstOrDefault(o => o.Id == orderId);
        if (order != null)
        {
            foreach (OrderItem item in order.OrderItems)
            {
                foreach (var orderItemOption in item.Options)
                {
                    _context.OrderItemOptions.Remove(orderItemOption);
                }
                _context.OrderItems.Remove(item);
            }
            _context.Orders.Remove(order);
        }
        await _context.SaveChangesAsync();
        scope.Complete();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<ProductOption>> GetProductOptionsAsync()
    {
        return await _context.ProductOptions.ToListAsync();
    }

    public async Task<List<ProductSize>> GetProductSizesAsync()
    {
        return await _context.ProductSizes.ToListAsync();
    }

    public async Task<List<OrderStatus>> GetOrderStatusesAsync()
    {
        return await _context.OrderStatuses.ToListAsync();
    }
}
