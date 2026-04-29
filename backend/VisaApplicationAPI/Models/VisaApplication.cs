using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Represents a visa application submitted by an applicant.

namespace VisaApplicationAPI.Models;

public class VisaApplication
{
    //Primary key
    public int Id { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public  string PassportNumber { get; set; } = string.Empty;
    public  DateTime ApplicationDate { get; set; }

    //Foreign keys & navigation properties
    public int CountryId { get; set; }
    public Country Nationality { get; set; }
    public int ApplicationStatusId { get; set; }
    public ApplicationStatus Status {get; set; }
    public int VisaTypeId { get; set; }
    public VisaType VisaType { get; set; }
}