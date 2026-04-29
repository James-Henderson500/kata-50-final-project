namespace VisaApplicationAPI.DTOs.VisaApplications;

// Data Transfer Object that returns a list of all visa applications.
// Contains the basic applicant and visa details required by the API.
public class VisaApplicationListDto
{
    public int Id { get; set; }
    public string ApplicantName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PassportNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public string Status { get; set; }
    public string VisaType { get; set; }


}
 