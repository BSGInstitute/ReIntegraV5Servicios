using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ComparativoIngresosController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ComparativoIngresos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ComparativoIngresosController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ComparativoIngresosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// TipoFuncion: GET
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para reporte de Tasas de Conversión Consolidada
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("ObtenerCombosReporteTasaConversionConsolidada")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteTasaConversionConsolidada()
        {
            try
            {
                var serPersonal = new PersonalService(unitOfWork);

                ReporteTasaConversionConsolidadaGeneralDTO result = new ReporteTasaConversionConsolidadaGeneralDTO();
                result.Asesores = serPersonal.ObtenerAsesoresVentasOficial();
                result.Coordinadores = serPersonal.ObtenerCoordinadoresVentasOficial();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Griselberto Huaman.
        /// Fecha: 15/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para reporte de Tasas de Conversión Consolidada
        /// </summary>
        /// <returns> Objeto DTO: ReporteTasaConversionConsolidadaGeneralDTO </returns>
        [Route("ObtenerCentroCostoPorAsesorDetalles")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorAsesorDetalles([FromBody] ReporteTasaConversionConsolidadaFiltroDTO Filtros)
        {
            try
            {
                var serReporte = new ReporteService(unitOfWork);

                var centroCostoOportunidadesdetalles = serReporte.ObtenerCentroCostoPorAsesorDetalles(Filtros);

                return Ok(centroCostoOportunidadesdetalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
