using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LearnEasyOnline.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<Student>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model if needed
            builder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Introduction to Blazor", Description = "Learn the basics of Blazor.", Price = 49.99M },
            new Course { Id = 2, Title = "Advanced C#", Description = "Master advanced C# concepts.", Price = 79.99M },
            new Course { Id = 3, Title = "Web API Development", Description = "Build RESTful APIs with ASP.NET Core.", Price = 99.99M },
            new Course { Id = 4, Title = "SQL Fundamentals", Description = "Learn the fundamentals of Querying a relational Database.", Price = null },
            new Course { Id = 5, Title = "Entity Framework Core", Description = "Learn how to use Entity Framework Core.", Price = 89.99M }

        );
        }

    }
}
