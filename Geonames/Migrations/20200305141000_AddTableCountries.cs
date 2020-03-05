using System.ComponentModel.DataAnnotations.Schema;
using FluentMigrator;
using Geonames.Extensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Geonames.Migrations
{
    [Migration(20200305141000)]
    public class AddTableCountries : Migration
    {
        public override void Up()
        {
            Create.Table("countries")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("geoname_id").AsString(200).NotNullable()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("iso").AsString(2).NotNullable()
                .WithColumn("iso3").AsString(3).NotNullable()
                .WithColumn("iso_numeric").AsInt32().NotNullable()
                .WithColumn("fips").AsString(2).Nullable()
                .WithColumn("capital").AsString(255).Nullable()
                .WithColumn("area").AsString(64).NotNullable()
                .WithColumn("population").AsInt32().NotNullable()
                .WithColumn("continent").AsString(255).NotNullable()
                .WithColumn("tld").AsString(3).Nullable()
                .WithColumn("currency_code").AsString(3).Nullable()
                .WithColumn("currency_name").AsString(255).Nullable()
                .WithColumn("phone").AsString(255).Nullable()
                .WithColumn("postal_code_format").AsString(255).Nullable()
                .WithColumn("postal_code_regex").AsString(255).Nullable()
                .WithColumn("languages").AsString(255).Nullable()
                .WithColumn("neighbours").AsString(255).Nullable()
                .WithColumn("equivalent_fips_code").AsString(255).Nullable()
                .WithTimeStamps();
        }

        public override void Down()
        {
            Delete.Table("countries");
        }
    }
}