using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class ReporteRevisionDocenteController : ControllerBase
    {
        private IReporteRevisionDocenteService _reporteRevisionDocenteService;
        private IUnitOfWork _unitOfWork;
        public ReporteRevisionDocenteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _reporteRevisionDocenteService = new ReporteRevisionDocenteService(_unitOfWork);
            
        }

        /// Controlador: ReporteRevisionDocente
        /// Autor: Edmundo Llaza
        /// Fecha: 26/07/2023
        /// <summary>
        /// Reporte combo modulo - revision docente
        /// </summary>
        /// <returns>area capacitacion, subareacapacitacion, pgeneral, proveedor</returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerComboModulo()
        {
            try
            {
                var reporte = _reporteRevisionDocenteService.ObtenerComboModulo();
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// <summary>
        /// Genera Reporte de Revision de Docentes para Foro
        /// </summary>
        /// <param name="condicion">Cadena de condiciones</param>
        /// <returns> List<RespuestaReporteRevisionDocenteDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult GenerarReporte(ReporteRevisionDocenteDTO Filtro)
        {
            try
            {
                IProveedorService _proveedorRepository = new ProveedorService(_unitOfWork);
                var resultado = _proveedorRepository.GenerarReporte(Filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
