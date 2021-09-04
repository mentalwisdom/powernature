using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nature.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Product",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productGroupId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    productGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    productGroupName = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.productGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_productGroupId",
                table: "Product",
                column: "productGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_productGroupId",
                table: "Product",
                column: "productGroupId",
                principalTable: "ProductGroup",
                principalColumn: "productGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_productGroupId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropIndex(
                name: "IX_Product_productGroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "productGroupId",
                table: "Product");
        }
    }
}
