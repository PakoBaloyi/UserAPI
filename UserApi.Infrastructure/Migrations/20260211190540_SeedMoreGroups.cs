using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Editors" },
                    { 3, "Viewers" }
                });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupsId", "PermissionsId" },
                values: new object[,]
                {
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupsId", "PermissionsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupsId", "PermissionsId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
