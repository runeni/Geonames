-- drop table geonames;
create table if not exists geonames (
    Id              serial primary key,
    GeonameId       integer,
    Name            varchar(200),
    AsciiName       varchar(200),
    AlternateNames  varchar(10000),
    Latitude        varchar(64),
    Longitude       varchar(64),
    FeatureClass    char(1),
    FeatureCode     varchar(10),
    CountryCode     char(2),
    Cc2             varchar(200),
    Admin1Code      varchar(20),
    Admin2Code      varchar(80),
    Admin3Code      varchar(20),
    Admin4Code      varchar(20),
    Population      integer,
    Elevation       integer,
    Dem             integer,
    Timezone        varchar(200)
);

create index if not exists geonames_admin1code_idx on geonames(admin1code);
create index if not exists geonames_geonameid_idx on geonames(geonameid);
create index if not exists geonames_countrycode_idx on geonames(countrycode);
create index if not exists geonames_name_idx on geonames(name);