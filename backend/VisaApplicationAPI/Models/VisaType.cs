using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisaApp.Models;

public class VisaType
{
    [Key, Column("Id")]
    public int Id { get; set; }
    [Column("TypeName")]
    public required string TypeName { get; set; }
}