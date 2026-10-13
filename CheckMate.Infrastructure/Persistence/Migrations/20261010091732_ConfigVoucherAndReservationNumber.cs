using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckMate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigVoucherAndReservationNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationNumber",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Voucher",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CancellationPolicyId",
                table: "ReservationRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationNumber",
                table: "Reservations",
                column: "ReservationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_CancellationPolicyId",
                table: "ReservationRooms",
                column: "CancellationPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRooms_CancellationPolicies_CancellationPolicyId",
                table: "ReservationRooms",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicies",
                principalColumn: "cancellationPolicyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRooms_CancellationPolicies_CancellationPolicyId",
                table: "ReservationRooms");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationNumber",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRooms_CancellationPolicyId",
                table: "ReservationRooms");

            migrationBuilder.DropColumn(
                name: "ReservationNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Voucher",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CancellationPolicyId",
                table: "ReservationRooms");
        }
    }
}
