using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ProductEntityEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_images_products_product_id1",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_category_id1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_images_product_id1",
                table: "images");

            migrationBuilder.DropColumn(
                name: "category_id1",
                table: "products");

            migrationBuilder.DropColumn(
                name: "product_id1",
                table: "images");

            migrationBuilder.AlterColumn<Guid>(
                name: "category_id",
                table: "products",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<List<string>>(
                name: "images_ids",
                table: "products",
                type: "text[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_products_category_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "images_ids",
                table: "products");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "category_id1",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "product_id1",
                table: "images",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id1",
                table: "products",
                column: "category_id1");

            migrationBuilder.CreateIndex(
                name: "ix_images_product_id1",
                table: "images",
                column: "product_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_images_products_product_id1",
                table: "images",
                column: "product_id1",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id1",
                table: "products",
                column: "category_id1",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
