using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisaApp.Models;

public class VisaApplication
{
    [Key, Column("Id")]
    public int Id { get; set; }
    [Column("ApplicantName")]
    public required string ApplicantName { get; set; }
    [Column("DateOfBirth")]
    public required DateOnly DateOfBirth { get; set; }
    [Column("PassportNumber")]
    public required string PassportNumber { get; set; }
    [Column("Nationality")]
    public required int Nationality { get; set; }
    [Column("ApplicationDate")]
    public required DateOnly ApplicationDate { get; set; }
    //Foreign keys
    public int CountryId { get; set; }
    public int StatusId { get; set; }
    public int VisaTypeId { get; set; }
    //Navigation properties
    public Country? Country { get; set; }
    public ApplicationStatus? ApplicationStatus { get; set; }
    public VisaType? VisaType { get; set; }
}