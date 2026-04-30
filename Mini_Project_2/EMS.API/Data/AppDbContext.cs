using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Nivetha",
                    LastName = "SenthilVel",
                    Email = "nivetha@gmail.com",
                    Phone = "9774543225",
                    Department = "Engineering",
                    Designation = "Software Engineer",
                    Salary = 80000,
                    JoinDate = new DateTime(2022, 1, 25),
                    Status = "Active",
                    CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Karthika",
                    LastName = "Shanmugam",
                    Email = "karthika@gmail.com",
                    Phone = "9876543211",
                    Department = "HR",
                    Designation = "HR Manager",
                    Salary = 57000,
                    JoinDate = new DateTime(2021, 6, 10),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Sathya",
                    LastName = "Magudeeshwaran",
                    Email = "sathya@gmail.com",
                    Phone = "9876543456",
                    Department = "Marketing",
                    Designation = "Marketing Executive",
                    Salary = 70000,
                    JoinDate = new DateTime(2022, 8, 5),
                    Status = "Inactive",
                    CreatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Kannika ",
                    LastName = "Devi",
                    Email = "kannika@gmail.com",
                    Phone = "9345447085",
                    Department = "Finance",
                    Designation = "Financial Analyst",
                    Salary = 95000,
                    JoinDate = new DateTime(2020, 1, 08),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 4, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "Swetha",
                    LastName = "Senthilvel",
                    Email = "swetha@gmail.com",
                    Phone = "9976543215",
                    Department = "Operations",
                    Designation = "Operations Lead",
                    Salary = 100000,
                    JoinDate = new DateTime(2019, 8, 1),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 5, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 5, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 6,
                    FirstName = "Thanish",
                    LastName = "Pandiyan",
                    Email = "thanish@gmail.com",
                    Phone = "9876543215",
                    Department = "Engineering",
                    Designation = "QA Engineer",
                    Salary = 78000,
                    JoinDate = new DateTime(2022, 9, 12),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 6, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 6, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 7,
                    FirstName = "Bala",
                    LastName = "Kumaran",
                    Email = "bala@gmail.com",
                    Phone = "9876543216",
                    Department = "Marketing",
                    Designation = "SEO Specialist",
                    Salary = 88000,
                    JoinDate = new DateTime(2023, 6, 18),
                    Status = "Inactive",
                    CreatedAt = new DateTime(2024, 1, 7, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 7, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 8,
                    FirstName = "Senthilvel",
                    LastName = "Subramaniyam",
                    Email = "senthil@gmail.com",
                    Phone = "9876543217",
                    Department = "HR",
                    Designation = "Recruiter",
                    Salary = 87000,
                    JoinDate = new DateTime(2021, 12, 1),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 8, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 8, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 9,
                    FirstName = "Santhosh",
                    LastName = "kumar",
                    Email = "santhosh@gmail.com",
                    Phone = "9876543218",
                    Department = "Finance",
                    Designation = "Accountant",
                    Salary = 82000,
                    JoinDate = new DateTime(2020, 2, 14),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 9, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 9, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 10,
                    FirstName = "Aishwarya",
                    LastName = "Saran",
                    Email = "aishwarya@gmail.com",
                    Phone = "9876543219",
                    Department = "Operations",
                    Designation = "Operations Executive",
                    Salary = 89000,
                    JoinDate = new DateTime(2022, 7, 25),
                    Status = "Inactive",
                    CreatedAt = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 11,
                    FirstName = "Harini",
                    LastName = "kumar",
                    Email = "harini@gmail.com",
                    Phone = "9876543220",
                    Department = "Engineering",
                    Designation = "Backend Developer",
                    Salary = 82000,
                    JoinDate = new DateTime(2021, 4, 9),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 11, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 11, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 12,
                    FirstName = "Ayra",
                    LastName = "Hakeem",
                    Email = "ayra@gmail.com",
                    Phone = "9876543221",
                    Department = "HR",
                    Designation = "HR Executive",
                    Salary = 46000,
                    JoinDate = new DateTime(2023, 1, 30),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 13,
                    FirstName = "Nandhitha",
                    LastName = "Thandapani",
                    Email = "nandhu@gmail.com",
                    Phone = "9876543222",
                    Department = "Finance",
                    Designation = "Finance Manager",
                    Salary = 80000,
                    JoinDate = new DateTime(2017, 10, 7),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 13, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 13, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 14,
                    FirstName = "Yazhini",
                    LastName = "Kandhasamy",
                    Email = "yazhini@gmail.com",
                    Phone = "9876543223",
                    Department = "Marketing",
                    Designation = "Content Strategist",
                    Salary = 53000,
                    JoinDate = new DateTime(2022, 11, 11),
                    Status = "Active",
                    CreatedAt = new DateTime(2024, 1, 14, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 14, 0, 0, 0, DateTimeKind.Utc)
                },
                new Employee
                {
                    Id = 15,
                    FirstName = "Harshitha",
                    LastName = "Sri",
                    Email = "sri@gmail.com",
                    Phone = "9876543224",
                    Department = "Operations",
                    Designation = "Operations Manager",
                    Salary = 35000,
                    JoinDate = new DateTime(2019, 5, 17),
                    Status = "Inactive",
                    CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new AppUser
                {
                    Id = 2,
                    Username = "viewer",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("viewer123"),
                    Role = "Viewer",
                    CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}