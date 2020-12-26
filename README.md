# CleanArchitecture

## Overview

A clean architecture implementation based on this project https://github.com/drminnaar/chinook.

## Getting started

### Requirements
- docker
- docker-compose
- dotnet core 3.1

### Database setup
Postgres is used to store the data releated to the service.
-  when run on Windows docker, the *data/migrations* folder must be shared. 
Go to Settings -> Resources -> File sharing in Docker desktop application and add this folder.
- run script to create the database and populate with data. The script will start postgres container,  drop / create the *chinook* database and apply the database migrations using [Flyway](https://flywaydb.org/)

```
# create database and apply migrations
./refresh-chinook-db.ps1
```

### Run dependencies
- postgres
- pgAdmin

```
docker-compose up
```
### Check the database and the tables
* go to http://localhost:8080/ and log in with the user and password specified in the *docker-compose.yml*
* add the server running in docker using the *user*: postgres and *password*: password
![pgAdmin-server](docs/images/pgAdmin-server.png)
* check that database and the tables were created and populated with data
![pgAdmin-data](docs/images/pgAdmin-data.png)

### Run application

```
cd src\Catalog\Catalog.Api
dotnet build
dotnet run
```

## Notes

- use Swagger embedded in Microsoft packages and not a third party library
- [MediatR](https://github.com/jbogard/MediatR) is used to implement CQRS
- IUrlHelper is used to generate links inside the application
- the APIs allow for filtering which is passed along to the repository
- Pagination is returned using X-Pagination header
```json
{
    "CurrentPageNumber":2,
    "ItemCount":3503,
    "PageSize":10,
    "PageCount":351,
    "FirstPageUrl":"http://localhost:5000/v1/tracks?page=1&limit=10",
    "LastPageUrl":"http://localhost:5000/v1/tracks?page=351&limit=10",
    "NextPageUrl":"http://localhost:5000/v1/tracks?page=3&limit=10",
    "PreviousPageUrl":"http://localhost:5000/v1/tracks?page=1&limit=10",
    "CurrentPageUrl":"http://localhost:5000/v1/tracks?page=2&limit=10"
    }
```
- [Exception handling](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling) is done with UseExceptionHandler middleware which is recommended over exception filters
- ProblemDetails is used to return more details in HTTP response in case of problems (https://tools.ietf.org/html/rfc7807)

## TODOs

- fluent validation of requests
- autogenerate client
- HealthChecks
- Integration tests with TestHost
- CosmosDb persistence