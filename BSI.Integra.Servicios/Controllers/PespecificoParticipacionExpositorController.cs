using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Controlador: PespecificoParticipacionExpositorController
    /// Autor: Christian Alex Quispe
    /// Fecha: 15/09/2023
    /// <summary>
    /// Gestión de Expositores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PespecificoParticipacionExpositorController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IPespecificoParticipacionExpositorService _pespecificoParticipacionExpositor;
        public PespecificoParticipacionExpositorController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _pespecificoParticipacionExpositor = new PespecificoParticipacionExpositorService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Recueperar todos los combos para el reporte
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosProgramaEspecificoProveedor()
        {
            try
            {
                var listado = _pespecificoParticipacionExpositor.ObtenerCombosProgramaEspecificoProveedor();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Recueperar todos los datos del reporte
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFiltro([FromBody] ParticipacionExpositorFiltroDTO dto)
        {
            try
            {
                var listado = _pespecificoParticipacionExpositor.GenerarReporteParticipacionExpositor(dto);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el registro si el proveedor confirmo
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPut]
        [Authorize]
        [JwtExpirationValidation]
        public ActionResult ActualizarProveedor([FromBody] ParticipacionExpositorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var listado = _pespecificoParticipacionExpositor.ActualizarProveedor(dto, _tokenManager.UserName);
            return Ok(listado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el registro si el proveedor confirmo de operaciones
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPut]
        [Authorize]
        [JwtExpirationValidation]
        public ActionResult ActualizarProveedorConfirmacion([FromBody] PEE_ProveedorOperacionesGrupoConfirmadoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var listado = _pespecificoParticipacionExpositor.ActualizarProveedorConfirmacion(dto, _tokenManager.UserName);
            return Ok(listado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el registro de asistencias
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{idCursoActual}")]
        [HttpPut]
        [Authorize]
        [JwtExpirationValidation]
        public ActionResult ActualizarRegistroAsistencia(int idCursoActual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pespecificoParticipacionExpositor.ActualizarRegistroAsistencia(idCursoActual, _tokenManager.UserName);
            return Ok(resultado);
        }
        /// Tipo Función: POST
        /// Autor: Christian Qm
        /// Fecha: 15/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualizar el registro de notas
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{idCursoActual}")]
        [HttpPut]
        [Authorize]
        [JwtExpirationValidation]
        public ActionResult ActualizarRegistroNotas(int idCursoActual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _pespecificoParticipacionExpositor.ActualizarRegistroNotas(idCursoActual, _tokenManager.UserName);
            return Ok(resultado);
        }
    }
}
