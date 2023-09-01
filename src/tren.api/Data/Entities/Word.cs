using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tren.api.Data.Entities;

[Table("words")]
public class Word
{
    [Column("id")]
    public int Id { get; set; }
    [Column("turkish")]
    public string Turkish { get; set; }
    [Column("english")]
    public string English { get; set; }
    [Column("category")]
    public string Category { get; set; }
}