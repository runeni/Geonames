Download gazetteer data from Geonames
-------------------------------------
wget https://download.geonames.org/export/dump/allCountries.zip

wget 'http://api.geonames.org/countryInfoCSV?lang=nb&username=yourusername'
(wget https://download.geonames.org/export/dump/countryInfo.txt)

wget https://download.geonames.org/export/dump/admin1CodesASCII.txt

wget https://download.geonames.org/export/dump/featureCodes_nb.txt


Unzip if needed and import as CSV to db
---------------------------------------
Use a database tool like DataGrip.


NOTES on how to import allCountries
-----------------------------------
Use DataGrip to import as CSV.
-Value separator: tab
-Row separator: newline
-Remove quotation settings
-Check insert inconvertible values as null.

