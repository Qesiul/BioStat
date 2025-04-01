using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioStat.MVVM.Model;

public class Measurement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column(TypeName = "TEXT")]
    public DateTime Date { get; set; }

    [MaxLength(5)] 
    public string Type { get; set; } = string.Empty;
    
    public double Value { get; set; }
    
    [MaxLength(10)]
    public string? Unit { get; set; }
    
    
}