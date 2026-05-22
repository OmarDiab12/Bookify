using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_permissions_permission_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_permission_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "permission_id",
                table: "users");

            migrationBuilder.DropColumn(
                 name: "version",
                 table: "apartments");

            migrationBuilder.AddColumn<byte[]>(
                name: "version",
                table: "apartments",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    occured_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_user",
                columns: table => new
                {
                    permissions_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_user", x => new { x.permissions_id, x.users_id });
                    table.ForeignKey(
                        name: "fk_permission_user_permission_permissions_id",
                        column: x => x.permissions_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_permission_user_user_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_permission_user_users_id",
                table: "permission_user",
                column: "users_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "permission_user");

            migrationBuilder.AddColumn<int>(
                name: "permission_id",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "version",
                table: "apartments");

            migrationBuilder.AddColumn<long>(
                name: "version",
                table: "apartments",
                type: "bigint",
                rowVersion: true,
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_users_permission_id",
                table: "users",
                column: "permission_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_permissions_permission_id",
                table: "users",
                column: "permission_id",
                principalTable: "permissions",
                principalColumn: "id");
        }
    }
}
