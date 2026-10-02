using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lab3.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldCustomerSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Skincare" },
                    { 2, "Makeup" },
                    { 3, "Haircare" },
                    { 4, "Fragrance" },
                    { 5, "Suncare" },
                    { 6, "Body Care" },
                    { 7, "Tools" },
                    { 8, "Sets" },
                    { 9, "Men's Care" },
                    { 10, "Kids Care" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Phone" },
                values: new object[] { "tasnim@mail.com", "010001" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 2, new DateTime(2005, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarah@mail.com", "Sarah", "010002" },
                    { 3, new DateTime(2004, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ali@mail.com", "Ali", "010003" },
                    { 4, new DateTime(2002, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "laila@mail.com", "Laila", "010004" },
                    { 5, new DateTime(2000, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hossam@mail.com", "Hossam", "010005" },
                    { 6, new DateTime(1998, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "nada@mail.com", "Nada", "010006" },
                    { 7, new DateTime(1999, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "youssef@mail.com", "Youssef", "010007" },
                    { 8, new DateTime(2001, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "dina@mail.com", "Dina", "010008" },
                    { 9, new DateTime(2003, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "mona@mail.com", "Mona", "010009" },
                    { 10, new DateTime(2004, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "khaled@mail.com", "Khaled", "010010" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { 2, 2, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 280m },
                    { 3, 3, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m },
                    { 4, 4, new DateTime(2024, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 5, 5, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m },
                    { 6, 6, new DateTime(2024, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 80m },
                    { 7, 7, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m },
                    { 8, 8, new DateTime(2024, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 9, 9, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m },
                    { 10, 10, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Cleanser", 100m },
                    { 2, 1, "Toner", 80m },
                    { 3, 2, "Foundation", 200m },
                    { 4, 2, "Lipstick", 150m },
                    { 5, 3, "Shampoo", 120m },
                    { 6, 4, "Perfume", 300m },
                    { 7, 5, "Sunscreen", 90m },
                    { 8, 6, "Body Lotion", 110m },
                    { 9, 7, "Brush Set", 250m },
                    { 10, 8, "Gift Box", 500m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 100m, 1 },
                    { 2, 3, 200m, 1 },
                    { 2, 4, 80m, 1 },
                    { 3, 5, 120m, 1 },
                    { 4, 2, 100m, 1 },
                    { 5, 10, 500m, 1 },
                    { 6, 7, 80m, 1 },
                    { 7, 8, 120m, 1 },
                    { 8, 6, 90m, 1 },
                    { 9, 9, 250m, 1 },
                    { 10, 3, 200m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 8 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Phone" },
                values: new object[] { null, "01050788567" });
        }
    }
}
