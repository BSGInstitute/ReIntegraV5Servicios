using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: RevisionNivelPwController
    /// Autor: Jonathan Caipo
    /// Fecha: 20/09/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión de PGeneralRevisionNivelPw
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class RevisionNivelPwController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IRevisionNivelPwService _revisionNivelPwService;
        public RevisionNivelPwController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _revisionNivelPwService = new RevisionNivelPwService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la por id plantilla todo RevisionNivelPw
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns> List<RevisionNivelPwDTO> </returns>
        [Route("[Action]/{idPlantilla}")]
        [HttpGet]
        public IActionResult ObtenerRevisionNivelPorIdPlantilla(int idPlantilla)
        {
            return Ok(_revisionNivelPwService.ObtenerPorIdRevisionPw(idPlantilla));
        }
    }
}
