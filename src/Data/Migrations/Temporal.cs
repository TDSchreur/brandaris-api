using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brandaris.Data.Migrations;

public partial class Temporal : Migration
{
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
                                    "PeriodEnd",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodStart",
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodEnd",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodStart",
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodEnd",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodStart",
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodEnd",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.DropColumn(
                                    "PeriodStart",
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "Product")
                        .OldAnnotation("SqlServer:IsTemporal", true)
                        .OldAnnotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                        .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "Person")
                        .OldAnnotation("SqlServer:IsTemporal", true)
                        .OldAnnotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                        .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "OrderLine")
                        .OldAnnotation("SqlServer:IsTemporal", true)
                        .OldAnnotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                        .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "Order")
                        .OldAnnotation("SqlServer:IsTemporal", true)
                        .OldAnnotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                        .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
    }

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterTable(
                                    "Product")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "ProductHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "Person")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "PersonHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "OrderLine")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderLineHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AlterTable(
                                    "Order")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "OrderHistory")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodEnd",
                                             "Product",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodStart",
                                             "Product",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodEnd",
                                             "Person",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodStart",
                                             "Person",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodEnd",
                                             "OrderLine",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodStart",
                                             "OrderLine",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodEnd",
                                             "Order",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

        migrationBuilder.AddColumn<DateTime>(
                                             "PeriodStart",
                                             "Order",
                                             "datetime2",
                                             nullable: false,
                                             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
    }
}
