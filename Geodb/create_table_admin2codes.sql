:screate table admin2codes
(
    id         serial not null
        constraint admin2codes_pk
            primary key,
    identifier varchar(32),
    name       varchar(255),
    name_ascii varchar(255),
    geoname_id integer
);

alter table admin2codes
    owner to geo;

create index admin2codes_geoname_id_idx
    on admin2codes (geoname_id);

create index admin2codes_identifier_idx
    on admin2codes (identifier);e
