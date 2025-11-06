using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
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
    [Route("api/SolicitudAlumno")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public SolicitudAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: POST
        /// Autor: Joseph Llanque.
        /// Fecha: 08/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Insertar SolicitudAlumno 
        /// </summary>
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudAlumno([FromForm] SolicitudAlumnoEntradaDTO SolicitudAlumnoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                //    var documentoOportunidadService = new DocumentoOportunidadService(unitOfWork); 
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";
                if (SolicitudAlumnoDetalle.Files != null)
                {
                    foreach (var file in SolicitudAlumnoDetalle.Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivotemp = file.FileName;
                        nombreArchivotemp = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", solicitudAlumnoService.SlugNombreArchivo(nombreArchivotemp));
                        urlArchivoRepositorio = solicitudAlumnoService.SubirArchivoSolicitudAlumnoRepositorio(file, file.ContentType, nombreArchivotemp);
                    }
                }
                var solicitudDetalle = new SolicitudAlumno
                {
                    IdEstadoSolicitud = 1,
                    IdPersonal = SolicitudAlumnoDetalle.IdPersonal,
                    IdSolicitud = SolicitudAlumnoDetalle.IdSolicitud,
                    IdMatriculaCabecera = SolicitudAlumnoDetalle.IdMatriculaCabecera,
                    IdPespescifico = SolicitudAlumnoDetalle.IdPEspecifico,
                    DetalleSolicitud = SolicitudAlumnoDetalle.DetalleSolicitud,
                    ContentTypeSolicitante = contentType,
                    NombreArchivoSolicitante = nombreArchivotemp,
                    ContentTypeSolucion = SolicitudAlumnoDetalle.ContentTypeSolucion,
                    NombreArchivoSolucion = SolicitudAlumnoDetalle.NombreArchivoSolucion,
                    ComentarioSolucion = SolicitudAlumnoDetalle.ComentarioSolucion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = SolicitudAlumnoDetalle.Usuario,
                    UsuarioModificacion = SolicitudAlumnoDetalle.Usuario,
                    Estado = true,
                    IdControlSolicitudOrigen = SolicitudAlumnoDetalle.IdControlSolicitudOrigen
                };
                var nuevaentidad = solicitudAlumnoService.Add(solicitudDetalle);
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
        /// Insertar SolicitudAlumno 
        /// </summary>
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarSolicitudAlumno([FromForm] SolicitudAlumnoEntradaDTO SolicitudAlumnoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                string nombreArchivotemp = "";
                string contentType = "";
                var urlArchivoRepositorio = "";
                if (SolicitudAlumnoDetalle.Files != null)
                {
                    foreach (var file in SolicitudAlumnoDetalle.Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivotemp = file.FileName;
                        nombreArchivotemp = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", solicitudAlumnoService.SlugNombreArchivo(nombreArchivotemp));
                        urlArchivoRepositorio = solicitudAlumnoService.SubirArchivoSolicitudAlumnoRepositorio(file, file.ContentType, nombreArchivotemp);
                    }
                }
                var resultado = solicitudAlumnoService.ObtenerPorId((int)SolicitudAlumnoDetalle.Id);
                resultado.IdEstadoSolicitud = (int)SolicitudAlumnoDetalle.IdEstadoSolicitud;
                resultado.IdSolicitud = SolicitudAlumnoDetalle.IdSolicitud;
                resultado.DetalleSolicitud = SolicitudAlumnoDetalle.DetalleSolicitud;
                resultado.IdPespescifico = SolicitudAlumnoDetalle.IdPEspecifico;
                resultado.ContentTypeSolicitante = contentType;
                resultado.NombreArchivoSolicitante = nombreArchivotemp;
                resultado.UsuarioModificacion = SolicitudAlumnoDetalle.Usuario;
                resultado.FechaModificacion = DateTime.Now;
                resultado.IdControlSolicitudOrigen = SolicitudAlumnoDetalle.IdControlSolicitudOrigen;
                var res = solicitudAlumnoService.Update(resultado);
                return Ok(resultado);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  Solicitudes de Alumno
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSolicitudesAlumno()
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.obtenerSolicitudAlumno();
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
        /// Obteniene Solicitudes Por Filtro 
        /// </summary>
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorFiltro([FromBody] FiltroSolicitudesDTO FiltroSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorFiltro(FiltroSolcitud);
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
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorFiltroAlumno([FromBody] FiltroSolicitudAlumnoDTO FiltroSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorFiltroAlumno(FiltroSolcitud);
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
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorFiltroAlumnoRevision([FromBody] FiltroSolicitudAlumnoDTO FiltroSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorFiltroAlumnoRevision(FiltroSolcitud);
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
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesPorFiltroAlumnoGestion([FromBody] FiltroSolicitudAlumnoDTO FiltroSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorFiltroAlumnoGestion(FiltroSolcitud);
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
        /// Obtiene log de  Solicitudes de Alumno
        /// </summary>
        /// <returns> List<HistorialSolicitudAlumnoDTO> </returns>
        [Route("[action]/{IdSolicitud}")]
        [HttpGet]
        public ActionResult ObtenerLogSolicitudes(int idSolicitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.obtenerLogSolicitudes(idSolicitud);
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
        /// Obteniene Solicitudes Por Filtro 
        /// </summary>
        /// <param name="SolicitudAlumnoDetalle"> Parametros de entrada </param>
        /// <param name="Files"> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudesAlumnoPorFiltroReporte([FromBody] FiltroSolicitudesDTO FiltroSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesAlumnoPorFiltroReporte(FiltroSolcitud);
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
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public ActionResult obtenerSolicitudesPorAreaPersonal(int idPersonal)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorArea(idPersonal);
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
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpPost]
        public ActionResult obtenerSolicitudesPorPersonal(int idPersonal)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerSolicitudesPorPersonal(idPersonal);
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
        /// Actualiza comentarios estado y archivo solucion 
        /// </summary>
        /// <param name="RevisionDetalle"> Parametros de entrada </param>
        /// <param name="Files"> ArchivoSolucion </param>
        /// <returns> RevisarSolicitudAlumnoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult revisarSolicitudAlumno([FromForm] RevisarSolicitudAlumnoDTO RevisionDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
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
                var solicitudAlumno = solicitudAlumnoService.ObtenerPorId(RevisionDetalle.id);
                solicitudAlumno.IdEstadoSolicitud = RevisionDetalle.IdEstadoSolicitud;
                solicitudAlumno.ContentTypeSolucion = contentType;
                solicitudAlumno.NombreArchivoSolucion = nombreArchivotemp;
                solicitudAlumno.ComentarioSolucion = RevisionDetalle.ComentarioSolucion;
                solicitudAlumno.FechaModificacion = DateTime.Now;
                solicitudAlumno.UsuarioModificacion = RevisionDetalle.Usuario;
                var nuevaentidad = solicitudAlumnoService.Update(solicitudAlumno);
                return Ok(nuevaentidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jorge Gamero.
        /// Fecha: 12/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obteniene Personal que esté dentro de Solicitud de alumnos
        /// </summary>
        /// <param> </param>
        /// <returns> SolicitudPersonalAlumnoDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPersonalSolicitanteAlumno()
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerPersonalSolicitanteAlumno();
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 11/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obteniene Personal que esté dentro de Solicitud de alumnos
        /// </summary>
        /// <param> </param>
        /// <returns> SolicitudPersonalAlumnoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPersonalSolucionSolicitudAlumno([FromBody] List<int> idPersonal)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerPersonalSolucionSolicitudAlumno(idPersonal);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Jorge Gamero.
        /// Fecha: 15/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obteniene Solicitudes de alumno por filtro 
        /// </summary>
        /// <param> Parametros de entrada </param>
        /// <param> Documentos de envío </param>
        /// <returns> DocumentoOportunidadInsertadoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteSolicitudesPorFiltroAlumno([FromBody] FiltroReporteSolicitudAlumnoDTO FiltroReporteSolcitud)
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerReporteSolicitudesPorFiltroAlumno(FiltroReporteSolcitud);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 15/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obteniene arbol de solicitudes por alumno
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTiposSolicitudCompleto()
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.ObtenerTiposSolicitudCompleto();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return Ok(
                    new TiposSolicitudAlumnosCompletoDTO
                    {
                        TiposSolicitud = null,
                        Error = new ErrorDetalleDTO
                        {
                            Descripción =
                                "No se encontraron registros de tipo de solicitud, categoría o problema.",
                            Exception = ex.ToString(),
                        },
                    }
                );
            }
        }
        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 15/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obteniene la solicitud activa del Alumno
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public ActionResult VerificarSolicitudActivaAlumno(
            [FromBody] VerificarSolicitudAlumnoDTO filtro
        )
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.VerificarSolicitudActivaAlumno(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return Ok(
                    new RespuestaVerificacionSolicitudDTO
                    {
                        Mensaje = null,
                        ExisteSolicitud = null,
                        TiempoPasadoHoras = null,
                        EstadoSolicitud = null,
                        NombreControlSolicitudOrigen = null,
                        Error = new ErrorDetalleDTO
                        {
                            Descripción = "Hubo problemas al calcular el tiempo",
                            Exception = ex.ToString(),
                        },
                    }
                );
            }
        }

        /// TipoFuncion: POST
        /// Autor: Lolo Zaa
        /// Fecha: 31/10/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra una nueva solicitud de alumno desde el ChatBot
        /// </summary>
        /// <param name="solicitud">Datos de la solicitud a registrar</param>
        /// <returns>RespuestaRegistroSolicitudDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarSolicitudAlumno(
            [FromBody] RegistrarSolicitudAlumnoDTO solicitud
        )
        {
            try
            {
                var solicitudAlumnoService = new SolicitudAlumnoService(unitOfWork);
                var resultado = solicitudAlumnoService.RegistrarSolicitudAlumno(solicitud);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return Ok(
                    new RespuestaRegistroSolicitudDTO
                    {
                        Mensaje = null,
                        SolicitudId = null,
                        Error = new ErrorDetalleDTO
                        {
                            Descripción = "No se pudo registrar la solicitud.",
                            Exception = ex.ToString(),
                        },
                    }
                );
            }
        }


    }
}
