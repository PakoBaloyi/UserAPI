using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UsersId",
                table: "UserGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                columns: new[] { "UsersId", "GroupsId" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Administrators" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "LastName", "Name", "Username" },
                values: new object[] { 1, "admin@example.com", true, "Admin", "System", "admin" });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupsId", "PermissionsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "GroupsId", "UsersId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupsId",
                table: "UserGroups",
                column: "GroupsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_GroupsId",
                table: "UserGroups");

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupsId", "PermissionsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumns: new[] { "GroupsId", "UsersId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroups",
                table: "UserGroups",
                columns: new[] { "GroupsId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UsersId",
                table: "UserGroups",
                column: "UsersId");
        }
    }
}
