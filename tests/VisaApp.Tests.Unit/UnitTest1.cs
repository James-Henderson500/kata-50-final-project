using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
// Access to API
using VisaApplicationAPI.Data;
using VisaApplicationAPI.Controllers;
using VisaApplicationAPI.Models;
using VisaApplicationAPI.DTOs.VisaApplications;

namespace VisaAPI.Tests.Unit
{
    // Class contains unit tests
    public class VisaApplicationsTests
    {
        // In-memory database context
        private AppDbContext _context;

        // Controller to be tested
        private VisaApplicationsController _controller;

        // SetUp: Runs before each test
        [SetUp]
        public void Setup()
        {
            // Create a new in-memory database for each test
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            // Create database context
            _context = new AppDbContext(options);

            // Seed lookup tables (FK relationships)

            var countryGB = new Country { Id = 1, CountryCode = "GB" };
            var countryUS = new Country { Id = 2, CountryCode = "US" };

            var tourist = new VisaType { Id = 1, TypeName = "Tourist" };
            var work = new VisaType { Id = 2, TypeName = "Work" };

            var newStatus = new ApplicationStatus { Id = 1, StatusName = "New" };
            var approved = new ApplicationStatus { Id = 2, StatusName = "Approved" };

            // Add lookup data into database
            _context.Countries.AddRange(countryGB, countryUS);
            _context.VisaTypes.AddRange(tourist, work);
            _context.ApplicationStatuses.AddRange(newStatus, approved);

            // Seed test visa applications

            _context.VisaApplications.AddRange(
                new VisaApplication
                {
                    Id = 1,
                    ApplicantName = "John Doe",
                    PassportNumber = "A12345678",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    ApplicationDate = DateTime.UtcNow,

                    // FK values
                    CountryId = 1,
                    VisaTypeId = 1,
                    ApplicationStatusId = 1,

                    // Navigation properties
                    Nationality = countryGB,
                    VisaType = tourist,
                    Status = newStatus
                },
                new VisaApplication
                {
                    Id = 2,
                    ApplicantName = "Jane Smith",
                    PassportNumber = "B87654321",
                    DateOfBirth = new DateTime(1995, 1, 1),
                    ApplicationDate = DateTime.UtcNow,

                    // FK values
                    CountryId = 2,
                    VisaTypeId = 2,
                    ApplicationStatusId = 2,

                    // Navigation properties
                    Nationality = countryUS,
                    VisaType = work,
                    Status = approved
                }
            );
            // Save all seeded data in database
            _context.SaveChanges();

            // Inject DB into controller to simulate API
            _controller = new VisaApplicationsController(_context);
        }
        // Cleanup: Runs after each test
        [TearDown]
        public void TearDown()
        {
            // Deletes in-memory test DB after each run
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        // GET ALL TESTS (GET /api/visa-applications)
        [Test]
        // Returns all applications
        public async Task GetAll_ReturnsAllApplications()
        {
            // Call API method
            var result = await _controller.GetAll(null, null);

            // Extract HTTP response (200 Ok)
            var okResult = result.Result as OkObjectResult;
            // Extract data returned (DTOs)
            var data = okResult?.Value as IEnumerable<VisaApplicationListDto>;

            // Assertions
            // Check response is not null
            Assert.That(okResult, Is.Not.Null);
            // Check that 2 records were returned
            Assert.That(data.Count(), Is.EqualTo(2));
        }

        [Test]
        // Returns applications filtered by status
        public async Task GetAll_FilterByStatus_ReturnsCorrectResults()
        {
            // Call API method with visa status filter
            var result = await _controller.GetAll("Approved", null);

            // Extract response
            var okResult = result.Result as OkObjectResult;
            var data = okResult?.Value as IEnumerable<VisaApplicationListDto>;

            // Assertions
            // Only 1 application should match "Approved"
            Assert.That(data.Count(), Is.EqualTo(1));
            // Check returned visa status is correct
            Assert.That(data.First().Status, Is.EqualTo("Approved"));
        }

        [Test]
        // Returns applications filtered by type
        public async Task GetAll_FilterByVisaType_ReturnsCorrectResults()
        {
            // Call API method with visa type filter
            var result = await _controller.GetAll(null, "Tourist");

            // Extract response
            var okResult = result.Result as OkObjectResult;
            var data = okResult?.Value as IEnumerable<VisaApplicationListDto>;

            // Assertions
            // Only 1 application should match "Tourist"
            Assert.That(data.Count(), Is.EqualTo(1));
            // Check returned visa type is correct
            Assert.That(data.First().VisaType, Is.EqualTo("Tourist"));
        }
        // GET BY ID TESTS (GET /api/visa-applications/{id})

        [Test]
        // Get by ID - valid
        public async Task GetById_ValidId_ReturnsApplication()
        {
            // Call API method with valid ID
            var result = await _controller.GetById(1);

            // Extract response
            var okResult = result.Result as OkObjectResult;
            var data = okResult?.Value as VisaApplicationDetailsDto;

            // Assertions
            // Ensure data exists
            Assert.That(data, Is.Not.Null);
            // Check correct application is returned
            Assert.That(data.ApplicantName, Is.EqualTo("John Doe"));
        }

        [Test]
        // Get by ID - invalid
        public async Task GetById_InvalidId_ReturnsNotFound()
        {
            // Call API method with invalid ID
            var result = await _controller.GetById(999);
            // Assertions
            // Expect a 404 not found reponse
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }
        // POST TESTS (POST /api/visa-applications)

        [Test]
        public async Task Create_ValidRequest_AddsApplication()
        {
            // Create a valid request DTO to simulate frontend request
            var dto = new CreateVisaApplicationDto
            {
                ApplicantName = "New Person",
                DateOfBirth = new DateTime(2000, 1, 1),
                PassportNumber = "C11223344",
                Nationality = "GB",
                VisaType = "Tourist"
            };
            // Call create endpoint
            var result = await _controller.Create(dto);

            // Assertions
            // Confirm that count has increased and that database now has 3 records
            Assert.That(_context.VisaApplications.Count(), Is.EqualTo(3));

            // Confirm that correct response type has been returned from API (201 Created)
            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task Create_InvalidLookup_ReturnsBadRequest()
        {
            // Create an invalid request DTO to simulate frontend request
            var dto = new CreateVisaApplicationDto
            {
                ApplicantName = "Invalid Person",
                DateOfBirth = new DateTime(2000, 1, 1),
                PassportNumber = "X99999999",
                Nationality = "INVALID", // this nationality does not exist
                VisaType = "Tourist"
            };
            // Call create endpoint
            var result = await _controller.Create(dto);
            // Assertions
            // Expect a HTTP reponse (400 Bad Request)
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }
    }
}