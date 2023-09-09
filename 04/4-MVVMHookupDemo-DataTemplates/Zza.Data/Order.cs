using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class Order
{
    public long Id { get; set; }
    public Guid? StoreId { get; set; }
    public Guid CustomerId { get; set; }
    public int OrderStatusId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public decimal? DeliveryCharge { get; set; }
    public decimal ItemsTotal { get; set; }
    [MaxLength(100)]
    public string? Phone { get; set; }
    [MaxLength(100)]
    public string? DeliveryStreet { get; set; }
    [MaxLength(100)]
    public string? DeliveryCity { get; set; }
    [MaxLength(2)]
    public string? DeliveryState { get; set; }
    [MaxLength(10)]
    public string? DeliveryZip { get; set; }

    //public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public OrderStatus Status { get; set; } = null!;
}
