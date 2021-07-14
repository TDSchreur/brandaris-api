using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                                       "OrderLine");

            migrationBuilder.DropTable(
                                       "Order");

            migrationBuilder.DropTable(
                                       "Product");

            migrationBuilder.DropTable(
                                       "Person");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                                         "Person",
                                         table => new
                                         {
                                             Id = table.Column<int>("int", nullable: false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                             FirstName = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: true),
                                             LastName = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: true)
                                         },
                                         constraints: table => { table.PrimaryKey("PK_Person", x => x.Id); });

            migrationBuilder.CreateTable(
                                         "Product",
                                         table => new
                                         {
                                             Id = table.Column<int>("int", nullable: false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                             Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true)
                                         },
                                         constraints: table => { table.PrimaryKey("PK_Product", x => x.Id); });

            migrationBuilder.CreateTable(
                                         "Order",
                                         table => new
                                         {
                                             Id = table.Column<int>("int", nullable: false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                             PersonId = table.Column<int>("int", nullable: false)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey("PK_Order", x => x.Id);
                                             table.ForeignKey(
                                                              "FK_Order_Person_PersonId",
                                                              x => x.PersonId,
                                                              "Person",
                                                              "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateTable(
                                         "OrderLine",
                                         table => new
                                         {
                                             Id = table.Column<int>("int", nullable: false)
                                                                .Annotation("SqlServer:Identity", "1, 1"),
                                             OrderId = table.Column<int>("int", nullable: false),
                                             ProductId = table.Column<int>("int", nullable: false)
                                         },
                                         constraints: table =>
                                         {
                                             table.PrimaryKey("PK_OrderLine", x => x.Id);
                                             table.ForeignKey(
                                                              "FK_OrderLine_Order_OrderId",
                                                              x => x.OrderId,
                                                              "Order",
                                                              "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                             table.ForeignKey(
                                                              "FK_OrderLine_Product_ProductId",
                                                              x => x.ProductId,
                                                              "Product",
                                                              "Id",
                                                              onDelete: ReferentialAction.Cascade);
                                         });

            migrationBuilder.CreateIndex(
                                         "IX_Order_PersonId",
                                         "Order",
                                         "PersonId");

            migrationBuilder.CreateIndex(
                                         "IX_OrderLine_OrderId",
                                         "OrderLine",
                                         "OrderId");

            migrationBuilder.CreateIndex(
                                         "IX_OrderLine_ProductId",
                                         "OrderLine",
                                         "ProductId");
        }
    }
}
