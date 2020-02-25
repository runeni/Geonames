te table admin1codesascii
(
    id         serial not null
        constraint admin1codesascii_pk
            primary key,
    identifier varchar(32),
    name       varchar(255),
    name_ascii varchar(255),
    geoname_id integer
);

alter table admin1codesascii
    owner to geo;

create index admin1codesascii__geoname_id_idx
    on admin1codesascii (geoname_id);

create index admin1codesascii_identifier_idx
	on admin1codesascii (identifier);

