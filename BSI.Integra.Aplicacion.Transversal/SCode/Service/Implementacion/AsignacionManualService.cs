using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text.RegularExpressions;
using System.Transactions;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionManualService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class AsignacionManualService : IAsignacionManualService
    {
        private static readonly int OCURRENCIA_ASIGNACION_MANUAL = 35;
        private IUnitOfWork _unitOfWork;
        //private Oportunidad _oportunidad;
        private Mapper _mapper;
        public List<OportunidadWhatsappEnvioDTO> listaOportunidadesTemp;

        public AsignacionManualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var serOportunida = new OportunidadService(_unitOfWork);

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOportunidad, Oportunidad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None);
                    cfg.CreateMap<TOportunidadLog, OportunidadLog>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
            //_oportunidad = serOportunida._maperOportunidad.Map<Oportunidad>(_unitOfWork.OportunidadRepository.FirstById(IdOportunidad));
            listaOportunidadesTemp= new List<OportunidadWhatsappEnvioDTO>();

        }
        private ActividadDetalle _ActividadDetalleLocal = new ActividadDetalle();


        //public object AsignarAsesor(AsignarAsesorManualDTO AsignarAsesor, string Usuario)
        //{

        //    try
        //    {
        //        var _repOportunidad = _unitOfWork.OportunidadRepository;
        //        var _repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
        //        var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
        //        var _repAsignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository;

        //        var oportunidadService = new OportunidadService(_unitOfWork);
        //        var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
        //        var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
        //        var oportunidadLogService = new OportunidadLogService(_unitOfWork);
        //        var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

        //        var asignacionOportunidadLogNuevo = new AsignacionOportunidadLog();
        //        var nuevaActividad = new ActividadDetalle();
        //        var nuevaOportunidadLog = new OportunidadLog();
        //        var oportunidadActualizado = new Oportunidad();
        //        var parametrosRetorno = new Oportunidad();


        //        List<OportunidadesAsesorAsignacionAutomaticaDTO> oportunidadesAsesorAsignacionAutomatica = new List<OportunidadesAsesorAsignacionAutomaticaDTO>();
        //        DateTime? fecha;

        //        if (!AsignarAsesor.IdAsesor.HasValue) AsignarAsesor.IdAsesor = 0;
        //        if (!AsignarAsesor.IdCentroCosto.HasValue) AsignarAsesor.IdCentroCosto = 0;
        //        if (!AsignarAsesor.FechaProgramada.HasValue) fecha = null; else fecha = AsignarAsesor.FechaProgramada;

        //        oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();


        //        var oportunidadesFaltantes = AsignarAsesor.IdOportunidades.ToList();
        //        List<OportunidadWhatsappEnvioDTO> listaOportunidades = new List<OportunidadWhatsappEnvioDTO>();

        //        var envioWhatsapp = AsignarAsesor.envioWhats == null ? false : AsignarAsesor.envioWhats.Value;

        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
        //        {
        //            foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
        //            {
        //                //Actualizar Oportunidad con centro costo y/o asesor
        //                Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));
        //                Oportunidad oportunidadNueva = new Oportunidad();
        //                oportunidadNueva.Id = oportunidadAntigua.Id;
        //                oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
        //                oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
        //                oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
        //                oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
        //                oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
        //                oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
        //                oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
        //                oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
        //                oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
        //                oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
        //                oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
        //                oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
        //                oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
        //                oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
        //                oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
        //                oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
        //                oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
        //                oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
        //                oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
        //                oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
        //                oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
        //                oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
        //                oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
        //                oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
        //                oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
        //                oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
        //                oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
        //                oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
        //                oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
        //                oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
        //                oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
        //                oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
        //                oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
        //                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
        //                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
        //                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
        //                oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
        //                oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
        //                oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
        //                oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
        //                oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
        //                oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
        //                oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
        //                oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
        //                oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
        //                oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
        //                oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

        //                oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
        //                //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
        //                oportunidadNueva.Estado = true;
        //                oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
        //                //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

        //                if (oportunidadAntigua.IdPersonalAsignado == 125)
        //                {
        //                    OportunidadesAsesorAsignacionAutomaticaDTO oportunidadesAsesorAsignacion = new OportunidadesAsesorAsignacionAutomaticaDTO()
        //                    {
        //                        Id = oportunidadAntigua.Id,
        //                        IdMigracion = new Guid()
        //                    };
        //                    oportunidadesAsesorAsignacionAutomatica.Add(oportunidadesAsesorAsignacion);
        //                }
        //                AsignacionOportunidadLog asignacionLog = new AsignacionOportunidadLog();
        //                asignacionLog.FechaLog = DateTime.Now;
        //                asignacionLog.IdPersonalAnterior = oportunidadAntigua.IdPersonalAsignado;
        //                asignacionLog.IdCentroCostoAnt = oportunidadAntigua.IdCentroCosto;
        //                asignacionLog.IdOportunidad = oportunidadAntigua.Id;

        //                oportunidadNueva.Id = idOportunidad;
        //                var validacionCentroCostoV2 = oportunidadAntigua.IdCentroCosto;
        //                oportunidadNueva.IdCentroCosto = AsignarAsesor.IdCentroCosto.Value == 0 ? oportunidadAntigua.IdCentroCosto : AsignarAsesor.IdCentroCosto.Value;
        //                oportunidadNueva.IdPersonalAsignado = AsignarAsesor.IdAsesor.Value == 0 ? oportunidadAntigua.IdPersonalAsignado : AsignarAsesor.IdAsesor.Value;

        //                //VALIDACION DE CAMBIO DE CENTRO DE COSTO
        //                if (AsignarAsesor.IdCentroCosto != null && AsignarAsesor.IdCentroCosto != 0)
        //                {
        //                    var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
        //                    var _repCentroCosto = _unitOfWork.CentroCostoRepository;
        //                    var _repPEspecifico = _unitOfWork.PEspecificoRepository;
        //                    var _repAlumnoCambio = _unitOfWork.AlumnoRepository;

        //                    //Obtener el IdPEspecifico según el centro de costo Anterior
        //                    if (validacionCentroCostoV2 != null)
        //                    {
        //                        var pEspecificoCambio = _repPEspecifico.GetBy(x => x.IdCentroCosto == validacionCentroCostoV2).FirstOrDefault();

        //                        if (pEspecificoCambio != null)
        //                        {
        //                            //Validamos que la matrícula exista con el Id del Alumno y el Id de PEspecifico
        //                            var validarMatricula = _repMatriculaCabecera.GetBy(x => x.IdAlumno == oportunidadNueva.IdAlumno && x.IdPespecifico == pEspecificoCambio.Id).FirstOrDefault();
        //                            if (validarMatricula != null)
        //                            {
        //                                _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarMatricula", Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

        //                                var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
        //                                return ("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
        //                            }

        //                            var _repMontoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository;
        //                            var validacionMontoPagoCronograma = _repMontoPagoCronograma.GetBy(x => x.IdOportunidad == idOportunidad).FirstOrDefault();
        //                            if (validacionMontoPagoCronograma != null)
        //                            {
        //                                var validarMatricula2 = _repMatriculaCabecera.GetBy(x => x.IdCronograma == validacionMontoPagoCronograma.Id).FirstOrDefault();
        //                                if (validarMatricula2 != null)
        //                                {
        //                                    var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
        //                                    _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarCronograma", Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}&IdCronograma={validacionMontoPagoCronograma.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
        //                                    return ("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
        //                                }
        //                            }
        //                        }
        //                    }
        //                }

        //                //Registramos la asignacion con los nuevos datos


        //                AsignacionOportunidad asig = _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(idOportunidad);

        //                if (asig == null)
        //                {
        //                    asig = new AsignacionOportunidad();
        //                    asig.FechaAsignacion = DateTime.Now;
        //                    asig.IdAlumno = oportunidadNueva.IdAlumno;
        //                    asig.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona;
        //                    asig.IdPersonal = oportunidadNueva.IdPersonalAsignado;
        //                    asig.IdCentroCosto = oportunidadNueva.IdCentroCosto.Value;
        //                    asig.IdOportunidad = idOportunidad;
        //                    asig.IdTipoDato = oportunidadNueva.IdTipoDato;
        //                    asig.IdFaseOportunidad = oportunidadNueva.IdFaseOportunidad;
        //                    asig.Estado = true;
        //                    asig.FechaCreacion = DateTime.Now;
        //                    asig.FechaModificacion = DateTime.Now;
        //                    asig.UsuarioCreacion = Usuario;
        //                    asig.UsuarioModificacion = Usuario;
        //                    _repAsignacionOportunidad.Add(asig);
        //                }
        //                else
        //                {
        //                    asig.FechaAsignacion = DateTime.Now;
        //                    asig.IdPersonal = oportunidadNueva.IdPersonalAsignado == 0 ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
        //                    asig.IdCentroCosto = oportunidadNueva.IdCentroCosto == 0 ? asig.IdCentroCosto : oportunidadNueva.IdCentroCosto.Value;
        //                    asig.IdAlumno = oportunidadNueva.IdAlumno == 0 ? asig.IdAlumno : oportunidadNueva.IdAlumno;
        //                    asig.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona == 0 ? asig.IdClasificacionPersona : oportunidadNueva.IdClasificacionPersona;
        //                    asig.IdPersonal = oportunidadNueva.IdPersonalAsignado == 0 ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
        //                    asig.FechaModificacion = DateTime.Now;
        //                    asig.UsuarioModificacion = Usuario;
        //                    _repAsignacionOportunidad.Update(asig);
        //                }

        //                asignacionLog.IdTipoDato = asig.IdTipoDato;
        //                asignacionLog.IdPersonal = asig.IdPersonal;
        //                asignacionLog.IdFaseOportunidad = asig.IdFaseOportunidad;
        //                asignacionLog.IdAlumno = asig.IdAlumno;
        //                asignacionLog.IdClasificacionPersona = asig.IdClasificacionPersona;
        //                asignacionLog.Estado = true;
        //                asignacionLog.FechaCreacion = DateTime.Now;
        //                asignacionLog.FechaModificacion = DateTime.Now;
        //                asignacionLog.UsuarioCreacion = Usuario;
        //                asignacionLog.UsuarioModificacion = Usuario;
        //                asignacionLog.IdCentroCosto = asig.IdCentroCosto;
        //                asignacionLog.IdAsignacionOportunidad = asig.Id;
        //                // opo.AsignacionOportunidads.AsignacionOportunidadLogs = asignacionLog;
        //                asignacionOportunidadLogService.Add(asignacionLog);
        //                //Finalizar Actividad

        //                ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
        //                //opo.ActividadDetalles = actividadService.ObtenerEntidadActividadDetallePorId(opo.IdActividadDetalleUltima.Value);

        //                ActividadDetalleAntigua.Comentario = "Asignacion Manual";
        //                ActividadDetalleAntigua.IdOcurrencia = OCURRENCIA_ASIGNACION_MANUAL;
        //                ActividadDetalleAntigua.IdOcurrenciaAlterno = OCURRENCIA_ASIGNACION_MANUAL;
        //                ActividadDetalleAntigua.IdOcurrenciaActividad = null;
        //                ActividadDetalleAntigua.IdOcurrenciaActividadAlterno = null;
        //                ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
        //                ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
        //                ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
        //                ActividadDetalleAntigua.IdCentralLlamada = 0;
        //                ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;
        //                ActividadDetalleAntigua.FechaReal = DateTime.Now;
        //                ActividadDetalleAntigua.UsuarioModificacion = Usuario;

        //                if (oportunidadAntigua.FechaPaso2 != null)
        //                {
        //                    oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
        //                }
        //                if (oportunidadAntigua.Paso2 != null)
        //                {
        //                    oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
        //                }
        //                if (oportunidadAntigua.CodMailing != null)
        //                {
        //                    oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
        //                }
        //                oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
        //                //oportunidadNueva.Usuario = Usuario;
        //                oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
        //                oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
        //                oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;


        //                oportunidadService.FinalizarActividades(false, Usuario);

        //                if (AsignarAsesor.IdAsesor != 0)
        //                    oportunidadService._asignacionManual.OportunidadLogNueva.IdAsesorAnt = AsignarAsesor.IdAsesor;

        //                if (fecha != null)
        //                {
        //                    oportunidadService._asignacionManual.OportunidadNueva.UltimaFechaProgramada = fecha;
        //                }

        //                if (AsignarAsesor.VentaCruzadaMarketing.HasValue)
        //                {
        //                    oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = AsignarAsesor.VentaCruzadaMarketing.Value;
        //                }
        //                else
        //                {
        //                    oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = false;
        //                }


        //                oportunidadService.ProgramaActividad(AsignarAsesor.SegunMejorPro.GetValueOrDefault());


        //                actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
        //                actividadDetalleService.Add(oportunidadService._asignacionManual.ActividadNuevaProgramarActividad);

        //                oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = AsignarAsesor.VentaCruzadaMarketing == true;


        //                _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
        //                _unitOfWork.Commit();
        //                oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

        //                IAgendaService agendaService = new AgendaService(_unitOfWork);
        //                // 827 Correo Informacion del Curso Completo
        //                agendaService.EnviarCorreoOportunidadAutomatico(oportunidadService._asignacionManual.OportunidadNueva.Id , 1967, "Automatico1967");

        //                try
        //                {
        //                    OportunidadWhatsappEnvioDTO item = new OportunidadWhatsappEnvioDTO();
        //                    item.IdOportunidad = (int)asig.IdOportunidad;
        //                    item.IdPersonal = (int)asig.IdPersonal;
        //                    item.IdCategoriaOrigen = (int)oportunidadNueva.IdCategoriaOrigen;

        //                    //si el asesor actual es asignacion automatica

        //                    if (envioWhatsapp || oportunidadAntigua.IdPersonalAsignado == 125)
        //                    {
        //                        item.AplicaEnvioWhatsapp = true;
        //                    }


        //                    listaOportunidades.Add(item);
        //                }
        //                catch (Exception e) { }

        //                //_unitOfWork.Commit();

        //                //oportunidadActualizado = oportunidadService.Update(parametrosRetorno);

        //                //Programar Actividad
        //                //int am = 0;
        //            }

        //            scope.Complete();
        //        }
        //        // nuevaActividad = oportunidadActualizado.ActividadDetalles.FirstOrDefault();
        //        var _repOportunidad2 = _unitOfWork.OportunidadRepository;
        //        foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
        //        {
        //            Oportunidad oportunidad = new Oportunidad();
        //            OportunidadLog oportunidadLog = new OportunidadLog();
        //            var actividadDetalle = _repActividadDetalle.GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
        //            if (actividadDetalle != null)
        //            {

        //                _unitOfWork.OportunidadRepository.ActualizarIdActividadDetalleUltima(actividadDetalle.Id, idOportunidad);
        //            }
        //        }

        //        try
        //        {
        //            if (AsignarAsesor.IdAsesor != null)
        //            {
        //                AgendaSocket.getInstance().NuevaActividadParaEjecutar(AsignarAsesor.IdOportunidades[0] ?? 0, AsignarAsesor.IdAsesor.Value);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //        }

        //        try
        //        {
        //            foreach (var item in listaOportunidades)
        //            {
        //                if (item.AplicaEnvioWhatsapp)
        //                {
        //                    var pais = _unitOfWork.AsignacionRegularRepository.ObtenerPaisPorOportunidad((int)item.IdOportunidad);
        //                    //51:PERU,56:CHILE,57:COLOMBIA,52:MEXICO
        //                    if (pais.Id == 51 || pais.Id == 56 || pais.Id == 57 || pais.Id == 52)
        //                    {
        //                        EnvioWhats((int)item.IdOportunidad, pais.Id, (int)item.IdPersonal, (int)item.IdCategoriaOrigen);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e) { }
        //        //lo asingo a una lista temporal para luego poder usarla en el envio de correos
        //        listaOportunidadesTemp = listaOportunidades;
        //        return new { data = true, OportunidadesAsesorAsignacionAutomatica = oportunidadesAsesorAsignacionAutomatica };
        //    }
        //    catch (Exception ex)
        //    {
        //        var _repLog = _unitOfWork.LogRepository;
        //        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor", Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto},{AsignarAsesor.IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

        //        return (ex.Message);
        //    }
        //}

        public object AsignarAsesor(AsignarAsesorManualDTO AsignarAsesor, string Usuario)
        {
            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
                var _repAsignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

                List<OportunidadesAsesorAsignacionAutomaticaDTO> oportunidadesAsesorAsignacionAutomatica = new();
                List<OportunidadWhatsappEnvioDTO> listaOportunidades = new();

                DateTime? fecha = AsignarAsesor.FechaProgramada;
                if (!AsignarAsesor.IdAsesor.HasValue) AsignarAsesor.IdAsesor = 0;
                if (!AsignarAsesor.IdCentroCosto.HasValue) AsignarAsesor.IdCentroCosto = 0;
                var envioWhatsapp = AsignarAsesor.envioWhats == true;

                // patrón “auto”: scope por oportunidad
                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(15)
                };

                foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                {
                    try
                    {
                        using (var scope = new TransactionScope(
                            TransactionScopeOption.Required,
                            txOptions,
                            TransactionScopeAsyncFlowOption.Enabled))
                        {
                            // 1) MAPEAR OPORTUNIDAD ANTIGUA -> NUEVA
                            var oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));
                            var oportunidadNueva = new Oportunidad
                            {
                                Id = oportunidadAntigua.Id,
                                IdCentroCosto = oportunidadAntigua.IdCentroCosto,
                                IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado,
                                IdTipoDato = oportunidadAntigua.IdTipoDato,
                                IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad,
                                IdOrigen = oportunidadAntigua.IdOrigen,
                                IdAlumno = oportunidadAntigua.IdAlumno,
                                UltimoComentario = oportunidadAntigua.UltimoComentario,
                                IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima,
                                IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima,
                                IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado,
                                UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada,
                                IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad,
                                IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo,
                                IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima,
                                IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial,
                                IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen,
                                IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio,
                                IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring,
                                IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp,
                                IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc,
                                FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf,
                                FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf,
                                FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc,
                                FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania,
                                IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal,
                                IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf,
                                CodigoPagoIc = oportunidadAntigua.CodigoPagoIc,
                                FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada,
                                IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion,
                                IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion,
                                IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato,
                                IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario,
                                UrlOrigen = oportunidadAntigua.UrlOrigen,
                                FechaPaso2 = oportunidadAntigua.FechaPaso2,
                                Paso2 = oportunidadAntigua.Paso2,
                                CodMailing = oportunidadAntigua.CodMailing,
                                IdPagina = oportunidadAntigua.IdPagina,
                                NroSolicitud = oportunidadAntigua.NroSolicitud,
                                NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea,
                                NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea,
                                NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral,
                                NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico,
                                IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona,
                                IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo,
                                IdPadre = oportunidadAntigua.IdPadre,
                                IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook,
                                ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta,
                                FechaCreacion = oportunidadAntigua.FechaCreacion,
                                Estado = true,
                                UsuarioCreacion = oportunidadAntigua.UsuarioCreacion
                            };

                            if (oportunidadAntigua.IdPersonalAsignado == 125)
                            {
                                oportunidadesAsesorAsignacionAutomatica.Add(new OportunidadesAsesorAsignacionAutomaticaDTO
                                {
                                    Id = oportunidadAntigua.Id,
                                    IdMigracion = Guid.NewGuid()
                                });
                            }

                            // 2) APLICAR CAMBIOS DE ASESOR/CC
                            var ccAnterior = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdCentroCosto = (AsignarAsesor.IdCentroCosto ?? 0) == 0
                                ? oportunidadAntigua.IdCentroCosto
                                : AsignarAsesor.IdCentroCosto.Value;

                            oportunidadNueva.IdPersonalAsignado = (AsignarAsesor.IdAsesor ?? 0) == 0
                                ? oportunidadAntigua.IdPersonalAsignado
                                : AsignarAsesor.IdAsesor.Value;

                            // Validaciones de cambio de CC (idénticas a tu versión original)
                            if (AsignarAsesor.IdCentroCosto.HasValue && AsignarAsesor.IdCentroCosto.Value != 0)
                            {
                                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                                var _repPEspecifico = _unitOfWork.PEspecificoRepository;
                                var _repAlumnoCambio = _unitOfWork.AlumnoRepository;
                                var _repMontoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository;

                                if (ccAnterior.HasValue)
                                {
                                    var pEspecificoCambio = _repPEspecifico.GetBy(x => x.IdCentroCosto == ccAnterior).FirstOrDefault();
                                    if (pEspecificoCambio != null)
                                    {
                                        var validarMatricula = _repMatriculaCabecera.GetBy(x =>
                                            x.IdAlumno == oportunidadNueva.IdAlumno && x.IdPespecifico == pEspecificoCambio.Id).FirstOrDefault();
                                        if (validarMatricula != null)
                                        {
                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "AsignarAsesor/ValidarMatricula",
                                                Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}",
                                                Mensaje = "Error en validacion de Matricula en Asignacion de asesor",
                                                Excepcion = "Error en validacion de Matricula en Asignacion de asesor",
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                            return $"El alumno: {datosAlumno.Nombre1} {datosAlumno.Nombre2} {datosAlumno.ApellidoPaterno} {datosAlumno.ApellidoMaterno} ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones";
                                        }

                                        var cron = _repMontoPagoCronograma.GetBy(x => x.IdOportunidad == idOportunidad).FirstOrDefault();
                                        if (cron != null)
                                        {
                                            var validarMatricula2 = _repMatriculaCabecera.GetBy(x => x.IdCronograma == cron.Id).FirstOrDefault();
                                            if (validarMatricula2 != null)
                                            {
                                                var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                                _unitOfWork.LogRepository.Insert(new TLog
                                                {
                                                    Ip = "-",
                                                    Usuario = "-",
                                                    Maquina = "-",
                                                    Ruta = "AsignarAsesor/ValidarCronograma",
                                                    Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdCronograma={cron.Id}",
                                                    Mensaje = "Error en validacion de Matricula en Asignacion de asesor",
                                                    Excepcion = "Error en validacion de Matricula en Asignacion de asesor",
                                                    Tipo = "UPDATE",
                                                    IdPadre = 0,
                                                    UsuarioCreacion = string.Empty,
                                                    UsuarioModificacion = string.Empty,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    Estado = true
                                                });
                                                return $"El alumno: {datosAlumno.Nombre1} {datosAlumno.Nombre2} {datosAlumno.ApellidoPaterno} {datosAlumno.ApellidoMaterno} ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones";
                                            }
                                        }
                                    }
                                }
                            }

                            // 3) CREAR/ACTUALIZAR AsignacionOportunidad (CAPTURANDO asigTemp)
                            var asig = _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(idOportunidad);
                            TAsignacionOportunidad asigTemp = new TAsignacionOportunidad();

                            if (asig == null)
                            {
                                asig = new AsignacionOportunidad
                                {
                                    FechaAsignacion = DateTime.Now,
                                    IdAlumno = oportunidadNueva.IdAlumno,
                                    IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona,
                                    IdPersonal = oportunidadNueva.IdPersonalAsignado,
                                    IdCentroCosto = oportunidadNueva.IdCentroCosto.Value,
                                    IdOportunidad = idOportunidad,
                                    IdTipoDato = oportunidadNueva.IdTipoDato,
                                    IdFaseOportunidad = oportunidadNueva.IdFaseOportunidad,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Usuario,
                                    UsuarioModificacion = Usuario
                                };
                                asigTemp = _unitOfWork.AsignacionOportunidadRepository.Add(asig);
                            }
                            else
                            {
                                asig.FechaAsignacion = DateTime.Now;
                                asig.IdPersonal = (oportunidadNueva.IdPersonalAsignado == 0) ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
                                asig.IdCentroCosto = (oportunidadNueva.IdCentroCosto == 0) ? asig.IdCentroCosto : oportunidadNueva.IdCentroCosto.Value;
                                asig.IdAlumno = (oportunidadNueva.IdAlumno == 0) ? asig.IdAlumno : oportunidadNueva.IdAlumno;
                                asig.IdClasificacionPersona = (oportunidadNueva.IdClasificacionPersona == 0) ? asig.IdClasificacionPersona : oportunidadNueva.IdClasificacionPersona;
                                asig.FechaModificacion = DateTime.Now;
                                asig.UsuarioModificacion = Usuario;

                                asigTemp = _unitOfWork.AsignacionOportunidadRepository.Update(asig);
                            }

                            // Materializa Id (igual que en “auto”)
                            _unitOfWork.Commit();

                            // 4) LOG de Asignación (usando asigTemp.Id)
                            var asignacionLog = new AsignacionOportunidadLog
                            {
                                FechaLog = DateTime.Now,
                                IdPersonalAnterior = oportunidadAntigua.IdPersonalAsignado,
                                IdCentroCostoAnt = oportunidadAntigua.IdCentroCosto,
                                IdOportunidad = oportunidadAntigua.Id,
                                IdTipoDato = asig.IdTipoDato,
                                IdPersonal = asig.IdPersonal,
                                IdFaseOportunidad = asig.IdFaseOportunidad,
                                IdAlumno = asig.IdAlumno,
                                IdClasificacionPersona = asig.IdClasificacionPersona,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                IdCentroCosto = asig.IdCentroCosto,
                                IdAsignacionOportunidad = asigTemp.Id // << clave
                            };
                            asignacionOportunidadLogService.Add(asignacionLog);

                            _unitOfWork.Commit();

                            // 5) FINALIZAR actividad anterior y programar nueva
                            var ActividadDetalleAntigua =
                                actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);

                            ActividadDetalleAntigua.Comentario = "Asignacion Manual";
                            ActividadDetalleAntigua.IdOcurrencia = OCURRENCIA_ASIGNACION_MANUAL;
                            ActividadDetalleAntigua.IdOcurrenciaAlterno = OCURRENCIA_ASIGNACION_MANUAL;
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdOcurrenciaActividadAlterno = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;
                            ActividadDetalleAntigua.FechaReal = DateTime.Now;
                            ActividadDetalleAntigua.UsuarioModificacion = Usuario;

                            if (oportunidadAntigua.FechaPaso2 != null)
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            if (oportunidadAntigua.Paso2 != null)
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            if (oportunidadAntigua.CodMailing != null)
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            //if (oportunidadAntigua.IdPagina.HasValue)
                            //    oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();
                            
                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            oportunidadService.FinalizarActividades(false, Usuario);

                            if (AsignarAsesor.IdAsesor != 0)
                                oportunidadService._asignacionManual.OportunidadLogNueva.IdAsesorAnt = AsignarAsesor.IdAsesor;

                            if (fecha != null)
                                oportunidadService._asignacionManual.OportunidadNueva.UltimaFechaProgramada = fecha;

                            if (AsignarAsesor.VentaCruzadaMarketing.HasValue)
                                oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = AsignarAsesor.VentaCruzadaMarketing.Value;
                            else
                                oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = false;

                            oportunidadService.ProgramaActividad(AsignarAsesor.SegunMejorPro.GetValueOrDefault());

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            actividadDetalleService.Add(oportunidadService._asignacionManual.ActividadNuevaProgramarActividad);

                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();

                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            // (Conservamos el envío de correo como estaba en manual)
                            IAgendaService agendaService = new AgendaService(_unitOfWork);
                            agendaService.EnviarCorreoOportunidadAutomatico(oportunidadService._asignacionManual.OportunidadNueva.Id, 1967, "Automatico1967");

                            // Build item para envío de WhatsApp posterior
                            try
                            {
                                var item = new OportunidadWhatsappEnvioDTO
                                {
                                    IdOportunidad = (int)asig.IdOportunidad,
                                    IdPersonal = (int)asig.IdPersonal,
                                    IdCategoriaOrigen = (int)oportunidadNueva.IdCategoriaOrigen,
                                    AplicaEnvioWhatsapp = envioWhatsapp || oportunidadAntigua.IdPersonalAsignado == 125
                                };
                                listaOportunidades.Add(item);
                            }
                            catch { }

                            // Actualiza IdActividadDetalleUltima a lo más reciente
                            var actividadUltima = _repActividadDetalle.GetBy(w => w.IdOportunidad == idOportunidad,
                                w => new { w.Id, w.FechaCreacion })
                                .OrderByDescending(y => y.FechaCreacion)
                                .FirstOrDefault();
                            if (actividadUltima != null)
                            {
                                _unitOfWork.OportunidadRepository.ActualizarIdActividadDetalleUltima(actividadUltima.Id, idOportunidad);
                                _unitOfWork.Commit();
                            }

                            scope.Complete();
                        }
                    }
                    catch (Exception exOp)
                    {
                        _unitOfWork.LogRepository.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = Usuario,
                            Maquina = "-",
                            Ruta = "AsignarAsesor-OPORTUNIDAD",
                            Parametros = $"{idOportunidad},{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto}/{Usuario}",
                            Mensaje = exOp.Message,
                            Excepcion = exOp.ToString(),
                            Tipo = "ERROR",
                            IdPadre = 0,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                        // continua con la siguiente oportunidad
                    }
                }

                
            try
            {
                    foreach (var item in listaOportunidades)
                    {
                        if (item.AplicaEnvioWhatsapp)
                        {
                            var pais = _unitOfWork.AsignacionRegularRepository.ObtenerPaisPorOportunidad((int)item.IdOportunidad);
                            // 51 PER, 56 CHI, 57 COL, 52 MEX
                            if (pais.Id == 51 || pais.Id == 56 || pais.Id == 57 || pais.Id == 52)
                            {
                                EnvioWhats((int)item.IdOportunidad, pais.Id, (int)item.IdPersonal, (int)item.IdCategoriaOrigen);
                            }
                        }
                    }
                }
                catch { }

                listaOportunidadesTemp = listaOportunidades;

                return new
                {
                    data = true,
                    OportunidadesAsesorAsignacionAutomatica = oportunidadesAsesorAsignacionAutomatica
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.LogRepository.Insert(new TLog
                {
                    Ip = "-",
                    Usuario = "-",
                    Maquina = "-",
                    Ruta = "AsignarAsesor",
                    Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto},{AsignarAsesor.IdOportunidades}/{Usuario}",
                    Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}",
                    Excepcion = ex.ToString(),
                    Tipo = "UPDATE",
                    IdPadre = 0,
                    UsuarioCreacion = string.Empty,
                    UsuarioModificacion = string.Empty,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                });

                return ex.Message;
            }
        }



        public object AsignarAsesorAuto(AsignarAsesorManualDTO AsignarAsesor, string Usuario)
        {
            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
                var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                var _repAsignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

                var asignacionOportunidadLogNuevo = new AsignacionOportunidadLog();
                var nuevaActividad = new ActividadDetalle();
                var nuevaOportunidadLog = new OportunidadLog();
                var oportunidadActualizado = new Oportunidad();
                var parametrosRetorno = new Oportunidad();

                var oportunidadesAsesorAsignacionAutomatica = new List<OportunidadesAsesorAsignacionAutomaticaDTO>();
                DateTime? fecha;
                if (!AsignarAsesor.IdAsesor.HasValue) AsignarAsesor.IdAsesor = 0;
                if (!AsignarAsesor.IdCentroCosto.HasValue) AsignarAsesor.IdCentroCosto = 0;
                fecha = AsignarAsesor.FechaProgramada.HasValue
                    ? AsignarAsesor.FechaProgramada
                    : (DateTime?)null;
                oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();


                var oportunidadesFaltantes = AsignarAsesor.IdOportunidades.ToList();
                var listaOportunidades = new List<OportunidadWhatsappEnvioDTO>();
                var envioWhatsapp = AsignarAsesor.envioWhats == true;
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(2),
                };
                foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                {
                    try
                    {

                        using (var scope = new TransactionScope(
                            TransactionScopeOption.Required,
                            transactionOptions, TransactionScopeAsyncFlowOption.Enabled
                            ))
                        {
                            // MAPEO DE LA OPORTUNIDAD ANTIGUA A NUEVA
                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(
                                _repOportunidad.FirstById(idOportunidad));
                            var oportunidadNueva = new Oportunidad
                            {
                                Id = oportunidadAntigua.Id,
                                IdCentroCosto = oportunidadAntigua.IdCentroCosto,
                                IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado,
                                IdTipoDato = oportunidadAntigua.IdTipoDato,
                                IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad,
                                IdOrigen = oportunidadAntigua.IdOrigen,
                                IdAlumno = oportunidadAntigua.IdAlumno,
                                UltimoComentario = oportunidadAntigua.UltimoComentario,
                                IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima,
                                IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima,
                                IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado,
                                UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada,
                                IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad,
                                IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo,
                                IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima,
                                IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial,
                                IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen,
                                IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio,
                                IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring,
                                IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp,
                                IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc,
                                FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf,
                                FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf,
                                FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc,
                                FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania,
                                IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal,
                                IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf,
                                CodigoPagoIc = oportunidadAntigua.CodigoPagoIc,
                                FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada,
                                IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion,
                                IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion,
                                IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato,
                                IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario,
                                UrlOrigen = oportunidadAntigua.UrlOrigen,
                                FechaPaso2 = oportunidadAntigua.FechaPaso2,
                                Paso2 = oportunidadAntigua.Paso2,
                                CodMailing = oportunidadAntigua.CodMailing,
                                IdPagina = oportunidadAntigua.IdPagina,
                                NroSolicitud = oportunidadAntigua.NroSolicitud,
                                NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea,
                                NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea,
                                NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral,
                                NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico,
                                IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona,
                                IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo,
                                IdPadre = oportunidadAntigua.IdPadre,
                                IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook,
                                ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta,
                                FechaCreacion = oportunidadAntigua.FechaCreacion,
                                Estado = true,
                                UsuarioCreacion = oportunidadAntigua.UsuarioCreacion
                            };

                            // Registro para migración si venía de 125
                            if (oportunidadAntigua.IdPersonalAsignado == 125)
                            {
                                oportunidadesAsesorAsignacionAutomatica.Add(
                                    new OportunidadesAsesorAsignacionAutomaticaDTO
                                    {
                                        Id = oportunidadAntigua.Id,
                                        IdMigracion = new Guid()
                                    });
                            }

                            // CREAR LOG DE ASIGNACIÓN
                            var asignacionLog = new AsignacionOportunidadLog
                            {
                                FechaLog = DateTime.Now,
                                IdPersonalAnterior = oportunidadAntigua.IdPersonalAsignado,
                                IdCentroCostoAnt = oportunidadAntigua.IdCentroCosto,
                                IdOportunidad = oportunidadAntigua.Id
                            };

                            // Aplicar cambios de centro/asesor
                            var validacionCentroCostoV2 = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdCentroCosto = AsignarAsesor.IdCentroCosto.Value == 0
                                                                    ? oportunidadAntigua.IdCentroCosto
                                                                    : AsignarAsesor.IdCentroCosto.Value;
                            oportunidadNueva.IdPersonalAsignado = AsignarAsesor.IdAsesor.Value == 0
                                                                    ? oportunidadAntigua.IdPersonalAsignado
                                                                    : AsignarAsesor.IdAsesor.Value;

                            // VALIDACIÓN CAMBIO DE CENTRO DE COSTO
                            if (AsignarAsesor.IdCentroCosto.HasValue && AsignarAsesor.IdCentroCosto.Value != 0)
                            {
                                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                                var _repPEspecifico = _unitOfWork.PEspecificoRepository;
                                var _repAlumnoCambio = _unitOfWork.AlumnoRepository;

                                if (validacionCentroCostoV2.HasValue)
                                {
                                    var pEspecificoCambio = _repPEspecifico
                                        .GetBy(x => x.IdCentroCosto == validacionCentroCostoV2)
                                        .FirstOrDefault();
                                    if (pEspecificoCambio != null)
                                    {
                                        var validarMatricula = _repMatriculaCabecera
                                            .GetBy(x => x.IdAlumno == oportunidadNueva.IdAlumno
                                                     && x.IdPespecifico == pEspecificoCambio.Id)
                                            .FirstOrDefault();
                                        if (validarMatricula != null)
                                        {
                                            _unitOfWork.LogRepository.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "AsignarAsesor/ValidarMatricula",
                                                Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}",
                                                Mensaje = "Error en validacion de Matricula en Asignacion de asesor",
                                                Excepcion = "Error en validacion de Matricula en Asignacion de asesor",
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = "IntegraLog",
                                                UsuarioModificacion = "IntegraLog",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                            var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                            return $"El alumno: {datosAlumno.Nombre1} {datosAlumno.Nombre2} {datosAlumno.ApellidoPaterno} {datosAlumno.ApellidoMaterno} ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones";
                                        }

                                        var _repMontoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository;
                                        var validacionMontoPagoCronograma = _repMontoPagoCronograma
                                            .GetBy(x => x.IdOportunidad == idOportunidad)
                                            .FirstOrDefault();
                                        if (validacionMontoPagoCronograma != null)
                                        {
                                            var validarMatricula2 = _repMatriculaCabecera
                                                .GetBy(x => x.IdCronograma == validacionMontoPagoCronograma.Id)
                                                .FirstOrDefault();
                                            if (validarMatricula2 != null)
                                            {
                                                var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                                _unitOfWork.LogRepository.Insert(new TLog
                                                {
                                                    Ip = "-",
                                                    Usuario = "-",
                                                    Maquina = "-",
                                                    Ruta = "AsignarAsesor/ValidarCronograma",
                                                    Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}&IdCronograma={validacionMontoPagoCronograma.Id}",
                                                    Mensaje = "Error en validacion de Matricula en Asignacion de asesor",
                                                    Excepcion = "Error en validacion de Matricula en Asignacion de asesor",
                                                    Tipo = "UPDATE",
                                                    IdPadre = 0,
                                                    UsuarioCreacion = "IntegraLog",
                                                    UsuarioModificacion = "IntegraLog",
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    Estado = true
                                                });
                                                return $"El alumno: {datosAlumno.Nombre1} {datosAlumno.Nombre2} {datosAlumno.ApellidoPaterno} {datosAlumno.ApellidoMaterno} ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones";
                                            }
                                        }
                                    }
                                }
                            }

                            // CREAR O ACTUALIZAR AsignacionOportunidad
                            AsignacionOportunidad asig = _unitOfWork.AsignacionOportunidadRepository
                                          .ObtenerPorIdOportunidad(idOportunidad);
                            TAsignacionOportunidad asigTemp = new TAsignacionOportunidad();
                            if (asig == null)
                            {
                                asig = new AsignacionOportunidad
                                {
                                    FechaAsignacion = DateTime.Now,
                                    IdAlumno = oportunidadNueva.IdAlumno,
                                    IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona,
                                    IdPersonal = oportunidadNueva.IdPersonalAsignado,
                                    IdCentroCosto = oportunidadNueva.IdCentroCosto.Value,
                                    IdOportunidad = idOportunidad,
                                    IdTipoDato = oportunidadNueva.IdTipoDato,
                                    IdFaseOportunidad = oportunidadNueva.IdFaseOportunidad,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = Usuario,
                                    UsuarioModificacion = Usuario
                                };
                                asigTemp = (_repAsignacionOportunidad.Add(asig));
                            }
                            else
                            {
                                asig.FechaAsignacion = DateTime.Now;
                                asig.IdPersonal = oportunidadNueva.IdPersonalAsignado == 0 ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
                                asig.IdCentroCosto = oportunidadNueva.IdCentroCosto == 0 ? asig.IdCentroCosto : oportunidadNueva.IdCentroCosto.Value;
                                asig.IdAlumno = oportunidadNueva.IdAlumno == 0 ? asig.IdAlumno : oportunidadNueva.IdAlumno;
                                asig.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona == 0 ? asig.IdClasificacionPersona : oportunidadNueva.IdClasificacionPersona;
                                asig.FechaModificacion = DateTime.Now;
                                asig.UsuarioModificacion = Usuario;
                                asigTemp = (_repAsignacionOportunidad.Update(asig));
                            }
                            _unitOfWork.Commit();

                            // COMPLETAR LOG DE ASIGNACIÓN
                            asignacionLog.IdTipoDato = asig.IdTipoDato;
                            asignacionLog.IdPersonal = asig.IdPersonal;
                            asignacionLog.IdFaseOportunidad = asig.IdFaseOportunidad;
                            asignacionLog.IdAlumno = asig.IdAlumno;
                            asignacionLog.IdClasificacionPersona = asig.IdClasificacionPersona;
                            asignacionLog.Estado = true;
                            asignacionLog.FechaCreacion = DateTime.Now;
                            asignacionLog.FechaModificacion = DateTime.Now;
                            asignacionLog.UsuarioCreacion = Usuario;
                            asignacionLog.UsuarioModificacion = Usuario;
                            asignacionLog.IdCentroCosto = asig.IdCentroCosto;
                            asignacionLog.IdAsignacionOportunidad = asigTemp.Id;
                            asignacionOportunidadLogService.Add(asignacionLog);

                            _unitOfWork.Commit();

                            // FINALIZAR ACTIVIDAD
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService
                                .ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            ActividadDetalleAntigua.Comentario = "Asignacion Manual";
                            ActividadDetalleAntigua.IdOcurrencia = OCURRENCIA_ASIGNACION_MANUAL;
                            ActividadDetalleAntigua.IdOcurrenciaAlterno = OCURRENCIA_ASIGNACION_MANUAL;
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdOcurrenciaActividadAlterno = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;
                            ActividadDetalleAntigua.FechaReal = DateTime.Now;
                            ActividadDetalleAntigua.UsuarioModificacion = Usuario;

                            if (oportunidadAntigua.FechaPaso2.HasValue)
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            if (oportunidadAntigua.Paso2.HasValue)
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            if (oportunidadAntigua.CodMailing != null)
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;

                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            oportunidadService.FinalizarActividades(false, Usuario);

                            if (AsignarAsesor.IdAsesor.Value != 0)
                                oportunidadService._asignacionManual.OportunidadLogNueva.IdAsesorAnt = AsignarAsesor.IdAsesor.Value;
                            if (fecha.HasValue)
                                oportunidadService._asignacionManual.OportunidadNueva.UltimaFechaProgramada = fecha.Value;
                            oportunidadService._asignacionManual.OportunidadLogNueva.VentaCruzadaMarketing = AsignarAsesor.VentaCruzadaMarketing == true;

                            oportunidadService.ProgramaActividad(AsignarAsesor.SegunMejorPro.GetValueOrDefault());

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            actividadDetalleService.Add(oportunidadService._asignacionManual.ActividadNuevaProgramarActividad);

                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();

                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            var agendaService = new AgendaService(_unitOfWork);
                            agendaService.EnviarCorreoOportunidadAutomatico(
                                oportunidadService._asignacionManual.OportunidadNueva.Id, 1967, "Automatico1967");

                            try
                            {
                                var item = new OportunidadWhatsappEnvioDTO
                                {
                                    IdOportunidad = asig.IdOportunidad.Value,
                                    IdPersonal = asig.IdPersonal.Value,
                                    IdCategoriaOrigen = oportunidadNueva.IdCategoriaOrigen.Value,
                                    AplicaEnvioWhatsapp = envioWhatsapp || oportunidadAntigua.IdPersonalAsignado == 125
                                };
                                listaOportunidades.Add(item);
                            }
                            catch { }

                            var ultimaActividad = _repActividadDetalle
                                                  .GetBy(w => w.IdOportunidad == idOportunidad,
                                                         w => new { w.Id, w.FechaCreacion })
                                                  .OrderByDescending(y => y.FechaCreacion)
                                                  .FirstOrDefault();
                            if (ultimaActividad != null)
                            {
                                _unitOfWork.OportunidadRepository
                                           .ActualizarIdActividadDetalleUltima(
                                                ultimaActividad.Id, idOportunidad);
                                _unitOfWork.Commit();
                            }

                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Logueo del error unitario
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = Usuario,
                            Maquina = "-",
                            Ruta = "AsignarAsesorAuto-OPORTUNIDAD",
                            Parametros = $"{idOportunidad},{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto}/{Usuario}",
                            Mensaje = ex.Message,
                            Excepcion = ex.ToString(),
                            Tipo = "ERROR",
                            IdPadre = 0,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                        // Sigue con la siguiente oportunidad
                    }
                }

                try
                {
                    foreach (var item in listaOportunidades)
                    {
                        if (item.AplicaEnvioWhatsapp)
                        {
                            var pais = _unitOfWork.AsignacionRegularRepository
                                                 .ObtenerPaisPorOportunidad(item.IdOportunidad);
                            if (new[] { 51, 56, 57, 52 }.Contains(pais.Id))
                                EnvioWhats(item.IdOportunidad, pais.Id, item.IdPersonal, item.IdCategoriaOrigen);
                        }
                    }
                }
                catch (Exception whatsEx)
                {
                    // Loguear pero sin abortar toda la asignación
                    _unitOfWork.LogRepository.Insert(new TLog
                    {
                        Ip = "-",
                        Usuario = Usuario,
                        Maquina = "-",
                        Ruta = "AsignarAsesorAuto-WHATSAPP",
                        Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto}/{Usuario}",
                        Mensaje = whatsEx.Message,
                        Excepcion = whatsEx.ToString(),
                        Tipo = "ERROR-ENVIOWSP-INTERNO",
                        IdPadre = 0,
                        UsuarioCreacion = Usuario,
                        UsuarioModificacion = Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    });
                }


                listaOportunidadesTemp = listaOportunidades;
                return new
                {
                    data = true,
                    OportunidadesAsesorAsignacionAutomatica = oportunidadesAsesorAsignacionAutomatica
                };
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog
                {
                    Ip = "-",
                    Usuario = "-",
                    Maquina = "-",
                    Ruta = "AsignarAsesorAuto-GENERAL",
                    Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto},{AsignarAsesor.IdOportunidades}/{Usuario}",
                    Mensaje = $"{ex.Message}-{(ex.InnerException?.Message ?? "No contiene InnerException")}",
                    Excepcion = ex.ToString(),
                    Tipo = "UPDATE",
                    IdPadre = 0,
                    UsuarioCreacion = "IntegraLog",
                    UsuarioModificacion = "IntegraLog",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                });
                return ex.Message;
            }
        }

        public object RegularizacionEnvioWhatsappAsignacion()
        {

            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                List<OportunidadWhatsappEnvioDTO> listaOportunidades = new List<OportunidadWhatsappEnvioDTO>();

                var oportunidadesPendientes = _repOportunidad.ObtenerOportunidadesPendientesEnvioWhatsapp();

                try
                {

                    foreach (var dato in oportunidadesPendientes)
                    {
                        OportunidadWhatsappEnvioDTO item = new OportunidadWhatsappEnvioDTO();
                        item.IdOportunidad = (int)dato.IdOportunidad;
                        item.IdPersonal = (int)dato.IdPersonal;
                        item.IdCategoriaOrigen = (int)dato.IdCategoriaOrigen;

                        item.AplicaEnvioWhatsapp = true;

                        listaOportunidades.Add(item);
                    }


                }
                catch (Exception e) { }

                try
                {
                    foreach (var item in listaOportunidades)
                    {
                        if (item.AplicaEnvioWhatsapp)
                        {
                            var pais = _unitOfWork.AsignacionRegularRepository.ObtenerPaisPorOportunidad((int)item.IdOportunidad);
                            //51:PERU,56:CHILE,57:COLOMBIA,52:MEXICO
                            if (pais.Id == 51 || pais.Id == 56 || pais.Id == 57 || pais.Id == 52)
                            {
                                EnvioWhats((int)item.IdOportunidad, pais.Id, (int)item.IdPersonal, (int)item.IdCategoriaOrigen);
                            }
                        }
                    }
                }
                catch (Exception e) { }


                return new { data = true, OportunidadesRegularizadas = listaOportunidades };
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Asignación de oportunidades por Asesor
        /// </summary>
        /// <param name="AsignarAsesor"> Lista de Id de oportunidades asignado a asesor </param>
        /// <param name="Usuario"> Usuario de módulo </param>
        /// <returns> Confirmación de asignación : Bool </returns>

        public object EnviarWhatsappPredictivas(int idOportunidad)
        {

            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
                var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                var _repAsignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

                var asignacionOportunidadLogNuevo = new AsignacionOportunidadLog();
                var nuevaActividad = new ActividadDetalle();
                var nuevaOportunidadLog = new OportunidadLog();
                var oportunidadActualizado = new Oportunidad();
                var parametrosRetorno = new Oportunidad();

                List<OportunidadWhatsappEnvioDTO> listaOportunidades = new List<OportunidadWhatsappEnvioDTO>();
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                {

                    //Actualizar Oportunidad con centro costo y/o asesor
                    Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                    try
                    {
                        OportunidadWhatsappEnvioDTO item = new OportunidadWhatsappEnvioDTO();
                        item.IdOportunidad = (int)oportunidadAntigua.Id;
                        item.IdPersonal = (int)oportunidadAntigua.IdPersonalAsignado;
                        item.IdCategoriaOrigen = (int)oportunidadAntigua.IdCategoriaOrigen;

                        listaOportunidades.Add(item);
                    }
                    catch (Exception e) { }

                    scope.Complete();
                }


                try
                {
                    foreach (var item in listaOportunidades)
                    {
                        var pais = _unitOfWork.AsignacionRegularRepository.ObtenerPaisPorOportunidad((int)item.IdOportunidad);
                        //51:PERU,56:CHILE,57:COLOMBIA,52:MEXICO
                        if (pais.Id == 51 || pais.Id == 56 || pais.Id == 57 || pais.Id == 52)
                        {
                            EnvioWhats((int)item.IdOportunidad, pais.Id, (int)item.IdPersonal, (int)item.IdCategoriaOrigen);
                        }
                    }
                }
                catch (Exception e) { }


                return new { data = true };
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor", Parametros = $"", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor:Margiory Ramirez
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OD
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool </returns>

        public object CerrarOportunidadOD(List<int> IdOportunidades, string Usuario)
        {
            try
            {

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        //using (TransactionScope scope = new TransactionScope())
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))

                        {
                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }



                            //AsignacionOportunidadManual opo = new AsignacionOportunidadManual();

                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                            //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();


                            //Finalizar Actividad

                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado OD";
                            ActividadDetalleAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("Cerrado Fase OD"); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;


                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);
                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            //_repOportunidad.Update(portu);
                            //_unitOfWork.Commit();

                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOD", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOD", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase OM
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>

        public object CerrarOportunidadOM(List<int> IdOportunidades, string Usuario)
        {
            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

                foreach (int idOportunidad in IdOportunidades)
                {
                    try
                    {
                        //using (TransactionScope scope = new TransactionScope())
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));


                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");

                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }


                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;


                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();

                            //Finalizar Actividad
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado Fase OM";
                            ActividadDetalleAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("Cerrado Fase OM"); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;



                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);
                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            //_repOportunidad.Update(portu);
                            //_unitOfWork.Commit();

                            scope.Complete();


                        }
                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOM", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadOM", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase RN5
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>

        public object CerrarOportunidadRN5(List<int> IdOportunidades, string Usuario)
        {
            try
            {

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");


                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                            //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            //if (oportunidadAntigua.IdPagina.HasValue)
                            //    oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();
                            // Esto asigna 0 si IdPagina es null


                            //Finalizar Actividad

                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado Fase RN5";
                            ActividadDetalleAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("130. Cliente no responde correo (RN5)"); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;


                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);
                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            //_repOportunidad.Update(portu);
                            //_unitOfWork.Commit();


                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadRN5", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadRN5", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: 
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase E
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>

        public object CerrarOportunidadE(List<int> IdOportunidades, string Usuario)
        {

            try
            {

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");


                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }

                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                            //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();


                            //Finalizar Actividad

                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado Fase E";
                            ActividadDetalleAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("67. Número no le pertenece (E)"); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;


                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);

                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            //_repOportunidad.Update(portu);
                            //_unitOfWork.Commit();

                            scope.Complete();



                        }
                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadE", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadE", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }



        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase BIC
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>

        public object CerrarOportunidadBic(List<int> IdOportunidades, string Usuario)
        {
            try
            {

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));


                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");


                            try


                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }


                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado Fase BIC";
                            ActividadDetalleAntigua.IdOcurrencia = 34;
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdEstadoActividadDetalle = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;



                            Oportunidad oportunidadDTO = new Oportunidad();
                            //oportunidadDTO.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            //oportunidadDTO.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            //  oportunidadDTO.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc.Value;
                            //  oportunidadDTO.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp.Value;
                            //  oportunidadDTO.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf.Value;
                            //  oportunidadDTO.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            //  oportunidadDTO.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            //  oportunidadDTO.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            //  oportunidadDTO.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;

                            oportunidadDTO = oportunidadAntigua;

                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadDTO;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;




                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);

                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);
                            //oportunidadDTO.FasesActivas = Oportunidad.fasesac;




                            // _repOportunidad.Update(oportunidadDTO.oportunidad);
                            scope.Complete();
                        }

                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBic", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception e)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBic", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Fase de Oportunidad en fase BRM1
        /// </summary>
        /// <returns> Confirmación de cambio de Fase : Bool  </returns>

        public object CerrarOportunidadBRM1(List<int> IdOportunidades, string Usuario)
        {
            try
            {

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");


                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }


                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                            //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();

                            //Finalizar Actividad


                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado  Fase BRM1";
                            ActividadDetalleAntigua.IdOcurrencia = 327;
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;


                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);
                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);





                            scope.Complete();
                        }

                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBRM1", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadBRM1", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }





        /// TipoFuncion: POST
        /// Autor:   Margiory Ramirez
        /// Fecha: 06/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra Oportunidad en NS
        /// </summary>
        /// <returns> Confirmación de inserción : Bool </returns>

        public object CerrarOportunidadNS(List<int> IdOportunidades, string Usuario)
        {
            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);


                foreach (var idOportunidad in IdOportunidades)
                {
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                        {

                            oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();
                            //var Oportunidad = _repOportunidad.FirstById(idOportunidad);

                            Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));

                            //Oportunidad oportunidades = new Oportunidad();
                            if (oportunidadAntigua == null)
                                throw new Exception("No existe oportunidad!");


                            try
                            {
                                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                            }
                            catch (Exception ex)
                            {
                            }



                            Oportunidad oportunidadNueva = new Oportunidad();
                            oportunidadNueva.Id = oportunidadAntigua.Id;
                            oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                            oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                            oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                            oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                            oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                            oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                            oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                            oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                            oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                            oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                            oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                            oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                            oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                            oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                            oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                            oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                            oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                            oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                            oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                            oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                            oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                            oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                            oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                            oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                            oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                            oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                            oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                            oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                            oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                            oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                            oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                            oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                            oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                            oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                            oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                            oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                            oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                            oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                            oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                            oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                            oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;

                            oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                            //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                            oportunidadNueva.Estado = true;
                            oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                            //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                            if (oportunidadAntigua.FechaPaso2 != null)
                            {
                                oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                            }
                            if (oportunidadAntigua.Paso2 != null)
                            {
                                oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                            }
                            if (oportunidadAntigua.CodMailing != null)
                            {
                                oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                            }
                            oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();
                            //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;




                            var ActividadAntigua = new ActividadDetalle();
                            ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                            //ActividadDetalleAntigua.Id = oportunidadNueva.IdActividadDetalleUltima;
                            ActividadDetalleAntigua.Comentario = "Cerrado NS";
                            ActividadDetalleAntigua.IdOcurrencia = _repOcurrencia.ObtenerOcurrenciaPorNombre("242. No solicitó información (NS)  "); //"B42B5A91-ADB4-C47A-9557-08D30721ED66";// 3. No le interesa en este momento, pero le interesa para los próximos meses (RN2)
                            ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                            ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                            ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                            ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                            ActividadDetalleAntigua.IdCentralLlamada = 0;
                            ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;


                            oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                            oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                            oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;

                            //var portu = serOportunidad.MapeoEntidadDesdeDTO(Oportunidad);
                            oportunidadService.FinalizarActividades(false, Usuario);

                            actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                            //actividadDetalleService.Add(oportunidadService.asignacionManual.ActividadNuevaProgramarActividad);
                            _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                            _unitOfWork.Commit();
                            oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                            //_repOportunidad.Update(portu);
                            //_unitOfWork.Commit();







                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        var _repLog = _unitOfWork.LogRepository;

                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadNS", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                        return (e.Message);
                    }

                }

                return (true);
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;

                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CerrarOportunidadNS", Parametros = $"{IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }
        public bool EnvioWhats(int idOportunidad, int idPais, int idPersonal, int IdCategoriaOrigen)
        {
            try
            {

                var alumno = _unitOfWork.AsignacionRegularRepository.ObtenerAlumnoPorOportunidad(idOportunidad);

                var validador = _unitOfWork.AsignacionRegularRepository.ValidarEnvioPorDias(alumno[0].Celular);

                //if (validador == null) { 
                var asesores = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresActivos();

                foreach (var asesor in asesores)
                {
                    if (asesor.Id == idPersonal)
                    {
                        DateTime currentTime = DateTime.Now;

                        ReemplazoPlantillaDTO plantilla = new ReemplazoPlantillaDTO();

                        var pgeneral = _unitOfWork.AsignacionRegularRepository.ObtenerPGeneralPorOportunidad(idOportunidad);

                        var idplantilla = 0;

                        if (currentTime.Hour >= 8 && currentTime.Hour <= 20)
                        {
                            plantilla = ReemplazarEtiquetas(1701, IdCategoriaOrigen, idPersonal, pgeneral.IdPGeneral, alumno[0].Nombre);

                            try
                            {
                                if (plantilla.textoPlantilla != null)
                                {
                                    //Se limpia las tildes
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("á", "a");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("é", "e");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("í", "i");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("ó", "o");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("ú", "u");

                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("Á", "A");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("É", "E");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("Í", "I");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("Ó", "O");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("Ú", "U");

                                    ////Elimina las Ñ
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("ñ", "n");
                                    //plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("Ñ", "N");

                                    plantilla.textoPlantilla = plantilla.textoPlantilla.Replace("?", " ");
                                    //fin se limpia las tildes
                                }
                            }
                            catch (Exception ex) { }

                            idplantilla = 1701;
                            //foreach (var item in plantilla.datos)
                            //{
                            //    //Elimina los caracteres con tilde
                            //    item.texto = item.texto.Replace("á", "a");
                            //    item.texto = item.texto.Replace("é", "e");
                            //    item.texto = item.texto.Replace("í", "i");
                            //    item.texto = item.texto.Replace("ó", "o");
                            //    item.texto = item.texto.Replace("ú", "u");

                            //    item.texto = item.texto.Replace("Á", "A");
                            //    item.texto = item.texto.Replace("É", "E");
                            //    item.texto = item.texto.Replace("Í", "I");
                            //    item.texto = item.texto.Replace("Ó", "O");
                            //    item.texto = item.texto.Replace("Ú", "U");

                            //    //Elimina las Ñ
                            //    item.texto = item.texto.Replace("ñ", "n");
                            //    item.texto = item.texto.Replace("Ñ", "N");
                            //}
                            List<string> ordenDeseado = new List<string>
                            {
                                "{tAlumno.Nombre1}",
                                "{tPersonal.NombreCompleto}",
                                "{tPegeneral.Nombre}",
                                "{tPersonal.proveedor}",
                            };

                            plantilla.datos = plantilla.datos.OrderBy(d => ordenDeseado.IndexOf(d.codigo)).ToList();

                            var plantillab = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idplantilla);

                            WhatsAppMensajePlantillaComDTO envioWhats = new WhatsAppMensajePlantillaComDTO();


                            if (alumno[0].Celular.Length > 0)
                            {
                                string codigoPais = idPais.ToString();

                                // Verificar si el número empieza con "00" seguido del prefijo del país
                                if (alumno[0].Celular.StartsWith("00" + codigoPais))
                                {
                                    alumno[0].Celular = alumno[0].Celular.Substring(2); // Limpiar "00"
                                }
                                else if (!alumno[0].Celular.StartsWith(codigoPais))
                                {
                                    alumno[0].Celular = codigoPais + alumno[0].Celular;
                                }
                            }

                            envioWhats.WaTo = alumno[0].Celular;
                            envioWhats.WaTypeMensaje = 8;
                            envioWhats.WaBody = plantillab.Descripcion;
                            envioWhats.IdPlantilla = idplantilla;
                            envioWhats.WaCaption = plantilla.textoPlantilla;
                            envioWhats.IdPais = idPais;
                            envioWhats.IdAlumno = alumno[0].Id;
                            envioWhats.DatosPlantillaWhatsApp = plantilla.datos;

                            WhatsAppMensajeEnviadoApiComercialService whatsAppMensajesService = new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork);

                            whatsAppMensajesService.EnvioMensajePorPlantilla(envioWhats, "WhatsappAsesor", idPersonal);
                        }
                    }
                }

                return true;

                //}

                //else
                //{
                //    return false; 
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioWhatsAppTercerDiaSinContacto(int idOportunidad, int idPais, int idPersonal, int IdCategoriaOrigen, ContadorBic contador)
        {
            try
            {

                var alumno = _unitOfWork.AsignacionRegularRepository.ObtenerAlumnoPorOportunidad(idOportunidad);

                var validador = _unitOfWork.AsignacionRegularRepository.ValidarEnvioPorDias(alumno[0].Celular);

                //if (validador == null)
                //{
                DateTime currentTime = DateTime.Now;

                ReemplazoPlantillaDTO plantilla = new ReemplazoPlantillaDTO();

                //var contador = _unitOfWork.AsignacionRegularRepository.ObtenerContadorBic(idOportunidad);
                var pgeneral = _unitOfWork.AsignacionRegularRepository.ObtenerPGeneralPorOportunidad(idOportunidad);

                var idplantilla = 0;

                if ((contador.DiasSinContactoManhana == 3) || (contador.DiasSinContactoTarde == 3))
                {

                    plantilla = ReemplazarEtiquetas(1702, IdCategoriaOrigen, idPersonal, pgeneral.IdPGeneral, alumno[0].Nombre);
                    idplantilla = 1702;

                    //foreach (var item in plantilla.datos)
                    //{
                    //    //Elimina los caracteres con tilde
                    //    item.texto = item.texto.Replace("á", "a");
                    //    item.texto = item.texto.Replace("é", "e");
                    //    item.texto = item.texto.Replace("í", "i");
                    //    item.texto = item.texto.Replace("ó", "o");
                    //    item.texto = item.texto.Replace("ú", "u");

                    //    item.texto = item.texto.Replace("Á", "A");
                    //    item.texto = item.texto.Replace("É", "E");
                    //    item.texto = item.texto.Replace("Í", "I");
                    //    item.texto = item.texto.Replace("Ó", "O");
                    //    item.texto = item.texto.Replace("Ú", "U");

                    //    //Elimina las Ñ
                    //    item.texto = item.texto.Replace("ñ", "n");
                    //    item.texto = item.texto.Replace("Ñ", "N");
                    //}

                    var plantillab = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idplantilla);

                    WhatsAppMensajePlantillaComDTO envioWhats = new WhatsAppMensajePlantillaComDTO();

                    envioWhats.WaTo = alumno[0].Celular;
                    envioWhats.WaTypeMensaje = 8;
                    envioWhats.WaBody = plantillab.Descripcion;
                    envioWhats.IdPlantilla = idplantilla;
                    envioWhats.WaCaption = plantilla.textoPlantilla;
                    envioWhats.IdPais = idPais;
                    envioWhats.IdAlumno = alumno[0].Id;
                    envioWhats.DatosPlantillaWhatsApp = plantilla.datos;


                    WhatsAppMensajeEnviadoApiComercialService whatsAppMensajesService = new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork);

                    whatsAppMensajesService.EnvioMensajePorPlantilla(envioWhats, "WhatsappAsesor", idPersonal);

                }


                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ReemplazoPlantillaDTO ReemplazarEtiquetas(int idPlantilla, int idCategoria, int idPersonal, int idPGeneral, string nombre)
        {
            try
            {

                var categoria = _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(idCategoria);
                var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);
                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idPlantilla);


                List<DatosPlantillaWhatsAppDTO> objetoPlantilla = new List<DatosPlantillaWhatsAppDTO>();
                var objetito = new DatosPlantillaWhatsAppDTO();

                objetito.codigo = "{tAlumno.Nombre1}";
                objetito.texto = nombre;

                objetoPlantilla.Add(objetito);

                plantilla.Texto = plantilla.Texto.Replace("{tAlumno.Nombre1}", nombre);

                if (plantilla.Texto.Contains("{tPersonal.NombreCompleto}"))
                {


                    var objet11 = new DatosPlantillaWhatsAppDTO();

                    objet11.codigo = "{tPersonal.NombreCompleto}";
                    objet11.texto = $"{personal.Nombre1} {personal.Nombre2} {personal.ApellidoPaterno} {personal.ApellidoMaterno}";

                    objetoPlantilla.Add(objet11);

                    plantilla.Texto = plantilla.Texto.Replace("{tPersonal.NombreCompleto}", $"{personal.Nombre1} {personal.Nombre2} {personal.ApellidoPaterno}  {personal.ApellidoMaterno}");



                }

                if (plantilla.Texto.Contains("{tPegeneral.Nombre}"))
                {

                    var objet10 = new DatosPlantillaWhatsAppDTO();

                    objet10.codigo = "{tPegeneral.Nombre}";
                    objet10.texto = pgeneral.Nombre;

                    objetoPlantilla.Add(objet10);

                    plantilla.Texto = plantilla.Texto.Replace("{tPegeneral.Nombre}", pgeneral.Nombre);


                }


                if (plantilla.Texto.Contains("{tPersonal.proveedor}"))
                {
                    if (categoria.Descripcion == "Datos provenientes de envios Masivos de Mensajes de Texto" ||
                        categoria.Descripcion == "Formulario Version de Prueba - Sitio Web" ||
                        categoria.Descripcion == "Formulario a partir de aviso en Google" ||
                        categoria.Descripcion == "Formulario de Contactenos - Sitio Web" ||
                        categoria.Descripcion == "Formulario de Registro - Sitio Web" ||
                        categoria.Descripcion == "Fomulario - Sitio Web" ||
                        categoria.Descripcion == "Formulario a partir de aviso de Google" ||
                        categoria.Descripcion == "Formulario - Sitio Web" ||
                        categoria.Descripcion == "Formulario  - Sitio Web" ||
                        categoria.Descripcion == "Formulario Contactenos - Sitio Web" ||
                        categoria.Descripcion == "Formulario Versión Prueba - Sitio Web" ||
                        categoria.Descripcion == "Formularios de Clientes Potenciales de Google" ||
                        categoria.Descripcion == "Mensaje de Texto")
                    {

                        var objet1 = new DatosPlantillaWhatsAppDTO();

                        objet1.codigo = "{tPersonal.proveedor}";
                        objet1.texto = "Sitio Web";

                        objetoPlantilla.Add(objet1);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Sitio Web");

                    }
                    else if (
                        categoria.Descripcion == "Marcador Predictivo Bases Propias" ||
                        categoria.Descripcion == "Llamada Oficina")
                    {

                        var objet2 = new DatosPlantillaWhatsAppDTO();

                        objet2.codigo = "{tPersonal.proveedor}";
                        objet2.texto = "Llamada Telefonica";

                        objetoPlantilla.Add(objet2);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Llamada Telefonica");


                    }
                    else if (
                        categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Remarketing" ||
                        categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Publico abierto" ||
                        categoria.Descripcion == "Respuesta por Whatsapp de correos Mailing" ||
                        categoria.Descripcion == "Whatsapp")
                    {

                        var objet3 = new DatosPlantillaWhatsAppDTO();

                        objet3.codigo = "{tPersonal.proveedor}";
                        objet3.texto = "Whatsapp";

                        objetoPlantilla.Add(objet3);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Whatsapp");


                    }
                    else if (
                        categoria.Descripcion == "Chat - Sitio Web")
                    {

                        var objet4 = new DatosPlantillaWhatsAppDTO();

                        objet4.codigo = "{tPersonal.proveedor}";
                        objet4.texto = "Chat de Nuestro Sitio Web";

                        objetoPlantilla.Add(objet4);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Chat de Nuestro Sitio Web");


                    }
                    else if (

                        categoria.Descripcion == "Mailing Rpta - Marketing" ||
                        categoria.Descripcion == "Formulario a partir de correo electronico" ||
                        categoria.Descripcion == "Mailing")
                    {

                        var objet5 = new DatosPlantillaWhatsAppDTO();

                        objet5.codigo = "{tPersonal.proveedor}";
                        objet5.texto = "Correo Electronico";

                        objetoPlantilla.Add(objet5);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Correo Electronico");


                    }
                    else if (
                        categoria.Descripcion == "Datos de Inbox Comentarios - Instangram" ||
                        categoria.Descripcion == "Comentarios de Instagram" ||
                        categoria.Descripcion == "Mensajes de Facebook" ||
                        categoria.Descripcion == "Comentarios Chat")
                    {

                        var objet6 = new DatosPlantillaWhatsAppDTO();

                        objet6.codigo = "{tPersonal.proveedor}";
                        objet6.texto = "Inbox o Comentarios";

                        objetoPlantilla.Add(objet6);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Inbox o Comentarios");

                    }
                    else if (
                        categoria.Descripcion == "Formulario a partir de aviso en Facebook" ||
                        categoria.Descripcion == "Comentarios de Facebook")
                    {

                        var objet7 = new DatosPlantillaWhatsAppDTO();

                        objet7.codigo = "{tPersonal.proveedor}";
                        objet7.texto = "Facebook";

                        objetoPlantilla.Add(objet7);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Facebook");


                    }
                    else if (
                        categoria.Descripcion == "Twitter")
                    {

                        var objet8 = new DatosPlantillaWhatsAppDTO();

                        objet8.codigo = "{tPersonal.proveedor}";
                        objet8.texto = "Twitter";

                        objetoPlantilla.Add(objet8);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Twitter");


                    }
                    else if (
                        categoria.Descripcion == "Formulario a partir de aviso en Instagram")
                    {

                        var objet9 = new DatosPlantillaWhatsAppDTO();

                        objet9.codigo = "{tPersonal.proveedor}";
                        objet9.texto = "Instagram";

                        objetoPlantilla.Add(objet9);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Instagram");


                    }
                    else if (
                        categoria.Descripcion == "Formulario de LinkedIn")
                    {

                        var objet9 = new DatosPlantillaWhatsAppDTO();

                        objet9.codigo = "{tPersonal.proveedor}";
                        objet9.texto = "LinkedIn";

                        objetoPlantilla.Add(objet9);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "LinkedIn");


                    }
                    else if (
                        categoria.Descripcion == "Historico")
                    {

                        var objet9 = new DatosPlantillaWhatsAppDTO();

                        objet9.codigo = "{tPersonal.proveedor}";
                        objet9.texto = "Web";

                        objetoPlantilla.Add(objet9);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Web");


                    }
                    else if (
                        categoria.Descripcion == "Referido")
                    {

                        var objet9 = new DatosPlantillaWhatsAppDTO();

                        objet9.codigo = "{tPersonal.proveedor}";
                        objet9.texto = "Referido";

                        objetoPlantilla.Add(objet9);

                        plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Referido");


                    }
                }



                ReemplazoPlantillaDTO datos = new ReemplazoPlantillaDTO();

                datos.datos = objetoPlantilla;
                datos.textoPlantilla = plantilla.Texto;

                return datos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public PlantillaAsig ReemplazarEtiquetas(int idPlantilla, int idCategoria, int idPersonal, int idPGeneral)
        //{
        //    try
        //    {
        //        var categoria = _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(idCategoria);
        //        var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idPlantilla);
        //        var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral);
        //        var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);

        //        if (plantilla.Texto.Contains("{tPersonal.proveedor}"))
        //        {
        //            if (categoria.Descripcion == "Datos provenientes de envios Masivos de Mensajes de Texto" ||
        //                categoria.Descripcion == "Formulario Version de Prueba - Sitio Web" ||
        //                categoria.Descripcion == "Formulario a partir de aviso en Google" ||
        //                categoria.Descripcion == "Formulario de Contactenos - Sitio Web" ||
        //                categoria.Descripcion == "Formulario de Registro - Sitio Web" ||
        //                categoria.Descripcion == "Fomulario - Sitio Web" ||
        //                categoria.Descripcion == "Formulario a partir de aviso de Google" ||
        //                categoria.Descripcion == "Formulario - Sitio Web" ||
        //                categoria.Descripcion == "Formulario Contactenos - Sitio Web" ||
        //                categoria.Descripcion == "Formulario Versión Prueba - Sitio Web" ||
        //                categoria.Descripcion == "Formularios de Clientes Potenciales de Google" ||
        //                categoria.Descripcion == "Mensaje de Texto")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Sitio Web");
        //            }
        //            else if (
        //                categoria.Descripcion == "Marcador Predictivo Bases Propias" ||
        //                categoria.Descripcion == "Llamada Oficina")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Lllamada Telefonica");
        //            }
        //            else if (
        //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Remarketing" ||
        //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Publico abierto" ||
        //                categoria.Descripcion == "Respuesta por Whatsapp de correos Mailing" ||
        //                categoria.Descripcion == "Whatsapp")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Whatsapp");
        //            }
        //            else if (
        //                categoria.Descripcion == "Chat - Sitio Web")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Chat de Nuestro Sitio Web");
        //            }
        //            else if (
        //                categoria.Descripcion == "Mailing Rpta - Marketing" ||
        //                categoria.Descripcion == "Formulario a partir de correo electronico" ||
        //                categoria.Descripcion == "Mailing")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Correo Electronico");
        //            }
        //            else if (
        //                categoria.Descripcion == "Datos de Inbox Comentarios - Instangram" ||
        //                categoria.Descripcion == "Comentarios de Instagram" ||
        //                categoria.Descripcion == "Mensajes de Facebook" ||
        //                categoria.Descripcion == "Comentarios Chat")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Inbox o Comentarios");
        //            }
        //            else if (
        //                categoria.Descripcion == "Formulario a partir de aviso en Facebook" ||
        //                categoria.Descripcion == "Comentarios de Facebook")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Facebook");
        //            }
        //            else if (
        //                categoria.Descripcion == "Twitter")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Twitter");
        //            }
        //            else if (
        //                categoria.Descripcion == "Formulario a partir de aviso en Instagram")
        //            {
        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Instagram");
        //            }
        //        }
        //        if (plantilla.Texto.Contains("{tPegeneral.Nombre}"))
        //        {
        //            plantilla.Texto = plantilla.Texto.Replace("{tPegeneral.Nombre}", pgeneral.Nombre);
        //        }
        //        if (plantilla.Texto.Contains("{tPersonal.NombreCompleto}"))
        //        {
        //            plantilla.Texto = plantilla.Texto.Replace("{tPersonal.NombreCompleto}", $"{personal.Nombres} {personal.ApellidoPaterno} {personal.ApellidoMaterno}");
        //        }


        //        PlantillaAsig plantillaDatos = new PlantillaAsig();

        //        plantillaDatos.Nombre = plantilla.Nombre;
        //        plantillaDatos.Texto = plantilla.Texto;
        //        plantillaDatos.Descripcion = plantilla.Descripcion;

        //        return plantillaDatos;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public ReemplazoPlantillaDTO ReemplazarEtiquetas(int idPlantilla, int idCategoria, int idPersonal, int idPGeneral, string nombre)
        //{
        //    try
        //    {
        //        var categoria = _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(idCategoria);
        //        var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral);
        //        var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);
        //        var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idPlantilla);


        //        List<DatosPlantillaWhatsAppDTO> objetoPlantilla = new List<DatosPlantillaWhatsAppDTO>();
        //        var objetito = new DatosPlantillaWhatsAppDTO();

        //        objetito.codigo = "{tAlumno.Nombre1}";
        //        objetito.texto = nombre;

        //        objetoPlantilla.Add(objetito);

        //        plantilla.Texto = plantilla.Texto.Replace("{tAlumno.Nombre1}", nombre);

        //        if (plantilla.Texto.Contains("{tPersonal.NombreCompleto}"))
        //        {
        //            var objet11 = new DatosPlantillaWhatsAppDTO();

        //            objet11.codigo = "{tPersonal.NombreCompleto}";
        //            objet11.texto = $"{personal.Nombres} {personal.ApellidoPaterno} {personal.ApellidoMaterno}";

        //            objetoPlantilla.Add(objet11);

        //            plantilla.Texto = plantilla.Texto.Replace("{tPersonal.NombreCompleto}", $"{personal.Nombres} {personal.ApellidoPaterno} {personal.ApellidoMaterno}");

        //        }

        //        if (plantilla.Texto.Contains("{tPegeneral.Nombre}"))
        //        {

        //            var objet10 = new DatosPlantillaWhatsAppDTO();

        //            objet10.codigo = "{tPegeneral.Nombre}";
        //            objet10.texto = pgeneral.Nombre;

        //            objetoPlantilla.Add(objet10);

        //            plantilla.Texto = plantilla.Texto.Replace("{tPegeneral.Nombre}", pgeneral.Nombre);


        //        }


        //        if (plantilla.Texto.Contains("{tPersonal.proveedor}"))
        //        {
        //            if (categoria.Descripcion == "Datos provenientes de envios Masivos de Mensajes de Texto" ||
        //                categoria.Descripcion == "Formulario Version de Prueba - Sitio Web" ||
        //                categoria.Descripcion == "Formulario a partir de aviso en Google" ||
        //                categoria.Descripcion == "Formulario de Contactenos - Sitio Web" ||
        //                categoria.Descripcion == "Formulario de Registro - Sitio Web" ||
        //                categoria.Descripcion == "Fomulario - Sitio Web" ||
        //                categoria.Descripcion == "Formulario a partir de aviso de Google" ||
        //                categoria.Descripcion == "Formulario - Sitio Web" ||
        //                categoria.Descripcion == "Formulario  - Sitio Web" ||
        //                categoria.Descripcion == "Formulario Contactenos - Sitio Web" ||
        //                categoria.Descripcion == "Formulario Versión Prueba - Sitio Web" ||
        //                categoria.Descripcion == "Formularios de Clientes Potenciales de Google" ||
        //                categoria.Descripcion == "Mensaje de Texto")
        //            {

        //                var objet1 = new DatosPlantillaWhatsAppDTO();

        //                objet1.codigo = "{tPersonal.proveedor}";
        //                objet1.texto = "Sitio Web";

        //                objetoPlantilla.Add(objet1);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Sitio Web");

        //            }
        //            else if (
        //                categoria.Descripcion == "Marcador Predictivo Bases Propias" ||
        //                categoria.Descripcion == "Llamada Oficina")
        //            {

        //                var objet2 = new DatosPlantillaWhatsAppDTO();

        //                objet2.codigo = "{tPersonal.proveedor}";
        //                objet2.texto = "Llamada Telefonica";

        //                objetoPlantilla.Add(objet2);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Llamada Telefonica");


        //            }
        //            else if (
        //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Remarketing" ||
        //                categoria.Descripcion == "Datos ingresados de facebook redirigidos a whatsapp - Publico abierto" ||
        //                categoria.Descripcion == "Respuesta por Whatsapp de correos Mailing" ||
        //                categoria.Descripcion == "Whatsapp")
        //            {

        //                var objet3 = new DatosPlantillaWhatsAppDTO();

        //                objet3.codigo = "{tPersonal.proveedor}";
        //                objet3.texto = "Whatsapp";

        //                objetoPlantilla.Add(objet3);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Whatsapp");


        //            }
        //            else if (
        //                categoria.Descripcion == "Chat - Sitio Web")
        //            {

        //                var objet4 = new DatosPlantillaWhatsAppDTO();

        //                objet4.codigo = "{tPersonal.proveedor}";
        //                objet4.texto = "Chat de Nuestro Sitio Web";

        //                objetoPlantilla.Add(objet4);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Chat de Nuestro Sitio Web");


        //            }
        //            else if (

        //                categoria.Descripcion == "Mailing Rpta - Marketing" ||
        //                categoria.Descripcion == "Formulario a partir de correo electronico" ||
        //                categoria.Descripcion == "Mailing")
        //            {

        //                var objet5 = new DatosPlantillaWhatsAppDTO();

        //                objet5.codigo = "{tPersonal.proveedor}";
        //                objet5.texto = "Correo Electronico";

        //                objetoPlantilla.Add(objet5);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Correo Electronico");


        //            }
        //            else if (
        //                categoria.Descripcion == "Datos de Inbox Comentarios - Instangram" ||
        //                categoria.Descripcion == "Comentarios de Instagram" ||
        //                categoria.Descripcion == "Mensajes de Facebook" ||
        //                categoria.Descripcion == "Comentarios Chat")
        //            {

        //                var objet6 = new DatosPlantillaWhatsAppDTO();

        //                objet6.codigo = "{tPersonal.proveedor}";
        //                objet6.texto = "Inbox o Comentarios";

        //                objetoPlantilla.Add(objet6);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Inbox o Comentarios");

        //            }
        //            else if (
        //                categoria.Descripcion == "Formulario a partir de aviso en Facebook" ||
        //                categoria.Descripcion == "Comentarios de Facebook")
        //            {

        //                var objet7 = new DatosPlantillaWhatsAppDTO();

        //                objet7.codigo = "{tPersonal.proveedor}";
        //                objet7.texto = "Facebook";

        //                objetoPlantilla.Add(objet7);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Facebook");


        //            }
        //            else if (
        //                categoria.Descripcion == "Twitter")
        //            {

        //                var objet8 = new DatosPlantillaWhatsAppDTO();

        //                objet8.codigo = "{tPersonal.proveedor}";
        //                objet8.texto = "Twitter";

        //                objetoPlantilla.Add(objet8);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Twitter");


        //            }
        //            else if (
        //                categoria.Descripcion == "Formulario a partir de aviso en Instagram")
        //            {

        //                var objet9 = new DatosPlantillaWhatsAppDTO();

        //                objet9.codigo = "{tPersonal.proveedor}";
        //                objet9.texto = "Instagram";

        //                objetoPlantilla.Add(objet9);

        //                plantilla.Texto = plantilla.Texto.Replace("{tPersonal.proveedor}", "Instagram");


        //            }
        //        }



        //        ReemplazoPlantillaDTO datos = new ReemplazoPlantillaDTO();

        //        datos.datos = objetoPlantilla;
        //        datos.textoPlantilla = plantilla.Texto;

        //        return datos;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
   
    public object AsignarAsesorFechaProgramacion(AsignarAsesorManuaWhatsapplDTO AsignarAsesor, string Usuario)
    {

            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
                var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                var _repAsignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository;

                var oportunidadService = new OportunidadService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                var oportunidadLogService = new OportunidadLogService(_unitOfWork);
                var asignacionOportunidadLogService = new AsignacionOportunidadLogService(_unitOfWork);

                var asignacionOportunidadLogNuevo = new AsignacionOportunidadLog();
                var nuevaActividad = new ActividadDetalle();
                var nuevaOportunidadLog = new OportunidadLog();
                var oportunidadActualizado = new Oportunidad();
                var parametrosRetorno = new Oportunidad();


                List<OportunidadesAsesorAsignacionAutomaticaDTO> oportunidadesAsesorAsignacionAutomatica = new List<OportunidadesAsesorAsignacionAutomaticaDTO>();
                DateTime? fecha;
                string? comentario;

                if (!AsignarAsesor.IdAsesor.HasValue) AsignarAsesor.IdAsesor = 0;
                if (!AsignarAsesor.IdCentroCosto.HasValue) AsignarAsesor.IdCentroCosto = 0;
                if (!AsignarAsesor.FechaProgramada.HasValue) fecha = null; else fecha = AsignarAsesor.FechaProgramada;
                comentario = !string.IsNullOrEmpty(AsignarAsesor.Comentario) ? AsignarAsesor.Comentario : string.Empty;


                oportunidadService._asignacionManual = new AsignacionOportunidadManualDTO();

                var oportunidadesFaltantes = AsignarAsesor.IdOportunidades.ToList();
                List<OportunidadWhatsappEnvioDTO> listaOportunidades = new List<OportunidadWhatsappEnvioDTO>();

                var envioWhatsapp = AsignarAsesor.envioWhats == null ? false : AsignarAsesor.envioWhats.Value;

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new System.TimeSpan(0, 15, 0)))
                {
                    foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                    {
                        //Actualizar Oportunidad con centro costo y/o asesor
                        Oportunidad oportunidadAntigua = _mapper.Map<Oportunidad>(_repOportunidad.FirstById(idOportunidad));
                        Oportunidad oportunidadNueva = new Oportunidad();
                        oportunidadNueva.Id = oportunidadAntigua.Id;
                        oportunidadNueva.IdCentroCosto = oportunidadAntigua.IdCentroCosto;
                        oportunidadNueva.IdPersonalAsignado = oportunidadAntigua.IdPersonalAsignado;
                        oportunidadNueva.IdTipoDato = oportunidadAntigua.IdTipoDato;
                        oportunidadNueva.IdFaseOportunidad = oportunidadAntigua.IdFaseOportunidad;
                        oportunidadNueva.IdOrigen = oportunidadAntigua.IdOrigen;
                        oportunidadNueva.IdAlumno = oportunidadAntigua.IdAlumno;
                        oportunidadNueva.UltimoComentario = oportunidadAntigua.UltimoComentario;
                        oportunidadNueva.IdActividadDetalleUltima = oportunidadAntigua.IdActividadDetalleUltima;
                        oportunidadNueva.IdActividadCabeceraUltima = oportunidadAntigua.IdActividadCabeceraUltima;
                        oportunidadNueva.IdEstadoActividadDetalleUltimoEstado = oportunidadAntigua.IdEstadoActividadDetalleUltimoEstado;
                        oportunidadNueva.UltimaFechaProgramada = oportunidadAntigua.UltimaFechaProgramada;
                        oportunidadNueva.IdEstadoOportunidad = oportunidadAntigua.IdEstadoOportunidad;
                        oportunidadNueva.IdEstadoOcurrenciaUltimo = oportunidadAntigua.IdEstadoOcurrenciaUltimo;
                        oportunidadNueva.IdFaseOportunidadMaxima = oportunidadAntigua.IdFaseOportunidadMaxima;
                        oportunidadNueva.IdFaseOportunidadInicial = oportunidadAntigua.IdFaseOportunidadInicial;
                        oportunidadNueva.IdCategoriaOrigen = oportunidadAntigua.IdCategoriaOrigen;
                        oportunidadNueva.IdConjuntoAnuncio = oportunidadAntigua.IdConjuntoAnuncio;
                        oportunidadNueva.IdCampaniaScoring = oportunidadAntigua.IdCampaniaScoring;
                        oportunidadNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                        oportunidadNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                        oportunidadNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                        oportunidadNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                        oportunidadNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                        oportunidadNueva.FechaRegistroCampania = oportunidadAntigua.FechaRegistroCampania;
                        oportunidadNueva.IdFaseOportunidadPortal = oportunidadAntigua.IdFaseOportunidadPortal;
                        oportunidadNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                        oportunidadNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                        oportunidadNueva.FlagVentaCruzada = oportunidadAntigua.FlagVentaCruzada;
                        oportunidadNueva.IdTiempoCapacitacion = oportunidadAntigua.IdTiempoCapacitacion;
                        oportunidadNueva.IdTiempoCapacitacionValidacion = oportunidadAntigua.IdTiempoCapacitacionValidacion;
                        oportunidadNueva.IdSubCategoriaDato = oportunidadAntigua.IdSubCategoriaDato;
                        oportunidadNueva.IdInteraccionFormulario = oportunidadAntigua.IdInteraccionFormulario;
                        oportunidadNueva.UrlOrigen = oportunidadAntigua.UrlOrigen;
                        oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2;
                        oportunidadNueva.Paso2 = oportunidadAntigua.Paso2;
                        oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                        oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina;
                        oportunidadNueva.NroSolicitud = oportunidadAntigua.NroSolicitud;
                        oportunidadNueva.NroSolicitudPorArea = oportunidadAntigua.NroSolicitudPorArea;
                        oportunidadNueva.NroSolicitudPorSubArea = oportunidadAntigua.NroSolicitudPorSubArea;
                        oportunidadNueva.NroSolicitudPorProgramaGeneral = oportunidadAntigua.NroSolicitudPorProgramaGeneral;
                        oportunidadNueva.NroSolicitudPorProgramaEspecifico = oportunidadAntigua.NroSolicitudPorProgramaEspecifico;
                        oportunidadNueva.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                        oportunidadNueva.IdPersonalAreaTrabajo = oportunidadAntigua.IdPersonalAreaTrabajo;
                        oportunidadNueva.IdPadre = oportunidadAntigua.IdPadre;
                        oportunidadNueva.IdAnuncioFacebook = oportunidadAntigua.IdAnuncioFacebook;
                        oportunidadNueva.ValidacionCorrecta = oportunidadAntigua.ValidacionCorrecta;
                        oportunidadNueva.UltimoComentario = comentario; // Asignar el comentario aquí
                        oportunidadNueva.UltimaFechaProgramada = fecha ?? DateTime.Now;



                        oportunidadNueva.FechaCreacion = oportunidadAntigua.FechaCreacion;
                        //oportunidadNueva.FechaModificacion = oportunidadAntigua.FechaModificacion;
                        oportunidadNueva.Estado = true;
                        oportunidadNueva.UsuarioCreacion = oportunidadAntigua.UsuarioCreacion;
                        //oportunidadNueva.UsuarioModificacion = oportunidadAntigua.UsuarioModificacion;

                        if (oportunidadAntigua.IdPersonalAsignado == 125)
                        {
                            OportunidadesAsesorAsignacionAutomaticaDTO oportunidadesAsesorAsignacion = new OportunidadesAsesorAsignacionAutomaticaDTO()
                            {
                                Id = oportunidadAntigua.Id,
                                IdMigracion = new Guid()
                            };
                            oportunidadesAsesorAsignacionAutomatica.Add(oportunidadesAsesorAsignacion);
                        }
                        AsignacionOportunidadLog asignacionLog = new AsignacionOportunidadLog();
                        asignacionLog.FechaLog = DateTime.Now;
                        asignacionLog.IdPersonalAnterior = oportunidadAntigua.IdPersonalAsignado;
                        asignacionLog.IdCentroCostoAnt = oportunidadAntigua.IdCentroCosto;
                        asignacionLog.IdOportunidad = oportunidadAntigua.Id;

                        oportunidadNueva.Id = idOportunidad;
                        var validacionCentroCostoV2 = oportunidadAntigua.IdCentroCosto;
                        oportunidadNueva.IdCentroCosto = AsignarAsesor.IdCentroCosto.Value == 0 ? oportunidadAntigua.IdCentroCosto : AsignarAsesor.IdCentroCosto.Value;
                        oportunidadNueva.IdPersonalAsignado = AsignarAsesor.IdAsesor.Value == 0 ? oportunidadAntigua.IdPersonalAsignado : AsignarAsesor.IdAsesor.Value;

                        //VALIDACION DE CAMBIO DE CENTRO DE COSTO
                        if (AsignarAsesor.IdCentroCosto != null && AsignarAsesor.IdCentroCosto != 0)
                        {
                            var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                            var _repCentroCosto = _unitOfWork.CentroCostoRepository;
                            var _repPEspecifico = _unitOfWork.PEspecificoRepository;
                            var _repAlumnoCambio = _unitOfWork.AlumnoRepository;

                            //Obtener el IdPEspecifico según el centro de costo Anterior
                            if (validacionCentroCostoV2 != null)
                            {
                                var pEspecificoCambio = _repPEspecifico.GetBy(x => x.IdCentroCosto == validacionCentroCostoV2).FirstOrDefault();

                                if (pEspecificoCambio != null)
                                {
                                    //Validamos que la matrícula exista con el Id del Alumno y el Id de PEspecifico
                                    var validarMatricula = _repMatriculaCabecera.GetBy(x => x.IdAlumno == oportunidadNueva.IdAlumno && x.IdPespecifico == pEspecificoCambio.Id).FirstOrDefault();
                                    if (validarMatricula != null)
                                    {
                                        _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarMatricula", Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                                        var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                        return ("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
                                    }

                                    var _repMontoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository;
                                    var validacionMontoPagoCronograma = _repMontoPagoCronograma.GetBy(x => x.IdOportunidad == idOportunidad).FirstOrDefault();
                                    if (validacionMontoPagoCronograma != null)
                                    {
                                        var validarMatricula2 = _repMatriculaCabecera.GetBy(x => x.IdCronograma == validacionMontoPagoCronograma.Id).FirstOrDefault();
                                        if (validarMatricula2 != null)
                                        {
                                            var datosAlumno = _repAlumnoCambio.FirstById((int)oportunidadNueva.IdAlumno);
                                            _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor/ValidarCronograma", Parametros = $"IdAlumno={oportunidadNueva.IdAlumno}&IdPEspecifico={pEspecificoCambio.Id}&IdCronograma={validacionMontoPagoCronograma.Id}", Mensaje = "Error en validacion de Matricula en Asignacion de asesor", Excepcion = "Error en validacion de Matricula en Asignacion de asesor", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                            return ("El alumno: " + datosAlumno.Nombre1 + " " + datosAlumno.Nombre2 + " " + datosAlumno.ApellidoPaterno + " " + datosAlumno.ApellidoMaterno + " ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Operaciones");
                                        }
                                    }
                                }
                            }
                        }

                        //Registramos la asignacion con los nuevos datos


                        AsignacionOportunidad asig = _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(idOportunidad);

                        if (asig == null)
                        {
                            asig = new AsignacionOportunidad();
                            asig.FechaAsignacion = DateTime.Now;
                            asig.IdAlumno = oportunidadNueva.IdAlumno;
                            asig.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona;
                            asig.IdPersonal = oportunidadNueva.IdPersonalAsignado;
                            asig.IdCentroCosto = oportunidadNueva.IdCentroCosto.Value;
                            asig.IdOportunidad = idOportunidad;
                            asig.IdTipoDato = oportunidadNueva.IdTipoDato;
                            asig.IdFaseOportunidad = oportunidadNueva.IdFaseOportunidad;
                            asig.Estado = true;
                            asig.FechaCreacion = DateTime.Now;
                            asig.FechaModificacion = DateTime.Now;
                            asig.UsuarioCreacion = Usuario;
                            asig.UsuarioModificacion = Usuario;
                            _repAsignacionOportunidad.Add(asig);
                        }
                        else
                        {
                            asig.FechaAsignacion = DateTime.Now;
                            asig.IdPersonal = oportunidadNueva.IdPersonalAsignado == 0 ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
                            asig.IdCentroCosto = oportunidadNueva.IdCentroCosto == 0 ? asig.IdCentroCosto : oportunidadNueva.IdCentroCosto.Value;
                            asig.IdAlumno = oportunidadNueva.IdAlumno == 0 ? asig.IdAlumno : oportunidadNueva.IdAlumno;
                            asig.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona == 0 ? asig.IdClasificacionPersona : oportunidadNueva.IdClasificacionPersona;
                            asig.IdPersonal = oportunidadNueva.IdPersonalAsignado == 0 ? asig.IdPersonal : oportunidadNueva.IdPersonalAsignado;
                            asig.FechaModificacion = DateTime.Now;
                            asig.UsuarioModificacion = Usuario;
                            _repAsignacionOportunidad.Update(asig);
                        }

                        asignacionLog.IdTipoDato = asig.IdTipoDato;
                        asignacionLog.IdPersonal = asig.IdPersonal;
                        asignacionLog.IdFaseOportunidad = asig.IdFaseOportunidad;
                        asignacionLog.IdAlumno = asig.IdAlumno;
                        asignacionLog.IdClasificacionPersona = asig.IdClasificacionPersona;
                        asignacionLog.Estado = true;
                        asignacionLog.FechaCreacion = DateTime.Now;
                        asignacionLog.FechaModificacion = DateTime.Now;
                        asignacionLog.UsuarioCreacion = Usuario;
                        asignacionLog.UsuarioModificacion = Usuario;
                        asignacionLog.IdCentroCosto = asig.IdCentroCosto;
                        asignacionLog.IdAsignacionOportunidad = asig.Id;
                        // opo.AsignacionOportunidads.AsignacionOportunidadLogs = asignacionLog;
                        asignacionOportunidadLogService.Add(asignacionLog);
                        //Finalizar Actividad

                        ActividadDetalle ActividadDetalleAntigua = actividadDetalleService.ObtenerEntidadActividadDetallePorId(oportunidadAntigua.IdActividadDetalleUltima.Value);
                        //opo.ActividadDetalles = actividadService.ObtenerEntidadActividadDetallePorId(opo.IdActividadDetalleUltima.Value);
                        ActividadDetalleAntigua.Comentario = string.IsNullOrEmpty(comentario) ? "Asignacion Manual" : comentario;

                        // ActividadDetalleAntigua.Comentario = "Asignacion Manual";
                        ActividadDetalleAntigua.IdOcurrencia = OCURRENCIA_ASIGNACION_MANUAL;
                        ActividadDetalleAntigua.IdOcurrenciaAlterno = OCURRENCIA_ASIGNACION_MANUAL;
                        ActividadDetalleAntigua.IdOcurrenciaActividad = null;
                        ActividadDetalleAntigua.IdOcurrenciaActividadAlterno = null;
                        ActividadDetalleAntigua.IdAlumno = oportunidadAntigua.IdAlumno;
                        ActividadDetalleAntigua.IdClasificacionPersona = oportunidadAntigua.IdClasificacionPersona;
                        ActividadDetalleAntigua.IdOportunidad = oportunidadAntigua.Id;
                        ActividadDetalleAntigua.IdCentralLlamada = 0;
                        ActividadDetalleAntigua.IdActividadCabecera = oportunidadAntigua.IdActividadCabeceraUltima;
                        ActividadDetalleAntigua.FechaReal = DateTime.Now;
                        ActividadDetalleAntigua.UsuarioModificacion = Usuario;
                        // ActividadDetalleAntigua.Comentario = comentario;
                        ActividadDetalleAntigua.FechaProgramada = fecha;

                        if (oportunidadAntigua.FechaPaso2 != null)
                        {
                            oportunidadNueva.FechaPaso2 = oportunidadAntigua.FechaPaso2.Value;
                        }
                        if (oportunidadAntigua.Paso2 != null)
                        {
                            oportunidadNueva.Paso2 = oportunidadAntigua.Paso2.Value;
                        }
                        if (oportunidadAntigua.CodMailing != null)
                        {
                            oportunidadNueva.CodMailing = oportunidadAntigua.CodMailing;
                        }
                        //oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.Value;
                        oportunidadNueva.IdPagina = oportunidadAntigua.IdPagina.GetValueOrDefault();
                        //oportunidadNueva.Usuario = Usuario;
                        oportunidadService._asignacionManual.OportunidadAntigua = oportunidadAntigua;
                        oportunidadService._asignacionManual.OportunidadNueva = oportunidadNueva;
                        oportunidadService._asignacionManual.ActividadAntigua = ActividadDetalleAntigua;


                        oportunidadService.FinalizarActividades(false, Usuario);

                        if (AsignarAsesor.IdAsesor != 0)
                            oportunidadService._asignacionManual.OportunidadLogNueva.IdAsesorAnt = AsignarAsesor.IdAsesor;

                        if (fecha != null)
                        {
                            oportunidadService._asignacionManual.OportunidadNueva.UltimaFechaProgramada = fecha;
                        }

                        oportunidadService.ProgramaActividad(AsignarAsesor.SegunMejorPro.GetValueOrDefault());


                        actividadDetalleService.Update(oportunidadService._asignacionManual.ActividadNueva);
                        actividadDetalleService.Add(oportunidadService._asignacionManual.ActividadNuevaProgramarActividad);
                        _unitOfWork.OportunidadLogRepository.Add(oportunidadService._asignacionManual.OportunidadLogNueva);
                        _unitOfWork.Commit();
                        oportunidadService.Update(oportunidadService._asignacionManual.OportunidadNueva);

                        try
                        {
                            OportunidadWhatsappEnvioDTO item = new OportunidadWhatsappEnvioDTO();
                            item.IdOportunidad = (int)asig.IdOportunidad;
                            item.IdPersonal = (int)asig.IdPersonal;
                            item.IdCategoriaOrigen = (int)oportunidadNueva.IdCategoriaOrigen;

                            //si el asesor actual es asignacion automatica

                            if (envioWhatsapp || oportunidadAntigua.IdPersonalAsignado == 125)
                            {
                                item.AplicaEnvioWhatsapp = true;
                            }


                            listaOportunidades.Add(item);
                        }
                        catch (Exception e) { }

                        //_unitOfWork.Commit();

                        //oportunidadActualizado = oportunidadService.Update(parametrosRetorno);

                        //Programar Actividad
                        //int am = 0;
                    }

                    scope.Complete();
                }
                // nuevaActividad = oportunidadActualizado.ActividadDetalles.FirstOrDefault();
                var _repOportunidad2 = _unitOfWork.OportunidadRepository;
                foreach (int idOportunidad in AsignarAsesor.IdOportunidades)
                {
                    Oportunidad oportunidad = new Oportunidad();
                    OportunidadLog oportunidadLog = new OportunidadLog();
                    var actividadDetalle = _repActividadDetalle.GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
                    if (actividadDetalle != null)
                    {

                        _unitOfWork.OportunidadRepository.ActualizarIdActividadDetalleUltima(actividadDetalle.Id, idOportunidad);
                    }
                }

                try
                {
                    if (AsignarAsesor.IdAsesor != null)
                    {
                        AgendaSocket.getInstance().NuevaActividadParaEjecutar(AsignarAsesor.IdOportunidades[0] ?? 0, AsignarAsesor.IdAsesor.Value);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    foreach (var item in listaOportunidades)
                    {
                        if (item.AplicaEnvioWhatsapp)
                        {
                            var pais = _unitOfWork.AsignacionRegularRepository.ObtenerPaisPorOportunidad((int)item.IdOportunidad);
                            //51:PERU,56:CHILE,57:COLOMBIA,52:MEXICO
                            if (pais.Id == 51 || pais.Id == 56 || pais.Id == 57 || pais.Id == 52)
                            {
                                EnvioWhats((int)item.IdOportunidad, pais.Id, (int)item.IdPersonal, (int)item.IdCategoriaOrigen);
                            }
                        }
                    }
                }
                catch (Exception e) { }


                return new { data = true, OportunidadesAsesorAsignacionAutomatica = oportunidadesAsesorAsignacionAutomatica };
            }
            catch (Exception ex)
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "AsignarAsesor", Parametros = $"{AsignarAsesor.IdAsesor},{AsignarAsesor.IdCentroCosto},{AsignarAsesor.IdOportunidades}/{Usuario}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return (ex.Message);
            }
        }
    }
}

