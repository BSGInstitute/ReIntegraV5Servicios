using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: EncuestaOnlineController
    /// Autor: Jeremy Pacheco
    /// Fecha: 12/11/2024
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte para Encuestas Sincrónicas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteEncuestasSincronicoController: ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private ITokenManager _tokenManager;
        private IReporteEncuestasSincronicoService _reporteEncuestasSincronicoServices;
        public ReporteEncuestasSincronicoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _reporteEncuestasSincronicoServices = new ReporteEncuestasSincronicoService(unitOfWork);
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco.
        /// Fecha: 12/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos de los docentes para combo en filtro
        /// </summary>
        /// <param></param>
        /// <returns> IEnumerable<ComboDTO> </returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenerComboDocentes()
        {
            try
            {
                var ComboDocente = _reporteEncuestasSincronicoServices.ObtenerComboDocentes();
                return Ok(ComboDocente);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco.
        /// Fecha: 12/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas iniciales
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO"> Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasInicialSincronicoDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaInicialSincronico([FromBody] ReporteEncuestaFiltroSincronicoPorVersionDTO encuestaOnlineFiltroDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteEncuestaInicialSincronico(encuestaOnlineFiltroDTO);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco.
        /// Fecha: 12/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas intermedias
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO"> Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasIntermediaSincronicoDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaIntermediaSincronico([FromBody] ReporteEncuestaFiltroSincronicoPorVersionDTO encuestaOnlineFiltroDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteEncuestaIntermediaSincronico(encuestaOnlineFiltroDTO);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco.
        /// Fecha: 12/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas finales
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO"> Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasFinalSincronicoDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaFinalSincronico([FromBody] ReporteEncuestaFiltroSincronicoPorVersionDTO encuestaOnlineFiltroDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteEncuestaFinalSincronico(encuestaOnlineFiltroDTO);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Margiory Ramirez .
        /// Fecha: 23/01/2025
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas finales
        /// </summary>
        /// <param name="filtro"> Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasFinalSincronicoDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaDocente([FromBody] ReporteEncuestaFiltroSincronicoDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteEncuestaDocente(filtro);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		[Route("[Action]")]
		[HttpPost]
		public IActionResult GenerarReporteTestimonioPorModalidad([FromBody] filtroTestimonioDTO filtroDTO)
		{
			try
			{
				var reporte = _reporteEncuestasSincronicoServices.GenerarReporteTestimonioPorModalidad(filtroDTO);
				return Ok(reporte);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
        [Route("[Action]")]
        [HttpPost]
        public IActionResult ObtenerRespuestaEncuestaCombo(FiltroRespuestaCombo filtro)
        {
            return Ok(_reporteEncuestasSincronicoServices.ObtenerRespuestaEncuestaCombo(filtro));
        }

        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult GuardarTestimonio([FromBody] TestimonioInsertarDTO dto)
        {
            try
            {
                var resultado = _reporteEncuestasSincronicoServices.GuardarTestimonio(dto, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteTestimonioASincronico([FromBody] filtroTestimonioDTO filtroDTO)
        {
            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteTestimonioASincronico(filtroDTO);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteValoracionTotal([FromBody] filtroValoracionDTO filtroDTO)
        {
            try
            {
                var reporte = _reporteEncuestasSincronicoServices.GenerarReporteValoracionTotal(filtroDTO);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpPut("[action]")]
        public IActionResult ActualizarValoracionVisiblePw([FromBody] ValoracionesActualizarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _reporteEncuestasSincronicoServices.ActualizarValoracionVisiblePw(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
