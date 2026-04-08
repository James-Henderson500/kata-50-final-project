using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisaApp.Models;

public class Country
{
    [Key, Column("Id")]
    public int Id { get; set; }
    [Column("CountryCode")]
    public required string CountryCode { get; set;}
    [Column("CountryName")]
    public required string CountryName {get; set; }
    public ICollection<VisaApplication> VisaApplication { get; set; }
}

