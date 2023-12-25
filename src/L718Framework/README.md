# L718Framework

Framework to group common patterns used within all of the Services and Projects

This project is a simplified version of [FullStack Hero Microsservices Boilerplate](https://github.com/fullstackhero/dotnet-microservices-boilerplate) adapted to my needs and view of Domain Driven Design

[Roadmap]
* Isolate the project: Turn this on an Isolated project that I can use for more ideas. 


## L718Framework.Core
Provides common usage for the Domain Projects and Applications

#### Provides
* Domain Model Based Entities
* Domain Repositories
* Common Exceptions
* Common Helpers and Extension Methods 


## L718Framework.Infrastructure
Provides the Infrastructure Interfaces to common Cross Cutting systems within the application.

#### Provides
* Controller with Mediator support (MediatR)
* Logging Support (Microsoft Logging Framework)
* DTO Mapping (AutoMapper)
* Data Context Interfaces (To be used on persistence layer)
* Common Configuration Extensions to define unique configuration between all the APIs and Services

## Hooks
* [Dapper Pesistence Layer](Persistence.DapperDatabase/README.md)