using System.ComponentModel.DataAnnotations;

namespace Zza.Data;

public class Product
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(255)]
    public string? Description { get; set; }
    [MaxLength(50)]
    public string? Image { get; set; }
    public bool HasOptions { get; set; }
    public bool? IsVegetarian { get; set; }
    public bool? WithTomatoSauce { get; set; }
    [MaxLength(10)]
    public string? SizeIds { get; set; }
    //[Timestamp]
    //public byte[] RowVersion { get; set; }
}
