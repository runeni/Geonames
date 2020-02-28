namespace Geonames.IntegrationsTests
{
    public class CreateTablesBuilder
    {
        public static string GetSql() =>
            @"create table if not exists geonames
            (
                id bigserial not null primary key,
                geoname_id     integer,
                name           varchar(200),
                asciiname      varchar(200),
                alternatenames varchar(10000),
                latitude       varchar(64),
                longitude      varchar(64),
                featureclass   char,
                featurecode    varchar(10),
                countrycode    char(2),
                cc2            varchar(200),
                admin1code     varchar(20),
                admin2code     varchar(80),
                admin3code     varchar(20),
                admin4code     varchar(20),
                population     bigint,
                elevation      integer,
                dem            integer,
                timezone       varchar(200),
                last_updated   varchar(255),
                name_tsv       tsvector
            );
            create table if not exists countries (
                id                   serial primary key,
                iso                  varchar(2),
                iso3                 varchar(3),
                iso_numeric          integer,
                fips                 varchar(2),
                name                 varchar(255),
                capital              varchar(255),
                area                 integer,
                population           integer,
                continent            varchar(255),
                tld                  varchar(3),
                currency_code        varchar(3),
                currency_name        varchar(255),
                phone                varchar(255),
                postal_code_format   varchar(255),
                postal_Code_regex    varchar(255),
                languages            varchar(255),
                geoname_id           integer,
                neighbours           varchar(255),
                equivalent_fips_code varchar(255)
            );
            create table if not exists featureclassifications (
                Id              serial primary key,
                ClassCode       varchar(12),
                Content         varchar(64),
                Lang            char(2),
                Description     varchar(200)
            );
            create table if not exists admin1codesascii (
                id serial primary key,
                identifier varchar(32),
                name       varchar(255),
                name_ascii varchar(255),
                geoname_id integer
            );
            create table if not exists admin2codes
            (
                id serial not null primary key,
                identifier varchar(32),
                name       varchar(255),
                name_ascii varchar(255),
                geoname_id integer
            );";
    };
}