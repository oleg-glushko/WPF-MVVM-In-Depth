using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class ProductOption
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public int Factor { get; set; }
    public bool IsPizzaOption { get; set; }
    public bool IsSaladOption { get; set; }
}
