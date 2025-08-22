using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class ProyectoPresentadoPorAlumnoController : ControllerBase
    {
        private IProyectoPresentadoPorAlumnoService _proyectoPresentadoPorAlumnoService;
        private IUnitOfWork _unitOfWork;
        public ProyectoPresentadoPorAlumnoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _proyectoPresentadoPorAlumnoService = new ProyectoPresentadoPorAlumnoService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="Filtros">Filtros que contienen el Nombre Parcial</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("ObtenerFiltroAutocomplete")]
        public IActionResult ObtenerFiltroAutocomplete([FromBody] StringDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ICentroCostoService servicio = new CentroCostoService(_unitOfWork);
            return Ok(servicio.ObtenerFiltroAutocomplete(filtro.Valor));
        }
        /// Tipo Función: POST
        /// Autor: Cesar Santillana
        /// Fecha: 25/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de los proyectos presentados por alumno
        /// </summary>
        /// <param name="filtroReporte">Filtro para el reporte de los proyectos presentados por alumno  </param>
        /// <returns>El reporte retorna una Lista List<ProyectoPresentadoPorAlumnoDTO></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ProyectoPresentadoPorAlumnoFiltroDTO filtroReporte)
        {
            try
            {
                if (filtroReporte != null)
                {
                    IMatriculaCabeceraService _repPEspecifico = new MatriculaCabeceraService(_unitOfWork);
                    return Ok(_repPEspecifico.GenerarReporteProyectoPresentadoPorAlumno(filtroReporte));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Funcion: Get
        /// Autor: Edmundo Llaza
        /// Fecha 2023-07-31
        /// <summary>
        /// Función que trae data para llenar los combos Coordinadores, Docente, CentroCosto y PEspecifico
        /// </summary>
        /// <returns>Retorma una lista</returns>
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosModulo()
        {
            try
            {
                var reporteCombo = _proyectoPresentadoPorAlumnoService.ObtenerCombosModulo();
                return Ok(reporteCombo);
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-23
        /// Versión: 1.0
        /// <summary>
        /// Generar un reporte con los nombre de los programas especificos según el valor ingresado
        /// </summary>
        /// <param name="valor"></param>
        /// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPorNombreAutocomplete([FromBody] Dictionary<string, string> valor)
        {
            try
            {
                if (valor != null)
                {
                    var listado = _proyectoPresentadoPorAlumnoService.ObtenerPorNombreAutocomplete(valor["valor"].ToString());
                    return Ok(listado);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
