using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisaApplicationAPI.Data;
using VisaApplicationAPI.Models;
using VisaApplicationAPI.DTOs.VisaApplications;

namespace VisaApplicationAPI.Controllers;

// This controller exposes REST API endpoints for managing visa applications.
// It supports creating new applications, retrieving individual applications,
// and listing applications with optional filtering by status and visa type.
// The controller uses Entity Framework Core to work with the database 
// and uses DTOs so the API only returns the data it needs, without exposing the internal database models.

[ApiController]
[Route("api/visa-applications")]
public class VisaApplicationsController : ControllerBase
{
    //Database context used to access application data
    private readonly AppDbContext _context;

    public VisaApplicationsController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/visa-applications
    //Returns a list of all visa applications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisaApplicationListDto>>> GetAll(
        [FromQuery] string? status,
        [FromQuery] string? visaType)
    {
        //Initial query including related lookup data
        var query = _context.VisaApplications
            .Include(v => v.Nationality)
            .Include(v => v.Status)
            .Include(v => v.VisaType)
            .AsQueryable();

        //Filter by Status
        if (!string.IsNullOrEmpty(status))
            query = query.Where(v => v.Status.StatusName == status);

        //Filter by Visa Type
        if (!string.IsNullOrEmpty(visaType))
            query = query.Where(v => v.VisaType.TypeName == visaType);

        //Default sort (most recent first)
        query = query.OrderByDescending(v => v.ApplicationDate);

        //Return data (AC001) in DTO
        var results = await query
            .Select(v => new VisaApplicationListDto
            {
                Id = v.Id,
                ApplicantName = v.ApplicantName,
                DateOfBirth = v.DateOfBirth,
                PassportNumber = v.PassportNumber,
                Nationality = v.Nationality.CountryCode,
                ApplicationDate = v.ApplicationDate,
                Status = v.Status.StatusName,
                VisaType = v.VisaType.TypeName
            })
            .ToListAsync();
        return Ok(results);
    }

    // GET /api/visa-applications/{id}
    //Returns information for a single visa application
    [HttpGet("{id}")]
    public async Task<ActionResult<VisaApplicationDetailsDto>> GetById(int id)
    {
        //Retrieve application with related data
        var visa = await _context.VisaApplications
            .Include(v => v.Nationality)
            .Include(v => v.Status)
            .Include(v => v.VisaType)
            .FirstOrDefaultAsync(v => v.Id == id);

        //Return error if not found
        if (visa == null)
            return NotFound();

        //Map to DTO
        return Ok(new VisaApplicationDetailsDto
        {
            Id = visa.Id,
            ApplicantName = visa.ApplicantName,
            DateOfBirth = visa.DateOfBirth,
            PassportNumber = visa.PassportNumber,
            Nationality = visa.Nationality.CountryCode,
            VisaType = visa.VisaType.TypeName,
            Status = visa.Status.StatusName,
            ApplicationDate = visa.ApplicationDate
        });
    }

    // POST /api/visa-applications
    //Create a new visa application
    [HttpPost]
    public async Task<ActionResult> Create([FromBody]CreateVisaApplicationDto dto)
    {
        //Look up related entities
        var country = await _context.Countries
            .FirstOrDefaultAsync(c => c.CountryCode == dto.Nationality);

        var visaType = await _context.VisaTypes
            .FirstOrDefaultAsync(v => v.TypeName == dto.VisaType);

        //New applications always start with 'New' status
        var status = await _context.ApplicationStatuses
            .FirstOrDefaultAsync(s => s.StatusName == "New");

        //Validate look up data
        if (country == null || visaType == null || status == null)
            return BadRequest("Invalid lookup values");

        //Create visa application entity
        var visaApplication = new VisaApplication
        {
            ApplicantName = dto.ApplicantName,
            DateOfBirth = dto.DateOfBirth,
            PassportNumber = dto.PassportNumber,
            CountryId = country.Id,
            VisaTypeId = visaType.Id,
            ApplicationStatusId = status.Id,
            ApplicationDate = DateTime.UtcNow
        };

        //Save to database
        _context.VisaApplications.Add(visaApplication);
        await _context.SaveChangesAsync();

        //Return record created
        return CreatedAtAction(nameof(GetById), new { id = visaApplication.Id }, null);
    }
}