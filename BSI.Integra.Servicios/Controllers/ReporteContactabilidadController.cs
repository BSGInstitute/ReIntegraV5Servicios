using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteContactabilidadController
    /// Autor: Gilmer Quispe
    /// Fecha: 23/09/2022
    /// <summary>
    /// Gestión Reporte de Contactabilidad
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteContactabilidadController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        public ReporteContactabilidadController(IUnitOfWork unitOfWork, IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            this.unitOfWork = unitOfWork;
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 23/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combo de asesor para Módulo de Reporte de Contactabilidad
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicioPersonal = new PersonalService(unitOfWork);
                    var resultado = new ReporteContactabilidadCombosDTO();

                    resultado.Asesores = servicioPersonal.ObtenerAsesoresVentasOficial_CONT(_respuestaCorrecta.RegistroClaimToken.IdPersonal).Where(w => w.TipoPersonal == "Asesor" || w.TipoPersonal == "otro").ToList();
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }

        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteOperaciones(int idPersonal)
        {
            try
            {
                var reporte = new ReporteService(unitOfWork);
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

                if (_respuestaCorrecta.TokenValida)
                {
                    idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(_respuestaCorrecta.RegistroClaimToken.IdPersonal, "Comercial/SeguimientoOportunidades");
                    var result = reporte.ObtenerCombosReporteOperaciones(idPersonal);
                    return Ok(result);
                }
                else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos de asesores para Módulo de Reporte de Contactabilidad para Operaciones
        /// </summary>
        /// <param name="idPersonal"> Id del Personal </param>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ReporteOperacionesPorPersonal(int idPersonal)
        {
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = servicioReporte.ObtenerCombosReporteOperaciones(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad para Comercial
        /// </summary>
        /// <param name="reporteContactabilidadFiltro"> Filtros de busqueda </param>
        /// <returns> objeto : ReporteContactabilidadDataV2DTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportev2([FromBody] ReporteContactabilidadFiltroAlternoDTO reporteContactabilidadFiltro)
        {
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = servicioReporte.ReporteContactabilidadV2(reporteContactabilidadFiltro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Generar Reporte de contactabilidad Desagregado (Deprecated)
        /// </summary>
        /// <param name="reporteContactabilidadFiltro"> Filtros de busqueda </param>
        /// <returns> objeto : Reportes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDesagregado([FromBody] ReporteContactabilidadFiltroDTO reporteContactabilidadFiltro)
        {
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = servicioReporte.ReporteContactabilidadDesagregado(reporteContactabilidadFiltro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
