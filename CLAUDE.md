# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

```bash
# Build the entire solution
dotnet build BSI.Integra.Servicios.V5.sln

# Build only the main API project
dotnet build BSI.Integra.Servicios/BSI.Integra.Servicios.csproj

# Run the API (from BSI.Integra.Servicios/)
dotnet run --project BSI.Integra.Servicios/BSI.Integra.Servicios.csproj

# Run unit tests
dotnet test BSI.Integra.PruebasUnitarias/BSI.Integra.PruebasUnitarias.csproj

# Run a single test by name
dotnet test BSI.Integra.PruebasUnitarias/BSI.Integra.PruebasUnitarias.csproj --filter "FullyQualifiedName~TestMethodName"
```

## Architecture

This is an ASP.NET Core 6.0 REST API using a layered clean architecture with domain-based separation.

### Layer Flow

```
Controllers (BSI.Integra.Servicios)
    → Services (BSI.Integra.Aplicacion.*)
        → Repositories (BSI.Integra.Repositorio)
            → DbContexts / Dapper (BSI.Integra.Persistencia)
                → SQL Server
```

### Projects

| Project | Purpose |
|---------|---------|
| `BSI.Integra.Servicios` | Main API. 566+ controllers organized by domain (Comercial, Marketing, Planificacion, Finanzas, GestionPersonas, Operaciones, Interaccion, Calidad, Configuracion, Wavix, Wolkbox) |
| `BSI.Integra.Persistencia` | EF Core 6 models, DbContexts, connection factories. Three contexts: `IntegraDBContext`, `IntegraDBInteraccionContext`, `AulaVirtualContext` |
| `BSI.Integra.Repositorio` | Generic repository + Unit of Work pattern. `IUnitOfWork` exposes all 180+ repositories as lazy-loaded properties |
| `BSI.Integra.Aplicacion.DTO` | Data Transfer Objects per domain |
| `BSI.Integra.Aplicacion.Base` | Base classes, custom exceptions (BadRequestException, NotFoundException, ConflictException, UnauthorizedAccessRequestException) |
| `BSI.Integra.Aplicacion.Transversal` | Cross-cutting: helpers, validators, tools, socket services |
| `BSI.Integra.Aplicacion.{Comercial,Planificacion,Marketing,Finanzas,GestionPersonas,Operaciones,Interaccion}` | Domain-specific business logic services with Interface/Implementacion folders |
| `BSI.Integra.PruebasUnitarias` | Unit tests (.NET 8.0, MSTest, Moq) |

### Key Patterns

- **Controllers** inject `IUnitOfWork` and instantiate domain services inline. Most have `[EnableCors("CorsVista")]` and `[Route("api/[controller]")]`.
- **Data access** uses both EF Core (LINQ) and Dapper (`IDapperRepository`) for complex queries/stored procedures. Stored procedures are heavily used (e.g., `pla.SP_*`, `pw.SP_*`).
- **Entity naming**: EF models use `T` prefix (e.g., `TAlumno`, `TCampania`) in `Persistencia/Modelos/IntegraDB/`.
- **AutoMapper** maps between entities and DTOs.
- **Hangfire** handles background jobs (SQL Server storage).
- **JWT Bearer** authentication with custom token validation middleware in `Configurations/`.
- **Global exception handling** via `GlobalExceptionHandlingMiddleware`.

### Configuration (Program.cs)

The entry point configures: JWT auth, CORS (25+ whitelisted origins), two EF DbContexts, Hangfire, Swagger, SignalR, memory caching, and 500MB file upload limits. DI registers all repositories and services.

## Conventions

- Domain services follow `Interface/Implementacion` folder structure with `I{Name}Service` / `{Name}Service` naming.
- Repositories follow `Interface/Implementation` folder structure with `I{Name}Repository` / `{Name}Repository` naming.
- New repositories must be registered as properties in both `IUnitOfWork.cs` and `UnitOfWork.cs` (these are very large files, ~80KB and ~365KB respectively).
- DTOs live in `BSI.Integra.Aplicacion.DTO/SCode/Modelos/IntegraDB/` organized by domain.
- Controller folders match domain names under `Controllers/`.

## CI/CD

- Azure DevOps pipeline (`IntegraV5ServiciosDespliegue.yml`) triggers on `master` branch.
- Docker multi-stage build available (`Dockerfile`) based on `mcr.microsoft.com/dotnet/aspnet:6.0`.
