using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ImageEdited2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_images_products_product_id1",
                table: "images");

            migrationBuilder.DropColumn(
                name: "link",
                table: "images");

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id1",
                table: "images",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<byte[]>(
                name: "image_data",
                table: "images",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddForeignKey(
                name: "fk_images_products_product_id1",
                table: "images",
                column: "product_id1",
                principalTable: "products",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_images_products_product_id1",
                table: "images");

            migrationBuilder.DropColumn(
                name: "image_data",
                table: "images");

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id1",
                table: "images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "images",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_images_products_product_id1",
                table: "images",
                column: "product_id1",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
