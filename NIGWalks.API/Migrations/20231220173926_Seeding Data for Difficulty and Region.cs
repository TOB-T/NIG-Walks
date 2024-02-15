using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NIGWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultyandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3bb354ce-fcc0-4cf9-8e40-269188b8729e"), "Easy" },
                    { new Guid("a0e7007b-1678-4045-a807-cc266ce520cd"), "Medium" },
                    { new Guid("f1d2780e-4176-4e31-9174-e294f908467c"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "WAR", "Warri", null },
                    { new Guid("36640363-96c9-4c80-8785-294c25ba44b9"), "LAG", "Lagos", null },
                    { new Guid("ba384c8f-a9ae-4225-841d-8937b700be33"), "ABJ", "Abuja", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("d9c9c0dc-c40e-46b5-ae5a-867e774f7d55"), "PH", "Port-Harcourt", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("d9c9c0dc-c40e-46b5-ae5a-867e774f7d65"), "BEN", "Benin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3bb354ce-fcc0-4cf9-8e40-269188b8729e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a0e7007b-1678-4045-a807-cc266ce520cd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f1d2780e-4176-4e31-9174-e294f908467c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("36640363-96c9-4c80-8785-294c25ba44b9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ba384c8f-a9ae-4225-841d-8937b700be33"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d9c9c0dc-c40e-46b5-ae5a-867e774f7d55"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d9c9c0dc-c40e-46b5-ae5a-867e774f7d65"));
        }
    }
}
