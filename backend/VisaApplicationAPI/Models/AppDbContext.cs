using Microsoft.EntityFrameworkCore;

namespace VisaApp.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<VisaApplication> VisaApplications { get;set; }
    public DbSet<Country> Countries { get;set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get;set; }
    public DbSet<VisaType> VisaTypes { get;set; }
}