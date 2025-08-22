using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
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
    public class SolicitudTipoReporteController : Controller
    {
        private IUnitOfWork unitOfWork;
        public SolicitudTipoReporteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] SolicitudTipoReporteEntradaDTO solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var solicitudTipoReporte = new SolicitudTipoReporte();
                solicitudTipoReporte.Nombre = solicitudTipoReporteEntradaDTO.Nombre;
                solicitudTipoReporte.UsuarioCreacion = solicitudTipoReporteEntradaDTO.Usuario;
                solicitudTipoReporte.UsuarioModificacion = solicitudTipoReporteEntradaDTO.Usuario;
                solicitudTipoReporte.FechaCreacion = DateTime.Now;
                solicitudTipoReporte.FechaModificacion = DateTime.Now;
                solicitudTipoReporte.Estado = true;
                var resultado = solicitudTipoReporteService.Add(solicitudTipoReporte);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<SolicitudTipoReporte> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<SolicitudTipoReporteEntradaDTO> solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var solicitudTipoReporteLista = new List<SolicitudTipoReporte>();
                foreach (var entidad in solicitudTipoReporteEntradaDTO)
                {
                    var solicitudTipoReporte = new SolicitudTipoReporte();
                    solicitudTipoReporte.Nombre = entidad.Nombre;
                    solicitudTipoReporte.UsuarioCreacion = entidad.Usuario;
                    solicitudTipoReporte.UsuarioModificacion = entidad.Usuario;
                    solicitudTipoReporte.FechaCreacion = DateTime.Now;
                    solicitudTipoReporte.FechaModificacion = DateTime.Now;
                    solicitudTipoReporte.Estado = true;
                    solicitudTipoReporteLista.Add(solicitudTipoReporte);
                }
                var resultado = solicitudTipoReporteService.Add(solicitudTipoReporteLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] SolicitudTipoReporteEntradaDTO solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var solicitudTipoReporte = new SolicitudTipoReporte();
                solicitudTipoReporte = solicitudTipoReporteService.ObtenerPorId(solicitudTipoReporteEntradaDTO.Id.Value);
                solicitudTipoReporte.Nombre = solicitudTipoReporteEntradaDTO.Nombre;
                solicitudTipoReporte.UsuarioModificacion = solicitudTipoReporteEntradaDTO.Usuario;
                solicitudTipoReporte.FechaModificacion = DateTime.Now;
                var resultado = solicitudTipoReporteService.Update(solicitudTipoReporte);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: SolicitudTipoReporte </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<SolicitudTipoReporteEntradaDTO> solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var solicitudTipoReporteLista = new List<SolicitudTipoReporte>();
                foreach (var entidad in solicitudTipoReporteEntradaDTO)
                {
                    var solicitudTipoReporte = new SolicitudTipoReporte();
                    solicitudTipoReporte = solicitudTipoReporteService.ObtenerPorId(entidad.Id.Value);
                    solicitudTipoReporte.Nombre = entidad.Nombre;
                    solicitudTipoReporte.UsuarioModificacion = entidad.Usuario;
                    solicitudTipoReporte.FechaModificacion = DateTime.Now;
                }
                var resultado = solicitudTipoReporteService.Update(solicitudTipoReporteLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
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

                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var resultado = solicitudTipoReporteService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="listadoIds"> Id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true or false </returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new SolicitudTipoReporteService(unitOfWork);
                var resultado = solicitudTipoReporteService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                var solicitudTipoReporteService= new SolicitudTipoReporteService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
