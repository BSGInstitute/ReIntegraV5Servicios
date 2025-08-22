using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralSmsDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampaniaGeneralSmsService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralSms
    /// </summary>
    public class CampaniaGeneralSmsService : ICampaniaGeneralSmsService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampaniaGeneralSmsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralSm, CampaniaGeneralSms>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        private readonly string apiUrl = "https://api-sms.masivapp.com/send-message";
        private readonly string username = "Peru_ClaroMultioperador_BSGrupo_X6LSE";
        private readonly string password = "Zd-[PA0yjk";


        public List<ObtenerCampaniaGeneralDetalleSmsDTO>? ObtenerCampaniaGeneralDetalleSms(IdDTO id)
        {
            try
            {

                List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO> modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerCampaniaGeneralDetalleSms(id);
                List<ObtenerCampaniaGeneralDetalleSmsDTO>? resultadoAgrupado = new List<ObtenerCampaniaGeneralDetalleSmsDTO>();

                resultadoAgrupado = modelo.GroupBy(x => new { x.Id, x.NombreCampaniaGeneralSms, x.FechaInicioEnvioSms, x.HoraEnvio }).Select(x => new ObtenerCampaniaGeneralDetalleSmsDTO
                {
                    Id = x.Key.Id,
                    NombreCampaniaGeneralSms = x.Key.NombreCampaniaGeneralSms,
                    FechaInicioEnvioSms = x.Key.FechaInicioEnvioSms,
                    HoraEnvio = x.Key.HoraEnvio,
                    ObtenerCampaniaGeneralDetallePrioridadSms = x.GroupBy(y => new { y.IdCampaniaGeneralDetalleSms, y.NombreCampaniaOrigen, y.Prioridad, y.Nombre, y.ActivarMasivo, y.Programados, y.CantidadBase, y.Enviados }).Select(y => new ObtenerCampaniaGeneralDetallePrioridadSmsDTO
                    {
                        IdCampaniaGeneralDetalleSms = y.Key.IdCampaniaGeneralDetalleSms,
                        NombreCampaniaOrigen = y.Key.NombreCampaniaOrigen,
                        Prioridad = y.Key.Prioridad,
                        Nombre = y.Key.Nombre,
                        ActivarMasivo = y.Key.ActivarMasivo,
                        Programados = y.Key.Programados,
                        CantidadBase = y.Key.CantidadBase,
                        Enviados = y.Key.Enviados,


                    }).ToList(),
                }).ToList();
                return resultadoAgrupado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCampaniaGeneralSms(StringDTO nombre, string Usuario)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.InsertarCampaniaGeneralSms(nombre, Usuario);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaGeneralSmsDTO ObtenerCampaniaGeneralSms(IdDTO id)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerCampaniaGeneralSms(id);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCampaniaGeneralSms(ActualizarCampaniaGeneralSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ActualizarCampaniaGeneralSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ObtenerCampaniaGeneralGrillaSmsDTO> ObtenerCampaniaGeneralGrillaSms()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerCampaniaGeneralGrillaSms();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCampaniaGeneralSms(EliminarCampaniaGeneralSmsDTO json)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.EliminarCampaniaGeneralSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ActualizarActivarMasivoPorCampania(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EliminarCampaniaGeneralDetalleSms(EliminarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.EliminarCampaniaGeneralDetalleSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarCampaniaGeneralDetalleSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.InsertarCampaniaGeneralDetalleSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarCampaniaGeneralDetalleExcelSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.InsertarCampaniaGeneralDetalleExcelSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarCamposCampaniaGeneralDetalleSms(ActualizarCamposCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ActualizarCamposCampaniaGeneralDetalleSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ProcesarDataPorPrioridadExcel(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO ObtenerConfiguracionCampaniaGeneralDetalleSms(IdDTO id)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerConfiguracionCampaniaGeneralDetalleSms(id);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id)
        {
            try
            {
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO> modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(id);
                ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO resultadoAgrupado = new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO();

                resultadoAgrupado = modelo.GroupBy(x => new { x.Id, x.CantidadBase, x.CantidadDisponible }).Select(x => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO
                {
                    Id = x.Key.Id,
                    CantidadBase = x.Key.CantidadBase,
                    CantidadDisponible = x.Key.CantidadDisponible,
                    ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaSms = x.GroupBy(y => new { y.IdCampaniaGeneralDetalleResponsableSms, y.Asesor, y.Plantilla, y.CentroCosto, y.Cantidad, y.Enviados, y.AlumnoConfigurado }).Select(y => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaSmsDTO
                    {
                        IdCampaniaGeneralDetalleResponsableSms = y.Key.IdCampaniaGeneralDetalleResponsableSms,
                        Asesor = y.Key.Asesor,
                        Plantilla = y.Key.Plantilla,
                        CentroCosto = y.Key.CentroCosto,
                        Cantidad = y.Key.Cantidad,
                        Enviados = y.Key.Enviados,
                        AlumnoConfigurado = y.Key.AlumnoConfigurado
                    }).ToList(),
                }).FirstOrDefault();

                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCampaniaGeneralDetalleResponsableSms(EliminarCampaniaGeneralDetalleResponsableSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.EliminarCampaniaGeneralDetalleResponsableSms(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCampaniaGeneralDetalleResponsableSms(InsertarCampaniaGeneralDetalleResponsableSmsDTO json, string usuario)
        {
            try
            {
                var serializerProceso = new JavaScriptSerializer();

                List<AlumnoSmsMasivo> ListaAlumnos = new List<AlumnoSmsMasivo>();
                List<AlumnoSmsMasivoJSON> ListaAlumnosReemplazo = new List<AlumnoSmsMasivoJSON>();
                PrioridadDatosSmsDTO obj = new PrioridadDatosSmsDTO();
                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.InsertarCampaniaGeneralDetalleResponsableSms(json);
                var modeloDatos = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerDatosPorPrioridadAsignada(modelo.Valor);

                obj.Cantidad = json.Cantidad;
                obj.IdCampaniaGeneralDetalleSms = json.IdCampaniaGeneralDetalleSms;
                List<AlumnoSmsMasivoBaseDTO> Alumnos = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerAlumnoConfiguradoPorPrioridad(obj);
                foreach (AlumnoSmsMasivoBaseDTO Alu in Alumnos)
                {
                    AlumnoSmsMasivo Alumno = new AlumnoSmsMasivo();
                    Alumno.IdAlumno = Alu.IdAlumno;
                    Alumno.Celular = Alu.CelularSms;
                    Alumno.IdPais = Alu.IdPais;
                    Alumno.ListaObjetoPlantilla = new List<DatoPlantillaSmsDTO>();
                    ListaAlumnos.Add(Alumno);
                }
                ListaAlumnos = ReemplazarEtiquetaMasivosSms(ListaAlumnos);
                InsertarCampaniaGeneralDetalleResponsableAlumnoSmsDTO dto = new InsertarCampaniaGeneralDetalleResponsableAlumnoSmsDTO();
                foreach (var AluRemplazo in ListaAlumnos)
                {
                    AlumnoSmsMasivoJSON Alumno = new AlumnoSmsMasivoJSON();
                    Alumno.IdAlumno = AluRemplazo.IdAlumno;
                    Alumno.Celular = AluRemplazo.Celular;
                    Alumno.Plantilla = AluRemplazo.Plantilla;
                    Alumno.IdPais = AluRemplazo.IdPais;
                    Alumno.ObjetoPlantilla = AluRemplazo.Nombre;
                    ListaAlumnosReemplazo.Add(Alumno);
                }
                dto.IdCampaniaGeneralDetalleResponsableSms = modelo.Valor;
                dto.Usuario = usuario;
                dto.Json = serializerProceso.Serialize(ListaAlumnosReemplazo);
                _unitOfWork.CampaniaGeneralSmsRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoSms(dto);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
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
        public bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueSmsDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ProcesarDataPorPrioridadSendinblue(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ComboCampaniaGeneralDetalleResponsableSmsDTO ObtenerComboCampaniaGeneralDetalleResponsableSms()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerComboCampaniaGeneralDetalleResponsableSms();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerComboCampaniasSendinBlue();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue()
        {
            try
            {

                List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> dto = new List<ObtenerComboCentroCostoCampaniasSendinBlueDTO>();
                dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerComboCentroCostoCampaniasSendinBlue();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AlumnoSmsMasivo> ReemplazarEtiquetaMasivosSms(List<AlumnoSmsMasivo> NumeroAlumno)
        {
            try
            {
                List<int> IdAlumno = NumeroAlumno.Select(x => x.IdAlumno).ToList();

                List<AlumnoInformacionBasicaDTO> listaAlumno = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerDatosAlumno(IdAlumno);

                foreach (var alumnoSmsMasivo in NumeroAlumno)
                {
                    var infoAlumno = listaAlumno.FirstOrDefault(x => x.Id == alumnoSmsMasivo.IdAlumno);

                    if (infoAlumno != null)
                    {
                        alumnoSmsMasivo.Nombre = infoAlumno.Nombre1;
                    }
                }

                return NumeroAlumno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id)
        {
            try
            {

                List<ReporteInteraccionCampaniaGeneralDetalleDTO> dto = new List<ReporteInteraccionCampaniaGeneralDetalleDTO>();
                dto = _unitOfWork.CampaniaGeneralSmsRepository.ReporteInteraccionCampaniaGeneralDetalle(id);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaSmsDato> ObtenerPlantillaSms()
        {
            try
            {

                List<PlantillaSmsDato> dto = new List<PlantillaSmsDato>();
                dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerPlantillaSms();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public IdDTO InsertarPlantillaSms(PlantillaSmsDTO datos)
        {
            try
            {

                IdDTO dto = new IdDTO();
                dto = _unitOfWork.CampaniaGeneralSmsRepository.InsertarPlantillaSms(datos);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarDetalllePlantillaSms(DetallePlantillaSmsDTO datos)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.InsertarDetalllePlantillaSms(datos);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ObtenerPlantillaSmsGrillaDTO> ObtenerPlantilla()
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerPlantilla();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.GenerarUrlFormulariosLink(datos, usuario);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ObtenerDetallePlantillaSmsDTO> ObtenerDetallePlantilla(IdDTO id)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerDetallePlantilla(id);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarPlantillaSms(ActualizarPlantillaSmsDTO datos)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ActualizarPlantillaSms(datos);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EliminarPlantillaSms(IdDTO datos)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.EliminarPlantillaSms(datos);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioCorreoSms(string subject, string mensaje)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    SenderDTO Sender = new SenderDTO();
                    Sender = _unitOfWork.AsignacionRegularRepository.ObtenerSender();

                    var listareceptorres = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerListaAsesores();


                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add("emayta@bsginstitute.com");

                    if (listareceptorres != null && listareceptorres.Count > 0)
                    {
                        foreach (var copia in listareceptorres)
                        {
                            mail.Bcc.Add(copia.Email);
                        }
                    }
                    mail.From = new MailAddress("emayta@bsginstitute.com", "EnviosSms", System.Text.Encoding.UTF8);
                    mail.Subject = subject;
                    mail.Body = mensaje;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    //CONFIGURACIÓN DEL STMP

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;

                    smtp.Credentials = new System.Net.NetworkCredential(Sender.Email, Sender.Contrasenia);// Enter seders   User name and password
                    smtp.EnableSsl = true;
                    smtp.Send(mail)
;
                    smtp.Dispose();
                    return true;
                }
                catch (Exception ex)
                {
                    smtp.Dispose();
                    return false;
                }
            }
        }



        public bool EjecutarCampaniaGeneralEnvioSms()
        {
            try
            {
                var ListaDePrioridades = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerPrioridadesEnvioSms();
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

                        EnvioCorreoSms(Subject, Message);
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción si es necesario
                    }

                    foreach (var Prioridad in ListaDePrioridades)
                    {
                        int RespuestaInsertarLog = 0;

                        try
                        {
                            try
                            {
                                List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO> IdLogActivoParaDeleteLogico = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerLogActivoCampaniaGeneralDetalleResponsableSms(Prioridad.IdCampaniaGeneralDetalleResponsableSms);
                                if (IdLogActivoParaDeleteLogico.Count() > 0)
                                {
                                    foreach (var Log in IdLogActivoParaDeleteLogico)
                                    {
                                        var RespuestaEliminarLog = _unitOfWork.CampaniaGeneralSmsRepository.EliminarLogSms(Log.Id, "EliminarLogDuplicadoSms");
                                    }
                                }
                                RespuestaInsertarLog = _unitOfWork.CampaniaGeneralSmsRepository.InsertarLogSms(Prioridad.IdCampaniaGeneralDetalleResponsableSms, Prioridad.HoraEnvio, Prioridad.FechaInicioEnvioSms, "InsertarLogSms");
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

                            var datosEnvio = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerDataEnvio(Prioridad.IdCampaniaGeneralDetalleResponsableSms);

                            foreach (var datos in datosEnvio)
                            {
                                datos.Text = datos.Text.Replace("[alumno]", datos.Nombre);
                            }

                            this.EnviarSmsMasivo(datosEnvio);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<bool> EnviarSmsMasivo(List<SmsEnviarMensajeDTO> datosEnvio)
        {
            try
            {

                var configuracionEnvio = this.ObtenerConfiguracionEnvio();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(configuracionEnvio.usuario + ":" + configuracionEnvio.pass);
                string decobase64 = Convert.ToBase64String(plainTextBytes);

                foreach (var envio in datosEnvio)
                {
                    if (_unitOfWork.CampaniaGeneralSmsRepository.ValidarEnvioDuplicado(envio.CelularSms) == false)
                    {
                        try
                        {
                            using (var httpClient = new WebClient())
                            {
                                httpClient.Headers[HttpRequestHeader.Authorization] = "Basic " + decobase64;
                                httpClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                                string jsonContent;

                                if (envio.ShortUrlConfig == false)
                                {
                                    jsonContent = JsonConvert.SerializeObject(new
                                    {
                                        to = envio.CelularSms,
                                        text = envio.Text,
                                        isPremium = envio.IsPremium,
                                        isFlash = envio.IsFlash,
                                        isLongmessage = envio.isLongmessage,
                                    });
                                }
                                else
                                {
                                    jsonContent = JsonConvert.SerializeObject(new
                                    {
                                        to = envio.CelularSms,
                                        text = envio.Text,
                                        customdata = envio.Customdata,
                                        isPremium = envio.IsPremium,
                                        isFlash = envio.IsFlash,
                                        isLongmessage = envio.isLongmessage,
                                        shortUrlConfig = new
                                        {
                                            url = envio.Url,
                                            domainShorturl = envio.DomainShorturl
                                        }
                                    });
                                }
                                var response = httpClient.UploadString(apiUrl, jsonContent);

                                var resultado = JsonConvert.DeserializeObject<respuestaMensajeSmsHook>(response);

                                _unitOfWork.CampaniaGeneralSmsRepository.InsertarRespuestaSmsEnvio(resultado, "achipanaa");

                                var insertDto = new InsertarResponsableAlumnoEnviadoSms
                                {
                                    IdCampaniaGeneralDetalleResponsableAlumnoSms = envio.IdCampaniaGeneralDetalleResponsableAlumnoSms,
                                    IdAlumno = envio.idalumno,
                                    CelularSms = envio.CelularSms,
                                    MessageId = resultado.messageId,
                                    JsonEnvio = jsonContent
                                };

                                if (resultado != null && resultado.statusCode == 200)
                                {
                                    _unitOfWork.CampaniaGeneralSmsRepository.InsertarAlumnoEnviado(insertDto, "achipanaa");
                                }
                                else
                                {
                                    _unitOfWork.CampaniaGeneralSmsRepository.InsertarAlumnoErroneo(insertDto, "achipanaa");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error en el envío individual: {ex.Message}");

                            continue;

                        }

                    }
                    else
                    {
                        continue;

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el envío masivo: {ex.Message}");
                return false;
            }
        }



        public bool EnviarSmsPrueba(PruebaPlantillaSmsDTO datosEnvio, string usuario)
        {
            try
            {
                var configuracionEnvio = this.ObtenerConfiguracionEnvio();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(configuracionEnvio.usuario + ":" + configuracionEnvio.pass); string decobase64 = Convert.ToBase64String(plainTextBytes);

                using (var httpClient = new WebClient())
                {
                    httpClient.Headers[HttpRequestHeader.Authorization] = "Basic " + decobase64;
                    httpClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string jsonContent;

                    if (datosEnvio.ShortUrlConfig == false)
                    {
                        jsonContent = JsonConvert.SerializeObject(new
                        {
                            to = datosEnvio.Celular,
                            text = datosEnvio.Text,
                            isPremium = datosEnvio.IsPremium,
                            isFlash = datosEnvio.IsFlash,
                            isLongmessage = datosEnvio.IsLongmessage,
                        });
                    }
                    else
                    {
                        jsonContent = JsonConvert.SerializeObject(new
                        {
                            to = datosEnvio.Celular,
                            text = datosEnvio.Text,
                            isPremium = datosEnvio.IsPremium,
                            isFlash = datosEnvio.IsFlash,
                            isLongmessage = datosEnvio.IsLongmessage,
                            shortUrlConfig = new
                            {
                                url = datosEnvio.Url,
                                domainShorturl = "http://ma.sv/"
                            }
                        });
                    }

                    var response = httpClient.UploadString(apiUrl, jsonContent);

                    var resultado = JsonConvert.DeserializeObject<respuestaMensajeSmsHook>(response);

                    PruebaPlantillaSmsDTO insertDto = new PruebaPlantillaSmsDTO
                    {
                        Celular = datosEnvio.Celular,
                        Text = datosEnvio.Text,
                        CustomData = "CUS_ID_0125",
                        IsPremium = datosEnvio.IsPremium,
                        IsFlash = datosEnvio.IsFlash,
                        IsLongmessage = datosEnvio.IsLongmessage,
                        IsRandomRoute = datosEnvio.IsRandomRoute,
                        ShortUrlConfig = datosEnvio.ShortUrlConfig,
                        Url = datosEnvio.Url,
                        DomainShortUrl = "http://ma.sv/",
                        MessageId = resultado.messageId,
                        Usuario = usuario
                    };

                    _unitOfWork.CampaniaGeneralSmsRepository.InsertarPruebaPlantillaSms(insertDto);


                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el envío masivo: {ex.Message}");
                return false;
            }
        }


        public bool EnviarMensajeUnitario(PruebaPlantillaSmsDTO datosEnvio, string usuario)
        {
            try
            {
                var configuracionEnvio = this.ObtenerConfiguracionEnvio();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(configuracionEnvio.usuario + ":" + configuracionEnvio.pass); string decobase64 = Convert.ToBase64String(plainTextBytes);

                using (var httpClient = new WebClient())
                {
                    httpClient.Headers[HttpRequestHeader.Authorization] = "Basic " + decobase64;
                    httpClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    string jsonContent;

                    if (datosEnvio.ShortUrlConfig == false)
                    {
                        jsonContent = JsonConvert.SerializeObject(new
                        {
                            to = datosEnvio.Celular,
                            text = datosEnvio.Text,
                            isPremium = datosEnvio.IsPremium,
                            isFlash = datosEnvio.IsFlash,
                            isLongmessage = datosEnvio.IsLongmessage,
                        });
                    }
                    else
                    {
                        jsonContent = JsonConvert.SerializeObject(new
                        {
                            to = datosEnvio.Celular,
                            text = datosEnvio.Text,
                            isPremium = datosEnvio.IsPremium,
                            isFlash = datosEnvio.IsFlash,
                            isLongmessage = datosEnvio.IsLongmessage,
                            shortUrlConfig = new
                            {
                                url = datosEnvio.Url,
                                domainShorturl = "http://ma.sv/"
                            }
                        });
                    }

                    var response = httpClient.UploadString(apiUrl, jsonContent);

                    var resultado = JsonConvert.DeserializeObject<respuestaMensajeSmsHook>(response);

                    PruebaPlantillaSmsDTO insertDto = new PruebaPlantillaSmsDTO
                    {
                        Celular = datosEnvio.Celular,
                        Text = datosEnvio.Text,
                        CustomData = "CUS_ID_0125",
                        IsPremium = datosEnvio.IsPremium,
                        IsFlash = datosEnvio.IsFlash,
                        IsLongmessage = datosEnvio.IsLongmessage,
                        IsRandomRoute = datosEnvio.IsRandomRoute,
                        ShortUrlConfig = datosEnvio.ShortUrlConfig,
                        Url = datosEnvio.Url,
                        DomainShortUrl = "http://ma.sv/",
                        MessageId = resultado.messageId,
                        Usuario = usuario
                    };

                    _unitOfWork.CampaniaGeneralSmsRepository.InsertarPruebaPlantillaSms(insertDto);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el envío masivo: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> EnviarSmsMasivoPorBatch(List<SmsEnviarMensajeDTO> datosEnvio)
        {
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
                string decobase64 = Convert.ToBase64String(plainTextBytes);

                using (var httpClient = new WebClient())
                {
                    httpClient.Headers[HttpRequestHeader.Authorization] = "Basic " + decobase64;
                    httpClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    const int batchSize = 100;

                    var batches = datosEnvio.Select((item, index) => new { Item = item, Index = index })
                                            .GroupBy(x => x.Index / batchSize)
                                            .Select(group => group.Select(x => x.Item).ToList())
                                            .ToList();

                    foreach (var batch in batches)
                    {
                        var mensajes = new List<object>();

                        foreach (var envio in batch)
                        {
                            var mensaje = new
                            {
                                to = envio.CelularSms,
                                text = envio.Text,
                                customData = envio.Customdata,
                                isPremium = envio.IsPremium,
                                isFlash = envio.IsFlash,
                                isLongMessage = envio.isLongmessage,
                                shortUrlConfig = new
                                {
                                    url = envio.Url,
                                    domainShorturl = envio.DomainShorturl
                                }
                            };

                            mensajes.Add(mensaje);
                        }

                        var jsonContent = JsonConvert.SerializeObject(mensajes);

                        var response = httpClient.UploadString(apiUrl, jsonContent);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el envío masivo: {ex.Message}");
                return false;
            }
        }


        public List<GrillaSms> ObtenerGrillaSms(int tab, int dia)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerGrillaSms(tab, dia);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatSms> ObtenerChatPorAlumno(string celular)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerChatPorAlumno(celular);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DatosAlumno> ObtenerAlumnoPorCelular(string celular)
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerAlumnoPorCelular(celular);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CondifuracionEnvioSmsDTO ObtenerConfiguracionEnvio()
        {
            try
            {

                var dto = _unitOfWork.CampaniaGeneralSmsRepository.ObtenerConfiguracionEnvio();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}
