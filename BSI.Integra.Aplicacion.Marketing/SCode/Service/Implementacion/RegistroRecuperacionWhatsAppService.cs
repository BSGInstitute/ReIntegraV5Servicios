using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Wordprocessing;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WhatsAppMensajeEnviadoAutomaticoDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.WhatsAppMensajeEnviadoAutomaticoDTO;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.WhatsAppResultadoConjuntoListaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class RegistroRecuperacionWhatsAppService : IRegistroRecuperacionWhatsAppService
    {
        private const int NRO_MAXIMO_INTENTOS = 5;

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RegistroRecuperacionWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public List<TRegistroRecuperacionWhatsApp> Insert(List<RegistroRecuperacionWhatsAppDTO> registroSeguimientoRecuperacion, string usuario)
        {
            IEnumerable<RegistroRecuperacionWhatsAppDTO> datos = registroSeguimientoRecuperacion.Select(X => new RegistroRecuperacionWhatsAppDTO
            {
                Completado = X.Completado,
                Dia = X.Dia,
                Dia1 = X.Dia1,
                Dia2 = X.Dia2,
                Dia3 = X.Dia3,
                Dia4 = X.Dia4,
                Dia5 = X.Dia5,
                Estado = X.Estado,
                Fallido = X.Fallido,
                FechaCreacion = X.FechaCreacion,
                FechaFinEnvioWhatsapp = X.FechaFinEnvioWhatsapp,
                FechaInicioEnvioWhatsapp = X.FechaInicioEnvioWhatsapp,
                FechaModificacion = X.FechaModificacion,
                HoraEnvio = X.HoraEnvio,
                IdCampaniaGeneral = X.IdCampaniaGeneral,
                IdCampaniaGeneralDetalle = X.IdCampaniaGeneralDetalle,
                IdCampaniaGeneralDetalleResponsable = X.IdCampaniaGeneralDetalleResponsable,
                IdMigracion = X.IdMigracion,
                IdPersonal = X.IdPersonal,
                IdPlantilla = X.IdPlantilla,
                IdWhatsAppConfiguracionEnvio = X.IdWhatsAppConfiguracionEnvio,
                RecuperacionEnProceso = X.RecuperacionEnProceso,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
            }).ToList();

            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Add(datos).ToList();
        }
        public TRegistroRecuperacionWhatsApp Add(RegistroRecuperacionWhatsAppDTO entidad)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Add(entidad);
        }
        public TRegistroRecuperacionWhatsApp Update(RegistroRecuperacionWhatsAppDTO entidad)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(entidad);
        }
        public bool Delete(int id, string usuario)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Delete(id, usuario);
        }
        public IEnumerable<TRegistroRecuperacionWhatsApp> Add(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Add(listadoEntidad);
        }
        public IEnumerable<TRegistroRecuperacionWhatsApp> Update(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(listadoEntidad);
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Delete(listadoIds, usuario);
        }

        public bool ActualizarCompletadoRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.ActualizarCompletadoRegistroWhatsApp(idCampaniaGeneralDetalle, idCampaniaGeneralDetalleResponsable);
        }
        public int ObtenerCantidadCaidaRecuperacionWhatsApp()
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.ObtenerCantidadCaidaRecuperacionWhatsApp();
        }
        public int ObtenerCantidadWhatsAppPreprocesadoRealizado(int idCampaniaGeneralDetalle, int idPersonal, DateTime fechaInicio, DateTime fechaFin)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.ObtenerCantidadWhatsAppPreprocesadoRealizado(idCampaniaGeneralDetalle, idPersonal, fechaInicio, fechaFin);
        }
        public bool ActualizarFalloRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.ActualizarFalloRegistroWhatsApp(idCampaniaGeneralDetalle, idCampaniaGeneralDetalleResponsable);
        }
        public bool DesactivarCompletadoRegistroWhatsApp(string usuario)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.DesactivarCompletadoRegistroWhatsApp(usuario);
        }
        public IEnumerable<RegistroRecuperacionWhatsAppDTO> GetBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            return _unitOfWork.RegistroRecuperacionWhatsAppRepository.GetBy(filter);
        }
        public string EjecutarRecuperacionFallidoEnvioWhatsApp(int IdTipoError)
        {
            try
            {
                var resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                var listaRegistroFallido = new List<RegistroRecuperacionWhatsAppDTO>();

                if (IdTipoError == 1)
                {
                    listaRegistroFallido = _unitOfWork.RegistroRecuperacionWhatsAppRepository.GetBy(
                    x => !x.Completado
                        && x.Fallido.HasValue
                        && x.Fallido.Value
                        &&
                        (
                        (x.RecuperacionEnProceso.HasValue && !x.RecuperacionEnProceso.Value) || (!x.RecuperacionEnProceso.HasValue)
                        )
                        && x.FechaCreacion >= DateTime.Now.Date
                        && x.FechaCreacion <= DateTime.Now.Date.AddDays(1)
                    ).ToList();
                }
                else if (IdTipoError == 2)
                {
                    listaRegistroFallido = _unitOfWork.RegistroRecuperacionWhatsAppRepository.GetBy(
                    x => !x.Completado
                        && x.Fallido.HasValue
                        && !x.Fallido.Value
                        &&
                        (
                        (x.RecuperacionEnProceso.HasValue && !x.RecuperacionEnProceso.Value) || (!x.RecuperacionEnProceso.HasValue)
                        )
                        && x.FechaCreacion >= DateTime.Now.Date
                        && x.FechaCreacion <= DateTime.Now.Date.AddDays(1)
                    ).ToList();
                }
                else
                {
                    return "No es un tipo de error permitido";
                }

                var listaRegistroEvaluado = listaRegistroFallido.Select(s => new DiaFallidoEvaluadoDTO
                {
                    Id = s.Id,
                    IdCampaniaGeneral = s.IdCampaniaGeneral,
                    IdCampaniaGeneralDetalle = s.IdCampaniaGeneralDetalle,
                    IdPlantilla = s.IdPlantilla,
                    IdPersonal = s.IdPersonal,
                    Dia = s.Dia,
                    FechaEvaluada = s.FechaInicioEnvioWhatsapp.AddDays(s.Dia - 1),
                    IdWhatsAppConfiguracionEnvio = s.IdWhatsAppConfiguracionEnvio,
                    ListaDiaConfigurado = new List<int>() { s.Dia1, s.Dia2, s.Dia3, s.Dia4, s.Dia5 },
                    Cantidad = 0
                }).ToList();

                foreach (var registro in listaRegistroEvaluado)
                {
                    try
                    {
                        #region Recuperacion Activa
                        var recuperacionActiva = _unitOfWork.RecuperacionAutomaticoModuloSistemaRepository.FirstBy(x => x.IdModuloSistema == Transversal.Helper.ValorEstatico.IdModuloSistemaWhatsAppMailing && x.Tipo == "WhatsApp");

                        if (recuperacionActiva == null || (recuperacionActiva != null && !recuperacionActiva.Habilitado))
                        {
                            return "No hay configuracion activa";
                        }
                        #endregion

                        bool obtencionPreResultadoExitosa = true;
                        bool insercionLogEjecucionExitosa = true;
                        bool actualizacionLogEjecucionExitosa = true;
                        bool actualizacionEstadoEnvioWhatsApp = true;
                        int nroActualIntentosObtencionListaPreProcesada = 0;
                        int nroActualIntentosInsercionLogEjecucion = 0;
                        int nroActualIntentosActualizacionLog = 0;
                        int nroActualIntentosActualizacionEstadoEnvio = 0;

                        #region Actualizacion registro En Proceso
                        var registroActualizable = _unitOfWork.RegistroRecuperacionWhatsAppRepository.FirstBy(x => x.Id == registro.Id);

                        registroActualizable.RecuperacionEnProceso = true;
                        registroActualizable.FechaModificacion = DateTime.Now;
                        registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                        _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(registroActualizable);
                        #endregion

                        registro.Cantidad = registro.ListaDiaConfigurado[registro.Dia - 1];

                        var cantidadResultante = _unitOfWork.RegistroRecuperacionWhatsAppRepository.ObtenerCantidadWhatsAppPreprocesadoRealizado(registro.IdCampaniaGeneralDetalle, registro.IdPersonal, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));

                        /* Realizar envio restante */
                        if (Math.Abs(registro.Cantidad - cantidadResultante) > 0)
                        {
                            var respuesta = new List<WhatsAppConfiguracionPreEnvioDTO>();

                            #region Obtencion Prerresultado
                            do
                            {
                                try
                                {
                                    respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(Math.Abs(registro.Cantidad - cantidadResultante), registro.IdCampaniaGeneralDetalle, registro.IdPersonal).Where(w => w.Celular != "1").ToList();

                                    obtencionPreResultadoExitosa = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _unitOfWork.LogRepository.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ObtencionPreEnvioWhatsApp-Rec-Fallida",
                                            Parametros = $"Cantidad={Math.Abs(registro.Cantidad - cantidadResultante)}/IdCampaniaGeneralDetalle={registro.IdCampaniaGeneralDetalle}/IdPersonal={registro.IdPersonal}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "GET",
                                            IdPadre = 0,
                                            UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                            UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    obtencionPreResultadoExitosa = false;
                                    nroActualIntentosObtencionListaPreProcesada++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosObtencionListaPreProcesada >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Supero el limite de obtencionPreEnvioWhatsApp-Rec-Fallida");
                                    }
                                }
                            } while (!obtencionPreResultadoExitosa && nroActualIntentosObtencionListaPreProcesada < NRO_MAXIMO_INTENTOS);
                            #endregion

                            /* Inicio ejecucion envio */
                            if (respuesta.Any())
                            {
                                var logEjecucion = new WhatsAppConfiguracionLogEjecucion();

                                #region Guardado de log de ejecucion
                                do
                                {
                                    try
                                    {

                                        //*---- Verificar si existe otro log antes de crearlo
                                        int flagLogDuplicado = 1;

                                        try
                                        {
                                            int idLogActivoParaDeleteLogico =
                                                _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.obtenerOtrosLogActivos(registro.IdWhatsAppConfiguracionEnvio);
                                            if (idLogActivoParaDeleteLogico != 0)
                                            {
                                                try
                                                {
                                                    var logEjecucionFinal_duplicado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(idLogActivoParaDeleteLogico);

                                                    logEjecucionFinal_duplicado.FechaFin = DateTime.Now;
                                                    logEjecucionFinal_duplicado.UsuarioModificacion = "Recuperacion-activa-Control-Log-Duplicado";
                                                    logEjecucionFinal_duplicado.Estado = false;
                                                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal_duplicado);
                                                    _unitOfWork.Commit();
                                                    //*--InsertarLogDuplicado
                                                    logEjecucion.FechaInicio = DateTime.Now;
                                                    logEjecucion.IdWhatsAppConfiguracionEnvio = registro.IdWhatsAppConfiguracionEnvio;
                                                    logEjecucion.Estado = true;
                                                    logEjecucion.FechaCreacion = DateTime.Now;
                                                    logEjecucion.FechaModificacion = DateTime.Now;
                                                    logEjecucion.UsuarioCreacion = "Recuperacion-activa-Inserccion-Log-Duplicado";
                                                    logEjecucion.UsuarioModificacion = "Recuperacion-activa-Inserccion-Log-Duplicado";
                                                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecucion);
                                                    _unitOfWork.Commit();

                                                    //*--- realizar recalculo
                                                    respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(Math.Abs(registro.Cantidad - cantidadResultante), registro.IdCampaniaGeneralDetalle, registro.IdPersonal).Where(w => w.Celular != "1").ToList();
                                                    flagLogDuplicado = 2;
                                                    insercionLogEjecucionExitosa = true;
                                                }
                                                catch
                                                {
                                                    //*---- mensaje si no se puede eliminar, insertar o hacer el recalculo
                                                    List<string> correosAlerta = new List<string>();
                                                    correosAlerta.Add("emayta@bsginstitute.com");
                                                    TMK_MailService mailServiceAlerta = new TMK_MailService();
                                                    TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                                    mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                                    mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                                    mailDataAlerta.Subject = "Recuperacion activa, ALERTA: No se pudo realizar el recalculo en el siguiente idwhatsappconfiguracionenvio" + registro.IdWhatsAppConfiguracionEnvio + " contactos del envio masivo programado";
                                                    mailDataAlerta.Message = "RIESGO MUY ALTO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + " Se encontro log activo, pero no se pudo eliminar el id es: " + idLogActivoParaDeleteLogico;
                                                    mailDataAlerta.Cc = string.Empty;
                                                    mailDataAlerta.Bcc = string.Empty;
                                                    mailDataAlerta.AttachedFiles = null;
                                                    mailServiceAlerta.SetData(mailDataAlerta);
                                                    mailServiceAlerta.SendMessageTask();
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            List<string> correosAlerta = new List<string>();
                                            correosAlerta.Add("emayta@bsginstitute.com");
                                            TMK_MailService mailServiceAlerta = new TMK_MailService();
                                            TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                            mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                            mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                            mailDataAlerta.Subject = "Recuperacion activa - ALERTA !: No se pudo verificar duplicidad en " + registro.IdWhatsAppConfiguracionEnvio + " contactos del envio masivo programado";
                                            mailDataAlerta.Message = "RIESGO MUY ALTO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + " Se encontro log activo, pero no se pudo eliminar el id es: ";
                                            mailDataAlerta.Cc = string.Empty;
                                            mailDataAlerta.Bcc = string.Empty;
                                            mailDataAlerta.AttachedFiles = null;
                                            mailServiceAlerta.SetData(mailDataAlerta);
                                            mailServiceAlerta.SendMessageTask();
                                        }
                                        //*---- FIN de Verificar si existe otro log antes de crearlo
                                        if (flagLogDuplicado == 1)
                                        {

                                            logEjecucion.FechaInicio = DateTime.Now;
                                            logEjecucion.IdWhatsAppConfiguracionEnvio = registro.IdWhatsAppConfiguracionEnvio;
                                            logEjecucion.Estado = true;
                                            logEjecucion.FechaCreacion = DateTime.Now;
                                            logEjecucion.FechaModificacion = DateTime.Now;
                                            logEjecucion.UsuarioCreacion = "Recuperacion-activa";
                                            logEjecucion.UsuarioModificacion = "Recuperacion-activa";

                                            using (TransactionScope scope = new TransactionScope())
                                            {
                                                _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecucion);

                                                scope.Complete();
                                            };

                                            insercionLogEjecucionExitosa = true;
                                        }
                                        ////*-----------------------------
                                        ////*-----------------------------
                                        ////*-----------------------------

                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "InsercionLogEjecucionWhatsApp",
                                                Parametros = $"FechaInicio={logEjecucion.FechaInicio}/IdWhatsAppConfiguracionEnvio={logEjecucion.IdWhatsAppConfiguracionEnvio}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "INSERT",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        insercionLogEjecucionExitosa = false;
                                        nroActualIntentosInsercionLogEjecucion++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosInsercionLogEjecucion >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de guardadoLogEjecucion");
                                        }
                                    }
                                } while (!insercionLogEjecucionExitosa && nroActualIntentosInsercionLogEjecucion < NRO_MAXIMO_INTENTOS);
                                #endregion

                                foreach (var item in respuesta)
                                {
                                    var objetoWhatsApp = new WhatsAppResultadoConjuntoListaDTO();

                                    try
                                    {
                                        objetoWhatsApp.IdPre = item.Id;
                                        objetoWhatsApp.IdConjuntoListaResultado = 1;
                                        objetoWhatsApp.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                                        objetoWhatsApp.IdAlumno = item.IdAlumno;
                                        objetoWhatsApp.Celular = item.Celular;
                                        objetoWhatsApp.IdCodigoPais = item.IdCodigoPais;
                                        objetoWhatsApp.NroEjecucion = item.NroEjecucion;
                                        objetoWhatsApp.Validado = item.Validado;
                                        objetoWhatsApp.Plantilla = item.Plantilla;
                                        objetoWhatsApp.IdPersonal = item.IdPersonal;
                                        objetoWhatsApp.IdPgeneral = item.IdPgeneral;
                                        objetoWhatsApp.IdPlantilla = item.IdPlantilla;

                                        if (!string.IsNullOrEmpty(item.objetoplantilla))
                                        {
                                            objetoWhatsApp.objetoplantilla = JsonConvert.DeserializeObject<List<DatoPlantillaWhatsAppDTO>>(item.objetoplantilla);
                                            resultado.Add(objetoWhatsApp);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "MapeoObjetoWhatsApp",
                                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={objetoWhatsApp.IdPre}/IdPrioridadMailChimpListaCorreo={objetoWhatsApp.IdPrioridadMailChimpListaCorreo}/IdPersonal={objetoWhatsApp.IdPersonal}/IdPGeneral={objetoWhatsApp.IdPgeneral}/IdPlantilla={objetoWhatsApp.IdPlantilla}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "MAP",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }

                                var EnvioMensajes = EnvioAutomaticoPlantillaMasivo(resultado, registro.IdPersonal, registro.IdPlantilla, logEjecucion.Id, true);

                                #region Actualizacion de logEjecucion Final
                                do
                                {
                                    try
                                    {
                                        var logEjecucionFinal = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(logEjecucion.Id);
                                        logEjecucionFinal.FechaFin = DateTime.Now;
                                        _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal);
                                        _unitOfWork.Commit();
                                        actualizacionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionLogEjecucionWhatsApp",
                                                Parametros = string.Empty,
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionLogEjecucionExitosa = false;
                                        nroActualIntentosActualizacionLog++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionLog >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionLogEjecucionWhatsApp");
                                        }
                                    }
                                } while (!actualizacionLogEjecucionExitosa && nroActualIntentosActualizacionLog < NRO_MAXIMO_INTENTOS);
                                #endregion

                                #region Actualizar estado envio WhatsApp
                                do
                                {
                                    try
                                    {
                                        _unitOfWork.CampaniaGeneralRepository.ActualizarEstadoEnvioWhatsApp(registro.IdCampaniaGeneral);
                                        _unitOfWork.Commit();
                                        actualizacionEstadoEnvioWhatsApp = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionEstadoEnvioWhatsApp",
                                                Parametros = $"IdCampaniaGeneral={registro.IdCampaniaGeneral}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionEstadoEnvioWhatsApp = false;
                                        nroActualIntentosActualizacionEstadoEnvio++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionEstadoEnvio >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionEstadoEnvioWhatsApp");
                                        }
                                    }
                                } while (!actualizacionEstadoEnvioWhatsApp && nroActualIntentosActualizacionEstadoEnvio < NRO_MAXIMO_INTENTOS);
                                #endregion
                            }
                            /* Fin ejecucion envio */
                        }

                        #region Actualizacion registro Finalizado
                        registroActualizable = _unitOfWork.RegistroRecuperacionWhatsAppRepository.FirstBy(x => x.Id == registro.Id);

                        registroActualizable.RecuperacionEnProceso = false;
                        registroActualizable.Fallido = false;
                        registroActualizable.Completado = true;
                        registroActualizable.FechaModificacion = DateTime.Now;
                        registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                        _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(registroActualizable);
                        _unitOfWork.Commit();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";
                            var cantidadResultante = _unitOfWork.RegistroRecuperacionWhatsAppRepository.ObtenerCantidadWhatsAppPreprocesadoRealizado(registro.IdCampaniaGeneralDetalle, registro.IdPersonal, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));

                            _unitOfWork.LogRepository.Insert(new TLog
                            {
                                Ip = "-",
                                Usuario = "-",
                                Maquina = "-",
                                Ruta = "ObtencionPreEnvioWhatsApp-Rec-Fallida",
                                Parametros = $"Cantidad={(Math.Abs(registro.Cantidad - cantidadResultante))}/IdCampaniaGeneralDetalle={registro.IdCampaniaGeneralDetalle}/IdPersonal={registro.IdPersonal}",
                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                Tipo = "GET",
                                IdPadre = 0,
                                UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            });
                            _unitOfWork.Commit();
                            #region Actualizacion registro Finalizado
                            var registroActualizable = _unitOfWork.RegistroRecuperacionWhatsAppRepository.FirstBy(x => x.Id == registro.Id);

                            registroActualizable.RecuperacionEnProceso = false;
                            registroActualizable.Fallido = true;
                            registroActualizable.Completado = false;
                            registroActualizable.FechaModificacion = DateTime.Now;
                            registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                            _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(registroActualizable);
                            _unitOfWork.Commit();
                            #endregion
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: La funcion permite ralizar el envio de mensajes de wahtsaap con la funcion editada en el tema de plantilla (template) en donde se valida el estado y registra en un objetos el envio que sera retornado
        /// </summary>
        /// <param name="MensajeAlumno">Lista de mensajes con los numeros a envaiar por whatsapp</param>
        /// <param name="IdPersonal">Identificador del personal</param>
        /// <param name="IdPlantilla">Identificador de la plantilla</param>
        /// <param name="IdWhatsAppConfiguracionLogEjecucion">Identificador de la configuracion de whatsapp</param>
        /// <returns>Retorna la lista de las condiguraciones del envio realizado segun el estado de cada mensaje procesado por whatsapp</returns>
        private List<WhatsAppConfiguracionEnvioDetalleDTO> EnvioAutomaticoPlantillaMasivo(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdPersonal, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion, bool aplicarLimiteEspera = false)
        {
            bool BanderaLogin = false;
            string TokenComunicacion = string.Empty;
            var Plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(IdPlantilla);

            int cantidadGrupoActual = 0;
            int cantidadErrorEnvioActual = 0;
            const int LIMITE_CANTIDAD_ESPERA = 5;
            const int LIMITE_CANTIDAD_ERROR_ENVIO = 100;
            //Contadores del control para envios duplciados
            int contaDuplicadosVerificados = 0;
            int contaNumerosSinDuplicados = 0;
            int contaNoVerificados = 0;

            var RegistroEstadoEnvio = new List<WhatsAppConfiguracionEnvioDetalleDTO>();

            foreach (var alumnoMensaje in MensajeAlumno)
            {
                int ForzarDetencionLogEstado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivo(IdWhatsAppConfiguracionLogEjecucion);

                if (ForzarDetencionLogEstado == 0)
                {
                    List<string> correosAlerta = new List<string>();
                    correosAlerta.Add("emayta@bsginstitute.com");
                    TMK_MailService mailServiceAlerta = new TMK_MailService();
                    TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                    mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                    mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                    mailDataAlerta.Subject = "////// ALERTA -Riesgo Bajo: Se eliminó el siguiente log:" + IdWhatsAppConfiguracionLogEjecucion;
                    mailDataAlerta.Message = "RIESGO BAJO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + "Ejecutar el siguiente SP: mkt.SP_SeguimientoDeEnviosDuplicados [Colocar id de la campania general]" + "<br/>" + "Asi mismo ejecutar el sp de salida de mensajes de whatsapp: mkt.SP_ControlDeSalidaDeMensajesWhatsApp [enviar el ID pais]";
                    mailDataAlerta.Cc = string.Empty;
                    mailDataAlerta.Bcc = string.Empty;
                    mailDataAlerta.AttachedFiles = null;
                    mailServiceAlerta.SetData(mailDataAlerta);
                    mailServiceAlerta.SendMessageTask();
                    return RegistroEstadoEnvio;
                }

                cantidadGrupoActual++;
                var DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = alumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = alumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = alumnoMensaje.objetoplantilla
                };

                try
                {
                    //Control_duplciados_anterior
                    int envioDuplicado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.VerificadEnvioDuplicado(alumnoMensaje.Celular);
                    //Control_duplciados_anterior
                    string celularWhatsappEstadoMensajeEnviado = (string)alumnoMensaje.Celular;
                    envioDuplicado = _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.VerificadEnvioDuplicadoPorEnvioMasivo(celularWhatsappEstadoMensajeEnviado);

                    if (envioDuplicado != 1)
                    {
                        contaNumerosSinDuplicados++; //Contador De Envios sin duplicados

                        if (envioDuplicado == 3)
                        {
                            if (contaNoVerificados >= 50)
                            {
                                List<string> correosAlerta = new List<string>();
                                correosAlerta.Add("sistemas@bsginstitute.com");
                                TMK_MailService mailServiceAlerta = new TMK_MailService();
                                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                mailDataAlerta.Subject = "! ALERTA !: No se pudo verificar duplicidad en " + contaNoVerificados + " contactos del envio masivo programado";
                                mailDataAlerta.Message = "RIESGO ALTO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + "Ejecutar el siguiente SP: mkt.SP_SeguimientoDeEnviosDuplicados [Colocar id de la campania general]" + "<br/>" + "Asi mismo ejecutar el sp de salida de mensajes de whatsapp: mkt.SP_ControlDeSalidaDeMensajesWhatsApp [La verificacion lo realiza por dia]";
                                mailDataAlerta.Cc = string.Empty;
                                mailDataAlerta.Bcc = string.Empty;
                                mailDataAlerta.AttachedFiles = null;
                                mailServiceAlerta.SetData(mailDataAlerta);
                                mailServiceAlerta.SendMessageTask();
                                contaNoVerificados = 0;
                            }
                        }
                        ServicePointManager.ServerCertificateValidationCallback = delegate (
                            object s,
                            X509Certificate certificate,
                            X509Chain chain,
                            SslPolicyErrors sslPolicyErrors
                        )
                        {
                            return true;
                        };

                        /*Repositorios*/
                        var _repCredenciales = new WhatsAppConfiguracionService(_unitOfWork);
                        var _repTokenUsuario = new WhatsAppUsuarioCredencialService(_unitOfWork);

                        var CredencialesHost = _repCredenciales.ObtenerCredencialHost(alumnoMensaje.IdCodigoPais);
                        var TokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, alumnoMensaje.IdCodigoPais);

                        string urlToPost = CredencialesHost.UrlWhatsApp;

                        string Resultado = string.Empty, WaType = string.Empty;

                        if (TokenValida == null || DateTime.Now >= TokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = $"{CredencialesHost.UrlWhatsApp}/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                            var restClient = new RestClient(urlToPostUsuario);
                            var restRequest = new RestSharp.RestRequest(Method.POST);

                            restRequest.AddHeader("cache-control", "no-cache");
                            restRequest.AddHeader("Content-Length", "");
                            restRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                            restRequest.AddHeader("Host", CredencialesHost.IpHost);
                            restRequest.AddHeader("Cache-Control", "no-cache");
                            restRequest.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                            restRequest.AddHeader("Content-Type", "application/json");

                            IRestResponse response = restClient.Execute(restRequest);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                foreach (var item in datos.users)
                                {
                                    var modelCredencial = new WhatsAppUsuarioCredencial
                                    {
                                        IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                        IdWhatsAppConfiguracion = CredencialesHost.Id,
                                        UserAuthToken = item.token,
                                        ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                        EsMigracion = true,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = "whatsapp",
                                        UsuarioModificacion = "whatsapp"
                                    };

                                    var rpta = _repTokenUsuario.Add(modelCredencial);

                                    TokenComunicacion = item.token;
                                }

                                BanderaLogin = true;
                            }
                            else
                            {
                                BanderaLogin = false;
                            }
                        }
                        else
                        {
                            TokenComunicacion = TokenValida.UserAuthToken;
                            BanderaLogin = true;
                        }

                        if (BanderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = $"{CredencialesHost.UrlWhatsApp}/v1/messages";
                                    WaType = "text";

                                    var MensajeTexto = new MensajeTextoEnvio
                                    {
                                        to = DTO.WaTo,
                                        type = DTO.WaType,
                                        recipient_type = DTO.WaRecipientType,
                                        text = new text
                                        {
                                            body = DTO.WaBody
                                        }
                                    };

                                    using (WebClient Client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeTexto);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeTexto);
                                        string myParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        Resultado = Client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate MensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    MensajePlantilla.to = DTO.WaTo;
                                    MensajePlantilla.type = "template";
                                    MensajePlantilla.template = new template();

                                    MensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    MensajePlantilla.template.name = DTO.WaBody;

                                    MensajePlantilla.template.language = new language();
                                    MensajePlantilla.template.language.policy = "deterministic";
                                    MensajePlantilla.template.language.code = "es";

                                    MensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";


                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.Texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    MensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "image";

                                    MensajeImagenEnvio MensajeImagen = new MensajeImagenEnvio();
                                    MensajeImagen.to = DTO.WaTo;
                                    MensajeImagen.type = DTO.WaType;
                                    MensajeImagen.recipient_type = DTO.WaRecipientType;

                                    MensajeImagen.image = new image();

                                    MensajeImagen.image.caption = DTO.WaCaption;
                                    MensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeImagen);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeImagen);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "document";

                                    MensajeDocumentoEnvio MensajeDocumento = new MensajeDocumentoEnvio();
                                    MensajeDocumento.to = DTO.WaTo;
                                    MensajeDocumento.type = DTO.WaType;
                                    MensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    MensajeDocumento.document = new document();

                                    MensajeDocumento.document.caption = DTO.WaCaption;
                                    MensajeDocumento.document.link = DTO.WaLink;
                                    MensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeDocumento);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeDocumento);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(Resultado);

                            var MensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle
                            {
                                IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                                EnviadoCorrectamente = true,
                                MensajeError = "",
                                IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                                IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                                Mensaje = DTO.WaCaption,
                                WhatsAppId = datoRespuesta.messages[0].id,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "Envio-Pre",
                                UsuarioModificacion = "Envio-Pre"
                            };

                            alumnoMensaje.Validado = true;

                            try
                            {
                                _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(MensajeEnviado);

                                var ActualizarRegistroPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.FirstById(alumnoMensaje.IdPre);
                                if (ActualizarRegistroPre != null)
                                {
                                    ActualizarRegistroPre.FechaModificacion = DateTime.Now;
                                    ActualizarRegistroPre.Procesado = true;
                                    ActualizarRegistroPre.MensajeProceso = "Procesado";
                                    _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.Update(ActualizarRegistroPre);
                                }
                                _unitOfWork.Commit();
                            }
                            catch (Exception ex1)
                            {
                                try
                                {
                                    var mensajeCompleto = $"{ex1.Message}-{(ex1.InnerException != null ? ex1.InnerException.Message : "No contiene InnerException")}";

                                    _unitOfWork.LogRepository.Add(new Log
                                    {
                                        Ip = "-",
                                        Usuario = "-",
                                        Maquina = "-",
                                        Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                        Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                        Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                        Excepcion = ex1.ToString().Length > 2500 ? ex1.ToString().Substring(0, 2500) : ex1.ToString(),
                                        Tipo = "SEND",
                                        IdPadre = 0,
                                        UsuarioCreacion = "gmiranda",
                                        UsuarioModificacion = "gmiranda",
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    });
                                    _unitOfWork.Commit();
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        else
                        {
                            var MensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle
                            {
                                IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                                EnviadoCorrectamente = false,
                                MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.",
                                IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                                IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                                ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "Envio-Pre",
                                UsuarioModificacion = "Envio-Pre"
                            };

                            alumnoMensaje.Validado = false;

                            try
                            {
                                var _actualizarRegistroPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.FirstById(alumnoMensaje.IdPre);
                                if (_actualizarRegistroPre != null)
                                {
                                    _actualizarRegistroPre.FechaModificacion = DateTime.Now;
                                    _actualizarRegistroPre.Procesado = false;
                                    _actualizarRegistroPre.MensajeProceso = "Mensaje no enviado / Error en credenciales";
                                    _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.Update(_actualizarRegistroPre);
                                    _unitOfWork.Commit();
                                }
                                _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(MensajeEnviado);
                                _unitOfWork.Commit();
                            }
                            catch (Exception ex2)
                            {
                                try
                                {
                                    var mensajeCompleto = $"{ex2.Message}-{(ex2.InnerException != null ? ex2.InnerException.Message : "No contiene InnerException")}";

                                    _unitOfWork.LogRepository.Insert(new TLog
                                    {
                                        Ip = "-",
                                        Usuario = "-",
                                        Maquina = "-",
                                        Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                        Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                        Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                        Excepcion = ex2.ToString().Length > 2500 ? ex2.ToString().Substring(0, 2500) : ex2.ToString(),
                                        Tipo = "SEND",
                                        IdPadre = 0,
                                        UsuarioCreacion = "gmiranda",
                                        UsuarioModificacion = "gmiranda",
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    });
                                    _unitOfWork.Commit();
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        //sigue el flujo y alerta a los seleccionados para revision manual
                        if (envioDuplicado == 1)
                        {
                            contaDuplicadosVerificados++;
                            if (contaDuplicadosVerificados >= 300)
                            {
                                contaDuplicadosVerificados = 0;
                                List<string> correosAlerta = new List<string>();
                                correosAlerta.Add("emayta@bsginstitute.com");
                                TMK_MailService mailServiceAlerta = new TMK_MailService();
                                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                mailDataAlerta.Subject = "***** ALERTA *****-Riesgo Bajo: Se van controlando mas de 300 mensajes duplciados idlog:" + IdWhatsAppConfiguracionLogEjecucion;
                                mailDataAlerta.Message = "RIESGO BAJO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + "Ejecutar el siguiente SP: mkt.SP_SeguimientoDeEnviosDuplicados [Colocar id de la campania general]" + "<br/>" + "Asi mismo ejecutar el sp de salida de mensajes de whatsapp: mkt.SP_ControlDeSalidaDeMensajesWhatsApp [enviar el ID pais]";
                                mailDataAlerta.Cc = string.Empty;
                                mailDataAlerta.Bcc = string.Empty;
                                mailDataAlerta.AttachedFiles = null;
                                mailServiceAlerta.SetData(mailDataAlerta);
                                mailServiceAlerta.SendMessageTask();
                            }
                        }


                        var MensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle
                        {
                            IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                            EnviadoCorrectamente = false,
                            MensajeError = "Control Envio Duplicado",
                            IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                            IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                            ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "Envio-Pre",
                            UsuarioModificacion = "Envio-Pre"
                        };

                        alumnoMensaje.Validado = false;

                        try
                        {
                            var _actualizarRegistroPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.FirstById(alumnoMensaje.IdPre);
                            if (_actualizarRegistroPre != null)
                            {
                                _actualizarRegistroPre.FechaModificacion = DateTime.Now;
                                _actualizarRegistroPre.UsuarioModificacion = MensajeEnviado.MensajeError;
                                _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.Update(_actualizarRegistroPre);
                                _unitOfWork.Commit();
                            }
                            _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(MensajeEnviado);
                            _unitOfWork.Commit();
                        }
                        catch (Exception ex3)
                        {
                            try
                            {
                                var mensajeCompleto = $"{ex3.Message}-{(ex3.InnerException != null ? ex3.InnerException.Message : "No contiene InnerException")}";

                                _unitOfWork.LogRepository.Insert(new TLog
                                {
                                    Ip = "-",
                                    Usuario = "-",
                                    Maquina = "-",
                                    Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                    Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                    Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                    Excepcion = ex3.ToString().Length > 2500 ? ex3.ToString().Substring(0, 2500) : ex3.ToString(),
                                    Tipo = "SEND",
                                    IdPadre = 0,
                                    UsuarioCreacion = "gmiranda",
                                    UsuarioModificacion = "gmiranda",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                });
                                _unitOfWork.Commit();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    cantidadErrorEnvioActual++;

                    try
                    {
                        var mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle
                        {
                            IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                            EnviadoCorrectamente = false,
                            MensajeError = e.ToString(),
                            IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                            IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                            ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "Envio-Pre",
                            UsuarioModificacion = "Envio-Pre"
                        };

                        alumnoMensaje.Validado = false;

                        try
                        {
                            var ActualizarRegistroPre = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.FirstById(alumnoMensaje.IdPre);

                            if (ActualizarRegistroPre != null)
                            {
                                ActualizarRegistroPre.FechaModificacion = DateTime.Now;
                                ActualizarRegistroPre.Procesado = false;
                                ActualizarRegistroPre.MensajeProceso = "Error";
                                _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.Update(ActualizarRegistroPre);
                                _unitOfWork.Commit();
                            }

                            _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                            _unitOfWork.Commit();
                        }
                        catch (Exception ex)
                        {
                            var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                            _unitOfWork.LogRepository.Insert(new TLog
                            {
                                Ip = "-",
                                Usuario = "WhatsAppMasivo",
                                Maquina = "-",
                                Ruta = "InsertarWhatsappEnvioDetalle/ActualizarWhatsappPreEnvio4",
                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                Tipo = "SEND",
                                IdPadre = 0,
                                UsuarioCreacion = "CampaniaGeneral",
                                UsuarioModificacion = "CampaniaGeneral",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            });
                            _unitOfWork.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                try
                {
                    // Valor por defecto es false para seguir el flujo normal
                    if (!aplicarLimiteEspera) Thread.Sleep(1500);

                    // Espera opcional
                    if (aplicarLimiteEspera && cantidadGrupoActual >= LIMITE_CANTIDAD_ESPERA)
                    {
                        cantidadGrupoActual = 0;
                        Thread.Sleep(1500);
                    }
                }
                catch (Exception ex)
                {

                    try
                    {
                        var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                        _unitOfWork.LogRepository.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "-",
                            Maquina = "-",
                            Ruta = "EnviarWhatsAppCatch",
                            Parametros = $"{IdWhatsAppConfiguracionLogEjecucion}/{alumnoMensaje.IdConjuntoListaResultado}/{alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                            Tipo = "SEND",
                            IdPadre = 0,
                            UsuarioCreacion = string.Empty,
                            UsuarioModificacion = string.Empty,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    catch (Exception)
                    {
                    }
                }

                if (cantidadErrorEnvioActual >= LIMITE_CANTIDAD_ERROR_ENVIO)
                {
                    try
                    {
                        _unitOfWork.LogRepository.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "WhatsAppMasivo",
                            Maquina = "-",
                            Ruta = "CantidadMaximaPermitida",
                            Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                            Mensaje = $"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}",
                            Excepcion = $"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}",
                            Tipo = "SEND",
                            IdPadre = 0,
                            UsuarioCreacion = "CampaniaGeneral",
                            UsuarioModificacion = "CampaniaGeneral",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    catch (Exception)
                    {
                    }

                    throw new Exception($"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}");
                }
            }

            return RegistroEstadoEnvio;
        }

        public TRegistroRecuperacionWhatsApp Update(TRegistroRecuperacionWhatsApp entidad)
        {
            try
            {
                return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Update(_mapper.Map<RegistroRecuperacionWhatsAppDTO>(entidad));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TRegistroRecuperacionWhatsApp FirstBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            try
            {
                return _unitOfWork.RegistroRecuperacionWhatsAppRepository.FirstBy(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string EjecutarCampaniaGeneralEnvioWhatsApp()
        {
            try
            {

                var listaWhatsApp = _unitOfWork.CampaniaGeneralRepository.ObtenerActividadCampaniaGeneralParaEjecutar();

                if (listaWhatsApp.Any())
                {
                    bool insercionRegistroExitosa = true;
                    bool obtencionPreResultadoExitosa = true;
                    bool insercionLogEjecucionExitosa = true;
                    bool actualizacionLogEjecucionExitosa = true;
                    bool actualizacionRecuperacionWhatsApp = true;
                    bool actualizacionEstadoEnvioWhatsApp = true;
                    bool actualizacionIntentoFallidoWhatsApp = true;
                    int nroActualIntentosRegistroSeguimiento = 0;
                    int nroActualIntentosObtencionListaPreProcesada = 0;
                    int nroActualIntentosInsercionLogEjecucion = 0;
                    int nroActualIntentosActualizacionLog = 0;
                    int nroActualIntentosActualizacionRecuperacion = 0;
                    int nroActualIntentosActualizacionEstadoEnvio = 0;
                    int nroActualIntentosFallidosActualizacion = 0;

                    #region InsercionRegistroSeguimientoRecuperacion
                    do
                    {
                        try
                        {
                            var registroSeguimientoRecuperacion = listaWhatsApp.Select(s => new RegistroRecuperacionWhatsAppDTO()
                            {
                                IdCampaniaGeneral = s.IdCampaniaGeneral,
                                IdCampaniaGeneralDetalle = s.IdCampaniaGeneralDetalle,
                                IdCampaniaGeneralDetalleResponsable = s.IdCampaniaGeneralDetalleResponsable,
                                IdPersonal = s.IdPersonal,
                                IdPlantilla = s.IdPlantilla,
                                IdWhatsAppConfiguracionEnvio = s.IdWhatsAppConfiguracionEnvio,
                                Dia = s.Dia,
                                Dia1 = s.Dia1,
                                Dia2 = s.Dia2,
                                Dia3 = s.Dia3,
                                Dia4 = s.Dia4,
                                Dia5 = s.Dia5,
                                FechaInicioEnvioWhatsapp = s.FechaInicioEnvioWhatsapp,
                                FechaFinEnvioWhatsapp = s.FechaFinEnvioWhatsapp,
                                HoraEnvio = s.HoraEnvio,
                                Completado = false,
                                Fallido = false,
                                Estado = true,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }).ToList();

                            using (TransactionScope scope = new TransactionScope())
                            {
                                _unitOfWork.RegistroRecuperacionWhatsAppRepository.Insert(registroSeguimientoRecuperacion);
                                _unitOfWork.Commit();
                                scope.Complete();
                            }

                            insercionRegistroExitosa = true;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                _unitOfWork.LogRepository.Insert(new TLog
                                {
                                    Ip = "-",
                                    Usuario = "-",
                                    Maquina = "-",
                                    Ruta = "InsercionRegistroRecuperacionWhatsApp",
                                    Parametros = $"IdCampaniaGeneral={listaWhatsApp.Select(s => s.IdCampaniaGeneral).Distinct().ToList()}/IdCampaniaGeneralDetalle={listaWhatsApp.Select(s => s.IdCampaniaGeneralDetalle).Distinct().ToList()}/IdCampaniaGeneralDetalleResponsable={listaWhatsApp.Select(s => s.IdCampaniaGeneralDetalleResponsable).Distinct().ToList()}/IdWhatsAppConfiguracionEnvio={listaWhatsApp.Select(s => s.IdWhatsAppConfiguracionEnvio).Distinct().ToList()}",
                                    Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                    Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                    Tipo = "INSERT",
                                    IdPadre = 0,
                                    UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                    UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                });
                                _unitOfWork.Commit();
                            }
                            catch (Exception)
                            {
                            }

                            insercionRegistroExitosa = false;
                            nroActualIntentosRegistroSeguimiento++;
                            Thread.Sleep(2000);

                            if (nroActualIntentosRegistroSeguimiento >= NRO_MAXIMO_INTENTOS)
                            {
                                throw new Exception("Supero el limite de registroSeguimientoRecuperacion");
                            }
                        }
                    } while (!insercionRegistroExitosa && nroActualIntentosRegistroSeguimiento < NRO_MAXIMO_INTENTOS);
                    #endregion

                    /* Va interar de prioridad en prioridad hasta enviar una por una sus contactos */
                    foreach (var campaniaGeneralWhatsApp in listaWhatsApp)
                    {
                        try
                        {
                            var resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                            int cantidad;

                            switch (campaniaGeneralWhatsApp.Dia)
                            {
                                case 1:
                                    cantidad = campaniaGeneralWhatsApp.Dia1;
                                    break;
                                case 2:
                                    cantidad = campaniaGeneralWhatsApp.Dia2;
                                    break;
                                case 3:
                                    cantidad = campaniaGeneralWhatsApp.Dia3;
                                    break;
                                case 4:
                                    cantidad = campaniaGeneralWhatsApp.Dia4;
                                    break;
                                case 5:
                                    cantidad = campaniaGeneralWhatsApp.Dia5;
                                    break;
                                default:
                                    cantidad = 0;
                                    break;
                            }

                            var respuesta = new List<WhatsAppConfiguracionPreEnvioDTO>();

                            #region Obtencion Prerresultado
                            do
                            {
                                try
                                {
                                    respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(cantidad, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdPersonal).Where(w => w.Celular != "1").ToList();

                                    obtencionPreResultadoExitosa = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _unitOfWork.LogRepository.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ObtencionPreEnvioWhatsApp",
                                            Parametros = $"Cantidad={cantidad}/IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdPersonal={campaniaGeneralWhatsApp.IdPersonal}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "GET",
                                            IdPadre = 0,
                                            UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                            UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    obtencionPreResultadoExitosa = false;
                                    nroActualIntentosObtencionListaPreProcesada++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosObtencionListaPreProcesada >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Supero el limite de obtencionPreEnvioWhatsApp");
                                    }
                                }
                            } while (!obtencionPreResultadoExitosa && nroActualIntentosObtencionListaPreProcesada < NRO_MAXIMO_INTENTOS);
                            #endregion

                            /* Inicio ejecucion envio */
                            if (respuesta.Any())
                            {
                                var logEjecucion = new WhatsAppConfiguracionLogEjecucion();

                                #region Guardado de log de ejecucion
                                do
                                {
                                    try
                                    {
                                        //*---- Verificar si existe otro log antes de crearlo
                                        int flagLogDuplicado = 1;

                                        try
                                        {
                                            int idLogActivoParaDeleteLogico = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.obtenerOtrosLogActivos(campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio);
                                            if (idLogActivoParaDeleteLogico != 0)
                                            {
                                                try
                                                {
                                                    var logEjecucionFinal_duplicado = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(idLogActivoParaDeleteLogico);

                                                    logEjecucionFinal_duplicado.FechaFin = DateTime.Now;
                                                    logEjecucionFinal_duplicado.UsuarioModificacion = "Control-Log-Duplicado";
                                                    logEjecucionFinal_duplicado.Estado = false;
                                                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal_duplicado);
                                                    _unitOfWork.Commit();
                                                    //*--InsertarLogDuplicado
                                                    logEjecucion.FechaInicio = DateTime.Now;
                                                    logEjecucion.IdWhatsAppConfiguracionEnvio = campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio;
                                                    logEjecucion.Estado = true;
                                                    logEjecucion.FechaCreacion = DateTime.Now;
                                                    logEjecucion.FechaModificacion = DateTime.Now;
                                                    logEjecucion.UsuarioCreacion = "Inserccion-Log-Duplicado";
                                                    logEjecucion.UsuarioModificacion = "Inserccion-Log-Duplicado";

                                                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecucion);
                                                    _unitOfWork.Commit();


                                                    //*--- realizar recalculo
                                                    respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(cantidad, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdPersonal).Where(w => w.Celular != "1").ToList();
                                                    flagLogDuplicado = 2;

                                                    insercionLogEjecucionExitosa = true;
                                                }
                                                catch
                                                {
                                                    //*---- mensaje si no se puede eliminar, insertar o hacer el recalculo
                                                    List<string> correosAlerta = new List<string>();
                                                    correosAlerta.Add("sistemas@bsginstitute.com");
                                                    TMK_MailService mailServiceAlerta = new TMK_MailService();
                                                    TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                                    mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                                    mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                                    mailDataAlerta.Subject = "!!!! ALERTA : RIESGO MUY ALTO DE ENVIO DUPLICADO: No se pudo realizar recalculo para la campania " + campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio + " contactos del envio masivo programado";
                                                    mailDataAlerta.Message = "RIESGO MUY ALTO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + " Se encontro log activo, pero no se pudo eliminar el id es: " + idLogActivoParaDeleteLogico;
                                                    mailDataAlerta.Cc = string.Empty;
                                                    mailDataAlerta.Bcc = string.Empty;
                                                    mailDataAlerta.AttachedFiles = null;
                                                    mailServiceAlerta.SetData(mailDataAlerta);
                                                    mailServiceAlerta.SendMessageTask();
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            List<string> correosAlerta = new List<string>();
                                            correosAlerta.Add("emayta@bsginstitute.com");
                                            TMK_MailService mailServiceAlerta = new TMK_MailService();
                                            TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                                            mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                                            mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                                            mailDataAlerta.Subject = "!!!! ALERTA : RIESGO MUY ALTO: No se pudo verificar duplicidad en idwhatsappconfiguracion: " + campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio;
                                            mailDataAlerta.Message = "RIESGO  ALTO: se recomienda realizar un seguimiento manual del envio " + " <br/>" + " No se pudo hacer la verificacion de log activo ";
                                            mailDataAlerta.Cc = string.Empty;
                                            mailDataAlerta.Bcc = string.Empty;
                                            mailDataAlerta.AttachedFiles = null;
                                            mailServiceAlerta.SetData(mailDataAlerta);
                                            mailServiceAlerta.SendMessageTask();
                                        }
                                        //*---- FIN de Verificar si existe otro log antes de crearlo
                                        if (flagLogDuplicado == 1)
                                        {
                                            logEjecucion.FechaInicio = DateTime.Now;
                                            logEjecucion.IdWhatsAppConfiguracionEnvio = campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio;
                                            logEjecucion.Estado = true;
                                            logEjecucion.FechaCreacion = DateTime.Now;
                                            logEjecucion.FechaModificacion = DateTime.Now;
                                            logEjecucion.UsuarioCreacion = "Pre-ProcesoAutomatico-CampaniaGeneral";
                                            logEjecucion.UsuarioModificacion = "Pre-ProcesoAutomatico-CampaniaGeneral";
                                            _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecucion);
                                            _unitOfWork.Commit();

                                            insercionLogEjecucionExitosa = true;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "InsercionLogEjecucionWhatsApp",
                                                Parametros = $"FechaInicio={logEjecucion.FechaInicio}/IdWhatsAppConfiguracionEnvio={logEjecucion.IdWhatsAppConfiguracionEnvio}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "INSERT",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        insercionLogEjecucionExitosa = false;
                                        nroActualIntentosInsercionLogEjecucion++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosInsercionLogEjecucion >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de guardadoLogEjecucion");
                                        }
                                    }
                                } while (!insercionLogEjecucionExitosa && nroActualIntentosInsercionLogEjecucion < NRO_MAXIMO_INTENTOS);
                                #endregion

                                foreach (var item in respuesta)
                                {
                                    var objetoWhatsApp = new WhatsAppResultadoConjuntoListaDTO();

                                    try
                                    {
                                        objetoWhatsApp.IdPre = item.Id;
                                        objetoWhatsApp.IdConjuntoListaResultado = 1;
                                        objetoWhatsApp.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                                        objetoWhatsApp.IdAlumno = item.IdAlumno;
                                        objetoWhatsApp.Celular = item.Celular;
                                        objetoWhatsApp.IdCodigoPais = item.IdCodigoPais;
                                        objetoWhatsApp.NroEjecucion = item.NroEjecucion;
                                        objetoWhatsApp.Validado = item.Validado;
                                        objetoWhatsApp.Plantilla = item.Plantilla;
                                        objetoWhatsApp.IdPersonal = item.IdPersonal;
                                        objetoWhatsApp.IdPgeneral = item.IdPgeneral;
                                        objetoWhatsApp.IdPlantilla = item.IdPlantilla;

                                        if (!string.IsNullOrEmpty(item.objetoplantilla))
                                        {
                                            objetoWhatsApp.objetoplantilla = JsonConvert.DeserializeObject<List<DatoPlantillaWhatsAppDTO>>(item.objetoplantilla);
                                            resultado.Add(objetoWhatsApp);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "MapeoObjetoWhatsApp",
                                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={objetoWhatsApp.IdPre}/IdPrioridadMailChimpListaCorreo={objetoWhatsApp.IdPrioridadMailChimpListaCorreo}/IdPersonal={objetoWhatsApp.IdPersonal}/IdPGeneral={objetoWhatsApp.IdPgeneral}/IdPlantilla={objetoWhatsApp.IdPlantilla}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "MAP",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }

                                var EnvioMensajes = EnvioAutomaticoPlantillaMasivo(resultado, campaniaGeneralWhatsApp.IdPersonal, campaniaGeneralWhatsApp.IdPlantilla, logEjecucion.Id, true);

                                #region Actualizacion de logEjecucion Final
                                do
                                {
                                    try
                                    {
                                        var logEjecucionFinal = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(logEjecucion.Id);

                                        logEjecucionFinal.FechaFin = DateTime.Now;
                                        _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal);
                                        _unitOfWork.Commit();
                                        actualizacionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionLogEjecucionWhatsApp",
                                                Parametros = string.Empty,
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionLogEjecucionExitosa = false;
                                        nroActualIntentosActualizacionLog++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionLog >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionLogEjecucionWhatsApp");
                                        }
                                    }
                                } while (!actualizacionLogEjecucionExitosa && nroActualIntentosActualizacionLog < NRO_MAXIMO_INTENTOS);
                                #endregion

                                #region Actualizar estado envio WhatsApp
                                do
                                {
                                    try
                                    {
                                        _unitOfWork.CampaniaGeneralRepository.ActualizarEstadoEnvioWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneral);
                                        _unitOfWork.Commit();
                                        actualizacionEstadoEnvioWhatsApp = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionEstadoEnvioWhatsApp",
                                                Parametros = $"IdCampaniaGeneral={campaniaGeneralWhatsApp.IdCampaniaGeneral}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            _unitOfWork.Commit();
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionEstadoEnvioWhatsApp = false;
                                        nroActualIntentosActualizacionEstadoEnvio++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionEstadoEnvio >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionEstadoEnvioWhatsApp");
                                        }
                                    }
                                } while (!actualizacionEstadoEnvioWhatsApp && nroActualIntentosActualizacionEstadoEnvio < NRO_MAXIMO_INTENTOS);
                                #endregion
                            }
                            /* Fin ejecucion envio */

                            #region Actualizacion de registro WhatsApp recuperacion
                            do
                            {
                                try
                                {
                                    _unitOfWork.RegistroRecuperacionWhatsAppRepository.ActualizarCompletadoRegistroWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable);
                                    _unitOfWork.Commit();
                                    actualizacionRecuperacionWhatsApp = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _unitOfWork.LogRepository.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ActualizacionRecuperacionWhatsApp",
                                            Parametros = $"IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdCampaniaGeneralDetalleResponsable={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "UPDATE",
                                            IdPadre = 0,
                                            UsuarioCreacion = string.Empty,
                                            UsuarioModificacion = string.Empty,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    actualizacionRecuperacionWhatsApp = false;
                                    nroActualIntentosActualizacionRecuperacion++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosActualizacionRecuperacion >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Error Envio WhatsApp");
                                    }
                                }
                            } while (!actualizacionRecuperacionWhatsApp && nroActualIntentosActualizacionRecuperacion < NRO_MAXIMO_INTENTOS);
                            #endregion
                        }
                        catch (Exception exx)
                        {
                            do
                            {
                                try
                                {
                                    _unitOfWork.RegistroRecuperacionWhatsAppRepository.ActualizarFalloRegistroWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable);

                                    actualizacionIntentoFallidoWhatsApp = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _unitOfWork.LogRepository.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ActualizacionFalloRecuperacionWhatsApp",
                                            Parametros = $"IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdCampaniaGeneralDetalleResponsable={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "UPDATE",
                                            IdPadre = 0,
                                            UsuarioCreacion = string.Empty,
                                            UsuarioModificacion = string.Empty,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    nroActualIntentosFallidosActualizacion++;
                                    actualizacionIntentoFallidoWhatsApp = false;
                                    Thread.Sleep(2000);
                                }
                            } while (!actualizacionIntentoFallidoWhatsApp && nroActualIntentosFallidosActualizacion < NRO_MAXIMO_INTENTOS);
                        }
                    }
                    /* Va interar de prioridad en prioridad hasta enviar una por una sus contactos */
                }

                return "true";

            }
            catch (Exception e)
            {

                _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "EnviarWhatsApp", Parametros = $"", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                _unitOfWork.Commit();
                throw new Exception(e.Message);
            }
        }
        public RegistroWhatsAppConfiguracionPreEnvioDTO VisualizacionListasWhatsAppEnvioAutomaticoMasivo(List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            try
            {
                RegistroWhatsAppConfiguracionPreEnvioDTO Registro = new RegistroWhatsAppConfiguracionPreEnvioDTO();
                Registro.ListaPreConfigurados = new List<VistaWhatsAppConfiguracionPreEnvioDTO>();
                foreach (var item in ListasWhatsApp)
                {
                    var Respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.ListasVisualizarWhatsAppEnvioAutomaticoMasivoPreProcesada(item.IdConjuntoListaDetalle);

                    if (Respuesta != null && Respuesta.Count > 0)
                    {
                        Registro.NumeroValidos = Registro.NumeroValidos + Respuesta.Where(x => x.Validado == true).Count();
                        Registro.NumerosNoValidos = Registro.NumerosNoValidos + Respuesta.Where(x => x.Validado == false).Count();
                        Registro.ListaPreConfigurados.AddRange(Respuesta);
                    }
                }
                return Registro;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public RegistroSeguimientoPreProcesoListaWhatsAppDTO EstadoSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista)
        {

            try
            {
                var Respuesta = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.RegistroSeguimientoPreProcesoListaWhatsApp(IdConjuntoLista);

                if (Respuesta != null)
                {
                    return Respuesta;
                }
                else
                {
                    RegistroSeguimientoPreProcesoListaWhatsAppDTO SinDato = new RegistroSeguimientoPreProcesoListaWhatsAppDTO();
                    SinDato.IdEstadoSeguimientoPreProcesoListaWhatsApp = 1;
                    return SinDato;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EnvioMasivoReasignacionesOperaciones()
        {
            try
            {

                WhatsAppConfiguracionLogEjecucion logEjecion = new WhatsAppConfiguracionLogEjecucion();
                try
                {
                    logEjecion.FechaInicio = DateTime.Now;
                    logEjecion.IdWhatsAppConfiguracionEnvio = 1;
                    logEjecion.Estado = true;
                    logEjecion.FechaCreacion = DateTime.Now;
                    logEjecion.FechaModificacion = DateTime.Now;
                    logEjecion.UsuarioCreacion = "EnvioOperaciones";
                    logEjecion.UsuarioModificacion = "EnvioOperaciones";
                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecion);
                    _unitOfWork.Commit();

                    var Respuesta = _unitOfWork.ConjuntoListaResultadoRepository.ObtenerOportunidadesReasignadasOperaciones();
                    ValidarNumeroConjuntoOperaciones(Respuesta, 1);
                    Respuesta = Respuesta.Where(w => w.Validado == true).ToList();

                    RemplazarEtiquetasOperaciones(Respuesta);
                    Respuesta = Respuesta.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();

                    EnvioAutomaticoPlantillaOperaciones(Respuesta, logEjecion.Id);

                    var logEjecucionFinal = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(logEjecion.Id);
                    logEjecucionFinal.FechaFin = DateTime.Now;
                    _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(logEjecucionFinal);
                    _unitOfWork.Commit();


                }
                catch (Exception ex)
                {
                    try
                    {
                        if (logEjecion.Id == 0 || logEjecion.Id == null)
                        {
                            logEjecion.FechaFin = DateTime.Now;
                            _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(logEjecion);
                            _unitOfWork.Commit();
                        }
                    }
                    catch (Exception e)
                    {

                    }


                }
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void ValidarNumeroConjuntoOperaciones(List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var Alumno in NumerosValidados)
            {
                TWhatsAppMensajePublicidad whatsAppMensajePublicidad = new TWhatsAppMensajePublicidad();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + Alumno.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };


                    var _credencialesHost = new WhatsAppConfiguracionService(_unitOfWork).ObtenerCredencialHost(Alumno.IdCodigoPais);
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(Alumno.IdPersonal ?? default(int), Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(Alumno.IdPersonal ?? default(int));

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
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(modelCredencial);
                                _unitOfWork.Commit();

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

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                Alumno.Validado = false;
                            }
                            else
                            {
                                Alumno.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        _unitOfWork.WhatsAppMensajePublicidadRepository.Insert(whatsAppMensajePublicidad);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        Alumno.Validado = false;

                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        _unitOfWork.WhatsAppMensajePublicidadRepository.Insert(whatsAppMensajePublicidad);
                        _unitOfWork.Commit();
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + Alumno.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }
        }
        public void RemplazarEtiquetasOperaciones(List<WhatsAppResultadoConjuntoListaDTO> NumeroAlumno)
        {
            string plantilla = string.Empty;
            string valor = string.Empty;
            string Numero = "";
            //PlantillaPwBO plantillaPw = new PlantillaPwBO();

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                try
                {
                    AlumnoEtiqueta.objetoplantilla = new List<DatoPlantillaWhatsAppDTO>();

                    Numero = AlumnoEtiqueta.Celular;
                    if (Numero.StartsWith("51"))
                    {
                        Numero = Numero.Substring(2, 9);
                    }
                    else if (Numero.StartsWith("57"))
                    {
                        Numero = "00" + Numero;
                    }
                    else if (Numero.StartsWith("591"))
                    {
                        Numero = "00" + Numero;
                    }
                    else
                    {

                    }
                    var Alumno = _unitOfWork.AlumnoRepository.FirstBy(w => w.Celular.Contains(Numero) && w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                    //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                    var Asesor = _unitOfWork.PersonalRepository.ObtenerDatoPersonal(AlumnoEtiqueta.IdPersonal ?? default(int));



                    plantilla = _unitOfWork.PlantillaClaveValorRepository.GetBy(w => w.Estado == true && w.IdPlantilla == AlumnoEtiqueta.IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                    PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();

                    rpta = _unitOfWork.CentroCostoRepository.ObtenerRemplazoPlantilla(AlumnoEtiqueta.IdPgeneral ?? default(int));
                    if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                    {
                        fecha = _unitOfWork.PGeneralRepository.ObtenerFechaInicioProgramaGeneral(AlumnoEtiqueta.IdPgeneral ?? default(int), AlumnoEtiqueta.IdCodigoPais);

                        if (fecha.Count > 0)
                        {
                            FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                            if (FechaInicioPrograma == null)
                            {
                                FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                            }
                        }
                    }
                    //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);



                    foreach (string word in plantilla.Split('{'))
                    {
                        DatoPlantillaWhatsAppDTO plantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                        if (word.Contains('}'))
                        {
                            string etiqueta = word.Split('}')[0];
                            //Separamos solo los Id´s

                            if (etiqueta.Contains("tPartner.nombre"))
                            {

                                valor = rpta.NombrePartner;
                            }
                            else if (etiqueta.Contains("tPEspecifico.nombre"))
                            {
                                valor = rpta.NombrePEspecifico;
                            }
                            else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                            {
                                valor = rpta.NombrePGeneral;
                            }

                            if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    CultureInfo ci = new CultureInfo("es-ES");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.Day.ToString();
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    //CultureInfo ci = new CultureInfo("es-Es");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            if (etiqueta.Contains("Template"))
                            {

                                valor = "";
                            }
                            else
                            {

                                if ((etiqueta == "tPersonal.Nombre1" || etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && AlumnoEtiqueta.IdPersonal > 0)
                                {
                                    switch (etiqueta)
                                    {
                                        case "tPersonal.PrimerNombreApellidoPaterno":
                                            valor = Asesor.PrimerNombreApellidoPaterno; break;
                                        case "tPersonal.email":
                                            valor = Asesor.Email; break;
                                        case "tPersonal.Nombre1":
                                            valor = Asesor.Nombre1; break;
                                        case "tPersonal.nombres":
                                            valor = Asesor.Nombres; break;
                                        case "tPersonal.apellidos":
                                            valor = Asesor.Apellidos; break;
                                        case "tPersonal.Telefono":
                                            {
                                                if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                {
                                                    valor = Asesor.MovilReferencia;
                                                }
                                                else
                                                {
                                                    if (Asesor.Central == "192.168.0.20" || Asesor.Central == "192.168.2.20")
                                                    {
                                                        //Arequipa //lima
                                                        valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.3.20")
                                                    {
                                                        //bogota
                                                        valor = "57 (601) 381 9462 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.4.20")
                                                    {
                                                        //cd mexico
                                                        valor = "52 (55) 4000 3255 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else if (Asesor.Central == "192.168.5.20")
                                                    {
                                                        //santiago
                                                        valor = "56 (2) 2760 9120 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        valor = "No registra central asignada";
                                                    }
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apepaterno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoPaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apematerno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoMaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre1":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre1;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre2":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre2;
                                                }
                                            }
                                            break;
                                        default:
                                            valor = ""; break;
                                    }

                                }
                            }
                            if (valor != null)
                            {
                                valor = valor.Replace("#$%", "<br>");
                                plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.Texto = valor;

                            }
                            else
                            {
                                plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.Texto = "";
                            }
                            AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                        }
                    }
                    AlumnoEtiqueta.Plantilla = plantilla;
                    //return Ok(new { plantilla, objetoplantilla });
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailService Mailservice = new TMK_MailService();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error Proceso Plantillas";
                    mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }
        public void EnvioAutomaticoPlantillaOperaciones(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(MensajeAlumno[0].IdPlantilla ?? default(int));
            foreach (var AlumnoMensaje in MensajeAlumno)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = AlumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = AlumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = AlumnoMensaje.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionService _repCredenciales = new WhatsAppConfiguracionService(_unitOfWork);
                    WhatsAppUsuarioCredencialService _repTokenUsuario = new WhatsAppUsuarioCredencialService(_unitOfWork);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(AlumnoMensaje.IdPersonal ?? default(int), AlumnoMensaje.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(AlumnoMensaje.IdPersonal ?? default(int));

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "hsm";

                                MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = DTO.WaType;
                                _mensajePlantilla.hsm = new hsm();

                                _mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.hsm.element_name = DTO.WaBody;

                                _mensajePlantilla.hsm.language = new language();
                                _mensajePlantilla.hsm.language.policy = "deterministic";
                                _mensajePlantilla.hsm.language.code = "es";

                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        localizable_params _dato = new localizable_params();
                                        _dato.@default = listaDatos.Texto;

                                        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                    }
                                }

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                        TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = true;
                        mensajeEnviado.MensajeError = "";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.Mensaje = DTO.WaCaption;
                        mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Operaciones";
                        mensajeEnviado.UsuarioModificacion = "Operaciones";

                        _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                        _unitOfWork.Commit();

                        //return Ok(mensajeEnviado.WaId);
                    }
                    else
                    {
                        TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Operaciones";
                        mensajeEnviado.UsuarioModificacion = "Operaciones";
                        _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                        _unitOfWork.Commit();
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                }
                catch (Exception ex)
                {
                    TWhatsAppConfiguracionEnvioDetalle mensajeEnviado = new TWhatsAppConfiguracionEnvioDetalle();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "Envio";
                    mensajeEnviado.UsuarioModificacion = "Envio";
                    _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Insert(mensajeEnviado);
                    _unitOfWork.Commit();
                }

                System.Threading.Thread.Sleep(5000);
            }

        }
        public bool RegularizarPlantilla(int IdWhatsAppConfiguracionEnvio)
        {
            try
            {
                if (IdWhatsAppConfiguracionEnvio != 0)
                {
                }
                else
                {
                    string plantilla = string.Empty;
                    string plantilla0 = string.Empty;
                    string valor = string.Empty;
                    string Numero = "";
                    //var Alumno = new object();
                    int IdPersonalProceso = 0;
                    int IdPlantillaProceso = 0;
                    int IdPgeneralProceso = 0;
                    int IdPaisProceso = 0;
                    int IdPpgeneralPlantillaProceso = 0;

                    PersonalDTO Asesor = new PersonalDTO();
                    PlantillaCentroCostoDTO rpta0 = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma0 = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha0 = new List<ModalidadProgramaDTO>();

                    var NumeroAlumno = _unitOfWork.ConjuntoListaResultadoRepository.ObtenerEnvioSinMensaje();
                    foreach (var AlumnoEtiqueta in NumeroAlumno)
                    {
                        try
                        {
                            //AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

                            Numero = AlumnoEtiqueta.Celular;
                            if (Numero.StartsWith("51"))
                            {
                                Numero = Numero.Substring(2, 9);
                            }
                            else if (Numero.StartsWith("57"))
                            {
                                Numero = "00" + Numero;
                            }
                            else if (Numero.StartsWith("591"))
                            {
                                Numero = "00" + Numero;
                            }
                            else
                            {

                            }

                            var Alumno = _unitOfWork.AlumnoRepository.FirstBy(w => w.Celular.Contains(Numero) && w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });

                            if (AlumnoEtiqueta.IdPersonal != IdPersonalProceso)
                            {
                                Asesor = _unitOfWork.PersonalRepository.FirstBy(w => w.Id == AlumnoEtiqueta.IdPersonal, y => new PersonalDTO { Nombres = y.Nombres, Apellidos = y.Apellidos, Anexo3Cx = y.Anexo3Cx, Central = y.Central, MovilReferencia = y.MovilReferencia });

                                IdPersonalProceso = AlumnoEtiqueta.IdPersonal;
                            }

                            if (AlumnoEtiqueta.IdPlantilla != IdPlantillaProceso)
                            {
                                plantilla0 = _unitOfWork.PlantillaClaveValorRepository.GetBy(w => w.Estado == true && w.IdPlantilla == AlumnoEtiqueta.IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;
                                plantilla = plantilla0;
                                IdPlantillaProceso = AlumnoEtiqueta.IdPlantilla;
                            }
                            else
                            {
                                plantilla = plantilla0;
                            }
                            PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                            ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                            List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();
                            //foreach (var item in ProgramaPrincipal)
                            //{
                            if (AlumnoEtiqueta.IdPgeneral != IdPgeneralProceso)
                            {
                                rpta0 = _unitOfWork.CentroCostoRepository.ObtenerRemplazoPlantilla(AlumnoEtiqueta.IdPgeneral);
                                rpta = rpta0;
                                IdPgeneralProceso = AlumnoEtiqueta.IdPgeneral;
                            }
                            else
                            {
                                rpta = rpta0;
                            }
                            if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                            {
                                if (AlumnoEtiqueta.IdPgeneral != IdPpgeneralPlantillaProceso || AlumnoEtiqueta.IdCodigoPais != IdPaisProceso)
                                {
                                    fecha0 = _unitOfWork.PGeneralRepository.ObtenerFechaInicioProgramaGeneral(AlumnoEtiqueta.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);
                                    IdPpgeneralPlantillaProceso = AlumnoEtiqueta.IdPgeneral;
                                    IdPaisProceso = AlumnoEtiqueta.IdCodigoPais;
                                    fecha = fecha0;
                                }
                                else
                                {
                                    fecha = fecha0;
                                }


                                if (fecha.Count > 0)
                                {
                                    FechaInicioPrograma0 = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL") && !w.NombreESP.Contains("Sesion Especial")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    if (FechaInicioPrograma0 == null)
                                    {
                                        FechaInicioPrograma0 = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                        FechaInicioPrograma = FechaInicioPrograma0;
                                    }
                                    else
                                    {
                                        FechaInicioPrograma = FechaInicioPrograma0;
                                    }
                                }
                            }
                            //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                            //}


                            foreach (string word in plantilla.Split('{'))
                            {
                                DatoPlantillaWhatsAppDTO plantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                                if (word.Contains('}'))
                                {
                                    string etiqueta = word.Split('}')[0];
                                    //Separamos solo los Id´s

                                    if (etiqueta.Contains("tPartner.nombre"))
                                    {

                                        valor = rpta.NombrePartner;
                                    }
                                    else if (etiqueta.Contains("tPEspecifico.nombre"))
                                    {
                                        valor = rpta.NombrePEspecifico;
                                    }
                                    else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                    {
                                        valor = rpta.NombrePGeneral;
                                    }

                                    if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                    {
                                        if (fecha.Count != 0)
                                        {
                                            CultureInfo ci = new CultureInfo("es-ES");
                                            DateTime FechaInicioetiqueta = new DateTime();
                                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                            valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                            valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                        }
                                        else
                                        {
                                            valor = "";
                                        }
                                    }
                                    else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                    {
                                        if (fecha.Count != 0)
                                        {
                                            DateTime FechaInicioetiqueta = new DateTime();
                                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                            valor = FechaInicioetiqueta.Day.ToString();
                                        }
                                        else
                                        {
                                            valor = "";
                                        }
                                    }
                                    else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                    {
                                        if (fecha.Count != 0)
                                        {
                                            //CultureInfo ci = new CultureInfo("es-Es");
                                            DateTime FechaInicioetiqueta = new DateTime();
                                            FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                            valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                            valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                        }
                                        else
                                        {
                                            valor = "";
                                        }
                                    }
                                    if (etiqueta.Contains("Template"))
                                    {

                                        valor = "";
                                    }
                                    else
                                    {

                                        if ((etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && AlumnoEtiqueta.IdPersonal > 0)
                                        {
                                            switch (etiqueta)
                                            {

                                                case "tPersonal.nombres":
                                                    valor = Asesor.Nombres; break;
                                                case "tPersonal.apellidos":
                                                    valor = Asesor.Apellidos; break;
                                                case "tPersonal.Telefono":
                                                    {
                                                        if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                        {
                                                            valor = Asesor.MovilReferencia;
                                                        }
                                                        else
                                                        {
                                                            if (Asesor.Central == "192.168.0.20" || Asesor.Central == "192.168.2.20")
                                                            {
                                                                //Arequipa //lima
                                                                valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else if (Asesor.Central == "192.168.3.20")
                                                            {
                                                                //bogota
                                                                valor = "57 (601) 381 9462 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else if (Asesor.Central == "192.168.4.20")
                                                            {
                                                                //cd mexico
                                                                valor = "52 (55) 4000 3255 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else if (Asesor.Central == "192.168.5.20")
                                                            {
                                                                //santiago
                                                                valor = "56 (2) 2760 9120 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else
                                                            {
                                                                valor = "No registra central asignada";
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case "tAlumnos.apepaterno":
                                                    {
                                                        if (Alumno != null)
                                                        {
                                                            valor = Alumno.ApellidoPaterno;
                                                        }
                                                    }
                                                    break;
                                                case "tAlumnos.apematerno":
                                                    {
                                                        if (Alumno != null)
                                                        {
                                                            valor = Alumno.ApellidoMaterno;
                                                        }
                                                    }
                                                    break;
                                                case "tAlumnos.nombre1":
                                                    {
                                                        if (Alumno != null)
                                                        {
                                                            valor = Alumno.Nombre1;
                                                        }
                                                    }
                                                    break;
                                                case "tAlumnos.nombre2":
                                                    {
                                                        if (Alumno != null)
                                                        {
                                                            valor = Alumno.Nombre2;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    valor = ""; break;
                                            }

                                        }
                                    }
                                    if (valor != null)
                                    {
                                        valor = valor.Replace("#$%", "<br>");
                                        plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                        plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                        plantillaEtiqueValor.Texto = valor;

                                    }
                                    else
                                    {
                                        plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                        plantillaEtiqueValor.Codigo = "{ " + etiqueta + "}";
                                        plantillaEtiqueValor.Texto = "";
                                    }
                                    //AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                                }
                            }
                            //AlumnoEtiqueta.Plantilla = plantilla;

                            var whatsAppConfiguracionEnvioDetalle = _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.FirstById(AlumnoEtiqueta.Id);
                            whatsAppConfiguracionEnvioDetalle.Mensaje = plantilla;
                            _unitOfWork.WhatsAppConfiguracionEnvioDetalleRepository.Update(whatsAppConfiguracionEnvioDetalle);
                            _unitOfWork.Commit();
                            //return Ok(new { plantilla, objetoplantilla });
                        }
                        catch (Exception ex)
                        {
                            List<string> correos = new List<string>();
                            correos.Add("fvaldez@bsginstitute.com");

                            TMK_MailService Mailservice = new TMK_MailService();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "fvaldez@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error Proceso Plantillas";
                            mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                    }
                }
                return true;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<TRegistroRecuperacionWhatsApp> Insert(List<RegistroRecuperacionWhatsAppDTO> registroSeguimientoRecuperacion)
        {
            try
            {
                return _unitOfWork.RegistroRecuperacionWhatsAppRepository.Add(registroSeguimientoRecuperacion).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ProcesarNumerosWhatsapp()
        {
            try
            {
                AlumnoDTO _ValidarAlumno = new AlumnoDTO();

                var alumnos = _unitOfWork.AlumnoRepository.ObtenerALumnosaValidarWhatsapp();
                foreach (var item in alumnos)
                {
                    ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                    contact.contacts = new List<string>();
                    var alumno = _unitOfWork.AlumnoRepository.FirstById(item.IdAlumno);

                    try
                    {

                        using (TransactionScope scope = new TransactionScope())
                        {
                            var alumnoNumero = _unitOfWork.AlumnoRepository.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                            contact.contacts.Add("+" + alumnoNumero);
                            var validacion = new AlumnoService(_unitOfWork).ValidarNumeroEnvioWhatsApp(4589, item.IdCodigoPais, contact);

                            //Actualizo el idestadowhatsapp del alumno
                            alumno.IdEstadoContactoWhatsApp = validacion == true ? 1 : 2; //1:valido 2:invalido
                            alumno.FechaModificacion = DateTime.Now;
                            alumno.UsuarioModificacion = "Masivo Whatsapp";
                            _unitOfWork.AlumnoRepository.Update(alumno);
                            scope.Complete();
                        }
                    }
                    catch
                    {
                        //Actualizo el idestadowhatsapp del alumno
                        alumno.IdEstadoContactoWhatsApp = 4; //error al validar
                        alumno.FechaModificacion = DateTime.Now;
                        alumno.UsuarioModificacion = "Masivo Whatsapp";
                        _unitOfWork.AlumnoRepository.Update(alumno);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool EjecutarenviowhatsappPruebaDesarrollo()
        {
            try
            {

                bool BanderaLogin = false;
                string TokenComunicacion = string.Empty;

                var objetoplantilla = new List<DatoPlantillaWhatsAppDTO>();
                DatoPlantillaWhatsAppDTO obj1 = new DatoPlantillaWhatsAppDTO();
                obj1.Codigo = "{tAlumnos.nombre1}";
                obj1.Texto = "Maria";
                DatoPlantillaWhatsAppDTO obj2 = new DatoPlantillaWhatsAppDTO();
                obj2.Codigo = "{tPLA_PGeneral.Nombre}";
                obj2.Texto = "Implementador Lider en Sistemas Integrados";
                objetoplantilla.Add(obj1);
                objetoplantilla.Add(obj2);


                var DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = "51947798302",//Cambiar el numero al que desea enviar
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = "saludo_bienvenida_tres",
                    WaCaption = "Hola Maria  Te saludamos desde BSG Institute para recordarte que te hemos enviado un correo electrónico con información actualizada del Implementador Líder en Sistemas Integrados de Gestión HSEQ ISO 9001, ISO 14001, ISO 45001. Quedamos a tu disposición para cualquier duda o consulta que puedas tener.",
                    datosPlantillaWhatsApp = objetoplantilla
                };

                ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };


                var _repCredenciales = new WhatsAppConfiguracionService(_unitOfWork);
                var _repTokenUsuario = new WhatsAppUsuarioCredencialService(_unitOfWork);

                var CredencialesHost = _repCredenciales.ObtenerCredencialHost(51);
                var TokenValida = _repTokenUsuario.ValidarCredencialesUsuario(4659, 51);


                string urlToPost = CredencialesHost.UrlWhatsApp;

                string Resultado = string.Empty, WaType = string.Empty;

                TokenComunicacion = TokenValida.UserAuthToken;
                BanderaLogin = true;

                switch (DTO.WaType.ToLower())
                {
                    case "text":
                        urlToPost = $"{CredencialesHost.UrlWhatsApp}/v1/messages";
                        WaType = "text";

                        var MensajeTexto = new MensajeTextoEnvio
                        {
                            to = DTO.WaTo,
                            type = DTO.WaType,
                            recipient_type = DTO.WaRecipientType,
                            text = new text
                            {
                                body = DTO.WaBody
                            }
                        };

                        using (WebClient Client = new WebClient())
                        {
                            //client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeTexto);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeTexto);
                            string myParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            Resultado = Client.UploadString(urlToPost, myParameters);
                        }

                        break;
                    case "hsm":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "template";

                        MensajePlantillaWhatsAppEnvioTemplate MensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                        MensajePlantilla.to = DTO.WaTo;
                        MensajePlantilla.type = "template";
                        MensajePlantilla.template = new template();

                        MensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                        MensajePlantilla.template.name = DTO.WaBody;

                        MensajePlantilla.template.language = new language();
                        MensajePlantilla.template.language.policy = "deterministic";
                        MensajePlantilla.template.language.code = "es";

                        MensajePlantilla.template.components = new List<components>();
                        components Componente = new components();
                        Componente.type = "body";


                        if (DTO.datosPlantillaWhatsApp != null)
                        {
                            Componente.parameters = new List<parameters>();
                            foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                            {
                                parameters Dato = new parameters();
                                Dato.type = "text";
                                Dato.text = listaDatos.Texto;

                                Componente.parameters.Add(Dato);
                            }
                        }

                        MensajePlantilla.template.components.Add(Componente);

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajePlantilla);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajePlantilla);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                    case "image":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "image";

                        MensajeImagenEnvio MensajeImagen = new MensajeImagenEnvio();
                        MensajeImagen.to = DTO.WaTo;
                        MensajeImagen.type = DTO.WaType;
                        MensajeImagen.recipient_type = DTO.WaRecipientType;

                        MensajeImagen.image = new image();

                        MensajeImagen.image.caption = DTO.WaCaption;
                        MensajeImagen.image.link = DTO.WaLink;

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeImagen);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeImagen);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                    case "document":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "document";

                        MensajeDocumentoEnvio MensajeDocumento = new MensajeDocumentoEnvio();
                        MensajeDocumento.to = DTO.WaTo;
                        MensajeDocumento.type = DTO.WaType;
                        MensajeDocumento.recipient_type = DTO.WaRecipientType;

                        MensajeDocumento.document = new document();

                        MensajeDocumento.document.caption = DTO.WaCaption;
                        MensajeDocumento.document.link = DTO.WaLink;
                        MensajeDocumento.document.filename = DTO.WaFileName;

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeDocumento);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeDocumento);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool ProcesarEjecucionEstadoWhatsappMasivoFinal()
        {
            try
            {
                AlumnoService alumno = new AlumnoService(_unitOfWork);
                int i = 0;
                bool bandera = true;
                bool banderaAlumnos = true;
                List<AlumnoDTO> alumnosPeru = null;
                List<AlumnoDTO> alumnosColombia = null;
                List<AlumnoDTO> alumnosBolivia = null;
                List<AlumnoDTO> alumnosInternacional = null;
                List<AlumnoDTO> temporal = null;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosPeru = alumno.ObtenerALumnosaValidarWhatsappPeru(3000, i);
                        temporal = alumnosPeru;
                    }
                    else
                    {
                        alumnosPeru = temporal;
                    }

                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(51, alumnosPeru);
                        if (alumnosPeru != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosPeru);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosPeru);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosColombia = _unitOfWork.AlumnoRepository.ObtenerALumnosaValidarWhatsappColombia(3000, i);
                        temporal = alumnosColombia;
                    }
                    else
                    {
                        alumnosColombia = temporal;
                    }

                    if (alumnosColombia.Count > 0)
                    {
                        alumnosColombia = alumno.ValidarEstadoContactoWhatsAppMasivo(57, alumnosColombia);
                        if (alumnosColombia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosColombia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosColombia);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosBolivia = _unitOfWork.AlumnoRepository.ObtenerALumnosaValidarWhatsappBolivia(3000, i);
                        temporal = alumnosBolivia;
                    }
                    else
                    {
                        alumnosBolivia = temporal;
                    }

                    if (alumnosBolivia.Count > 0)
                    {
                        alumnosBolivia = alumno.ValidarEstadoContactoWhatsAppMasivo(591, alumnosBolivia);
                        if (alumnosBolivia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosBolivia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosBolivia);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosInternacional = _unitOfWork.AlumnoRepository.ObtenerALumnosaValidarWhatsappInternacional(3000, i);
                        temporal = alumnosInternacional;
                    }
                    else
                    {
                        alumnosInternacional = temporal;
                    }

                    if (alumnosInternacional.Count > 0)
                    {
                        alumnosInternacional = alumno.ValidarEstadoContactoWhatsAppMasivo(0, alumnosInternacional);
                        if (alumnosInternacional != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosInternacional);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosInternacional);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool RegularizarEstadoWhatsapp()
        {
            try
            {
                AlumnoService alumno = new AlumnoService(_unitOfWork);
                bool bandera = true;
                bool banderaAlumnos = true;
                List<AlumnoDTO> alumnosPeru = null;
                List<AlumnoDTO> alumnosColombia = null;
                List<AlumnoDTO> alumnosBolivia = null;
                List<AlumnoDTO> alumnosInternacional = null;
                List<AlumnoDTO> temporal = null;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosPeru = _unitOfWork.AlumnoRepository.ObtenerALumnosaRegularizarWhatsappPeru();
                        temporal = alumnosPeru;
                    }
                    else
                    {
                        alumnosPeru = temporal;
                    }

                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(51, alumnosPeru);
                        if (alumnosPeru != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosPeru);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosPeru);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosColombia = _unitOfWork.AlumnoRepository.ObtenerALumnosaRegularizarWhatsappColombia();
                        temporal = alumnosColombia;
                    }
                    else
                    {
                        alumnosColombia = temporal;
                    }

                    if (alumnosColombia.Count > 0)
                    {
                        alumnosColombia = alumno.ValidarEstadoContactoWhatsAppMasivo(57, alumnosColombia);
                        if (alumnosColombia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosColombia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosColombia);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosBolivia = _unitOfWork.AlumnoRepository.ObtenerALumnosaRegularizarWhatsappBolivia();
                        temporal = alumnosBolivia;
                    }
                    else
                    {
                        alumnosBolivia = temporal;
                    }

                    if (alumnosBolivia.Count > 0)
                    {
                        alumnosBolivia = alumno.ValidarEstadoContactoWhatsAppMasivo(591, alumnosBolivia);
                        if (alumnosBolivia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosBolivia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosBolivia);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosInternacional = _unitOfWork.AlumnoRepository.ObtenerALumnosaRegularizarWhatsappInternacional();
                        temporal = alumnosInternacional;
                    }
                    else
                    {
                        alumnosInternacional = temporal;
                    }

                    if (alumnosInternacional.Count > 0)
                    {
                        alumnosInternacional = alumno.ValidarEstadoContactoWhatsAppMasivo(0, alumnosInternacional);
                        if (alumnosInternacional != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(alumnosInternacional);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(alumnosInternacional);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
