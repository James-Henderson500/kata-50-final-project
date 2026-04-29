using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Represents the country associated with an applicant

namespace VisaApplicationAPI.Models;

public class Country
{
    //Primary key
    public int Id { get; set; }
    //2 character short code to identify country
    //e.g. GB - Great Britain
    public string CountryCode { get; set; }
    public string CountryName {get; set; }
}

