using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ProgramaGeneralCertificacionController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 22/07/2022
    /// <summary>
    /// Gestión de ProgramaGeneralCertificacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralCertificacionController : ControllerBase
    {
        private IProgramaGeneralCertificacionService _programaGeneralCertificacionService;
        private ITokenManager _tokenManager;
        public ProgramaGeneralCertificacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            _programaGeneralCertificacionService = new ProgramaGeneralCertificacionService(unitOfWork);
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/07/2022
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns> Retorna 200 </returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{idProgramaGeneralCertificacion}")]
        [HttpDelete]
        public ActionResult EliminarCertificacionVenta(int idProgramaGeneralCertificacion)
        {
            var resultado = _programaGeneralCertificacionService.EliminarCertificacionVenta(idProgramaGeneralCertificacion, _tokenManager.UserName);
            return Ok(resultado);
        }
    }
}
