using System.ComponentModel.DataAnnotations;

namespace VisaApplicationAPI.DTOs.VisaApplications;

// Data Transfer Object used when creating a new visa application.
// Contains the basic applicant and visa details required by the API.

public class CreateVisaApplicationDto
{
    [Required]
    public string ApplicantName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string PassportNumber { get; set; }
    [Required]
    public string Nationality { get; set; }
    [Required]
    public string VisaType { get; set; }
}