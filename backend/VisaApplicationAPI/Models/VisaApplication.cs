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
    [Column("StatusId")]
    public required int StatusId {get; set; }
    [Column("VisaTypeId")]
    public required int VisaTypeId {get; set; }
    [ForeignKey("Id")]
    public Country? Country { get; set; }
    [ForeignKey("Id")]
    public ApplicationStatus? ApplicationStatus { get; set; }
    [ForeignKey("Id")]
    public VisaType? VisaType {get; set; }
}