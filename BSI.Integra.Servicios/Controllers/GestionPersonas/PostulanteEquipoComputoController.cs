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
    public class PostulanteEquipoCompuntoController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IConfiguracionAsignacionEvaluacionService _examenAsignadoEvaluadorService;
        public PostulanteEquipoCompuntoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenAsignadoEvaluadorService = new ConfiguracionAsignacionEvaluacionService(unitOfWork);
            _tokenManager = tokenManager;
        }
    }
}
