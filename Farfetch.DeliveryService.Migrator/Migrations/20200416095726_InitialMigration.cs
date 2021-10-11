using Microsoft.EntityFrameworkCore.Migrations;

namespace Farfetch.DeliveryService.Migrator.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'Farfetch')
                     BEGIN
                        CREATE DATABASE[Farfetch]
                     END;");

            migrationBuilder.Sql(@"Create table Points 
                                (Id int primary key identity(1,1),
                                 Name varchar(25) not null,
                                 IsDelete bit not null default 0);");

            migrationBuilder.Sql(@"Create table Routes 
                                 (Id int primary key identity(1,1),
                                 SourcePointId int,
                                 DestinationPointId int,
                                 Time decimal(18,2) not null,
                                 Cost decimal(18,2) not null,
                                 IsDelete bit not null default 0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Points");
            migrationBuilder.DropTable("Routes");
        }
    }
}
