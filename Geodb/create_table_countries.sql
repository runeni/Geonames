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

create index if not exists counries_iso_idx on countries(iso);

alter table countries owner to geo;
