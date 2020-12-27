# CleanArchitecture

## Overview

A clean architecture implementation based on different sources like [drminnaar](https://github.com/drminnaar/chinook), [alexcodetuts](https://alexcodetuts.com/2020/02/05/asp-net-core-3-1-clean-architecture-invoice-management-app-part-1/) and [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers), [jasontaylor](https://jasontaylor.dev/clean-architecture-getting-started/)
![CleanArchitecture](docs/images/CleanArchitecture.png)

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
- [MediatR](https://github.com/jbogard/MediatR) is used to implement CQRS. A good explenation of the Mediator pattern can be found [here](https://refactoring.guru/design-patterns/mediator)
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
- ProblemDetails and ValidationProblemDetails is used to return more details in HTTP response in case of problems and complies with [RFC 7807](https://tools.ietf.org/html/rfc7807)
- the validation of requests can be done in several ways:
    - with data annotations
    ```cs
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 5, ErrorMessage = "Price should be set at 0-5 dollars")]
        public decimal Price { get; set; }
    ```

    - using MediatR pipeline behaviour as described [here](https://timdows.com/projects/use-mediatr-with-fluentvalidation-in-the-asp-net-core-pipeline/) or using a nuget package like [MediatR.Extensions.FluentValidation.AspNetCore](https://github.com/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore). EShopOnContainers uses this approach. The validators are defined on Commands  or Query objects in Application layer and a Domain exception is thrown which gets handled with an IExceptionFilter and a BadRequest response is produced

    - using [Fluent Validation with AspNetCore](https://docs.fluentvalidation.net/en/latest/aspnet.html). This approach is more generic since it does not require MediatR and this is the one used in this repo. Validation is done in the Application layer on the models bound by asp net core. When validation fails, a BadRequest response is sent using a ValidationProblemDetails model.
    ```json
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "One or more validation errors occurred.",
        "status": 400,
        "traceId": "|d23b405c-4cc972fec0cf47c2.",
        "errors": {
            "name": [
            "'Name' must be between 0 and 10 characters. You entered 11 characters."
            ],
            "PriceTo": [
            "Value must be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5"
            ],
            "PriceFrom": [
            "Value must be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5"
            ]
        }
    }
    ```

- [NSwag](https://github.com/RicoSuter/NSwag) is used to autogenerate the C# client library based on the details explained [here](https://blog.sanderaernouts.com/autogenerate-csharp-api-client-with-nswag). 
A client is generated for each controller from the Api project. 
In order to use the client from any other project, you need to add the [dependency injection in HTTP Client Factory](https://itnext.io/use-http-client-factory-with-nswag-generated-classes-in-asp-net-core-3-c1dd66ee004c) for it and then inject it in the classes where needed.
```cs
services.AddHttpClient<ITracksClient, TracksClient>(client => 
    client.BaseAddress = new Uri("https://localhost:5000"));
```

## TODOs

- implement a selector capability
- add documentation about the repository and unit of work
- investigate if [Scrutor](https://github.com/khellang/Scrutor) can be used to achive something similar to StructureMap scan.Assembly("InfraStructure") [this article]() to remove the project reference from Api to Infrastructure project
- HealthChecks
- Integration tests with TestHost
- CosmosDb persistence