using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$N7DHv5cF5mKz5JqKGZwNXeZw3JRj9zJ6V8yqK4xZ9tJ7k5C6V4uXy");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@cxp.com", true, "CXP Admin", "$2a$11$N7DHv5cF5mKz5JqKGZwNXeZw3JRj9zJ6V8yqK4xZ9tJ7k5C6V4uXy", "Admin", null },
                    { 3, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "downtown@mithai.com", true, "Downtown Mithai Shop", "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ", "Shop", null },
                    { 4, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "mall@mithai.com", true, "Mall Mithai Shop", "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ", "Shop", null },
                    { 5, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "suburb@mithai.com", true, "Suburb Mithai Shop", "$2a$11$8Xvt90PQnNpxhOhqh8D8JeF3vZL8H7nJ5K9mL4pR6sT2uV1wX3yZ", "Shop", null },
                    { 6, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "john.doe@example.com", true, "John Doe", "$2a$11$K5mZ7n8P9qR1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N", "User", null },
                    { 7, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "jane.smith@example.com", true, "Jane Smith", "$2a$11$K5mZ7n8P9qR1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N", "User", null },
                    { 8, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "test@test.com", true, "Test User", "$2a$11$L6nA8o9Q0rS1tU2vW3xY4zA5B6C7D8E9F0G1H2I3J4K5L6M7N8O", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { 9, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "inactive@example.com", "Inactive User", "$2a$11$M7oB9p0R1sT2uV3wX4yZ5A6B7C8D9E0F1G2H3I4J5K6L7M8N9O", "User", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$rZ1f6K3Z3Z3Z3Z3Z3Z3Z3OMqH6K3Z3Z3Z3Z3Z3Z3Z3Z3Z3Z3Z3Z3Z");
        }
    }
}
