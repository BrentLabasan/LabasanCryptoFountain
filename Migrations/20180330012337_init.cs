﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LabasanCryptoFountain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: false),
                    TotalSentDay = table.Column<decimal>(nullable: false),
                    TotalSentHour = table.Column<decimal>(nullable: false),
                    TotalSentMinute = table.Column<decimal>(nullable: false),
                    TotalSentMonth = table.Column<decimal>(nullable: false),
                    TotalSentSecond = table.Column<decimal>(nullable: false),
                    TotalSentWeek = table.Column<decimal>(nullable: false),
                    TotalSentYear = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Send",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TokenName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Send", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Send");
        }
    }
}
