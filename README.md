# Redis Product API

![.NET](https://img.shields.io/badge/.NET-8-512BD4?style=flat&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=flat&logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat&logo=postgresql)
![Redis](https://img.shields.io/badge/Redis-Cache-FF4438?style=flat&logo=redis)
![EF Core](https://img.shields.io/badge/Entity_Framework-Core-512BD4?style=flat&logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat&logo=docker)
 
A .NET 8 Web API demonstrating Redis caching with PostgreSQL.
 
## Technologies
 
- .NET 8
- ASP.NET Core Web API
- PostgreSQL
- Entity Framework Core
- Redis
- StackExchange.Redis
- Docker
 
## Features
 
- Product CRUD API
- Redis Cache Aside Pattern
- PostgreSQL persistence
- Cache invalidation
- Swagger API documentation
 
## Architecture
 
Client → API → Redis Cache → PostgreSQL
 
## Endpoints
 
GET /api/product
GET /api/product/{id}
POST /api/product
PUT /api/product/{id}
DELETE /api/product/{id}