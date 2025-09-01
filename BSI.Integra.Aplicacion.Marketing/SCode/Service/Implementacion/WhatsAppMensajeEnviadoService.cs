using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.Mvc;
using Twilio.TwiML.Voice;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppMensajeEnviadoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_WhatsAppMensajeEnviado
    /// </summary>
    public class WhatsAppMensajeEnviadoService : IWhatsAppMensajeEnviadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppMensajeEnviadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviado>(MemberList.None).ReverseMap());

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public WhatsAppMensajeEnviado Add(WhatsAppMensajeEnviado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeEnviadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajeEnviado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppMensajeEnviado Update(WhatsAppMensajeEnviado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeEnviadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajeEnviado>(modelo);
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
                _unitOfWork.WhatsAppMensajeEnviadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppMensajeEnviado> Add(List<WhatsAppMensajeEnviado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeEnviadoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajeEnviado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppMensajeEnviado> Update(List<WhatsAppMensajeEnviado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajeEnviadoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajeEnviado>>(modelo);
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
                _unitOfWork.WhatsAppMensajeEnviadoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppMensajeEnviado
        /// </summary>
        /// <returns> List<WhatsAppMensajeEnviadoDTO> </returns>
        public IEnumerable<WhatsAppMensajeEnviadoDTO> ObtenerWhatsAppMensajeEnviado()
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerWhatsAppMensajeEnviado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppMensajeEnviado para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppMensajeEnviadoComboDTO> </returns>
        public IEnumerable<WhatsAppMensajeEnviadoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerCombo();
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
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.HistorialChatsRecibido(idPersonal, numero, area);
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
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaHistorialMensajeChat(idPersonal, numero, area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatAtc(int idPersonal, string numero, string area, string Idpais)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaHistorialMensajeChatAtc(idPersonal, numero, area, Idpais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaUltimoMensajeChatsRecibido(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 12/09/2022
        /// <summary>
        /// Obtiene Lista de último mensaje enviado por IdPersonal
        /// </summary>
        /// <param name="idPersonal">Id de personal</param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaUltimoMensajeChatsEnviado(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///Autor: Jonathan Caipo
        ///Fecha: 13/03/2021
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppHistorialMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaHistorialMensajeChatControlMensaje(idPersonal, numero, area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadas(string plantilla, string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarPlantillasEnviadas(plantilla, numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarPlantillasEnviadasNuevoWebHook(plantilla, numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidarMesajesEnviadosEn24Horas(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarMesajesEnviadosEn24Horas(numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarMesajesEnviadosEn24HorasNuevoWebHook(numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidarMesajesEnviadosEn24HorasComercial(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado)
        {
            try
            {
                WhatsAppConfiguracionApiNumeroIdentificadorDto numeroIdentificadorWhatsAPP = new WhatsAppConfiguracionApiNumeroIdentificadorDto();
                var TipoPersonal = _unitOfWork.PersonalRepository.ObtenerPorId(IdPersonal);
                switch (TipoPersonal.TipoPersonal)
                {
                    case "Coordinador":
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(idPersonalAsignado, idCodigoPais);
                        break;
                    case "Asesor":
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(IdPersonal, idCodigoPais);
                        break;
                    default:
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(idPersonalAsignado, idCodigoPais);
                        break;
                }
                if (numeroIdentificadorWhatsAPP == null)
                {
                    return true;
                }
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ValidarMesajesEnviadosEn24HorasComercial(numero, numeroIdentificadorWhatsAPP.NumeroIndentificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MensajeChatDTO ValidarUltimoMensajeRecibido(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado)
        {
            try
            {
                WhatsAppConfiguracionApiNumeroIdentificadorDto numeroIdentificadorWhatsAPP = new WhatsAppConfiguracionApiNumeroIdentificadorDto();
                var TipoPersonal = _unitOfWork.PersonalRepository.ObtenerPorId(IdPersonal);
                switch (TipoPersonal.TipoPersonal)
                {
                    case "Coordinador":
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(idPersonalAsignado, idCodigoPais);
                        break;
                    case "Asesor":
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(IdPersonal, idCodigoPais);
                        break;
                    default:
                        numeroIdentificadorWhatsAPP = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerNumeroIdentificadorWhatsAppPorID(idPersonalAsignado, idCodigoPais);
                        break;
                }
                if (numeroIdentificadorWhatsAPP == null)
                {
                    return null;
                }
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.UltimoMensajeRecibido(numero, numeroIdentificadorWhatsAPP.NumeroIndentificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ObtenerRespuestaValidarNumeroLibreCompleto(string numero, int idPais, int idCentroCosto, int idPersonal)
        {
            try
            {
                OportunidadService servicioOportunidad = new OportunidadService(_unitOfWork);
                PGeneralService servicioPGeneral = new PGeneralService(_unitOfWork);
                string celular = "";
                if (idPais == 51)
                {
                    celular = numero.Substring(2, 9);
                }
                else if (idPais == 57)
                {
                    celular = "00" + numero;
                }
                else if (idPais == 591)
                {
                    celular = "00" + numero;
                }
                else
                {
                    celular = "00" + numero;
                }
                var programaGeneral = servicioPGeneral.ObtenerPGeneralPEspecificoPorCentroCosto(idCentroCosto);
                var oportunidad = servicioOportunidad.ValidarOportunidadesWhatsApp(celular, programaGeneral.IdProgramaGeneral);
                foreach (var item in oportunidad)
                {
                    if ((item.FaseOportunidad.ToUpper() == "IS" || item.FaseOportunidad.ToUpper() == "M") && (item.IdPgeneral == programaGeneral.IdProgramaGeneral) && item.IdPersonal != idPersonal)
                    {
                        return false;
                    }
                }
                foreach (var item in oportunidad)
                {
                    if (item.IdPersonal != idPersonal)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene Lista de último mensaje de chat por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChats(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaUltimoMensajeChats(idPersonal);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene historial de chat por idpersonal, numero, area y udTipoAgenda
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Numero de celular </param>
        /// <param name="area"> Area del asesor </param>
        /// <param name="idTipoAgenda"> Id del tipo de Agenda </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area, int idTipoAgenda)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.HistorialChatsRecibido(idPersonal, numero, area, idTipoAgenda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene mensaje multimedia de WhatsApp
        /// </summary>
        /// <param name="waId"> Id de chat WhatsApp </param>
        /// <returns> String </returns>
        public string ObtenerMensajeMultimedia(string waId)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerMensajeMultimedia(waId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerConversacionNumero(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerConversacionNumero(numero);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor con menos chats offline.
        /// </summary>
        /// <returns> Objeto PersonalNumeroMinimoChatDTO </returns>
        public PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChat()
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerAsesorConMenorChat();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 15/03/2023
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal ordenado por Fecha Modificación para Control de Mensajes Ofensivos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibidoControlMensaje(int idPersonal)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaUltimoMensajeChatsRecibidoControlMensaje(idPersonal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<InfoApiWhatsappDTO> ListaInformacionApiWhatsapp(int idPais)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaInformacionApiWhatsapp(idPais);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool EsAsesorVentasValido(int idAsesor)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.EsAsesorVentasValido(idAsesor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
     
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketing(int tab, int Dia)
        {
            try
            {
                List<ChatWhatsAppMarketingDTO> ChatWhatsAppMarketing = new List<ChatWhatsAppMarketingDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketing(tab, Dia);
                return ChatWhatsAppMarketing;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketingV2(FiltroChatWhatsappDTO filtro)
        {
            try
            {
                filtro.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                filtro.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                var resultado = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingv2(filtro.Tab, filtro.FechaInicio, filtro.FechaFin);
                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var dato in resultado)
                {
                   
                    if (!string.IsNullOrWhiteSpace(dato.Celular))
                        dato.CelularEncriptado = alumnoService.EncriptarNumeroHash(Regex.Replace(dato.Celular, @"[^\d]", ""));
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/23/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chat asignados a los asesores de marketing
        /// </summary>
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia, int IdAsesor)
        {
            try
            {
                List<ChatWhatsAppMarketingDTO> ChatWhatsAppMarketing = new List<ChatWhatsAppMarketingDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppFacebookMarketing(Tab, Dia, IdAsesor);

                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var item in ChatWhatsAppMarketing)
                {
                    if (!string.IsNullOrWhiteSpace(item.Celular))
                        item.CelularEncriptado = alumnoService.EncriptarCorreoHash(item.Celular);
                }

                return ChatWhatsAppMarketing;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingPorCelular(Celular);
                List<ChatWhatsAppMarketingPorCelularDTO> resultadoAgrupado = new List<ChatWhatsAppMarketingPorCelularDTO>();

                resultadoAgrupado = ChatWhatsAppMarketing.GroupBy(x => new { x.CelularUM, x.IdAlumnoUM, x.EmailUM, x.IdPaisEmpresa }).Select(x => new ChatWhatsAppMarketingPorCelularDTO
                {
                    CelularUM = x.Key.CelularUM,
                    IdAlumnoUM = x.Key.IdAlumnoUM,
                    EmailUM = x.Key.EmailUM,
                    IdPaisEmpresa = x.Key.IdPaisEmpresa,
                    ListaAlumnosPorCelular = x.GroupBy(y => new { y.IdAlumno, y.Email, y.FechaCreacion }).Select(y => new ObtenerChatWhatsAppMarketingAlumnoDTO
                    {
                        IdAlumno = y.Key.IdAlumno,
                        Email = y.Key.Email,
                        FechaCreacion = y.Key.FechaCreacion,
                    }).ToList(),


                    MensajePorCelular = x.GroupBy(y => new { y.Estatus, y.Tipo, y.IdAlumnoCelular, y.Celular, y.Alumno, y.Mensaje, y.Personal, y.FechaMensaje }).Select(y => new ObtenerChatWhatsAppMarketingMensajeDTO
                    {
                        Estatus = y.Key.Estatus,
                        Tipo = y.Key.Tipo,
                        IdAlumnoCelular = y.Key.IdAlumnoCelular,
                        Celular = y.Key.Celular,
                        Alumno = y.Key.Alumno,
                        Mensaje = y.Key.Mensaje,
                        Personal = y.Key.Personal,
                        FechaMensaje = y.Key.FechaMensaje,

                    }).ToList(),
                }).ToList();
                if (resultadoAgrupado.Count() == 0)
                {
                    throw new Exception("No se encontraron Datos");
                }

                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var item in resultadoAgrupado)
                {
                    if (!string.IsNullOrWhiteSpace(item.EmailUM))
                        item.EmailUMEncriptado = alumnoService.EncriptarCorreoHash(item.EmailUM);

                    if (!string.IsNullOrWhiteSpace(item.CelularUM))
                        item.CelularUMEncriptado = alumnoService.EncriptarNumeroHash(Regex.Replace(item.CelularUM, @"[^\d]", ""));

                    var result = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerRangoProbabilidadAlumno(item.IdAlumnoUM.Value);

                    var parsed = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    item.Rango = parsed != null && parsed.ContainsKey("Rango") ? parsed["Rango"] : string.Empty;
                }
                return resultadoAgrupado;
            }
            catch (Exception e)
            {

                throw new Exception("No se encontraron Datos");
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingMasivoPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingMasivoPorCelular(Celular);
                List<ChatWhatsAppMarketingPorCelularDTO> resultadoAgrupado = new List<ChatWhatsAppMarketingPorCelularDTO>();

                resultadoAgrupado = ChatWhatsAppMarketing.GroupBy(x => new { x.CelularUM, x.IdAlumnoUM, x.EmailUM, x.IdPaisEmpresa }).Select(x => new ChatWhatsAppMarketingPorCelularDTO
                {
                    CelularUM = x.Key.CelularUM,
                    IdAlumnoUM = x.Key.IdAlumnoUM,
                    EmailUM = x.Key.EmailUM,
                    IdPaisEmpresa = x.Key.IdPaisEmpresa,
                    ListaAlumnosPorCelular = x.GroupBy(y => new { y.IdAlumno, y.Email, y.FechaCreacion }).Select(y => new ObtenerChatWhatsAppMarketingAlumnoDTO
                    {
                        IdAlumno = y.Key.IdAlumno,
                        Email = y.Key.Email,
                        FechaCreacion = y.Key.FechaCreacion,
                    }).ToList(),


                    MensajePorCelular = x.GroupBy(y => new { y.Estatus, y.Tipo, y.IdAlumnoCelular, y.Celular, y.Alumno, y.Mensaje, y.Personal, y.FechaMensaje }).Select(y => new ObtenerChatWhatsAppMarketingMensajeDTO
                    {
                        Estatus = y.Key.Estatus,
                        Tipo = y.Key.Tipo,
                        IdAlumnoCelular = y.Key.IdAlumnoCelular,
                        Celular = y.Key.Celular,
                        Alumno = y.Key.Alumno,
                        Mensaje = y.Key.Mensaje,
                        Personal = y.Key.Personal,
                        FechaMensaje = y.Key.FechaMensaje,

                    }).ToList(),
                }).ToList();
                if (resultadoAgrupado.Count() == 0)
                {
                    throw new Exception("No se encontraron Datos");
                }

                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var item in resultadoAgrupado)
                {
                    if (!string.IsNullOrWhiteSpace(item.EmailUM))
                        item.EmailUMEncriptado = alumnoService.EncriptarCorreoHash(item.EmailUM);

                    if (!string.IsNullOrWhiteSpace(item.CelularUM))
                        item.CelularUMEncriptado = alumnoService.EncriptarNumeroHash(Regex.Replace(item.CelularUM, @"[^\d]", ""));
                }
                return resultadoAgrupado;
            }
            catch (Exception e)
            {

                throw new Exception("No se encontraron Datos");
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingBusquedaPorCelular(string Celular)
        {
            try
            {
                List<ObtenerChatWhatsAppMarketingPorCelularDTO> ChatWhatsAppMarketing = new List<ObtenerChatWhatsAppMarketingPorCelularDTO>();
                ChatWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingBusquedaPorCelular(Celular);
                List<ChatWhatsAppMarketingPorCelularDTO> resultadoAgrupado = new List<ChatWhatsAppMarketingPorCelularDTO>();

                resultadoAgrupado = ChatWhatsAppMarketing.GroupBy(x => new { x.CelularUM, x.IdAlumnoUM, x.EmailUM, x.IdPaisEmpresa }).Select(x => new ChatWhatsAppMarketingPorCelularDTO
                {
                    CelularUM = x.Key.CelularUM,
                    IdAlumnoUM = x.Key.IdAlumnoUM,
                    IdPaisEmpresa = x.Key.IdPaisEmpresa,
                    EmailUM = x.Key.EmailUM,

                    ListaAlumnosPorCelular = x.GroupBy(y => new { y.IdAlumno, y.Email, y.FechaCreacion }).Select(y => new ObtenerChatWhatsAppMarketingAlumnoDTO
                    {
                        IdAlumno = y.Key.IdAlumno,
                        Email = y.Key.Email,
                        FechaCreacion = y.Key.FechaCreacion,

                    }).ToList(),


                    MensajePorCelular = x.GroupBy(y => new { y.Estatus, y.Tipo, y.IdAlumnoCelular, y.Celular, y.Alumno, y.Mensaje, y.Personal, y.FechaMensaje }).Select(y => new ObtenerChatWhatsAppMarketingMensajeDTO
                    {
                        Estatus = y.Key.Estatus,
                        Tipo = y.Key.Tipo,
                        IdAlumnoCelular = y.Key.IdAlumnoCelular,
                        Celular = y.Key.Celular,
                        Alumno = y.Key.Alumno,
                        Mensaje = y.Key.Mensaje,
                        Personal = y.Key.Personal,
                        FechaMensaje = y.Key.FechaMensaje,

                    }).ToList(),
                }).ToList();
                return resultadoAgrupado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool ArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.ArchivarChat(Celular, IdAlumno, IdPersonal, UsuarioModificacion);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool ArchivarChatMasivo(List<WhatsAppChatArchivadoDTO> lista, int idPersonal, string usuario)
        {
            try
            {
                foreach (var item in lista)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ArchivarChat(item.Celular, item.IdAlumno, idPersonal, usuario);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool DesArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.DesArchivarChat(Celular, UsuarioModificacion);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool Desuscribir(string Celular, int IdAlumno, string UsuarioModificacion)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.Desuscribir(Celular, IdAlumno, UsuarioModificacion);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool DesuscribirChatMasivo(List<WhatsAppChatArchivadoDTO> lista, string usuario)
        {
            try
            {
                foreach (var item in lista)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.Desuscribir(item.Celular, item.IdAlumno, usuario);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los chats asosciados al celular
        /// </summary>
        public bool SuscribirAlumno(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.SuscribirAlumno(Celular, IdAlumno, UsuarioModificacion);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Combos para atributos de alumnos
        /// </summary>
        public ComboAtributoAlumnoDTO ObtenerCombosAtributosAlumno()
        {
            ComboAtributoAlumnoDTO CombosAtributosAlumno = new ComboAtributoAlumnoDTO();
            try
            {
                CombosAtributosAlumno.ComboIndustria = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerComboIndustria();
                CombosAtributosAlumno.ComboAreaFormacion = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerComboAreaFormacion();
                CombosAtributosAlumno.ComboAreaTrabajo = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerComboAreaTrabajo();
                CombosAtributosAlumno.ComboCargo = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerComboCargo();
                CombosAtributosAlumno.ComboTamanioEmpresa = _unitOfWork.TamanioEmpresaRepository.ObtenerCombo().ToList();
                return CombosAtributosAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return CombosAtributosAlumno;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Combos para atributos de alumnos
        /// </summary>
        public AtributosAlumnoDTO ObtenerDatosAlumnoWhatsApp(int IdAlumno)
        {
            AtributosAlumnoDTO AtributosAlumno = new AtributosAlumnoDTO();
            try
            {
                AtributosAlumno.ObtenerAtributosAlumno = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerDatosAlumnoWhatsApp(IdAlumno);
                AtributosAlumno.HistorialAlumno = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerHistorialAlumnoWhatsApp(IdAlumno);

                return AtributosAlumno;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return AtributosAlumno;
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 05/28/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Combos para atributos de alumnos
        /// </summary>
        public ObtenerAtributosAlumnoOriginalDTO ActualizarDatosAlumno(ObtenerAtributosAlumnoDTO AlumnoActualizar, string Usuario)
        {
            var oportunidad = new OportunidadService(_unitOfWork);

            ObtenerAtributosAlumnoOriginalDTO ObtenerAtributosOriginalAlumno = new ObtenerAtributosAlumnoOriginalDTO();
            try
            {
                if (!_unitOfWork.WhatsAppMensajeEnviadoRepository.VerificarActualizarAlumno(AlumnoActualizar.Id.Value))
                {
                    throw new Exception("No se puede actualizar el alumno más de dos veces en el mismo día.");
                }
                ObtenerAtributosOriginalAlumno = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerDatosOriginalesAlumnoWhatsApp(AlumnoActualizar.Id);
                bool recalculoProgramaAlumno = false;

                if (AlumnoActualizar.IdAFormacion != ObtenerAtributosOriginalAlumno.IdAFormacion ||
                    AlumnoActualizar.IdATrabajo != ObtenerAtributosOriginalAlumno.IdATrabajo ||
                    AlumnoActualizar.IdIndustria != ObtenerAtributosOriginalAlumno.IdIndustria ||
                    AlumnoActualizar.IdCargo != ObtenerAtributosOriginalAlumno.IdCargo)
                {
                    recalculoProgramaAlumno = true;
                }


                if (AlumnoActualizar.IdAFormacion != ObtenerAtributosOriginalAlumno.IdAFormacion)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "IdAFormacion", ObtenerAtributosOriginalAlumno.IdAFormacion.ToString(), AlumnoActualizar.IdAFormacion.ToString(), Usuario);
                }
                if (AlumnoActualizar.IdATrabajo != ObtenerAtributosOriginalAlumno.IdATrabajo)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "IdATrabajo", ObtenerAtributosOriginalAlumno.IdATrabajo.ToString(), AlumnoActualizar.IdATrabajo.ToString(), Usuario);
                }
                if (AlumnoActualizar.IdIndustria != ObtenerAtributosOriginalAlumno.IdIndustria)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "IdIndustria", ObtenerAtributosOriginalAlumno.IdIndustria.ToString(), AlumnoActualizar.IdIndustria.ToString(), Usuario);
                }
                if (AlumnoActualizar.IdCargo != ObtenerAtributosOriginalAlumno.IdCargo)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "IdCargo", ObtenerAtributosOriginalAlumno.IdCargo.ToString(), AlumnoActualizar.IdCargo.ToString(), Usuario);
                }
                if (AlumnoActualizar.Nombre1 != ObtenerAtributosOriginalAlumno.Nombre1)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Nombre1", ObtenerAtributosOriginalAlumno.Nombre1, AlumnoActualizar.Nombre1, Usuario);
                }
                if (AlumnoActualizar.Nombre2 != ObtenerAtributosOriginalAlumno.Nombre2)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Nombre2", ObtenerAtributosOriginalAlumno.Nombre2, AlumnoActualizar.Nombre2, Usuario);
                }
                if (AlumnoActualizar.Email2 != ObtenerAtributosOriginalAlumno.Email2)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Email2", ObtenerAtributosOriginalAlumno.Email2, AlumnoActualizar.Email2, Usuario);
                }
                if (AlumnoActualizar.ApellidoPaterno != ObtenerAtributosOriginalAlumno.ApellidoPaterno)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "ApellidoPaterno", ObtenerAtributosOriginalAlumno.ApellidoPaterno, AlumnoActualizar.ApellidoPaterno, Usuario);
                }
                if (AlumnoActualizar.ApellidoMaterno != ObtenerAtributosOriginalAlumno.ApellidoMaterno)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "ApellidoMaterno", ObtenerAtributosOriginalAlumno.ApellidoMaterno, AlumnoActualizar.ApellidoMaterno, Usuario);
                }
                var nuevoCelular = AlumnoActualizar.Celular?.Trim();
                if (AlumnoActualizar.Celular != ObtenerAtributosOriginalAlumno.Celular)
                {
                    if (AlumnoActualizar.Celular != null && AlumnoActualizar.Celular != "" && !string.IsNullOrEmpty(AlumnoActualizar.Celular) && !string.IsNullOrWhiteSpace(nuevoCelular))
                    {
                        _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Celular", ObtenerAtributosOriginalAlumno.Celular, AlumnoActualizar.Celular, Usuario);
                    }
                    
                }
                if (AlumnoActualizar.Telefono != ObtenerAtributosOriginalAlumno.Telefono)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Telefono", ObtenerAtributosOriginalAlumno.Telefono, AlumnoActualizar.Telefono, Usuario);
                }
                var nuevoCelular2 = AlumnoActualizar.Celular2?.Trim();
                if (AlumnoActualizar.Celular2!= ObtenerAtributosOriginalAlumno.Celular2)
                {
                    if(AlumnoActualizar.Celular2 != null && AlumnoActualizar.Celular2 != "" &&  !string.IsNullOrEmpty(AlumnoActualizar.Celular2) && !string.IsNullOrWhiteSpace(nuevoCelular2))
                    {
                        _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Celular2", ObtenerAtributosOriginalAlumno.Celular2, AlumnoActualizar.Celular2, Usuario);
                    }
                       
                }
                if (AlumnoActualizar.Dni != ObtenerAtributosOriginalAlumno.Dni)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "Dni", ObtenerAtributosOriginalAlumno.Dni, AlumnoActualizar.Dni, Usuario);
                }
                if (AlumnoActualizar.IdTamanioEmpresaAgenda != ObtenerAtributosOriginalAlumno.IdTamanioEmpresaAgenda)
                {
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "IdTamanioEmpresaAgenda", ObtenerAtributosOriginalAlumno.IdTamanioEmpresaAgenda.ToString(), AlumnoActualizar.IdTamanioEmpresaAgenda.ToString(), Usuario);
                }
                //if (AlumnoActualizar.UsuarioModificacion != ObtenerAtributosOriginalAlumno.UsuarioModificacion)
                //{
                //    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(AlumnoActualizar.Id, "UsuarioModificacion", ObtenerAtributosOriginalAlumno.IdTamanioEmpresaAgenda.ToString(), AlumnoActualizar.UsuarioModificacion.ToString(), Usuario);
                //}
                //if (AlumnoActualizar.FechaModificacion != ObtenerAtributosOriginalAlumno.FechaModificacion)
                //{
                //    AlumnoActualizar.FechaModificacion = DateTime.Now;

                //    _unitOfWork.WhatsAppMensajeEnviadoRepository.ActualizarDatosAlumno(
                //        AlumnoActualizar.Id,
                //        "FechaModificacion",
                //        ObtenerAtributosOriginalAlumno.FechaModificacion?.ToString() ?? "N/A", 
                //        AlumnoActualizar.FechaModificacion.ToString(),
                //        Usuario
                //    );
                //}

                //  _unitOfWork.AlumnoRepository.ActualizarAlumnoWhatsapp(valor);
                // Llamar al recalculo si el flag está activado
                if (recalculoProgramaAlumno)
                {
                    int idAlumno = AlumnoActualizar.Id.Value;
                    _unitOfWork.WhatsAppMensajeEnviadoRepository.EliminarRegistroModeloPredictivoPorAlumno(idAlumno);

                    oportunidad.ObtenerProbabilidadTodosProgramasPorAlumno(idAlumno);
                }
                //_unitOfWork.WhatsAppMensajeEnviadoRepository.RegistrarActualizacionAlumno(AlumnoActualizar.Id.Value, Usuario);

                return ObtenerAtributosOriginalAlumno;

            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ObtenerAtributosOriginalAlumno;
        }

        public int CrearOportunidadWhatsapp(OportunidadWhatsappDTO dto, string usuario)
        {
            try
            {
                var reprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                var oportunidadService = new OportunidadService(_unitOfWork);

                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(dto.IdCentroCosto);
                if (pespecifico == null)
                {
                    throw new BadRequestException("No existe el Programa Especifico");
                }
                var alummno = _unitOfWork.AlumnoRepository.ObtenerPorId(dto.IdAlumno);
                //var alummno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno);
                if (alummno == null)
                {
                    throw new BadRequestException("El alumno no existe");
                }

                var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdAlumno(dto.IdAlumno);

                ReprogramacionDTO oportunidadReprogramacionNueva = new ReprogramacionDTO();

                oportunidadReprogramacionNueva.Oportunidad = new Oportunidad();
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            //Logica Nueva Oportunidad Actividad
                            oportunidadReprogramacionNueva.Oportunidad.IdFaseOportunidad = ValorEstatico.IdFaseOportunidadBNC;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAsignado = dto.IdPersonalAsignado;
                            oportunidadReprogramacionNueva.Oportunidad.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                            oportunidadReprogramacionNueva.Oportunidad.IdOrigen = 954;// Whatsapp Chat Bases Propias
                            oportunidadReprogramacionNueva.Oportunidad.IdAlumno = dto.IdAlumno;
                            oportunidadReprogramacionNueva.Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                            oportunidadReprogramacionNueva.Oportunidad.UltimaFechaProgramada = null;
                            //oportunidadReprogramacionNueva.IdTipoInteraccion = 15;
                            oportunidadReprogramacionNueva.Oportunidad.IdCentroCosto = dto.IdCentroCosto;
                            oportunidadReprogramacionNueva.Oportunidad.Estado = true;
                            oportunidadReprogramacionNueva.Oportunidad.FechaRegistroCampania = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.FechaCreacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.FechaModificacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioCreacion = usuario + "Whatsapp_C";
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioModificacion = usuario;
                            oportunidadReprogramacionNueva.Oportunidad.IdClasificacionPersona = clasificacionPersona.Id;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAreaTrabajo = ValorEstatico.IdPersonalAreaTrabajoVentas;


                            //SE CREA UNA NUEVA OPORTUNIDAD
                            reprogramacionService.CrearOportunidad(ref oportunidadReprogramacionNueva, false, TipoPersona.Alumno);

                            scope.Complete();


                        }

                        catch (Exception ex)
                        {
                            // scope.Dispose();
                            List<string> correos = new List<string>
                            {
                                "sistemas@bsginstitute.com"
                            };
                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error Creacion Oportunidad Transaction";
                            mailData.Message = "IdAlumno: " + dto.IdAlumno.ToString() + "<br/>Asesor:" + "<br/><br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;
                            try
                            {
                                TMK_MailService mailService = new TMK_MailService();
                                mailService.SetData(mailData);
                                mailService.SendMessageTask();
                            }
                            catch { }
                            throw;
                        }
                    }


                    ///Calculo nuevo modelo predictivo
                    ///Carlos Crispin Riquelme
                    try
                    {
                        var nuevaProbabilidad = _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivo(oportunidadReprogramacionNueva.Oportunidad.Id);
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        oportunidadService.MetodoODyOM(oportunidadReprogramacionNueva.Oportunidad.Id);
                    }
                    catch (Exception ex)
                    {
                    }
                
                    return (oportunidadReprogramacionNueva.Oportunidad.Id);
                    //return _unitOfWork.OportunidadRepository.ObtenerDatosOportunidad(oportunidadReprogramacionNueva.Oportunidad!.Id)!;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesOperacionesDTO> ListaHistorialMensajeChatOperaciones(int idPersonal, string numero, string area)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ListaHistorialMensajeChatOperaciones(idPersonal, numero, area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Autor: Margiory Ramirez
        /// Fecha: 11/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene probabilidad por oportunidad
        /// </summary>
        public List<ProbabilidaWhatsAppDTO> ObtenerProbabilidadPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerProbabilidadPorOportunidad(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 11/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene nombre del programa  orginal por oportunidad
        /// </summary>
        public List<ProgramaPorOportunidadDTO> ObtenerProgramaPorOportunidadWhatsapp(int idOportunidad)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerProgramaPorOportunidadWhatsapp(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 20/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los progrmas por vneta cruzada
        /// </summary>
        public ProbabilidadResultadoDTO ValidarProbabilidadOportunidades(int idOportunidad, int idAlumno, int idArea, int idPGeneral)
        {
            try
            {
                List<ProbabilidaWhatsAppDTO> probabilidades = ObtenerProbabilidadPorOportunidad(idOportunidad);

                var probabilidad = probabilidades.FirstOrDefault();
                var resultado = new ProbabilidadResultadoDTO();

                if (probabilidad != null)
                {
                    string probabilidadPrincipal = "";
                    switch (probabilidad.IdProbabilidadRegistroPW)
                    {
                        case 2:
                            probabilidadPrincipal = "Media";
                            break;
                        case 3:
                            probabilidadPrincipal = "Alta";
                            break;
                        case 4:
                            probabilidadPrincipal = "Muy Alta";
                            break;
                    }

                    resultado.Probabilidad = probabilidadPrincipal;

                    if (probabilidadPrincipal == "Media" || probabilidadPrincipal == "Alta")
                    {
                        var oportunidadInformacion = ObtenerOportunidadInformacionWhatsapp(idAlumno, idArea, idPGeneral);

                        var listaFiltrada = oportunidadInformacion.ListaVentaCruzada
                            .Where(vc => vc.ProbabilidadTexto != null && vc.ProbabilidadTexto == "Muy Alta")
                            .ToList();

                        resultado.ListaVentaCruzadaWhatsapp = listaFiltrada;

                        if (listaFiltrada.Any())
                        {
                            resultado.Apto = true;
                            resultado.Mensaje = "El prospecto es apto para promocionar otro programa.";
                        }
                        else
                        {
                            resultado.Apto = false;
                            resultado.Mensaje = "El prospecto NO es apto para promocionar otro programa.";
                        }
                    }
                    else if (probabilidadPrincipal == "Muy Alta")
                    {
                        resultado.Apto = true;
                        resultado.Mensaje = "La probabilidad es Muy Alta. Continuar con el siguiente flujo.";
                    }
                }
                else
                {
                    resultado.Mensaje = "No se encontró la probabilidad para la oportunidad.";
                }

                return resultado;
            }
            catch (Exception e)
            {
                return new ProbabilidadResultadoDTO { Mensaje = $"Ocurrió un error: {e.Message}" };
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 20/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos probabilidad
        /// </summary>
        public OportunidadInformacionWhatsappDTO ObtenerOportunidadInformacionWhatsapp(int idAlumno, int idArea, int idPGeneral)
        {
            try
            {
                var oportunidadInformacion = new OportunidadInformacionWhatsappDTO()
                {
                    IdAlumno = idAlumno,
                    IdArea = idArea,
                    IdPGeneral = idPGeneral,
                };
                var whatsAppMensajeEnviadoService = new WhatsAppMensajeEnviadoService(_unitOfWork);
                oportunidadInformacion.ListaVentaCruzada =
                    whatsAppMensajeEnviadoService.ObtenerVentaCruzadaPorIdAlumnoWhatsapp(oportunidadInformacion.IdAlumno, oportunidadInformacion.IdArea, oportunidadInformacion.IdPGeneral).ToList();

                return oportunidadInformacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Margiory Ramirez
        /// Fecha: 20/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene top 3 de pgeneral con mas probabilidad
        /// </summary>
        public IEnumerable<OportunidadVentaCruzadaWhatsappDTO> ObtenerVentaCruzadaPorIdAlumnoWhatsapp(int idAlumno, int IdArea, int idPGeneral)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerVentaCruzadaPorIdAlumnoWhatsapp(idAlumno, IdArea, idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerPersonalOportunidad()
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerPersonalOportunidad();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AsesorActualDTO ObtenerIdAsesorActual(int idOportunidad)
        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerIdAsesorActual(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ModeloPredictivoDTO ObtenerModeloPredictivoPorAlumnoYPrograma(int idAlumno, int idPGeneral)

        {
            try
            {
                return _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerModeloPredictivoPorAlumnoYPrograma(idAlumno, idPGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ProbabilidadResultadoDTO ValidarProbabilidadOportunidadesRecalculo(int idOportunidad, int idAlumno, int idArea, int idPGeneral)
        {
            try
            {

                var modeloTipo = ObtenerModeloPredictivoPorAlumnoYPrograma(idAlumno, idPGeneral);


                var servicioOportunidad = new OportunidadService(_unitOfWork);
                var recalculoModeloPredictivo = servicioOportunidad.ObtenerProbabilidadModeloPredictivoMarketing(idOportunidad, modeloTipo.Tipo);

                List<ProbabilidaWhatsAppDTO> probabilidades = ObtenerProbabilidadPorOportunidad(idOportunidad);

                var probabilidad = probabilidades.FirstOrDefault();
                var resultado = new ProbabilidadResultadoDTO();

                if (probabilidad != null)
                {
                    string probabilidadPrincipal = "";
                    switch (probabilidad.IdProbabilidadRegistroPW)
                    {
                        case 2:
                            probabilidadPrincipal = "Media";
                            break;
                        case 3:
                            probabilidadPrincipal = "Alta";
                            break;
                        case 4:
                            probabilidadPrincipal = "Muy Alta";
                            break;
                    }

                    resultado.Probabilidad = probabilidadPrincipal;

                    if (probabilidadPrincipal == "Media" || probabilidadPrincipal == "Alta")
                    {
                        var oportunidadInformacion = ObtenerOportunidadInformacionWhatsapp(idAlumno, idArea, idPGeneral);

                        var listaFiltrada = oportunidadInformacion.ListaVentaCruzada
                            .Where(vc => vc.ProbabilidadTexto != null && vc.ProbabilidadTexto == "Muy Alta")
                            .ToList();

                        resultado.ListaVentaCruzadaWhatsapp = listaFiltrada;

                        if (listaFiltrada.Any())
                        {
                            resultado.Apto = true;  // Es apto
                            resultado.Mensaje = "El prospecto es apto para promocionar otro programa.";
                        }
                        else
                        {
                            resultado.Apto = false; // No es apto
                            resultado.Mensaje = "El prospecto NO es apto para promocionar otro programa.";
                        }
                    }
                    else if (probabilidadPrincipal == "Muy Alta")
                    {
                        resultado.Apto = true;
                        resultado.Mensaje = "La probabilidad es Muy Alta. Continuar con el siguiente flujo.";
                    }
                }
                else
                {
                    resultado.Mensaje = "No se encontró la probabilidad para la oportunidad.";
                }

                return resultado;
            }
            catch (Exception e)
            {
                return new ProbabilidadResultadoDTO { Mensaje = $"Ocurrió un error: {e.Message}" };
            }
        }

        public void EliminarRegistroModeloPredictivoPorAlumno(int idAlumno)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerIdAsesorActual(idAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Humberto Oscata
        /// Fecha: 29/08/2025
        /// Version: 1.0
        /// <summary>
        /// Captura registros de alumnos en base a chats mediante un modelo IA
        /// </summary>
        /// <param name="celular">Celular del alumno</param>
        /// <param name="rango">Antiguedad de los chats a analizar</param>
        /// <returns>Datos capturados por el modelo IA</returns>
        public async Task<DatosExtraccionRegistrosResponseDTO> CapturarRegistrosModeloIA(DatosExtraccionRegistrosDTO datosExtraccionRegistros)
        {
            // 1. Obtencion de chats
            List<MensajeExtraccionRegistroDTO> ChatsWhatsAppMarketing = new List<MensajeExtraccionRegistroDTO>();
            DateTime fechaFin = DateTime.Now;
            DateTime fechaInicio = fechaFin.AddDays(-datosExtraccionRegistros.Rango);

            ChatsWhatsAppMarketing = _unitOfWork.WhatsAppMensajeEnviadoRepository.ObtenerChatWhatsAppMarketingPorCelularRangoFecha(datosExtraccionRegistros.CelularAlumno, fechaInicio, fechaFin);
            var ultimoMensajeCampania = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerUltimoMensajeCampaniaEnviado(datosExtraccionRegistros.CelularAlumno);

            DatosExtraccionRegistrosRequestDTO datosExtraccionRegistrosRequest = new DatosExtraccionRegistrosRequestDTO
            {
                Id_cliente = datosExtraccionRegistros.CelularAlumno,
                Timestamp = fechaFin.ToString(),
                Mensajes = ChatsWhatsAppMarketing,
                Campos = new List<string>
                                {
                                    "nombres",
                                    "apellidos",
                                    "cargo",
                                    "area_de_formacion",
                                    "area_de_trabajo",
                                    "industria"
                                },
                Info_curso = ultimoMensajeCampania
            };

            // 2. Envio de chats al modelo IA
            string url = $"http://api-asistente-marketing-whatsapp.bsginstitute.com/testing/api/extractor_texto/consulta/";
            //string url = $"http://api-asistente-marketing-whatsapp.bsginstitute.com/api/extractor_texto/consulta/";

            var Serializer = new JavaScriptSerializer();
            var serializedResult = Serializer.Serialize(datosExtraccionRegistrosRequest);

            var resultado = await PostJsonAsync<DatosExtraccionRegistrosResponseDTO>(url, serializedResult);

            if (resultado == null)
                throw new Exception("La respuesta de la API externa fue nula o falló.");

            return resultado;
        }

        private async Task<T> PostJsonAsync<T>(string url, string jsonString)
        {
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                byte[] bytes = Encoding.ASCII.GetBytes(jsonString);

                using (Stream requestStream = await http.GetRequestStreamAsync())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                using (var response = await http.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string content = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el metodo PostJsonAsync: {ex.Message}", ex);
            }
        }
    }
}



