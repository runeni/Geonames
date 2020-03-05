using FluentMigrator;
using Geonames.Extensions;

namespace Geonames.Migrations
{
    [Migration(20200305150700)]
    public class AddTableFeatureClassifications : Migration
    {
        public override void Up()
        {
            Create.Table("featureclassifications")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("class_code").AsString(12).NotNullable().Indexed()
                .WithColumn("content").AsString(64).NotNullable()
                .WithColumn("description").AsString(255).Nullable()
                .WithTimeStamps();
        }

        public override void Down()
        {
            Delete.Table("featureclassifications");
        }
    }
}