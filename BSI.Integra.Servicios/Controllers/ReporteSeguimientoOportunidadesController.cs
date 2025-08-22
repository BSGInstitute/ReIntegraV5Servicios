using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteSeguimientoOportunidadesController
    /// Autor: Gilmer Quispe
    /// Fecha: 28/09/2022
    /// <summary>
    /// Gestión Reporte Seguimiento de Oportunidades
    /// </summary>

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteSeguimientoOportunidadesController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        public ReporteSeguimientoOportunidadesController(IUnitOfWork unitOfWork, IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            this.unitOfWork = unitOfWork;
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos del reporte se seguimiento
        /// </summary>
        /// <param name="idPersonal">Id del personal </param>
        /// <returns> Objeto de clase ReporteSeguimientoOportunidadCombosDTO </returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
                    var idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(_respuestaCorrecta.RegistroClaimToken.IdPersonal, "Comercial/SeguimientoOportunidades");
                    return Ok(servicio.ObtenerCombosReporte(idPersonal));
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos del modulo de habilitar discador
        /// </summary>
        /// <param name="idPersonal">Id del personal </param>
        /// <returns> Objeto de clase ReporteSeguimientoOportunidadCombosDTO </returns>
        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPersonalDiscador(int idPersonal)
        {
            try
            {
                var servicioPersonal = new PersonalService(unitOfWork);
                var resultado = new ReporteSeguimientoOportunidadCombosDTO();

                resultado.Asesores = idPersonal == 213 ?
                                    servicioPersonal.AsesoresVentasOficialReporteSeguimiento() :
                                    servicioPersonal.PersonalAsignadoVentas(idPersonal);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para el reporte de seguimiento de operaciones
        /// </summary>
        /// <param name="idPersonal">Id del personal </param>
        /// <returns> Objeto de clase ReporteSeguimientoOportunidadCombosDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosOperaciones(int idPersonal)
        {
            try
            {
                var servicioCentroCosto = new CentroCostoService(unitOfWork);
                var servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                var servicioPersonal = new PersonalService(unitOfWork);
                var servicioEstadoMatricula = new EstadoMatriculaService(unitOfWork);
                var resultado = new ReporteSeguimientoOportunidadCombosDTO();

                resultado.CentroCostos = servicioCentroCosto.ObtenerCombo().ToList();
                resultado.FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList();
                resultado.Asesores = servicioPersonal.PersonalAsignadoOperacionesTotal(idPersonal);
                resultado.Estados = servicioEstadoMatricula.ObtenerEstadoMatriculaCombo().ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los centros de costos por el personal
        /// </summary>
        /// <param name="idsAsesor">Id de los asesores </param>
        /// <returns> List<ComboDTO> </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPersonal([FromBody] ListadoIdDTO idsAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioCentroCosto = new CentroCostoService(unitOfWork);
                string asesores = string.Join(",", idsAsesor.Ids);
                var resultado = servicioCentroCosto.ObtenerCentroCostoPorAsesores(idsAsesor.Ids);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 13/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene le reporte de seguimiento oportunidad por filtros
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadesDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = new List<ReporteSeguimientoOportunidadDTO>();
                resultado = servicioReporte.ReporteSeguimientoOportunidad(filtros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte solicitudes de visualizacion
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteSolicitudesVisualizacion([FromBody] ReporteSolicitudesVisualizacionFiltroDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = new List<ReporteSeguimientoOportunidadDTO>();
                resultado = servicioReporte.GenerarReporteSolicitudesVisualizacion(filtros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte solicitudes de visualizacion de operaciones
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> List<ReporteSeguimientoOportunidadDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteSolicitudesVisualizacionOperaciones([FromBody] ReporteSolicitudesVisualizacionFiltroDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = new List<ReporteSeguimientoOportunidadDTO>();
                resultado = servicioReporte.GenerarReporteSolicitudesVisualizacionOperaciones(filtros);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Aprueba solicitudes de visualizacion
        /// </summary>
        /// <param name="aprobacionFiltro"> Filtros de aprobacion </param>
        /// <returns> IntDTO.Valor = 1 ó 0  </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarSolicitudVisualizacion([FromBody] AprobacionSolicitudesVisualizacionFiltroDTO aprobacionFiltro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = servicioReporte.AprobacionSolicitudVisualizacion(aprobacionFiltro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 14/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de log de la oportunidad
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerListaOportunidadLog(int idOportunidad)
        {
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var servicioBloqueHorario = new BloqueHorarioService(unitOfWork);

                var fechas = servicioReporte.ObtenerActividadesNoEjecutadas(idOportunidad);
                var bloques = servicioBloqueHorario.ObtenerCombo().Select(y => new BloqueHorarioProcesarBicDTO
                {
                    Nombre = y.Nombre,
                    HoraInicio = y.HoraInicio,
                    HoraFin = y.HoraFin
                }).ToList();
                foreach (var bloque in bloques)
                {
                    bloque.Contador = 0;
                }
                var nombreTurnoUltimo = string.Empty;
                DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                foreach (var fecha in fechas)
                {
                    TimeSpan horaFecha = fecha.TimeOfDay;

                    foreach (var bloque in bloques)
                    {
                        if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                        {
                            if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                            else
                            {
                                nombreTurnoUltimo = bloque.Nombre;
                                fechaUltima = fecha.Date;
                                bloque.Contador++;
                                break;
                            }
                        }
                    }
                }
                return Ok(new { log = servicioReporte.ObtenerOportunidadesLog(idOportunidad), bloques = bloques });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 19/10/2022
        /// AutorModificacion: Flavio R. Mamani Fabian
        /// FechaModificacion: 21/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica la url de la llamada
        /// </summary>
        /// <returns>Url del archivo Generado</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ModificarLlamadaWebphone([FromForm] EditarActividadLlamadaDTO obj)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
                    return Ok(servicio.ModificarLlamadaWebphone(obj, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }

        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta una nueva llamada en la actividad
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarNuevaLlamadaActividad([FromForm] NuevaLlamadaActividadDTO obj)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
                    var resultado = servicio.GenerarNuevaLlamadaActividad(obj, _respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(new
                    {
                        resultado.IdLlamadaWebphoneAsterisk,
                        resultado.IdLlamadaWebphoneCruceCentral,
                        resultado.url
                    });
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }
        /// Tipo Función: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta una nueva llamada en la actividad
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[Action]/{idAlumno}/{idPersonalAsignado}")]
        [HttpGet]
        public ActionResult ObtenerDatosNuevaLlamada(int idAlumno, int idPersonalAsignado)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
                    return Ok(servicio.ObtenerDatosNuevaLlamada(idAlumno, idPersonalAsignado));
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos del reporte se seguimiento
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de clase ReporteSeguimientoOportunidadCombosDTO</returns>
        [Route("[Action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteSeguimiento(int idPersonal)
        {
            try
            {
                CentroCostoService servicioCentroCosto = new CentroCostoService(unitOfWork);
                FaseOportunidadService servicioFaseOportunidad = new FaseOportunidadService(unitOfWork);
                PersonalService servicioPersonal = new PersonalService(unitOfWork);

                ReporteSeguimientoOportunidadCombosDTO resultado = new ReporteSeguimientoOportunidadCombosDTO();
                resultado.CentroCostos = servicioCentroCosto.ObtenerCombo().ToList();
                resultado.FaseOportunidades = servicioFaseOportunidad.ObtenerCombo().ToList();
                resultado.Asesores = idPersonal == 213 ? servicioPersonal.ObtenerAsesoresVentasOficialReporteSeguimiento() : servicioPersonal.ObtenerPersonalAsignadoVentas(idPersonal);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de la oportunidad por su Id y el IdAlumno
        /// </summary>
        /// <param name="idOportunidad"> Id de la Oportunidad </param>
        /// <param name="idAlumno"> Id del Alumno </param>
        /// <returns>Objeto de clase ReporteSeguimientoOportunidadCombosDTO</returns>
        [Route("[action]/{idOportunidad}/{idAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInformacionOportunidad(int idOportunidad, int idAlumno)
        {
            try
            {
                var oportunidadService = new OportunidadService(unitOfWork);
                var oportunidadInformacionService = new OportunidadInformacionService(unitOfWork);
                var alumnoService = new AlumnoService(unitOfWork);
                var sentinelService = new SentinelService(unitOfWork);
                var personalService = new PersonalService(unitOfWork);
                var reporteOpotunidadDetalleDTO = new ReporteOportunidadDetalleDTO();
                oportunidadInformacionService.oportunidadInformacionBoDTO = new OportunidadInformacionBoDTO();

                reporteOpotunidadDetalleDTO.listaOportunidadVentaCruzada = oportunidadService.ObtenerHistorialOportunidadesReporte(idAlumno); // jrivera
                
                var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);
                var idClasificacionPersona = oportunidad.IdClasificacionPersona.Value;

                reporteOpotunidadDetalleDTO.datosAlumno = alumnoService.ObtenerPorIdClasificacionPersona(idClasificacionPersona);
                reporteOpotunidadDetalleDTO.datosAlumno.CelularOriginal = reporteOpotunidadDetalleDTO.datosAlumno.Celular;
                reporteOpotunidadDetalleDTO.datosAlumno.EmailOriginal = reporteOpotunidadDetalleDTO.datosAlumno.Email1;


                //ENCRIPTAR EMIAL Y NRO
                reporteOpotunidadDetalleDTO.datosAlumno.Email1 = alumnoService.EncriptarCorreoHash(reporteOpotunidadDetalleDTO.datosAlumno.Email1 == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Email1);
                reporteOpotunidadDetalleDTO.datosAlumno.Email2 = alumnoService.EncriptarCorreoHash(reporteOpotunidadDetalleDTO.datosAlumno.Email2 == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Email2);
                reporteOpotunidadDetalleDTO.datosAlumno.Telefono = alumnoService.EncriptarNumeroHash(Regex.Replace(reporteOpotunidadDetalleDTO.datosAlumno.Telefono == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Telefono, @"[^\d]", ""));
                reporteOpotunidadDetalleDTO.datosAlumno.Telefono2 = alumnoService.EncriptarNumeroHash(Regex.Replace(reporteOpotunidadDetalleDTO.datosAlumno.Telefono2 == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Telefono2, @"[^\d]", ""));
                reporteOpotunidadDetalleDTO.datosAlumno.Celular = alumnoService.EncriptarNumeroHash(Regex.Replace(reporteOpotunidadDetalleDTO.datosAlumno.Celular == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Celular, @"[^\d]", ""));
                reporteOpotunidadDetalleDTO.datosAlumno.Celular2 = alumnoService.EncriptarNumeroHash(Regex.Replace(reporteOpotunidadDetalleDTO.datosAlumno.Celular2 == null ? "" : reporteOpotunidadDetalleDTO.datosAlumno.Celular2, @"[^\d]", ""));

                //FIN ENCRIPTAR EMIAL Y NRO
               
                reporteOpotunidadDetalleDTO.datosAlumno.CodigoMatricula = sentinelService.ObtenerCodigoMatricula(idOportunidad);

                if (reporteOpotunidadDetalleDTO.datosAlumno.IdCargo == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdCargo = 11;
                if (reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria = 48;

                if (reporteOpotunidadDetalleDTO.datosAlumno.DNI == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.DNI = "";

                reporteOpotunidadDetalleDTO.probabilidadsueldo = sentinelService.ObtenerPromedioSueldo(reporteOpotunidadDetalleDTO.datosAlumno.IdEmpresa, reporteOpotunidadDetalleDTO.datosAlumno.DNI, reporteOpotunidadDetalleDTO.datosAlumno.IdCargo, reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria);
                oportunidadInformacionService.CargarPrerequisitosBeneficios(idOportunidad);
                reporteOpotunidadDetalleDTO.ProgramaGeneralPreBen = oportunidadInformacionService.oportunidadInformacionBoDTO.ProgramaGeneralPreBen;
                reporteOpotunidadDetalleDTO.ListaProblemaCliente = oportunidadService.ObtenerOportunidadProblemasCliente(idOportunidad);
                reporteOpotunidadDetalleDTO.OportunidadComplementos = oportunidadService.ObtenerInformacionComplementariaReporteSeguimiento(idOportunidad);

                reporteOpotunidadDetalleDTO.idFaseOportunidad = oportunidad.IdFaseOportunidad;
                reporteOpotunidadDetalleDTO.idActividadDetalle = oportunidad.IdActividadDetalleUltima == null ? 0 : oportunidad.IdActividadDetalleUltima.Value;

                try
                {
                    var personal = personalService.ObtenerPorId(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);
                    if (personal != null)
                    {
                        reporteOpotunidadDetalleDTO.nombresPersonal = personal.Nombres == null ? "" : personal.Nombres;
                        reporteOpotunidadDetalleDTO.apellidosPersonal = personal.Apellidos == null ? "" : personal.Apellidos;
                    }
                }
                catch (Exception ex) { }

                return Ok(reporteOpotunidadDetalleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de los Alumnos filtrado por el IdReferido
        /// </summary> 
        /// <param name="idReferido"> Id del referido </param>
        /// <returns> List<AlumnoReferidosDTO> </returns>
        [Route("[action]/{idReferido}")]
        [HttpGet]
        public ActionResult ObtenerReferidos(int idReferido)
        {
            try
            {
                var alumnoService = new AlumnoService(unitOfWork);
                return Ok(alumnoService.ObtenerReferidos(idReferido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene informacion de las interaccion del alumno por el IdAlumno
        /// </summary> 
        /// <param name="idAlmuno"> Id del alumno </param>
        /// <returns> List<AlumnoReferidosDTO> </returns>
        [Route("[action]/{idAlmuno}")]
        [HttpGet]
        public ActionResult ObtenerInteraccionesAlumno(int idAlmuno)
        {
            try
            {
                var interaccionPaginaRepositorio = new InteraccionPaginaService(unitOfWork);
                return Ok(interaccionPaginaRepositorio.ObtenerInteraccionesPorAlumno(idAlmuno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Gilmer Quispe
        /// Fecha: 27/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las oportunidades anteriores del alumno por el IdAlumno
        /// </summary> 
        /// <param name="idAlmuno"> Id del alumno </param>
        /// <returns> List<OportunidadAnteriorDTO></returns>
        [Route("[action]/{idAlmuno}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesAnteriores(int idAlmuno)
        {
            try
            {
                var oportunidadService = new OportunidadService(unitOfWork);
                return Ok(oportunidadService.ObtenerOportunidadesAnterioresAlumno(idAlmuno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de reporte seguimiento operaciones.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteSeguimientoOperaciones(int idPersonal)
        {
            try
            {
                CentroCostoService centroCostoService = new CentroCostoService(unitOfWork);
                FaseOportunidadService faseOportunidadService = new FaseOportunidadService(unitOfWork);
                PersonalService personalService = new PersonalService(unitOfWork);
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
                ReporteSeguimientoInscritoComboDTO result = new ReporteSeguimientoInscritoComboDTO();
                result.CentroCostos = centroCostoService.ObtenerCombo().ToList();
                result.FaseOportunidades = faseOportunidadService.ObtenerFaseOportunidadTodoFiltro();
                result.Asesores = personalService.ObtenerPersonalAsignadoOperaciones(idPersonal);
                result.Estados = matriculaCabeceraService.ObtenerEstadosMatricula();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reportes de operaciones.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteOperaciones([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteService = new ReporteService(unitOfWork);
                List<ReporteSeguimientoOportunidadesOperacionesDTO> resultado = new List<ReporteSeguimientoOportunidadesOperacionesDTO>();
                resultado = reporteService.ObtenerReporteSeguimientoOportunidadOperaciones(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Joseph Llanque
        /// Fecha: 03/10/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reportes de operaciones de inscritos en instituto.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteSeguimientoInscritosCarreraOperaciones([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteService = new ReporteService(unitOfWork);
                List<ReporteInscritosCarreraOperacionesDTO> resultado = new List<ReporteInscritosCarreraOperacionesDTO>();
                resultado = reporteService.ObtenerReporteSeguimientoInscritosCarreraOperaciones(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte de probabilidad.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteProbabilidad([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteService = new ReporteService(unitOfWork);
                List<ReporteSeguimientoOportunidadesModeloDTO> result = new List<ReporteSeguimientoOportunidadesModeloDTO>();
                result = reporteService.ObtenerReporteSeguimientoOportunidadProbabilidad(filtro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera el reporte de fecha de creacion de registro tanto para creacion de oportunidad o de la campania
        /// </summary>
        /// <returns>ActionResult con estado 200 con lista de objetos de clase ReporteSeguimientoOportunidadesDTO, caso contrario 400</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteFechaCreacionRegistro([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReporteService reporteService = new ReporteService(unitOfWork);
                List<ReporteSeguimientoOportunidadDTO> resultado = new List<ReporteSeguimientoOportunidadDTO>();
                if (filtro.TipoFecha == 1)//fecha creación
                {
                    resultado = reporteService.ObtenerReporteSeguimientoOportunidadFC(filtro);
                }
                if (filtro.TipoFecha == 2)//fecha registro campania
                {
                    resultado = reporteService.ObtenerReporteSeguimientoOportunidadFRC(filtro);
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 16/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene información por las operaciones de las oportunidades
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idAlumno"></param>
        /// <returns> DTO: reporteOpotunidadDetalleDTO </returns>
        [Route("[action]/{IdOportunidad}/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInformacionOportunidadOperaciones(int idOportunidad, int idAlumno)
        {
            try
            {
                OportunidadService oportunidadRepositorio = new OportunidadService(unitOfWork);
                OportunidadInformacionService oportunidadInformacionService = new OportunidadInformacionService(unitOfWork);
                AlumnoService alumnoService = new AlumnoService(unitOfWork);
                SentinelService Sentinel = new SentinelService(unitOfWork);
                ReporteOportunidadDetalleDTO reporteOpotunidadDetalleDTO = new ReporteOportunidadDetalleDTO();

                reporteOpotunidadDetalleDTO.listaOportunidadVentaCruzada = oportunidadRepositorio.ObtenerHistorialOportunidadesReporte(idAlumno); // jrivera
                var idClasificacionPersona = oportunidadRepositorio.ObtenerPorId(idOportunidad).IdClasificacionPersona.Value;
                reporteOpotunidadDetalleDTO.datosAlumno = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);

                if (reporteOpotunidadDetalleDTO.datosAlumno.IdCargo == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdCargo = 11;
                if (reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria = 48;
                if (reporteOpotunidadDetalleDTO.datosAlumno.DNI == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.DNI = "";

                oportunidadInformacionService.oportunidadInformacionBoDTO = new OportunidadInformacionBoDTO(); //Instanciando

                reporteOpotunidadDetalleDTO.probabilidadsueldo = Sentinel.ObtenerPromedioSueldo(reporteOpotunidadDetalleDTO.datosAlumno.IdEmpresa, reporteOpotunidadDetalleDTO.datosAlumno.DNI, reporteOpotunidadDetalleDTO.datosAlumno.IdCargo, reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria);
                oportunidadInformacionService.CargarPrerequisitosBeneficios(idOportunidad);
                reporteOpotunidadDetalleDTO.ProgramaGeneralPreBen = oportunidadInformacionService.oportunidadInformacionBoDTO.ProgramaGeneralPreBen;
                reporteOpotunidadDetalleDTO.ListaProblemaCliente = oportunidadRepositorio.ObtenerOportunidadProblemasCliente(idOportunidad);
                reporteOpotunidadDetalleDTO.OportunidadComplementos = oportunidadRepositorio.ObtenerInformacionComplementariaReporteSeguimiento(idOportunidad);
                return Ok(reporteOpotunidadDetalleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Dniel Huaita Caprio
        /// Fecha: 04/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene información por las operaciones de las oportunidades para los reportes
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: ReporteOportunidadOperacionesDetalleDTO </returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerDatosOportunidadOperaciones(int idOportunidad)
        {
            try
            {
                OportunidadService oportunidadRepositorio = new OportunidadService(unitOfWork);
                AlumnoService alumnoService = new AlumnoService(unitOfWork);
                ReporteOportunidadOperacionesDetalleDTO reporteOpotunidadDetalleDTO = new ReporteOportunidadOperacionesDetalleDTO();
                reporteOpotunidadDetalleDTO.OportunidadComplementos = oportunidadRepositorio.ObtenerInformacionComplementariaReporteSeguimiento(idOportunidad);
                var idClasificacionPersona = oportunidadRepositorio.ObtenerPorId(idOportunidad).IdClasificacionPersona.Value;
                //reporteOpotunidadDetalleDTO.datosAlumno = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);
                reporteOpotunidadDetalleDTO.datosAlumno = alumnoService.ObtenerInformacionAlumnoPorIdClasificacionPersonaOperaciones(idClasificacionPersona);
                reporteOpotunidadDetalleDTO.datosAlumno.IdCargo ??= 11;
                reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria ??= 48;
                reporteOpotunidadDetalleDTO.datosAlumno.DNI ??= "";
                return Ok(reporteOpotunidadDetalleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Dniel Huaita Caprio
        /// Fecha: 04/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene información por las operaciones de las oportunidades para los reportes
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: ReporteOportunidadOperacionesDetalleDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ActualizarCronogramaVersionFinal()
        {
            try
            {
                IReporteSeguimientoOportunidadService servicio = new ReporteSeguimientoOportunidadService(unitOfWork);
                var resultado = servicio.ActualizarCronogramaVersionFinal();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
