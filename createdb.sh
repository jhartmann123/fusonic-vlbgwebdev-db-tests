#!/bin/bash

cd FancyLib

docker run --rm --net neovac_default -e PGPASSWORD=postgres tmaier/postgresql-client -h postgres -p 5432 -U postgres -d postgres -c "DROP DATABASE IF EXISTS fancylib WITH(FORCE)"
docker run --rm --net neovac_default -e PGPASSWORD=postgres tmaier/postgresql-client -h postgres_test -p 5432 -U postgres -d postgres -c "DROP DATABASE IF EXISTS fancylib WITH(FORCE)"

dotnet ef database update --connection "Port=5432;Host=localhost;Username=postgres;Password=postgres;Database=fancylib"
dotnet ef database update --connection "Port=5433;Host=localhost;Username=postgres;Password=postgres;Database=fancylib"

pause