using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudAlumnoController
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2023
    /// <summary>
    /// Gestión general de SolicitudAlumno
    /// </summary>
    [Route("api/SolicitudInterna")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudInternaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SolicitudInternaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Insertar SolicitudInterna 
        /// </summary>
        /// <param name="SolicitudInternaDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudInterna([FromForm] SolicitudInternaEntradaDTO SolicitudInternaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                //    var documentoOportunidadService = new DocumentoOportunidadService(unitOfWork); 
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";

                if (SolicitudInternaDetalle.Files != null)
                {
                    foreach (var file in SolicitudInternaDetalle.Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivotemp = file.FileName;
                        nombreArchivotemp = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", solicitudInternaService.SlugNombreArchivo(nombreArchivotemp));
                        urlArchivoRepositorio = solicitudInternaService.SubirArchivoSolicitudInternaRepositorio(file, file.ContentType, nombreArchivotemp);
                    }
                }

                var solicitudDetalle = new SolicitudInterna
                {
                    IdEstadoSolicitud = 1,
                    IdPersonal = SolicitudInternaDetalle.IdPersonal,
                    IdSolicitud = SolicitudInternaDetalle.IdSolicitud,
                    DetalleSolicitud = SolicitudInternaDetalle.DetalleSolicitud,
                    ContentTypeSolicitante = contentType,
                    NombreArchivoSolicitante = nombreArchivotemp,
                    ContentTypeSolucion = SolicitudInternaDetalle.ContentTypeSolucion,
                    NombreArchivoSolucion = SolicitudInternaDetalle.NombreArchivoSolucion,
                    ComentarioSolucion = SolicitudInternaDetalle.ComentarioSolucion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = SolicitudInternaDetalle.Usuario,
                    UsuarioModificacion = SolicitudInternaDetalle.Usuario,
                    Estado = true,

                };
                var nuevaentidad = solicitudInternaService.Add(solicitudDetalle);
                return Ok(nuevaentidad);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obteniene Solicitud por revisar 
        /// </summary>
        /// <param name="idPersonal"> Parametros de entrada </param>
        /// <returns> SolicitudInternaFiltradaDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public ActionResult obtenerSolicitudesPorAreaPersonal(int idPersonal)
        {
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                var resultado = solicitudInternaService.ObtenerSolicitudesPorArea(idPersonal);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obteniene Solicitudes Por Filtro 
        /// </summary>
        /// <param name="idPersonal"> Parametros de entrada </param>
        /// <returns> SolicitudInternaFiltradaDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public ActionResult obtenerSolicitudesInternasGestion(int idPersonal)
        {
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                var resultado = solicitudInternaService.ObtenerSolicitudesGestion(idPersonal);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // TipoFuncion: POST
        // Autor: Joseph Llanque.
        // Fecha: 08/03/2023
        // Versión: 1.0
        // <summary>
        // Obteniene Solicitudes Por Filtro 
        // </summary>
        // <param name = "SolicitudAlumnoDetalle" > Parametros de entrada </param>
        // <param name = "Files" > Documentos de envío </param>
        // <returns> SolicitudInternaFiltradaDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorFiltro([FromBody] FiltroSolicitudesInternasDTO FiltroSolcitud)
        {
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                var resultado = solicitudInternaService.ObtenerSolicitudesPorFiltro(FiltroSolcitud);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        // TipoFuncion: POST
        // Autor: Joseph Llanque.
        // Fecha: 08/03/2023
        // Versión: 1.0
        // <summary>
        // Obteniene Solicitudes Por Filtro 
        // </summary>
        // <param name = "SolicitudAlumnoDetalle" > Parametros de entrada </param>
        // <param name = "Files" > Documentos de envío </param>
        // <returns> SolicitudInternaFiltradaDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesInternasPorFiltroReporte([FromBody] FiltroSolicitudesInternasDTO FiltroSolcitud)
        {
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                var resultado = solicitudInternaService.ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolcitud);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  Solicitudes de Alumno
        /// </summary>
        /// <returns> List<SolicitudInternaFiltradaDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSolicitudesInternas()
        {
            try
            {
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                var resultado = solicitudInternaService.obtenerSolicitudInterna();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza comentarios estado y archivo solucion 
        /// </summary>
        /// <param name="RevisionDetalle"> Parametros de entrada </param>
        /// <param name="Files"> ArchivoSolucion </param>
        /// <returns> RevisarSolicitudAlumnoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult revisarSolicitudInterna([FromForm] RevisarSolicitudAlumnoDTO RevisionDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var solicitudInternaService = new SolicitudInternaService(unitOfWork);
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";

                if (RevisionDetalle.Files != null)
                {
                    foreach (var file in RevisionDetalle.Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivotemp = file.FileName;
                        nombreArchivotemp = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", solicitudAlumnoService.SlugNombreArchivo(nombreArchivotemp));
                        urlArchivoRepositorio = solicitudAlumnoService.SubirArchivoSolicitudAlumnoRepositorio(file, file.ContentType, nombreArchivotemp);
                    }
                }
                var solicitudInterna = solicitudInternaService.ObtenerPorId(RevisionDetalle.id);


                solicitudInterna.IdEstadoSolicitud = RevisionDetalle.IdEstadoSolicitud;
                solicitudInterna.ContentTypeSolucion = contentType;
                solicitudInterna.NombreArchivoSolucion = nombreArchivotemp;
                solicitudInterna.ComentarioSolucion = RevisionDetalle.ComentarioSolucion;
                solicitudInterna.FechaModificacion = DateTime.Now;
                solicitudInterna.UsuarioModificacion = RevisionDetalle.Usuario;


                var nuevaentidad = solicitudInternaService.Update(solicitudInterna);
                return Ok(nuevaentidad);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
