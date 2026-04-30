using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EMS.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$ZZM4gXJQtojlU1RS1LH5k.ni5ROWsi/sofzFnOx45nVpr3XZ.rBTu", "Admin", "admin" },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "$2a$11$tmnuELaffo935lmBEbAqcewZztf60.7TD0D8I.mcSBRq/ZIJv.AIu", "Viewer", "viewer" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Department", "Designation", "Email", "FirstName", "JoinDate", "LastName", "Phone", "Salary", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Engineering", "Software Engineer", "nivetha@gmail.com", "Nivetha", new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "SenthilVel", "9774543225", 80000m, "Active", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "HR", "HR Manager", "karthika@gmail.com", "Karthika", new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shanmugam", "9876543211", 57000m, "Active", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "Marketing Executive", "sathya@gmail.com", "Sathya", new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Magudeeshwaran", "9876543456", 70000m, "Inactive", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "Financial Analyst", "kannika@gmail.com", "Kannika ", new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Devi", "9345447085", 95000m, "Active", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Operations", "Operations Lead", "swetha@gmail.com", "Swetha", new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senthilvel", "9976543215", 100000m, "Active", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Engineering", "QA Engineer", "thanish@gmail.com", "Thanish", new DateTime(2022, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pandiyan", "9876543215", 78000m, "Active", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "SEO Specialist", "bala@gmail.com", "Bala", new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kumaran", "9876543216", 88000m, "Inactive", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "HR", "Recruiter", "senthil@gmail.com", "Senthilvel", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Subramaniyam", "9876543217", 87000m, "Active", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "Accountant", "santhosh@gmail.com", "Santhosh", new DateTime(2020, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "kumar", "9876543218", 82000m, "Active", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Operations", "Operations Executive", "aishwarya@gmail.com", "Aishwarya", new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Saran", "9876543219", 89000m, "Inactive", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Engineering", "Backend Developer", "harini@gmail.com", "Harini", new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "kumar", "9876543220", 82000m, "Active", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "HR", "HR Executive", "ayra@gmail.com", "Ayra", new DateTime(2023, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hakeem", "9876543221", 46000m, "Active", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "Finance Manager", "nandhu@gmail.com", "Nandhitha", new DateTime(2017, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thandapani", "9876543222", 80000m, "Active", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Marketing", "Content Strategist", "yazhini@gmail.com", "Yazhini", new DateTime(2022, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kandhasamy", "9876543223", 53000m, "Active", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Operations", "Operations Manager", "sri@gmail.com", "Harshitha", new DateTime(2019, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sri", "9876543224", 35000m, "Inactive", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Username",
                table: "AppUsers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
