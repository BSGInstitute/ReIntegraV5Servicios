using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: GmailCorreoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GmailCorreo
    /// </summary>
    public class GmailCorreoService : IGmailCorreoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public List<GmailCorreoArchivoAdjuntoDTO> ListaGmailCorreoArchivoAdjunto = new List<GmailCorreoArchivoAdjuntoDTO>();
        public GmailCorreoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGmailCorreo, GmailCorreo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GmailCorreo Add(GmailCorreo entidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailCorreoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GmailCorreo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GmailCorreo Update(GmailCorreo entidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailCorreoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GmailCorreo>(modelo);
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
                _unitOfWork.GmailCorreoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GmailCorreo> Add(List<GmailCorreo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailCorreoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GmailCorreo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GmailCorreo> Update(List<GmailCorreo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GmailCorreoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GmailCorreo>>(modelo);
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
                _unitOfWork.GmailCorreoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GmailCorreo
        /// </summary>
        /// <returns> List<GmailCorreoDTO> </returns>
        public IEnumerable<GmailCorreoDTO> ObtenerGmailCorreo()
        {
            try
            {
                return _unitOfWork.GmailCorreoRepository.ObtenerGmailCorreo();
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
        /// Obtiene registros de T_GmailCorreo para mostrarse en combo.
        /// </summary>
        /// <returns> List<GmailCorreoComboDTO> </returns>
        public IEnumerable<GmailCorreoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.GmailCorreoRepository.ObtenerCombo();
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
        /// Obtiene todos los correos enviados por el asesor segun ciertos filtros
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtros para Bandeja Correo</param>
        /// <returns> List<GmailCorreoComboDTO> </returns>
        public BandejaCorreoEnviadoPorPersonalDTO ObtenerCorreosEnviadosPorFiltroBandeja(FiltroBandejaCorreoDTO filtroBandejaCorreo)
        {
            try
            {
                string filtroAsunto = string.Empty, filtroDestinatarios = string.Empty, filtroRemitente = string.Empty;
                if (filtroBandejaCorreo.FiltroKendo != null)
                {
                    foreach (var filtro in filtroBandejaCorreo.FiltroKendo.Filters)
                    {
                        switch (filtro.Field)
                        {
                            case "asunto":
                                filtroAsunto = filtro.Value;
                                break;
                            case "destinatarios":
                                filtroDestinatarios = filtro.Value;
                                break;
                            case "destinatario":
                                filtroDestinatarios = filtro.Value;
                                break;
                            case "remitente":
                                filtroRemitente = filtro.Value;
                                break;
                        }
                    }
                }
                if (filtroBandejaCorreo.Take == 0) filtroBandejaCorreo.Take = 10000;
                if (filtroBandejaCorreo.TipoCorreos == "Normal") filtroBandejaCorreo.IdAsesor = 0;

                var correosEnviados = _unitOfWork.GmailCorreoRepository.ObtenerCorreosEnviadosPorFiltroBandeja(
                    new FiltroBandejaCorreoParaRepositorioDTO()
                    {
                        IdPersonal = filtroBandejaCorreo.IdAsesor,
                        Skip = filtroBandejaCorreo.Skip,
                        Take = filtroBandejaCorreo.Take,
                        Destinatarios = filtroDestinatarios,
                        Asunto = filtroAsunto,
                        Remitente = filtroRemitente
                    }
                );
                var bandejaSalida = new BandejaCorreoEnviadoPorPersonalDTO
                {
                    ListaCorreos = correosEnviados,
                    TotalEnviados = 0
                };
                if (correosEnviados.Count() > 0)
                {
                    bandejaSalida.TotalEnviados = correosEnviados.ToList()[0].TotalCorreos ?? 0;
                }
                return bandejaSalida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 25/08/2022
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirArchivo(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                return _unitOfWork.GmailCorreoRepository.SubirArchivo(archivo, tipo, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public async Task<string> SubirArchivoAsync(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                return await _unitOfWork.GmailCorreoRepository.SubirArchivoAsync(archivo, tipo, nombreArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ConvertToByte(IFormFile file)
        {
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            byte[] imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 25/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia mensaje
        /// </summary>
        /// <param name="DatosOportunidad"></param>
        /// <param name="MensajeCabecera"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        public async Task<bool> EnviarMensajeCorreo(ParametrosEnviarMensajeDTO informacionCorreo, IList<IFormFile> Files, string usuario)
        {
            try
            {
                if (informacionCorreo.Remitente == null || informacionCorreo.Remitente.Trim() == "")
                    throw new BadRequestException("Remitente Vacio");
                if (informacionCorreo.Destinatario == null || informacionCorreo.Destinatario == "")
                    throw new BadRequestException("No tiene destinatarios");

                var asesor = _unitOfWork.PersonalRepository.ObtenerNombreApellido(informacionCorreo.Remitente);

                byte[] dataMensaje = Convert.FromBase64String(informacionCorreo.Mensaje);
                var mensajeCorreo = Encoding.UTF8.GetString(dataMensaje);

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }


                informacionCorreo.Destinatario = informacionCorreo.Destinatario.Replace("<", "").Replace(">", "");

                var mailData = new TMKMailDataDTO
                {
                    Sender = informacionCorreo.Remitente,
                    Recipient = informacionCorreo.Destinatario,
                    Subject = informacionCorreo.Asunto,
                    Message = mensajeCorreo,
                    Cc = informacionCorreo.DestinatarioCc ?? "",
                    Bcc = informacionCorreo.DestinatarioBcc ?? "",
                    AttachedFiles = null,
                    RemitenteC = string.Concat(asesor.Nombres, ' ', asesor.Apellidos)
                };

                TMK_MailService serviceMail = new TMK_MailService();
                serviceMail.SetData(mailData);
                if (Files != null && Files.Count() > 0)
                {
                    foreach (var file in Files)
                    {
                        serviceMail.SetFiles(file);
                    }
                }
                var listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();
                List<TMKMensajeIdDTO> MensajeIdDTO = serviceMail.SendMessageTask();

                foreach (var mensaje in MensajeIdDTO)
                {
                    var validarEmail = _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(mensaje.Email);
                    int idAlumno = validarEmail == null ? 0 : validarEmail.Id;
                    var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                    {
                        IdOportunidad = informacionCorreo.IdOportunidad,
                        IdPersonal = asesor.Id,
                        IdAlumno = idAlumno,
                        IdCentroCosto = informacionCorreo.IdCentroCosto,
                        IdMandrilTipoAsignacion = informacionCorreo.IdOportunidad == 0 ? 4 : 0, //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 2, //Manual = 2
                        FechaEnvio = DateTime.Now,
                        Asunto = informacionCorreo.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        EsEnvioMasivo = false
                    };
                    listaMandrilEnvioCorreo.Add(mandrilEnvioCorreoEntidad);
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Logica Guardar Correo
                    GmailCorreo gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = informacionCorreo.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = mensajeCorreo,
                        Seen = false,
                        Remitente = informacionCorreo.Remitente,
                        Cc = informacionCorreo.DestinatarioCc,
                        Bcc = informacionCorreo.DestinatarioBcc,
                        Destinatarios = informacionCorreo.Destinatario,
                        IdPersonal = asesor.Id,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        IdClasificacionPersona = informacionCorreo.IdClasificacionPersona,
                        Estado = true
                    };

                    gmailCorreo.GmailCorreoArchivoAdjuntos = new List<GmailCorreoArchivoAdjunto>();
                    string urlArchivo = "";
                    if (Files != null && Files.Count() > 0)
                    {
                        Task<string>[] tasks = new Task<string>[Files.Count()];
                        for (int i = 0; i < Files.Count(); i++)
                        {
                            var nombreArchivo = string.Concat(gmailCorreo.Id, '-', DateTime.Now.ToString("yyyyMMddHHmmss"), '-', Files[i].FileName);
                            tasks[i] = _unitOfWork.GmailCorreoRepository.SubirArchivoAsync(serviceMail.ConvertToByte(Files[i]), Files[i].ContentType, nombreArchivo);
                        }

                        for (int i = 0; i < Files.Count(); i++)
                        {
                            urlArchivo = await tasks[i];
                            var gmailCorreoArchivoAdjunto = new GmailCorreoArchivoAdjunto
                            {
                                Nombre = Files[i].FileName,
                                UrlArchivoRepositorio = urlArchivo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            gmailCorreo.GmailCorreoArchivoAdjuntos.Add(gmailCorreoArchivoAdjunto);
                        }
                    }
                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);

                    if (informacionCorreo.IdActividadDetalle != null && informacionCorreo.IdActividadDetalle != 0)
                    {
                        Interaccion interacionEntidad = new Interaccion()
                        {
                            IdActividadDetalle = informacionCorreo.IdActividadDetalle,
                            IdTipoInteraccionGeneral = 1,
                            Fecha = DateTime.Now,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        _unitOfWork.InteraccionRepository.Add(interacionEntidad);
                    }

                    _unitOfWork.MandrilEnvioCorreoRepository.Add(listaMandrilEnvioCorreo);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 25/08/2022
        /// Version: 1.0
        /// <summary>
        /// Envia mensaje
        /// </summary>
        /// <param name="DatosOportunidad"></param>
        /// <param name="MensajeCabecera"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        public async Task<bool> EnviarMensajeCorreoPla(ParametrosEnviarMensajePlaDTO informacionCorreo, IList<IFormFile> Files, string usuario)
        {
            try
            {
                if (informacionCorreo.Remitente == null || informacionCorreo.Remitente.Trim() == "")
                    throw new BadRequestException("Remitente Vacio");
                if (informacionCorreo.Destinatario == null || informacionCorreo.Destinatario == "")
                    throw new BadRequestException("No tiene destinatarios");

                var asesor = _unitOfWork.PersonalRepository.ObtenerNombreApellido(informacionCorreo.Remitente);

                byte[] dataMensaje = Convert.FromBase64String(informacionCorreo.Mensaje);
                var mensajeCorreo = Encoding.UTF8.GetString(dataMensaje);

                if (!mensajeCorreo.Contains("https://repositorioweb.blob.core.windows.net/firmas/"))
                {
                    string firma = string.Empty;
                    string[] separacionEmail = asesor.Email.Split('@');
                    firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + separacionEmail[0] + ".png' />";
                    mensajeCorreo += "<br/>--<br/>" + firma;
                }


                informacionCorreo.Destinatario = informacionCorreo.Destinatario.Replace("<", "").Replace(">", "");

                var mailData = new TMKMailDataDTO
                {
                    Sender = informacionCorreo.Remitente,
                    Recipient = informacionCorreo.Destinatario,
                    Subject = informacionCorreo.Asunto,
                    Message = mensajeCorreo,
                    Cc = informacionCorreo.DestinatarioCc ?? "",
                    Bcc = informacionCorreo.DestinatarioBcc ?? "",
                    AttachedFiles = null,
                    RemitenteC = string.Concat(asesor.Nombres, ' ', asesor.Apellidos)
                };

                TMK_MailService serviceMail = new TMK_MailService();
                serviceMail.SetData(mailData);
                if (Files != null && Files.Count() > 0)
                {
                    foreach (var file in Files)
                    {
                        serviceMail.SetFiles(file);
                    }
                }
                var listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();
                List<TMKMensajeIdDTO> MensajeIdDTO = serviceMail.SendMessageTask();

                foreach (var mensaje in MensajeIdDTO)
                {
                    var validarEmail = _unitOfWork.AlumnoRepository.ValidarEmailProveedor(mensaje.Email);
                    int idClasificacionPersona = validarEmail == null ? 0 : validarEmail.Id;
                    var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                    {
                        //IdGestionContacto = informacionCorreo.IdGestionContacto, PENDIENTE AJUSTE DTO
                        IdPersonal = asesor.Id,
                        //IdClasificacionPersona = idClasificacionPersona,
                        IdCentroCosto = informacionCorreo.IdCentroCosto,
                        //IdMandrilTipoAsignacion = informacionCorreo.IdOportunidad == 0 ? 4 : 0, PENDIENTE AJUSTE DTO
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 2, //Manual = 2 PENDIENTE AJUSTE DTO
                        FechaEnvio = DateTime.Now,
                        Asunto = informacionCorreo.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        EsEnvioMasivo = false
                    };
                    listaMandrilEnvioCorreo.Add(mandrilEnvioCorreoEntidad);
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Logica Guardar Correo
                    GmailCorreo gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = informacionCorreo.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = mensajeCorreo,
                        Seen = false,
                        Remitente = informacionCorreo.Remitente,
                        Cc = informacionCorreo.DestinatarioCc,
                        Bcc = informacionCorreo.DestinatarioBcc,
                        Destinatarios = informacionCorreo.Destinatario,
                        IdPersonal = asesor.Id,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        IdClasificacionPersona = informacionCorreo.IdClasificacionPersona,
                        Estado = true
                    };

                    gmailCorreo.GmailCorreoArchivoAdjuntos = new List<GmailCorreoArchivoAdjunto>();
                    string urlArchivo = "";
                    if (Files != null && Files.Count() > 0)
                    {
                        Task<string>[] tasks = new Task<string>[Files.Count()];
                        for (int i = 0; i < Files.Count(); i++)
                        {
                            var nombreArchivo = string.Concat(gmailCorreo.Id, '-', DateTime.Now.ToString("yyyyMMddHHmmss"), '-', Files[i].FileName);
                            tasks[i] = _unitOfWork.GmailCorreoRepository.SubirArchivoAsync(serviceMail.ConvertToByte(Files[i]), Files[i].ContentType, nombreArchivo);
                        }

                        for (int i = 0; i < Files.Count(); i++)
                        {
                            urlArchivo = await tasks[i];
                            var gmailCorreoArchivoAdjunto = new GmailCorreoArchivoAdjunto
                            {
                                Nombre = Files[i].FileName,
                                UrlArchivoRepositorio = urlArchivo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            gmailCorreo.GmailCorreoArchivoAdjuntos.Add(gmailCorreoArchivoAdjunto);
                        }
                    }
                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);

                    if (informacionCorreo.IdActividadDetalle != null && informacionCorreo.IdActividadDetalle != 0)
                    {
                        Interaccion interacionEntidad = new Interaccion()
                        {
                            IdActividadDetalle = informacionCorreo.IdActividadDetalle,
                            IdTipoInteraccionGeneral = 1,
                            Fecha = DateTime.Now,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        _unitOfWork.InteraccionRepository.Add(interacionEntidad);
                    }

                    _unitOfWork.MandrilEnvioCorreoRepository.Add(listaMandrilEnvioCorreo);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio Rodrigo
        /// Fecha: 02/02/2022
        /// Version: 1.0
        /// <summary>
        /// Limpia la cadena de caracteres html y retiras las tildes de la cadena
        /// </summary>
        /// <returns>Cadena limpia</returns>
        private string LimpiarCadena(string valor)
        {
            string decodeString = HttpUtility.HtmlDecode(valor);
            string valorSinTildes = Regex.Replace(decodeString.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            return valorSinTildes;
        }
        /// Autor:Gilmer Quispe.
        /// Fecha: 26/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea GmailCorreoDTO a una lista de GmailCorreo
        /// </summary>
        /// <param name="items">Lista de DTOs</param>
        /// <returns> List<GmailCorreo> </returns>
        public List<GmailCorreo> MapeoEntidadesDesdeListaDTO(List<GmailCorreoDTO> items)
        {
            try
            {
                var entidades = _mapper.Map<List<GmailCorreo>>(items);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gilmer Quispe.
        /// Fecha: 26/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de un GmailCorreoDTO a ActividadDetalle
        /// </summary>
        /// <returns> GmailCorreo </returns>
        public GmailCorreo MapeoEntidadDesdeDTO(GmailCorreoDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<GmailCorreo>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gilmer Quispe.
        /// Fecha: 26/08/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene el correo enviado por el ID
        /// </summary>
        /// <returns> CorreoBodyDTO </returns>
        public CorreoBodyDTO ObtenerCorreoEnviadoPorId(int IdGmailCorreo, string Usuario)
        {
            try
            {
                GmailCorreoArchivoAdjuntoDTO gmailcorreoarchivoDTO = new GmailCorreoArchivoAdjuntoDTO();
                var serviceGmailCorreoArchivoAdjunto = new GmailCorreoArchivoAdjuntoService(_unitOfWork);
                var serviceGmailCorreo = new GmailCorreoService(_unitOfWork);
                var gmailcorreo = ObtenerCorreoPorId(IdGmailCorreo);
                gmailcorreo.Seen = true;
                gmailcorreo.FechaModificacion = DateTime.Now;
                gmailcorreo.UsuarioModificacion = Usuario;
                if (gmailcorreo != null)
                {
                    serviceGmailCorreo.Update(gmailcorreo);
                }
                else
                {
                    return null;
                }
                var correo = new CorreoBodyDTO()
                {
                    EmailBody = gmailcorreo.EmailBody
                };


                correo.ArchivosAdjuntos = serviceGmailCorreoArchivoAdjunto.obtenerCorreoArchivoAdjuntoPorId(IdGmailCorreo).
                    Select(x => new CorreoArchivoAdjuntoDTO()
                    {
                        IdCorreo = x.Id,
                        NombreArchivo = x.Nombre,
                        UrlArchivoRepositorio = x.UrlArchivoRepositorio
                    }).ToList();
                return (correo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el correo por el Id
        /// </summary>
        /// <param name="idCorreo"></param>
        /// <returns>GmailCorreo</returns>
        public GmailCorreo ObtenerCorreoPorId(int idCorreo)
        {
            try
            {
                return _unitOfWork.GmailCorreoRepository.ObtenerCorreoPorId(idCorreo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados de ventas a los alumnos
        /// </summary>
        /// <param name="emailAlumno"></param>
        public List<CorreoAlumnoVentasDTO> ObtenerCorreosAlumnosSoloVentas(string emailAlumno)
        {
            try
            {
                return _unitOfWork.GmailCorreoRepository.ObtenerCorreosAlumnosSoloVentas(emailAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
