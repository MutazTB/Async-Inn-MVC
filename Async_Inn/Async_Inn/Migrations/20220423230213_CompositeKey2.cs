using Microsoft.EntityFrameworkCore.Migrations;

namespace Async_Inn.Migrations
{
    public partial class CompositeKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HotelRoom");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom",
                columns: new[] { "HotelId", "RoomNumber" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HotelRoom",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelRoom",
                table: "HotelRoom",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_HotelId",
                table: "HotelRoom",
                column: "HotelId");
        }
    }
}
