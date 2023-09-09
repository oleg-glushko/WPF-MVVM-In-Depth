using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class Customer
{
    public Guid Id { get; set; }
    public Guid? StoreId { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    public string FullName => FirstName + " " + LastName;
    [MaxLength(100)]
    public string? Phone { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
    [MaxLength(100)]
    public string? Street { get; set; }
    [MaxLength(100)]
    public string? City { get; set; }
    [MaxLength(2)]
    public string? State { get; set; }
    [MaxLength(10)]
    public string? Zip { get; set; }
    public List<Order> Orders { get; set; } = new();
}
