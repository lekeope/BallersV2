using System.Collections.Generic; 
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Pitch> Pitches { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public DbSet<User> Users { get; set; }

    // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "BallersDB");
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // initial players for db
        modelBuilder.Entity<Pitch>().HasData(
            new Pitch {
                Id = 1, PitchType = "Astro 2G", PitchSize = "11v11",
                PostCode= "LN67TS", Address="HELLO WORLD", Note="Helllo Hello",
                CreatedOn = DateTime.Now, LastUpdatedOn = DateTime.Now, LastUpdatedBy = 1
            }
            );

        // initial players for db
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Now },
            new User { Id = 2, FirstName = "Thomas", LastName = "Jones", DateOfBirth = DateTime.Now },
            new User { Id = 3, FirstName = "Carrick", LastName = "Dean", DateOfBirth = DateTime.Now },
            new User { Id = 4, FirstName = "Derrick", LastName = "John", DateOfBirth = DateTime.Now },
            new User { Id = 5, FirstName = "Daniel", LastName = "Hogg", DateOfBirth = DateTime.Now },
            new User { Id = 6, FirstName = "Joseph", LastName = "Oink", DateOfBirth = DateTime.Now },
            new User { Id = 7, FirstName = "James", LastName = "Balmer", DateOfBirth = DateTime.Now }
            );

        // initial players for db
        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = 1, PitchId = 1, Duration = 1.5f, AppointmentTime = DateTime.Now.AddHours(6), CreatedOn = DateTime.Now, UserId = 1 }
            );
    }*/
}