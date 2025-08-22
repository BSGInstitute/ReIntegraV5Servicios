using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: QuejaSugerenciaController
    /// Autor: Gilmer Qm
    /// Fecha: 20/07/2023
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de quejas y sugerencias 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class QuejaSugerenciaController : ControllerBase
    {
        private IQuejaSugerenciaService _quejaSugerenciaService;
        public QuejaSugerenciaController(IUnitOfWork unitOfWork)
        {
            _quejaSugerenciaService = new QuejaSugerenciaService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de quejas y sugerencias segun el filtro ingresado
        /// </summary>
        /// <param name="quejaSugerenciaFiltro">filtro para la seleccion del reporte de quejas y sugerencias</param>
        /// <returns>Lista del reporte quejas y sugerencias en un List<QuejaSugerenciaDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] QuejaSugerenciaFiltroDTO quejaSugerenciaFiltro)
        {
            try
            { 
                var lista = _quejaSugerenciaService.GenerarReporteQuejaSugerencia(quejaSugerenciaFiltro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
