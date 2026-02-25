# Análisis Técnico — BSI.Integra.Servicios V5

> **Fecha de análisis:** 19 de febrero de 2026
> **Proyecto:** BSI.Integra.Servicios.V5
> **Ubicación:** `c:\Proyectos\GithubCopilot\ServiciosV5`
> **Framework:** .NET 6.0 — ASP.NET Core Web API

---

## Tabla de Contenidos

1. [Resumen Ejecutivo](#1-resumen-ejecutivo)
2. [Stack Tecnológico](#2-stack-tecnológico)
3. [Arquitectura del Sistema](#3-arquitectura-del-sistema)
4. [Estructura de Proyectos](#4-estructura-de-proyectos)
5. [Dominios de Negocio](#5-dominios-de-negocio)
6. [Controladores (API)](#6-controladores-api)
7. [Capa de Servicios](#7-capa-de-servicios)
8. [Capa de Repositorios](#8-capa-de-repositorios)
9. [Entidades y Modelos de Datos](#9-entidades-y-modelos-de-datos)
10. [Data Transfer Objects (DTOs)](#10-data-transfer-objects-dtos)
11. [Integraciones Externas](#11-integraciones-externas)
12. [Autenticación y Autorización](#12-autenticación-y-autorización)
13. [Configuración e Infraestructura](#13-configuración-e-infraestructura)
14. [Middleware y Pipeline](#14-middleware-y-pipeline)
15. [Patrones de Diseño Implementados](#15-patrones-de-diseño-implementados)
16. [Métricas del Proyecto](#16-métricas-del-proyecto)
17. [Observaciones y Recomendaciones](#17-observaciones-y-recomendaciones)
18. [Rutas de Referencia](#18-rutas-de-referencia)

---

## 1. Resumen Ejecutivo

**BSI.Integra.Servicios V5** es una plataforma empresarial de tecnología educativa (EdTech) desarrollada por **BSG Institute**. El sistema implementa un backend API REST de gran escala que centraliza la gestión comercial, marketing, finanzas, recursos humanos y operaciones del instituto.

El proyecto sigue una arquitectura N-Capas orientada a dominios, con más de **566 controladores**, **878+ entidades**, **939 repositorios** y **12+ integraciones** con servicios externos. Representa una solución CRM + ERP + Marketing Automation construida sobre .NET 6.

### Características Principales

| Característica | Detalle |
|----------------|---------|
| Tipo de aplicación | REST API Backend (.NET 6) |
| Patrón arquitectónico | N-Capas + DDD |
| Base de datos | SQL Server (2 bases de datos) |
| Autenticación | JWT Bearer |
| Programación asíncrona | Hangfire (background jobs) |
| Almacenamiento de archivos | Azure Blob Storage |
| Documentación de API | Swagger / OpenAPI |
| Contenedores | Docker (Linux target) |

---

## 2. Stack Tecnológico

### Framework y Lenguaje

| Componente | Versión / Detalle |
|------------|-------------------|
| Plataforma | .NET 6.0 |
| Lenguaje | C# (Language Version: Preview) |
| API | ASP.NET Core Web API |
| Nullable References | Habilitado |
| Implicit Usings | Habilitado |

### Dependencias NuGet Principales

| Paquete | Propósito |
|---------|-----------|
| `Microsoft.EntityFrameworkCore` | ORM principal |
| `Microsoft.EntityFrameworkCore.SqlServer` | Provider SQL Server |
| `Dapper` | Micro-ORM para queries de alto rendimiento |
| `Hangfire` | Background jobs y tareas programadas |
| `Hangfire.SqlServer` | Almacenamiento de jobs en SQL Server |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | Autenticación JWT |
| `Microsoft.IdentityModel.Tokens` | Gestión de tokens |
| `Swashbuckle.AspNetCore` | Swagger / OpenAPI |
| `Azure.Storage.Blobs` | Azure Blob Storage |
| `WindowsAzure.Storage` | SDK legado Azure Storage |
| `Microsoft.Azure.KeyVault.WebKey` | Azure Key Vault |
| `Azure.DataMovement` | Movimiento de datos Azure |
| `Google.Ads.GoogleAds` | Google Ads API |
| `sib-api-v3-sdk` | SendingBlue / Brevo SDK |
| `Newtonsoft.Json` | Serialización JSON |
| `AutoMapper` | Mapeo de objetos (DTO ↔ Entity) |
| `System.IdentityModel.Tokens.Jwt` | JWT generation/validation |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | ASP.NET Identity |
| `ClosedXML` / `EPPlus` | Exportación a Excel |
| `iTextSharp` / `PdfSharp` | Generación de PDF |
| `Twilio` (SMS) | Mensajería de texto |

---

## 3. Arquitectura del Sistema

### Diagrama de Capas

```
┌─────────────────────────────────────────────────────────────────┐
│                   CAPA DE PRESENTACIÓN (API)                     │
│           BSI.Integra.Servicios  (566 Controllers)               │
│        JWT Auth │ Swagger │ CORS │ Middleware Global             │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────────┐
│                    CAPA DE APLICACIÓN                            │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│  │  Comercial   │  │  Marketing   │  │      Finanzas        │  │
│  │  (88+ svcs)  │  │  (70+ svcs)  │  │     (30+ svcs)       │  │
│  └──────────────┘  └──────────────┘  └──────────────────────┘  │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│  │GestionPerson.│  │ Operaciones  │  │    Planificacion     │  │
│  │  (25+ svcs)  │  │  (20+ svcs)  │  │      (15+ svcs)      │  │
│  └──────────────┘  └──────────────┘  └──────────────────────┘  │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────────────┐  │
│  │ Transversal  │  │  Servicios   │  │     Interaccion      │  │
│  │  (10+ svcs)  │  │   (5+ svcs)  │  │      (8+ svcs)       │  │
│  └──────────────┘  └──────────────┘  └──────────────────────┘  │
│                   BSI.Integra.Aplicacion.DTO                     │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────────┐
│                    CAPA DE INFRAESTRUCTURA                       │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │          BSI.Integra.Repositorio                           │ │
│  │  GenericRepository<T>  │  UnitOfWork  │  DapperRepository  │ │
│  │         939 Repositorios específicos                       │ │
│  └────────────────────────────────────────────────────────────┘ │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │          BSI.Integra.Persistencia                          │ │
│  │  IntegraDBContext  │  IntegraDBInteraccionContext          │ │
│  │         878+ Entidades EF Core                             │ │
│  └────────────────────────────────────────────────────────────┘ │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────▼─────────────────────────────────────┐
│                      BASE DE DATOS                               │
│         IntegraDB (Principal)  │  IntegraDB_Interaccion          │
│                SQL Server                                        │
└─────────────────────────────────────────────────────────────────┘
```

### Flujo de una Petición HTTP

```
Cliente → JWT Middleware → Controller → Service → Repository → DB
                                          ↕
                                  UnitOfWork (transacción)
                                          ↕
                               Integraciones Externas
```

---

## 4. Estructura de Proyectos

### Solución: `BSI.Integra.Servicios.V5.sln`

```
ServiciosV5/
├── BSI.Integra.Servicios/                    ← Proyecto principal (API Host)
│   ├── Controllers/                           ← 566 controladores
│   ├── Configurations/                        ← Middleware, extensiones
│   ├── Helpers/                               ← TokenManager, utilidades
│   ├── Program.cs                             ← Entry point, DI, configuración
│   └── appsettings.json                       ← Configuración de la app
│
├── BSI.Integra.Aplicacion.Comercial/          ← Servicios de dominio Comercial
│   └── SCode/Service/                         ← 88+ implementaciones de servicio
│
├── BSI.Integra.Aplicacion.Marketing/          ← Servicios de dominio Marketing
│   └── SCode/Service/                         ← 70+ implementaciones de servicio
│
├── BSI.Integra.Aplicacion.Finanzas/           ← Servicios de dominio Finanzas
├── BSI.Integra.Aplicacion.GestionPersonas/    ← Servicios de dominio RRHH
├── BSI.Integra.Aplicacion.Operaciones/        ← Servicios de dominio Operaciones
├── BSI.Integra.Aplicacion.Planificacion/      ← Servicios de dominio Planificación
├── BSI.Integra.Aplicacion.Servicios/          ← Servicios transversales
├── BSI.Integra.Aplicacion.Transversal/        ← Helpers globales, configuración
├── BSI.Integra.Aplicacion.Interaccion/        ← Servicios de interacción
│
├── BSI.Integra.Aplicacion.DTO/                ← Data Transfer Objects (500+)
│   └── SCode/Modelos/                         ← DTOs por dominio
│
├── BSI.Integra.Repositorio/                   ← Capa de acceso a datos
│   ├── Repository/                            ← 939 repositorios
│   │   ├── GenericRepository.cs               ← Base genérica EF Core
│   │   └── DapperRepository.cs                ← Queries SQL directas
│   └── UnitOfWork/
│       └── UnitOfWork.cs                      ← Gestión de transacciones
│
└── BSI.Integra.Persistencia/                  ← Modelos EF Core
    └── Entidades/IntegraDB/                   ← 878+ entidades de dominio
```

**Total de proyectos en la solución: 13**

---

## 5. Dominios de Negocio

### Mapa de Dominios

```
                     ┌─────────────────┐
                     │    COMERCIAL    │
                     │  Sales & CRM    │
                     │   88+ servicios │
                     └────────┬────────┘
                              │
          ┌───────────────────┼───────────────────┐
          │                   │                   │
   ┌──────▼──────┐   ┌────────▼────────┐  ┌──────▼──────┐
   │  MARKETING  │   │   OPERACIONES   │  │  FINANZAS   │
   │  Campañas   │   │  Flujos/Tareas  │  │  Pagos/Caja │
   │  70+ svcs   │   │   20+ servicios │  │  30+ svcs   │
   └─────────────┘   └─────────────────┘  └─────────────┘
          │                   │                   │
   ┌──────▼──────┐   ┌────────▼────────┐  ┌──────▼──────┐
   │   RRHH      │   │ PLANIFICACION   │  │ INTERACCION │
   │  Personal   │   │  Agenda/Sched.  │  │  Tracking   │
   │  25+ svcs   │   │   15+ servicios │  │  8+ svcs    │
   └─────────────┘   └─────────────────┘  └─────────────┘
```

### Descripción Detallada por Dominio

#### 5.1 Comercial (Sales & CRM)
Núcleo del negocio. Gestiona todo el ciclo de vida del prospecto/alumno.

**Responsabilidades:**
- Gestión de oportunidades comerciales (`OportunidadComercial`)
- Ciclo de matrículas (`MatriculaCabecera`, `MatriculaCabeceraBeneficios`)
- Gestión de alumnos (`Alumno`, `AlumnoCuponRegistro`)
- Chat con asesores (`Chat`, `ChatDetalle`, `AsesorChat`)
- Llamadas VoIP (Wavix + Asterisk)
- Agenda y citas (`Agenda`, `AgendaTab`, `Cita`)
- Gestión de actividades de seguimiento (`ActividadCabecera`, `ActividadDetalle`)
- Control de competidores (`OportunidadCompetidor`)
- Documentos de oportunidad (`DocumentoOportunidad`)
- Ficha del alumno (`FichaAlumno`)
- Reportes de conversión y seguimiento

**Servicios destacados:**
- `OportunidadComercialService` — Pipeline de ventas
- `MatriculaCabeceraBeneficiosService` — Beneficios de matrícula
- `LlamadaWavixService` — Integración VoIP
- `InteraccionService` — Registro de interacciones
- `ReporteTasaConversionConsolidadaService` — KPIs de conversión
- `SemaforoFinancieroService` — Dashboard financiero
- `TableroComercialService` — Dashboard comercial
- `SentinelService` — Evaluación crediticia

#### 5.2 Marketing
Automatización de marketing multicanal.

**Responsabilidades:**
- Campañas de mailing y WhatsApp (`CampaniaGeneral`, `CampaniaGeneralWhatsApp`)
- Remarketing y retargeting (`CampaniaRemarketingGeneral`)
- Gestión de audiencias Facebook (`FacebookAudiencia`, `ConjuntoLista`)
- Segmentación de prospectos (`FiltroSegmento`, `FiltroSegmentoDetalle`)
- Bot de WhatsApp con esquema conversacional (`EsquemaWhatsApp*`)
- Integración Facebook Leads Ads
- Messenger Bot
- Cupones de alumnos (`AlumnoCuponRegistro`)
- Reportes de campañas WhatsApp

**Bot de WhatsApp (sistema de IA/NLP):**
```
EsquemaWhatsAppAsignacionService  ← Asignación de bot por contexto
EsquemaLecturaMensajeService      ← Parseo de mensajes entrantes
EsquemaInterpretarInformacionService ← Comprensión del mensaje
EsquemaRespuestaService           ← Generación de respuesta
EsquemaActividadService           ← Acciones post-respuesta
MensajeExactoService              ← Matching exacto de frases
FaseService                       ← Fases del flujo conversacional
PerfilService                     ← Perfiles de usuario
```

#### 5.3 Finanzas
Gestión financiera integrada.

**Responsabilidades:**
- Caja (ingresos/egresos) (`Caja`, `CajaEgreso`, `CajaPorRendir`)
- Cronogramas de pago (`CronogramaPago`, `CronogramaPagoDetalleFinal`)
- Comprobantes de pago (`ComprobantePago`)
- Cuentas bancarias (`CuentaBancaria`)
- Centros de costo (`CentroCosto`)
- Integración contable SIIGO
- Pasarela de pago PartnerWeb (`PasarelaPagoPwService`)
- Medios de pago en matrículas

#### 5.4 Gestión de Personas (RRHH)
Recursos humanos y control de acceso.

**Responsabilidades:**
- Gestión de personal (`Personal`)
- Roles y permisos (`Rol`, `RolPermiso`, `PermisoClave`)
- Cargos y áreas (`Cargo`, `AreaTrabajo`)
- Acceso y configuración personal (`ConfiguracionAccesoPersonal`)
- Perfiles de usuario (`Perfil`)

#### 5.5 Operaciones
Flujos operacionales y control de calidad.

**Responsabilidades:**
- Gestión de actividades operativas
- Asignación automática de coordinadores
- Control de calidad de llamadas (`CalidadLlamadaLog`)
- Ocurrencias (`OcurrenciaService`)
- Archivos de llamada (`GestionArchivoLlamadaService`)

#### 5.6 Planificación
Configuración y planificación académica.

**Responsabilidades:**
- Configuración de comunicación en matrícula
- Tipos de descuento en solicitudes
- Cronogramas de capacitación
- Configuración de periodos de matrícula

#### 5.7 Transversal (Cross-cutting)
Servicios y utilidades globales.

**Responsabilidades:**
- Configuración global del sistema (`ConfiguracionIntegraService`)
- Valores estáticos (`IValorEstatico`)
- Tracking de conversiones AdWords (`AdwordsConversionService`)
- Helpers y extensiones globales

#### 5.8 Interacción
Tracking de interacciones de prospectos/alumnos con el sistema.

**Responsabilidades:**
- Registro de interacciones con páginas web
- Historial de interacciones por prospecto
- Segunda base de datos independiente (`IntegraDB_Interaccion`)

---

## 6. Controladores (API)

### Información General

- **Total de controladores:** 566
- **Ruta base:** `/api/[controller]`
- **Formato de respuesta:** JSON (PascalCase por defecto)
- **Documentación:** Swagger UI disponible

### Estructura Típica de un Controlador

```csharp
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EntidadController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenManager _tokenManager;

    // GET /api/Entidad/ObtenerTodo
    [HttpGet("ObtenerTodo")]
    public IActionResult ObtenerTodo() { }

    // GET /api/Entidad/ObtenerPorId/{id}
    [HttpGet("ObtenerPorId/{id}")]
    public IActionResult ObtenerPorId(int id) { }

    // POST /api/Entidad/Insertar
    [HttpPost("Insertar")]
    public IActionResult Insertar([FromBody] EntidadDTO dto) { }

    // POST /api/Entidad/Actualizar
    [HttpPost("Actualizar")]
    public IActionResult Actualizar([FromBody] EntidadDTO dto) { }

    // POST /api/Entidad/Eliminar/{id}
    [HttpPost("Eliminar/{id}")]
    public IActionResult Eliminar(int id) { }
}
```

### Controladores Representativos por Dominio

#### Comercial
| Controlador | Endpoints | Descripción |
|------------|-----------|-------------|
| `ActividadCabeceraController` | 8+ | CRUD de actividades de seguimiento |
| `AlumnoController` | 7+ | Gestión de alumnos |
| `MatriculaCabeceraController` | 10+ | Proceso de matrícula |
| `OportunidadComercialController` | 12+ | Pipeline de ventas |
| `ChatController` | 8+ | Chat con asesores |
| `AgendaController` | 6+ | Calendario y citas |
| `WavixController` | 6+ | Llamadas VoIP |
| `LlamadaWebphoneAsteriskController` | 5+ | Integración Asterisk |
| `SentinelController` | 4+ | Scoring crediticio |
| `TableroComercialController` | 5+ | Dashboard comercial |
| `ReporteSeguimientoOportunidadController` | 4+ | Reportes de oportunidades |

#### Marketing
| Controlador | Endpoints | Descripción |
|------------|-----------|-------------|
| `CampaniaGeneralController` | 10+ | Gestión de campañas |
| `FacebookAudienciController` | 6+ | Audiencias Facebook |
| `FacebookLeadsController` | 4+ | Facebook Leads Ads |
| `MessengerFacebookChatController` | 6+ | Messenger Bot |
| `ConfiguracionEnvioAutomaticoController` | 6+ | Auto-envío |
| `FiltroSegmentoController` | 5+ | Segmentación |
| `WhatsAppPlantillaController` | 5+ | Plantillas WhatsApp |
| `EsquemaWhatsAppController` | 8+ | Bot WhatsApp |

#### Finanzas
| Controlador | Endpoints | Descripción |
|------------|-----------|-------------|
| `CajaController` | 8+ | Caja chica |
| `CronogramaPagoController` | 6+ | Cronograma de pagos |
| `ComprobantePagoController` | 5+ | Comprobantes |
| `SiigoController` | 4+ | Contabilidad SIIGO |

#### Configuración / Transversal
| Controlador | Endpoints | Descripción |
|------------|-----------|-------------|
| `AdwordsController` | 8+ | Google Ads |
| `AdwordsConversionController` | 5+ | Conversiones Ads |
| `ConfiguracionIntegraController` | 6+ | Configuración global |
| `PersonalController` | 8+ | Gestión de personal |
| `RolController` | 5+ | Roles y permisos |

### Formato de Respuesta HTTP

**Éxito (200 OK):**
```json
{
  "data": { },
  "status": 200,
  "message": null
}
```

**Error (4xx / 5xx):**
```json
{
  "error": "Descripción del error",
  "codigoError": "CODIGO_ERROR",
  "status": 400,
  "stackTrace": "..."
}
```

---

## 7. Capa de Servicios

### Convención de Nomenclatura

```
I{Nombre}Service  ← Interfaz
{Nombre}Service   ← Implementación
```

Todos los servicios se registran con su interfaz en el contenedor DI de `Program.cs`.

### Servicios por Dominio

#### Comercial (88+ servicios)

| Servicio | Responsabilidad |
|---------|----------------|
| `OportunidadComercialService` | Gestión del pipeline de ventas |
| `MatriculaCabeceraBeneficiosService` | Beneficios en matrículas |
| `AlumnoService` | CRUD y lógica de negocio de alumnos |
| `AgendaTabService` | Tabs de agenda del asesor |
| `AsesorChatService` | Mensajería entre asesores y prospectos |
| `ChatDetalleIntegraArchivoService` | Archivos adjuntos en chat |
| `LlamadaWavixService` | Integración VoIP Wavix |
| `LlamadaWebphoneAsteriskService` | Integración Asterisk PBX |
| `LlamadaWebphoneCruceCentralService` | Enrutamiento de llamadas |
| `FichaAlumnoService` | Perfil detallado del alumno |
| `InteraccionService` | Registro de interacciones |
| `InteraccionPaginaService` | Tracking de páginas visitadas |
| `OcurrenciaService` | Registro de ocurrencias |
| `SemaforoFinancieroService` | Semáforo de estado financiero |
| `SentinelService` | Evaluación crediticia |
| `TableroComercialService` | Métricas comerciales en tiempo real |
| `MandrilService` | Envío de emails transaccionales |
| `MensajeTextoService` | Envío de SMS |
| `AdwordsService` | Gestión de campañas Google Ads |
| `AvatarService` | Gestión de fotos de perfil |
| `HoraBloqueadaService` | Bloqueo de horarios en agenda |
| `TiempoLibreService` | Gestión de tiempo libre de asesores |
| `RecordAreaComercialService` | Récords por área comercial |
| `DocumentoOportunidadService` | Gestión de documentos |
| `GestionArchivoLlamadaService` | Archivos de grabaciones de llamadas |
| `PasarelaPagoPwService` | Pasarela de pago PartnerWeb |
| `ReporteActividadesRealizadasService` | Reporte de actividades |
| `ReporteCambioDeFaseService` | Reporte de cambios de fase |
| `ReporteMensajesWhatsAppService` | Reporte mensajes WhatsApp |
| `ReporteSeguimientoOportunidadService` | Reporte seguimiento |
| `ReporteTasaConversionConsolidadaService` | KPI de conversión |

#### Marketing (70+ servicios)

**Bot de WhatsApp (Sistema NLP):**

| Servicio | Rol en el Bot |
|---------|---------------|
| `EsquemaWhatsAppAsignacionService` | Asigna el esquema correcto por contexto |
| `EsquemaLecturaMensajeService` | Lee y clasifica el mensaje entrante |
| `EsquemaInterpretarInformacionService` | Extrae datos del mensaje (NLP) |
| `EsquemaRespuestaService` | Genera la respuesta apropiada |
| `EsquemaActividadService` | Ejecuta acciones post-respuesta |
| `MensajeExactoService` | Matching de frases exactas |
| `FaseService` | Fases del flujo conversacional |
| `PerfilService` | Perfiles del usuario en el bot |

**Automatización de Marketing:**

| Servicio | Responsabilidad |
|---------|----------------|
| `CampaniaRemarketingGeneralService` | Campañas de retargeting |
| `ConfiguracionEnvioAutomaticoService` | Config. de envíos automáticos |
| `BloqueHorarioService` | Bloqueo de horarios de envío |
| `FiltroBandejaCorreoService` | Filtrado de bandeja de correo |
| `FiltroSegmentoDetalleService` | Segmentación detallada |
| `AsignacionAutomaticaErrorService` | Errores de asignación automática |
| `AsignacionRegularService` | Asignación estándar de prospectos |
| `AlumnoCuponRegistroService` | Registro de cupones de descuento |
| `AsesorChatDetalleService` | Detalle del chat de asesores |
| `AsesorChatMktService` | Chat de marketing |
| `SendingblueService` | Email y SMS via SendingBlue/Brevo |

**Integraciones Facebook:**

| Servicio | Responsabilidad |
|---------|----------------|
| `FacebookAudienciumService` | Gestión de audiencias personalizadas |
| `FacebookAudienciaAlumnoService` | Audiencias basadas en alumnos |
| `FacebookAudienciaCuentaPublicitariumService` | Cuentas publicitarias |
| `FacebookLeadsRecuperacionDatosService` | Recuperación de leads de Facebook |
| `MessengerFacebookChatService` | Bot de Facebook Messenger |

#### Finanzas (30+ servicios)

| Servicio | Responsabilidad |
|---------|----------------|
| `MontoPagoCronogramaService` | Montos en cronograma de pago |
| `MedioPagoMatriculaCronogramaService` | Métodos de pago |
| `MatriculaCabeceraBeneficiosService` | Beneficios financieros |
| `PasarelaPagoPwService` | Integración pasarela de pago |
| `SiigoService` | Integración contable SIIGO |

#### Transversal (Cross-cutting)

| Servicio | Responsabilidad |
|---------|----------------|
| `ConfiguracionIntegraService` | Configuración global del sistema |
| `IValorEstatico` | Valores estáticos compartidos |
| `AdwordsConversionService` | Tracking de conversiones |
| `TokenManager` | Gestión y parsing de JWT tokens |

---

## 8. Capa de Repositorios

### Repositorio Genérico Base

**Archivo:** [BSI.Integra.Repositorio/Repository/GenericRepository.cs](BSI.Integra.Repositorio/Repository/GenericRepository.cs)

```csharp
public class GenericRepository<TEntity> where TEntity : BaseIntegraEntity
{
    // EF Core — Operaciones básicas
    bool Insert(TEntity entidad)
    bool InsertAsync(TEntity entidad)
    bool Update(TEntity entidad)
    bool Delete(TEntity entidad)
    List<TEntity> GetAll()
    TEntity GetById(object id)
    List<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression)

    // Dapper — Queries de alto rendimiento
    protected IDapperRepository _dapperRepository
}
```

**Nota:** Se usa `NoTracking` en EF Core por defecto para mejorar el rendimiento en lecturas.

### Dapper Repository

**Archivo:** [BSI.Integra.Repositorio/Repository/DapperRepository.cs](BSI.Integra.Repositorio/Repository/DapperRepository.cs)

Usado para:
- Queries complejas con múltiples JOINs
- Procedimientos almacenados
- Consultas masivas de alto rendimiento
- Queries específicas de reportes

### Unit of Work

**Archivo:** [BSI.Integra.Repositorio/UnitOfWork/UnitOfWork.cs](BSI.Integra.Repositorio/UnitOfWork/UnitOfWork.cs)

```csharp
public interface IUnitOfWork
{
    bool Commit()           // Guarda cambios (síncrono)
    Task<bool> CommitAsync() // Guarda cambios (asíncrono)
    void Rollback()         // Descarta cambios
}
```

**Dos instancias de UnitOfWork:**
- `IUnitOfWork` → IntegraDB (base de datos principal)
- `IUnitOfWorkInteraccion` → IntegraDB_Interaccion (base de datos de interacciones)

### Estadísticas de Repositorios

| Dominio | Repositorios |
|---------|-------------|
| Comercial | 80+ |
| Marketing | 120+ |
| Finanzas | 60+ |
| Configuración | 40+ |
| Gestión de Personas | 30+ |
| Operaciones | 20+ |
| Planificación | 15+ |
| Otros | 574+ |
| **Total** | **939** |

---

## 9. Entidades y Modelos de Datos

### Bases de Datos

```
IntegraDB (Principal)
├── 878+ tablas mapeadas como entidades EF Core
└── Contexto: IntegraDBContext

IntegraDB_Interaccion (Secundaria)
├── Tablas de tracking de interacciones
└── Contexto: IntegraDBInteraccionContext
```

### Categorías de Entidades

#### Gestión de Actividades (`Actividad*`)
- `ActividadCabecera` — Actividades de seguimiento (cabecera)
- `ActividadDetalle` — Detalles de la actividad
- `ActividadMarcadorLog` — Logs de marcadores de actividad
- `ActividadCrepLog` — Logs de errores de actividad
- `ActividadCabeceraDiaSemana` — Días de la semana para actividades

#### Alumnos y Matrículas (`Alumno*`, `Matricula*`)
- `Alumno` — Alumno/prospecto
- `AlumnoCuponRegistro` — Cupones de descuento
- `AlumnoLog` — Auditoría de alumnos
- `MatriculaCabecera` — Cabecera de matrícula
- `MatriculaCabeceraBeneficios` — Beneficios en matrícula
- `MatriculaFormularioProgresivo` — Formulario progresivo
- `BeneficioAlumnoPEspecifico` — Beneficios específicos
- `CategoriaAlumno` — Categorías de alumnos

#### Programas Académicos (`Programa*`)
- `ProgramaGeneral` — Programa general
- `ProgramaGeneralBeneficio` — Beneficios del programa
- `ProgramaGeneralCertificacion` — Certificaciones
- `ProgramaGeneralModeloCertificado` — Plantillas de certificado
- `ProgramaGeneralMotivacion` — Factores motivacionales
- `ProgramaGeneralPrerequisito` — Prerequisitos
- `ProgramaGeneralProblemaDetalleSolucion` — Mapeo problema-solución
- `ProgramaGeneralMaterialEstudioAdicional` — Materiales de estudio

#### Oportunidades Comerciales (`Oportunidad*`, `Fase*`)
- `OportunidadComercial` — Oportunidades de venta
- `OportunidadCompetidor` — Competidores en la oportunidad
- `DetalleOportunidadCompetidor` — Detalle de competidores
- `OportunidadMaximaPorCategoria` — Límites de oportunidad
- `TransicionFaseOportunidad` — Transiciones de fase
- `DocumentoOportunidad` — Documentos adjuntos
- `ComprobantePagoOportunidad` — Comprobantes de pago

#### Comunicación (`Chat*`, `Mensaje*`, `Interaccion*`)
- `Chat` — Conversaciones de chat
- `ChatDetalle` — Mensajes del chat
- `ChatDetalleIntegraArchivo` — Archivos en el chat
- `ChatIntegraHistorialAsesor` — Historial de asesores
- `Interaccion` — Interacciones con el sistema
- `InteraccionPagina` — Páginas visitadas
- `MensajeTexto` — Mensajes SMS
- `AsesorChat` — Perfil del asesor en chat

#### Agenda y Programación (`Agenda*`, `Cita*`, `Horario*`)
- `Agenda` — Calendario principal
- `AgendaTab` — Tabs del calendario
- `AgendaTipoUsuario` — Tipo de usuario en agenda
- `HoraBloqueada` — Horas bloqueadas
- `HoraReprogramacionAutomatica` — Reprogramación automática
- `Cita` — Citas programadas
- `Cronograma` — Cronograma académico/financiero
- `CronogramaPago` — Cronograma de pagos
- `CronogramaPagoDetalleFinal` — Detalle final de pagos

#### Financiero (`Caja*`, `Pago*`, `Comprobante*`)
- `Caja` — Caja
- `CajaEgreso` — Egresos de caja
- `CajaPorRendir` — Caja por rendir
- `ComprobantePago` — Comprobantes
- `CuentaBancaria` — Cuentas bancarias
- `CuentaContablePadre` — Cuentas contables padre
- `MontoPagoCronograma` — Montos de pago
- `MedioPagoMatriculaCronograma` — Medios de pago
- `Detencion` — Retenciones/Hold
- `CentroCosto` — Centros de costo

#### Estructura Organizacional (`Area*`, `Cargo*`, `Empresa*`)
- `AreaCapacitacion` — Áreas de capacitación
- `AreaFormacion` — Áreas de formación
- `AreaTrabajo` — Áreas de trabajo
- `Cargo` — Cargos/Puestos
- `Empresa` — Empresa/Organización
- `EmpresaAutorizada` — Empresas autorizadas
- `DepartamentoPai` — Departamentos/Provincias
- `Ciudad` — Ciudades
- `Pais` — Países

#### Usuarios y Acceso (`AspNetUser*`, `Personal*`, `Perfil*`, `Rol*`)
- `AspNetUser` — Usuarios Identity (ASP.NET Core)
- `Personal` — Miembros del staff
- `Perfil` — Perfiles de usuario
- `Rol` — Roles del sistema
- `PermisoClave` — Claves de permiso
- `RolPermiso` — Mapeo Rol-Permiso
- `ConfiguracionAccesoPersonal` — Acceso personalizado

#### Marketing y Campañas (`Campania*`, `Anuncio*`, `Facebook*`)
- `CampaniaGeneral` — Campañas generales
- `CampaniaGeneralDetalle` — Detalle de campañas
- `CampaniaGeneralDetallePrograma` — Programas en campaña
- `CampaniaGeneralDetalleSubArea` — Sub-áreas en campaña
- `CampaniaGeneralSms` — Campañas SMS
- `CampaniaGeneralWhatsApp` — Campañas WhatsApp
- `CampaniaRemarketingGeneral` — Campañas de retargeting
- `AnuncioFacebook` — Anuncios de Facebook
- `AnuncioFacebookMetrica` — Métricas de anuncios
- `ConjuntoAnuncio` — Ad sets
- `ConjuntoLista` — Listas de audiencia
- `FiltroSegmento` — Segmentos de filtrado
- `FiltroSegmentoDetalle` — Detalle de segmentos
- `FacebookAudiencia` — Audiencias de Facebook

#### Logs y Auditoría (`Log*`)
- `ActividadMarcadorLog` — Logs de marcadores
- `ActividadCrepLog` — Logs de errores
- `AlumnoLog` — Logs de alumnos
- `CalidadLlamadaLog` — Calidad de llamadas
- `ChatIntegraHistorialAsesor` — Historial de chat

#### Integraciones Externas
- `AdwordsApiPalabraClave` — Keywords de Google Ads
- `AdwordsApiVolumenBusquedium` — Volumen de búsquedas
- `AdworkCredencialApi` — Credenciales de Google Ads
- `SentinelSdt*` — Datos de crédito Sentinel (10+ tablas)
- `AnuncioFacebookMetrica` — Métricas de Facebook

---

## 10. Data Transfer Objects (DTOs)

### Ubicación
[BSI.Integra.Aplicacion.DTO/SCode/Modelos/](BSI.Integra.Aplicacion.DTO/SCode/Modelos/)

### DTOs Comunes

| DTO | Uso |
|----|-----|
| `ComboDTO` | Listas desplegables (id + descripción) |
| `ErrorGenericoDTO` | Respuestas de error estandarizadas |
| `GridFiltersDTO` | Paginación y filtrado de grids |

### DTOs por Dominio

**Comercial:**
- `ActividadCabeceraDTO`, `ActividadDetalleDTO`, `ActividadMarcadorLogDTO`
- `AlumnoDTO`, `AlumnoCuponRegistroDTO`, `AlumnoLogDTO`
- `MatriculaCabeceraDTO`, `OportunidadComercialDTO`
- `ChatDTO`, `ChatDetalleDTO`
- `AgendaDTO`, `AgendaFiltroDTO`
- `CertificadoSolicitudDTO`

**Marketing:**
- `CampaniaGeneralDTO`, `CampaniaMailingWhatsappDTO`
- `FacebookLeadsDTO`, `MessengerDTO`
- `ConfiguracionEnvioAutomaticoDTO`
- `FiltroSegmentoDTO`
- `EsquemaWhatsAppDTO` (familia de DTOs del bot)

**Finanzas:**
- `CajaDTO`, `CajaEgresoDTO`
- `CronogramaPagoDTO`, `ComprobantePagoDTO`
- `SiigoDTO`

**Google Ads:**
- `GoogleAdsDTO`, `AdwordsDTO`, `AdwordsConversionDTO`

**Otras Integraciones:**
- `LinkedInDTO`, `MelissaDTO`
- `TranscriptionDTO`

---

## 11. Integraciones Externas

### Mapa de Integraciones

```
                   ┌──────────────────────┐
                   │   BSI.Integra API    │
                   └──────────┬───────────┘
                              │
       ┌──────────────────────┼──────────────────────┐
       │                      │                      │
┌──────▼──────┐    ┌──────────▼────────┐   ┌─────────▼──────┐
│ Google Ads  │    │  Facebook (Meta)   │   │  Wavix (VoIP)  │
│ API v12+    │    │  Graph API v16+    │   │  API v1 & v2   │
└─────────────┘    └───────────────────┘   └────────────────┘
       │                      │                      │
┌──────▼──────┐    ┌──────────▼────────┐   ┌─────────▼──────┐
│SendingBlue  │    │  Asterisk PBX     │   │  Azure Cloud   │
│  Email/SMS  │    │  SIP/WebRTC       │   │  Blob + KeyV.  │
└─────────────┘    └───────────────────┘   └────────────────┘
       │                      │                      │
┌──────▼──────┐    ┌──────────▼────────┐   ┌─────────▼──────┐
│   SIIGO     │    │    Sentinel        │   │    Mandril     │
│ Contabilidad│    │  Credit Scoring    │   │ Email Transac. │
└─────────────┘    └───────────────────┘   └────────────────┘
       │                      │
┌──────▼──────┐    ┌──────────▼────────┐
│  LinkedIn   │    │     Melissa        │
│  Social     │    │  Data Quality      │
└─────────────┘    └───────────────────┘
```

### Detalle de Integraciones

#### 1. Google Ads API
- **SDK:** `Google.Ads.GoogleAds`
- **Autenticación:** OAuth2 (APPLICATION mode)
- **Customer ID:** 6799915323
- **Servicios:** `AdwordsService`, `AdwordsConversionService`
- **Funcionalidades:** Gestión de campañas, keywords, conversiones
- **Configuración en:** `appsettings.json` → sección `GoogleAds`

#### 2. Facebook (Meta) — Ads & Pixel
- **API:** Meta Graph API
- **Pixel IDs configurados:**
  - Principal: `269257245868695`
  - Construcción Fase Máxima: `1852389438820795`
- **Funcionalidades:**
  - Lead Ads (captura de prospectos)
  - Pixel tracking (comportamiento web)
  - Custom Audiences (listas de remarketing)
  - Messenger Bot
- **Servicios:**
  - `FacebookAudienciumService`
  - `FacebookLeadsRecuperacionDatosService`
  - `MessengerFacebookChatService`

#### 3. Wavix (VoIP / WebRTC)
- **Documentación interna:** [WAVIX_API_DOCUMENTATION.md](WAVIX_API_DOCUMENTATION.md)
- **APIs:** Wavix API v1 y v2
- **Funcionalidades:**
  - SIP Trunks (gestión de líneas SIP)
  - Generación de tokens WebRTC (TTL: 12 horas)
  - Números por país/usuario
  - Estado de llamadas en tiempo real
  - Configuración completa por asesor
- **Endpoints expuestos:**
  - `ObtenerConfiguracionCompletaWavix/{idPersonal}`
  - `GetUserAccess/{idPersonal}`
  - `ObtenerNúmerosPorUsuario`
  - `ListarSipTrunks`
  - `GenerarTokenWidget`
  - `ObtenerEstadoLlamada`
- **Almacenamiento:** Tokens guardados en BD (`WavixTokenDiario`)

#### 4. SendingBlue / Brevo
- **SDK:** `sib-api-v3-sdk`
- **Registro DI:** Singleton (instancia compartida)
- **Funcionalidades:** Campañas de email, SMS masivo, gestión de contactos

#### 5. Asterisk (PBX)
- **Integración:** Via HTTP/SIP
- **Servicio:** `LlamadaWebphoneAsteriskService`
- **Funcionalidades:** Enrutamiento de llamadas, central telefónica

#### 6. 3CX (Comunicaciones Unificadas)
- **Ambiente:** `https://integrav5-3cx.bsginstitute.com`
- **Rol:** Sistema telefónico alternativo/adicional

#### 7. Azure Cloud
- **Blob Storage:** `Azure.Storage.Blobs` — Almacenamiento de archivos
- **Key Vault:** `Microsoft.Azure.KeyVault.WebKey` — Gestión de secretos
- **Data Movement:** Azure data movement utilities
- **SDK Legado:** `WindowsAzure.Storage`
- **Uso:** Archivos de chat, grabaciones, documentos, avatares

#### 8. SIIGO (Contabilidad)
- **Integración:** API REST de SIIGO
- **Archivos:** `SiigoDTO`, `SiigoRepository`
- **Funcionalidades:** Sincronización de datos contables, facturas

#### 9. LinkedIn
- **Integración:** LinkedIn Marketing API
- **Funcionalidades:** Marketing en redes sociales

#### 10. Melissa Data
- **Servicios:** `MelissaService`, `MelissaRepository`
- **Funcionalidades:** Verificación de email, validación de direcciones

#### 11. Sentinel (Credit Scoring)
- **Servicios:** Familia `SentinelSdt*`
- **Entidades:** 10+ tablas de datos crediticios
- **Funcionalidades:** Evaluación de riesgo crediticio, benchmarking salarial

#### 12. Mandril (Mailchimp Transactional)
- **Servicio:** `MandrilService`
- **Funcionalidades:** Emails transaccionales (confirmaciones, notificaciones)

---

## 12. Autenticación y Autorización

### Sistema de Autenticación JWT

**Archivo:** [BSI.Integra.Servicios/Helpers/TokenManager.cs](BSI.Integra.Servicios/Helpers/TokenManager.cs)

#### Configuración JWT

```json
{
  "TokenKey": "1234567890123456",
  "JwtSettings": {
    "Algorithm": "HS256",
    "ValidateIssuer": false,
    "ValidateAudience": false,
    "ValidateLifetime": true
  }
}
```

#### Claims del Token

| Claim | Descripción |
|-------|-------------|
| `IdPersonal` | ID del staff/usuario |
| `IdRol` | ID del rol asignado |
| `AreaTrabajo` | Área de trabajo del usuario |
| `UserName` | Nombre de usuario |
| `UserAsp` | Identidad ASP.NET |
| `Expira` | Fecha de expiración del token |
| `TipoPersonal` | Clasificación del tipo de staff |

#### Decoradores de Autorización

```csharp
[Authorize]                           // Requiere token válido
[AllowAnonymous]                      // Endpoint público
[Authorize(Roles = "Admin,Manager")]  // Rol específico requerido
```

### CORS Policy

**Política:** `CorsVista`

**Orígenes permitidos (27 en total):**

| Ambiente | URL |
|----------|-----|
| Desarrollo | `http://localhost:4200` |
| Desarrollo HTTPS | `https://localhost:7288` |
| Producción principal | `https://integrav5.bsginstitute.com` |
| Producción beta | `https://integrav5-beta.bsginstitute.com` |
| 3CX | `https://integrav5-3cx.bsginstitute.com` |
| Otros ambientes | `...` (stagings, testing, etc.) |

---

## 13. Configuración e Infraestructura

### appsettings.json — Secciones Principales

```json
{
  "TokenKey": "...",
  "ConnectionStrings": {
    "IntegraDB": "Server=...;Database=IntegraDB;...",
    "IntegraDBInteraccion": "Server=...;Database=IntegraDB_Interaccion;..."
  },
  "GoogleAds": {
    "DeveloperToken": "...",
    "OAuth2Mode": "APPLICATION",
    "ClientId": "...",
    "ClientSecret": "...",
    "LoginCustomerId": "6799915323"
  },
  "FacebookAds": {
    "AccessToken": "...",
    "PixelId": "269257245868695"
  },
  "FacebookAdsFaseMaximaConstruccion": {
    "AccessToken": "...",
    "PixelId": "1852389438820795"
  },
  "Hangfire": {
    "Dashboard": "/hangfire"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Hangfire — Background Jobs

- **Storage:** SQL Server (tabla de jobs en IntegraDB)
- **Dashboard:** `/hangfire` (panel de administración)
- **Auto-start:** Servidor de jobs inicia automáticamente
- **Uso típico:**
  - Envío masivo de WhatsApp/Email en background
  - Sincronización con Facebook Audiences
  - Reportes pesados
  - Tareas de mantenimiento programadas

### Límites de Request

```csharp
// Program.cs
options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
FormOptions.ValueCountLimit = 8192;
```

### Bases de Datos

| Base de Datos | Propósito | Contexto EF |
|---------------|-----------|-------------|
| `IntegraDB` | Base principal (878+ tablas) | `IntegraDBContext` |
| `IntegraDB_Interaccion` | Tracking de interacciones | `IntegraDBInteraccionContext` |

---

## 14. Middleware y Pipeline

### Pipeline de Requests

```
HTTP Request
     │
     ▼
[Swagger UI]           ← /swagger (solo desarrollo)
     │
     ▼
[HTTPS Redirect]       ← HTTP → HTTPS automático
     │
     ▼
[CORS Middleware]      ← Política "CorsVista" (27 orígenes)
     │
     ▼
[Global Exception]     ← Captura todas las excepciones
  Middleware           ← Inicializa configuración estática
     │
     ▼
[JWT Authentication]   ← Valida Bearer token
     │
     ▼
[Authorization]        ← Verifica roles/claims
     │
     ▼
[Route Matching]       ← /api/[controller]/[action]
     │
     ▼
[Controller Action]
     │
     ▼
HTTP Response
```

### GlobalExceptionHandlingMiddleware

**Archivo:** [BSI.Integra.Servicios/Configurations/GlobalExceptionHandlingMiddleware.cs](BSI.Integra.Servicios/Configurations/GlobalExceptionHandlingMiddleware.cs)

| Excepción | HTTP Status |
|-----------|-------------|
| `BadRequestException` | 400 Bad Request |
| `NotFoundException` | 404 Not Found |
| `NotImplementedException` | 501 Not Implemented |
| `KeyNotFoundException` | 400 Bad Request |
| `UnauthorizedAccessRequestException` | 401 Unauthorized |
| `ConflictException` | 409 Conflict |
| Cualquier otra excepción | 400 Bad Request |

**Respuesta de error estandarizada:**
```json
{
  "error": "Mensaje de error legible",
  "codigoError": "EXCEPTION_TYPE",
  "status": 400,
  "stackTrace": "..."
}
```

---

## 15. Patrones de Diseño Implementados

### 1. Repository Pattern
Abstrae el acceso a datos detrás de interfaces. 939 repositorios específicos heredan de `GenericRepository<T>`.

### 2. Unit of Work Pattern
Gestiona transacciones y agrupa múltiples operaciones de repositorios en una unidad atómica.

```
Controller → UnitOfWork.Begin()
           → Repository.Insert()
           → Repository.Update()
           → UnitOfWork.Commit() / Rollback()
```

### 3. Service Layer Pattern
Toda la lógica de negocio está en servicios, nunca en controladores. Los servicios se organizan por dominio (Comercial, Marketing, Finanzas, etc.).

### 4. DTO Pattern
Los controladores reciben y retornan DTOs (nunca entidades). Las entidades solo circulan dentro de repositorios y servicios.

### 5. Dependency Injection (IoC)
Todo el sistema usa el contenedor DI nativo de ASP.NET Core. Lifetime:
- **Scoped:** La mayoría de servicios y repositorios
- **Transient:** Servicios sin estado
- **Singleton:** `SendingblueService` (instancia compartida)

### 6. Factory Pattern
`IConnectionFactory` abstrae la creación de conexiones SQL para Dapper.

### 7. Middleware Pattern
Pipeline configurable para cross-cutting concerns (auth, CORS, excepciones).

### 8. Strategy Pattern (Bot WhatsApp)
El sistema de bot usa diferentes estrategias de respuesta según la fase conversacional y el perfil del usuario.

### 9. Observer Pattern (Hangfire)
Los background jobs observan y reaccionan a eventos del sistema de forma asíncrona.

---

## 16. Métricas del Proyecto

### Estadísticas Generales

| Métrica | Cantidad |
|---------|----------|
| Controladores | **566** |
| Entidades de base de datos | **878+** |
| Implementaciones de Servicio | **280+** |
| Interfaces de Servicio | **280+** |
| Repositorios | **939** |
| DTOs | **500+** |
| Tablas en BD | **878+** |
| Integraciones externas | **12+** |
| Dominios de negocio | **9** |
| Proyectos en la solución | **13** |
| Dependencias NuGet | **22+** |
| Endpoints API (estimado) | **3,000+** |
| Orígenes CORS permitidos | **27** |

### Distribución por Dominio

```
Servicios por Dominio:
Comercial      ████████████████████████████████  88 servicios
Marketing      ████████████████████████          70 servicios
Finanzas       ████████████                      30 servicios
RRHH           ████████                          25 servicios
Operaciones    ██████                            20 servicios
Planificación  ████                              15 servicios
Transversal    ███                               10 servicios
Interacción    ██                                8 servicios
Servicios      █                                 5 servicios
```

---

## 17. Observaciones y Recomendaciones

### Fortalezas

1. **Arquitectura empresarial sólida** — Separación clara de capas y dominios
2. **Integración multicanal completa** — 12+ integraciones con servicios externos
3. **Diseño escalable** — Patrón repositorio/servicio facilita el crecimiento
4. **Plataforma completa** — CRM + Marketing + RRHH + Finanzas en un solo sistema
5. **Stack moderno** — .NET 6, async/await, EF Core, Azure
6. **Seguridad robusta** — JWT, RBAC, CORS configurado
7. **Transaccionalidad** — Unit of Work garantiza consistencia de datos
8. **Rendimiento** — Combinación de EF Core (NoTracking) + Dapper para queries pesadas
9. **Observabilidad** — Hangfire dashboard, logs integrados

### Áreas de Mejora Potencial

1. **566 controladores** — Considerar API versioning (`/api/v1/`, `/api/v2/`) para organización
2. **TokenKey en appsettings** — Mover a Azure Key Vault para mayor seguridad
3. **CORS hardcodeado** — Considerar configuración dinámica de orígenes por ambiente
4. **Mezcla de sync/async** — Incrementar cobertura de métodos async en toda la cadena
5. **Sin versionado de API** — Agregar versionado podría facilitar evolución del contrato API
6. **27 orígenes CORS** — Revisar periódicamente si todos siguen siendo necesarios
7. **Background jobs sin retry explicito** — Considerar políticas de reintentos en Hangfire

---

## 18. Rutas de Referencia

### Archivos Clave

| Archivo | Descripción |
|---------|-------------|
| [BSI.Integra.Servicios/Program.cs](BSI.Integra.Servicios/Program.cs) | Entry point, DI, pipeline (257 líneas) |
| [BSI.Integra.Servicios/appsettings.json](BSI.Integra.Servicios/appsettings.json) | Configuración de la aplicación |
| [BSI.Integra.Servicios/Configurations/GlobalExceptionHandlingMiddleware.cs](BSI.Integra.Servicios/Configurations/GlobalExceptionHandlingMiddleware.cs) | Middleware de excepciones |
| [BSI.Integra.Servicios/Helpers/TokenManager.cs](BSI.Integra.Servicios/Helpers/TokenManager.cs) | Gestión de JWT |
| [BSI.Integra.Repositorio/Repository/GenericRepository.cs](BSI.Integra.Repositorio/Repository/GenericRepository.cs) | Repositorio base |
| [BSI.Integra.Repositorio/Repository/DapperRepository.cs](BSI.Integra.Repositorio/Repository/DapperRepository.cs) | Repositorio Dapper |
| [BSI.Integra.Repositorio/UnitOfWork/UnitOfWork.cs](BSI.Integra.Repositorio/UnitOfWork/UnitOfWork.cs) | Unit of Work |
| [WAVIX_API_DOCUMENTATION.md](WAVIX_API_DOCUMENTATION.md) | Documentación API Wavix |

### Directorios Clave

| Directorio | Contenido |
|-----------|-----------|
| [BSI.Integra.Servicios/Controllers/](BSI.Integra.Servicios/Controllers/) | 566 controladores |
| [BSI.Integra.Persistencia/Entidades/IntegraDB/](BSI.Integra.Persistencia/Entidades/IntegraDB/) | 878+ entidades EF Core |
| [BSI.Integra.Repositorio/Repository/](BSI.Integra.Repositorio/Repository/) | 939 repositorios |
| [BSI.Integra.Aplicacion.DTO/SCode/Modelos/](BSI.Integra.Aplicacion.DTO/SCode/Modelos/) | 500+ DTOs |
| [BSI.Integra.Aplicacion.Comercial/SCode/Service/](BSI.Integra.Aplicacion.Comercial/SCode/Service/) | Servicios comerciales |
| [BSI.Integra.Aplicacion.Marketing/SCode/Service/](BSI.Integra.Aplicacion.Marketing/SCode/Service/) | Servicios de marketing |

---

*Documento generado automáticamente el 19 de febrero de 2026 mediante análisis estático del proyecto BSI.Integra.Servicios V5.*
