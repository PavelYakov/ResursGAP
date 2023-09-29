using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResursGAP.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Routers_RouteId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RouteId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routers",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EndCity",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Routers");

            migrationBuilder.DropColumn(
                name: "StartCity",
                table: "Routers");

            migrationBuilder.RenameTable(
                name: "Routers",
                newName: "Routes");

            migrationBuilder.AlterColumn<double>(
                name: "WeightInTons",
                table: "Trucks",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "AvailableWeightInTons",
                table: "Trucks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CityId",
                table: "Routes",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_OrderId",
                table: "Routes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Cities_CityId",
                table: "Routes",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Orders_OrderId",
                table: "Routes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Cities_CityId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Orders_OrderId",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_CityId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_OrderId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "AvailableWeightInTons",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Routers");

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightInTons",
                table: "Trucks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EndCity",
                table: "Routers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Routers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartCity",
                table: "Routers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routers",
                table: "Routers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RouteId",
                table: "Orders",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Routers_RouteId",
                table: "Orders",
                column: "RouteId",
                principalTable: "Routers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
