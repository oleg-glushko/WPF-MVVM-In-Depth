using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class OrderItem
{
    public long Id { get; set; }
    public Guid? StoreId { get; set; }
    public long OrderId { get; set; }
    public int ProductId { get; set; }
    public int ProductSizeId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    [MaxLength(100)]
    public string? Instructions { get; set; }

    public List<OrderItemOption> Options { get; set; } = new();
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public ProductSize Size { get; set; } = null!;
}
