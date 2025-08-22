using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudOperacionesController
    /// Autor: Jashin Salazar Taco
    /// Fecha: 02/11/2022
    /// Controlador: SolicitudOperacionesController
    /// Autor: Jonathan Caipo
    /// Fecha: 31/10/2022
    /// <summary>
    /// Gestión de SolicitudOperaciones
    /// Gestión de Solicitud de Operaciones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudOperacionesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public SolicitudOperacionesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza solicitudes de Operaciones
        /// </summary>
        /// <param name="IdSolicitudOperaciones"></param>
        /// <param name="Usuario"></param>
        /// <param name="Observacion"></param>
        /// <returns></returns>
        [Route("[Action]/{idSolicitudOperaciones}/{usuario}/{observacion}")]
        [HttpGet]
        public ActionResult RealizadoSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.RealizadoSolicitudOperaciones(idSolicitudOperaciones, usuario, observacion);
                return Ok(new { respuesta.CodigoMatricula, respuesta.IdMatriculaCabecera });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza solicitudes de Operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudOperaciones(int idOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.ObtenerSolicitudOperaciones(idOportunidad);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cancela solicitudes de Operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        [Route("[Action]/{idSolicitudOperaciones}/{usuario}/{observacion}")]
        [HttpGet]
        public ActionResult CancelarSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.CancelarSolicitudOperaciones(idSolicitudOperaciones, usuario, observacion);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ReporteFiltro(filtroReporteDTO filtroSolicitudReporte )
        {
            try {

                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.ObtenerTodoFiltroOperaciones(filtroSolicitudReporte);
                return Ok(respuesta);
            }
                        
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
    }

}
        /// Tipo Función: GET
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza solicitudes de Operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoSolicitudOperaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.ObtenerTodoSolicitudOperaciones();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoSolicitud()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var respuesta = servicioSolicitudOperaciones.ObtenerTipoSolicitud();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las solicitudes de Operaciones Realizadas
        /// </summary>
        /// <returns>retorna lista de solicitudes realizadas: List<DatosSolicitudOperacionesDTO></returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudOperacionesRealizadas(int idOportunidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var resultado = servicioSolicitudOperaciones.ObtenerSolicitudOperacionesRealizadas(idOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el histiral de asesoras asignados al alumno
        /// </summary>
        /// <returns>retorna lista de solicitudes realizadas: List<DatosSolicitudOperacionesDTO></returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerHistorialAsesora(int IdMatriculaCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var resultado = servicioSolicitudOperaciones.ObtenerHistorialAsesora(IdMatriculaCabecera);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obitene Historial de Acceso Temporal
        /// </summary>
        /// <returns></returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerHistorialAccesoTemporal(int idOportunidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesService servicioSolicitudOperaciones = new SolicitudOperacionesService(_unitOfWork);
                var resultado = servicioSolicitudOperaciones.ObtenerHistorialAccesoTemporal(idOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Autor Modificacion: Daniel Huaita
        /// Fecha: 20/12/2022
        /// Version: 1.2
        /// <summary>
        /// Inserta diversas solicitudes de operaciones
        /// Se cambio el parametro de entrada de un fromBody por un fromForm
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudOperaciones([FromForm] SolicitudOperacionesDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesService solicitudOperacionesService = new SolicitudOperacionesService(_unitOfWork);
                var solicitudOperaciones = solicitudOperacionesService.InsertarSolicitudOperaciones(obj);
                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve la aprobación de la solicitud operaciones
        /// </summary>
        /// <returns></returns>
        [Route("[Action]/{idSolicitudOperaciones}/{usuario}/{idPersonal}")]
        [HttpGet]
        public ActionResult AprobarSolicitudOperaciones(int idSolicitudOperaciones, string usuario, int idPersonal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesService solicitudOperacionesService = new SolicitudOperacionesService(_unitOfWork);
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                MoodleCronogramaEvaluacionService moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                PlantillaOperacionesController plantillaOperacionesController = new PlantillaOperacionesController(_unitOfWork);

                var solicitudOperaciones = solicitudOperacionesService.ObtenerPorIdSolicitudOperaciones(idSolicitudOperaciones);

                solicitudOperaciones.Aprobado = true;
                solicitudOperaciones.FechaAprobacion = DateTime.Now;
                solicitudOperaciones.UsuarioModificacion = usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;

                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 4)/*Estado*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                    solicitudOperaciones.Realizado = true;

                    solicitudOperacionesService.Update(solicitudOperaciones);
                    matriculaCabeceraService.ActualizarEstadoMatriculaPorSolicitud(idSolicitudOperaciones, solicitudOperaciones.ValorNuevo);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 5)/*SubEstado*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                    solicitudOperaciones.Realizado = true;
                    solicitudOperacionesService.Update(solicitudOperaciones);
                    matriculaCabeceraService.ActualizarSubEstadoMatriculaPorSolicitud(idSolicitudOperaciones, solicitudOperaciones.ValorNuevo);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 6)/*6:Autoevaluaciones*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                    solicitudOperaciones.Realizado = true;

                    var compuesto = solicitudOperaciones.ObservacionEncargado.Split(",");
                    var respuesta = moodleCronogramaEvaluacionService.ReprogramarCronograma(Convert.ToInt32(compuesto[0]), Convert.ToInt32(compuesto[1]), Convert.ToInt32(compuesto[2]), Convert.ToBoolean(compuesto[3]), usuario);
                    if (respuesta.Estado == false)
                    {
                        return BadRequest(respuesta.Mensaje);
                    }
                    else
                    {
                        try
                        {
                            //var idPlantilla = 0;

                            IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                            var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                            {
                                IdOportunidad = solicitudOperaciones.IdOportunidad,
                                IdPlantilla = 1226 //Información Cronograma de Autoevaluación - Aonline
                            });
                            //envio correo
                            var oportunidad = oportunidadService.ObtenerEmailPorOportunidad(solicitudOperaciones.IdOportunidad);

                            List<string> correosPersonalizados = new List<string>
                            {
                                oportunidad.EmailAlumno
                            };

                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = oportunidad.EmailPersonal,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = resultadoReemplazo.EmailReemplazado.Asunto,
                                Message = resultadoReemplazo.EmailReemplazado.CuerpoHTML,
                                Cc = "",
                                Bcc = "",//"fvaldez@bsginstitute.com",//string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                AttachedFiles = resultadoReemplazo.EmailReemplazado.ListaArchivosAdjuntos,
                            };
                            var mailServie = new TMK_MailService();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    solicitudOperacionesService.Update(solicitudOperaciones);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 7)/*7: FechaFinalizacion*/
                {
                    var idMatriculaCabecera = solicitudOperacionesService.ObtenerMatriculaPorOportunidad(solicitudOperaciones.IdOportunidad);
                    var matriculaCabeceraEntidad = matriculaCabeceraService.ObtenerPorId(idMatriculaCabecera);

                    solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                    solicitudOperaciones.Realizado = true;

                    matriculaCabeceraEntidad.FechaFinalizacion = Convert.ToDateTime(solicitudOperaciones.ValorNuevo);
                    matriculaCabeceraEntidad.UsuarioModificacion = usuario;
                    matriculaCabeceraEntidad.FechaModificacion = DateTime.Now;

                    matriculaCabeceraService.Update(matriculaCabeceraEntidad);
                    solicitudOperacionesService.Update(solicitudOperaciones);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 8)/*8: Acceso Temporal*/
                {
                    try
                    {
                        solicitudOperacionesService.RegistrarCursoPrueba(idSolicitudOperaciones);

                        solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                        solicitudOperaciones.FechaAprobacion = DateTime.Now;
                        solicitudOperaciones.Observacion = "Aprobado";
                        solicitudOperaciones.Aprobado = true;
                        solicitudOperaciones.Realizado = true;

                        solicitudOperacionesService.Update(solicitudOperaciones);

                        try
                        {
                            // Mailing
                            IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                            var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                            {
                                IdOportunidad = solicitudOperaciones.IdOportunidad,
                                IdPlantilla = ValorEstatico.IdPlantillaAccesoTemporalMailing
                            });
                            //Envio correo
                            var oportunidad = oportunidadService.ObtenerEmailPorOportunidad(solicitudOperaciones.IdOportunidad);

                            List<string> correosPersonalizados = new List<string>
                            {
                                oportunidad.EmailAlumno
                            };

                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = oportunidad.EmailPersonal,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = resultadoReemplazo.EmailReemplazado.Asunto,
                                Message = resultadoReemplazo.EmailReemplazado.CuerpoHTML,
                                Cc = "",
                                Bcc = "gmiranda@bsginstitute.com",
                                AttachedFiles = resultadoReemplazo.EmailReemplazado.ListaArchivosAdjuntos,
                            };
                            var mailServie = new TMK_MailService();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                        }
                        catch (Exception e)
                        {
                            List<string> correos = new List<string>
                            {
                                "gmiranda@bsginstitute.com"
                            };
                            TMK_MailService Mailservice = new TMK_MailService();
                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "gmiranda@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                                Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }

                        try
                        {
                            //plantillaOperacionesController.EnvioWhatsappNumeroIndividual(solicitudOperaciones.IdOportunidad, ValorEstatico.IdPlantillaAccesoTemporalWhatsApp, 1);/*Reemplazo de operaciones*/
                        }
                        catch (Exception e)
                        {
                            List<string> correos = new List<string>
                            {
                                "gmiranda@bsginstitute.com"
                            };
                            TMK_MailService Mailservice = new TMK_MailService();
                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "gmiranda@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                                Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                    }
                    catch (Exception e)
                    {
                        List<string> correos = new List<string>
                        {
                            "gmiranda@bsginstitute.com"
                        };
                        TMK_MailService Mailservice = new TMK_MailService();
                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "gmiranda@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                            Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };
                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 9)
                {
                    try
                    {
                        var curso = solicitudOperaciones.ObservacionEncargado;
                        solicitudOperacionesService.AprobarCambioCategoriaAlumno(solicitudOperaciones.IdOportunidad, solicitudOperaciones.ValorNuevo);

                        solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                        solicitudOperaciones.FechaAprobacion = DateTime.Now;
                        solicitudOperaciones.Observacion = "Aprobado";
                        solicitudOperaciones.Aprobado = true;
                        solicitudOperaciones.Realizado = true;

                        solicitudOperacionesService.Update(solicitudOperaciones);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 10)
                {
                    try
                    {
                        var curso = solicitudOperaciones.ObservacionEncargado;
                        solicitudOperacionesService.AmpliacionAccesosTemporales(solicitudOperaciones.IdOportunidad, solicitudOperaciones.ValorNuevo, curso);

                        solicitudOperaciones.IdPersonalAprobacion = idPersonal;
                        solicitudOperaciones.FechaAprobacion = DateTime.Now;
                        solicitudOperaciones.Observacion = "Aprobado";
                        solicitudOperaciones.Aprobado = true;
                        solicitudOperaciones.Realizado = true;

                        solicitudOperacionesService.Update(solicitudOperaciones);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else
                {
                    solicitudOperacionesService.Update(solicitudOperaciones);
                }
                var IdMatricula = solicitudOperacionesService.ObtenerMatriculaPorOportunidad(solicitudOperaciones.IdOportunidad);
                var matriculaCabecera = matriculaCabeceraService.ObtenerPorId(IdMatricula);

                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 5 || solicitudOperaciones.IdTipoSolicitudOperaciones == 4)
                {
                    if (matriculaCabecera.IdEstadoMatricula == 5 &&
                        (
                        matriculaCabecera.IdSubEstadoMatricula == 1 ||
                        matriculaCabecera.IdSubEstadoMatricula == 5 ||
                        matriculaCabecera.IdSubEstadoMatricula == 6 ||
                        matriculaCabecera.IdSubEstadoMatricula == 43)
                        )
                    {
                        IntegraAspNetUserService integraAspNetUserService = new IntegraAspNetUserService(_unitOfWork);
                        UsuarioService usuarioService = new UsuarioService(_unitOfWork);

                        DatosRegistroEnvioFisicoDTO datosRegistroEnvioFisicoDTO = new DatosRegistroEnvioFisicoDTO();
                        datosRegistroEnvioFisicoDTO.IdMatriculaCabecera = matriculaCabecera.Id;
                        datosRegistroEnvioFisicoDTO.IdAlumno = matriculaCabecera.IdAlumno;
                        var datosalumno = alumnoService.ObtenerDatosAlumnoPorId(datosRegistroEnvioFisicoDTO.IdAlumno);
                        datosRegistroEnvioFisicoDTO.Nombre = datosalumno.Nombre1 + " " + datosalumno.Nombre2;
                        var correoAlumno = alumnoService.ObtenerDatosAlumnoPorId(datosRegistroEnvioFisicoDTO.IdAlumno);
                        var usuarioCoordinadora = matriculaCabeceraService.ObtenerIdAlumnoCoordinadorAcademico(datosRegistroEnvioFisicoDTO.IdMatriculaCabecera);
                        var correoCoordinadora = integraAspNetUserService.ObtenerPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        //var idPlantilla = 1391;
                        var idPlantilla = 1453;

                        IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);

                        var user = usuarioService.ObtenerTotalPorUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        if (user != null)
                        {
                            datosRegistroEnvioFisicoDTO.IdPersonal = user.IdPersonal;
                        }
                        var emailCalculado = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFisico(datosRegistroEnvioFisicoDTO, idPlantilla).EmailReemplazado;

                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            "lhuallpa@bsginstitute.com",
                            "mmora@bsginstitute.com"
                        };

                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = correoCoordinadora.Email,
                            Recipient = correoAlumno.Email1,
                            Subject = emailCalculado.Asunto,
                            Message = emailCalculado.CuerpoHTML,
                            Cc = "",
                            Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                        };
                        var mailServie = new TMK_MailService();

                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();

                        ValidarNumerosWhatsAppDTO contact = new ValidarNumerosWhatsAppDTO();
                        contact.contacts = new List<string>();
                        var alumno = alumnoService.ObtenerPorId(datosRegistroEnvioFisicoDTO.IdAlumno);
                        var alumnoNumero = alumnoService.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                        contact.contacts.Add("+" + alumnoNumero);

                        var respuestaw = plantillaOperacionesController.Envio(correoAlumno.Email1, matriculaCabecera.CodigoMatricula, alumnoNumero, 1461);
                    };
                }
                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Miguel Quiñones
        /// Fecha: 15/02/2023
        /// Version: 1
        /// <summary>
        /// envia 
        /// </summary>
        /// <returns></returns>


        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerConfirmacionSolicitudes(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                SolicitudOperacionesService solicitudOperacionesService = new SolicitudOperacionesService(_unitOfWork);
                var rpta = solicitudOperacionesService.ObtenerConfirmacionSolicitudes(Id);
                return Ok(rpta);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






        /// Tipo Función: GET
        /// Autor: Miguel Quiñones
        /// Fecha: 09/02/2023
        /// Version: 1
        /// <summary>
        /// envia 
        /// </summary>
        /// <returns></returns>


        [Route("[action]/{remitente}/{codigoAlumno}/{destinatarios}/{idPlantilla}")]
        [HttpGet]
        public ActionResult Envio(string remitente, string codigoAlumno, string destinatarios, int idPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {


                //SolicitudOperacionesService envioPlantillaOperaciones = new SolicitudOperacionesService(unitOfWork);

                //var envioRespuesta = envioPlantillaOperaciones.envioPlantillaOperaciones(string Remitente, string CodigoAlumno, string Destinatarios, int IdPlantilla);
                PlantillaOperacionesService plantillaOperacionesService = new PlantillaOperacionesService(_unitOfWork);
                var respuesta = plantillaOperacionesService.Envio(remitente, codigoAlumno, destinatarios, idPlantilla);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}