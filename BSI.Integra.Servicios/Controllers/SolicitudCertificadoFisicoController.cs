using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudCertificadoFisicoController
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión de Solicitud Certificado Físico
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SolicitudCertificadoFisicoController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public SolicitudCertificadoFisicoController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el estado del certificdo físico
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoCertificadoFisico()
        {
            try
            {
                EstadoCertificadoFisicoService servicioEstadoCertificadoFisico = new EstadoCertificadoFisicoService(_unitOfWork);
                var filtro = new
                {
                    EstadoCertificadoFisico = servicioEstadoCertificadoFisico.ObtenerEstadParaFiltro()
                };

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerSolicitudCertificadoFisico([FromBody] filtroSolicitudCertificadoFisicoDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ISolicitudCertificadoFisicoService solicitudCertificadoFisicoService = new SolicitudCertificadoFisicoService(_unitOfWork);

                var rpta = solicitudCertificadoFisicoService.ObtenerSolicitudesCertificadoFisico(json);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdCertificadoGeneradoAutomatico}/{IdSolicitudCertificadoFisico}/{Usuario}")]
        [HttpGet]
        public ActionResult GenerarCertificadoFisico(int IdCertificadoGeneradoAutomatico, int IdSolicitudCertificadoFisico, string Usuario)
        {
            ICertificadoDetalleService certificadoDetalleService = new CertificadoDetalleService(_unitOfWork);
            ICertificadoGeneradoAutomaticoContenidoService certificadoGeneradoAutomaticoContenidoService = new CertificadoGeneradoAutomaticoContenidoService(_unitOfWork);
            IRegistroCertificadoFisicoGeneradoService registroCertificadoFisicoGeneradoService = new RegistroCertificadoFisicoGeneradoService(_unitOfWork);
            ISolicitudCertificadoFisicoService solicitudCertificadoFisicoService = new SolicitudCertificadoFisicoService(_unitOfWork);

            var dato = certificadoGeneradoAutomaticoContenidoService.ObtenerDatosParaCertificadoFisico(IdCertificadoGeneradoAutomatico);
            string urlDocumento = "";
            foreach (var item in dato)
            {
                try
                {
                    var registroCertificadoFisicoGenerado = new RegistroCertificadoFisicoGenerado();
                    var documentoService = new DocumentoService(_unitOfWork);

                    var documentoByte = documentoService.GenerarCertificadoSinFondo(item);

                    urlDocumento = certificadoDetalleService.GuardarArchivoCertificadoFisico(documentoByte, "application/pdf", item.CodigoCertificado + "IMP");

                    registroCertificadoFisicoGenerado.IdSolicitudCertificadoFisico = IdSolicitudCertificadoFisico;
                    registroCertificadoFisicoGenerado.IdUrlBlockStorage = 11;/*Certificado Fisico*/
                    registroCertificadoFisicoGenerado.FormatoArchivo = "application/pdf";
                    registroCertificadoFisicoGenerado.NombreArchivo = item.CodigoCertificado + "IMP";
                    registroCertificadoFisicoGenerado.UltimaFechaGeneracion = DateTime.Now;
                    registroCertificadoFisicoGenerado.Estado = true;
                    registroCertificadoFisicoGenerado.FechaCreacion = DateTime.Now;
                    registroCertificadoFisicoGenerado.FechaModificacion = DateTime.Now;
                    registroCertificadoFisicoGenerado.UsuarioCreacion = Usuario;
                    registroCertificadoFisicoGenerado.UsuarioModificacion = Usuario;

                    registroCertificadoFisicoGeneradoService.Add(registroCertificadoFisicoGenerado);

                    var solicitudCertificado = solicitudCertificadoFisicoService.ObtenerPorId(IdSolicitudCertificadoFisico);

                    if (solicitudCertificado.IdEstadoCertificadoFisico == 1)/*1:Pendiente de Envio*/
                    {
                        solicitudCertificado.IdEstadoCertificadoFisico = 2; /*2:Impreso Pendiente de envio*/
                        solicitudCertificado.FechaModificacion = DateTime.Now;
                        solicitudCertificado.UsuarioModificacion = Usuario;

                        solicitudCertificadoFisicoService.Update(solicitudCertificado);
                    }


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return Ok(new { urlDocumento });
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            try
            {
                IPersonalService personalService = new PersonalService(_unitOfWork);
                IEstadoCertificadoFisicoService estadoCertificadoFisicoService = new EstadoCertificadoFisicoService(_unitOfWork);
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);


                var filtro = new
                {
                    ListaCoordinadores = personalService.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal),
                    ListaEstadoCertificadoFisico = estadoCertificadoFisicoService.ObtenerEstadParaFiltro(),
                    ListaMatriculaCabecera = matriculaCabeceraService.ObtenerCombo()
                };

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 25/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que envia correos para la solicitud de envio de certificados fisicos
        /// </summary>
        /// <param name="Id">Id de un registro de la tabla mkt.T_SolicitudCertificadoFisico</param>
        /// <returns>Objeto del tipo DatosRegistroEnvioFisico</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDatosRegistroEnvioFisico(int Id)
        {
            try
            {
                ISolicitudCertificadoFisicoService solicitudCertificadoFisicoService = new SolicitudCertificadoFisicoService(_unitOfWork);

                var rpta = solicitudCertificadoFisicoService.DatosRegistroEnvioFisico(Id);
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarDatosCourierEnvioFisico([FromBody] SolicitudCertificadoFisicoDTO json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ISolicitudCertificadoFisicoService solicitudCertificadoFisicoService = new SolicitudCertificadoFisicoService(_unitOfWork);

                SolicitudCertificadoFisico solicitudCertificadoFisico = solicitudCertificadoFisicoService.ObtenerPorId(json.Id);

                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                IFurService furService = new FurService(_unitOfWork);
                ICourierService courierService = new CourierService(_unitOfWork);
                ICourierDetalleService courierDetalleService = new CourierDetalleService(_unitOfWork);
                IIntegraAspNetUserService integraAspNetUserService = new IntegraAspNetUserService(_unitOfWork);
                IUsuarioService usuarioService = new UsuarioService(_unitOfWork);
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                var _PlantillaOperacionesController = new PlantillaOperacionesController(_unitOfWork);

                if (json.IdFur != null)
                {
                    solicitudCertificadoFisico.IdFur = json.IdFur;
                    solicitudCertificadoFisico.IdProveedor = json.IdProveedor;
                }
                if (json.IdEstadoCertificadoFisico != 0)
                {
                    solicitudCertificadoFisico.IdEstadoCertificadoFisico = json.IdEstadoCertificadoFisico;
                    if (solicitudCertificadoFisico.IdEstadoCertificadoFisico == 4)
                    {
                        DatosRegistroEnvioFisicoDTO datos = new DatosRegistroEnvioFisicoDTO();
                        var matricula = matriculaCabeceraService.ObtenerPorId(solicitudCertificadoFisico.IdMatriculaCabecera);
                        var courier = courierService.ObtenerPorId((int)solicitudCertificadoFisico.IdCourier);
                        datos.IdMatriculaCabecera = matricula.Id;
                        datos.IdAlumno = matricula.IdAlumno;
                        datos.Courier = courier.Nombre;
                        datos.CodigoSeguimiento = solicitudCertificadoFisico.CodigoSeguimiento;
                        var correoAlumno = alumnoService.ObtenerDatosAlumnoPorId(datos.IdAlumno);
                        
                        var usuarioCoordinadora = matriculaCabeceraService.ObtenerIdAlumnoCoordinadorAcademico(datos.IdMatriculaCabecera);
                        var correoCoordinadora = integraAspNetUserService.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        var user = usuarioService.ObtenerPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        if (user != null)
                        {
                            datos.IdPersonal = user.IdPersonal;
                        }

                        var idPlantilla = 1455;
                        var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);

                        var emailCalculado = _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFisico(datos, idPlantilla).EmailReemplazado;
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            "lhuallpa@bsginstitute.com",
                            "mmora@bsginstitute.com",
                        };


                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = correoCoordinadora,
                            //Sender = personal.Email,
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

                        ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                        contact.contacts = new List<string>();
                        var alumno = alumnoService.ObtenerPorId(datos.IdAlumno);
                        var alumnoNumero = alumnoService.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                        contact.contacts.Add("+" + alumnoNumero);

                        var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matricula.CodigoMatricula, alumnoNumero, 1463);
                    }
                }
                if (json.CodigoSeguimientoEnvio != null)
                {
                    solicitudCertificadoFisico.CodigoSeguimientoEnvio = json.CodigoSeguimientoEnvio;
                }
                if (json.FechaEntregaReal != null)
                {
                    solicitudCertificadoFisico.FechaEntregaReal = json.FechaEntregaReal;
                }
                if (json.FechaEntregaEstimada != null)
                {

                    solicitudCertificadoFisico.FechaEntregaEstimada = json.FechaEntregaEstimada;
                }
                if (json.FechaEntregaCourier != null)
                {

                    solicitudCertificadoFisico.FechaEntregaCourier = json.FechaEntregaCourier;

                    if (solicitudCertificadoFisico.IdCourier != null && solicitudCertificadoFisico.IdPaisConsignado != null && solicitudCertificadoFisico.IdCiudadConsignada != null)
                    {
                        var CourierDetalle = courierDetalleService.ObtenerCourierDetallePorIdCourierYIdCiudad(solicitudCertificadoFisico.IdCourier.Value, solicitudCertificadoFisico.IdCiudadConsignada.Value);
                        if (CourierDetalle != null)
                        {
                            solicitudCertificadoFisico.FechaEntregaEstimada = ((DateTime)solicitudCertificadoFisico.FechaEntregaCourier).AddDays(CourierDetalle.TiempoEnvio);
                        }
                    }
                }
                if (json.EstadoCourier != null)
                {
                    solicitudCertificadoFisico.EstadoCourier = json.EstadoCourier;
                }
                if (json.IdPaisConsignado != null)
                {
                    if (solicitudCertificadoFisico.IdPaisConsignado != json.IdPaisConsignado)
                    {
                        solicitudCertificadoFisico.IdCiudadConsignada = null;

                    }
                    solicitudCertificadoFisico.IdPaisConsignado = json.IdPaisConsignado;
                }
                if (json.IdCiudadConsignada != null)
                {
                    solicitudCertificadoFisico.IdCiudadConsignada = json.IdCiudadConsignada;
                }
                if (json.CodigoSeguimiento != null)
                {
                    solicitudCertificadoFisico.CodigoSeguimiento = json.CodigoSeguimiento;
                }
                if (json.IdCourier != null)
                {
                    //if (solicitudCertificadoFisico.IdCourier != json.IdCourier)
                    //{
                    //    solicitudCertificadoFisico.IdPaisConsignado = null;
                    //    solicitudCertificadoFisico.IdCiudadConsignada = null;
                    //}
                    solicitudCertificadoFisico.IdCourier = json.IdCourier;
                }

                solicitudCertificadoFisico.FechaModificacion = DateTime.Now;
                solicitudCertificadoFisico.UsuarioModificacion = json.Usuario;

                var rpta = solicitudCertificadoFisicoService.Update(solicitudCertificadoFisico);

                if ((json.IdCourier != null || json.CodigoSeguimiento != null) && solicitudCertificadoFisico.IdCourier != null && solicitudCertificadoFisico.CodigoSeguimiento != null)
                {
                    DatosRegistroEnvioFisicoDTO datos = new DatosRegistroEnvioFisicoDTO();
                    var matricula = matriculaCabeceraService.ObtenerPorId(solicitudCertificadoFisico.IdMatriculaCabecera);
                    var courier = courierService.ObtenerPorId((int)solicitudCertificadoFisico.IdCourier);
                    datos.IdMatriculaCabecera = matricula.Id;
                    datos.IdAlumno = matricula.IdAlumno;
                    datos.Courier = courier.Nombre;
                    datos.CodigoSeguimiento = solicitudCertificadoFisico.CodigoSeguimiento;
                    var correoAlumno = alumnoService.ObtenerDatosAlumnoPorId(datos.IdAlumno);
                    var usuarioCoordinadora = matriculaCabeceraService.ObtenerIdAlumnoCoordinadorAcademico(datos.IdMatriculaCabecera);
                    var correoCoordinadora = integraAspNetUserService.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                    var user = usuarioService.ObtenerPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                    if (user != null)
                    {
                        datos.IdPersonal = user.IdPersonal;
                    }

                    var idPlantilla = 1454;
                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                    

                    var emailCalculado = _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFisico(datos, idPlantilla).EmailReemplazado;
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                        "mmora@bsginstitute.com",
                    };


                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = correoCoordinadora,
                        //Sender = personal.Email,
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

                    ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                    contact.contacts = new List<string>();
                    var alumno = alumnoService.ObtenerPorId(datos.IdAlumno);
                    var alumnoNumero = alumnoService.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                    contact.contacts.Add("+" + alumnoNumero);

                    var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matricula.CodigoMatricula, alumnoNumero, 1462);
                }


                return Ok(solicitudCertificadoFisico);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos del reporte de certificados físicos, según el CodigoMatricula
        /// </summary>
        /// <returns>List<CourierDetalleDTO><returns>
        [Route("[Action]/{codigoMatricula}")]
        [HttpGet]
        public ActionResult DatosReporteCertificadoEnvioFisicoPorId(string codigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudCertificadoFisicoService servicioReporteCertificadoFisico = new SolicitudCertificadoFisicoService(_unitOfWork);
                return Ok(servicioReporteCertificadoFisico.DatosReporteCertificadoEnvioFisicoPorId(codigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Joseph Llanque
        /// Fecha: 03/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos del reporte de certificados físicos, según el CodigoMatricula
        /// </summary>
        /// <returns>List<CourierDetalleDTO><returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudCertificadoFisicoService obtenerSolicitud = new SolicitudCertificadoFisicoService(_unitOfWork);
                return Ok(obtenerSolicitud.ObtenerPorIdMatriculaCabecera(idMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult obtenerdatosenvio(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudCertificadoFisicoService _repReporteCertificadoFisico = new SolicitudCertificadoFisicoService(_unitOfWork);
                return Ok(_repReporteCertificadoFisico.obtenerDatosEnvio(IdMatriculaCabecera));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] SolicitudCertificadoFisicoDTO json)
        {
            try
            {
                SolicitudCertificadoFisicoService _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoService(_unitOfWork);
                var respuesta = _repSolicitudCertificadoFisico.InsertarSolicitudCertificado(json);
                return Ok(respuesta);

               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarDatosEnvioOperaciones([FromBody] DatosEnvioAlumnoDTO json)
        {
            try
            {
                if (json != null)
                {
                    SolicitudCertificadoFisicoService _repReporteCertificadoFisico = new SolicitudCertificadoFisicoService(_unitOfWork);

                    return Ok(_repReporteCertificadoFisico.InsertarDatosEnviosOperaciones(json));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
