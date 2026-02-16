using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AlumnoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Alumno
    /// </summary>
    public class AlumnoService : IAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private Alumno _alumno;
        public Alumno Alumno { get => _alumno; set => _alumno = value; }

        public AlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAlumno, Alumno>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAlumno, AlumnoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<AlumnoDTO, Alumno>(MemberList.None).ReverseMap();
                cfg.CreateMap<AlumnoFormularioOportunidadDTO, Alumno>(MemberList.None);
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Alumno Add(Alumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Alumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Alumno Update(Alumno entidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Alumno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Alumno> Update(List<AlumnoDTO> entidad)
        {
            try
            {
                var mapeado = _mapper.Map<List<TAlumno>>(entidad);
                var modelo = _unitOfWork.AlumnoRepository.Update(mapeado);
                _unitOfWork.Commit();
                return _mapper.Map<List<Alumno>>(mapeado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.AlumnoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Alumno> Add(List<Alumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Alumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Alumno> Update(List<Alumno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AlumnoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Alumno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.AlumnoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Alumno asociado a un Id.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDTO </returns>
        public Alumno ObtenerPorId(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Alumno para mostrarse en combo.
        /// </summary>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Alumno</param>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerAutocomplete(string nombreParcial)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerAutocomplete(nombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 08/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de Alumnos basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Alumno</param>
        /// <returns> List<AlumnoComboDTO> </returns>
        public IEnumerable<AlumnoComboDTO> ObtenerAlumnoMatriculadoAutocomplete(string nombreParcial)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerAlumnoMatriculadoAutocomplete(nombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdPais de un Alumno basado en el CodigoPais
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> ValorIntDTO </returns>
        public IntDTO ObtenerIdPaisPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerIdPaisPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener IdCiudad y IdPais de un Alumno segun su Id
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoCiudadPaisDTO </returns>
        public AlumnoCiudadPaisDTO ObtenerCiudadPaisPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCiudadPaisPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Datos de Alumno para Documento
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> AlumnoDatosDocumentoDTO </returns>
        public AlumnoDatosDocumentoDTO ObtenerDatosDocumentoPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerDatosDocumentoPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StringDTO RegistrarLoginPortal(int idAlumno, string usuario)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.RegistrarLoginPortal(idAlumno, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion del Alumno asociados a una Clasificacion Persona
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                var informacion = _unitOfWork.AlumnoRepository.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);
                informacion.EmailOriginal = informacion.Email1;

                //if(!string.IsNullOrWhiteSpace(informacion.Email1))
                //    informacion.Email1=EncriptarCorreoHash(informacion.Email1);

                if (informacion.IdCargo == null) informacion.IdCargo = 11;
                if (informacion.IdIndustria == null) informacion.IdIndustria = 48;
                if (informacion.DNI == null) informacion.DNI = "";
                
                //if (!string.IsNullOrWhiteSpace(informacion.Email1))
                //    informacion.Email1 = EncriptarCorreoHash(informacion.Email1);
                //if (!string.IsNullOrWhiteSpace(informacion.Celular))
                //    informacion.Celular = EncriptarNumeroHash(informacion.Celular);
              

                return informacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdClasificacionPersonaOperaciones(int idClasificacionPersona)
        {
            try
            {
                var informacion = _unitOfWork.AlumnoRepository.ObtenerInformacionAlumnoPorIdClasificacionPersona(idClasificacionPersona);
                informacion.EmailOriginal = informacion.Email1;

                //if(!string.IsNullOrWhiteSpace(informacion.Email1))
                //    informacion.Email1=EncriptarCorreoHash(informacion.Email1);

                if (informacion.IdCargo == null) informacion.IdCargo = 11;
                if (informacion.IdIndustria == null) informacion.IdIndustria = 48;
                if (informacion.DNI == null) informacion.DNI = "";

                if (!string.IsNullOrWhiteSpace(informacion.Email1))
                    informacion.Email1 = EncriptarCorreoHash(informacion.Email1);
                if (!string.IsNullOrWhiteSpace(informacion.Email2))
                    informacion.Email2 = EncriptarCorreoHash(informacion.Email2);
                if (!string.IsNullOrWhiteSpace(informacion.Celular))
                    informacion.Celular = EncriptarNumeroHash(informacion.Celular);
                if (!string.IsNullOrWhiteSpace(informacion.Celular2))
                    informacion.Celular = EncriptarNumeroHash(informacion.Celular2);


                return informacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ciudad de origen del _alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public string ObtenerCiudadOrigen(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCiudadOrigen(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el pais de origen del _alumno
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> string </returns>
        public string ObtenerPaisOrigen(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerPaisOrigen(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene si ya se ha enviado en el mismo dia un mensaje
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="fecha">Fecha para Busqueda</param>
        /// <returns> EnvioSMSOportunidad </returns>
        public EnvioSMSOportunidad Obtener_EnvioSMSPorDiaOportunidad(int idOportunidad, DateTime fecha)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.Obtener_EnvioSMSPorDiaOportunidad(idOportunidad, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia en la tabla mkt.T_SmsMensajeEnviado
        /// </summary>
        /// <param name="celular">Celular al que se envia el mensaje</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="idAlumno">Id del _alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="mensaje">Mensaje a enviar</param>
        /// <param name="parteMensaje">Parte del mensaje seccionado</param>
        /// <param name="idPais">Id del pais (PK de la tabla gp.T_Pais)</param>
        /// <returns> bool </returns>
        public bool InsertaSMSOportunidadUsuario(string celular, int idPersonal, int idAlumno, string mensaje, int parteMensaje, int idPais, string usuario)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.InsertaSMSOportunidadUsuario(celular, idPersonal, idAlumno, mensaje, parteMensaje, idPais, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta el envio de SMS por oportunidad y dia
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="fechaEnvio">Fecha en Envio del Mensaje</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO InsertaSMSOportunidad(int idOportunidad, DateTime fechaEnvio)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.InsertaSMSOportunidad(idOportunidad, fechaEnvio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nro de celular del contacto mas la extension del pais
        /// </summary>
        /// <returns></returns>
        public string ObtenerNroCelularCompleto(int idCodigoPais, string celular)
        {
            try
            {
                var numeroCelular = "";
                if (!string.IsNullOrEmpty(celular))
                {
                    numeroCelular = celular;
                    numeroCelular = numeroCelular.TrimStart('0');
                    if (idCodigoPais == 591 && numeroCelular.StartsWith("591"))
                    {
                        numeroCelular = numeroCelular.Length > 11 ? numeroCelular.Substring(0, 11) : numeroCelular;
                    }
                    else if (idCodigoPais == 591 && !numeroCelular.StartsWith("591"))
                    {
                        numeroCelular = numeroCelular.Length > 8 ? numeroCelular.Substring(0, 8) : numeroCelular;
                        numeroCelular = string.Concat("591", numeroCelular);
                    }
                    else if (idCodigoPais == 57 && numeroCelular.StartsWith("57"))
                    {
                        numeroCelular = numeroCelular.Length > 12 ? numeroCelular.Substring(0, 12) : numeroCelular;
                    }
                    else if (idCodigoPais == 57 && !numeroCelular.StartsWith("57"))
                    {
                        numeroCelular = numeroCelular.Length > 10 ? numeroCelular.Substring(0, 10) : numeroCelular;
                        numeroCelular = string.Concat("57", numeroCelular);
                    }
                    else if (idCodigoPais == 51 && numeroCelular.StartsWith("51"))
                    {
                        numeroCelular = numeroCelular.Length > 11 ? numeroCelular.Substring(0, 11) : numeroCelular;
                    }
                    else if (idCodigoPais == 51 && !numeroCelular.StartsWith("51"))
                    {
                        numeroCelular = numeroCelular.Length > 9 ? numeroCelular.Substring(0, 9) : numeroCelular;
                        numeroCelular = string.Concat("51", numeroCelular);
                    }
                }
                return numeroCelular;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="dni">DNI del Alumno</param>
        /// <returns>Configura el IdEstadoWhatsApp del _alumno</returns>
        public Alumno ValidarEstadoContactoWhatsAppTemporalAlterno(Alumno alumno)
        {
            try
            {
                string urlToPost;
                bool banderaLogin = false;
                string tokenComunicacion = string.Empty;
                var idPersonal = 4589;//TODO
                var nroCelularCompleto = ObtenerNroCelularCompleto(alumno.IdCodigoPais.Value, alumno.Celular);
                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                DTO.contacts.Add("+" + nroCelularCompleto);
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                // Correccion temporal
                int[] listaIdPaisesServidoresDedicados = new int[3] { 51/*Peru*/, 57/*Colombia*/, 591 /*Bolivia*/};

                var credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(listaIdPaisesServidoresDedicados.Contains(alumno.IdCodigoPais.Value) ? alumno.IdCodigoPais.Value : 0/*Internacional*/);
                var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(idPersonal, alumno.IdCodigoPais.Value);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);

                string resultado = string.Empty;

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = credencialesHost.UrlWhatsApp + "/v1/users/login";
                    var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(idPersonal);

                    var client = new RestClient(urlToPostUsuario);
                    var request = new RestSharp.RestRequest("/v1/users/login", Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Length", "");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request.AddHeader("Content-Type", "application/json");
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        foreach (var item in datos.users)
                        {
                            var modelCredencial = new WhatsAppUsuarioCredencial
                            {
                                IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                IdWhatsAppConfiguracion = credencialesHost.Id,
                                UserAuthToken = item.token,
                                ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                EsMigracion = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "whatsapp",
                                UsuarioModificacion = "whatsapp"
                            };
                            var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Add(modelCredencial);
                            _unitOfWork.Commit();
                            tokenComunicacion = item.token;
                        }
                        banderaLogin = true;
                    }
                    else
                    {
                        banderaLogin = false;
                    }
                }
                else
                {
                    tokenComunicacion = tokenValida.UserAuthToken;
                    banderaLogin = true;
                }

                urlToPost = credencialesHost.UrlWhatsApp + "/v1/contacts";

                if (banderaLogin)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                    var resultadoEstadoContactoWhatsApp = datoRespuesta.contacts.FirstOrDefault();
                    if (datoRespuesta == null)
                    {
                        alumno.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                    }
                    else
                    {
                        alumno.IdEstadoContactoWhatsApp = resultadoEstadoContactoWhatsApp.status == "invalid" ? ValorEstatico.IdEstadoContactoWhatsAppInvalido : ValorEstatico.IdEstadoContactoWhatsAppValido;
                    }
                }
                else
                {
                    alumno.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                }
                return alumno;
            }
            catch (Exception e)
            {
                return alumno;
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <param name="dni">DNI del Alumno</param>
        /// <returns>Configura el IdEstadoWhatsApp del _alumno</returns>

        /// Modificacion
        /// Fecha: 24/04/2024
        /// Autor: Juan Diego Huanaco Quispe
        /// Por defecto a los numeros de Mexico y Chile se les pondrá IdEstadoWhatsapp en 1 (Activado).
        /// Esto debido a que no existe un host de whatsapp para validar los numeros de esos paises.
        public void ValidarEstadoContactoWhatsAppTemporal()
        {
            try
            {

                if (_alumno.IdCodigoPais.Value == 52 || _alumno.IdCodigoPais.Value == 56)
                {
                    _alumno.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppValido;
                    return;
                }

                string urlToPost;
                bool banderaLogin = false;
                string tokenComunicacion = string.Empty;
                var idPersonal = 4589;//TODO
                var nroCelularCompleto = ObtenerNroCelularCompleto(_alumno.IdCodigoPais.Value, _alumno.Celular);
                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                DTO.contacts.Add("+" + nroCelularCompleto);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                // Correccion temporal
                int[] listaIdPaisesServidoresDedicados = new int[3] { 51/*Peru*/, 57/*Colombia*/, 591 /*Bolivia*/};

                var credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(listaIdPaisesServidoresDedicados.Contains(_alumno.IdCodigoPais.Value) ? _alumno.IdCodigoPais.Value : 0/*Internacional*/);
                var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(idPersonal, _alumno.IdCodigoPais.Value);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);

                string resultado = string.Empty;
                if (credencialesHost != null)
                {
                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = credencialesHost.UrlWhatsApp + "/v1/users/login";
                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.ObtenerCredencialUsuarioLogin(idPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest("/v1/users/login", Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                var modelCredencial = new WhatsAppUsuarioCredencial
                                {
                                    IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                    IdWhatsAppConfiguracion = credencialesHost.Id,
                                    UserAuthToken = item.token,
                                    ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                    EsMigracion = true,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "whatsapp",
                                    UsuarioModificacion = "whatsapp"
                                };
                                var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Add(modelCredencial);
                                _unitOfWork.Commit();
                                tokenComunicacion = item.token;
                            }
                            banderaLogin = true;
                        }
                        else
                        {
                            banderaLogin = false;
                        }
                    }
                    else
                    {
                        tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                }




                if (banderaLogin)
                {
                    urlToPost = credencialesHost.UrlWhatsApp + "/v1/contacts";
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                    var resultadoEstadoContactoWhatsApp = datoRespuesta.contacts.FirstOrDefault();
                    if (datoRespuesta == null)
                    {
                        _alumno.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                    }
                    else
                    {
                        _alumno.IdEstadoContactoWhatsApp = resultadoEstadoContactoWhatsApp.status == "invalid" ? ValorEstatico.IdEstadoContactoWhatsAppInvalido : ValorEstatico.IdEstadoContactoWhatsAppValido;
                    }
                }
                else
                {
                    _alumno.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                }
            }
            catch (Exception e)
            {
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea desde AlumnoDTO a Alumno
        /// </summary>
        /// <param name="dto">AlumnoDTO</param>
        /// <returns> Alumno </returns>
        public Alumno MapeoEntidadDesdeDTO(AlumnoDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<Alumno>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AlumnoDTO MapeoDtoDesdeEntidad(Alumno dto)
        {
            try
            {
                var entidad = _mapper.Map<AlumnoDTO>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 1 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailDTO ValidarEmail1Alumno(string email)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Validar Email 2 - Alumno
        /// </summary>
        /// <param name="email">Email a Validar</param>
        /// <returns> AlumnoEmailDTO </returns>
        public AlumnoEmailDTO ValidarEmail2Alumno(string email)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ValidarEmail2Alumno(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene cupon por IdAlumno  
        /// </summary>
        /// <param name="idAlumno">Id de _alumno</param>
        /// <returns>ObjetoDTO: AlumnoCuponDTO</returns>
        public AlumnoCuponDTO ObtenerCuponPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCuponPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de SolicitudVisualizacionOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idPersonal"></param>
        /// <returns>ValorIntDTO</returns>
        public ValorIntDTO InsertarSolicitudVisualizarDatosOportunidad(int idOportunidad, int idPersonal)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.InsertarSolicitudVisualizarDatosOportunidad(idOportunidad, idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nro de WhatsApp de la coordinadora asignada
        /// </summary>
        /// <returns>String: Nro de WhatsApp de coordinador</returns>
        public string ObtenerNroWhatsAppCoordinador(int idCodigoPais)
        {
            try
            {
                string numeroCelular = "";
                switch (idCodigoPais)
                {
                    case Pais.CodigoPeru:
                        numeroCelular = "+51 992 651 774";
                        break;
                    case Pais.CodigoColombia:
                        numeroCelular = "+57 350 3189803";
                        break;
                    case Pais.CodigoBolivia:
                        numeroCelular = "+591 76398490";
                        break;
                    default:
                        numeroCelular = "+51 932 104 477";//Internacional
                        break;
                }
                return numeroCelular;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el telefono del coordinador en base al pais y ciudad
        /// </summary>
        /// <returns></returns>
        public string ObtenerNroTelefonoCoordinador(int idCodigoPais, int idCiudad)
        {
            try
            {
                var numeroTelefono = "";
                if (idCodigoPais == 51 && idCiudad == 4)
                {
                    numeroTelefono += "(51) 54 258787";
                }
                else if (idCodigoPais == 51 && idCiudad == 14)
                {
                    numeroTelefono += "(51) 1 207 2770";
                }
                else if (idCodigoPais == 57)
                {
                    numeroTelefono += "(57) 1 3819462";
                }
                else if (idCodigoPais == 591)
                {
                    numeroTelefono += "(591) 3 3403140";
                }
                else
                {
                    numeroTelefono += "(51) 1 207 2770";
                }
                return numeroTelefono;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la forma de pago en base al codigo pais del _alumno
        /// </summary>
        /// <returns></returns>
        public string ObtenerFormaPago(int idCodigoPais)
        {
            try
            {
                var formaPago = "";
                switch (idCodigoPais)
                {
                    case 51://Peru
                        formaPago = $@"<p><span style='color:#ff0000;'><strong>PERU</span></strong></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</li><li>Se habilitar&aacute; un recuadro para colocar su DNI y para seleccionar si desea se le emita una Boleta o Factura por su pago.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p><strong>VENTANILLA</strong><br />Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco de Cr&eacute;dito</p><ul><li>Decir que va a realizar un&nbsp;<strong>Pago de Servicios</strong>a trav&eacute;s del Sistema&nbsp;<strong>Credipago</strong>.</li><li>Indicar que el tipo de empresa es&nbsp;<strong>EMPRESAS DIVERSAS</strong>.</li><li>La empresa es&nbsp;<strong>BSG Institute</strong>.</li><li>Cuenta a Abonar:</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Dar su&nbsp;<strong>c&oacute;digo de _alumno </strong>y listo.</li></ul><p><strong>ALUMNOS CON CUENTA EN EL BANCO DE CREDITO</strong></p><ul><li>Ingrese a&nbsp;<a href='http://www.viabcp.com/'>viabcp.com</a>.</li><li>Seleccione la opci&oacute;n:&nbsp;<strong>PAGOS</strong>.</li><li>Seleccione:&nbsp;P<strong>agar un Servicio</strong>.</li><li>Digite&nbsp;<strong>BSG Institute</strong>.</li><li>Ubicada la empresa se mostrar&aacute; un nuevo cuadro donde se muestra:</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Digite su c&oacute;digo de _alumno y haga click en continuar</li><li>Seleccione la cuenta o tarjeta con que pagar&aacute; y la cuota a pagar, de un clic en continuar y confirme la operaci&oacute;n con su clave token.</li></ul><p>Para pagar sus cuotas a trav&eacute;s de Internet requiere tener su clave de internet de 6 d&iacute;gitos, y llave token, para solicitarlas ac&eacute;rquese a cualquier agencia del Banco Cr&eacute;dito.</p><p><br /></p><p><strong>AGENTE BCP</strong></p><ul><li>Decir que va a realizar un&nbsp;<strong>Pago de Servicios</strong>.</li><li>Indicar que el pago es a la empresa de c&oacute;digo&nbsp;<strong>18185</strong>.</li><li>Cuenta a :</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Dar su&nbsp;<strong>c&oacute;digo de _alumno</strong>y listo.</li></ul><p>&nbsp;</p><p>NOTA:</p><p>En caso de que la cuota sea en d&oacute;lares y se quiere realizar el pago en soles, se aplicar&aacute; el tipo de cambio bancario.</p><p>Para pagos en agentes BCP solo reciben soles por lo que el monto de la cuota ser&aacute; cambiada autom&aacute;ticamente a soles de acuerdo al tipo de cambio bancario.</p><p>El c&oacute;digo solo es v&aacute;lido en la cuenta detallada seg&uacute;n el programa al que esta Ud. matriculado &nbsp;y seg&uacute;n su cronograma de pagos, por ejemplo si su programa es en Modalidad Online la cuenta ser&iacute;a: Lima-Soles (si su cronograma es en soles) y Lima-D&oacute;lares (si su cronograma es en d&oacute;lares), no importando la moneda con la que realizar&aacute; el pago.</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    case 57://Colombia
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>COLOMBIA</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</li><li>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta Y tarjetahabiente y confirme el pago.</li></ul><p><strong>VENTANILLA BANCOLOMBIA</strong><br /> Los&nbsp;Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco Bancolombia</p><ul><li>Debe indicar&nbsp;queva a realizar un dep&oacute;sito a la empresa Bs Grupo Colombia con Nro de Convenio 56470.</li><li>Debe indicar su nro de referencia:&nbsp;<span style='color: #ff0000;'><strong>XXXXXXXX (de acuerdo al _alumno)</strong>.</span></li><li>Debe indicar el monto a pagar.</li></ul><p><strong>TRANSFERENCIA</strong></p><p>Los alumnos pueden registrarnos como proveedores con los datos indicados y al cabo de dos horas pueden transferirnos el monto de su cuota.<br /> Empresa: BS GRUPO COLOMBIA SAS</p><p>NIT: 900776296</p><p>N&uacute;mero de Cuenta de Ahorros: 65231918412</p><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    case 591://Bolivia
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>BOLIVIA</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</li><li>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula de Identidad.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p><strong>&nbsp;</strong></p><p><strong>AGENCIA O AGENTE BANCO DE CREDITO</strong><br /> Los&nbsp;Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia o agente del Banco de Cr&eacute;dito con los siguientes datos:</p><p>BSG Institute Bolivia SRL</p><p>NIT 376053024</p><p>Cuenta Corriente Banco de Cr&eacute;dito de Bolivia - Sucursal Santa Cruz</p><p>Cta en Bolivianos: 701-5051921-3-41</p><p>Cta en D&oacute;lares: 701-5041553-2-04</p><p>Indicar el monto a pagar</p><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    default://Internacional
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>EXTRANJEROS</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</li><li>Se habilitar&aacute; un recuadro para colocar su Documento de Identidad.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de la forma detallada puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                }
                return formaPago;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 27/09/2022
        /// Version: 1.0
        /// <summary>
        /// Concatena el nombre completo del _alumno
        /// </summary>
        /// <returns></returns>
        public string ObtenerNombreCompleto(Alumno alumno)
        {
            try
            {
                return string.Concat(alumno.Nombre1, " ",
                                    alumno.Nombre2, " ",
                                    alumno.ApellidoPaterno, " ",
                                   alumno.ApellidoMaterno).ToUpper();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.M.F.
        /// Fecha: 26/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida si el Alumno existe
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public int ValidarAlumnoExiste(AlumnoAutocompleteEmailDTO filtro)
        {
            try
            {
                var resultado = ExisteContacto(filtro.Email1, filtro.Email2, filtro.Id);
                int id = 0;
                if (resultado)
                {
                    id = _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(filtro.Email1)!.Id;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Verifica si existen alumnos con un mismo correo
        /// </summary>
        /// <param name="email1"> email1 del _alumno </param>
        /// <param name="email2"> email2 del _alumno </param>
        /// <param name="idAlumno"> Id del _alumno </param>
        /// <returns></returns>
        public bool ExisteContacto(string email1, string email2, int idAlumno = 0)
        {
            bool result = true;
            List<AlumnoEmailDTO> listaAlumno = _unitOfWork.AlumnoRepository.ObtenerAlumnoPorEmail(email1, email2);
            if (listaAlumno.Count() == 0)
            {
                result = false;
            }
            else if (listaAlumno.Count() == 1)
            {
                //Si es el registro que se esta editando, retorna false por que no existe duplicados, si podria admitirlo en cualquiera: email 1 o email2
                if (listaAlumno.FirstOrDefault()!.Id == idAlumno)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                //Verificar el caso, que hay varias filas, pero en el row que se esta editando se quiere pasar el email2 y duplicarlo en email1
                bool cumpleCondiciones = false;
                AlumnoEmailDTO alumnoBD = _unitOfWork.AlumnoRepository.ObtenerEmailAlumno(idAlumno)!;

                foreach (var iter in listaAlumno)
                {
                    if (idAlumno == iter.Id && alumnoBD != null && string.IsNullOrEmpty(alumnoBD.Email1) && iter.Email2.Equals(email1))
                    {
                        cumpleCondiciones = true;
                    }
                }
                if (cumpleCondiciones)
                {
                    result = false;
                }
            }
            return result;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del programa general del ultimo envio masivo
        /// </summary>
        /// <param name="id"></pa
        public string ObtenerNombreProgramaGeneralUltimoEnvioMasivo(int id)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerNombreProgramaGeneralUltimoEnvioMasivo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del programa general de la ultima solicitud de información
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(int id)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Alumno por número de celular y número de celular alterno
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="numeroAlterno"></param>
        /// <returns></returns>
        /// <exception></exception>
        public AlumnoPorCelularDTO ObtenerPorCelular(string numero, string numeroAlterno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerPorCelular(numero, numeroAlterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 15/10/2022
        /// Version: 1.0
        /// <summary>
        /// ELimina de forma fisica de la base de datos de Alumno
        /// </summary>
        /// <returns> true o false </returns>
        public bool EliminarFisicaAlumno(string tablaV3, string tablaV4, int idV4, string idv3, int? idv3Int)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.EliminarFisicaAlumno(tablaV3, tablaV4, idV4, idv3, idv3Int);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerFechaInicioCapacitacion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerFechaFinCapacitacion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la nota promedio del _alumno coincidente con la matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera">Es el Id de la Matricula Cabecera (PK de la tabla fin.T_MatriculaCabeceraa)</param>
        /// <returns>Cadena con nota promedio del _alumno</returns>
        public string ObtenerNotaPromedio(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerNotaPromedio(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha de emision
        /// </summary>
        /// <returns>Cadena con la fecha de emision formateada en texto comprensible</returns>
        public string ObtenerFechaEmision()
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerFechaEmision();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigo del certificado
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena con la el codigo del certificado</returns>
        public string ObtenerCodigoCertificado(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCodigoCertificado(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la URL de feliz cumpleaños
        /// </summary>
        /// <returns>Cadena con la URL de la imagen de feliz cumpleaños</returns>
        public string ObtenerUrlImagenFelizCumpleanios()
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerUrlImagenFelizCumpleanios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula la forma de pago en base a la matricula
        /// </summary>
        /// <param name="idModalidadCurso">Id de la modalidad del curso (PK de la tabla pla.T_ModalidadCurso)</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <param name="codigoMatricula">Codigo de matricula del _alumno</param>
        /// <param name="monedaCronograma">Moneda registrada en el cronograma</param>
        /// <returns>Cadena formateada con la forma de pago</returns>
        public string ObtenerFormaPagoReferencia(int idCodigoPais, int idModalidadCurso, int idCiudad, string codigoMatricula, string monedaCronograma)
        {
            try
            {
                var formaPago = "";
                switch (idCodigoPais)
                {
                    case 51://Peru
                        formaPago = @"<p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Plataforma</strong> </span></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Pasos a seguir:</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese su DNI y seleccione si desea boleta o factura, en el caso de ser factura indique la Raz&oacute;n social y el RUC.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y confirme el pago.</span></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong></strong> </span> <strong style='font-family:Calibri, sans-serif;font-size:11pt;'>Ventanilla</strong> <br /></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco de Cr&eacute;dito.</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Pasos a seguir:</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Mencione que realizar&aacute; un Pago de Servicios a trav&eacute;s del Sistema Credipago a la empresa BSG Institute</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indique la cuenta a Abonar: {T_MatriculaCabecera.CuentaAbonar}</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar su c&oacute;digo de _alumno.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Alumnos con cuenta en el Banco de Cr&eacute;dito</strong> </span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a www.viabcp.com.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione la opci&oacute;n: <strong>PAGOS.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione: <strong>Pagar un Servicio.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Digite y seleccione <strong>BSG Institute.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; un nuevo cuadro con nuestras cuentas y usted deber&aacute; elegir:&nbsp;{T_MatriculaCabecera.CuentaAbonar} </span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Digite su c&oacute;digo de _alumno y haga click en continuar</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione la cuenta o tarjeta con que pagar&aacute; y la cuota a pagar, dar click en continuar y confirme la operaci&oacute;n con su clave token.Para pagar sus cuotas a trav&eacute;s de Internet requiere tener su clave de internet de 6 d&iacute;gitos y llave token, para solicitarlas ac&eacute;rquese a cualquier agencia del Banco Cr&eacute;dito.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Agente BCP</strong> </span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Decir que va a realizar un <strong>Pago de Servicios.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar el c&oacute;digo de la empresa: <strong>18185</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indicar la cuenta: {T_MatriculaCabecera.CuentaAbonar} </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar su c&oacute;digo de _alumno.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Nota:</strong> </span> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si su cuota es en d&oacute;lares y quiere realizar el pago en soles, se aplicar&aacute; el tipo de cambio bancario.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Tener en cuenta que en los agentes BCP, s&oacute;lo reciben moneda en soles y si desea pagar una cuota en d&oacute;lares el monto ser&aacute; cambiado autom&aacute;ticamente a soles de acuerdo al tipo de cambio bancario.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor(a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span></p>";
                        break;
                    case 57://Colombia
                        formaPago = @"<p style='text-align:justify;'><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <span style='font-family:Calibri, sans-serif;font-size:11pt;'></span></p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y tarjeta habiente y confirme el pago.</span><p style='text-align:justify;'><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Ventanilla Bancolombia</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco Bancolombia</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar que va a realizar un dep&oacute;sito a la empresa Bs Grupo Colombia con n&uacute;mero de Convenio 56470.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar su N&uacute;mero de referencia: {T_MatriculaCabecera.NroReferenciaAlumno}</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar el monto a pagar.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Transferencia</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los alumnos pueden registrarnos como proveedores con los datos indicados y al cabo de dos horas pueden transferirnos el monto de su cuota.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Empresa: BS GRUPO COLOMBIA SAS</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>NIT: 900776296</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>N&uacute;mero de Cuenta de Ahorros: 65231918412</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras formas de pago alternativas.</span></p>";
                        break;
                    case 591://Bolivia
                        formaPago = @"<p><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos y seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego elija el m&eacute;todo de pago: <strong>VISA / MASTERCARD</strong></span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula de Identidad.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingresar sus datos de tarjeta y confirme el pago.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Agencia o Agente Banco de Cr&eacute;dito</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia o agente del Banco de Cr&eacute;dito con los siguientes datos:</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>BSG Institute Bolivia SRL</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>NIT 376053024</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Cuenta Corriente Banco de Cr&eacute;dito de Bolivia - Sucursal Santa Cruz</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Cuenta en Bolivianos:</strong> 701-5051921-3-41</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Cuenta en D&oacute;lares</strong>: 701-5041553-2-04</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indicar el monto a pagar.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span><br /> <span style='font-family:Calibri, sans-serif;font-size:14.6667px;text-align:justify;'></span><strong><span style='color:red;'></span></strong><span lang='EN-US' style='font-size:11.0pt;font-family:'Calibri','sans-serif';'></span></p>";
                        break;
                    default://Internacional
                        formaPago = @"<p><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su Documento de Identidad.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>De click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y confirme el pago.</span></p><p style='text-align:justify;'><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'></span><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span></p>";
                        break;
                }

                // Nro de referencia
                if (formaPago.Contains("{T_MatriculaCabecera.NroReferenciaAlumno}"))
                {
                    formaPago = formaPago.Replace("{T_MatriculaCabecera.NroReferenciaAlumno}", codigoMatricula.Replace("A", ""));
                }

                // Nro de referencia
                if (formaPago.Contains("{T_MatriculaCabecera.CuentaAbonar}"))
                {
                    formaPago = formaPago.Replace("{T_MatriculaCabecera.CuentaAbonar}", this.CalcularCuentaAbonar(idModalidadCurso, idCiudad, monedaCronograma));
                }

                return formaPago;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve el medio de pago segun etiqueta
        /// </summary>
        /// <param name="idMedioPago">Id del medio de pago </param>
        /// <returns>Cadena formateada con la forma de pago</returns>
        public string ObtenerMedioPago(string etiqueta)
        {
            try
            {
                switch (etiqueta)
                {
                    case "{BCP_soles}":
                        return "215-1863341-0-42";
                    case "{BCP_CCI_soles}":
                        return "00221500186334104220";
                    case "{BCP_dolares}":
                        return "215-1870934-1-48";
                    case "{BCP_CCI_dolares}":
                        return "00221500187093414823";
                    case "{BBVA_soles}":
                        return "0011-0220-01-00131737";
                    case "{BBVA_CCI_soles}":
                        return "011-220-000100131737-17";
                    case "{Scotiabank_soles}":
                        return "000-4654102";
                    case "{Scotiabank_CCI_soles}":
                        return "009-313-000004654102-85";
                    case "{Num_YAPE}":
                        return "992651774";
                    case "{Swift_Code}":
                        return "BCPLPEPL";
                    case "{CuentaUSD}":
                        return "215-1870934-1-48";
                    case "{BanCol}":
                        return "65231918412";
                    case "{Bancol_suc}":
                        return "56470";
                    case "{BCP_Bolivianos}":
                        return "701-5051921-3-41";
                    case "{BCP_Dolares_Bolivia}":
                        return "701-5041553-2-04";
                    case "{BUnion_Bolivianos}":
                        return "10000043804083";
                    case "{BUnion_Dolares_Bolivia}":
                        return "20000043804306";
                    case "{BBVA_MXpesos}":
                        return "0116490468";
                    case "{BBVA_CCI_MXpesos}":
                        return "012180001164904687";
                    case "{BBVA_MXdol}":
                        return "0116490522";
                    case "{BBVA_CCI_MXdol}":
                        return "012180001164905220";
                    case "{Banorte_MXpesos}":
                        return "1185992526";
                    case "{Banorte_CCI_MXpesos}":
                        return "072180011859925260";
                    case "{Banorte_MXdol}":
                        return "1193560126";
                    case "{Banorte_CCI_MXdol}":
                        return "072180011935601262";
                    default:
                        return "-";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve el numero para contacto de atención al cliente
        /// </summary>
        /// <param name="idMedioPago">Id del medio de pago </param>
        /// <returns>Cadena formateada con la forma de pago</returns>
        public string ObtenerNumeroAtc(string etiqueta)
        {
            try
            {
                switch (etiqueta)
                {
                    case "{numPeru}":
                        return "51 (1)2072770";
                    case "{numColombia}":
                        return "57 (1) 3819462";
                    case "{numMexico}":
                        return "52 (55) 40003255";
                    default:
                        return "-";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Implementa la logica para calcular la cuenta a abonar
        /// </summary>
        /// <param name="idModalidadCurso">Id de la modalidad del curso (PK de la tabla pla.T_ModalidadCurso)</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <param name="monedaCronograma">Moneda segun el cronograma registrado</param>
        /// <returns>Cadena formateada con la cuenta a abonar</returns>
        public string CalcularCuentaAbonar(int idModalidadCurso, int idCiudad, string monedaCronograma)
        {
            try
            {
                var monedaSoles = "soles";
                var monedaDolares = "dolares";

                var modalidadPresencial = 0;
                var modalidadOnline = 1;
                var modalidadAOnline = 2;

                var idCiudadArequipa = 4;
                var idCiudadLima = 14;

                var cuentaAbonar = "";

                if ((idModalidadCurso == modalidadPresencial || idModalidadCurso == modalidadOnline || idModalidadCurso == modalidadAOnline) && monedaCronograma == monedaSoles && idCiudad == idCiudadLima)
                {
                    cuentaAbonar = "Lima - Soles";
                }
                else if ((idModalidadCurso == modalidadPresencial || idModalidadCurso == modalidadOnline || idModalidadCurso == modalidadAOnline) && monedaCronograma == monedaDolares && idCiudad == idCiudadLima)
                {
                    cuentaAbonar = "Lima - Dólares";
                }
                else if (idModalidadCurso == modalidadPresencial && monedaCronograma == monedaSoles && idCiudad == idCiudadArequipa)
                {
                    cuentaAbonar = "Arequipa - Soles";
                }
                else if (idModalidadCurso == modalidadPresencial && monedaCronograma == monedaDolares && idCiudad == idCiudadArequipa)
                {
                    cuentaAbonar = "Arequipa – Dólares";
                }
                else
                {
                    cuentaAbonar = "";
                }
                return cuentaAbonar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del _alumno para messenger chat mediante IdAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionMessengerDTO ObtenerAlumnoInformacionMessengerChatPorId(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerAlumnoInformacionMessengerChatPorId(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor: Jonathan Caipo
        ///Fecha: 03/11/2021
        /// <summary>
        /// Obtener información de Id, Alumnos por nombre AutoComplete
        /// </summary>
        /// <param name="valor"> valor de búsqueda </param>
        /// <returns> Lista de Alumnos por nombre Registrados </returns>
        /// <returns> Objeto DTO: List<AlumnoFiltroAutocompleteDTO> </returns>	
        public List<AlumnoComboDTO> ObtenerTodoComboAutoComplete(string valor)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerTodoComboAutoComplete(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del _alumno mediante su email
        /// </summary>
        /// <param name="valor">
        public List<AlumnoComboDTO> AlumnnosTodoComboAutoCompletePorEmail(string valor)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.AlumnnosTodoComboAutoCompletePorEmail(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y NombreCompleto del Alumno mediante el IdReferido
        /// </summary>
        /// <param name="idR"></param>
        /// <returns></returns>
        public List<AlumnoComboDTO> ObtenerTodoFiltroAutoCompleteReferido(int idR)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerTodoFiltroAutoCompleteReferido(idR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Guarda Archivos QR
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns> string </returns>
        public string GuardarArchivosQR(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string nombreLink = string.Empty;

                try
                {
                    string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
                    string direccionBlob = @"repositorioweb/certificados/CodigoQR/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(direccionBlob);
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        nombreLink = "https://repositorioweb.blob.core.windows.net/" + direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        nombreLink = "";
                    }
                    return nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de inicio de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de inicio de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaInicioCapacitacionPortalWeb(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerFechaInicioCapacitacionPortalWeb(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de fin de capacitacion
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera que esta enlazada (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena formateada con la fecha de fin de capacitacion de una matricula cabecera</returns>
        public string ObtenerFechaFinCapacitacionPortalWeb(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerFechaFinCapacitacionPortalWeb(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma de Nota por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<CronogramaNotaDTO> </returns>
        public List<CronogramaNotaDTO> ObtenerCronogramaNota(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCronogramaNota(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma de Asistencia por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<CronogramaAsistenciaDTO> </returns>
        public List<CronogramaAsistenciaDTO> ObtenerCronogramaAsistencia(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerCronogramaAsistencia(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.Exist(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de WhatsApp por idAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerEstadoWhatsapp(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerEstadoWhatsapp(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene documentos para _alumno por ID   
        /// </summary>
        /// <param name="id"></param>
        /// <returns> ObjetoDTO: AlumnoCompuestoDocumentoDTO </returns>
        public AlumnoDatosDocumentoDTO ObtenerDatosAlumnoDocumentoPorId(int id)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerDatosAlumnoDocumentoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene documentos para _alumno para completar por medio de un valor de entrada
        /// </summary>
        /// <param name="valor"></param>
        /// <returns> ObjetoDTO: AlumnoCompuestoDocumentoDTO </returns>
        public IEnumerable<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerTodoFiltroAutoComplete(valor);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// lisbeth
        /// </summary>
        /// <param name="id"></param>
        /// <returns> DTO: AlumnoComprobanteDTO </returns>
        public AlumnoComprobanteDTO ObtenerDatosAlumnoPorId(int id)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerDatosAlumnoPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene número de WhatsApp
        /// </summary>
        /// <param name="codigoPais"></param>
        /// <param name="celular"></param>
        /// <returns> string: celular </returns>
        public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
        {
            try
            {
                if (codigoPais == 51)
                {
                    if (celular.Length == 9)
                    {
                        celular = "51" + celular;
                    }
                }
                else if (codigoPais == 57)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 12)
                    {
                        celular = "57" + celular;
                    }
                }
                else if (codigoPais == 591)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 11)
                    {
                        celular = "591" + celular;
                    }
                }
                return celular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        public bool ValidarNumeroEnvioWhatsApp(int IdPersonal, int IdPais, ValidarNumerosWhatsAppAsyncDTO DTO)
        {
            if (DTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(IdPais);
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(IdPersonal, IdPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            banderaLogin = true;
                        }
                        else
                        {
                            banderaLogin = false;
                        }
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        if (datoRespuesta.contacts[0].status == "invalid")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 13/11/2021
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen del BO / Repositorio
        /// </summary>
        /// <param name="cantidad"> Cantidad de alumnos </param>
        /// <param name="iterador"> Iterador </param>
        /// <returns> List<AlumnoBO> </returns>
        public List<AlumnoDTO> ObtenerALumnosaValidarWhatsappPeru(int cantidad, int iterador)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerALumnosaValidarWhatsappPeru(cantidad, iterador);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del _alumno</returns>
        public List<AlumnoDTO> ValidarEstadoContactoWhatsAppMasivo(int idPais, List<AlumnoDTO> alumnos)
        {
            try
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                var idPersonal = 4589;//TODO
                bool secundario = false;


                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                ValidarNumerosWhatsAppDTO DTOSecundario = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                foreach (var alumno in alumnos)
                {
                    DTO.contacts.Add("+" + alumno.NroCelularCompleto);
                    if (alumno.NroCelularSecundarioCompleto != "")
                    {
                        secundario = true;
                        DTOSecundario.contacts.Add("+" + alumno.NroCelularSecundarioCompleto);
                    }
                }
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(idPais);
                var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(idPersonal, idPais);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);
                var mensajeJSONSecundario = JsonConvert.SerializeObject(DTOSecundario);

                string resultado = string.Empty;
                string resultado2 = string.Empty;

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(idPersonal);

                    var client = new RestClient(urlToPostUsuario);
                    var request = new RestSharp.RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Length", "");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", _credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request.AddHeader("Content-Type", "application/json");
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        foreach (var item in datos.users)
                        {
                            TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial
                            {
                                IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                IdWhatsAppConfiguracion = _credencialesHost.Id,
                                UserAuthToken = item.token,
                                ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                EsMigracion = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "whatsapp",
                                UsuarioModificacion = "whatsapp"
                            };

                            var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(modelCredencial);
                            _tokenComunicacion = item.token;
                        }
                        banderaLogin = true;
                    }
                    else
                    {
                        banderaLogin = false;
                    }
                }
                else
                {
                    _tokenComunicacion = tokenValida.UserAuthToken;
                    banderaLogin = true;
                }

                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                if (banderaLogin)
                {
                    try
                    {
                        //using ()
                        //{
                        WebClient client = new WebClient();
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                        //}

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        numerosValidos datoRespuestaSecundario = new numerosValidos();

                        if (secundario)
                        {
                            WebClient client2 = new WebClient();
                            client2.Encoding = Encoding.UTF8;
                            var serializer2 = new JavaScriptSerializer();
                            var serializedResult2 = serializer2.Serialize(DTOSecundario);
                            client2.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client2.Headers[HttpRequestHeader.ContentLength] = mensajeJSONSecundario.Length.ToString();
                            client2.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client2.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado2 = client2.UploadString(urlToPost, serializedResult2);
                            datoRespuestaSecundario = JsonConvert.DeserializeObject<numerosValidos>(resultado2);
                        }
                        //foreach(var item in datoRespuesta.contacts)
                        //{
                        //    var _alumno = alumnos.FirstOrDefault(x => x.Celular == item.input);
                        //}
                        for (int i = 0; i < alumnos.Count; i++)
                        {
                            var estadoCelular = datoRespuesta.contacts.FirstOrDefault(x => x.input.Contains(alumnos[i].NroCelularCompleto));
                            if (estadoCelular.status == "invalid")
                            {
                                alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                            }
                            else
                            {
                                alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppValido;
                            }
                            if (!string.IsNullOrEmpty(alumnos[i].Celular2))
                            {
                                var estadoCelularSecundario = datoRespuestaSecundario.contacts.FirstOrDefault(x => x.input.Contains(alumnos[i].NroCelularSecundarioCompleto));
                                if (estadoCelularSecundario.status == "invalid")
                                {
                                    alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                                }
                                else
                                {
                                    alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppValido;
                                }
                            }
                            alumnos[i].UsuarioModificacion = "jsalazart4";
                            alumnos[i].FechaModificacion = DateTime.Now;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                }
                else
                {
                    for (int i = 0; i < alumnos.Count; i++)
                    {
                        alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                        alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                    }
                }
                return alumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del _alumno</returns>
        public void ActualizacionMasivaEstadoWhatsApp(List<AlumnoDTO> alumnos)
        {
            try
            {
                var alumnosValidos = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 1).Select(y => y.Id).ToList();
                var alumnosModificados = String.Join(",", alumnosValidos);
                var resp = _unitOfWork.AlumnoRepository.ActualizarValidos(alumnosModificados, 1);
                var alumnosNoValidos = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 2).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosNoValidos);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidos(alumnosModificados, 2);
                var alumnosSinValidar = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 3).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosSinValidar);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidos(alumnosModificados, 3);
                var alumnosErrorValidar = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 4).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosErrorValidar);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidos(alumnosModificados, 4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del _alumno</returns>
        public void ActualizacionMasivaEstadoWhatsAppSecundario(List<AlumnoDTO> alumnos)
        {
            try
            {
                var alumnosValidos = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 1).Select(y => y.Id).ToList();
                var alumnosModificados = String.Join(",", alumnosValidos);
                var resp = _unitOfWork.AlumnoRepository.ActualizarValidosSecundario(alumnosModificados, 1);
                var alumnosNoValidos = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 2).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosNoValidos);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidosSecundario(alumnosModificados, 2);
                var alumnosSinValidar = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 3).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosSinValidar);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidosSecundario(alumnosModificados, 3);
                var alumnosErrorValidar = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 4).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosErrorValidar);
                resp = _unitOfWork.AlumnoRepository.ActualizarValidosSecundario(alumnosModificados, 4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Sustituye el Email por digitos X
        /// </summary>
        /// <param name="email"> email </param>
        /// <returns> email encriptado </returns>
        public string EncriptarStringCorreo(string email)
        {
            string respuesta = email;
            if (email != null)
            {
                int posicion = email.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + email.Remove(0, posicion);
                }
            }
            return respuesta;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Sustituye los 4 ultimos digitos del numero telefonico por X
        /// </summary>
        /// <param name="numero"> numero telefono </param>
        /// <returns> numero encriptado </returns>
        public string EncriptarStringNumero(string numero)
        {
            string respuesta = numero;
            if (numero != null)
            {
                int longitud = numero.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    respuesta = numero.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }


        /// <summary>
        /// Se encriptar el correo y celular con SHA256 para todas la interfaces de Integra
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string EncriptarCorreoHash(string email)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(email);
                byte[] hash = sha256.ComputeHash(bytes);

            
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return (builder.ToString().Substring(0, 9) + "@" + builder.ToString().Substring(9, 9));
            }
        }
        public string EncriptarNumeroHash(string numero)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(numero);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir a cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return (builder.ToString().Substring(0, 2) + " "+builder.ToString().Substring(2, 3) + " " + builder.ToString().Substring(5, 3) + " " + builder.ToString().Substring(8, 3));
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los alumnos por el IdReferido
        /// </summary>
        /// <param name="idReferido"> Id del referido</param>
        /// <returns> Objeto lista DTO: List<AlumnoReferidoDTO> </returns>
        public List<AlumnoReferidosDTO> ObtenerReferidos(int idReferido)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerReferidos(idReferido);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 05/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del _alumno por el IdClasificacionPersona
        /// </summary>
        /// <param name="idClasificacionPersona"> Id de T_ClasificacionPersona </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerPorIdClasificacionPersona(idClasificacionPersona);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Carlos Crispin
        /// Fecha: 24/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del _alumno por el idActividadDetalle
        /// </summary>
        /// <param name="idActividadDetalle"> Id de T_ActividadDetalle </param>
        /// <returns> ObjetoDTO: AlumnoInformacionDTO </returns>
        public AlumnoInformacionDTO ObtenerPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerPorIdActividadDetalle(idActividadDetalle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 05/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos del _alumno acceso portal
        /// </summary>
        /// <param name="idAlumno"> Id de T_Alumno </param>
        /// <returns> ObjetoDTO: AlumnoAccesosDTO </returns>
        public AlumnoAccesosDTO obteneAccesoAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.ObtenerAccesosAlumno(idAlumno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 05/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de cobranza del _alumno 
        /// </summary>
        /// <param name="idAlumno"> IdMatriculaCabecera </param>
        /// <returns> ObjetoDTO: AlumnoAccesosDTO </returns>
        public DatosCorbranzaAlumnoDTO obtenerDatosCobranzaAlumno(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.obtenerDatosCobranzaAlumno(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 05/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de avance Aonline de los alumno
        /// </summary>
        /// <param name="idAlumno"> IdMatriculaCabecera </param>
        /// <returns> ObjetoDTO: AlumnoAccesosDTO </returns>
        public AvanceAonlineAlumnoDTO obtenerDatosAvanceAonline(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.obtenerDatosAvanceAonline(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public AvanceAonlineAlumnoATCDTO obtenerDatosAvanceAonlineATC(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.obtenerDatosAvanceAonlineATC(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 05/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de avance Online de los alumno
        /// </summary>
        /// <param name="idAlumno"> IdMatriculaCabecera </param>
        /// <returns> ObjetoDTO: AlumnoAccesosDTO </returns>
        public AvanceOnlineAlumnoDTO obtenerDatosAvanceOnline(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.AlumnoRepository.obtenerDatosAvanceOnline(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Envia un mensaje de texto
        /// </summary>
        /// <param name="IdMatriculaCabecera"> Id de T_MatriculaCabecera </param>
        /// <param name="IdPlantilla"> Id de T_Plantilla </param>
        /// <param name="IdAsesor"> Id del asesor </param>
        /// <returns> bool </returns>
        public bool EnviarSMS(int IdMatriculaCabecera, int IdPlantilla, int IdAsesor)
        {

            try
            {
                var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);
                var plantillaService = new PlantillaService(_unitOfWork);
                var oportunidadService = new OportunidadService(_unitOfWork);
                string url = String.Empty;

                var matriculaCabecera = matriculaCabeceraService.ObtenerPorId(IdMatriculaCabecera);
                var alumno = alumnoService.ObtenerPorId(matriculaCabecera.IdAlumno);
                var detalleMatriculaCabecera = matriculaCabeceraService.ObtenerDetalleMatricula(matriculaCabecera.Id);
                var plantilla = plantillaService.ObtenerPorId(IdPlantilla);
                var oportunidad = oportunidadService.ObtenerPorId(detalleMatriculaCabecera.IdOportunidad);

                //_alumno.Celular = "947798302";
                string Mensaje = "BSG Institute,!Intentamos comunicarnos contigo sin exito. Nos puedes contactar para lo que requieras a este numero.";
                //string MensajeTarde = "BSG INSTITUTE | Solicitud de información |Hola, soy Milagros Landeo de BSG Institute.Me estoy intentado comunicar contigo para brindarte asesoría personalizada sobre el programa de capacitación del que solicitaste información.Te pido por favor que puedas registrar mi numero ya que me intentare comunicar contigo en las próximas horas o el día de mañana.También me puedes indicar por este medio la hora en la que te puedo llamar.";                

                if (alumno.Celular == null || String.IsNullOrEmpty(alumno.Celular))
                {
                    throw new Exception("El numero del celular es nullo o vacio");
                }
                switch (alumno.IdCodigoPais)
                {
                    case 51://Peru
                        if (alumno.Celular.Length != 9)
                        {
                            throw new Exception("El numero de Peru no tiene los digitos para el pais");
                        }
                        url = "http://192.168.3.24:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&port=gsm-1.1&report=String&timeout=5";
                        break;
                    case 57://Colombia
                        if (alumno.Celular.Length == 14)
                        {
                            alumno.Celular = alumno.Celular.Replace("0057", "", StringComparison.OrdinalIgnoreCase);
                        }
                        if (alumno.Celular.Length != 10)
                        {
                            throw new Exception("El numero de Colombia no tiene los digitos para el pais");
                        }
                        url = "http://192.168.6.28:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&port=gsm-4.16&report=String&timeout=5";
                        break;
                    case 591://Bolivia
                        if (alumno.Celular.Length == 13)
                        {
                            alumno.Celular = alumno.Celular.Replace("00591", "", StringComparison.OrdinalIgnoreCase);
                        }
                        if (alumno.Celular.Length != 8)
                        {
                            throw new Exception("El numero de Bolivia no tiene los digitos para el pais");
                        }
                        url = "http://192.168.7.26:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&port=gsm-1.1&report=String&timeout=5";
                        break;
                    case 52://Mexico
                        if (alumno.Celular.Length == 14)
                        {
                            alumno.Celular = alumno.Celular.Replace("0052", "", StringComparison.OrdinalIgnoreCase);
                        }
                        if (alumno.Celular.Length != 10)
                        {
                            throw new Exception("El numero de Mexico no tiene los digitos para el pais");
                        }
                        url = "http://192.168.8.30:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&port=lte-1.1&report=String&timeout=5";
                        break;
                }
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    wc.DownloadString(url);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        ///Autor: Jonathan Caipo
        ///Fecha: 28/03/2023
        /// <summary>
        /// Obtiene el nombre completa del Alumno por medio del email1
        /// </summary>
        /// <param name="email"> email1 del Alumno</param>
        /// <returns> string: nombreCompleto </returns>
        private string ObtenerNombreCompletoAlumnoPorId(int id)
        {
            try
            {
                var alumno = _unitOfWork.AlumnoRepository.ObtenerNombreCompletoAlumnoPorId(id);
                string nombreCompleto = string.Empty;

                if (string.IsNullOrEmpty(alumno.Nombre2))
                    nombreCompleto += $"{alumno.Nombre1} {alumno.Nombre2}";
                else
                    nombreCompleto += $"{alumno.Nombre1}";

                if (string.IsNullOrEmpty(alumno.ApellidoMaterno))
                    nombreCompleto += $" {alumno.ApellidoMaterno} {alumno.ApellidoPaterno}";
                else
                    nombreCompleto += $" {alumno.ApellidoPaterno}";
                return nombreCompleto.ToUpper();
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Email Principal de Alumno
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> DTO - AlumnoDTO - alumnoDTO </returns>
        public AlumnoDTO ActualizarEmailPrincipal(AlumnoActualizarEmailPrincipalDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    var existeAlumno = _unitOfWork.AlumnoRepository.ValidarEmailPrincipal(dto.EmailAPrincipal);

                    if (existeAlumno != null && existeAlumno.Id > 0 && existeAlumno.Id != dto.IdAlumno)
                    {
                        string nombreCompleto = ObtenerNombreCompletoAlumnoPorId(existeAlumno.Id);
                        if (existeAlumno.Estado && existeAlumno.EstadoPer && existeAlumno.EstadoCP) //VALIDACIÓN DE EXISTENCIA DEL ALUMNO
                        {
                            var verificacion = _unitOfWork.OportunidadRepository.VerificacionOportunidades(dto.IdAlumno, existeAlumno.Id);  //VERIFICA SI SE PUEDE HACER LA REASIGNACION DE OPORTUNIDADES
                            if (verificacion.Valor)
                            {
                                throw new ConflictException($"El correo le pertenece a {nombreCompleto} ({existeAlumno.Id}) y {verificacion.Caso}");
                            }
                            else
                                throw new BadRequestException($"El correo le pertenece a {nombreCompleto} ({existeAlumno.Id}) y {verificacion.Caso}");
                        }
                        else
                            throw new BadRequestException($"Correo existente en {nombreCompleto} ({existeAlumno.Id}) - Estado 0");
                    }
                    else
                    {
                        var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdAlumno(dto.IdAlumno);
                        if (clasificacionPersona != null && clasificacionPersona.Id != 0)
                        {
                            var persona = _unitOfWork.PersonaRepository.ObtenerPorId(clasificacionPersona.IdPersona);
                            if (persona != null && persona.Id != 0)
                            {
                                //ACTUALIZO ALUMNO
                                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(dto.IdAlumno);
                                if (_alumno == null || _alumno.Id == 0)
                                {
                                    throw new BadRequestException("No se encontro el alumno");
                                }
                                if (_unitOfWork.OportunidadRepository.ValidarOportunidadesISMPorIdAlumnoCelular(_alumno.Id, _alumno.Celular))
                                {
                                    throw new BadRequestException("El alumno actual presenta oportunidad(es) en IS/M");
                                }
                                ValidarEstadoContactoWhatsAppTemporal();
                                string correoAntiguo = _alumno.Email1;
                                _alumno.Email1 = dto.EmailAPrincipal;
                                _alumno.FechaModificacion = DateTime.Now;
                                _alumno.UsuarioModificacion = usuario;
                                _alumno.IdEmpresa = (_alumno.IdEmpresa == 0 || _alumno.IdEmpresa == -1) ? null : _alumno.IdEmpresa;
                                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                                _unitOfWork.Commit();

                                //ACTUALIZO PERSONA
                                persona.Email1 = dto.EmailAPrincipal;
                                persona.FechaModificacion = DateTime.Now;
                                persona.UsuarioModificacion = usuario;
                                _unitOfWork.PersonaRepository.Update(persona);
                                _unitOfWork.Commit();

                                //SE INSERTA LOG
                                var alumnoLogEmail = new AlumnoLog();
                                alumnoLogEmail.IdAlumno = dto.IdAlumno;
                                alumnoLogEmail.CampoActualizado = "Email 1";
                                alumnoLogEmail.ValorAnterior = correoAntiguo;
                                alumnoLogEmail.ValorNuevo = dto.EmailAPrincipal;
                                alumnoLogEmail.UsuarioCreacion = usuario;
                                alumnoLogEmail.UsuarioModificacion = usuario;
                                alumnoLogEmail.Estado = true;
                                alumnoLogEmail.FechaCreacion = DateTime.Now;
                                alumnoLogEmail.FechaModificacion = DateTime.Now;
                                _unitOfWork.AlumnoLogRepository.Add(alumnoLogEmail);
                                _unitOfWork.Commit();

                                var alumnoDTO = _mapper.Map<AlumnoDTO>(resultadoAlumno);
                                if (alumnoDTO.IdCodigoPais != null || alumnoDTO.IdCiudad != null)
                                {
                                    alumnoDTO.NroWhatsAppCoordinador = ObtenerNroWhatsAppCoordinador(alumnoDTO.IdCodigoPais.GetValueOrDefault());
                                    alumnoDTO.NroTelefonoCoordinador = ObtenerNroTelefonoCoordinador(alumnoDTO.IdCodigoPais.GetValueOrDefault(), alumnoDTO.IdCiudad.GetValueOrDefault());
                                    alumnoDTO.FormaPago = ObtenerFormaPago(alumnoDTO.IdCodigoPais.GetValueOrDefault());
                                    alumnoDTO.NroCelularCompleto = ObtenerNroCelularCompleto(alumnoDTO.IdCodigoPais.GetValueOrDefault(), alumnoDTO.Celular ?? "");
                                }
                                alumnoDTO.NombrePaisOrigen = _unitOfWork.AlumnoRepository.ObtenerPaisOrigen(alumnoDTO.Id);
                                alumnoDTO.NombreCiudadOrigen = _unitOfWork.AlumnoRepository.ObtenerCiudadOrigen(alumnoDTO.Id);
                                alumnoDTO.NombreCompleto = ObtenerNombreCompleto(_mapper.Map<Alumno>(resultadoAlumno));
                                return alumnoDTO;
                            }
                            else
                            {
                                throw new Exception("No existe Persona");
                            }
                        }
                        else
                        {
                            throw new Exception("No existe Clasificacion Persona");
                        }
                    }
                }
                else
                {
                    throw new Exception("No se enviaron datos");
                }
            }
            catch
            {
                throw;
            }
        }
        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza la reasigaanación de oportunidades y el log respectivo
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> DTO - IntNullDTO - reasignacion </returns>
        public (int IdClasificacionPersona, bool EstadoReasignacion) ReasignacionOportunidadesActualizarEmail(AlumnoActualizarEmailPrincipalDTO dto, string usuario)
        {
            try
            {
                var existeAlumno = _unitOfWork.AlumnoRepository.ValidarEmailPrincipal(dto.EmailAPrincipal);
                if (existeAlumno != null && existeAlumno.Id > 0 && existeAlumno.Id != dto.IdAlumno)
                {
                    if (existeAlumno.Estado && existeAlumno.EstadoPer && existeAlumno.EstadoCP) //VALIDACIÓN DE EXISTENCIA DEL ALUMNO
                    {
                        var verificacion = _unitOfWork.OportunidadRepository.VerificacionOportunidades(dto.IdAlumno, existeAlumno.Id);  //VERIFICA SI SE PUEDE HACER LA REASIGNACION DE OPORTUNIDADES
                        if (!verificacion.Valor)
                        {
                            string nombreCompleto = ObtenerNombreCompletoAlumnoPorId(existeAlumno.Id);
                            throw new BadRequestException($"El correo le pertenece a {nombreCompleto} ({existeAlumno.Id}) y {verificacion.Caso}");
                        }
                    }
                    else
                        throw new Exception($"El correo esta siendo utilizado, alumno {existeAlumno.Id} en estado 0");
                }

                var alumnoPrincipal = _unitOfWork.AlumnoRepository.ObtenerPorId(dto.IdAlumno);
                if (alumnoPrincipal.Id == 0)
                {
                    throw new Exception("No se encontro el alumno principal");
                }
                var alumnoSecundario = _unitOfWork.AlumnoRepository.ObtenerPorId(existeAlumno.Id);
                if (alumnoSecundario.Id == 0)
                {
                    throw new Exception("No se encontro el alumno secundario");
                }

                var idOportunidades = _unitOfWork.OportunidadRepository.GetBy(x => x.IdAlumno == dto.IdAlumno).Select(x => x.Id).ToList();

                //SE INSERTA ALUMNO LOG
                var alumnosLog = new List<AlumnoLog>
                {
                    new AlumnoLog { CampoActualizado = "Reasignación Oportunidad", ValorAnterior = "", ValorNuevo = string.Join(",", idOportunidades) },
                    new AlumnoLog {  CampoActualizado = "Reasignación Alumno", ValorAnterior = alumnoPrincipal.Id.ToString(), ValorNuevo = alumnoSecundario.Id.ToString() },
                    new AlumnoLog { CampoActualizado = "Reasignación Email", ValorAnterior = alumnoPrincipal.Email1 ?? "", ValorNuevo = alumnoSecundario.Email1 ?? "" }
                };

                //LOG DEL DNI
                if (!alumnoPrincipal.Dni.Equals(alumnoSecundario.Dni) && !string.IsNullOrEmpty(alumnoPrincipal.Dni))
                {
                    alumnosLog.Add(new AlumnoLog { CampoActualizado = "Reasignación DNI", ValorAnterior = alumnoSecundario.Dni, ValorNuevo = alumnoPrincipal.Dni });
                    alumnoSecundario.Dni = alumnoPrincipal.Dni;
                }
                //LOG DEL CELULAR
                if (!alumnoPrincipal.Celular.Equals(alumnoSecundario.Celular) && !string.IsNullOrEmpty(alumnoPrincipal.Celular))
                {
                    alumnosLog.Add(new AlumnoLog
                    {
                        CampoActualizado = "Reasignación Celular",
                        ValorAnterior = alumnoSecundario.Celular.Trim(),
                        ValorNuevo = alumnoPrincipal.Celular.Trim()
                    });
                    alumnoSecundario.Celular = alumnoPrincipal.Celular;
                }
                //Log Pais
                if (!alumnoPrincipal.IdCodigoPais.Equals(alumnoSecundario.IdCodigoPais) && alumnoPrincipal.IdCodigoPais != null && alumnoPrincipal.IdCodigoPais != 0)
                {
                    alumnosLog.Add(new AlumnoLog { CampoActualizado = "Reasignación Pais", ValorAnterior = alumnoSecundario.IdCodigoPais.GetValueOrDefault().ToString(), ValorNuevo = alumnoPrincipal.IdCodigoPais.GetValueOrDefault().ToString() });
                    alumnoSecundario.IdCodigoPais = alumnoPrincipal.IdCodigoPais;
                    alumnoSecundario.IdPais = alumnoPrincipal.IdPais;
                }
                //Log Ciudad
                if (!alumnoPrincipal.IdCiudad.Equals(alumnoSecundario.IdCiudad) && alumnoPrincipal.IdCiudad != null && alumnoPrincipal.IdCiudad != 0)
                {
                    string nombreCiudad = string.Empty;
                    if (alumnoPrincipal.IdCiudad != null && alumnoPrincipal.IdCiudad != 0)
                    {
                        nombreCiudad = _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorId(alumnoPrincipal.IdCiudad.GetValueOrDefault()).Valor;
                    }
                    alumnosLog.Add(new AlumnoLog { CampoActualizado = "Reasignación Ciudad", ValorAnterior = alumnoSecundario.NombreCiudad ?? "", ValorNuevo = nombreCiudad ?? "" });
                    alumnoSecundario.IdCiudad = alumnoPrincipal.IdCiudad;
                    alumnoSecundario.NombreCiudad = nombreCiudad;
                }
                alumnosLog.ForEach(x =>
                {
                    x.IdAlumno = alumnoSecundario.Id;
                    x.UsuarioCreacion = usuario;
                    x.UsuarioModificacion = usuario;
                    x.Estado = true;
                    x.FechaCreacion = DateTime.Now;
                    x.FechaModificacion = DateTime.Now;
                });
                _unitOfWork.AlumnoRepository.Update(alumnoSecundario);
                _unitOfWork.AlumnoLogRepository.Add(alumnosLog);
                _unitOfWork.Commit();

                //REALIZA LA REASIGNACION DE OPORTUNIDADES A UN SOLO ALUMNO
                _unitOfWork.Commit();
                var reasignacion = _unitOfWork.OportunidadRepository.ResignacionOportunidades(dto.IdAlumno, alumnoSecundario.Id);
                if (reasignacion.Valor == true)
                {
                    return (reasignacion.IdClasificacionPersonaSecundario, reasignacion.Valor);
                }
                else
                {
                    return (reasignacion.IdClasificacionPersonaPrincipal, reasignacion.Valor);
                }
            }
            catch
            {
                throw;
            }
        }
        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 23/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos del alumno
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> DTO - IntNullDTO - reasignacion </returns>

        /// Modificacion
        /// Fecha: 24/04/2024
        /// Autor: Juan D. Huanaco Quispe
        /// Se añadio soporte para los nuevos campos 'EstadoLugar', 'CodigoPostal', 'Colonia'
        public AlumnoActualizarDTO ActualizarAlumno(AlumnoActualizarDTO dto, string usuario, string areaTrabajo)
        {
            try
            {
                var validarEmail1 = _unitOfWork.AlumnoRepository.ValidarEmailPrincipal(dto.Email1);
                if (validarEmail1.Id != 0 && dto.Email1 == validarEmail1.Email1 && dto.Id != validarEmail1.Id)
                {
                    string nombreCompleto = ObtenerNombreCompletoAlumnoPorId(validarEmail1.Id);
                    if (validarEmail1.Estado && validarEmail1.EstadoCP && validarEmail1.EstadoPer)
                        throw new BadRequestException($"El Email 1 le pertenece a {nombreCompleto}");
                    else
                        throw new BadRequestException($"El Email 1 le pertenece a {nombreCompleto} - Estado 0");
                }
                //if (!string.IsNullOrEmpty(dto.Email2))
                //{
                //    var validarEmail2 = _unitOfWork.AlumnoRepository.ValidarEmailPrincipal(dto.Email2);
                //    if (validarEmail2.Id != 0)
                //    {
                //        if (dto.Email2 == validarEmail2.Email1 && dto.Id != validarEmail2.Id)
                //        {
                //            string nombreCompleto = ObtenerNombreCompletoAlumnoPorId(validarEmail2.Id);
                //            throw new BadRequestException($"El Email 2 le pertenece a {nombreCompleto}");
                //        }
                //    }
                //}
                if (dto.IdEmpresa == 0) dto.IdEmpresa = null;
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(dto.Id);
                if (_alumno == null || _alumno.Id <= 0)
                {
                    throw new BadRequestException("El Alumno no existe");
                }

                List<AlumnoLog> alumnoLogs = new List<AlumnoLog>();

                if (_alumno.Nombre1 != dto.Nombre1)
                {
                    alumnoLogs.Add(new AlumnoLog
                    {
                        IdAlumno = _alumno.Id,
                        CampoActualizado = "Nombre 1",
                        ValorAnterior = _alumno.Nombre1??"",
                        ValorNuevo = dto.Nombre1,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    });
                }
                if (_alumno.Nombre2 != dto.Nombre2)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Nombre 2", ValorAnterior = _alumno.Nombre2, ValorNuevo = dto.Nombre2 });
                if (_alumno.ApellidoPaterno != dto.ApellidoPaterno)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Apellido Paterno", ValorAnterior = _alumno.ApellidoPaterno ?? "", ValorNuevo = dto.ApellidoPaterno });
                if (_alumno.ApellidoMaterno != dto.ApellidoMaterno && (_alumno.ApellidoMaterno != null || dto.ApellidoMaterno != ""))
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Apellido Materno", ValorAnterior = _alumno.ApellidoMaterno ?? "", ValorNuevo = dto.ApellidoMaterno });


                if (_alumno.Dni != dto.Dni) {
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Dni", ValorAnterior = _alumno.Dni ?? "", ValorNuevo = dto.Dni ?? "" });
                }
                var email2 = Regex.Replace(dto.Email2, @"\s", "");
                if (_alumno.Email2 != email2)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Email 2", ValorAnterior = _alumno.Email2 ?? "", ValorNuevo = email2 ?? "" });
                if (_alumno.Celular2 != dto.Celular2)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Celular 2", ValorAnterior = _alumno.Celular2 ?? "", ValorNuevo = dto.Celular2 ?? "" });
                if (_alumno.Telefono != dto.Telefono)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Telefono", ValorAnterior = _alumno.Telefono ?? "", ValorNuevo = dto.Telefono ?? "" });
                if (_alumno.Telefono2 != dto.Telefono2)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Telefono 2", ValorAnterior = _alumno.Telefono2 ?? "", ValorNuevo = dto.Telefono2 ?? "" });
                if (_alumno.Direccion != dto.Direccion)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Direccion", ValorAnterior = _alumno.Direccion ?? "", ValorNuevo = dto.Direccion ?? "" });
                if (_alumno.IdCargo != dto.IdCargo && _alumno.Cargo != dto.Cargo && dto.IdCargo != null)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Cargo", ValorAnterior = _alumno.Cargo ?? "", ValorNuevo = dto.Cargo ?? "" });
                if (_alumno.IdAtrabajo != dto.IdAtrabajo && _alumno.Atrabajo != dto.Atrabajo && dto.IdAtrabajo != null)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Trabajo", ValorAnterior = _alumno.Atrabajo ?? "", ValorNuevo = dto.Atrabajo ?? "" });
                if (_alumno.IdEmpresa != dto.IdEmpresa && _alumno.Empresa != dto.Empresa && dto.IdEmpresa != null)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Empresa", ValorAnterior = _alumno.Empresa ?? "", ValorNuevo = dto.Empresa ?? "" });
                if (_alumno.IdAformacion != dto.IdAformacion && _alumno.Aformacion != dto.Aformacion && dto.IdAformacion != null)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Formacion", ValorAnterior = _alumno.Aformacion ?? "", ValorNuevo = dto.Aformacion ?? "" });
                if (_alumno.IdIndustria != dto.IdIndustria && _alumno.Industria != dto.Industria && dto.IdIndustria != null)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Industria", ValorAnterior = _alumno.Industria ?? "", ValorNuevo = dto.Industria ?? "" });

                if (_alumno.IdCiudad != dto.IdCiudad)
                {

                    string nombreCiudadAnterior = string.Empty;
                    string nombreCiudadNuevo = string.Empty;
                    if (_alumno.IdCiudad != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorId(_alumno.IdCiudad.Value);
                        if (resultado != null)
                            nombreCiudadAnterior = resultado.Valor;
                    }
                    if (dto.IdCiudad != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorId(dto.IdCiudad.Value);
                        if (resultado != null)
                            nombreCiudadNuevo = resultado.Valor;
                    }
                    if (_alumno.IdPais == 52)
                    {
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Estado", ValorAnterior = nombreCiudadAnterior ?? "", ValorNuevo = nombreCiudadNuevo ?? "" });
                    }
                    else
                    {
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Ciudad", ValorAnterior = nombreCiudadAnterior ?? "", ValorNuevo = nombreCiudadNuevo ?? "" });
                    }

                }
                /*-------------------------------------------------------------------------------------------------------------------------------------------*/
                if (_alumno.IdCiudadMexico != dto.IdCiudadMexico)
                {

                    string nombreCiudadMexicoAnterior = string.Empty;
                    string nombreCiudadMexicoNuevo = string.Empty;
                    if (_alumno.IdCiudadMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerCiudadMexicoById(_alumno.IdCiudadMexico.Value);
                        if (resultado != null)
                            nombreCiudadMexicoAnterior = resultado.Nombre;
                    }
                    if (dto.IdCiudadMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerCiudadMexicoById(dto.IdCiudadMexico.Value);
                        if (resultado != null)
                            nombreCiudadMexicoNuevo = resultado.Nombre;
                    }
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Ciudad", ValorAnterior = nombreCiudadMexicoAnterior ?? "", ValorNuevo = nombreCiudadMexicoNuevo ?? "" });
                }

                if (_alumno.IdMunicipioMexico != dto.IdMunicipioMexico)
                {
                    string nombreMunicipioAnterior = string.Empty;
                    string nombreMunicipioNuevo = string.Empty;
                    if (_alumno.IdMunicipioMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerMunicipioById(_alumno.IdMunicipioMexico.Value);
                        if (resultado != null)
                            nombreMunicipioAnterior = resultado.Nombre;
                    }
                    if (dto.IdMunicipioMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerMunicipioById(dto.IdMunicipioMexico.Value);
                        if (resultado != null)
                            nombreMunicipioNuevo = resultado.Nombre;
                    }
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Municipio", ValorAnterior = nombreMunicipioAnterior ?? "", ValorNuevo = nombreMunicipioNuevo ?? "" });

                }

                if (_alumno.IdAsentamientoMexico != dto.IdAsentamientoMexico)
                {

                    string nombreAsentamientoAnterior = string.Empty;
                    string nombreAsentamientoNuevo = string.Empty;
                    if (_alumno.IdAsentamientoMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerAsentammientoById(_alumno.IdAsentamientoMexico.Value);
                        if (resultado != null)
                            nombreAsentamientoAnterior = resultado.Nombre;
                    }
                    if (dto.IdAsentamientoMexico != null)
                    {
                        var resultado = _unitOfWork.CiudadRepository.ObtenerAsentammientoById(dto.IdAsentamientoMexico.Value);
                        if (resultado != null)
                            nombreAsentamientoNuevo = resultado.Nombre;
                    }
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Colonia", ValorAnterior = nombreAsentamientoAnterior ?? "", ValorNuevo = nombreAsentamientoNuevo ?? "" });

                }

                if (_alumno.EstadoLugar != dto.EstadoLugar)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "EstadoLugar", ValorAnterior = _alumno.EstadoLugar ?? "", ValorNuevo = dto.EstadoLugar ?? "" });

                if (_alumno.IdPais == 52)
                {
                    if (_alumno.Rfc != dto.Rfc)
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "RFC", ValorAnterior = _alumno.Rfc ?? "", ValorNuevo = dto.Rfc ?? "" });
                    if (_alumno.Curp != dto.Curp)
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "CURP", ValorAnterior = _alumno.Curp ?? "", ValorNuevo = dto.Curp ?? "" });
                }


                /*-------------------------------------------------------------------------------------------------------------------------------------------*/

                //if (dto.IdCiudad.GetValueOrDefault() != 0)
                //{
                //    var ciudadAlumnoDestino = _unitOfWork.CiudadRepository.ObtenerNombreCiudadPorId(dto.IdCiudad.GetValueOrDefault());
                //}
                if (dto.IdAsentamientoMexico != null)
                {
                    var codigoTmp = _unitOfWork.CiudadRepository.ObtenerCodigoPostalPorIdAsentamiento(dto.IdAsentamientoMexico.Value);
                    dto.CodigoPostal = codigoTmp.CodigoPostal;
                }



                _alumno.Nombre1 = dto.Nombre1;
                _alumno.Nombre2 = dto.Nombre2;
                _alumno.ApellidoPaterno = dto.ApellidoPaterno;
                _alumno.ApellidoMaterno = dto.ApellidoMaterno;
                _alumno.Dni = dto.Dni;
                _alumno.Email2 = email2;
                _alumno.Celular = dto.Celular;
                _alumno.Celular2 = dto.Celular2;
                _alumno.Telefono = dto.Telefono;
                _alumno.Telefono2 = dto.Telefono2;
                _alumno.Direccion = dto.Direccion;
                if (_alumno.IdPais == null || _alumno.IdPais == 0)
                    _alumno.IdPais = dto.IdCodigoPais;
                _alumno.IdCargo = dto.IdCargo == null ? _alumno.IdCargo : dto.IdCargo;
                _alumno.Cargo = dto.Cargo;
                _alumno.IdAtrabajo = dto.IdAtrabajo == null ? _alumno.IdAtrabajo : dto.IdAtrabajo;
                _alumno.Atrabajo = dto.Atrabajo;
                _alumno.IdEmpresa = dto.IdEmpresa == null ? _alumno.IdEmpresa : dto.IdEmpresa;
                _alumno.IdAformacion = dto.IdAformacion == null ? _alumno.IdAformacion : dto.IdAformacion;
                _alumno.Aformacion = dto.Aformacion;
                _alumno.IdIndustria = dto.IdIndustria == null ? _alumno.IdIndustria : dto.IdIndustria;
                _alumno.Industria = dto.Industria;
                _alumno.PrincipalResponsabilidadProfesional = dto.PrincipalResponsabilidadProfesional == null ? _alumno.PrincipalResponsabilidadProfesional : dto.PrincipalResponsabilidadProfesional;
                _alumno.IdExperiencia = dto.IdExperiencia == null ? _alumno.IdExperiencia : dto.IdExperiencia;
                _alumno.IdTamanioEmpresaAgenda = dto.IdTamanioEmpresaAgenda == null ? _alumno.IdTamanioEmpresaAgenda : dto.IdTamanioEmpresaAgenda;
                _alumno.IdCiudad = dto.IdCiudad;
                _alumno.IdMunicipioMexico = dto.IdMunicipioMexico;
                _alumno.EstadoLugar = dto.EstadoLugar;
                _alumno.CodigoPostal = dto.CodigoPostal;
                _alumno.IdAsentamientoMexico = dto.IdAsentamientoMexico;
                _alumno.IdCiudadMexico = dto.IdCiudadMexico;
                if (dto.IdCodigoPais == 52 || _alumno.IdPais == 52)
                {
                    _alumno.Curp = dto.Curp;
                    _alumno.Rfc = dto.Rfc;
                    if (dto.Rfc != null && dto.Rfc != "")
                    {
                        _alumno.Dni = dto.Rfc;
                    }
                    else
                    {
                        if (dto.Curp != null && dto.Curp != "")
                        {
                            _alumno.Dni = dto.Curp;
                        }
                    }
                }
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                if (areaTrabajo == "OP")
                {
                    if (_alumno.Genero != dto.Genero)
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Genero", ValorAnterior = _alumno.Genero ?? "", ValorNuevo = dto.Genero ?? "" });
                    if (_alumno.Parentesco != dto.Parentesco)
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Parentesco", ValorAnterior = _alumno.Parentesco ?? "", ValorNuevo = dto.Parentesco ?? "" });
                    if (_alumno.NombreFamiliar != dto.NombreFamiliar && (_alumno.NombreFamiliar != null || dto.NombreFamiliar != ""))
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Nombre Familiar", ValorAnterior = _alumno.NombreFamiliar ?? "", ValorNuevo = dto.NombreFamiliar ?? "" });
                    if (_alumno.TelefonoFamiliar != dto.TelefonoFamiliar)
                        alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Telefono Familiar", ValorAnterior = _alumno.TelefonoFamiliar ?? "", ValorNuevo = dto.TelefonoFamiliar ?? "" });

                    _alumno.Genero = dto.Genero;
                    _alumno.Parentesco = dto.Parentesco;
                    _alumno.NombreFamiliar = dto.NombreFamiliar;
                    _alumno.TelefonoFamiliar = dto.TelefonoFamiliar;
                    _alumno.FechaNacimiento = dto.FechaNacimiento;
                }
                ValidarEstadoContactoWhatsAppTemporal();
                alumnoLogs.ForEach(x =>
                {
                    x.IdAlumno = _alumno.Id;
                    x.Estado = true;
                    x.UsuarioCreacion = usuario;
                    x.UsuarioModificacion = usuario;
                    x.FechaCreacion = DateTime.Now;
                    x.FechaModificacion = DateTime.Now;
                });
                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.AlumnoLogRepository.Add(alumnoLogs);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TAlumno ActualizarAlumnoAFormacion(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdAformacion = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
  

        }
        public TAlumno ActualizarAlumnoCargo(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdCargo = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoIndustria(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdIndustria = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoAreaTrabajo(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdAtrabajo = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoEmpresa(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdEmpresa = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoTamanioEmpresaAgenda(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdTamanioEmpresaAgenda = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoExperiencia(int idAlumno, int idNuevo, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                _alumno.IdExperiencia = idNuevo;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarAlumnoPrincipalResponsabilidad(int idAlumno, string nuevoValor, string usuario)
        {
            try
            {

                List<AlumnoLog> alumnoLogs = new List<AlumnoLog>();

                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);


                if (_alumno.PrincipalResponsabilidadProfesional != nuevoValor)
                    alumnoLogs.Add(new AlumnoLog { CampoActualizado = "Principal Responsabilidad", ValorAnterior = _alumno.PrincipalResponsabilidadProfesional == null ? "" : _alumno.PrincipalResponsabilidadProfesional, ValorNuevo = nuevoValor });


                _alumno.PrincipalResponsabilidadProfesional = nuevoValor;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;



                alumnoLogs.ForEach(x =>
                {
                    x.IdAlumno = _alumno.Id;
                    x.Estado = true;
                    x.UsuarioCreacion = usuario;
                    x.UsuarioModificacion = usuario;
                    x.FechaCreacion = DateTime.Now;
                    x.FechaModificacion = DateTime.Now;
                });

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.AlumnoLogRepository.Add(alumnoLogs);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TAlumno ActualizarPerfilProfesional(PerfilProfesionalDTO perfil, string usuario)
        {
            try
            {
                _alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(perfil.IdAlumno.Value);

                _alumno.IdAformacion = perfil.IdAFormacion;
                _alumno.IdCargo = perfil.IdCargo;
                _alumno.IdAtrabajo = perfil.IdATrabajo;
                _alumno.IdIndustria = perfil.IdIndustria;
                _alumno.IdEmpresa = perfil.IdEmpresa;
                _alumno.IdTamanioEmpresaAgenda = perfil.IdTamanioEmpresa;
                _alumno.IdExperiencia = perfil.IdTiempoExperiencia;
                _alumno.PrincipalResponsabilidadProfesional = perfil.PrincipalResponsabilidad;
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.UsuarioModificacion = usuario;

                var resultadoAlumno = _unitOfWork.AlumnoRepository.Update(_alumno);
                _unitOfWork.Commit();
                return resultadoAlumno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ObtenerAlumnoPorDNI(StringDTO valor)
        {
            return _unitOfWork.AlumnoRepository.ObtenerAlumnoPorDNI(valor);
        }
        public PruebaCFD ObtenerAlumnoPorDNIV2(StringDTO valor)
        {
            return _unitOfWork.AlumnoRepository.ObtenerAlumnoPorDNIV2(valor);
        }
        public InformacionAlumnoDTO ObtenerInformacionAlumno(int idAlumno)
        {
            return _unitOfWork.AlumnoRepository.ObtenerInformacionAlumno(idAlumno);
        }



    }

}
