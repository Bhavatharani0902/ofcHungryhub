using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HungryHUB.Migrations
{
    public partial class delivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryPartners_DeliveryPartnerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryPartnerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryPartnerId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryPartnerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryPartnerId",
                table: "Orders",
                column: "DeliveryPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryPartners_DeliveryPartnerId",
                table: "Orders",
                column: "DeliveryPartnerId",
                principalTable: "DeliveryPartners",
                principalColumn: "DeliveryPartnerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
