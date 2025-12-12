using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using BSI.Integra.Servicios.Hubs;
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
            builder.WithOrigins("http://localhost:4200", "http://localhost:51260", "https://localhost:7288", "https://integrav5.bsginstitute.com", "https://integrav5-servicios.bsginstitute.com", "https://integrav5p.bsginstitute.com", "https://integrav4.bsginstitute.com", "https://integrav4-prepublicacion-interfaz.bsginstitute.com", "https://integrav5mejora.bsginstitute.com", "https://integrav5-mejora-servicios.bsginstitute.com", "https://integrav5prepublicacion.bsginstitute.com", "https://integrav5-prepublicacion-servicios.bsginstitute.com", "https://integrav5publicacion.bsginstitute.com", "https://integrav5-publicacion-servicios.bsginstitute.com", "https://integrav5pruebainterfaz.bsginstitute.com", "https://integrav5-3cx.bsginstitute.com", "https://webhook-facebook.bsginstitute.com", "http://ia-proceso-resumen-sesiones-api.bsginstitute.com", "https://integrav5-prepublicacion-eariasf.bsginstitute.com", "https://hook-whatsapp.bsginstitute.com", "https://nuevointegrav5.bsginstitute.com", "https://beta8.moontechnolabs.com", "https://prototipo.bsginstitute.com", "https://bsginstitute.com", "https://integrav5prepublicacionmarcador.bsginstitute.com", "https://integrav5-nuevaagenda.bsginstitute.com")
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
builder.Services.AddTransient<IMatriculaFormularioProgresivoService, MatriculaFormularioProgresivoService>();

builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IMessengerFacebookChatService, MessengerFacebookChatService>();
builder.Services.AddScoped<IMessengerFacebookChatRepository, MessengerFacebookChatRepository>();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


builder.Services.AddHttpClient(); // Si no lo tienes ya
builder.Services.AddScoped<IFacebookLeadsRecuperacionDatosService, FacebookLeadsRecuperacionDatosService>();


builder.Services.AddTransient<
    BSI.Integra.Repositorio.Repository.Interface.IMatriculaConfiguracionComunicacionAsesorRepository,
    BSI.Integra.Repositorio.Repository.Implementation.Planificacion.MatriculaConfiguracionComunicacionAsesorRepository
>();

// Service
builder.Services.AddTransient<
    BSI.Integra.Aplicacion.Planificacion.Service.Interface.IMatriculaConfiguracionComunicacionAsesorService,
    BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.MatriculaConfiguracionComunicacionAsesorService
>();



// Google Ads Conversion Service
builder.Services.AddScoped<BSI.Integra.Repositorio.Repository.Interface.IAdwordsConversionRepository, BSI.Integra.Repositorio.Repository.Implementation.AdwordsConversionRepository>();
builder.Services.AddScoped<BSI.Integra.Aplicacion.Transversal.Service.Interface.IAdwordsConversionService, BSI.Integra.Aplicacion.Transversal.Service.Implementacion.AdwordsConversionService>();


var connectionString = builder.Configuration.GetConnectionString("IntegraDB");
// Registrar Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(connectionString));
builder.Services.AddHangfireServer();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsSignalR", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();


// Dashboard opcional
app.UseHangfireDashboard("/hangfire");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors();
app.UseCors("CorsSignalR");


app.AddGlobalErrorHandler();

app.UseAuthentication();

app.UseAuthorization();

//app.AddGlobalJwtHandler();

app.MapControllers();
app.MapHub<WebinarHub>("/webinarHub");

app.Run();
