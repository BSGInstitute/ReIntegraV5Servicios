using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.Classes;
using Microsoft.Data.SqlClient;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using Nancy.Json;
using iText.Commons.Utils;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: ConfiguracionResumenGrabacionOnlineService
    /// Autor: Jorge Gamero
    /// Fecha: 28/01/2025
    /// <summary>
    /// Gestión general de T_ConfiguracionResumenGrabacionOnline
    /// </summary>
    public class ConfiguracionResumenGrabacionOnlineService : IConfiguracionResumenGrabacionOnlineService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ConfiguracionResumenGrabacionOnlineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionResumenGrabacionOnline, ConfiguracionResumenGrabacionOnline>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public List<ConfiguracionResumenGrabacionOnline> Add(List<ConfiguracionResumenGrabacionOnline> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionResumenGrabacionOnline>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza activando registro de T_ConfiguracionResumenGrabacionOnline
        /// </summary> 
        /// <returns> bool </returns>
        public bool ActualizaActivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario)
        {
            try
            {
                _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ActualizaActivo(listadoIds, idPEspecificoSesion, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza desactivando registro de T_ConfiguracionResumenGrabacionOnline
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizaInactivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario)
        {
            try
            {
                _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ActualizaInactivo(listadoIds, idPEspecificoSesion, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuración para resúmenes de videos de T_ConfiguracionResumenGrabacionOnline
        /// </summary>
        /// <returns> IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> </returns>
        public IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> ObtenerConfiguracionResumenGrabacionOnlinePorSesion(int idPEspecificoSesion)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ObtenerConfiguracionResumenGrabacionOnlinePorSesion(idPEspecificoSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Proceso para generar resúmenes de grabaciones
        /// </summary>
        /// <returns> Task<(string resultado, HttpStatusCode statusCode)> </returns>
        public async Task<(string resultado, HttpStatusCode statusCode)> GenerarResumenGrabaciones(IniciarProcesoResumenGrabacionesDTO datos)
        {
            try
            {
                var idResumenExistente = ConsultaRegistroResumenPordPEspecificoSesion(datos.IdPEspecificoSesion)?.FirstOrDefault()?.Id ?? 0;

                if (idResumenExistente != 0) //Elimina registro de tablas T_ProcesamientoSesionOnline y sus outputs en T_ProcesamientoTipoGenerar por Id
                {
                    EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(idResumenExistente, datos.Usuario);
                    EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(idResumenExistente, datos.Usuario);
                }

                datos.UrlVideo = ConstruirUrlVimeo(datos.UrlVideo);
                var urlBase = "http://ia-proceso-resumen-sesiones-api.bsginstitute.com/api/v1/workflow/batch";
                var proceso = new List<IniciarProcesoResumenGrabacionesDTO>
                {
                    new IniciarProcesoResumenGrabacionesDTO
                    {
                        IdPEspecifico = datos.IdPEspecifico,
                        IdPEspecificoSesion = datos.IdPEspecificoSesion,
                        TipoResumenGrabacionOnline = datos.TipoResumenGrabacionOnline,
                        Sesion = datos.Sesion,
                        UrlVideo = datos.UrlVideo,
                        Usuario = datos.Usuario
                    }
                };

                using (HttpClient client = new HttpClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(proceso);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(urlBase, content);
                    var jsonString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return (jsonString, response.StatusCode);
                    }
                    else
                    {
                        return ($"Error al procesar la respuesta del servidor: {response.ReasonPhrase}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return ($"Excepción: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id de registro en T_ProcesamientoSesionOnline filtrado por idPEspecificoSesion
        /// </summary>
        /// <returns> string </returns>
        public IEnumerable<RegistroProcesamientoSesionOnlineDTO> ConsultaRegistroResumenPordPEspecificoSesion(int idPEspecificoSesion)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ConsultaRegistroResumenPordPEspecificoSesion(idPEspecificoSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/04/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina registro de tabla T_ProcesamientoSesionOnline de manera lógica por filtro de id
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(int id, string usuario)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/04/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina registro de tabla T_ProcesamientoTipoGenerar de manera lógica por filtro de idProcesamientoSesionOnline
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(int idProcesamientoSesionOnline, string usuario)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(idProcesamientoSesionOnline, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene formato de URL Vimeo a partir de código embebido
        /// </summary>
        /// <returns> string </returns>
        private string ConstruirUrlVimeo(string urlEmbebida)
        {
            if (string.IsNullOrWhiteSpace(urlEmbebida)) return "";
            var match = Regex.Match(urlEmbebida, @"^(\d+)\?h=([a-zA-Z0-9]+)$");
            return match.Success ? $"https://vimeo.com/{match.Groups[1].Value}/{match.Groups[2].Value}" : "";
        }

        /// Autor: Jorge Gamero
        /// Fecha: 07/02/2025
        /// Version: 1.0
        /// <summary>
        /// Proceso para enviar resúmenes según configuración de matriculados
        /// </summary>
        /// <returns>  </returns>
        public async Task<bool> EnvioResumenGrabaciones(int idPEspecificoSesion)
        {
            try
            {
                var correoRegistroUrl = ObtenerConfiguracionResumenGrabacionesEnvioCorreo(idPEspecificoSesion).ToList();
                var whatsAppRegistroUrl = ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(idPEspecificoSesion).ToList();
                var idPlantilla = 1927; // Id referencia a plantilla de WhatsApp en tabla mkt.T_Plantilla
                //const string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphAtc";
                const string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphAtc";
                var serializer = new JavaScriptSerializer();

                HelperCorreo helperCorreo = new HelperCorreo();
                var gruposCorreo = correoRegistroUrl.GroupBy(x => x.IdMatriculaCabecera);

                foreach (var grupo in gruposCorreo)
                {
                    string mensaje = GetEmailTemplate(grupo.ToList());
                    string tempFilePathAudio = string.Empty;
                    string tempFilePathPdf = string.Empty;
                    List<string> tempFiles = new List<string>();

                    foreach (var itemResumen in grupo.ToList())
                    {
                        // Obtener el nombre del archivo de la URL
                        string fileName = Path.GetFileName(new Uri(itemResumen.RegistroUrl).LocalPath);
                        string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

                        if (!File.Exists(tempFilePath)) {
                            // Descargar el archivo
                            using (WebClient webClient = new WebClient())
                            {
                                await webClient.DownloadFileTaskAsync(new Uri(itemResumen.RegistroUrl), tempFilePath);
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo ya existe, no se descargó nuevamente.");
                        }

                        if(!string.IsNullOrEmpty(tempFilePath))
                        {
                            tempFiles.Add(tempFilePath);
                        }
                        //if (File.Exists(tempFilePath))
                        //{
                        //    tempFilePathPdf = Path.Combine(tempFilePath, fileName);
                        //}
                    }

                    //helperCorreo.envioEmailResumenGrabacion("jrivera@bsginstitute.com", "BSG Institute", "BSG Institute: Repaso de tu última clase dictada", mensaje, new List<string>(), tempFiles);
                    helperCorreo.envioEmailResumenGrabacion(grupo.First().Correo, "BSG Institute", "BSG Institute: Repaso de tu última clase dictada", mensaje, new List<string>(), tempFiles);
                }

                // se realiza primero un envio de plantilla
                //var _registroAgenda = whatsAppRegistroUrl.Select(x=>x).FirstOrDefault();
                IAgendaService agendaService = new AgendaService(_unitOfWork);
                var gruposWhatsApp = whatsAppRegistroUrl.GroupBy(x => x.IdMatriculaCabecera);
                foreach (var grupo in gruposWhatsApp)
                {
                    var _registroAgenda = grupo.FirstOrDefault();
                    try
                    {
                        var resultadoProceso = agendaService.GenerarPlantillaWhatsappResumenGrabaciones(_registroAgenda.IdOportunidad, idPlantilla, _registroAgenda.IdResumenGrabacionOnline, idPEspecificoSesion, _registroAgenda.IdProcesamientoTipoGenerar);

                        foreach (var itemREgistro in resultadoProceso.ListaEtiquetas)
                        {
                            if (itemREgistro.Codigo != "{tAlumnos.nombre1}" && itemREgistro.Codigo != "{tPLA_PGeneral.Nombre}")
                            {
                                itemREgistro.Texto = "";
                            }
                        }

                        if (resultadoProceso != null)
                        {
                            MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();
                            objetoWhatsAppHook.Id = 0;
                            objetoWhatsAppHook.WaTo = _registroAgenda.IdCodigoPais + _registroAgenda.Celular;
                            objetoWhatsAppHook.WaId = "232249556631957";
                            objetoWhatsAppHook.WaType = "hsm";
                            objetoWhatsAppHook.WaTypeMensaje = 8;
                            objetoWhatsAppHook.WaRecipientType = "string";
                            objetoWhatsAppHook.WaBody = "envio_resumen_grabaciones";
                            objetoWhatsAppHook.WaFile = null;
                            objetoWhatsAppHook.WaFileName = null;
                            objetoWhatsAppHook.WaMimeType = null;
                            objetoWhatsAppHook.WaSha256 = null;
                            objetoWhatsAppHook.WaLink = null;
                            objetoWhatsAppHook.WaCaption = resultadoProceso.Plantilla;
                            objetoWhatsAppHook.IdPais = 0;
                            objetoWhatsAppHook.EsMigracion = true;
                            objetoWhatsAppHook.IdMigracion = 0;
                            objetoWhatsAppHook.IdPersonal = 4477;
                            objetoWhatsAppHook.IdAlumno = _registroAgenda.IdAlumno;
                            objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                            objetoWhatsAppHook.imagen = null;
                            objetoWhatsAppHook.botones = null;
                            objetoWhatsAppHook.DatosPlantillaWhatsApp = resultadoProceso.ListaEtiquetas
                                .Select(e => new DatosPlantillaWhatsAppDTO
                                {
                                    codigo = e.Codigo,
                                    texto = e.Texto
                                }).ToList();

                            var serializedResult = serializer.Serialize(objetoWhatsAppHook);

                            var task = UrlPostAsync(url, serializedResult);
                        }
                    }
                    catch (Exception exec)
                    {

                    }
                }
                
                //esta funcion debe mandar los resumenes pdf y audio (el audio se esta viendo como mandar)
                //foreach (var dataWhatsApp in whatsAppRegistroUrl)
                //{
                //    var resultado = agendaService.GenerarPlantillaWhatsappResumenGrabaciones(dataWhatsApp.IdOportunidad, idPlantilla, dataWhatsApp.IdResumenGrabacionOnline, idPEspecificoSesion, dataWhatsApp.IdProcesamientoTipoGenerar);
                //    if (resultado != null)
                //    {
                //        if (dataWhatsApp.RegistroUrl.Contains(".pdf"))
                //        {
                //            MktWhatsAppEnviarMensajeDTO objetoWhatsAppHook = new MktWhatsAppEnviarMensajeDTO();
                //            objetoWhatsAppHook.Id = 0;
                //            objetoWhatsAppHook.WaTo = "+51963382772";// dataWhatsApp.IdCodigoPais + dataWhatsApp.Celular;
                //            objetoWhatsAppHook.WaId = "232249556631957";
                //            objetoWhatsAppHook.WaType = "template";
                //            objetoWhatsAppHook.WaTypeMensaje = 8;
                //            objetoWhatsAppHook.WaRecipientType = "string";
                //            objetoWhatsAppHook.WaBody = "resumen_documento_sesion";
                //            objetoWhatsAppHook.WaFile = null;
                //            objetoWhatsAppHook.WaFileName = null;
                //            objetoWhatsAppHook.WaMimeType = null;
                //            objetoWhatsAppHook.WaSha256 = null;
                //            objetoWhatsAppHook.WaLink = dataWhatsApp.RegistroUrl;
                //            objetoWhatsAppHook.WaCaption = resultado.Plantilla;
                //            objetoWhatsAppHook.IdPais = 0;
                //            objetoWhatsAppHook.EsMigracion = true;
                //            objetoWhatsAppHook.IdMigracion = 0;
                //            objetoWhatsAppHook.IdPersonal = 4477;
                //            objetoWhatsAppHook.IdAlumno = dataWhatsApp.IdAlumno;
                //            objetoWhatsAppHook.usuario = "WhatsAppMasivoPlantilla";
                //            objetoWhatsAppHook.imagen = null;
                //            objetoWhatsAppHook.botones = null;
                //            objetoWhatsAppHook.DatosPlantillaWhatsApp = resultado.ListaEtiquetas
                //                .Select(e => new DatosPlantillaWhatsAppDTO
                //                {
                //                    codigo = e.Codigo,
                //                    texto = e.Texto
                //                }).ToList();

                //            var serializedResult = serializer.Serialize(objetoWhatsAppHook);

                //            var task = UrlPostAsync(url, serializedResult);
                //        }
                //        else
                //        {

                //        }
                            
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GenerarMensajeResumenGrabacion(int idMatriculaCabecera, List<ConfiguracionResumenGrabacionesEnvioCorreoDTO> registros)
        //{
        //    var nombre = registros.First().Nombre1;
        //    var nombreEspecifico = registros.First().NombreEspecifico;
        //    var nombreResumen = registros.First().NombreResumen;

        //    StringBuilder mensaje = new StringBuilder();
        //    mensaje.AppendLine($"<p>Hola {nombre}.</p>");
        //    mensaje.AppendLine($"<p>Aquí tienes el resumen de {nombreEspecifico} en:</p>");
        //    foreach (var registro in registros)
        //    {
        //        mensaje.AppendLine($"<p>- {registro.NombreResumen}:</p>");
        //        mensaje.AppendLine($"<p><a href='{registro.RegistroUrl}'>{registro.RegistroUrl}</a></p>");
        //    }
        //    mensaje.AppendLine("<p>Esperamos que disfrutes del material!</p>");
        //    mensaje.AppendLine("<p>Ten buen día.</p>");
        //    mensaje.AppendLine("<p>BSG Institute</p>");

        //    return mensaje.ToString();
        //}

        public static string GetEmailTemplate(List<ConfiguracionResumenGrabacionesEnvioCorreoDTO> registros)
        {
            string studentName = registros.First().Nombre1;
            string courseName = registros.First().NombreGeneral;
            string lessonTitle = "";
            string nombreAsesor = registros.First().NombreAsesorCompleto;
            string rolAsesor = registros.First().RolAsesor;
            DateTime lessonDate;

            string _mensaje = string.Empty;

            _mensaje = @$"<!DOCTYPE html>
                        <html lang=""es"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Correo BSG Institute</title>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                    margin: 0;
                                    padding: 0;
                                    background: #ebecf0;
                                }}
                                .container {{
                                    max-width: 600px;
                                    margin: 20px auto;
                                    background: #ffffff;
                                    border: 0px solid #ddd;
                                    border-radius: 0px;
                                    overflow: hidden;
                                }}
                                .header {{
                                    background-color: #0866ff;
                                    color: #ffffff;
                                    text-align: center;
                                    padding: 20px;
                                    font-size: 24px;
                                }}
                                .header img {{
                                    width: 30px;
                                    height: 30px;
                                }}
                                .content {{
                                    padding: 20px;
                                    color: #333333;
                                }}
                                .content h1 {{
                                    font-size: 24px;
                                    color: #0056b3;
                                }}
                                .content p {{
                                    font-size: 16px;
                                    line-height: 1.5;
                                }}
                                .footer {{
                                    background-color: #ebecf0;
                                    text-align: center;
                                    padding: 20px;
                                    font-size: 12px;
                                    color: #a9b0c8;
                                }}
                                .footer a {{
                                    color: #a9b0c8;
                                    text-decoration: underline;
                                }}
                                .footer .social-icons {{
                                    margin: 10px 0;
                                }}
                                .footer .social-icons img {{
                                    width: 24px;
                                    margin: 0 5px;
                                }}
                                .footer .locations {{
                                    margin-top: 10px;
                                    font-size: 12px;
                                    line-height: 1.5;
                                    color: #a9b0c8;
                                }}
                                .footer .locations p {{
                                    margin-top: 4px;
                                    margin-bottom: 0px;
                                }}
                            </style>
                        </head>
                        <body style=""background: #ebecf0"">
                            <div class=""container"">
                                <div class=""header"">
                                    <img src=""https://bsginstitute.com/favicon.ico"" alt=""BSG Institute""><span style=""margin-left: 10px;"">BSG Institute</span>
                                </div>
                                <div class=""content"">
                                    <h1>Hola {studentName},</h1>
                                    <p>
                                        Queremos ayudarte a reforzar los conocimientos adquiridos en la sesión 
                                        <strong>{lessonTitle}</strong> de tu curso <strong>{courseName}</strong>.
                                    </p>
                                    <p>
                                        Por ello, te envío adjunto el <strong>documento</strong> y el <strong>archivo de audio</strong> con el resumen de los temas más importantes que vimos en clase. Sabemos que revisar estos contenidos puede ser de gran utilidad para consolidar tu aprendizaje.
                                    </p>
                                    <p>
                                        Si tienes alguna pregunta o necesitas más información, estoy aquí para ayudarte, en los siguientes canales y horarios de atención:
                                    </p>
                                    <ul>
                                        <li><strong>Número Telefónico:</strong> (51) 54 258787 Anexo 1366</li>
                                        <li><strong>Número WhatsApp:</strong> +51 992 651 774</li>
                                        <li><strong>Horario de atención (Hora de Perú):</strong></li>
                                        <ul>
                                            <li>Lunes a viernes: 09:00 a 14:00 y de 15:00 a 19:00 horas</li>
                                            <li>Sábado: 09:00 a 14:00 horas</li>
                                        </ul>
                                    </ul>
                                    <p>Atentamente,</p>
                                    <p><strong>{nombreAsesor}</strong><br>{rolAsesor}<br>BSG Institute</p>
                                </div>
                                <div class=""footer"">
                                    <p>
                                        <a href=""https://bsginstitute.com/termino-uso-web"">Términos de uso</a> | <a href=""https://bsginstitute.com/politica-privacidad"">Política de privacidad</a><br>
                                        ©2023 BSG Institute, todos los derechos reservados<br>
                                        <a href=""https://www.bsginstitute.com"">www.bsginstitute.com</a>
                                    </p>
                                    <div class=""social-icons"">
                                        <a href=""https://www.facebook.com/BSGInstituteOficial/""><img src=""https://cdn-icons-png.flaticon.com/512/733/733547.png"" alt=""Facebook""></a>
                                        <a href=""https://www.instagram.com/bsg_institute/""><img src=""https://cdn-icons-png.flaticon.com/512/733/733558.png"" alt=""Instagram""></a>
                                        <a href=""https://www.youtube.com/user/BSGRUPOsac""><img src=""https://cdn-icons-png.flaticon.com/512/733/733646.png"" alt=""YouTube""></a>
                                        <a href=""https://twitter.com/BSG_Institute""><img src=""https://cdn-icons-png.flaticon.com/512/733/733579.png"" alt=""X (Twitter)""></a>
                                        <a href=""https://pe.linkedin.com/school/bsg-institute/""><img src=""https://cdn-icons-png.flaticon.com/512/733/733561.png"" alt=""LinkedIn""></a>
                                    </div>
                                    <div class=""locations"">
                                        <p><strong>Arequipa</strong> - Urb. León XIII, Calle 2 N° 107 - Cayma</p>
                                        <p><strong>Lima</strong> - Av. Alfredo Benavides N° 768, Oficina N° 401 - Centro Empresarial Reducto, Miraflores</p>
                                        <p><strong>Bogotá</strong> - Av. Carrera 45 N° 108-27, Torre N° 1, Oficina N° 1002</p>
                                        <p><strong>Ciudad de México</strong> - Av. Patriotismo N° 229, Piso 7 Oficina N° 216</p>
                                        <p><strong>Santiago de Chile</strong> - Av. Manquehue Norte N° 151, Oficina N° 1205 - Las Condes</p>
                                    </div>
                                </div>
                            </div>
                        </body>
                        </html>";

            return _mensaje;
        }

        public IEnumerable<ConfiguracionResumenGrabacionesEnvioCorreoDTO> ObtenerConfiguracionResumenGrabacionesEnvioCorreo(int idPEspecificoSesion)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ObtenerConfiguracionResumenGrabacionesEnvioCorreo(idPEspecificoSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO> ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(int idPEspecificoSesion)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(idPEspecificoSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();

            try
            {
                string LimpiarTexto(string texto)
                {
                    Dictionary<string, string> reemplazos = new Dictionary<string, string>
                    {
                        {"á", "a"}, {"é", "e"}, {"í", "i"}, {"ó", "o"}, {"ú", "u"},
                        {"Á", "A"}, {"É", "E"}, {"Í", "I"}, {"Ó", "O"}, {"Ú", "U"},
                        {"ñ", "n"}, {"Ñ", "N"}, {"?", " "} // Espacio en lugar de "?"
                    };

                    foreach (var par in reemplazos)
                    {
                        texto = texto.Replace(par.Key, par.Value);
                    }

                    return texto;
                }

                string parsedContent = LimpiarTexto(jsonStringResult);

                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

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

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene campo TextoTranscripcion de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerTextoTranscripcionPorId(int id)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ObtenerTextoTranscripcionPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene campo TextoGuionAudio de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerTextoGuionAudioPorId(int id)
        {
            try
            {
                return _unitOfWork.ConfiguracionResumenGrabacionOnlineRepository.ObtenerTextoGuionAudioPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        //{
        //    RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
        //    try
        //    {
        //        string ConvertirTildesAEntidadesHtml(string texto)
        //        {
        //            Dictionary<char, string> htmlEntities = new Dictionary<char, string>
        //            {
        //                {'á', "&aacute;"}, {'é', "&eacute;"}, {'í', "&iacute;"},
        //                {'ó', "&oacute;"}, {'ú', "&uacute;"}, {'Á', "&Aacute;"},
        //                {'É', "&Eacute;"}, {'Í', "&Iacute;"}, {'Ó', "&Oacute;"},
        //                {'Ú', "&Uacute;"}
        //            };

        //            foreach (var entidad in htmlEntities)
        //            {
        //                texto = texto.Replace(entidad.Key.ToString(), entidad.Value);
        //            }

        //            return texto;
        //        }

        //        // Convertir el JSON completo
        //        string parsedContent = ConvertirTildesAEntidadesHtml(jsonStringResult);

        //        var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
        //        http.Accept = "application/json";
        //        http.ContentType = "application/json";
        //        http.Method = "POST";

        //        ASCIIEncoding encoding = new ASCIIEncoding();
        //        Byte[] bytes = encoding.GetBytes(parsedContent);

        //        Stream newStream = await http.GetRequestStreamAsync();
        //        newStream.Write(bytes, 0, bytes.Length);
        //        newStream.Close();

        //        var response = await http.GetResponseAsync();
        //        var stream = response.GetResponseStream();
        //        var sr = new StreamReader(stream);
        //        var content = await sr.ReadToEndAsync();

        //        respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(content);

        //        return respuestaMensajeHook;
        //    }
        //    catch (Exception ex)
        //    {
        //        return respuestaMensajeHook;
        //    }
        //}

    }
}