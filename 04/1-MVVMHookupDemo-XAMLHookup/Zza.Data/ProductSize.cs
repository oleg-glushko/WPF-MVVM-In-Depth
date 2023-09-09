using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class ProductSize
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? PremiumPrice { get; set; }
    public decimal? ToppingPrice { get; set; }
    public bool? IsGlutenFree { get; set; }
}
