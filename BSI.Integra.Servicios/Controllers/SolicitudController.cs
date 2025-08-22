using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudController
    /// Autor: Gilmer Quispe.
    /// Fecha: 23/12/2022
    /// <summary>
    /// Gestión general de Solicitud 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudController : Controller
    {
        private IUnitOfWork unitOfWork;
        public SolicitudController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Solicitud </returns>
        [HttpPost("[Action]")]
        public IActionResult Insertar([FromBody] SolicitudEntradaDTO solicitudEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var solicitud = new Solicitud();
                solicitud.Nombre = solicitudEntradaDTO.Nombre;
                solicitud.Prioridad = solicitudEntradaDTO.Prioridad;
                solicitud.IdSolicitudSubCategoria = solicitudEntradaDTO.IdSolicitudSubCategoria;
                solicitud.IdPersonalRevision = solicitudEntradaDTO.IdPersonalRevision;
                solicitud.IdPersonalSolucion = solicitudEntradaDTO.IdPersonalSolucion;
                solicitud.UsuarioCreacion = solicitudEntradaDTO.Usuario;
                solicitud.UsuarioModificacion = solicitudEntradaDTO.Usuario;
                solicitud.FechaCreacion = DateTime.Now;
                solicitud.FechaModificacion = DateTime.Now;
                solicitud.Estado = true;
                var resultado = solicitudService.Add(solicitud);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica de tipo lista a la tabla
        /// </summary>
        /// <param name="solicitudEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> List<Solicitud> </returns>
        [HttpPost("[Action]")]
        public IActionResult InsertarLista([FromBody] List<SolicitudEntradaDTO> solicitudEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var solicitudLista = new List<Solicitud>();
                foreach (var entidad in solicitudEntradaDTO)
                {
                    var solicitud = new Solicitud();
                    solicitud.Nombre = entidad.Nombre;
                    solicitud.Prioridad = solicitud.Prioridad;
                    solicitud.IdSolicitudSubCategoria = solicitud.IdSolicitudSubCategoria;
                    solicitud.IdPersonalRevision = solicitud.IdPersonalRevision;
                    solicitud.IdPersonalSolucion = solicitud.IdPersonalSolucion;
                    solicitud.UsuarioCreacion = entidad.Usuario;
                    solicitud.UsuarioModificacion = entidad.Usuario;
                    solicitud.FechaCreacion = DateTime.Now;
                    solicitud.FechaModificacion = DateTime.Now;
                    solicitud.Estado = true;
                    solicitudLista.Add(solicitud);
                }
                var resultado = solicitudService.Add(solicitudLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Solicitud </returns>
        [HttpPut("[Action]")]
        public IActionResult Actualizar([FromBody] SolicitudEntradaDTO solicitudEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var solicitud = solicitudService.ObtenerPorId(solicitudEntradaDTO.Id.Value);
                solicitud.Prioridad = solicitudEntradaDTO.Prioridad;
                solicitud.IdSolicitudSubCategoria = solicitudEntradaDTO.IdSolicitudSubCategoria;
                solicitud.IdPersonalRevision = solicitudEntradaDTO.IdPersonalRevision;
                solicitud.IdPersonalSolucion = solicitudEntradaDTO.IdPersonalSolucion;
                solicitud.Nombre = solicitudEntradaDTO.Nombre;
                solicitud.IdSolicitudSubCategoria = solicitudEntradaDTO.IdSolicitudSubCategoria;
                solicitud.UsuarioModificacion = solicitudEntradaDTO.Usuario;
                solicitud.FechaModificacion = DateTime.Now;
                var resultado = solicitudService.Update(solicitud);
                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="solicitudEntradaDTO"> Datos necesarios para la insercion de datos </param>
        /// <returns> Entidad: Solicitud </returns>
        [HttpPut("[Action]")]
        public IActionResult ActualizarLista([FromBody] List<SolicitudEntradaDTO> solicitudEntradaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var solicitudLista = new List<Solicitud>();
                foreach (var entidad in solicitudEntradaDTO)
                {
                    var solicitud = new Solicitud();
                    solicitud = solicitudService.ObtenerPorId(entidad.Id.Value);
                    solicitud.Prioridad = solicitud.Prioridad;
                    solicitud.IdSolicitudSubCategoria = solicitud.IdSolicitudSubCategoria;
                    solicitud.IdPersonalRevision = solicitud.IdPersonalRevision;
                    solicitud.IdPersonalSolucion = solicitud.IdPersonalSolucion;
                    solicitud.Nombre = entidad.Nombre;
                    solicitud.UsuarioModificacion = entidad.Usuario;
                    solicitud.FechaModificacion = DateTime.Now;
                }
                var resultado = solicitudService.Update(solicitudLista);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
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

                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.Delete(id, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 23/12/2022
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
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.Delete(listadoIds, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Solicitudes
        /// </summary>
        /// <returns> List<TipoReporteSubCategoriaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSolicitudes()
        {
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.ObtenerTipoReporteSubCategoria();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Historial de Solicitudes de Alumno
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]/{IdMatriculaCabecera}/{PEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerHistorialSolcitudAlumno(int IdMatriculaCabecera,int PEspecifico)
        {
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.ObtenerHistorialSolicitudAlumno(IdMatriculaCabecera, PEspecifico);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estados de Solicitudes
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadosSolicitud(int IdMatriculaCabecera, int PEspecifico)
        {
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.ObtenerEstadosSolicitud();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estados de Solicitudes para revision
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadosSolicitudRevision(int IdMatriculaCabecera, int PEspecifico)
        {
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.ObtenerEstadosSolicitudRevision();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Estados de Solicitudes para gestion
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadosSolicitudGestion(int IdMatriculaCabecera, int PEspecifico)
        {
            try
            {
                var solicitudService = new SolicitudService(unitOfWork);
                var resultado = solicitudService.ObtenerEstadosSolicitudGestion();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
