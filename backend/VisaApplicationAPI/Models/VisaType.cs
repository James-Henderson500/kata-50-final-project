using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Represents the type of visa applied for:
//Tourist, Work, Student

namespace VisaApplicationAPI.Models;

public class VisaType
{
    public int Id { get; set; }
    public  string TypeName { get; set; }
}