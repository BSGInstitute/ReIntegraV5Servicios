using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.IntegraDBMongo.Config;
using BSI.Integra.Persistencia.IntegraDBMongo.Context;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.IntegraDBMongo.Implementacion;
using BSI.Integra.Repositorio.IntegraDBMongo.Interface;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Comercial;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsVista",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:51260", "https://localhost:7288", "https://integrav5.bsginstitute.com", "http://integrav5.bsginstitute.com", "https://integrav5-servicios.bsginstitute.com", "https://integrav5p.bsginstitute.com", "https://integrav4.bsginstitute.com", "https://integrav4-prepublicacion-interfaz.bsginstitute.com", "https://integrav5mejora.bsginstitute.com", "https://integrav5-mejora-servicios.bsginstitute.com", "https://integrav5prepublicacion.bsginstitute.com", "https://integrav5-prepublicacion-servicios.bsginstitute.com", "https://integrav5publicacion.bsginstitute.com", "https://integrav5-publicacion-servicios.bsginstitute.com", "https://integrav5pruebainterfaz.bsginstitute.com", "https://integrav5-3cx.bsginstitute.com", "https://webhook-facebook.bsginstitute.com", "http://ia-proceso-resumen-sesiones-api.bsginstitute.com", "https://integrav5-prepublicacion-eariasf.bsginstitute.com", "https://hook-whatsapp.bsginstitute.com", "https://nuevointegrav5.bsginstitute.com", "https://beta8.moontechnolabs.com", "https://prototipo.bsginstitute.com", "https://bsginstitute.com", "https://integrav5prepublicacionmarcador.bsginstitute.com", "https://integrav5-nuevaagenda.bsginstitute.com", "https://integrav5-nuevaagendaatc.bsginstitute.com", "https://integrav5-nuevaagendaia.bsginstitute.com", "http://ia-automatizacion-planificacion-api.bsginstitute.com")
                .AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueCountLimit = 8192;
    x.MultipartBodyLengthLimit = 524288000; //500MB
}
);
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = long.MaxValue); // Allows for even larger files

//Add services to the container.
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<JwtActionFilter>();
//});

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

var tokenKey = builder.Configuration.GetValue<string>("TokenKey");

var key = Encoding.ASCII.GetBytes(tokenKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        LifetimeValidator = LifetimeValidator,
        TokenDecryptionKey = new SymmetricSecurityKey(key),
    };
});
static bool LifetimeValidator(DateTime? notBefore,
    DateTime? expires,
    SecurityToken securityToken,
    TokenValidationParameters validationParameters) => expires != null && expires > DateTime.Now;


//Add Contexts
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>(cn => new ConnectionFactory(builder.Configuration.GetConnectionString("IntegraDB")));
builder.Services.AddDbContext<IntegraDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IntegraDB")));
builder.Services.AddScoped<IConnectionFactoryInteraccion, ConnectionFactoryInteraccion>(cn => new ConnectionFactoryInteraccion(builder.Configuration.GetConnectionString("IntegraDB_Interaccion")));
builder.Services.AddDbContext<IntegraDBInteraccionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IntegraDB_Interaccion")));

builder.Services.AddTransient<IDapperRepository, DapperRepository>();
builder.Services.AddTransient<IDapperRepositoryInteraccion, DapperRepositoryInteraccion>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkInteraccion, UnitOfWorkInteraccion>();
builder.Services.AddScoped<ILlamadaWavixService, LlamadaWavixService>();

builder.Services.AddScoped<IValorEstatico, ValorEstatico>();
builder.Services.AddScoped<IConfiguracionAccesoPersonalService, ConfiguracionAccesoPersonalService>();
builder.Services.AddScoped<IConfiguracionIntegraService, ConfiguracionIntegraService>();

