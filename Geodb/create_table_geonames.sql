-- drop table geonames;
create table geonames
(
    id             serial not null
        constraint geonames_pkey
            primary key,
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
    population     integer,
    elevation      integer,
    dem            integer,
    timezone       varchar(200),
    name_tsv       tsvector
);

alter table geonames
    owner to geo;

create index geonames_admin1code_idx
    on geonames (admin1code);

create index geonames_geonameid_idx
    on geonames (geoname_id);

create index geonames_countrycode_idx
    on geonames (countrycode);

create index geonames_name_idx
    on geonames (name);

create index geonames_name_tsv_idx
    on geonames (name_tsv);
