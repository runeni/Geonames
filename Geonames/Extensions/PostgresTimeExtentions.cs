using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace Geonames.Extensions
{
    internal static class PostgresTimeExtentions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithTimeStamps(
            this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("created_at").AsDateTime().NotNullable().WithDefaultValue(RawSql.Insert("now()"))
                .WithColumn("updated_at").AsDateTime().NotNullable().WithDefaultValue(RawSql.Insert("now()"));
        }
    }
}