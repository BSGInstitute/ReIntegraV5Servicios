using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudTipoReporteController
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SeguimientoAlumnoCategoriaController : Controller
    {
        private IUnitOfWork unitOfWork;
        public SeguimientoAlumnoCategoriaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


            /// Tipo Función: GET
            /// Autor:Joseph Llanque
            /// Fecha: 26/12/2022
            /// Versión: 1.0
            /// <summary>
            /// Obtiene todos los registros de la tabla
            /// </summary>
            /// <returns> List<ComboDTO> </returns>
            [Route("[action]")]
            [HttpGet]
            public ActionResult ObtenerConfiguracion()
            {
                try
                {
                    var solicitudTipoReporteService = new SeguimientoAlumnoCategoriaService(unitOfWork);
                    var resultado = solicitudTipoReporteService.ObtenerConfiguracion();
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 26/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPost("[Action]")]
        public bool Insertar([FromBody] SeguimientoAlumnoCategoriaEntradaDTO solicitudTipoReporteEntradaDTO)
        {
            
            try
            {
                var seguimientoAlumnoCategoriaService = new SeguimientoAlumnoCategoriaService(unitOfWork);
                return seguimientoAlumnoCategoriaService.Insertar(solicitudTipoReporteEntradaDTO);              
               
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 26/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPut("[Action]")]
        public bool Actualizar([FromBody] SeguimientoAlumnoCategoriaEntradaDTO solicitudTipoReporteEntradaDTO)
        {

            try
            {

                var seguimientoAlumnoCategoriaService = new SeguimientoAlumnoCategoriaService(unitOfWork);
                return seguimientoAlumnoCategoriaService.Actualizar(solicitudTipoReporteEntradaDTO);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Tipo Función: POST
        /// Autor: joseph Llanque.
        /// Fecha: 21/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SeguimientoAlumnoCategoriaService(unitOfWork);
                var resultado = solicitudTipoReporteService.Eliminar(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}
