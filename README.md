Note that this source was for demo purposes during the presentation only.

Needs the following to be installed:
- docker
- docker-compose
- dotnet SDK 7.0
- dotnet-ef -> `dotnet tool install -g dotnet-ef`

Depending on your installation, start postgres via  
`docker-compose -f postgres.yml up`  
or without the dash in `docker compose`  
`docker compose -f postgres.yml up`  


(Re-)create the test databases with `createdb.cmd` or `createdb.sh`.