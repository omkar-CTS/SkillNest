using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillNest.Migrations
{
    /// <inheritdoc />
    public partial class SeededMoreEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "rahul@gmail.com");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Location", "Name", "Password", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[,]
                {
                    { 3, "amit.sharma@gmail.com", "Delhi", "Amit Sharma", "Amit@123", new byte[0], new byte[0], "Employee" },
                    { 4, "suresh.kumar@gmail.com", "Mumbai", "Suresh Kumar", "Suresh@123", new byte[0], new byte[0], "Employee" },
                    { 5, "priya.singh@gmail.com", "Bangalore", "Priya Singh", "Priya@123", new byte[0], new byte[0], "Manager" },
                    { 6, "deepak.verma@gmail.com", "Kolkata", "Deepak Verma", "Deepak@123", new byte[0], new byte[0], "Employee" },
                    { 7, "neha.gupta@gmail.com", "Jaipur", "Neha Gupta", "Neha@123", new byte[0], new byte[0], "Employee" },
                    { 8, "rakesh.yadav@gmail.com", "Chennai", "Rakesh Yadav", "Rakesh@123", new byte[0], new byte[0], "Employee" },
                    { 9, "anjali.mishra@gmail.com", "Ahmedabad", "Anjali Mishra", "Anjali@123", new byte[0], new byte[0], "Manager" },
                    { 10, "vikas.jain@gmail.com", "Lucknow", "Vikas Jain", "Vikas@123", new byte[0], new byte[0], "Employee" },
                    { 11, "sunita.rani@gmail.com", "Chandigarh", "Sunita Rani", "Sunita@123", new byte[0], new byte[0], "Employee" },
                    { 12, "rohan.mehta@gmail.com", "Indore", "Rohan Mehta", "Rohan@123", new byte[0], new byte[0], "Manager" },
                    { 13, "kiran.patel@gmail.com", "Surat", "Kiran Patel", "Kiran@123", new byte[0], new byte[0], "Employee" },
                    { 14, "pooja.joshi@gmail.com", "Bhopal", "Pooja Joshi", "Pooja@123", new byte[0], new byte[0], "Employee" },
                    { 15, "arun.dubey@gmail.com", "Kanpur", "Arun Dubey", "Arun@123", new byte[0], new byte[0], "Manager" },
                    { 16, "sneha.choudhary@gmail.com", "Nagpur", "Sneha Choudhary", "Sneha@123", new byte[0], new byte[0], "Employee" },
                    { 17, "tarun.kapoor@gmail.com", "Patna", "Tarun Kapoor", "Tarun@123", new byte[0], new byte[0], "Employee" },
                    { 18, "ankita.sinha@gmail.com", "Agra", "Ankita Sinha", "Ankita@123", new byte[0], new byte[0], "Manager" },
                    { 19, "nitin.saxena@gmail.com", "Varanasi", "Nitin Saxena", "Nitin@123", new byte[0], new byte[0], "Employee" },
                    { 20, "meena.pandey@gmail.com", "Ranchi", "Meena Pandey", "Meena@123", new byte[0], new byte[0], "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "rahul@gamil.com");
        }
    }
}
