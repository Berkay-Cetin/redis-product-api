# Redis Product API
 
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