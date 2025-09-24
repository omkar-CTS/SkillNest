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
                 new Employee { Id = 1, Name = "Omkar Bajaga", Email = "omkar@gmail.com", Location = "Hyderabad", Role = "Employee", Password = "Omkar@123" },
                 new Employee { Id = 2, Name = "Rahul", Email = "rahul@gmail.com", Location = "Pune", Role = "Manager", Password = "Rahul@123" },
                 new Employee { Id = 3, Name = "Amit Sharma", Email = "amit.sharma@gmail.com", Location = "Delhi", Role = "Employee", Password = "Amit@123" },
                 new Employee { Id = 4, Name = "Suresh Kumar", Email = "suresh.kumar@gmail.com", Location = "Mumbai", Role = "Employee", Password = "Suresh@123" },
                 new Employee { Id = 5, Name = "Priya Singh", Email = "priya.singh@gmail.com", Location = "Bangalore", Role = "Manager", Password = "Priya@123" },
                 new Employee { Id = 6, Name = "Deepak Verma", Email = "deepak.verma@gmail.com", Location = "Kolkata", Role = "Employee", Password = "Deepak@123" },
                 new Employee { Id = 7, Name = "Neha Gupta", Email = "neha.gupta@gmail.com", Location = "Jaipur", Role = "Employee", Password = "Neha@123" },
                 new Employee { Id = 8, Name = "Rakesh Yadav", Email = "rakesh.yadav@gmail.com", Location = "Chennai", Role = "Employee", Password = "Rakesh@123" },
                 new Employee { Id = 9, Name = "Anjali Mishra", Email = "anjali.mishra@gmail.com", Location = "Ahmedabad", Role = "Manager", Password = "Anjali@123" },
                 new Employee { Id = 10, Name = "Vikas Jain", Email = "vikas.jain@gmail.com", Location = "Lucknow", Role = "Employee", Password = "Vikas@123" },
                 new Employee { Id = 11, Name = "Sunita Rani", Email = "sunita.rani@gmail.com", Location = "Chandigarh", Role = "Employee", Password = "Sunita@123" },
                 new Employee { Id = 12, Name = "Rohan Mehta", Email = "rohan.mehta@gmail.com", Location = "Indore", Role = "Manager", Password = "Rohan@123" },
                 new Employee { Id = 13, Name = "Kiran Patel", Email = "kiran.patel@gmail.com", Location = "Surat", Role = "Employee", Password = "Kiran@123" },
                 new Employee { Id = 14, Name = "Pooja Joshi", Email = "pooja.joshi@gmail.com", Location = "Bhopal", Role = "Employee", Password = "Pooja@123" },
                 new Employee { Id = 15, Name = "Arun Dubey", Email = "arun.dubey@gmail.com", Location = "Kanpur", Role = "Manager", Password = "Arun@123" },
                 new Employee { Id = 16, Name = "Sneha Choudhary", Email = "sneha.choudhary@gmail.com", Location = "Nagpur", Role = "Employee", Password = "Sneha@123" },
                 new Employee { Id = 17, Name = "Tarun Kapoor", Email = "tarun.kapoor@gmail.com", Location = "Patna", Role = "Employee", Password = "Tarun@123" },
                 new Employee { Id = 18, Name = "Ankita Sinha", Email = "ankita.sinha@gmail.com", Location = "Agra", Role = "Manager", Password = "Ankita@123" },
                 new Employee { Id = 19, Name = "Nitin Saxena", Email = "nitin.saxena@gmail.com", Location = "Varanasi", Role = "Employee", Password = "Nitin@123" },
                 new Employee { Id = 20, Name = "Meena Pandey", Email = "meena.pandey@gmail.com", Location = "Ranchi", Role = "Employee", Password = "Meena@123" }
            );
        }

    }
}
