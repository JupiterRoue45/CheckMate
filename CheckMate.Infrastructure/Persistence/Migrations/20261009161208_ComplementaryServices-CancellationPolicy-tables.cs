using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckMate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ComplementaryServicesCancellationPolicytables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeCancellation",
                table: "ReservationTypes");

            migrationBuilder.CreateTable(
                name: "CancellationPolicies",
                columns: table => new
                {
                    cancellationPolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DeadlineHours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationPolicies", x => x.cancellationPolicyId);
                    table.CheckConstraint("CK_CancellationPolicies_DeadlineHoursNullOrPositive", "[DeadlineHours] IS NULL OR [DeadlineHours] > 0");
                });

            migrationBuilder.CreateTable(
                name: "ComplementaryServices",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ReservationRoomId = table.Column<int>(type: "int", nullable: false),
                    StartingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementaryServices", x => new { x.ServiceId, x.ReservationRoomId });
                    table.CheckConstraint("CK_ComplementaryServices_PositiveQuantity", "[Quantity] > 0");
                    table.CheckConstraint("CK_ComplementaryServices_PositiveUnitPrice", "[UnitPrice] > 0");
                    table.CheckConstraint("CK_ComplementaryServices_StartingDateSupEndingDate", "[StartingDate] > [EndingDate]");
                    table.ForeignKey(
                        name: "FK_ComplementaryServices_ReservationRooms_ReservationRoomId",
                        column: x => x.ReservationRoomId,
                        principalTable: "ReservationRooms",
                        principalColumn: "ReservationRoomId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComplementaryServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancellationPolicies_Code",
                table: "CancellationPolicies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplementaryServices_ReservationRoomId",
                table: "ComplementaryServices",
                column: "ReservationRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancellationPolicies");

            migrationBuilder.DropTable(
                name: "ComplementaryServices");

            migrationBuilder.AddColumn<bool>(
                name: "FreeCancellation",
                table: "ReservationTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
