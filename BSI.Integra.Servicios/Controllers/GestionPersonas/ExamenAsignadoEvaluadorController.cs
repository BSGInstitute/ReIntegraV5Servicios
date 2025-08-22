using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
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
    public class ExamenAsignadoEvaluadorController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IExamenAsignadoEvaluadorService _examenAsignadoEvaluadorService;
        public ExamenAsignadoEvaluadorController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenAsignadoEvaluadorService = new ExamenAsignadoEvaluadorService(unitOfWork);
            _tokenManager = tokenManager;
        }
    }
}
