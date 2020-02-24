create table if not exists featureclassifications (
    Id              serial primary key,
    ClassCode       varchar(12),
    Content         varchar(64),
    Lang            char(2),
    Description     varchar(200)
);

create index if not exists featureclassifications_classcode_idx on featureclassifications(ClassCode);
create index if not exists featureclassifications_lang_idx on featureclassifications(Lang);
