using Microsoft.EntityFrameworkCore;
using VisaApplicationAPI.Models;

namespace VisaApplicationAPI.Data;

// Represents the Entity Framework Core database context for the application.
// This class is responsible for configuring and managing access to the database.

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Represents the VisaApplication table in the database.
    // Used to query, insert, update, and delete visa application records.
    public DbSet<VisaApplication> VisaApplications { get;set; }
    // Represents the Country lookup table.    
    // Stores country codes and country names used by visa applications.
    public DbSet<Country> Countries { get;set; }
    // Represents the ApplicationStatus lookup table.    
    // Stores possible application states such as New, Approved and Rejected.
    public DbSet<ApplicationStatus> ApplicationStatuses { get;set; }
    // Represents the VisaType lookup table.    
    // Stores visa categories such as Tourist, Work and Student
    public DbSet<VisaType> VisaTypes { get;set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Explicitly map entities to tables to avoid pluralization issues.
        modelBuilder.Entity<VisaApplication>().ToTable("VisaApplication");
        modelBuilder.Entity<Country>().ToTable("Country");
        modelBuilder.Entity<ApplicationStatus>().ToTable("ApplicationStatus");
        modelBuilder.Entity<VisaType>().ToTable("VisaType");

        // Column mappings for VisaApplication    
        modelBuilder.Entity<VisaApplication>().Property(v => v.CountryId).HasColumnName("Nationality");    
        modelBuilder.Entity<VisaApplication>().Property(v => v.ApplicationStatusId).HasColumnName("StatusId");    
        modelBuilder.Entity<VisaApplication>().Property(v => v.VisaTypeId).HasColumnName("VisaTypeId");

        base.OnModelCreating(modelBuilder);
    }
} 