# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

> Las respuestas y la documentación generada se mantienen en español (preferencia del usuario).

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

> **Nota de targets**: la API y todos los proyectos de aplicación apuntan a `net6.0`. Solo `BSI.Integra.PruebasUnitarias` apunta a `net8.0` (MSTest.Sdk 3.x + Moq). No mezclar APIs específicas de .NET 8 en código que vaya a la API.

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
| `BSI.Integra.Servicios` | API principal. ~598 controladores organizados por dominio bajo `Controllers/`. Contiene además `Configurations/` (middlewares JWT y manejo global de excepciones), `Jobs/` (workers Hangfire) y `Services/` (servicios cross-domain como `ActividadEnvioService` y `ClasificacionRespuestaService`) |
| `BSI.Integra.Persistencia` | Modelos EF Core 6, DbContexts, fábricas de conexión. Backends: `IntegraDBContext` (SQL principal), `IntegraDBInteraccionContext` (SQL de interacciones/chats) y `MongoDBContext` (`IntegraDBMongo/`). Existe `AulaVirtualContext` en `Modelos/AulaVirtual/` pero **no se registra en `Program.cs`** — tratarlo como legacy salvo evidencia en contrario |
| `BSI.Integra.Repositorio` | Patrón repositorio genérico + Unit of Work. **Dos UoW**: `IUnitOfWork` (IntegraDB, 180+ repositorios lazy-loaded) e `IUnitOfWorkInteraccion` (IntegraDB_Interaccion). Repositorios Mongo viven aparte en `IntegraDBMongo/` con `IMongoDocumentRepository` |
| `BSI.Integra.Aplicacion.DTO` | Objetos de transferencia de datos por dominio |
| `BSI.Integra.Aplicacion.Base` | Clases base, excepciones personalizadas (`BadRequestException`, `NotFoundException`, `ConflictException`, `UnauthorizedAccessRequestException`) |
| `BSI.Integra.Aplicacion.Transversal` | Transversales: helpers, validadores, herramientas, servicios socket, integraciones de WhatsApp y Google Ads |
| `BSI.Integra.Aplicacion.Servicios` | Integraciones con sistemas externos: Sentinel/Experian (SOAP), Moodle, TMK (Mail/Twilio/IMAP), Wavix (telefonía) y Wolkbox |
| `BSI.Integra.Aplicacion.{Comercial,Planificacion,Marketing,Finanzas,GestionPersonas,Operaciones,Interaccion}` | Servicios de lógica de negocio por dominio con carpetas `Interface/Implementacion` |
| `BSI.Integra.PruebasUnitarias` | Pruebas unitarias (.NET 8.0, MSTest.Sdk 3.x, Moq) |

### Patrones Clave

- **Controllers** inyectan `IUnitOfWork` e instancian servicios de dominio inline. La mayoría tienen `[EnableCors("CorsVista")]` y `[Route("api/[controller]")]`.
- **Acceso a datos**: dos rutas paralelas — EF Core (LINQ) para CRUD y Dapper (`IDapperRepository`, `IDapperRepositoryInteraccion`) para consultas complejas y stored procedures. Los SP se usan intensivamente (ej. `pla.SP_*`, `pw.SP_*`).
- **Nomenclatura de entidades**: los modelos EF usan prefijo `T` (ej. `TAlumno`, `TCampania`) en `Persistencia/Modelos/IntegraDB/`.
- **AutoMapper** mapea entre entidades y DTOs.
- **Hangfire**: paquetes y jobs presentes (`BSI.Integra.Servicios/Jobs/ActividadesCongeladasJob`, `ClasificacionRespuestaJob`, `GmailPlaBackgroundWorker`), pero `AddHangfire` y `RecurringJob.AddOrUpdate` están **comentados** en `Program.cs`. Si se necesita activarlos, descomentar el bloque y registrar el server.
- **JWT Bearer** autenticación con `JwtMiddleware` y `JwtActionFilter` en `Configurations/`. El secreto se lee de `TokenKey` en configuración.
- **Manejo global de excepciones** vía `GlobalExceptionHandlingMiddleware` registrado por `app.AddGlobalErrorHandler()` (extension en `Configurations/ApplicationBuilderExtensions.cs`).

