using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Represents the status of a visa application:
// New, Review, Approved, Rejected

namespace VisaApplicationAPI.Models;

public class ApplicationStatus
{
    //Primary key
    public int Id { get; set; }
    public string StatusName {get; set; }
}