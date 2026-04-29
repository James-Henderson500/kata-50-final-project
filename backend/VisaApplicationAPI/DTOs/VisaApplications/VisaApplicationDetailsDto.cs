namespace VisaApplicationAPI.DTOs.VisaApplications;

// Data Transfer Object used to retrieve a specific visa application.
// Contains the basic applicant and visa details required by the API.
public class VisaApplicationDetailsDto
{
    public int Id { get; set; }
    public string ApplicantName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PassportNumber { get; set; }
    public string Nationality { get; set; }
    public string VisaType { get; set; }
    public string Status { get; set; }
    public DateTime ApplicationDate { get; set; }
} 