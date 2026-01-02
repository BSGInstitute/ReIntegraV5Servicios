using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: AreaCentroCostoController
    /// Autor Modificacion: Klebert Layme.
    /// Fecha: 26/04/2023
    /// <summary>
    /// Gestión de AreaCentroCosto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PespecificoSesionController : Controller
    {
        private IPEspecificoSesionService _pEspecificoSesionService;
        private IAsistenciaWebinarService _asistenciaWebinarService;

        public PespecificoSesionController(IUnitOfWork unitOfWork)
        {
            _pEspecificoSesionService = new PEspecificoSesionService(unitOfWork);
            _asistenciaWebinarService = new AsistenciaWebinarService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Klebert Layme
        /// Fecha: 26/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los Areas centro de costo
        /// </summary>
        /// <returns> ListaPespecificoSesion </returns>
        //[AllowAnonymous]
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VerificarFechaSesion([FromBody] FechaSesionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pEspecificoSesionService.VerificarFechaSesion(dto.IdSesion, dto.Fecha);
            return Ok(resultado);
        }
        /// Tipo Función: PUT
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Datos del Cronograma de Sesiones
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPut]
        public IActionResult ActualizarDatosCronogramaSesiones([FromBody] InformacionCronogramaSesionesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = _pEspecificoSesionService.ActualizarDatosCronogramaSesiones(dto, registroClaimToken.UserName);
            if (resultado.IdTipoPrograma == 3)
            { // tipo 3 son programas webinar
                var nuevaFecha = resultado.FechaSesion.Value.Date.AddMinutes(1);
                var jobId = BackgroundJob.Schedule(
                    () => _asistenciaWebinarService.ConfirmacionWebinarAutomatica(resultado.IdPEspecificoSesion.Value),
                    nuevaFecha
                );
            }
            return Ok(new
            {
                resultado.EstadoCruce,
                resultado.Cruces,
                resultado.Detalle
            });
        }
        /// Tipo Función: GET
		/// Autor: Flavio R. Mamani Fabian
		/// Fecha: 31/05/2023
		/// Version: 1.0
        /// <summary>
        /// Verifica si tiene duracion
        /// </summary>
        /// <param name="idPespecificoPadre">Id Pespecifico padre</param>
        [Route("[Action]/{idProgramaEspecificoSesion}")]
        [Authorize]
        [HttpPut]
        public ActionResult<bool> EstablecerSesionInicial(int idProgramaEspecificoSesion)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoSesionService.EstablecerSesionInicial(idProgramaEspecificoSesion, registroClaimToken.UserName));
        }
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult CancelarWebinar([FromBody] CancelarWebinarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoSesionService.CancelarWebinar(dto, registroClaimToken.UserName));
        }
        [Authorize]
        [Route("[Action]/{idProgramaEspecifico}/{idProgramaEspecificoSesion}")]
        [HttpDelete]
        public ActionResult EliminarSesion(int idProgramaEspecifico, int idProgramaEspecificoSesion)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            return Ok(_pEspecificoSesionService.EliminarSesion(idProgramaEspecifico, idProgramaEspecificoSesion, registroClaimToken.UserName));
        }
        ///Metodo Http: POST
        /// Autor: Gilmer Qm
        /// Fecha: 20/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Confirma webinar
        /// </summary>
        /// <param name="Json"> parametros entrada </param>
        /// <returns> bool </returns>
        [Route("[Action]")]
        [Authorize]
        [HttpPost]
        public ActionResult ConfirmarWebinar([FromBody] ConfirmacionWebinarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(_pEspecificoSesionService.ConfirmarWebinar(Json, registroClaimToken.UserName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
