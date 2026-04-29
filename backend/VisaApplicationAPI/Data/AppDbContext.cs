using Microsoft.EntityFrameworkCore;
using VisaApplicationAPI.Models;

namespace VisaApplicationAPI.Data;

// Represents the Entity Framework Core database context for the application.
// This class is responsible for configuring and managing access to the database.

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<VisaApplication> VisaApplications { get;set; }
    public DbSet<Country> Countries { get;set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get;set; }
    public DbSet<VisaType> VisaTypes { get;set; }
} 