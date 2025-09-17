using Microsoft.EntityFrameworkCore;
using SkillNest.Models;


namespace SkillNest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().ToTable("Employees");

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Skills)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Projects>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.Projects)
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificates>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Certificates)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Resume>()
                .HasOne(r => r.Employee)
                .WithOne(e => e.Resume)
                .HasForeignKey<Resume>(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Omkar Bajaga",
                    Email = "omkar@gmail.com",
                    Location = "Hyderabad",
                    Role = "Employee",
                    Password = "Omkar@123"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Rahul",
                    Email = "rahul@gamil.com",
                    Location = "Pune",
                    Role = "Manager",
                    Password = "Rahul@123"
                }
            );
        }

    }
}
