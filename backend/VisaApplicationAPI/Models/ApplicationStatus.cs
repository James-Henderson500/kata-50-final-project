using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisaApp.Models;

public class ApplicationStatus
{
    [Key, Column("Id")]
    public int Id { get; set; }
    [Column("StatusName")]
    public required string StatusName {get; set; }
}