using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
  /// <inheritdoc />
  public partial class AddUserSuspendColumn : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<int>(
          name: "khoa_boi",
          table: "nguoi_dung",
          type: "int",
          nullable: true);

      migrationBuilder.AddColumn<DateTime>(
          name: "khoa_den_luc",
          table: "nguoi_dung",
          type: "datetime2",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "ly_do_khoa",
          table: "nguoi_dung",
          type: "nvarchar(500)",
          maxLength: 500,
          nullable: true);

      migrationBuilder.AddColumn<int>(
          name: "phien_ban_token",
          table: "nguoi_dung",
          type: "int",
          nullable: false,
          defaultValue: 1);

      migrationBuilder.AddColumn<int>(
          name: "trang_thai",
          table: "nguoi_dung",
          type: "int",
          nullable: false,
          defaultValue: 1);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "khoa_boi",
          table: "nguoi_dung");

      migrationBuilder.DropColumn(
          name: "khoa_den_luc",
          table: "nguoi_dung");

      migrationBuilder.DropColumn(
          name: "ly_do_khoa",
          table: "nguoi_dung");

      migrationBuilder.DropColumn(
          name: "phien_ban_token",
          table: "nguoi_dung");

      migrationBuilder.DropColumn(
          name: "trang_thai",
          table: "nguoi_dung");
    }
  }
}