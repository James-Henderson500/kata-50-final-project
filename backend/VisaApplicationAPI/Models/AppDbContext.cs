using Microsoft.EntityFrameworkCore; 

namespace VisaApp.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
    public DbSet<VisaApplication> VisaApplications { get;set; }
    public DbSet<Country> Countries { get;set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get;set; }
    public DbSet<VisaType> VisaTypes { get;set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Unique index on Passport Number
        modelBuilder.Entity<VisaApplication>()
            .HasIndex(v => v.PassportNumber)
            .IsUnique();

        //Default value for Application Date
        modelBuilder.Entity<VisaApplication>()
            .Property(v => v.ApplicationDate)
            .HasDefaultValueSql("GETUTCDATE()");
        
        //Relationships - Foreign Keys
        modelBuilder.Entity<VisaApplication>()
            .HasOne(v => v.Country)
            .WithMany()
            .HasForeignKey(v => v.CountryId);

        modelBuilder.Entity<VisaApplication>()
            .HasOne(v => v.ApplicationStatus)
            .WithMany()
            .HasForeignKey(v => v.StatusId);

        modelBuilder.Entity<VisaApplication>()
            .HasOne(v => v.VisaType)
            .WithMany()
            .HasForeignKey(v => v.VisaTypeId);
    }

}