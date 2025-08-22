using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: CategoriaPreguntaController
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 16/04/2024
    /// <summary>
    /// Criterio Evaluacion Proceso
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class ConfiguracionAsignacionExamenController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IConfiguracionAsignacionExamenService _examenAsignadoEvaluadorService;
        public ConfiguracionAsignacionExamenController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenAsignadoEvaluadorService = new ConfiguracionAsignacionExamenService(unitOfWork);
            _tokenManager = tokenManager;
        }
    }
}
