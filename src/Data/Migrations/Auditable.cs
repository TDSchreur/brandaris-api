using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brandaris.Data.Migrations;

public partial class Auditable : Migration
{
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
                                    "CreatedBy",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedById",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedDate",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedBy",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedById",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedDate",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedBy",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedById",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedDate",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedBy",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedById",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedDate",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedBy",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedById",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedDate",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedBy",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedById",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedDate",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedBy",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedById",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "CreatedDate",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedBy",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedById",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);

        migrationBuilder.DropColumn(
                                    "UpdatedDate",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null);
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
                                           "CreatedBy",
                                           "Product",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "CreatedById",
                                         "Product",
                                         "uniqueidentifier",
                                         nullable: false,
                                         defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "CreatedDate",
                                                   "Product",
                                                   "datetimeoffset",
                                                   nullable: false,
                                                   defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
                                           "UpdatedBy",
                                           "Product",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "UpdatedById",
                                         "Product",
                                         "uniqueidentifier",
                                         nullable: true);

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "UpdatedDate",
                                                   "Product",
                                                   "datetimeoffset",
                                                   nullable: true);

        migrationBuilder.AddColumn<string>(
                                           "CreatedBy",
                                           "Person",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "CreatedById",
                                         "Person",
                                         "uniqueidentifier",
                                         nullable: false,
                                         defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "CreatedDate",
                                                   "Person",
                                                   "datetimeoffset",
                                                   nullable: false,
                                                   defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
                                           "UpdatedBy",
                                           "Person",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "UpdatedById",
                                         "Person",
                                         "uniqueidentifier",
                                         nullable: true);

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "UpdatedDate",
                                                   "Person",
                                                   "datetimeoffset",
                                                   nullable: true);

        migrationBuilder.AddColumn<string>(
                                           "CreatedBy",
                                           "OrderLine",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "CreatedById",
                                         "OrderLine",
                                         "uniqueidentifier",
                                         nullable: false,
                                         defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "CreatedDate",
                                                   "OrderLine",
                                                   "datetimeoffset",
                                                   nullable: false,
                                                   defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
                                           "UpdatedBy",
                                           "OrderLine",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "UpdatedById",
                                         "OrderLine",
                                         "uniqueidentifier",
                                         nullable: true);

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "UpdatedDate",
                                                   "OrderLine",
                                                   "datetimeoffset",
                                                   nullable: true);

        migrationBuilder.AddColumn<string>(
                                           "CreatedBy",
                                           "Order",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "CreatedById",
                                         "Order",
                                         "uniqueidentifier",
                                         nullable: false,
                                         defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "CreatedDate",
                                                   "Order",
                                                   "datetimeoffset",
                                                   nullable: false,
                                                   defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.AddColumn<string>(
                                           "UpdatedBy",
                                           "Order",
                                           "nvarchar(max)",
                                           nullable: true);

        migrationBuilder.AddColumn<Guid>(
                                         "UpdatedById",
                                         "Order",
                                         "uniqueidentifier",
                                         nullable: true);

        migrationBuilder.AddColumn<DateTimeOffset>(
                                                   "UpdatedDate",
                                                   "Order",
                                                   "datetimeoffset",
                                                   nullable: true);
    }
}
