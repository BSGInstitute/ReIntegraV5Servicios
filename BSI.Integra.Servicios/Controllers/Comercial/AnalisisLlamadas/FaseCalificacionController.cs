using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial.AnalisisLlamadas
{
    /// Controlador: FaseCalificacionController
    /// Autor: Joseph Llanque.
    /// Fecha:07/03/2025
    /// <summary>
    /// Fase Calificacion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FaseCalificacionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public FaseCalificacionController(IUnitOfWork unitOfWork)
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
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: FaseCalificacion </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] FaseCalificacionEntradaDTO solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var FaseCalificacion = new FaseCalificacion();
                FaseCalificacion.Nombre = solicitudTipoReporteEntradaDTO.Nombre;
                FaseCalificacion.Orden = solicitudTipoReporteEntradaDTO.Orden;
                FaseCalificacion.Descripcion = solicitudTipoReporteEntradaDTO.Descripcion;
                FaseCalificacion.UsuarioCreacion = solicitudTipoReporteEntradaDTO.Usuario;
                FaseCalificacion.UsuarioModificacion = solicitudTipoReporteEntradaDTO.Usuario;
                FaseCalificacion.FechaCreacion = DateTime.Now;
                FaseCalificacion.FechaModificacion = DateTime.Now;
                FaseCalificacion.Estado = true;
                FaseCalificacion.IdPersonalAreaTrabajo = solicitudTipoReporteEntradaDTO.IdPersonalAreaTrabajo;
                var resultado = solicitudTipoReporteService.Add(FaseCalificacion);
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
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<FaseCalificacion> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<FaseCalificacionEntradaDTO> solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var solicitudTipoReporteLista = new List<FaseCalificacion>();
                foreach (var entidad in solicitudTipoReporteEntradaDTO)
                {
                    var FaseCalificacion = new FaseCalificacion();
                    FaseCalificacion.Nombre = entidad.Nombre;
                    FaseCalificacion.Orden = entidad.Orden;
                    FaseCalificacion.Descripcion = entidad.Descripcion;
                    FaseCalificacion.UsuarioCreacion = entidad.Usuario;
                    FaseCalificacion.UsuarioModificacion = entidad.Usuario;
                    FaseCalificacion.FechaCreacion = DateTime.Now;
                    FaseCalificacion.FechaModificacion = DateTime.Now;
                    FaseCalificacion.Estado = true;
                    solicitudTipoReporteLista.Add(FaseCalificacion);
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
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: FaseCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] FaseCalificacionEntradaDTO solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var FaseCalificacion = new FaseCalificacion();
                FaseCalificacion = solicitudTipoReporteService.ObtenerPorId(solicitudTipoReporteEntradaDTO.Id.Value);
                FaseCalificacion.Nombre = solicitudTipoReporteEntradaDTO.Nombre;
                FaseCalificacion.Orden = solicitudTipoReporteEntradaDTO.Orden;
                FaseCalificacion.Descripcion = solicitudTipoReporteEntradaDTO.Descripcion;
                FaseCalificacion.UsuarioModificacion = solicitudTipoReporteEntradaDTO.Usuario;
                FaseCalificacion.FechaModificacion = DateTime.Now;
                FaseCalificacion.IdPersonalAreaTrabajo = solicitudTipoReporteEntradaDTO.IdPersonalAreaTrabajo;
                var resultado = solicitudTipoReporteService.Update(FaseCalificacion);
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
        /// <param name="solicitudTipoReporteEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: FaseCalificacion </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<FaseCalificacionEntradaDTO> solicitudTipoReporteEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var solicitudTipoReporteLista = new List<FaseCalificacion>();
                foreach (var entidad in solicitudTipoReporteEntradaDTO)
                {
                    var FaseCalificacion = new FaseCalificacion();
                    FaseCalificacion = solicitudTipoReporteService.ObtenerPorId(entidad.Id.Value);
                    FaseCalificacion.Nombre = entidad.Nombre;
                    FaseCalificacion.Orden = entidad.Orden;
            FaseCalificacion.Descripcion = entidad.Descripcion;
                    FaseCalificacion.UsuarioModificacion = entidad.Usuario;
                    FaseCalificacion.FechaModificacion = DateTime.Now;
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

                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
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
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
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
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
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
        public ActionResult ObtenerFases()
        {
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerFases();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 25/11/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla por area
        /// </summary>
       /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idPersonalAreaTrabajo}")]
        [HttpGet]
        public ActionResult ObtenerFasesPorArea(int idPersonalAreaTrabajo)
        {
            try
            {
                var solicitudTipoReporteService = new FaseCalificacionService(unitOfWork);
                var resultado = solicitudTipoReporteService.ObtenerFasesPorArea(idPersonalAreaTrabajo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