builder.Services.AddSingleton<ISendingblueService, SendingblueService>();
builder.Services.AddTransient<IAvatarService, AvatarService>();
builder.Services.AddTransient<IMaterialAccionService, MaterialAccionService>();
builder.Services.AddTransient<IMaterialCriterioVerificacionService, MaterialCriterioVerificacionService>();
builder.Services.AddTransient<ICargoService, CargoService>();
builder.Services.AddTransient<IAreaCcService, AreaCcService>();
builder.Services.AddTransient<ICriterioEvaluacionCategoriumService, CriterioEvaluacionCategoriumService>();
builder.Services.AddTransient<IAreaParametroSeoPwService, AreaParametroSeoPwService>();
builder.Services.AddTransient<ISubAreaCapacitacionService, SubAreaCapacitacionService>();
builder.Services.AddTransient<ISubNivelCcService, SubNivelCcService>();
builder.Services.AddTransient<IFeedbackTipoService, FeedbackTipoService>();
builder.Services.AddTransient<IMaterialTipoService, MaterialTipoService>();
builder.Services.AddTransient<IAsociarTagProgramaService, AsociarTagProgramaService>();
builder.Services.AddTransient<IMaterialVersionService, MaterialVersionService>();
builder.Services.AddTransient<ICrucigramaProgramaCapacitacionService, CrucigramaProgramaCapacitacionService>();
builder.Services.AddTransient<IProgramaGeneralMaterialEstudioAdicionalService, ProgramaGeneralMaterialEstudioAdicionalService>();
builder.Services.AddTransient<ICourierService, CourierService>();
builder.Services.AddTransient<ICourierDetalleService, CourierDetalleService>();
builder.Services.AddTransient<IGestionContactoService, GestionContactoService>();
builder.Services.AddTransient<IDocentePostulanteService, DocentePostulanteService>();
builder.Services.AddTransient<IFaseGestionContactoService, FaseGestionContactoService>();
builder.Services.AddTransient<IMatriculaFormularioProgresivoService, MatriculaFormularioProgresivoService>();
builder.Services.AddTransient<IGestionDocenteActividadService, GestionDocenteActividadService>();
builder.Services.AddTransient<IGestionDocenteFlujoService, GestionDocenteFlujoService>();
builder.Services.AddTransient<IGestionDocenteAgendaService, GestionDocenteAgendaService>();

builder.Services.AddTransient<ITransicionFaseOportunidadService, TransicionFaseOportunidadService>();
builder.Services.AddTransient<ITransicionFaseOportunidadRepository, TransicionFaseOportunidadRepository>();

builder.Services.AddTransient<IValidacionRn2Service, ValidacionRn2Service>();



builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IMessengerFacebookChatService, MessengerFacebookChatService>();
builder.Services.AddScoped<IMessengerFacebookChatRepository, MessengerFacebookChatRepository>();
builder.Services.AddScoped<ICampaniaRemarketingGeneralService, CampaniaRemarketingGeneralService>();
builder.Services.AddScoped<ICampaniaRemarketingGeneralRepository, CampaniaRemarketingGeneralRepository>();
builder.Services.AddScoped<ICategoriaArgumentosService, CategoriaArgumentosService>();
builder.Services.AddScoped<ICategoriaArgumentosRepository, CategoriaArgumentosRepository>();
builder.Services.AddScoped<IEsquemaRespuestasService, EsquemaRespuestasService>();
builder.Services.AddScoped<IEsquemaRespuestasRepository, EsquemaRespuestasRepository>();
builder.Services.AddScoped<IConfiguracionExternaRepository, ConfiguracionExternaRepository>();
builder.Services.AddScoped<IConfiguracionExternaService, ConfiguracionExternaService>();
builder.Services.AddScoped<IChatbotActividadBotIARepository, ChatbotActividadBotIARepository>();
builder.Services.AddScoped<IChatbotActividadBotIAService, ChatbotActividadBotIAService>();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

builder.Services.AddHttpContextAccessor();


builder.Services.AddHttpClient();
builder.Services.AddScoped<IFacebookLeadsRecuperacionDatosService, FacebookLeadsRecuperacionDatosService>();

// ============= INYECCIÓN DE DEPENDENCIAS - SISTEMA DE ESQUEMAS BOT IA WHATSAPP =============
// Repositories
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IMensajeExactoRepository, BSI.Integra.Repositorio.Repository.Implementation.MensajeExactoRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IFaseRepository, BSI.Integra.Repositorio.Repository.Implementation.FaseRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IPerfilRepository, BSI.Integra.Repositorio.Repository.Implementation.PerfilRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IEsquemaWhatsAppAsignacionRepository, BSI.Integra.Repositorio.Repository.Implementation.EsquemaWhatsAppAsignacionRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IEsquemaLecturaMensajeRepository, BSI.Integra.Repositorio.Repository.Implementation.EsquemaLecturaMensajeRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IEsquemaInterpretarInformacionRepository, BSI.Integra.Repositorio.Repository.Implementation.EsquemaInterpretarInformacionRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IEsquemaRespuestaRepository, BSI.Integra.Repositorio.Repository.Implementation.EsquemaRespuestaRepository>();
builder.Services.AddTransient<BSI.Integra.Repositorio.Repository.Interface.IEsquemaActividadRepository, BSI.Integra.Repositorio.Repository.Implementation.EsquemaActividadRepository>();

