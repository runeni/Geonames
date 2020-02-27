select geo.*, feature.*, co.*, admin.*, admin2.*
                    from geonames geo
                      join featureclassifications feature on feature.classcode = geo.featureclass || '.' || geo.featurecode
                      join countries co on co.iso = geo.countrycode
                      join admin1codesascii admin on admin.identifier = geo.countrycode || '.' || geo.admin1code
                      join admin2codes admin2 on admin2.identifier = geo.countrycode || '.' || geo.admin1.code || '.' || geo.admin2code
                    where geo.name_tsv @@ to_tsquery('simple', @SearchString)