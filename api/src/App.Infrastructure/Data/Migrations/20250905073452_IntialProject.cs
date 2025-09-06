using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
  /// <inheritdoc />
  public partial class IntialProject : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "nguoi_dung",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ngay_xoa = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            nguoi_xoa = table.Column<int>(type: "integer", nullable: true),
            ten_dang_nhap = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            mat_khau_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            email_da_xac_thuc = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_nguoi_dung", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "quyen",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ten = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            ten_hien_thi = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
            mo_ta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
            danh_muc = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_quyen", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "vai_tro",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ten = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            ten_hien_thi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            mo_ta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
            la_mac_dinh = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_vai_tro", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "dang_nhap_ngoai",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            nha_cung_cap = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            ma_ngoai = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
            ten_hien_thi = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
            token_truy_cap = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
            token_lam_moi = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
            het_han_luc_token_truy_cap = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            het_han_luc_token_lam_moi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            pham_vi = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_dang_nhap_ngoai", x => x.id);
            table.ForeignKey(
                      name: "FK_dang_nhap_ngoai_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "token_dat_lai_mat_khau",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            het_han_luc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            su_dung_luc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_token_dat_lai_mat_khau", x => x.id);
            table.ForeignKey(
                      name: "FK_token_dat_lai_mat_khau_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "token_lam_moi",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            het_han_luc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            da_thu_hoi = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_token_lam_moi", x => x.id);
            table.ForeignKey(
                      name: "FK_token_lam_moi_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "token_xac_thuc_email",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            token = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            het_han_luc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            su_dung_luc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_token_xac_thuc_email", x => x.id);
            table.ForeignKey(
                      name: "FK_token_xac_thuc_email_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "nguoi_dung_quyen",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            ma_quyen = table.Column<int>(type: "integer", nullable: false),
            duoc_cap_phep = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_nguoi_dung_quyen", x => x.id);
            table.ForeignKey(
                      name: "FK_nguoi_dung_quyen_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_nguoi_dung_quyen_quyen_ma_quyen",
                      column: x => x.ma_quyen,
                      principalTable: "quyen",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "nguoi_dung_vai_tro",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_nguoi_dung = table.Column<int>(type: "integer", nullable: false),
            ma_vai_tro = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_nguoi_dung_vai_tro", x => x.id);
            table.ForeignKey(
                      name: "FK_nguoi_dung_vai_tro_nguoi_dung_ma_nguoi_dung",
                      column: x => x.ma_nguoi_dung,
                      principalTable: "nguoi_dung",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_nguoi_dung_vai_tro_vai_tro_ma_vai_tro",
                      column: x => x.ma_vai_tro,
                      principalTable: "vai_tro",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "vai_tro_quyen",
          columns: table => new
          {
            id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            ngay_tao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            ngay_cap_nhat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            ma_vai_tro = table.Column<int>(type: "integer", nullable: false),
            ma_quyen = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_vai_tro_quyen", x => x.id);
            table.ForeignKey(
                      name: "FK_vai_tro_quyen_quyen_ma_quyen",
                      column: x => x.ma_quyen,
                      principalTable: "quyen",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_vai_tro_quyen_vai_tro_ma_vai_tro",
                      column: x => x.ma_vai_tro,
                      principalTable: "vai_tro",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_dang_nhap_ngoai_ma_nguoi_dung",
          table: "dang_nhap_ngoai",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_nguoi_dung_quyen_ma_nguoi_dung",
          table: "nguoi_dung_quyen",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_nguoi_dung_quyen_ma_quyen",
          table: "nguoi_dung_quyen",
          column: "ma_quyen");

      migrationBuilder.CreateIndex(
          name: "IX_nguoi_dung_vai_tro_ma_nguoi_dung",
          table: "nguoi_dung_vai_tro",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_nguoi_dung_vai_tro_ma_vai_tro",
          table: "nguoi_dung_vai_tro",
          column: "ma_vai_tro");

      migrationBuilder.CreateIndex(
          name: "IX_token_dat_lai_mat_khau_ma_nguoi_dung",
          table: "token_dat_lai_mat_khau",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_token_lam_moi_ma_nguoi_dung",
          table: "token_lam_moi",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_token_xac_thuc_email_ma_nguoi_dung",
          table: "token_xac_thuc_email",
          column: "ma_nguoi_dung");

      migrationBuilder.CreateIndex(
          name: "IX_vai_tro_quyen_ma_quyen",
          table: "vai_tro_quyen",
          column: "ma_quyen");

      migrationBuilder.CreateIndex(
          name: "IX_vai_tro_quyen_ma_vai_tro",
          table: "vai_tro_quyen",
          column: "ma_vai_tro");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "dang_nhap_ngoai");

      migrationBuilder.DropTable(
          name: "nguoi_dung_quyen");

      migrationBuilder.DropTable(
          name: "nguoi_dung_vai_tro");

      migrationBuilder.DropTable(
          name: "token_dat_lai_mat_khau");

      migrationBuilder.DropTable(
          name: "token_lam_moi");

      migrationBuilder.DropTable(
          name: "token_xac_thuc_email");

      migrationBuilder.DropTable(
          name: "vai_tro_quyen");

      migrationBuilder.DropTable(
          name: "nguoi_dung");

      migrationBuilder.DropTable(
          name: "quyen");

      migrationBuilder.DropTable(
          name: "vai_tro");
    }
  }
}