using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class WhatsAppEnvioAutomaticoService : IWhatsAppEnvioAutomaticoService
    {
        private IUnitOfWork _unitOfWork;
        private string token = string.Empty;
        private Mapper _mapper;
        public WhatsAppEnvioAutomaticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var objToken = _unitOfWork.AutenticacionServicioExternoRepository.ObtenerTokenpoId(1);//ValorEstatico.IdAutenticacionFacebookLeadsReportes
            token = objToken != null ? objToken.Valor : string.Empty;


        }

        private string urlBaseV12 = "https://graph.facebook.com/v20.0/";



        #region Metodos Base

        #endregion


        public bool EjecutarCampaniaGeneralEnvioWhatsApp()
        {

            try
            {
                var ListaDePrioridades = _unitOfWork.CampaniaGeneralRepository.ObtenerPrioridadesEnvioWhatsApp();
                if (ListaDePrioridades.Any())
                {

                    try
                    {
                        string Subject = "Inicio de Envio masivo " + ListaDePrioridades[0].NombreCampania + " Hora Inicio: " + ListaDePrioridades[0].HoraEnvio;
                        StringBuilder messageBuilder = new StringBuilder();

                        foreach (var Prioridad in ListaDePrioridades)
                        {
                            messageBuilder.Append("Campania-Prioridad: " + Prioridad.Nombre + " <br/>");
                            messageBuilder.Append("Hora Inicio : " + "Prioridad: " + Prioridad.Prioridad + " <br/>");
                            messageBuilder.Append("Asesor: " + Prioridad.Personal);

                            messageBuilder.Append("<br/><br/>");
                        }

                        string Message = messageBuilder.ToString();

                        this.EnvioCorreoMasivosMarketing(Subject, Message);
                    }
                    catch (Exception ex)
                    {

                    }

                    foreach (var Prioridad in ListaDePrioridades)
                    {
                        int RespuestaInsertarLog = 0;

                        try
                        {
                            try
                            {

                                List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> IdLogActivoParaDeleteLogico = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivoCampaniaGeneralDetalleResponsableWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                if (IdLogActivoParaDeleteLogico.Count() > 0)
                                {
                                    foreach (var Log in IdLogActivoParaDeleteLogico)
                                    {
                                        var RespuestaEliminarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.EliminarLog(Log.Id, "EliminarLogDuplicadoWhatsApp");

                                    }
                                }
                                RespuestaInsertarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarLogWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp, Prioridad.HoraEnvio, Prioridad.FechaInicioEnvioWhatsapp, "InsertarLogWhatsApp");
                                if (RespuestaInsertarLog == 0)
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                            var Hora_Inicio = DateTime.Now;

                            List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreRespuesta = new List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>();
                            PreRespuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.PreListaWhatsAppEnvioMasivo(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
                            ConfiguracionPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetalleDeCampaniaWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                            /* Inicio ejecucion envio */
                            if (PreRespuesta.Any())
                            {
                                WhatsAppConfiguracionEnvioDetalle EnvioMensajes = new WhatsAppConfiguracionEnvioDetalle();
                                //try
                                //{
                                //    string Subject = "Inicio de Envio masivo - Prioridad : " + ConfiguracionPre.Prioridad + " Hora Inicio: " + Hora_Inicio + " Asesor: " + ConfiguracionPre.Asesor;
                                //    string Message = "Campania-Prioridad: " + ConfiguracionPre.Nombre + " <br/>" + "Cantidad de contactos peocesados " + PreRespuesta.Count() + " <br/>" + " Hora Inicio : " + Hora_Inicio+ " <br/>" + "Prioridad: " + ConfiguracionPre.Prioridad + " <br/>" + " Campania: " + ConfiguracionPre.Nombre + " <br/>" + " Asesor: " + ConfiguracionPre.Asesor;
                                //    this.EnvioCorreoMasivosMarketing(Subject, Message);
                                //}
                                //catch {}
                                int CantidadEnviados = 0;
                                foreach (var item in PreRespuesta)
                                {

                                    var logsActivos = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.logsActivos(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                                    if (logsActivos.Count() == 1)
                                    {
                                        if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarEnvioDuplicado(item.CelularWhatsApp, item.Dias) == false)

                                        {
                                            //if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarDesuscritos(item.CelularWhatsApp) == false)
                                            //{

                                                var detalle = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetallePlantillaWhatsApp(item.IdPlantilla);
                                                var respuesta = new List<BotonDTO>();

                                                //validacion
                                                if (detalle.Count != 0 && !string.IsNullOrEmpty(detalle[0].Imagen))
                                                {

                                                    foreach (var value in detalle)
                                                    {
                                                        respuesta.Add(new BotonDTO { Nombre = value.Boton.Replace(" ", "") });
                                                    }
                                                    var Serializer = new JavaScriptSerializer();
                                                    RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                    MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                    if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                    {
                                                        item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                    }

                                                    List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                    objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                    foreach (var obj in objeto)
                                                    {
                                                        //Elimina los caracteres con tilde
                                                        obj.texto = obj.texto.Replace("á", "a");
                                                        obj.texto = obj.texto.Replace("é", "e");
                                                        obj.texto = obj.texto.Replace("í", "i");
                                                        obj.texto = obj.texto.Replace("ó", "o");
                                                        obj.texto = obj.texto.Replace("ú", "u");

                                                        obj.texto = obj.texto.Replace("Á", "A");
                                                        obj.texto = obj.texto.Replace("É", "E");
                                                        obj.texto = obj.texto.Replace("Í", "I");
                                                        obj.texto = obj.texto.Replace("Ó", "O");
                                                        obj.texto = obj.texto.Replace("Ú", "U");

                                                        //Elimina las Ñ
                                                        obj.texto = obj.texto.Replace("ñ", "n");
                                                        obj.texto = obj.texto.Replace("Ñ", "N");


                                                        //Elimina los caracteres con tilde
                                                        obj.texto = obj.texto.Replace("á", "a");
                                                        obj.texto = obj.texto.Replace("é", "e");
                                                        obj.texto = obj.texto.Replace("í", "i");
                                                        obj.texto = obj.texto.Replace("ó", "o");
                                                        obj.texto = obj.texto.Replace("ú", "u");

                                                        obj.texto = obj.texto.Replace("Á", "A");
                                                        obj.texto = obj.texto.Replace("É", "E");
                                                        obj.texto = obj.texto.Replace("Í", "I");
                                                        obj.texto = obj.texto.Replace("Ó", "O");
                                                        obj.texto = obj.texto.Replace("Ú", "U");

                                                    }

                                                    objetoWhatsAppHook.Id = 0;
                                                    objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                    objetoWhatsAppHook.WaId = "";
                                                    objetoWhatsAppHook.WaType = "template";
                                                    objetoWhatsAppHook.WaTypeMensaje = 8;
                                                    objetoWhatsAppHook.WaRecipientType = "individual";
                                                    objetoWhatsAppHook.WaBody = item.Descripcion;
                                                    objetoWhatsAppHook.WaFile = "";
                                                    objetoWhatsAppHook.WaFileName = "";
                                                    objetoWhatsAppHook.WaMimeType = "";
                                                    objetoWhatsAppHook.WaSha256 = "";
                                                    objetoWhatsAppHook.WaLink = "";
                                                    objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                    objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                    objetoWhatsAppHook.EsMigracion = true;
                                                    objetoWhatsAppHook.IdMigracion = 0;
                                                    objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                    objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                    objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                    objetoWhatsAppHook.imagen = detalle[0].Imagen;
                                                    objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                    objetoWhatsAppHook.botones = respuesta;
                                                    var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                    //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                    string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                    try
                                                    {

                                                        datoRespuesta = UrlPost(url, serializedResult);

                                                        if (datoRespuesta.EstadoMensaje == true && datoRespuesta.WaId != null)
                                                        {
                                                            PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                            //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                            //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                            //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                            //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                            //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                            item.WaId = datoRespuesta.WaId;
                                                            //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                            var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                            bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                            CantidadEnviados = CantidadEnviados + 1;
                                                        }
                                                        else
                                                        {

                                                            MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                            DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                            DatosErroneos.IdAlumno = item.IdAlumno;
                                                            DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                            DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                            DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                            DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                            DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                            DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                            DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                            DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                            DatosErroneos.Estado = true;
                                                            DatosErroneos.FechaCreacion = DateTime.Now;
                                                            DatosErroneos.FechaModificacion = DateTime.Now;
                                                            DatosErroneos.UsuarioCreacion = "whatsapp";
                                                            DatosErroneos.UsuarioModificacion = "whatsapp";




                                                            bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);

                                                            //aca has el guardado de la tabla de errores de envios ;)
                                                            var Resul = datoRespuesta.EstadoMensaje;
                                                        }

                                                    }
                                                    catch { continue; }
                                                }

                                                else
                                                {
                                                    var Serializer = new JavaScriptSerializer();
                                                    RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                    MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                    if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                    {
                                                        item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                    }

                                                    List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                    objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                    foreach (var obj in objeto)
                                                    {
                                                        //Elimina los caracteres con tilde
                                                        obj.texto = obj.texto.Replace("á", "a");
                                                        obj.texto = obj.texto.Replace("é", "e");
                                                        obj.texto = obj.texto.Replace("í", "i");
                                                        obj.texto = obj.texto.Replace("ó", "o");
                                                        obj.texto = obj.texto.Replace("ú", "u");

                                                        obj.texto = obj.texto.Replace("Á", "A");
                                                        obj.texto = obj.texto.Replace("É", "E");
                                                        obj.texto = obj.texto.Replace("Í", "I");
                                                        obj.texto = obj.texto.Replace("Ó", "O");
                                                        obj.texto = obj.texto.Replace("Ú", "U");

                                                        //Elimina las Ñ
                                                        obj.texto = obj.texto.Replace("ñ", "n");
                                                        obj.texto = obj.texto.Replace("Ñ", "N");


                                                        //Elimina los caracteres con tilde
                                                        obj.texto = obj.texto.Replace("á", "a");
                                                        obj.texto = obj.texto.Replace("é", "e");
                                                        obj.texto = obj.texto.Replace("í", "i");
                                                        obj.texto = obj.texto.Replace("ó", "o");
                                                        obj.texto = obj.texto.Replace("ú", "u");

                                                        obj.texto = obj.texto.Replace("Á", "A");
                                                        obj.texto = obj.texto.Replace("É", "E");
                                                        obj.texto = obj.texto.Replace("Í", "I");
                                                        obj.texto = obj.texto.Replace("Ó", "O");
                                                        obj.texto = obj.texto.Replace("Ú", "U");

                                                    }

                                                    objetoWhatsAppHook.Id = 0;
                                                    objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                    objetoWhatsAppHook.WaId = null;
                                                    objetoWhatsAppHook.WaType = "hsm";
                                                    objetoWhatsAppHook.WaTypeMensaje = 8;
                                                    objetoWhatsAppHook.WaRecipientType = "hsm";
                                                    objetoWhatsAppHook.WaBody = item.Descripcion;
                                                    objetoWhatsAppHook.WaFile = null;
                                                    objetoWhatsAppHook.WaFileName = null;
                                                    objetoWhatsAppHook.WaMimeType = null;
                                                    objetoWhatsAppHook.WaSha256 = null;
                                                    objetoWhatsAppHook.WaLink = null;
                                                    objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                    objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                    objetoWhatsAppHook.EsMigracion = true;
                                                    objetoWhatsAppHook.IdMigracion = 0;
                                                    objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                    objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                    objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                    objetoWhatsAppHook.imagen = null;
                                                    objetoWhatsAppHook.botones = null;
                                                    objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                    var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                    string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                    //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                    try
                                                    {

                                                        datoRespuesta = UrlPost(url, serializedResult);

                                                        if (datoRespuesta.EstadoMensaje == true)
                                                        {
                                                            PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                            //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                            //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                            //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                            //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                            //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                            item.WaId = datoRespuesta.WaId;
                                                            //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                            var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                            bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                            CantidadEnviados = CantidadEnviados + 1;
                                                        }
                                                        else
                                                        {

                                                            MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                            DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                            DatosErroneos.IdAlumno = item.IdAlumno;
                                                            DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                            DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                            DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                            DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                            DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                            DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                            DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                            DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                            DatosErroneos.Estado = true;
                                                            DatosErroneos.FechaCreacion = DateTime.Now;
                                                            DatosErroneos.FechaModificacion = DateTime.Now;
                                                            DatosErroneos.UsuarioCreacion = "whatsapp";
                                                            DatosErroneos.UsuarioModificacion = "whatsapp";


                                                            bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);
                                                            //aca has el guardado de la tabla de errores de envios ;)
                                                            var Resul = datoRespuesta.EstadoMensaje;

                                                        }

                                                    }
                                                    catch { continue; }
                                                }
                                            //}
                                            //else
                                            //{
                                            //    continue;
                                            //}

                                        }
                                        else
                                        {
                                            continue;
                                        }


                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                try
                                {
                                    string Subject = "Fin de Envio masivo - Prioridad : " + ConfiguracionPre.Prioridad + " Hora Inicio: " + Hora_Inicio + " Asesor: " + ConfiguracionPre.Asesor;
                                    string Message = "Campania-Prioridad: " + ConfiguracionPre.Nombre + " <br/>" + "Cantidad de contactos peocesados " + PreRespuesta.Count() + " <br/>" + "Cantidad de mensajes Enviado " + CantidadEnviados + " <br/>" + " Hora Inicio : " + Hora_Inicio + " <br/>" + "Hora Fin : " + DateTime.Now + " <br/>" + "Prioridad: " + ConfiguracionPre.Prioridad + " <br/>" + " Campania: " + ConfiguracionPre.Nombre + " <br/>" + " Asesor: " + ConfiguracionPre.Asesor;
                                    this.EnvioCorreoMasivosMarketing(Subject, Message);
                                }
                                catch { }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }

                return (true);

            }
            catch (Exception e)
            {
                return (true);
            }

        }

        public async Task EjecutarCampaniaGeneralEnvioWhatsAppAuto()
        {

            try
            {
                var ListaDePrioridades = _unitOfWork.CampaniaGeneralRepository.ObtenerPrioridadesEnvioWhatsApp();
                if (ListaDePrioridades.Count > 1)
                {

                    try
                    {
                        string Subject = "Inicio de Envio masivo " + ListaDePrioridades[0].NombreCampania + " Hora Inicio: " + ListaDePrioridades[0].HoraEnvio;
                        StringBuilder messageBuilder = new StringBuilder();

                        foreach (var Prioridad in ListaDePrioridades)
                        {
                            messageBuilder.Append("Campania-Prioridad: " + Prioridad.Nombre + " <br/>");
                            messageBuilder.Append("Hora Inicio : " + "Prioridad: " + Prioridad.Prioridad + " <br/>");
                            messageBuilder.Append("Asesor: " + Prioridad.Personal);

                            messageBuilder.Append("<br/><br/>");
                        }

                        string Message = messageBuilder.ToString();

                        EnvioCorreoMasivosMarketing(Subject, Message);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error al enviar correo de inicio de campaña: {ex.Message}");
                    }

                    foreach (var Prioridad in ListaDePrioridades)
                    {
                        int RespuestaInsertarLog = 0;

                        try
                        {
                            try
                            {

                                List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> IdLogActivoParaDeleteLogico = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivoCampaniaGeneralDetalleResponsableWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                if (IdLogActivoParaDeleteLogico.Count() > 0)
                                {
                                    foreach (var Log in IdLogActivoParaDeleteLogico)
                                    {
                                        var RespuestaEliminarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.EliminarLog(Log.Id, "EliminarLogDuplicadoWhatsApp");

                                    }
                                }
                                RespuestaInsertarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarLogWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp, Prioridad.HoraEnvio, Prioridad.FechaInicioEnvioWhatsapp, "InsertarLogWhatsApp");
                                if (RespuestaInsertarLog == 0)
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine($"Error al gestionar logs para la prioridad : {ex.Message}");
                                continue;
                            }
                            var Hora_Inicio = DateTime.Now;

                            List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreRespuesta = new List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>();
                            PreRespuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.PreListaWhatsAppEnvioMasivo(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
                            ConfiguracionPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetalleDeCampaniaWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                            /* Inicio ejecucion envio */
                            if (PreRespuesta.Any())
                            {
                                WhatsAppConfiguracionEnvioDetalle EnvioMensajes = new WhatsAppConfiguracionEnvioDetalle();
                                int CantidadEnviados = 0;
                                foreach (var item in PreRespuesta)
                                {

                                    var logsActivos = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.logsActivos(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                                    if (logsActivos.Count() == 1)
                                    {
                                        if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarEnvioDuplicado(item.CelularWhatsApp, item.Dias) == false)

                                        {
                                            //if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarDesuscritos(item.CelularWhatsApp) == false)
                                            //{

                                            var detalle = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetallePlantillaWhatsApp(item.IdPlantilla);
                                            var respuesta = new List<BotonDTO>();

                                            //validacion
                                            if (detalle.Count != 0 && !string.IsNullOrEmpty(detalle[0].Imagen))
                                            {

                                                foreach (var value in detalle)
                                                {
                                                    respuesta.Add(new BotonDTO { Nombre = value.Boton.Replace(" ", "") });
                                                }
                                                var Serializer = new JavaScriptSerializer();
                                                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                {
                                                    item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                }

                                                List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                foreach (var obj in objeto)
                                                {
                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                    //Elimina las Ñ
                                                    obj.texto = obj.texto.Replace("ñ", "n");
                                                    obj.texto = obj.texto.Replace("Ñ", "N");


                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                }

                                                objetoWhatsAppHook.Id = 0;
                                                objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                objetoWhatsAppHook.WaId = "";
                                                objetoWhatsAppHook.WaType = "template";
                                                objetoWhatsAppHook.WaTypeMensaje = 8;
                                                objetoWhatsAppHook.WaRecipientType = "individual";
                                                objetoWhatsAppHook.WaBody = item.Descripcion;
                                                objetoWhatsAppHook.WaFile = "";
                                                objetoWhatsAppHook.WaFileName = "";
                                                objetoWhatsAppHook.WaMimeType = "";
                                                objetoWhatsAppHook.WaSha256 = "";
                                                objetoWhatsAppHook.WaLink = "";
                                                objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                objetoWhatsAppHook.EsMigracion = true;
                                                objetoWhatsAppHook.IdMigracion = 0;
                                                objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                objetoWhatsAppHook.imagen = detalle[0].Imagen;
                                                objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                objetoWhatsAppHook.botones = respuesta;
                                                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                try
                                                {

                                                    datoRespuesta = UrlPost(url, serializedResult);

                                                    if (datoRespuesta.EstadoMensaje == true && datoRespuesta.WaId != null)
                                                    {
                                                        PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                        //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                        //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                        //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                        //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                        //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        item.WaId = datoRespuesta.WaId;
                                                        //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                        var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                        bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                        CantidadEnviados = CantidadEnviados + 1;
                                                    }
                                                    else
                                                    {

                                                        MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                        DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                        DatosErroneos.IdAlumno = item.IdAlumno;
                                                        DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                        DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                        DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                        DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                        DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                        DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                        DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                        DatosErroneos.Estado = true;
                                                        DatosErroneos.FechaCreacion = DateTime.Now;
                                                        DatosErroneos.FechaModificacion = DateTime.Now;
                                                        DatosErroneos.UsuarioCreacion = "whatsapp";
                                                        DatosErroneos.UsuarioModificacion = "whatsapp";




                                                        bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);

                                                        //aca has el guardado de la tabla de errores de envios ;)
                                                        var Resul = datoRespuesta.EstadoMensaje;
                                                    }

                                                }
                                                catch { continue; }
                                            }

                                            else
                                            {
                                                var Serializer = new JavaScriptSerializer();
                                                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                {
                                                    item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                }

                                                List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                foreach (var obj in objeto)
                                                {
                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                    //Elimina las Ñ
                                                    obj.texto = obj.texto.Replace("ñ", "n");
                                                    obj.texto = obj.texto.Replace("Ñ", "N");


                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                }

                                                objetoWhatsAppHook.Id = 0;
                                                objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                objetoWhatsAppHook.WaId = null;
                                                objetoWhatsAppHook.WaType = "hsm";
                                                objetoWhatsAppHook.WaTypeMensaje = 8;
                                                objetoWhatsAppHook.WaRecipientType = "hsm";
                                                objetoWhatsAppHook.WaBody = item.Descripcion;
                                                objetoWhatsAppHook.WaFile = null;
                                                objetoWhatsAppHook.WaFileName = null;
                                                objetoWhatsAppHook.WaMimeType = null;
                                                objetoWhatsAppHook.WaSha256 = null;
                                                objetoWhatsAppHook.WaLink = null;
                                                objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                objetoWhatsAppHook.EsMigracion = true;
                                                objetoWhatsAppHook.IdMigracion = 0;
                                                objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                objetoWhatsAppHook.imagen = null;
                                                objetoWhatsAppHook.botones = null;
                                                objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                try
                                                {

                                                    datoRespuesta = UrlPost(url, serializedResult);

                                                    if (datoRespuesta.EstadoMensaje == true)
                                                    {
                                                        PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                        //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                        //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                        //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                        //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                        //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        item.WaId = datoRespuesta.WaId;
                                                        //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                        var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                        bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                        CantidadEnviados = CantidadEnviados + 1;
                                                    }
                                                    else
                                                    {

                                                        MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                        DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                        DatosErroneos.IdAlumno = item.IdAlumno;
                                                        DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                        DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                        DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                        DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                        DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                        DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                        DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                        DatosErroneos.Estado = true;
                                                        DatosErroneos.FechaCreacion = DateTime.Now;
                                                        DatosErroneos.FechaModificacion = DateTime.Now;
                                                        DatosErroneos.UsuarioCreacion = "whatsapp";
                                                        DatosErroneos.UsuarioModificacion = "whatsapp";


                                                        bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);
                                                        //aca has el guardado de la tabla de errores de envios ;)
                                                        var Resul = datoRespuesta.EstadoMensaje;

                                                    }

                                                }
                                                catch { continue; }
                                            }
                                            //}
                                            //else
                                            //{
                                            //    continue;
                                            //}

                                        }
                                        else
                                        {
                                            continue;
                                        }


                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                try
                                {
                                    string Subject = "Fin de Envio masivo - Prioridad : " + ConfiguracionPre.Prioridad + " Hora Inicio: " + Hora_Inicio + " Asesor: " + ConfiguracionPre.Asesor;
                                    string Message = "Campania-Prioridad: " + ConfiguracionPre.Nombre + " <br/>" + "Cantidad de contactos peocesados " + PreRespuesta.Count() + " <br/>" + "Cantidad de mensajes Enviado " + CantidadEnviados + " <br/>" + " Hora Inicio : " + Hora_Inicio + " <br/>" + "Hora Fin : " + DateTime.Now + " <br/>" + "Prioridad: " + ConfiguracionPre.Prioridad + " <br/>" + " Campania: " + ConfiguracionPre.Nombre + " <br/>" + " Asesor: " + ConfiguracionPre.Asesor;
                                    this.EnvioCorreoMasivosMarketing(Subject, Message);
                                }
                                catch { }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }


            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error general en EjecutarCampaniaGeneralEnvioWhatsAppAsync: {e.Message}");

            }

        }


        public async Task<bool> EjecutarCampaniaGeneralEnvioWhatsAppAsync()
        {

            try
            {
                var ListaDePrioridades = _unitOfWork.CampaniaGeneralRepository.ObtenerPrioridadesEnvioWhatsApp();
                if (ListaDePrioridades.Any())
                {

                    try
                    {
                        string Subject = "Inicio de Envio masivo " + ListaDePrioridades[0].NombreCampania + " Hora Inicio: " + ListaDePrioridades[0].HoraEnvio;
                        StringBuilder messageBuilder = new StringBuilder();

                        foreach (var Prioridad in ListaDePrioridades)
                        {
                            messageBuilder.Append("Campania-Prioridad: " + Prioridad.Nombre + " <br/>");
                            messageBuilder.Append("Hora Inicio : " + "Prioridad: " + Prioridad.Prioridad + " <br/>");
                            messageBuilder.Append("Asesor: " + Prioridad.Personal);

                            messageBuilder.Append("<br/><br/>");
                        }

                        string Message = messageBuilder.ToString();

                        this.EnvioCorreoMasivosMarketing(Subject, Message);
                    }
                    catch (Exception ex)
                    {

                    }

                    foreach (var Prioridad in ListaDePrioridades)
                    {
                        int RespuestaInsertarLog = 0;

                        try
                        {
                            try
                            {

                                List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> IdLogActivoParaDeleteLogico = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivoCampaniaGeneralDetalleResponsableWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                if (IdLogActivoParaDeleteLogico.Count() > 0)
                                {
                                    foreach (var Log in IdLogActivoParaDeleteLogico)
                                    {
                                        var RespuestaEliminarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.EliminarLog(Log.Id, "EliminarLogDuplicadoWhatsApp");

                                    }
                                }
                                RespuestaInsertarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarLogWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp, Prioridad.HoraEnvio, Prioridad.FechaInicioEnvioWhatsapp, "InsertarLogWhatsApp");
                                if (RespuestaInsertarLog == 0)
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                            var Hora_Inicio = DateTime.Now;

                            List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreRespuesta = new List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>();
                            PreRespuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.PreListaWhatsAppEnvioMasivo(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
                            ConfiguracionPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetalleDeCampaniaWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                            /* Inicio ejecucion envio */


                            if (PreRespuesta.Any())
                            {
                                WhatsAppConfiguracionEnvioDetalle EnvioMensajes = new WhatsAppConfiguracionEnvioDetalle();

                                // Task[] preRespuestaTasks = new Task[PreRespuesta.Count()];
                                int CantidadEnviados = 0;
                                var tareasMensaje = new List<TareasRespuestaMensajeHookDTO>();
                                
                                foreach (var item in PreRespuesta)
                                {


                                    var logsActivos = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.logsActivos(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                                    if (logsActivos.Count() == 1)
                                    {
                                        if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarEnvioDuplicado(item.CelularWhatsApp, item.Dias) == false)

                                        {
                                            //if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarDesuscritos(item.CelularWhatsApp) == false)
                                            //{

                                                var detalle = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetallePlantillaWhatsApp(item.IdPlantilla);
                                                var respuesta = new List<BotonDTO>();
                                                const string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                //const string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();
                                                var serializer = new JavaScriptSerializer();
                                                if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                {
                                                    item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                }
                                                if (detalle.Count != 0 && detalle[0].Imagen != null)
                                                {

                                                    foreach (var value in detalle)
                                                    {
                                                        respuesta.Add(new BotonDTO { Nombre = value.Boton.Replace(" ", "") });
                                                    }


                                                    List<DatosPlantillaWhatsAppDTO> objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla)!;
                                                    LimpiarPlantillaWhatsapp(ref objeto);

                                                    objetoWhatsAppHook.Id = 0;
                                                    objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                    objetoWhatsAppHook.WaId = "";
                                                    objetoWhatsAppHook.WaType = "template";
                                                    objetoWhatsAppHook.WaTypeMensaje = 8;
                                                    objetoWhatsAppHook.WaRecipientType = "individual";
                                                    objetoWhatsAppHook.WaBody = item.Descripcion;
                                                    objetoWhatsAppHook.WaFile = "";
                                                    objetoWhatsAppHook.WaFileName = "";
                                                    objetoWhatsAppHook.WaMimeType = "";
                                                    objetoWhatsAppHook.WaSha256 = "";
                                                    objetoWhatsAppHook.WaLink = "";
                                                    objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                    objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                    objetoWhatsAppHook.EsMigracion = true;
                                                    objetoWhatsAppHook.IdMigracion = 0;
                                                    objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                    objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                    objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                    objetoWhatsAppHook.imagen = detalle[0].Imagen;
                                                    objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                    objetoWhatsAppHook.botones = respuesta;

                                                }

                                                else
                                                {

                                                    List<DatosPlantillaWhatsAppDTO> objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla)!;
                                                    LimpiarPlantillaWhatsapp(ref objeto);


                                                    objetoWhatsAppHook.Id = 0;
                                                    objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                    objetoWhatsAppHook.WaId = null;
                                                    objetoWhatsAppHook.WaType = "hsm";
                                                    objetoWhatsAppHook.WaTypeMensaje = 8;
                                                    objetoWhatsAppHook.WaRecipientType = "hsm";
                                                    objetoWhatsAppHook.WaBody = item.Descripcion;
                                                    objetoWhatsAppHook.WaFile = null;
                                                    objetoWhatsAppHook.WaFileName = null;
                                                    objetoWhatsAppHook.WaMimeType = null;
                                                    objetoWhatsAppHook.WaSha256 = null;
                                                    objetoWhatsAppHook.WaLink = null;
                                                    objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                    objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                    objetoWhatsAppHook.EsMigracion = true;
                                                    objetoWhatsAppHook.IdMigracion = 0;
                                                    objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                    objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                    objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                    objetoWhatsAppHook.imagen = null;
                                                    objetoWhatsAppHook.botones = null;
                                                    objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;

                                                }
                                                var serializedResult = serializer.Serialize(objetoWhatsAppHook);
                                                try
                                                {


                                                    var task = UrlPostAsync(url, serializedResult);
                                                    tareasMensaje.Add(new TareasRespuestaMensajeHookDTO
                                                    {
                                                        Item= item,
                                                        Tarea=task,
                                                      
                                                    });




                                                }
                                                catch { continue; }
                                           // }
                                            //else
                                            //{
                                            //    continue;
                                            //}


                                        }
                                        else
                                        {
                                            continue;
                                        }


                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                foreach (var tarea2 in tareasMensaje)
                                {
                                    try
                                    {
                                        var datoRespuesta = await tarea2.Tarea;


                                        if (datoRespuesta.EstadoMensaje == true)
                                        {
                                            var serializer = new JavaScriptSerializer();
                                            PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                            //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                            //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                            //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                            //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                            //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                            tarea2.Item.WaId = datoRespuesta.WaId;
                                            //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                            var serializedResultInsertEnviado = serializer.Serialize(tarea2.Item);
                                            bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, tarea2.Item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                            CantidadEnviados = CantidadEnviados + 1;
                                        }
                                        else
                                        {

                                            MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                            DatosErroneos.CelularWhatsapp = tarea2.Item.CelularWhatsApp;
                                            DatosErroneos.IdAlumno = tarea2.Item.IdAlumno;
                                            DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = tarea2.Item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                            DatosErroneos.IdPlantilla = tarea2.Item.IdPlantilla;
                                            DatosErroneos.MensajePlantillaHtml = tarea2.Item.MensajePlantillaHtml;
                                            DatosErroneos.ObjetoPlantilla = tarea2.Item.ObjetoPlantilla;
                                            DatosErroneos.IdPais = tarea2.Item.WhatsAppEmpresaIdPais;
                                            DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                            DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                            DatosErroneos.WaId = tarea2.Item.WaId != null ? tarea2.Item.WaId : "";
                                            DatosErroneos.Estado = true;
                                            DatosErroneos.FechaCreacion = DateTime.Now;
                                            DatosErroneos.FechaModificacion = DateTime.Now;
                                            DatosErroneos.UsuarioCreacion = "whatsapp";
                                            DatosErroneos.UsuarioModificacion = "whatsapp";


                                            bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);
                                            //aca has el guardado de la tabla de errores de envios ;)
                                            var Resul = datoRespuesta.EstadoMensaje;

                                        }

                                    }
                                    catch { continue; }
                                    


                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }

                return (true);

            }
            catch (Exception e)
            {
                return (true);
            }

        }

        private List<DatosPlantillaWhatsAppDTO> LimpiarPlantillaWhatsapp(ref List<DatosPlantillaWhatsAppDTO> objeto)
        {
            foreach (var obj in objeto)
            {
                //Elimina los caracteres con tilde
                obj.texto = obj.texto.Replace("á", "a");
                obj.texto = obj.texto.Replace("é", "e");
                obj.texto = obj.texto.Replace("í", "i");
                obj.texto = obj.texto.Replace("ó", "o");
                obj.texto = obj.texto.Replace("ú", "u");

                obj.texto = obj.texto.Replace("Á", "A");
                obj.texto = obj.texto.Replace("É", "E");
                obj.texto = obj.texto.Replace("Í", "I");
                obj.texto = obj.texto.Replace("Ó", "O");
                obj.texto = obj.texto.Replace("Ú", "U");

                //Elimina las Ñ
                obj.texto = obj.texto.Replace("ñ", "n");
                obj.texto = obj.texto.Replace("Ñ", "N");


                //Elimina los caracteres con tilde
                obj.texto = obj.texto.Replace("á", "a");
                obj.texto = obj.texto.Replace("é", "e");
                obj.texto = obj.texto.Replace("í", "i");
                obj.texto = obj.texto.Replace("ó", "o");
                obj.texto = obj.texto.Replace("ú", "u");

                obj.texto = obj.texto.Replace("Á", "A");
                obj.texto = obj.texto.Replace("É", "E");
                obj.texto = obj.texto.Replace("Í", "I");
                obj.texto = obj.texto.Replace("Ó", "O");
                obj.texto = obj.texto.Replace("Ú", "U");

            }
            return objeto;

        }
        /// <summary>

        /// Descripción: Esta funcion permite visualizar las listas pre procesadas por la vista del sistema
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns></returns>
        public bool EnvioCorreoMasivosMarketing(string _Subject, string _Message)
        {
            try
            {


                List<AsesoresMktDTO> correosAlerta = new List<AsesoresMktDTO>();
                List<string> correosAlerta2 = new List<string>();
                //correosAlerta2.Add("emayta@bsginstitute.com");
                correosAlerta2.Add("jllanque@bsginstitute.com");
                List<string> correosAlertaCopia = new List<string>();

                correosAlerta.AddRange(_unitOfWork.RegistroRecuperacionWhatsAppRepository.ListaAsesoresMarketing());
                foreach (var item in correosAlerta)
                {
                    correosAlertaCopia.Add(item.Email);
                }
                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta2);
                mailDataAlerta.Subject = _Subject;
                mailDataAlerta.Message = _Message;
                mailDataAlerta.Cc = string.Join(",", correosAlertaCopia);
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// Autor: 
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
        public RespuestaMensajeHookDTO UrlPost(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = jsonStringResult;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(content);

                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }

        }

        /// Autor: 
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
        public async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = jsonStringResult;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = await http.GetRequestStreamAsync();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = await http.GetResponseAsync();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = await sr.ReadToEndAsync();
                respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(content);

                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }

        }


        public bool EnvioWhatsappChatBot(WhatsAppChatBotDTO datos)
        {

            try
            {


                List<DatosPlantillaWhatsAppDTO> objetoPlantilla = new List<DatosPlantillaWhatsAppDTO>();

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(datos.idPlantilla);

                var plantillab = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(datos.idPlantilla);

                // Reemplazo de etiquetas //

                if (datos.nombre != null || datos.nombre != "")
                {

                    plantilla.Texto = plantilla.Texto.Replace("{tAlumno.Nombre1}", datos.nombre);

                    var objetito = new DatosPlantillaWhatsAppDTO();

                    objetito.codigo = "{tAlumno.Nombre1}";
                    objetito.texto = datos.nombre;

                    objetoPlantilla.Add(objetito);
                }
                // si se le aumenta etiquetas, crear nueva funcion, sino no XD //


                var detalle = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetallePlantillaWhatsApp(datos.idPlantilla);
                var respuesta = new List<BotonDTO>();

                foreach (var value in detalle)
                {
                    respuesta.Add(new BotonDTO { Nombre = value.Boton.Replace(" ", "") });
                }
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();


                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = datos.celular;
                objetoWhatsAppHook.WaId = "";
                objetoWhatsAppHook.WaType = "hsm";
                objetoWhatsAppHook.WaTypeMensaje = 8;
                objetoWhatsAppHook.WaRecipientType = "individual";
                objetoWhatsAppHook.WaBody = plantillab.Descripcion;
                objetoWhatsAppHook.WaFile = "";
                objetoWhatsAppHook.WaFileName = "";
                objetoWhatsAppHook.WaMimeType = "";
                objetoWhatsAppHook.WaSha256 = "";
                objetoWhatsAppHook.WaLink = "";
                objetoWhatsAppHook.WaCaption = plantilla.Texto;
                objetoWhatsAppHook.IdPais = 0;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = 0;
                objetoWhatsAppHook.IdAlumno = 0;
                objetoWhatsAppHook.usuario = "´WhatsappChatBot";
                objetoWhatsAppHook.DatosPlantillaWhatsApp = objetoPlantilla;
                objetoWhatsAppHook.botones = respuesta;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                try
                {

                    datoRespuesta = UrlPost(url, serializedResult);

                    if (datoRespuesta.EstadoMensaje == true && datoRespuesta.WaId != null)
                    {
                        // Aca guarda tus datos en tu tabla si se hizo el envio


                    }
                    else
                    {
                        var jeje = datoRespuesta.EstadoMensaje;
                    }

                    return true;


                }

                catch { return false; }
            }


            catch (Exception e)
            {
                return true;
            }

        }

        public List<MensajeEnviadoErroneoWhatsAppDTO> ObtenerReporteMensajesEnviadosErroneos(FiltroMensajesEnviadosErroneosDTO filtros)
        {
            try
            {
                return _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.ObtenerReporteMensajesEnviadosErroneos(filtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ResultadoEjecucionCampaniaDTO EjecutarCampaniaGeneralEnvioWhatsAppBoton()
        {

            try
            {
                var resultado = _unitOfWork.CampaniaGeneralRepository.ObtenerPrioridadesEnvioWhatsApp2();
                var ListaDePrioridades = resultado.ListaPrioridades;

                string subject = $"🟢 Inicio de envío WhatsApp - Servidor: {resultado.FechaServidor} {resultado.HoraServidor}";
                string message = $"📍 Hora Servidor: {resultado.FechaServidor} {resultado.HoraServidor}<br/>" +
                                 $"📍 Hora Programada (BD): {resultado.FechaProgramada} {resultado.HoraProgramada}";
                if (resultado.ListaPrioridades.Any())
                {

                    try
                    {
                        string Subject = "Inicio de Envio masivo " + ListaDePrioridades[0].NombreCampania + " Hora Inicio: " + ListaDePrioridades[0].HoraEnvio;
                        StringBuilder messageBuilder = new StringBuilder();


                        foreach (var Prioridad in ListaDePrioridades)
                        {
                            messageBuilder.Append("Campania-Prioridad: " + Prioridad.Nombre + " <br/>");
                            messageBuilder.Append("Hora Inicio : " + "Prioridad: " + Prioridad.Prioridad + " <br/>");
                            messageBuilder.Append("Asesor: " + Prioridad.Personal);

                            messageBuilder.Append("<br/><br/>");
                        }

                        string Message = messageBuilder.ToString();

                        this.EnvioCorreoMasivosMarketing(Subject, Message);
                    }
                    catch (Exception ex)
                    {

                    }

                    foreach (var Prioridad in ListaDePrioridades)
                    {
                        int RespuestaInsertarLog = 0;

                        try
                        {
                            try
                            {

                                List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> IdLogActivoParaDeleteLogico = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivoCampaniaGeneralDetalleResponsableWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                if (IdLogActivoParaDeleteLogico.Count() > 0)
                                {
                                    foreach (var Log in IdLogActivoParaDeleteLogico)
                                    {
                                        var RespuestaEliminarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.EliminarLog(Log.Id, "EliminarLogDuplicadoWhatsApp");

                                    }
                                }
                                RespuestaInsertarLog = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarLogWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp, Prioridad.HoraEnvio, Prioridad.FechaInicioEnvioWhatsapp, "InsertarLogWhatsApp");
                                if (RespuestaInsertarLog == 0)
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                            var Hora_Inicio = DateTime.Now;

                            List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreRespuesta = new List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO>();
                            PreRespuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.PreListaWhatsAppEnvioMasivo(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                            DetalleCampaniaDTO ConfiguracionPre = new DetalleCampaniaDTO();
                            ConfiguracionPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetalleDeCampaniaWhatsApp(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                            /* Inicio ejecucion envio */
                            if (PreRespuesta.Any())
                            {
                                WhatsAppConfiguracionEnvioDetalle EnvioMensajes = new WhatsAppConfiguracionEnvioDetalle();
                                //try
                                //{
                                //    string Subject = "Inicio de Envio masivo - Prioridad : " + ConfiguracionPre.Prioridad + " Hora Inicio: " + Hora_Inicio + " Asesor: " + ConfiguracionPre.Asesor;
                                //    string Message = "Campania-Prioridad: " + ConfiguracionPre.Nombre + " <br/>" + "Cantidad de contactos peocesados " + PreRespuesta.Count() + " <br/>" + " Hora Inicio : " + Hora_Inicio+ " <br/>" + "Prioridad: " + ConfiguracionPre.Prioridad + " <br/>" + " Campania: " + ConfiguracionPre.Nombre + " <br/>" + " Asesor: " + ConfiguracionPre.Asesor;
                                //    this.EnvioCorreoMasivosMarketing(Subject, Message);
                                //}
                                //catch {}
                                int CantidadEnviados = 0;
                                foreach (var item in PreRespuesta)
                                {

                                    var logsActivos = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.logsActivos(Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);

                                    if (logsActivos.Count() == 1)
                                    {
                                        if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarEnvioDuplicado(item.CelularWhatsApp, item.Dias) == false)

                                        {
                                            //if (_unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ValidarDesuscritos(item.CelularWhatsApp) == false)
                                            //{

                                            var detalle = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ObtenerDetallePlantillaWhatsApp(item.IdPlantilla);
                                            var respuesta = new List<BotonDTO>();

                                            //validacion
                                            if (detalle.Count != 0 && !string.IsNullOrEmpty(detalle[0].Imagen))
                                            {

                                                foreach (var value in detalle)
                                                {
                                                    respuesta.Add(new BotonDTO { Nombre = value.Boton.Replace(" ", "") });
                                                }
                                                var Serializer = new JavaScriptSerializer();
                                                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                {
                                                    item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                }

                                                List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                foreach (var obj in objeto)
                                                {
                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                    //Elimina las Ñ
                                                    obj.texto = obj.texto.Replace("ñ", "n");
                                                    obj.texto = obj.texto.Replace("Ñ", "N");


                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                }

                                                objetoWhatsAppHook.Id = 0;
                                                objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                objetoWhatsAppHook.WaId = "";
                                                objetoWhatsAppHook.WaType = "template";
                                                objetoWhatsAppHook.WaTypeMensaje = 8;
                                                objetoWhatsAppHook.WaRecipientType = "individual";
                                                objetoWhatsAppHook.WaBody = item.Descripcion;
                                                objetoWhatsAppHook.WaFile = "";
                                                objetoWhatsAppHook.WaFileName = "";
                                                objetoWhatsAppHook.WaMimeType = "";
                                                objetoWhatsAppHook.WaSha256 = "";
                                                objetoWhatsAppHook.WaLink = "";
                                                objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                objetoWhatsAppHook.EsMigracion = true;
                                                objetoWhatsAppHook.IdMigracion = 0;
                                                objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                objetoWhatsAppHook.imagen = detalle[0].Imagen;
                                                objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                objetoWhatsAppHook.botones = respuesta;
                                                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                try
                                                {

                                                    datoRespuesta = UrlPost(url, serializedResult);

                                                    if (datoRespuesta.EstadoMensaje == true && datoRespuesta.WaId != null)
                                                    {
                                                        PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                        //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                        //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                        //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                        //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                        //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        item.WaId = datoRespuesta.WaId;
                                                        //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                        var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                        bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                        CantidadEnviados = CantidadEnviados + 1;
                                                    }
                                                    else
                                                    {

                                                        MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                        DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                        DatosErroneos.IdAlumno = item.IdAlumno;
                                                        DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                        DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                        DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                        DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                        DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                        DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                        DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                        DatosErroneos.Estado = true;
                                                        DatosErroneos.FechaCreacion = DateTime.Now;
                                                        DatosErroneos.FechaModificacion = DateTime.Now;
                                                        DatosErroneos.UsuarioCreacion = "whatsapp";
                                                        DatosErroneos.UsuarioModificacion = "whatsapp";




                                                        bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);

                                                        //aca has el guardado de la tabla de errores de envios ;)
                                                        var Resul = datoRespuesta.EstadoMensaje;
                                                    }

                                                }
                                                catch { continue; }
                                            }

                                            else
                                            {
                                                var Serializer = new JavaScriptSerializer();
                                                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();
                                                MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();

                                                if (!string.IsNullOrWhiteSpace(item.ObjetoPlantilla))
                                                {
                                                    item.ObjetoPlantilla = item.ObjetoPlantilla.Replace("\t", "");
                                                }

                                                List<DatosPlantillaWhatsAppDTO> objeto = new List<DatosPlantillaWhatsAppDTO>();

                                                objeto = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(item.ObjetoPlantilla);

                                                foreach (var obj in objeto)
                                                {
                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                    //Elimina las Ñ
                                                    obj.texto = obj.texto.Replace("ñ", "n");
                                                    obj.texto = obj.texto.Replace("Ñ", "N");


                                                    //Elimina los caracteres con tilde
                                                    obj.texto = obj.texto.Replace("á", "a");
                                                    obj.texto = obj.texto.Replace("é", "e");
                                                    obj.texto = obj.texto.Replace("í", "i");
                                                    obj.texto = obj.texto.Replace("ó", "o");
                                                    obj.texto = obj.texto.Replace("ú", "u");

                                                    obj.texto = obj.texto.Replace("Á", "A");
                                                    obj.texto = obj.texto.Replace("É", "E");
                                                    obj.texto = obj.texto.Replace("Í", "I");
                                                    obj.texto = obj.texto.Replace("Ó", "O");
                                                    obj.texto = obj.texto.Replace("Ú", "U");

                                                }

                                                objetoWhatsAppHook.Id = 0;
                                                objetoWhatsAppHook.WaTo = item.CelularWhatsApp;
                                                objetoWhatsAppHook.WaId = null;
                                                objetoWhatsAppHook.WaType = "hsm";
                                                objetoWhatsAppHook.WaTypeMensaje = 8;
                                                objetoWhatsAppHook.WaRecipientType = "hsm";
                                                objetoWhatsAppHook.WaBody = item.Descripcion;
                                                objetoWhatsAppHook.WaFile = null;
                                                objetoWhatsAppHook.WaFileName = null;
                                                objetoWhatsAppHook.WaMimeType = null;
                                                objetoWhatsAppHook.WaSha256 = null;
                                                objetoWhatsAppHook.WaLink = null;
                                                objetoWhatsAppHook.WaCaption = item.MensajePlantillaHtml;
                                                objetoWhatsAppHook.IdPais = item.WhatsAppEmpresaIdPais;
                                                objetoWhatsAppHook.EsMigracion = true;
                                                objetoWhatsAppHook.IdMigracion = 0;
                                                objetoWhatsAppHook.IdPersonal = item.IdPersonal;
                                                objetoWhatsAppHook.IdAlumno = item.IdAlumno;
                                                objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                                                objetoWhatsAppHook.imagen = null;
                                                objetoWhatsAppHook.botones = null;
                                                objetoWhatsAppHook.DatosPlantillaWhatsApp = objeto;
                                                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                                                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";
                                                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketingMasivos";

                                                try
                                                {

                                                    datoRespuesta = UrlPost(url, serializedResult);

                                                    if (datoRespuesta.EstadoMensaje == true)
                                                    {
                                                        PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO Json = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                                                        //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                                                        //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                                                        //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                                                        //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                                                        //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        item.WaId = datoRespuesta.WaId;
                                                        //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                                                        var serializedResultInsertEnviado = Serializer.Serialize(item);
                                                        bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, Prioridad.IdCampaniaGeneralDetalleResponsableWhatsApp);
                                                        CantidadEnviados = CantidadEnviados + 1;
                                                    }
                                                    else
                                                    {

                                                        MensajeEnviadoErroneoWhatsappLogDTO DatosErroneos = new MensajeEnviadoErroneoWhatsappLogDTO();
                                                        DatosErroneos.CelularWhatsapp = item.CelularWhatsApp;
                                                        DatosErroneos.IdAlumno = item.IdAlumno;
                                                        DatosErroneos.IdCampaniaGeneralDetalleResponsableWhatsapp = item.IdCampaniaGeneralDetalleResponsableWhatsApp;
                                                        DatosErroneos.IdPlantilla = item.IdPlantilla;
                                                        DatosErroneos.MensajePlantillaHtml = item.MensajePlantillaHtml;
                                                        DatosErroneos.ObjetoPlantilla = item.ObjetoPlantilla;
                                                        DatosErroneos.IdPais = item.WhatsAppEmpresaIdPais;
                                                        DatosErroneos.MensajeErroneo = datoRespuesta.Mensaje != null ? datoRespuesta.Mensaje : "";
                                                        DatosErroneos.NumeroEnviado = datoRespuesta.NumeroEnvio != null ? datoRespuesta.NumeroEnvio : "";
                                                        DatosErroneos.WaId = item.WaId != null ? item.WaId : "";
                                                        DatosErroneos.Estado = true;
                                                        DatosErroneos.FechaCreacion = DateTime.Now;
                                                        DatosErroneos.FechaModificacion = DateTime.Now;
                                                        DatosErroneos.UsuarioCreacion = "whatsapp";
                                                        DatosErroneos.UsuarioModificacion = "whatsapp";


                                                        bool ResultadoErroneo = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarMensajeEnviadoErroneoWhatsappLog(DatosErroneos);
                                                        //aca has el guardado de la tabla de errores de envios ;)
                                                        var Resul = datoRespuesta.EstadoMensaje;

                                                    }

                                                }
                                                catch { continue; }
                                            }
                                            //}
                                            //else
                                            //{
                                            //    continue;
                                            //}

                                        }
                                        else
                                        {
                                            continue;
                                        }


                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                try
                                {
                                    string Subject = "Fin de Envio masivo - Prioridad : " + ConfiguracionPre.Prioridad + " Hora Inicio: " + Hora_Inicio + " Asesor: " + ConfiguracionPre.Asesor;
                                    string Message = "Campania-Prioridad: " + ConfiguracionPre.Nombre + " <br/>" + "Cantidad de contactos peocesados " + PreRespuesta.Count() + " <br/>" + "Cantidad de mensajes Enviado " + CantidadEnviados + " <br/>" + " Hora Inicio : " + Hora_Inicio + " <br/>" + "Hora Fin : " + DateTime.Now + " <br/>" + "Prioridad: " + ConfiguracionPre.Prioridad + " <br/>" + " Campania: " + ConfiguracionPre.Nombre + " <br/>" + " Asesor: " + ConfiguracionPre.Asesor;
                                    this.EnvioCorreoMasivosMarketing(Subject, Message);
                                }
                                catch { }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }

                return resultado;

            }
            catch (Exception e)
            {
                return new ResultadoEjecucionCampaniaDTO
                {
                    ListaPrioridades = new List<ObtenerPrioridadesEnvioWhatsAppDTO>(),
                    FechaServidor = "",
                    HoraServidor = "",
                    FechaProgramada = "",
                    HoraProgramada = "",

                };
            }

        }


    }
}

