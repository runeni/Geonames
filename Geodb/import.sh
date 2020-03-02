#!/usr/bin/env bash

psql geonames_development --command "\copy public.geonames (geoname_id, name, asciiname, alternatenames, latitude, longitude, featureclass, featurecode, countrycode, cc2, admin1code, admin2code, admin3code, admin4code, population, elevation, dem, timezone, last_updated) FROM 'tmp/allCountries.txt' CSV DELIMITER E'\t' QUOTE E'\b' ESCAPE '\' NULL AS ''"
