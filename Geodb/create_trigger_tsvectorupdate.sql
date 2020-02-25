CREATE FUNCTION geonames_search_trigger() RETURNS trigger AS $$
begin
      new.name_tsv :=
        to_tsvector(new.name);
          return new;
end
$$ LANGUAGE plpgsql;

CREATE TRIGGER tsvectorupdate BEFORE INSERT OR UPDATE
    ON geonames FOR EACH ROW EXECUTE PROCEDURE geonames_search_trigger();
