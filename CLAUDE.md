# CLAUDE.md

Este archivo proporciona orientación a Claude Code (claude.ai/code) para trabajar con el código de este repositorio.

## Comandos de Compilación y Ejecución

```bash
# Compilar toda la solución
dotnet build BSI.Integra.Servicios.V5.sln

# Compilar solo el proyecto API principal
dotnet build BSI.Integra.Servicios/BSI.Integra.Servicios.csproj

# Ejecutar la API
dotnet run --project BSI.Integra.Servicios/BSI.Integra.Servicios.csproj

# Ejecutar pruebas unitarias
dotnet test BSI.Integra.PruebasUnitarias/BSI.Integra.PruebasUnitarias.csproj

# Ejecutar una prueba específica por nombre
dotnet test BSI.Integra.PruebasUnitarias/BSI.Integra.PruebasUnitarias.csproj --filter "FullyQualifiedName~NombreDelTest"
```

## Arquitectura

API REST en ASP.NET Core 6.0 con arquitectura limpia por capas y separación por dominios.

### Flujo de Capas

```
Controllers (BSI.Integra.Servicios)
    → Services (BSI.Integra.Aplicacion.*)
        → Repositories (BSI.Integra.Repositorio)
            → DbContexts / Dapper (BSI.Integra.Persistencia)
                → SQL Server
```

### Proyectos

| Proyecto | Propósito |
|----------|-----------|
| `BSI.Integra.Servicios` | API principal. 566+ controladores organizados por dominio (Comercial, Marketing, Planificacion, Finanzas, GestionPersonas, Operaciones, Interaccion, Calidad, Configuracion, Wavix, Wolkbox) |
| `BSI.Integra.Persistencia` | Modelos EF Core 6, DbContexts, fábricas de conexión. Tres contextos: `IntegraDBContext`, `IntegraDBInteraccionContext`, `AulaVirtualContext` |
| `BSI.Integra.Repositorio` | Patrón repositorio genérico + Unit of Work. `IUnitOfWork` expone los 180+ repositorios como propiedades lazy-loaded |
| `BSI.Integra.Aplicacion.DTO` | Objetos de transferencia de datos por dominio |
| `BSI.Integra.Aplicacion.Base` | Clases base, excepciones personalizadas (BadRequestException, NotFoundException, ConflictException, UnauthorizedAccessRequestException) |
| `BSI.Integra.Aplicacion.Transversal` | Transversales: helpers, validadores, herramientas, servicios socket |
| `BSI.Integra.Aplicacion.{Comercial,Planificacion,Marketing,Finanzas,GestionPersonas,Operaciones,Interaccion}` | Servicios de lógica de negocio por dominio con carpetas Interface/Implementacion |
| `BSI.Integra.PruebasUnitarias` | Pruebas unitarias (.NET 8.0, MSTest, Moq) |

### Patrones Clave

- **Controllers** inyectan `IUnitOfWork` e instancian servicios de dominio inline. La mayoría tienen `[EnableCors("CorsVista")]` y `[Route("api/[controller]")]`.
- **Acceso a datos** usa tanto EF Core (LINQ) como Dapper (`IDapperRepository`) para consultas complejas/stored procedures. Los stored procedures se usan intensivamente (ej. `pla.SP_*`, `pw.SP_*`).
- **Nomenclatura de entidades**: los modelos EF usan prefijo `T` (ej. `TAlumno`, `TCampania`) en `Persistencia/Modelos/IntegraDB/`.
- **AutoMapper** mapea entre entidades y DTOs.
- **Hangfire** gestiona trabajos en segundo plano (almacenamiento en SQL Server).
- **JWT Bearer** autenticación con middleware de validación personalizado en `Configurations/`.
- **Manejo global de excepciones** mediante `GlobalExceptionHandlingMiddleware`.

### Configuración (Program.cs)

El punto de entrada configura: autenticación JWT, CORS (25+ orígenes permitidos), dos DbContexts de EF, Hangfire, Swagger, SignalR, caché en memoria y límite de carga de archivos de 500MB. La inyección de dependencias registra todos los repositorios y servicios.

## Convenciones

- Los servicios de dominio siguen la estructura de carpetas `Interface/Implementacion` con nomenclatura `I{Nombre}Service` / `{Nombre}Service`.
- Los repositorios siguen la estructura `Interface/Implementation` con nomenclatura `I{Nombre}Repository` / `{Nombre}Repository`.
- Los nuevos repositorios deben registrarse como propiedades en `IUnitOfWork.cs` y `UnitOfWork.cs` (archivos muy grandes, ~80KB y ~365KB respectivamente).
- Los DTOs están en `BSI.Integra.Aplicacion.DTO/SCode/Modelos/IntegraDB/` organizados por dominio.
- Las carpetas de controladores corresponden a los nombres de dominio bajo `Controllers/`.

## CI/CD

- Pipeline de Azure DevOps (`IntegraV5ServiciosDespliegue.yml`) se ejecuta en la rama `master`.
- Build Docker multi-stage disponible (`Dockerfile`) basado en `mcr.microsoft.com/dotnet/aspnet:6.0`.