### Configuración (Program.cs)

Punto de entrada (~330 líneas) que registra inline cientos de servicios. Configura:

- **CORS** (`CorsVista`) con ~30 orígenes permitidos (`localhost:4200`, `*.bsginstitute.com`, hooks externos).
- **Connection strings**: `IntegraDB` y `IntegraDB_Interaccion` (dos DbContexts SQL Server) + `MongoDBSettings` (sección de configuración).
- **JWT** con `LifetimeValidator` custom (`expires > DateTime.Now`).
- **Subida de archivos** hasta 500MB (`FormOptions.MultipartBodyLengthLimit`) y `Kestrel.MaxRequestBodySize = long.MaxValue`.
- **Swagger**, **MemoryCache**, **HttpContextAccessor**, **HttpClientFactory** (incluye named client `PythonLlm` apuntando a la API de IA de planificación).
- **Inyección de dependencias** masiva: `IUnitOfWork`/`IUnitOfWorkInteraccion`, repositorios y servicios por dominio. Cuando agregues un servicio o repositorio nuevo, **debes registrarlo aquí**, además de en la propiedad correspondiente del UoW.

### Integraciones externas

Documentadas porque exigen credenciales/conectividad y aparecen distribuidas:

- **Python LLM API** (`http://ia-automatizacion-planificacion-api.bsginstitute.com`) vía `HttpClient` named `"PythonLlm"`, usado por `ClasificacionRespuestaService`.
- **Google Ads** (`Google.Ads.GoogleAds`) — conversiones desde `AdwordsConversionService`.
- **Sendingblue/Brevo** — `ISendingblueService` (Singleton).
- **Azure Blob Storage** y `WindowsAzure.Storage` — almacenamiento de archivos y media.
- **Facebook/Messenger, LinkedIn, Computrabajo, Glassdoor, Google** — servicios de reseñas y leads en `Aplicacion.Marketing.SCode`.
- **Sentinel (Experian)** SOAP — `BSI.Integra.Aplicacion.Servicios/SCode/ExperianSentinel/`.
- **Moodle**, **Twilio**, **Wavix**, **Wolkbox** — viven en `BSI.Integra.Aplicacion.Servicios/SCode/Service/`.

## Convenciones

- Servicios de dominio: estructura `Interface/Implementacion`, nomenclatura `I{Nombre}Service` / `{Nombre}Service`.
- Repositorios: estructura `Interface/Implementation`, nomenclatura `I{Nombre}Repository` / `{Nombre}Repository`.
- **Registro de nuevos repositorios**: dos pasos obligatorios — (1) propiedad lazy en `IUnitOfWork.cs` + `UnitOfWork.cs` (archivos enormes, ~80KB y ~365KB; usar el patrón existente como referencia), y (2) registro DI en `Program.cs` si el servicio se inyecta directamente fuera del UoW.
- DTOs en `BSI.Integra.Aplicacion.DTO/SCode/Modelos/IntegraDB/` organizados por dominio.
- Controladores agrupados por dominio bajo `Controllers/`. **No** usar subcarpetas por dominio (todos los controllers viven planos).
- Para integraciones nuevas con sistemas externos, preferir `BSI.Integra.Aplicacion.Servicios` antes que ensuciar dominios de negocio.

## CI/CD

- Pipeline de Azure DevOps (`IntegraV5ServiciosDespliegue.yml`) se ejecuta en la rama `master`.
- Build Docker multi-stage disponible (`Dockerfile`) basado en `mcr.microsoft.com/dotnet/aspnet:6.0`.
