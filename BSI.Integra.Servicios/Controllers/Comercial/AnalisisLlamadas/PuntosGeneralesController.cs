using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial.AnalisisLlamadas
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class PuntosGeneralesController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PuntosGeneralesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="puntosGeneralesCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PuntosGeneralesCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] PuntosGeneralesEntradaDTO puntosGeneralesCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var PuntosGeneralesCalificacion = new PuntosGeneralesCalificacion();
                PuntosGeneralesCalificacion.Nombre = puntosGeneralesCalificacionEntradaDTO.Nombre;
                PuntosGeneralesCalificacion.Orden = puntosGeneralesCalificacionEntradaDTO.Orden;
                PuntosGeneralesCalificacion.Descripcion = puntosGeneralesCalificacionEntradaDTO.Descripcion;
                PuntosGeneralesCalificacion.UsuarioCreacion = puntosGeneralesCalificacionEntradaDTO.Usuario;
                PuntosGeneralesCalificacion.UsuarioModificacion = puntosGeneralesCalificacionEntradaDTO.Usuario;
                PuntosGeneralesCalificacion.FechaCreacion = DateTime.Now;
                PuntosGeneralesCalificacion.FechaModificacion = DateTime.Now;
                PuntosGeneralesCalificacion.Estado = true;
                PuntosGeneralesCalificacion.IdPersonalAreaTrabajo = puntosGeneralesCalificacionEntradaDTO.IdPersonalAreaTrabajo;
                var resultado = solicitudTipoReporteService.Add(PuntosGeneralesCalificacion);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="puntosGeneralesCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<PuntosGeneralesCalificacion> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<PuntosGeneralesEntradaDTO> puntosGeneralesCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var solicitudTipoReporteLista = new List<PuntosGeneralesCalificacion>();
                foreach (var entidad in puntosGeneralesCalificacionEntradaDTO)
                {
                    var PuntosGeneralesCalificacion = new PuntosGeneralesCalificacion();
                    PuntosGeneralesCalificacion.Nombre = entidad.Nombre;
                    PuntosGeneralesCalificacion.Orden = entidad.Orden;
                    PuntosGeneralesCalificacion.Descripcion = entidad.Descripcion;
                    PuntosGeneralesCalificacion.UsuarioCreacion = entidad.Usuario;
                    PuntosGeneralesCalificacion.UsuarioModificacion = entidad.Usuario;
                    PuntosGeneralesCalificacion.FechaCreacion = DateTime.Now;
                    PuntosGeneralesCalificacion.FechaModificacion = DateTime.Now;
                    PuntosGeneralesCalificacion.Estado = true;
                    solicitudTipoReporteLista.Add(PuntosGeneralesCalificacion);
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
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="puntosGeneralesCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PuntosGeneralesCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] PuntosGeneralesEntradaDTO puntosGeneralesCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var PuntosGeneralesCalificacion = new PuntosGeneralesCalificacion();
                PuntosGeneralesCalificacion = solicitudTipoReporteService.ObtenerPorId(puntosGeneralesCalificacionEntradaDTO.Id.Value);
                PuntosGeneralesCalificacion.Nombre = puntosGeneralesCalificacionEntradaDTO.Nombre;
                PuntosGeneralesCalificacion.Orden = puntosGeneralesCalificacionEntradaDTO.Orden;
                PuntosGeneralesCalificacion.Descripcion = puntosGeneralesCalificacionEntradaDTO.Descripcion;
                PuntosGeneralesCalificacion.UsuarioModificacion = puntosGeneralesCalificacionEntradaDTO.Usuario;
                PuntosGeneralesCalificacion.FechaModificacion = DateTime.Now;
                PuntosGeneralesCalificacion.IdPersonalAreaTrabajo = puntosGeneralesCalificacionEntradaDTO.IdPersonalAreaTrabajo;
                var resultado = solicitudTipoReporteService.Update(PuntosGeneralesCalificacion);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="puntosGeneralesCalificacionEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: PuntosGeneralesCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<PuntosGeneralesEntradaDTO> puntosGeneralesCalificacionEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var solicitudTipoReporteLista = new List<PuntosGeneralesCalificacion>();
                foreach (var entidad in puntosGeneralesCalificacionEntradaDTO)
                {
                    var PuntosGeneralesCalificacion = new PuntosGeneralesCalificacion();
                    PuntosGeneralesCalificacion = solicitudTipoReporteService.ObtenerPorId(entidad.Id.Value);
                    PuntosGeneralesCalificacion.Nombre = entidad.Nombre;
                    PuntosGeneralesCalificacion.Orden = entidad.Orden;
                    PuntosGeneralesCalificacion.Descripcion = entidad.Descripcion;
                    PuntosGeneralesCalificacion.UsuarioModificacion = entidad.Usuario;
                    PuntosGeneralesCalificacion.FechaModificacion = DateTime.Now;
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
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
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

                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var resultado = solicitudTipoReporteService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque.
        /// Fecha:07/03/2025
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
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var resultado = solicitudTipoReporteService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
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
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerCombo();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPuntosGenerales()
        {
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerPuntosGenerales();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla por area
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPuntosGeneralesPorArea(int idPersonalAreaTrabajo)
        {
            try
            {
                var solicitudTipoReporteService = new PuntosGeneralesService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerPuntosGeneralesPorArea(idPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
