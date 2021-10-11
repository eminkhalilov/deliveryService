using Microsoft.EntityFrameworkCore.Migrations;

namespace Farfetch.DeliveryService.Migrator.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insert example points
            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 1, "A" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 2, "B" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 3, "C" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 4, "D" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 5, "E" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 6, "F" });

            migrationBuilder.InsertData(
            table: "Points",
            columns: new[] { "Id", "Name" },
            values: new object[] { 7, "G" });


            //Insert example routes
            migrationBuilder.InsertData(
            table: "Routes",
            columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
            values: new object[] { 1, 3, 1, 20, 0 });

            migrationBuilder.InsertData(
            table: "Routes",
            columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
            values: new object[] { 3, 2, 1, 12, 0 });

            migrationBuilder.InsertData(
         table: "Routes",
         columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
         values: new object[] { 1, 5, 30, 5, 0 });

            migrationBuilder.InsertData(
         table: "Routes",
         columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
         values: new object[] { 4, 5, 30, 1, 0 });

            migrationBuilder.InsertData(
       table: "Routes",
       columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
       values: new object[] { 4, 6, 4, 2, 0 });

            migrationBuilder.InsertData(
   table: "Routes",
   columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
   values: new object[] { 1, 4, 12, 2, 0 });

            migrationBuilder.InsertData(
   table: "Routes",
   columns: new[] { "SourcePointId", "DestinationPointId", "Time", "Cost", "IsDelete" },
   values: new object[] { 6, 2, 25, 2, 0 });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Points");
            migrationBuilder.Sql("Delete From Routes");
        }
    }
}
