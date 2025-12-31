using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FacebookAudiencia;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Azure;
using Newtonsoft.Json;
using sib_api_v3_sdk.Client;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using Error = BSI.Integra.Aplicacion.DTOs.Error;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{

    /// Service: FiltroSegmentoService
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class FiltroSegmentoService : IFiltroSegmentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private string UrlApiFacebook = "http://integra.bsgrupo.net:84/apps/external/facebookads3-v4/public/index.php/";
        //private string UrlApiFacebook = "http://localhost:84/facebookads3/public/";
        private string urlApiGraphV15 = "https://graph.facebook.com/v20.0/"; 
        private string accessToken = "EAAIbfrfWKZCABADDrWyma2h3YyZBI1Dh66wxhqFGv58Ot4QzLEXyZAVZBFJd5hj3r9JAym56mMVv5DWN7xjdj0DG1RFI1aa4FubQV9IX39jL1JtTBU7ks2TFNjbCCQQbrrf65hWjejeqwOKCv46ojuEUbZB7VhErXMtHzjcbOgJuCtVHBPLFZA";
        public FiltroSegmentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //new ValorEstatico(_unitOfWork);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFiltroSegmento, FiltroSegmento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAlumno, Alumno>(MemberList.None).ReverseMap();
                cfg.CreateMap<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutado>(MemberList.None).ReverseMap();

            });

            _mapper = new Mapper(config);
        }
        

        #region Metodos Base
        public FiltroSegmento Add(FiltroSegmento entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmento>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FiltroSegmento Update(FiltroSegmento entidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FiltroSegmento>(modelo);
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
                _unitOfWork.FiltroSegmentoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmento> Add(List<FiltroSegmento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmento>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FiltroSegmento> Update(List<FiltroSegmento> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FiltroSegmentoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FiltroSegmento>>(modelo);
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
                _unitOfWork.FiltroSegmentoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {

                return _unitOfWork.FiltroSegmentoRepository.ObtenerCombo();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO.ComboDTO> ObtenerSubArea(string idAreas)
        {
            try
            {
                return _unitOfWork.FiltroSegmentoRepository.ObtenerSubArea(idAreas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO.ComboDTO> ObtenerProgramaSubArea(string idAreas, string idSubAreas)
        {
            try
            {
                return _unitOfWork.FiltroSegmentoRepository.ObtenerProgramaSubArea(idAreas, idSubAreas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO.ComboDTO> ObtenerProgramaEspecifico(string IdProgramaGeneral)
        {
            try
            {
                return _unitOfWork.FiltroSegmentoRepository.ObtenerProgramaEspecifico(IdProgramaGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {

                return _unitOfWork.FiltroSegmentoRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EjecutarFiltroOrginal(int Id, string UsuarioModificacion)
        {
            List<AddresseeDTO> correosPersonalizados = new List<AddresseeDTO>();
            AddresseeDTO UsuarioEjecucion = new AddresseeDTO();
            UsuarioEjecucion.Email = UsuarioModificacion + "@bsginstitute.com";
            DateTime horaInicio = DateTime.Now;
            try
            {


                if (!_unitOfWork.FiltroSegmentoRepository.Exist(Id))
                {
                    throw new Exception("Filtro segmento no existente.");
                }

                try
                {

                    //var MailservicePersonalizado = new TMK_MailService();
                    //var mailDataPersonalizado = new TMKMailDataDTO
                    //{
                    //    Sender = "wchoque@bsginstitute.com",
                    //    Recipient = string.Join(",", correosPersonalizados),
                    //    Subject = string.Concat("Procesar filtro segmento - ", _unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre),
                    //    Message = $@"
                    //<p style='color: red;'><strong>----Servicio de confirmación de filtro segmento----</strong></p>
                    //<p>El filtro segmento <strong>{_unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre}<span style='color: green;'> FINALIZO CORRECTAMENTE</span></strong></p>
                    //<p><strong>Hora de Inicio:</strong></p>
                    //<p><span style='color: orange;'>{horaInicio}</span></p>
                    //<p><strong>Hora de Finalizacion:</strong></p>
                    //<p><span style='color: orange;'>{DateTime.Now}</span></p> 
                    //",
                    //    Cc = "",
                    //    Bcc = "",
                    //    AttachedFiles = null
                    //};
                    //MailservicePersonalizado.SetData(mailDataPersonalizado);
                    //MailservicePersonalizado.SendMessageTask();
                    //return (true);


                    correosPersonalizados = _unitOfWork.FiltroSegmentoRepository.ObtenerAddressee();
                    correosPersonalizados.Add(UsuarioEjecucion);
                    string Mensaje1 = $@"
                    <p style='color: red;'><strong>----Servicio de confirmación de inicio de procesamiento de filtro segmento----</strong></p>
                    <p>El filtro segmento <strong>{_unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre}<span style='color: green;'> está listo para iniciar el procesamiento</span></strong></p>
                    <p><strong>Hora de Inicio:</strong></p>
                    <p><span style='color: orange;'>{horaInicio}</span></p>";
                    EnvioCorreo("Filtro Segmento", "Se va procesar el siguiente Filtro Segmento: " + _unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre, Mensaje1, correosPersonalizados);
                    this.VerificarEjecución(Id, UsuarioModificacion);
                    string Mensaje = $@"
                    <p style='color: red;'><strong>----Servicio de confirmación de filtro segmento----</strong></p>
                    <p>El filtro segmento <strong>{_unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre}<span style='color: green;'> FINALIZO CORRECTAMENTE</span></strong></p>
                    <p><strong>Hora de Inicio:</strong></p>
                    <p><span style='color: orange;'>{horaInicio}</span></p>
                    <p><strong>Hora de Finalizacion:</strong></p>
                    <p><span style='color: orange;'>{DateTime.Now}</span></p>";
                    EnvioCorreo("Filtro Segmento", "Procesar filtro segmento: " + _unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre, Mensaje, correosPersonalizados);

                }
                catch
                {

                }

                return true;
            }
            catch (Exception ex)
            {
                correosPersonalizados = _unitOfWork.FiltroSegmentoRepository.ObtenerAddressee();
                correosPersonalizados.Add(UsuarioEjecucion);
                string Mensaje1 = "Ocurrió un error inesperado";
                EnvioCorreo("Filtro Segmento", "Procesar filtro segmento - Error.", Mensaje1, correosPersonalizados);

                throw ex;
            }
        }


        public bool EjecutarFiltro(int Id, string UsuarioModificacion)
        {
            var filtroSegmentoService = new FiltroSegmentoService(_unitOfWork);
            DateTime horaInicio = DateTime.Now;

            try
            {
                if (!_unitOfWork.FiltroSegmentoRepository.Exist(Id))
                {
                    throw new Exception("Filtro segmento no existente!");
                }
                var filtroSegmento = new FiltroSegmento()
                {
                    Id = Id,
                    UsuarioCreacion = UsuarioModificacion
                };
                filtroSegmentoService.VerificarEjecución(Id, UsuarioModificacion);

                var _repIntegraAspNetUsers = new IntegraAspNetUser();
                var correosPersonalizados = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "mramirez@bsginstitute.com"
                };

                if (_unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(UsuarioModificacion))
                {
                    try
                    {
                        correosPersonalizados.Add(_unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(UsuarioModificacion));
                    }
                    catch (Exception)
                    {
                    }
                }


                var MailservicePersonalizado = new TMK_MailService();
                var mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Procesar filtro segmento - ", _unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre),
                    Message = $@"
                    <p style='color: red;'><strong>----Servicio de confirmación de filtro segmento----</strong></p>
                    <p>El filtro segmento <strong>{_unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre}<span style='color: green;'> FINALIZO CORRECTAMENTE</span></strong></p>
                    <p><strong>Hora de Inicio:</strong></p>
                    <p><span style='color: orange;'>{horaInicio}</span></p>
                    <p><strong>Hora de Finalizacion:</strong></p>
                    <p><span style='color: orange;'>{DateTime.Now}</span></p> 
                    ",
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();
                return (true);
            }
            catch (Exception e)
            {

             

                List<string> correos = new List<string>
                {
                    "mramirez@bsginstitute.com",
                  
                };
                if (_unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(UsuarioModificacion))
                {
                    try
                    {
                        correos.Add(_unitOfWork.IntegraAspNetUserRepository.ObtenerEmailFiltro(UsuarioModificacion));
                    }
                    catch (Exception)
                    {
                    }
                }
                TMK_MailService Mailservice = new TMK_MailService();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar filtro segmento - Error ", _unitOfWork.FiltroSegmentoRepository.FirstById(Id).Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e),
                    $@"

                        >>>>> Servicio de confirmación de filtro segmento <<<<<
                    "
                    ),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

             
            throw new Exception(e.Message);
        }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que manda correos por sender
        /// </summary>
        /// <returns>bool</returns>
        public bool EnvioCorreo(string displayname, string subject, string mensaje, List<AddresseeDTO> listaReceptores)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    SenderDTO Sender = new SenderDTO();
                    Sender = _unitOfWork.AsignacionRegularRepository.ObtenerSender();


                    //CONFIGURACION DEL MENSAJE
                    MailMessage mail = new MailMessage();
                    mail.To.Add("emayta@bsginstitute.com");

                    if (listaReceptores != null && listaReceptores.Count > 0)
                    {
                        foreach (var copia in listaReceptores)
                        {
                            mail.Bcc.Add(copia.Email);
                        }
                    }
                    mail.From = new MailAddress("emayta@bsginstitute.com", displayname, System.Text.Encoding.UTF8);
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



        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que manda correos por sender
        /// </summary>
        /// <returns>bool</returns>
        public bool VerificarEjecución(int Id, string UsuarioModificacion)
        {
            {
                try
                {
                    _unitOfWork.FiltroSegmentoRepository.EliminarPorFiltroSegmento(Id, UsuarioModificacion);
                    return this.EjecutarFiltroSegmento(this.ObtenerFiltroValorPorIdFiltroSegmento(Id, UsuarioModificacion));
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public FiltroSegmentoDTO ObtenerFiltroValorPorIdFiltroSegmento(int Id, string UsuarioModificacion)
        {
            try
            {
                FiltroSegmentoDTO filtroSegmento = _unitOfWork.FiltroSegmentoRepository.ObtenerFiltroSegmentoDatosPorId(Id);
                List<FiltroSegmentoValorTipoDTO> lista = new List<FiltroSegmentoValorTipoDTO>();
                lista = _unitOfWork.FiltroSegmentoRepository.ObtenerFiltroValorPorIdFiltroSegmento(Id);
                List<FiltroSegmentoDetalleDTO> listaDetalle = new List<FiltroSegmentoDetalleDTO>();
                listaDetalle = _unitOfWork.FiltroSegmentoRepository.ObtenerDetallePorIdFiltroSegmento(Id);


                //se agrega usuario ejecucucion
                filtroSegmento.NombreUsuario = UsuarioModificacion;

                filtroSegmento.ListaArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                filtroSegmento.ListaSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                filtroSegmento.ListaProgramaGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                filtroSegmento.ListaProgramaEspecifico = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico).ToList();
                //filtroSegmento.ListaScore = lista.Where(s => s.Tipo.Equals(ValorEstatico.SegmentoTipoScore)).ToList();

                filtroSegmento.ListaOportunidadInicialFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadInicialFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual).ToList();
                filtroSegmento.ListaOportunidadActualFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadActualFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual).ToList();

                filtroSegmento.ListaPais = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPais).ToList();
                filtroSegmento.ListaCiudad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCiudad).ToList();

                filtroSegmento.ListaTipoCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen).ToList();
                filtroSegmento.ListaCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen).ToList();

                filtroSegmento.ListaCargo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCargo).ToList();
                filtroSegmento.ListaIndustria = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroIndustria).ToList();
                filtroSegmento.ListaAreaFormacion = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion).ToList();
                filtroSegmento.ListaAreaTrabajo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo).ToList();

                filtroSegmento.ListaTipoFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario).ToList();
                filtroSegmento.ListaTipoInteraccionFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario).ToList();

                filtroSegmento.ListaProbabilidadOportunidad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad).ToList();
                filtroSegmento.ListaActividadLlamada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada).ToList();

                filtroSegmento.ListaVCArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCArea).ToList();
                filtroSegmento.ListaVCSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCSubArea).ToList();
                filtroSegmento.ListaVCPGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral).ToList();

                filtroSegmento.ListaProbabilidadVentaCruzada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada).ToList();

                filtroSegmento.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir).ToList();

                filtroSegmento.ListaExcluirPorFiltroSegmento = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento).ToList();
                filtroSegmento.ListaExcluirPorConjuntoLista = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista).ToList();
                filtroSegmento.ListaExcluirPorCampaniaMailing = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing).ToList();

                filtroSegmento.ListaActividadCabecera = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera).ToList();
                filtroSegmento.ListaOcurrencia = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOcurrencia).ToList();
                filtroSegmento.ListaDocumentoAlumno = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno).ToList();
                filtroSegmento.ListaEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula).ToList();
                filtroSegmento.ListaSubEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula).ToList();
                filtroSegmento.ListaModalidadCurso = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso).ToList();

                filtroSegmento.ListaEstadoAcademico = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico).ToList();
                filtroSegmento.ListaEstadoPago = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoPago).ToList();
                filtroSegmento.ListaPorcentajeAvance = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance).ToList();
                filtroSegmento.ListaEstadoLlamada = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada).ToList();
                filtroSegmento.ListaSesion = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesion).ToList();

                filtroSegmento.ListaSesionWebinar = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar).ToList();
                filtroSegmento.ListaTrabajoAlumno = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno).ToList();
                filtroSegmento.ListaTrabajoAlumnoFinal = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal).ToList();

                filtroSegmento.ListaTarifario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTarifario).ToList();

                filtroSegmento.ListaEnvioAutomaticoOportunidadFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual).ToList();

                return filtroSegmento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EjecutarFiltroSegmento(FiltroSegmentoDTO obj)
        {
            if (!obj.ConsiderarFiltroGeneral || !obj.ConsiderarFiltroEspecifico)
            {
                throw new Exception("No seleccionó considerar filtro General o Especifico");
            }

            try
            {
                switch (obj.IdFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        _unitOfWork.FiltroSegmentoRepository.EjecutarFiltroTipoContactoAlumnoExAlumno(obj);
                        break;
                    case 6:///prospecto
                        _unitOfWork.FiltroSegmentoRepository.EjecutarFiltroTipoContactoProspecto(obj);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoDTO ObtenerDetalleFiltroSegmento(int IdFiltroSegmento)
        {
            if (!_unitOfWork.FiltroSegmentoRepository.Exist(IdFiltroSegmento))
            {
                throw new Exception("Filtro segmento no existente!");
            }

            try
            {
                var filtroSegmento = _unitOfWork.FiltroSegmentoRepository.ObtenerFiltroSegmentoDatosPorId(IdFiltroSegmento);
                var lista = _unitOfWork.FiltroSegmentoRepository.ObtenerFiltroValorPorIdFiltroSegmento(IdFiltroSegmento);
                var listaDetalle = _unitOfWork.FiltroSegmentoRepository.ObtenerDetallePorIdFiltroSegmento(IdFiltroSegmento);


                //se agrega usuario ejecucucion
                filtroSegmento.ListaArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                filtroSegmento.ListaSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                filtroSegmento.ListaProgramaGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                filtroSegmento.ListaProgramaEspecifico = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico).ToList();
                //filtroSegmento.ListaScore = lista.Where(s => s.Tipo.Equals(ValorEstatico.SegmentoTipoScore)).ToList();

                filtroSegmento.ListaOportunidadInicialFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadInicialFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual).ToList();
                filtroSegmento.ListaOportunidadActualFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadActualFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual).ToList();

                filtroSegmento.ListaPais = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPais).ToList();
                filtroSegmento.ListaCiudad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCiudad).ToList();

                filtroSegmento.ListaTipoCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen).ToList();
                filtroSegmento.ListaCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen).ToList();

                filtroSegmento.ListaCargo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCargo).ToList();
                filtroSegmento.ListaIndustria = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroIndustria).ToList();
                filtroSegmento.ListaAreaFormacion = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion).ToList();
                filtroSegmento.ListaAreaTrabajo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo).ToList();

                filtroSegmento.ListaTipoFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario).ToList();
                filtroSegmento.ListaTipoInteraccionFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario).ToList();

                filtroSegmento.ListaProbabilidadOportunidad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad).ToList();
                filtroSegmento.ListaActividadLlamada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada).ToList();

                filtroSegmento.ListaVCArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCArea).ToList();
                filtroSegmento.ListaVCSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCSubArea).ToList();
                filtroSegmento.ListaVCPGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral).ToList();

                filtroSegmento.ListaProbabilidadVentaCruzada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada).ToList();

                filtroSegmento.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir).ToList();

                filtroSegmento.ListaExcluirPorFiltroSegmento = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento).ToList();
                filtroSegmento.ListaExcluirPorConjuntoLista = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista).ToList();
                filtroSegmento.ListaExcluirPorCampaniaMailing = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing).ToList();

                filtroSegmento.ListaActividadCabecera = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera).ToList();
                filtroSegmento.ListaOcurrencia = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOcurrencia).ToList();
                filtroSegmento.ListaDocumentoAlumno = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno).ToList();
                filtroSegmento.ListaEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula).ToList();
                filtroSegmento.ListaSubEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula).ToList();
                filtroSegmento.ListaModalidadCurso = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso).ToList();

                filtroSegmento.ListaEstadoAcademico = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico).ToList();
                filtroSegmento.ListaEstadoPago = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoPago).ToList();
                filtroSegmento.ListaPorcentajeAvance = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance).ToList();
                filtroSegmento.ListaEstadoLlamada = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada).ToList();
                filtroSegmento.ListaSesion = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesion).ToList();

                filtroSegmento.ListaSesionWebinar = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar).ToList();
                filtroSegmento.ListaTrabajoAlumno = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno).ToList();
                filtroSegmento.ListaTrabajoAlumnoFinal = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal).ToList();

                filtroSegmento.ListaTarifario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTarifario).ToList();

                filtroSegmento.ListaEnvioAutomaticoOportunidadFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual).ToList();

                filtroSegmento.ListaUOArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUOArea).ToList();
                filtroSegmento.ListaUOSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUOSubArea).ToList();
                filtroSegmento.ListaUOPGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUOPGeneral).ToList();

                return filtroSegmento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool InsertarFiltro(FiltroSegmentoDTO obj, String UsuarioModificacion)
        {

            try
            {


                FiltroSegmento FiltroNuevo = new FiltroSegmento();
                FiltroNuevo.Nombre = obj.Nombre;
                FiltroNuevo.Descripcion = obj.Descripcion;
                FiltroNuevo.IdFiltroSegmentoTipoContacto = obj.IdFiltroSegmentoTipoContacto;
                FiltroNuevo.FechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                FiltroNuevo.FechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;
                FiltroNuevo.FechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                FiltroNuevo.FechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;
                FiltroNuevo.FechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2;
                FiltroNuevo.FechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2;
                FiltroNuevo.EsRn2 = obj.EsRn2;
                FiltroNuevo.IdOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                FiltroNuevo.NroOportunidades = obj.NroOportunidades;
                FiltroNuevo.IdOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                FiltroNuevo.NroSolicitudInformacion = obj.NroSolicitudInformacion;

                //
                FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                FiltroNuevo.NroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                FiltroNuevo.NroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                FiltroNuevo.NroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;
                //

                FiltroNuevo.FechaInicioFormulario = obj.FechaInicioFormulario;
                FiltroNuevo.FechaFinFormulario = obj.FechaFinFormulario;
                FiltroNuevo.FechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                FiltroNuevo.FechaFinChatIntegra = obj.FechaFinChatIntegra;
                FiltroNuevo.IdOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                FiltroNuevo.TiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                FiltroNuevo.IdOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                FiltroNuevo.NroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                FiltroNuevo.IdOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                FiltroNuevo.TiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                FiltroNuevo.IdOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                FiltroNuevo.NroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;

                FiltroNuevo.FechaInicioCorreo = obj.FechaInicioCorreo;
                FiltroNuevo.FechaFinCorreo = obj.FechaFinCorreo;
                FiltroNuevo.IdOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                FiltroNuevo.NroCorreosAbiertos = obj.NroCorreosAbiertos;
                FiltroNuevo.IdOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                FiltroNuevo.NroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                FiltroNuevo.IdOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                FiltroNuevo.NroClicksEnlace = obj.NroClicksEnlace;
                FiltroNuevo.EsSuscribirme = obj.EsSuscribirme;
                FiltroNuevo.EsDesuscribirme = obj.EsDesuscribirme;

                FiltroNuevo.IdOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                FiltroNuevo.NroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                FiltroNuevo.IdOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                FiltroNuevo.NroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                FiltroNuevo.IdOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                FiltroNuevo.NroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                FiltroNuevo.ConsiderarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                FiltroNuevo.ConsiderarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                FiltroNuevo.TieneVentaCruzada = obj.TieneVentaCruzada;
                FiltroNuevo.IdOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                FiltroNuevo.NroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                FiltroNuevo.IdOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                FiltroNuevo.MontoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                FiltroNuevo.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                FiltroNuevo.MontoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                FiltroNuevo.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                FiltroNuevo.MontoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                FiltroNuevo.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                FiltroNuevo.NroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                FiltroNuevo.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                FiltroNuevo.MontoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                FiltroNuevo.IdOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;

                FiltroNuevo.NroTcOtorgada = obj.NroTcOtorgada;
                FiltroNuevo.IdOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                FiltroNuevo.MontoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;
                FiltroNuevo.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                FiltroNuevo.MontoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                FiltroNuevo.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                FiltroNuevo.MontoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                FiltroNuevo.IdOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                FiltroNuevo.MontoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;
                FiltroNuevo.FechaInicioLlamada = obj.FechaInicioLlamada;
                FiltroNuevo.FechaFinLlamada = obj.FechaFinLlamada;
                FiltroNuevo.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                FiltroNuevo.DuracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                FiltroNuevo.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                FiltroNuevo.DuracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                FiltroNuevo.IdOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                FiltroNuevo.NroLlamada = obj.NroLlamada;
                FiltroNuevo.IdOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                FiltroNuevo.DuracionLlamada = obj.DuracionLlamada;
                FiltroNuevo.IdOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                FiltroNuevo.TasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                FiltroNuevo.FechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                FiltroNuevo.FechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                FiltroNuevo.TiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                FiltroNuevo.IdOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                FiltroNuevo.NroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                FiltroNuevo.TiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                FiltroNuevo.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                FiltroNuevo.IdOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                FiltroNuevo.NroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                FiltroNuevo.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                FiltroNuevo.ConsiderarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                FiltroNuevo.ConsiderarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;

                FiltroNuevo.TiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                FiltroNuevo.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                FiltroNuevo.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                FiltroNuevo.NroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                FiltroNuevo.TiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                FiltroNuevo.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                FiltroNuevo.NroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                FiltroNuevo.IdOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                FiltroNuevo.NroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                FiltroNuevo.TiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                FiltroNuevo.IdOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                FiltroNuevo.NroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                FiltroNuevo.IdOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                FiltroNuevo.NroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                FiltroNuevo.TiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                FiltroNuevo.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                FiltroNuevo.NroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                FiltroNuevo.ConsiderarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;

                FiltroNuevo.ConsiderarOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                FiltroNuevo.ConsiderarCategoriaDato = obj.ConsiderarCategoriaDato;
                FiltroNuevo.ConsiderarInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                FiltroNuevo.ConsiderarInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                FiltroNuevo.ConsiderarInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                FiltroNuevo.ConsiderarInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                FiltroNuevo.ConsiderarInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                FiltroNuevo.ConsiderarHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                FiltroNuevo.ConsiderarInteraccionWhatsApp = obj.ConsiderarInteraccionWhatsApp;
                FiltroNuevo.ConsiderarInteraccionChatMessenger = obj.ConsiderarInteraccionChatMessenger;
                FiltroNuevo.ConsiderarEnvioAutomatico = obj.ConsiderarEnvioAutomatico;

                FiltroNuevo.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                FiltroNuevo.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                FiltroNuevo.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                FiltroNuevo.IdTiempoFrecuenciaMatriculaAlumno = obj.IdTiempoFrecuenciaMatriculaAlumno;
                FiltroNuevo.CantidadTiempoMatriculaAlumno = obj.CantidadTiempoMatriculaAlumno;

                FiltroNuevo.ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                FiltroNuevo.ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                FiltroNuevo.ConsiderarConEmailValido = obj.ConsiderarConEmailValido;

                FiltroNuevo.IdTiempoFrecuenciaCumpleaniosContactoDentroDe = obj.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                FiltroNuevo.CantidadTiempoCumpleaniosContactoDentroDe = obj.CantidadTiempoCumpleaniosContactoDentroDe;
                FiltroNuevo.FechaInicioMatriculaAlumno = obj.FechaInicioMatriculaAlumno;
                FiltroNuevo.FechaFinMatriculaAlumno = obj.FechaFinMatriculaAlumno;
                FiltroNuevo.ConsiderarAlumnosAsignacionAutomaticaOperaciones = obj.ConsiderarAlumnosAsignacionAutomaticaOperaciones;
                FiltroNuevo.ExcluirMatriculados = obj.ExcluirMatriculados;

                FiltroNuevo.AplicaSobreCreacionOportunidad = obj.AplicaSobreCreacionOportunidad;
                FiltroNuevo.IdOperadorMedidaTiempoCreacionOportunidad = obj.IdOperadorMedidaTiempoCreacionOportunidad;
                FiltroNuevo.NroMedidaTiempoCreacionOportunidad = obj.NroMedidaTiempoCreacionOportunidad;
                FiltroNuevo.AplicaSobreUltimaActividad = obj.AplicaSobreUltimaActividad;
                FiltroNuevo.IdOperadorMedidaTiempoUltimaActividadEjecutada = obj.IdOperadorMedidaTiempoUltimaActividadEjecutada;
                FiltroNuevo.NroMedidaTiempoUltimaActividadEjecutada = obj.NroMedidaTiempoUltimaActividadEjecutada;
                FiltroNuevo.EnvioAutomaticoEstadoActividadDetalle = obj.EnvioAutomaticoEstadoActividadDetalle;
                FiltroNuevo.ConsiderarYaEnviados = 0; //valor que no se considera  

                FiltroNuevo.ConsiderarUltimaOportunidad = obj.ConsiderarUltimaOportunidad;

                FiltroNuevo.UsuarioCreacion = UsuarioModificacion;
                FiltroNuevo.UsuarioModificacion = UsuarioModificacion;
                FiltroNuevo.FechaCreacion = DateTime.Now;
                FiltroNuevo.FechaModificacion = DateTime.Now;
                FiltroNuevo.Estado = true;
                obj.Id = this.Add(FiltroNuevo).Id;
                LlenarHijoParaInsertar(obj);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void LlenarHijoParaInsertar(FiltroSegmentoDTO filtro)
        {
            try
            {
                foreach (var item in filtro.ListaArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaSubArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaProgramaGeneral)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaProgramaEspecifico)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseMaxima)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseMaxima)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaPais)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPais,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaCiudad)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCiudad,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaCargo)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCargo,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaIndustria)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroIndustria,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaAreaFormacion)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaAreaTrabajo)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTipoCategoriaOrigen)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaCategoriaOrigen)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTipoFormulario)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTipoInteraccionFormulario)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaProbabilidadOportunidad)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaActividadLlamada)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaVCArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id


                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaVCSubArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCSubArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaVCPGeneral)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }

                foreach (var item in filtro.ListaProbabilidadVentaCruzada)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                //excluir
                foreach (var item in filtro.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                //excluir por resultado filtro
                foreach (var item in filtro.ListaExcluirPorFiltroSegmento)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }

                //excluir por campanias maling
                foreach (var item in filtro.ListaExcluirPorCampaniaMailing)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }

                //excluir por conjunto lista
                foreach (var item in filtro.ListaExcluirPorConjuntoLista)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }

                foreach (var item in filtro.ListaActividadCabecera)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaOcurrencia)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOcurrencia,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaDocumentoAlumno)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoMatricula)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaSubEstadoMatricula)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaModalidadCurso)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }

                //Filtro segmento  detalle

                foreach (var item in filtro.ListaSesion)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesion,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoAcademico)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoPago)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoPago,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaPorcentajeAvance)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoLlamada)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }

                //nuevos operaciones
                foreach (var item in filtro.ListaSesionWebinar)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTrabajoAlumno)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTrabajoAlumnoFinal)
                {
                    var _new = new FiltroSegmentoDetalle
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoDetalleRepository.Add(_new);
                }
                foreach (var item in filtro.ListaTarifario)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTarifario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaEnvioAutomaticoOportunidadFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaUOArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUOArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id


                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaUOSubArea)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUOSubArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                foreach (var item in filtro.ListaUOPGeneral)
                {
                    var _new = new FiltroSegmentoValorTipo
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUOPGeneral,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdFiltroSegmento = filtro.Id
                    };
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Add(_new);
                }
                _unitOfWork.Commit();
                ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento)
        {

            try
            {
                List<FacebookAudienciaHistorialDTO> listaFacebookAudienciaHistorial = new List<FacebookAudienciaHistorialDTO>();
                listaFacebookAudienciaHistorial = _unitOfWork.FiltroSegmentoRepository.ObtenerHistorialPorIdFiltroSegmento(idFiltroSegmento);
                return listaFacebookAudienciaHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerTodoFiltroFiltroSegmento()
        {
            try
            {

                return _unitOfWork.FiltroSegmentoRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadosFiltro(int Id)
        {

            try
            {
                if (!_unitOfWork.FiltroSegmentoRepository.Exist(Id))
                {
                    throw new Exception("Filtro segmento no existente!");
                }
                var Filtro = _unitOfWork.FiltroSegmentoRepository.FirstById(Id);
                return this.ObtenerResultadoCompuesto(Id, Filtro.IdFiltroSegmentoTipoContacto.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool EliminarFiltro(int IdFiltroSegmento, String UsuarioModificacion)
        {
            try
            {

                if (_unitOfWork.FiltroSegmentoRepository.Exist(IdFiltroSegmento))
                {
                    _unitOfWork.FiltroSegmentoRepository.Delete(IdFiltroSegmento, UsuarioModificacion);
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Delete(_unitOfWork.FiltroSegmentoValorTipoRepository.GetBy(x => x.IdFiltroSegmento == IdFiltroSegmento).Select(x => x.Id), UsuarioModificacion);
                    _unitOfWork.FiltroSegmentoDetalleRepository.Delete(_unitOfWork.FiltroSegmentoDetalleRepository.GetBy(x => x.IdFiltroSegmento == IdFiltroSegmento).Select(x => x.Id), UsuarioModificacion);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadoCompuesto(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                return _unitOfWork.FiltroSegmentoRepository.ObtenerResultadoFiltro(id, idFiltroSegmentoTipoContacto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Duplicar(int IdFiltroSegmento, string UsuarioModificacion)
        {
            try
            {
                var filtroSegmento = new FiltroSegmento();
                filtroSegmento.Id = IdFiltroSegmento;

                var filtroDetalle = this.ObtenerDetalleFiltroSegmento(filtroSegmento.Id);
                filtroDetalle.Nombre = string.Concat(filtroDetalle.Nombre, " - COPIA");
                filtroDetalle.Descripcion = string.Concat(filtroDetalle.Descripcion, " - COPIA");
                filtroDetalle.NombreUsuario = UsuarioModificacion;
                filtroDetalle.Id = 0;
                return this.InsertarFiltro(filtroDetalle, UsuarioModificacion);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ActualizarFiltro(FiltroSegmentoDTO obj, string UsuarioModificacion)
        {
            try
            {

                if (_unitOfWork.FiltroSegmentoRepository.Exist(obj.Id))
                {

                    var FiltroNuevo = _unitOfWork.FiltroSegmentoRepository.FirstById(obj.Id);

                    FiltroNuevo.Id = obj.Id;
                    FiltroNuevo.Nombre = obj.Nombre;
                    FiltroNuevo.Descripcion = obj.Descripcion;
                    FiltroNuevo.IdFiltroSegmentoTipoContacto = obj.IdFiltroSegmentoTipoContacto;
                    FiltroNuevo.FechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                    FiltroNuevo.FechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;
                    FiltroNuevo.FechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                    FiltroNuevo.FechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;
                    FiltroNuevo.FechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2;
                    FiltroNuevo.FechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2;
                    FiltroNuevo.EsRn2 = obj.EsRn2;
                    FiltroNuevo.IdOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                    FiltroNuevo.NroOportunidades = obj.NroOportunidades;
                    FiltroNuevo.IdOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                    FiltroNuevo.NroSolicitudInformacion = obj.NroSolicitudInformacion;

                    //
                    FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                    FiltroNuevo.NroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                    FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                    FiltroNuevo.NroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                    FiltroNuevo.IdOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                    FiltroNuevo.NroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;
                    //

                    FiltroNuevo.FechaInicioFormulario = obj.FechaInicioFormulario;
                    FiltroNuevo.FechaFinFormulario = obj.FechaFinFormulario;
                    FiltroNuevo.FechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                    FiltroNuevo.FechaFinChatIntegra = obj.FechaFinChatIntegra;
                    FiltroNuevo.IdOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                    FiltroNuevo.TiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                    FiltroNuevo.IdOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                    FiltroNuevo.NroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                    FiltroNuevo.IdOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                    FiltroNuevo.TiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                    FiltroNuevo.IdOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                    FiltroNuevo.NroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;

                    FiltroNuevo.FechaInicioCorreo = obj.FechaInicioCorreo;
                    FiltroNuevo.FechaFinCorreo = obj.FechaFinCorreo;
                    FiltroNuevo.IdOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                    FiltroNuevo.NroCorreosAbiertos = obj.NroCorreosAbiertos;
                    FiltroNuevo.IdOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                    FiltroNuevo.NroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                    FiltroNuevo.IdOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                    FiltroNuevo.NroClicksEnlace = obj.NroClicksEnlace;
                    FiltroNuevo.EsSuscribirme = obj.EsSuscribirme;
                    FiltroNuevo.EsDesuscribirme = obj.EsDesuscribirme;

                    FiltroNuevo.IdOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                    FiltroNuevo.NroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                    FiltroNuevo.IdOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                    FiltroNuevo.NroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                    FiltroNuevo.IdOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                    FiltroNuevo.NroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                    FiltroNuevo.ConsiderarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                    FiltroNuevo.ConsiderarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                    FiltroNuevo.TieneVentaCruzada = obj.TieneVentaCruzada;
                    FiltroNuevo.IdOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                    FiltroNuevo.NroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                    FiltroNuevo.IdOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                    FiltroNuevo.MontoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                    FiltroNuevo.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                    FiltroNuevo.MontoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                    FiltroNuevo.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                    FiltroNuevo.MontoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                    FiltroNuevo.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                    FiltroNuevo.NroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                    FiltroNuevo.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                    FiltroNuevo.MontoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                    FiltroNuevo.IdOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;

                    FiltroNuevo.NroTcOtorgada = obj.NroTcOtorgada;
                    FiltroNuevo.IdOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                    FiltroNuevo.MontoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;
                    FiltroNuevo.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                    FiltroNuevo.MontoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                    FiltroNuevo.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                    FiltroNuevo.MontoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                    FiltroNuevo.IdOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                    FiltroNuevo.MontoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;
                    FiltroNuevo.FechaInicioLlamada = obj.FechaInicioLlamada;
                    FiltroNuevo.FechaFinLlamada = obj.FechaFinLlamada;
                    FiltroNuevo.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                    FiltroNuevo.DuracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                    FiltroNuevo.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                    FiltroNuevo.DuracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                    FiltroNuevo.IdOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                    FiltroNuevo.NroLlamada = obj.NroLlamada;
                    FiltroNuevo.IdOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                    FiltroNuevo.DuracionLlamada = obj.DuracionLlamada;
                    FiltroNuevo.IdOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                    FiltroNuevo.TasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                    FiltroNuevo.FechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                    FiltroNuevo.FechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                    FiltroNuevo.TiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                    FiltroNuevo.IdOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                    FiltroNuevo.NroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                    FiltroNuevo.TiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                    FiltroNuevo.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                    FiltroNuevo.IdOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                    FiltroNuevo.NroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                    FiltroNuevo.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                    FiltroNuevo.ConsiderarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                    FiltroNuevo.ConsiderarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;

                    FiltroNuevo.TiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                    FiltroNuevo.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                    FiltroNuevo.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                    FiltroNuevo.NroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                    FiltroNuevo.TiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                    FiltroNuevo.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                    FiltroNuevo.NroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                    FiltroNuevo.IdOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                    FiltroNuevo.NroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                    FiltroNuevo.TiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                    FiltroNuevo.IdOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                    FiltroNuevo.NroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                    FiltroNuevo.IdOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                    FiltroNuevo.NroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                    FiltroNuevo.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                    FiltroNuevo.TiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                    FiltroNuevo.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                    FiltroNuevo.NroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                    FiltroNuevo.ConsiderarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;

                    FiltroNuevo.ConsiderarOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                    FiltroNuevo.ConsiderarCategoriaDato = obj.ConsiderarCategoriaDato;
                    FiltroNuevo.ConsiderarInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                    FiltroNuevo.ConsiderarInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                    FiltroNuevo.ConsiderarInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                    FiltroNuevo.ConsiderarInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                    FiltroNuevo.ConsiderarInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                    FiltroNuevo.ConsiderarHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                    FiltroNuevo.ConsiderarInteraccionWhatsApp = obj.ConsiderarInteraccionWhatsApp;
                    FiltroNuevo.ConsiderarInteraccionChatMessenger = obj.ConsiderarInteraccionChatMessenger;
                    FiltroNuevo.ConsiderarEnvioAutomatico = obj.ConsiderarEnvioAutomatico;

                    FiltroNuevo.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                    FiltroNuevo.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                    FiltroNuevo.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                    FiltroNuevo.IdTiempoFrecuenciaMatriculaAlumno = obj.IdTiempoFrecuenciaMatriculaAlumno;
                    FiltroNuevo.CantidadTiempoMatriculaAlumno = obj.CantidadTiempoMatriculaAlumno;

                    FiltroNuevo.ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                    FiltroNuevo.ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                    FiltroNuevo.ConsiderarConEmailValido = obj.ConsiderarConEmailValido;

                    FiltroNuevo.IdTiempoFrecuenciaCumpleaniosContactoDentroDe = obj.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                    FiltroNuevo.CantidadTiempoCumpleaniosContactoDentroDe = obj.CantidadTiempoCumpleaniosContactoDentroDe;
                    FiltroNuevo.FechaInicioMatriculaAlumno = obj.FechaInicioMatriculaAlumno;
                    FiltroNuevo.FechaFinMatriculaAlumno = obj.FechaFinMatriculaAlumno;
                    FiltroNuevo.ConsiderarAlumnosAsignacionAutomaticaOperaciones = obj.ConsiderarAlumnosAsignacionAutomaticaOperaciones;
                    FiltroNuevo.ExcluirMatriculados = obj.ExcluirMatriculados;

                    FiltroNuevo.AplicaSobreCreacionOportunidad = obj.AplicaSobreCreacionOportunidad;
                    FiltroNuevo.IdOperadorMedidaTiempoCreacionOportunidad = obj.IdOperadorMedidaTiempoCreacionOportunidad;
                    FiltroNuevo.NroMedidaTiempoCreacionOportunidad = obj.NroMedidaTiempoCreacionOportunidad;
                    FiltroNuevo.AplicaSobreUltimaActividad = obj.AplicaSobreUltimaActividad;
                    FiltroNuevo.IdOperadorMedidaTiempoUltimaActividadEjecutada = obj.IdOperadorMedidaTiempoUltimaActividadEjecutada;
                    FiltroNuevo.NroMedidaTiempoUltimaActividadEjecutada = obj.NroMedidaTiempoUltimaActividadEjecutada;
                    FiltroNuevo.EnvioAutomaticoEstadoActividadDetalle = obj.EnvioAutomaticoEstadoActividadDetalle;
                    FiltroNuevo.ConsiderarYaEnviados = 0; //valor que no se considera  
                    FiltroNuevo.ConsiderarUltimaOportunidad = obj.ConsiderarUltimaOportunidad;

                    FiltroNuevo.UsuarioModificacion = UsuarioModificacion;
                    FiltroNuevo.FechaModificacion = DateTime.Now;
                    FiltroNuevo.Estado = true;

                    ActualizarFiltroEliminacionLogica(obj.Id, UsuarioModificacion);
                    LlenarHijoParaInsertar(obj);
                    _unitOfWork.FiltroSegmentoRepository.Update(FiltroNuevo);
                    _unitOfWork.Commit();

                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public void LlenarHijoParaActualizar(FiltroSegmentoDTO filtro, string UsuarioModificacion)
        {
            try
            {
                this.ActualizarFiltroEliminacionLogica(filtro.Id, UsuarioModificacion);
                this.LlenarHijoParaInsertar(filtro);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public bool ActualizarFiltroEliminacionLogica(int IdFiltroSegmento, String UsuarioModificacion)
        {
            try
            {

                if (_unitOfWork.FiltroSegmentoRepository.Exist(IdFiltroSegmento))
                {
                    _unitOfWork.FiltroSegmentoValorTipoRepository.Delete(_unitOfWork.FiltroSegmentoValorTipoRepository.GetBy(x => x.IdFiltroSegmento == IdFiltroSegmento).Select(x => x.Id), UsuarioModificacion);
                    _unitOfWork.FiltroSegmentoDetalleRepository.Delete(_unitOfWork.FiltroSegmentoDetalleRepository.GetBy(x => x.IdFiltroSegmento == IdFiltroSegmento).Select(x => x.Id), UsuarioModificacion);
                    _unitOfWork.FiltroSegmentoRepository.EliminarPorFiltroSegmento(IdFiltroSegmento, UsuarioModificacion);
                    _unitOfWork.Commit();


                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarAudiencia(FacebookAudienciaDTO Json, string UsuarioCreacion)
        {
            try
            {

                string rpta;
                bool ValidarRespuestaAPI = false;
                rpta = FacebookNewAudiencie(Json);
                string Atributos = "audience_id";
                ValidarRespuestaAPI = rpta.Contains(Atributos);
                Atributos = "session_id";
                ValidarRespuestaAPI = rpta.Contains(Atributos);
                Atributos = "num_received";
                ValidarRespuestaAPI = rpta.Contains(Atributos);
                if (ValidarRespuestaAPI)
                {
                    try
                    {
                        var RespuestaSubirAudiencia = JsonConvert.DeserializeObject<RespuestaFacebookDTO>(rpta);
                        TFacebookAudiencium facebookAudienciaBO = new TFacebookAudiencium();
                        facebookAudienciaBO.IdFiltroSegmento = Json.IdFiltroSegmento;
                        facebookAudienciaBO.FacebookIdAudiencia = RespuestaSubirAudiencia.audience_id;
                        facebookAudienciaBO.Nombre = Json.Nombre;
                        facebookAudienciaBO.Descripcion = Json.Descripcion;
                        facebookAudienciaBO.Subtipo = "CUSTOM";
                        facebookAudienciaBO.RecursoArchivoCliente = "USER_PROVIDED_ONLY";
                        facebookAudienciaBO.Estado = true;
                        facebookAudienciaBO.FechaCreacion = DateTime.Now;
                        facebookAudienciaBO.FechaModificacion = DateTime.Now;
                        facebookAudienciaBO.UsuarioCreacion = Json.Usuario;
                        facebookAudienciaBO.UsuarioModificacion = Json.Usuario;

                     _unitOfWork.FacebookAudienciumRepository.Insert(facebookAudienciaBO);
                        _unitOfWork.Commit();
                        var idRecuperado = facebookAudienciaBO.Id;
                        Console.WriteLine("ID de FacebookAudiencia: " + facebookAudienciaBO.Id);

                        

                        var CuentaPublicitaria = _unitOfWork.FacebookCuentaPublicitariaRepository.FirstBy(x => x.FacebookIdCuentaPublicitaria == Json.Cuenta);
                        FacebookAudienciaCuentaPublicitarium facebookAudienciaCuentaPublicitariaBO = new FacebookAudienciaCuentaPublicitarium();
                        facebookAudienciaCuentaPublicitariaBO.IdFacebookAudiencia = facebookAudienciaBO.Id;
                        facebookAudienciaCuentaPublicitariaBO.IdFacebookCuentaPublicitaria = CuentaPublicitaria != null ? CuentaPublicitaria.Id : 0;
                        facebookAudienciaCuentaPublicitariaBO.Subtipo = "CUSTOM";
                        facebookAudienciaCuentaPublicitariaBO.Origen = "Propio";
                        facebookAudienciaCuentaPublicitariaBO.Estado = true;
                        facebookAudienciaCuentaPublicitariaBO.FechaCreacion = DateTime.Now;
                        facebookAudienciaCuentaPublicitariaBO.FechaModificacion = DateTime.Now;
                        facebookAudienciaCuentaPublicitariaBO.UsuarioCreacion = Json.Usuario;
                        facebookAudienciaCuentaPublicitariaBO.UsuarioModificacion = Json.Usuario;


                      _unitOfWork.FacebookAudienciaCuentaPublicitariumRepository.Add(facebookAudienciaCuentaPublicitariaBO); 
                        _unitOfWork.Commit();


                        //Activar para Insertar en tabla cuando sea necesario
                        //var listaOportunidadActualFaseActual = string.Join(",", Json.Alumnos.Select(x => x.IdAlumno));
                        //facebookAudienciaRepositorio.InsertarFacebookAudienciaAlumno(listaOportunidadActualFaseActual, facebookAudienciaBO.Id, Json.Usuario);
                        List<AddresseeDTO> correosPersonalizados = new List<AddresseeDTO>();
                        AddresseeDTO UsuarioEjecucion = new AddresseeDTO();
                        UsuarioEjecucion.Email = UsuarioCreacion + "@bsginstitute.com";
                        correosPersonalizados.Add(UsuarioEjecucion);
                        correosPersonalizados = _unitOfWork.FiltroSegmentoRepository.ObtenerAddressee();
                        string Mensaje = "Audiencia creada con exito <br/> Audiencia: " + Json.Nombre + "<br/>" + "Descripcion: " + Json.Descripcion + "<br/>" + "Cantidad de contactos subidos: " + RespuestaSubirAudiencia.num_received;
                        EnvioCorreo("New audience - FACEBOOK", " Audiencia creada: " + Json.Nombre, Mensaje, correosPersonalizados);
                        return true;

                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                }
                else throw new Exception("Error inesperado en la respuesta de facebook");

            }
            catch (Exception e)
            {
                return false;
            }
        }


        public string FacebookNewAudiencie(FacebookAudienciaDTO Json)
        {

            try
            {
                //+ Json.FacebookIdAudiencia + "/" + "users"
                string urlCrearAudiencia = urlApiGraphV15 + Json.Cuenta.ToString() + "/" + "customaudiences?access_token=" + accessToken + "&name=" + Json.Nombre + "&subtype=CUSTOM&description=" + Json.Descripcion + "&customer_file_source=USER_PROVIDED_ONLY";
                string rptaCrearAudiencia = "";
                string rptaSubirAudiencia = "";
                bool exist = false;
                using (WebClient wc = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(Json);
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    try
                    {
                        rptaCrearAudiencia = wc.UploadString(new Uri(urlCrearAudiencia), "POST");
                        string sub = "id";
                        exist = rptaCrearAudiencia.Contains(sub);
                    }
                    catch (Exception ex)
                    {
                        return rptaCrearAudiencia;
                    }
                }
                if (rptaCrearAudiencia != "" && exist)
                {
                    IdCustomAudienceDTO CustomAudience = JsonConvert.DeserializeObject<IdCustomAudienceDTO>(rptaCrearAudiencia);
                    string urlSubirAudiencia = urlApiGraphV15 + CustomAudience.Id.ToString() + "/users?&access_token=" + accessToken;
                    string JsonSubirAudiencia = "{\"payload\": {\"schema\": \"EMAIL_SHA256\",\"data\": [";
                    string ListaCorreosEncriptados = "";
                    foreach (var item in Json.Alumnos)
                    {
                        string Encriptar = "";
                        Encriptar = ComputeSHA256(item.Email1);
                        Encriptar = "\"" + Encriptar + "\",";
                        ListaCorreosEncriptados = ListaCorreosEncriptados + Encriptar;
                    }
                    JsonSubirAudiencia = JsonSubirAudiencia + ListaCorreosEncriptados + "]}}";
                    var JsonSubirAudienciaSerializado = JsonConvert.SerializeObject(JsonSubirAudiencia);

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                        try
                        {
                            rptaSubirAudiencia = wc.UploadString(new Uri(urlSubirAudiencia), "POST", JsonSubirAudiencia);
                            return rptaSubirAudiencia;
                        }
                        catch (Exception ex)
                        {
                            return rptaSubirAudiencia;
                        }
                    }
                }
                return rptaSubirAudiencia;
            }
            catch (Exception e)
            {
                return "No se procesó";
            }
        }

        public bool ActualizarAudiencia(FacebookAudienciaDTO Json)
        {
            if (string.IsNullOrEmpty(Json.FacebookIdAudiencia))
            {
                return false;
            }

            string urlActualizarAudiencia = urlApiGraphV15 + Json.FacebookIdAudiencia + "?access_token=" + accessToken;
            string rptaActualizarAudiencia = "";
            bool updateSuccess = false;

            using (WebClient wc = new WebClient())
            {
                var dataActualizar = JsonConvert.SerializeObject(new
                {
                    name = Json.Nombre ?? "Nombre Actualizado",
                    description = Json.Descripcion ?? "Descripción actualizada de la audiencia."
                });

                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                try
                {
                    rptaActualizarAudiencia = wc.UploadString(new Uri(urlActualizarAudiencia), "POST", dataActualizar);
                    updateSuccess = rptaActualizarAudiencia.Contains("success");
                }
                catch (WebException ex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            if (updateSuccess && Json.Alumnos != null && Json.Alumnos.Count > 0)
            {
                string urlSubirAudiencia = urlApiGraphV15 + Json.FacebookIdAudiencia + "/users?&access_token=" + accessToken;
                string JsonSubirAudiencia = "{\"payload\": {\"schema\": \"EMAIL_SHA256\",\"data\": [";
                string ListaCorreosEncriptados = "";

                foreach (var item in Json.Alumnos)
                {
                    string Encriptar = ComputeSHA256(item.Email1);
                    ListaCorreosEncriptados += "\"" + Encriptar + "\",";
                }
                JsonSubirAudiencia += ListaCorreosEncriptados.TrimEnd(',') + "]}}";

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    try
                    {
                        string rptaSubirAudiencia = wc.UploadString(new Uri(urlSubirAudiencia), "POST", JsonSubirAudiencia);
                        if (!rptaSubirAudiencia.Contains("num_received"))
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            if (!updateSuccess)
            {
                return false;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var facebookAudienciaBO = _unitOfWork.FacebookAudienciumRepository.FirstBy(x => x.FacebookIdAudiencia == Json.FacebookIdAudiencia);
                    if (facebookAudienciaBO == null)
                    {
                        return false;
                    }

                    facebookAudienciaBO.FechaModificacion = DateTime.Now;
                    facebookAudienciaBO.UsuarioModificacion = Json.Usuario;
                    _unitOfWork.FacebookAudienciumRepository.Update(facebookAudienciaBO);
                    _unitOfWork.Commit();



                    foreach (var objeto in Json.Alumnos)
                    {
                        var facebookAudienciaAlumnoBO = _unitOfWork.FacebookAudienciaAlumnoRepository.FirstBy(x => x.IdFacebookAudiencia == facebookAudienciaBO.Id && x.IdAlumno == objeto.IdAlumno);
                        if (facebookAudienciaAlumnoBO == null)
                        {
                            facebookAudienciaAlumnoBO = new TFacebookAudienciaAlumno
                            {
                                IdFacebookAudiencia = facebookAudienciaBO.Id,
                                IdAlumno = objeto.IdAlumno,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Json.Usuario,
                                UsuarioModificacion = Json.Usuario
                            };
                            _unitOfWork.FacebookAudienciaAlumnoRepository.Insert(facebookAudienciaAlumnoBO);
                            _unitOfWork.Commit();


                        }
                        else
                        {
                            facebookAudienciaAlumnoBO.FechaModificacion = DateTime.Now;
                            facebookAudienciaAlumnoBO.UsuarioModificacion = Json.Usuario;
                            _unitOfWork.FacebookAudienciaAlumnoRepository.Update(facebookAudienciaAlumnoBO);
                            _unitOfWork.Commit();

                        }
                    }

                    _unitOfWork.Commit();
                    scope.Complete();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        static string ComputeSHA256(string s)
        {
            string hash = String.Empty;
            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }
            return hash.ToLower();
        }


        public RespuestaOportunidadFiltroSegmentoDTO   CrearOportunidadPorFiltroSegmento(OportunidadFiltroSegmentoDTO OportunidadFiltroSegmento)
        {
            try
            {
                var servicioOportunidad = new OportunidadService(_unitOfWork);
                var _repLogFiltroSegmentoEjecutado = _unitOfWork.LogFiltroSegmentoEjecutadoRepository;

                var _repAlumno = _unitOfWork.FiltroSegmentoRepository;


                var listadoErrores = new ListError();

                int cantidadOportunidadesCreadas = 0;

                foreach (var id in OportunidadFiltroSegmento.ListadoIdsAlumnos)
                {
                    //var Alumno = _repAlumno.ObtenerAlumnoPorId(id);
                    var TAlumno = _unitOfWork.AlumnoRepository.FirstById(id);

                    var alumno = _mapper.Map<Alumno>(TAlumno);

                    if (alumno != null)
                    {
                        try
                        {
                            var oportunidad = new OportunidadBoDTO()
                            {
                                IdCentroCosto = OportunidadFiltroSegmento.IdCentroCosto,
                                IdTipoDato = OportunidadFiltroSegmento.IdTipoDato,
                                IdOrigen = OportunidadFiltroSegmento.IdOrigen,
                                IdFaseOportunidad = OportunidadFiltroSegmento.IdFaseOportunidad,
                                IdPersonalAsignado = ValorEstatico.IdPersonalAsesorAsignacionHistorico,
                                UltimoComentario = "Segmento Historico",
                                UltimaFechaProgramada = null,
                                IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                                FechaRegistroCampania = DateTime.Now,
                                IdAlumno = id,
                                Estado = true,
                                UsuarioCreacion = OportunidadFiltroSegmento.NombreUsuario,
                                UsuarioModificacion = OportunidadFiltroSegmento.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Alumno = alumno,


                            };
                            // oportunidad.Alumno = _repAlumno.ObtenerAlumnoPorId(id);

                            //oportunidad.Alumno = _unitOfWork.AlumnoRepository.FirstById(id);

                            servicioOportunidad.CrearOportunidadMarketing(ref oportunidad);

                            int nroIntentos = 0;
                            bool flagValidado = false;

                            while (!flagValidado && nroIntentos < 10)
                            {
                                try
                                {
                                    servicioOportunidad.ValidarCasosOportunidad(oportunidad.Id, 0, false);

                                    flagValidado = true;
                                }
                                catch (Exception ex)
                                {
                                    nroIntentos++;

                                    Thread.Sleep(3000);
                                }
                            }

                            if (nroIntentos == 10)
                            {
                                var _repLog = _unitOfWork.LogRepository;
                                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadPorFiltroSegmento", Parametros = $"IdOportunidad={oportunidad.Id}&IdAsignacionAutomatica=0", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                            }

                            if (oportunidad.Id != null && oportunidad.Id != 0)
                            {
                                cantidadOportunidadesCreadas++;
                            }
                        }
                        catch (Exception e)
                        {
                            listadoErrores.AgregarError(new Error(id, "ERROR", $"Ocurrio un error con el alumno con id: {id} - {e.Message}"));

                        }
                    }
                    else
                    {
                        listadoErrores.AgregarError(new Error(id, "ERROR", $"El alumno con id: {id} no existe"));

                    }
                }


                //INSERTAMOS EN LOG FILTRO SEGMENTO

                //var oportunidad con id erroneos


                var filtro = new LogFiltroSegmentoEjecutadoService(_unitOfWork);
                var listadoErroresAlumno = listadoErrores.ObtenerErrores().DistinctBy(x => x.Id).Select(x => x.Id);

                var logFiltroSegmentoEjecutado = new TLogFiltroSegmentoEjecutado()
                {
                    IdCentroCosto = OportunidadFiltroSegmento.IdCentroCosto,
                    IdTipoDato = OportunidadFiltroSegmento.IdTipoDato,
                    IdOrigen = OportunidadFiltroSegmento.IdOrigen,
                    IdFaseOportunidad = OportunidadFiltroSegmento.IdFaseOportunidad,
                    IdFiltroSegmento = OportunidadFiltroSegmento.IdFiltroSegmento,
                    //TotalOportunidadesCreadas = OportunidadFiltroSegmento.ListadoIdsAlumnos.Count(),
                    TotalOportunidadesCreadas = OportunidadFiltroSegmento.ListadoIdsAlumnos.Where(w => !listadoErroresAlumno.Any(x => w == x)).Count(),
                    Estado = true,
                    UsuarioCreacion = OportunidadFiltroSegmento.NombreUsuario,
                    UsuarioModificacion = OportunidadFiltroSegmento.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };


                if (listadoErrores.ObtenerErrores().Count == 0)
                {
                    var entidad = _mapper.Map<LogFiltroSegmentoEjecutado>(logFiltroSegmentoEjecutado);
                    _unitOfWork.LogFiltroSegmentoEjecutadoRepository.Add(entidad);
                    _unitOfWork.Commit();

                }
                else
                {
                    listadoErrores.AgregarError(new Error(200, "ERROR", $"Ocurrion un error con el log con id FiltroSegmento {OportunidadFiltroSegmento.IdFiltroSegmento} "));
                }

                if (listadoErrores.TieneErrores)
                {

                    var resultado = (listadoErrores.ObtenerErrores());
                    throw new Exception("Error en la creación de oportunidades: " + string.Join(", ", resultado.Select(e => e.Descripcion)));



                    // return (listadoErrores.ObtenerErrores());
                }
                //if (cantidadOportunidadesCreadas == 0)
                //{
                //    return new RespuestaOportunidadFiltroSegmentoDTO
                //    {
                //        CantidadOportunidadesCreadas = 0,
                //        Errores = listadoErrores.ObtenerErrores()
                //    };
                //}
                //return new RespuestaOportunidadFiltroSegmentoDTO
                //{
                //    CantidadOportunidadesCreadas = cantidadOportunidadesCreadas,
                //    Errores = listadoErrores.ObtenerErrores()
                //};
                return new RespuestaOportunidadFiltroSegmentoDTO
                {
                    CantidadOportunidadesCreadas = cantidadOportunidadesCreadas,
                    Errores = listadoErrores.ObtenerErrores()
                };
            }
            catch (Exception e)
            {
                return new RespuestaOportunidadFiltroSegmentoDTO
                {
                    CantidadOportunidadesCreadas = 0,
                    Errores = new List<Error> { new Error(500, "ERROR", $"Ocurrió un error en la operación: {e.Message}") }
                };
           
        }
          

        }
        

    }
}
