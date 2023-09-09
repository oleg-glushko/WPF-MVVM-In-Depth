using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class OrderStatus
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}