// Services
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IMensajeExactoService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.MensajeExactoService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IFaseService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.FaseService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IPerfilService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.PerfilService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IEsquemaWhatsAppAsignacionService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.EsquemaWhatsAppAsignacionService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IEsquemaLecturaMensajeService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.EsquemaLecturaMensajeService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IEsquemaInterpretarInformacionService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.EsquemaInterpretarInformacionService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IEsquemaRespuestaService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.EsquemaRespuestaService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.IEsquemaActividadService, BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.EsquemaActividadService>();
// ===========================================================================================


builder.Services.AddTransient<
    BSI.Integra.Repositorio.Repository.Interface.IMatriculaConfiguracionComunicacionAsesorRepository,
    BSI.Integra.Repositorio.Repository.Implementation.Planificacion.MatriculaConfiguracionComunicacionAsesorRepository
>();

// Service
builder.Services.AddTransient<
    BSI.Integra.Aplicacion.Planificacion.Service.Interface.IMatriculaConfiguracionComunicacionAsesorService,
    BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.MatriculaConfiguracionComunicacionAsesorService
>();

// TipoDescuentoSolicitud Service
builder.Services.AddScoped<
    BSI.Integra.Aplicacion.Planificacion.Service.Interface.ITipoDescuentoSolicitudService,
    BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.TipoDescuentoSolicitudService
>();

// Google Ads Conversion Service
builder.Services.AddScoped<BSI.Integra.Repositorio.Repository.Interface.IAdwordsConversionRepository, BSI.Integra.Repositorio.Repository.Implementation.AdwordsConversionRepository>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Transversal.Service.Interface.IAdwordsConversionService, BSI.Integra.Aplicacion.Transversal.Service.Implementacion.AdwordsConversionService>();

// WhatsApp Service
builder.Services.AddScoped<BSI.Integra.Aplicacion.Transversal.Service.Interface.IWhatsAppMensajeEnviadoApiPlanificacionService, BSI.Integra.Aplicacion.Transversal.Service.Implementacion.WhatsAppMensajeEnviadoApiPlanificacionService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Transversal.Service.Interface.IWhatsAppMensajeEnviadoApiAtcService, BSI.Integra.Aplicacion.Transversal.Service.Implementacion.WhatsAppMensajeEnviadoApiAtcService>();

// Servicio unificado de envio de actividades automaticas (Email + WhatsApp)
builder.Services.AddScoped<BSI.Integra.Servicios.Services.IActividadEnvioService, BSI.Integra.Servicios.Services.ActividadEnvioService>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Marketing.Service.Interface.IGmailCorreoService, BSI.Integra.Aplicacion.Marketing.Service.Implementacion.GmailCorreoService>();

// Clasificacion de respuestas docentes via servicio externo Python
builder.Services.AddScoped<BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface.IClasificacionRespuestaService, BSI.Integra.Servicios.Services.ClasificacionRespuestaService>();
builder.Services.AddScoped<BSI.Integra.Servicios.Jobs.ClasificacionRespuestaJob>();
builder.Services.AddHttpClient("PythonLlm", client =>
{
    client.BaseAddress = new Uri("http://ia-automatizacion-planificacion-api.bsginstitute.com");
    client.Timeout     = TimeSpan.FromSeconds(30);
});



//var connectionString = builder.Configuration.GetConnectionString("IntegraDB");

// Registrar Hangfire
//builder.Services.AddHangfire(config =>
//    config.UseSqlServerStorage(connectionString));
//builder.Services.AddHangfireServer();

/// Conexion Base de Datos MongoDB 
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Registro de contexto MongoDB (Singleton para reutilizar conexión)
builder.Services.AddSingleton<MongoDBContext>();

// Registro del repositorio (Scoped para cada request)
builder.Services.AddScoped<IMongoDocumentRepository, MongoDocumentRepository>();

// Registrar el Job de Actividades Congeladas
builder.Services.AddScoped<BSI.Integra.Servicios.Jobs.ActividadesCongeladasJob>();

var app = builder.Build();

// Dashboard Hangfire
//app.UseHangfireDashboard("/hangfire");

// Configurar Job Recurrente: Procesar actividades congeladas cada 5 minutos
//Hangfire.RecurringJob.AddOrUpdate<BSI.Integra.Servicios.Jobs.ActividadesCongeladasJob>(
//    "procesar-actividades-congeladas",
//    job => job.ProcesarActividadesPendientesAsync(),
//    "*/5 * * * *");

//// Clasificacion de respuestas docentes — cada 1 minuto
//Hangfire.RecurringJob.AddOrUpdate<BSI.Integra.Servicios.Jobs.ClasificacionRespuestaJob>(
//    "clasificacion-respuestas-docentes",
//    job => job.ProcesarClasificacionesAsync(),
//    "*/2 * * * *");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.AddGlobalErrorHandler();

app.UseAuthentication();

app.UseAuthorization();

//app.AddGlobalJwtHandler();

app.MapControllers();

app.Run();