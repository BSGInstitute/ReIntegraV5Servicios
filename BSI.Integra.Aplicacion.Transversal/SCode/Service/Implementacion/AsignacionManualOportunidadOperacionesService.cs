using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionManualOportunidadOperacionesService
    /// Autor: Jonathan Caipo
    /// Fecha: 07/01/2023
    /// <summary>
    /// Gestión general de AsignacionManualOportunidadOperaciones
    /// </summary>
    public class AsignacionManualOportunidadOperacionesService : IAsignacionManualOportunidadOperacionesService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AsignacionManualOportunidadOperacionesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOportunidad, Oportunidad>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/01/2023
        /// Version: 1.0
        /// <summary>
        /// Asigna Oportunidad Tab Actual
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> bool: true </returns>
        public bool AsignarOportunidadTabActual(AsignarOportunidadOperacionesFiltroDTO objeto)
        {
            try
            {
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                AsignacionOportunidadService asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                OportunidadClasificacionOperacionesService oportunidadClasificacionOperacionesService = new OportunidadClasificacionOperacionesService(_unitOfWork);
                IntegraAspNetUserService integraAspNetUsersService = new IntegraAspNetUserService(_unitOfWork);
                oportunidadService._oportunidadBo = new OportunidadBoDTO()
                {
                    AsignacionOportunidad = new AsignacionOportunidad()
                    {
                        AsignacionOportunidadLogs = new List<AsignacionOportunidadLog>()
                    }
                };
                TransactionOptions options = new TransactionOptions();
                options.Timeout = new TimeSpan(0, 15, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options.Timeout))
                {
                    foreach (int idOportunidad in objeto.ListaOportunidades)
                    {
                        oportunidadService._oportunidadBo = oportunidadService.MapeoOportunidadBaseObjetDesdeEntidad(oportunidadService.ObtenerPorId(idOportunidad));
                        oportunidadService._oportunidadBo.Usuario = objeto.Usuario;
                        AsignacionOportunidadLog asignacionLog = new AsignacionOportunidadLog();

                        int idant = oportunidadService._oportunidadBo.IdPersonalAsignado.Value;
                        int idactCabant = oportunidadService._oportunidadBo.IdActividadCabeceraUltima.Value;
                        asignacionLog.FechaLog = DateTime.Now;
                        asignacionLog.IdPersonalAnterior = oportunidadService._oportunidadBo.IdPersonalAsignado;
                        asignacionLog.IdCentroCostoAnt = oportunidadService._oportunidadBo.IdCentroCosto;
                        asignacionLog.IdOportunidad = oportunidadService._oportunidadBo.Id;

                        oportunidadService._oportunidadBo.Id = idOportunidad;
                        oportunidadService._oportunidadBo.IdPersonalAsignado = objeto.IdPersonal;

                        oportunidadService._oportunidadBo.AsignacionOportunidad = asignacionOportunidadService.ObtenerPorIdOportunidad(idOportunidad);
                        AsignacionOportunidad asignacion = new AsignacionOportunidad();

                        if (oportunidadService._oportunidadBo.AsignacionOportunidad.Id < 0 && oportunidadService._oportunidadBo.AsignacionOportunidad.Id == null)
                        {
                            //Creamos un nuevo registro en asignacionOportunidad
                            asignacion.FechaAsignacion = DateTime.Now;
                            asignacion.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                            asignacion.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                            asignacion.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado;
                            asignacion.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto.Value;
                            asignacion.IdOportunidad = idOportunidad;
                            asignacion.IdTipoDato = oportunidadService._oportunidadBo.IdTipoDato;
                            asignacion.IdFaseOportunidad = oportunidadService._oportunidadBo.IdFaseOportunidad;
                            asignacion.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                            asignacion.Estado = true;
                            asignacion.FechaCreacion = DateTime.Now;
                            asignacion.FechaModificacion = DateTime.Now;
                            asignacion.UsuarioCreacion = objeto.Usuario;
                            asignacion.UsuarioModificacion = objeto.Usuario;
                            oportunidadService._oportunidadBo.AsignacionOportunidad = asignacion;
                        }
                        oportunidadService._oportunidadBo.AsignacionOportunidad.FechaAsignacion = DateTime.Now;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal : oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto : oportunidadService._oportunidadBo.IdCentroCosto.Value;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno = oportunidadService._oportunidadBo.IdAlumno == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno : oportunidadService._oportunidadBo.IdAlumno;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona : oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal : oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.FechaModificacion = DateTime.Now;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.UsuarioModificacion = objeto.Usuario;

                        asignacionLog.IdTipoDato = oportunidadService._oportunidadBo.AsignacionOportunidad.IdTipoDato;
                        asignacionLog.IdPersonal = oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal;
                        asignacionLog.IdFaseOportunidad = oportunidadService._oportunidadBo.AsignacionOportunidad.IdFaseOportunidad;
                        asignacionLog.IdAlumno = oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno;
                        asignacionLog.IdClasificacionPersona = oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona;
                        asignacionLog.Estado = true;
                        asignacionLog.FechaCreacion = DateTime.Now;
                        asignacionLog.FechaModificacion = DateTime.Now;
                        asignacionLog.UsuarioCreacion = objeto.Usuario;
                        asignacionLog.UsuarioModificacion = objeto.Usuario;
                        asignacionLog.IdCentroCosto = oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto;
                        asignacionLog.IdAsignacionOportunidad = oportunidadService._oportunidadBo.AsignacionOportunidad.Id;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.AsignacionOportunidadLogs.Add(asignacionLog);

                        //Finalizar Actividad
                        ActividadDetalleService actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                        oportunidadService._oportunidadBo.ActividadAntigua = actividadDetalleService.ObtenerPorId(oportunidadService._oportunidadBo.IdActividadDetalleUltima.Value);
                        oportunidadService._oportunidadBo.ActividadAntigua.Comentario = "Asignacion Manual";
                        oportunidadService._oportunidadBo.ActividadAntigua.IdOcurrenciaActividad = null;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdOportunidad = oportunidadService._oportunidadBo.Id;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdCentralLlamada = 0;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdActividadCabecera = oportunidadService._oportunidadBo.IdActividadCabeceraUltima;

                        OportunidadDTO oportunidad = new OportunidadDTO();

                        oportunidad.Id = oportunidadService._oportunidadBo.Id;
                        oportunidad.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto.Value;
                        oportunidad.IdPersonalAsignado = oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidad.IdTipoDato = oportunidadService._oportunidadBo.IdTipoDato;
                        oportunidad.IdFaseOportunidad = oportunidadService._oportunidadBo.IdFaseOportunidad;
                        oportunidad.IdOrigen = oportunidadService._oportunidadBo.IdOrigen;
                        oportunidad.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                        oportunidad.UltimoComentario = oportunidadService._oportunidadBo.UltimoComentario;
                        oportunidad.IdActividadDetalleUltima = oportunidadService._oportunidadBo.IdActividadDetalleUltima;
                        oportunidad.IdActividadCabeceraUltima = oportunidadService._oportunidadBo.IdActividadCabeceraUltima;
                        oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadService._oportunidadBo.IdEstadoActividadDetalleUltimoEstado;
                        oportunidad.UltimaFechaProgramada = oportunidadService._oportunidadBo.UltimaFechaProgramada.ToString();
                        oportunidad.IdEstadoOportunidad = oportunidadService._oportunidadBo.IdEstadoOportunidad;
                        oportunidad.IdEstadoOcurrenciaUltimo = oportunidadService._oportunidadBo.IdEstadoOcurrenciaUltimo;
                        oportunidad.IdFaseOportunidadMaxima = oportunidadService._oportunidadBo.IdFaseOportunidadMaxima;
                        oportunidad.IdFaseOportunidadInicial = oportunidadService._oportunidadBo.IdFaseOportunidadInicial;
                        oportunidad.IdCategoriaOrigen = oportunidadService._oportunidadBo.IdCategoriaOrigen;
                        oportunidad.IdConjuntoAnuncio = oportunidadService._oportunidadBo.IdConjuntoAnuncio;
                        oportunidad.IdCampaniaScoring = oportunidadService._oportunidadBo.IdCampaniaScoring;
                        oportunidad.IdFaseOportunidadIp = oportunidadService._oportunidadBo.IdFaseOportunidadIp;
                        oportunidad.IdFaseOportunidadIc = oportunidadService._oportunidadBo.IdFaseOportunidadIc;
                        oportunidad.FechaEnvioFaseOportunidadPf = oportunidadService._oportunidadBo.FechaEnvioFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadPf = oportunidadService._oportunidadBo.FechaPagoFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadIc = oportunidadService._oportunidadBo.FechaPagoFaseOportunidadIc;
                        oportunidad.FechaRegistroCampania = oportunidadService._oportunidadBo.FechaRegistroCampania;
                        oportunidad.IdFaseOportunidadPortal = oportunidadService._oportunidadBo.IdFaseOportunidadPortal;
                        oportunidad.IdFaseOportunidadPf = oportunidadService._oportunidadBo.IdFaseOportunidadPf;
                        oportunidad.CodigoPagoIc = oportunidadService._oportunidadBo.CodigoPagoIc;
                        oportunidad.FlagVentaCruzada = oportunidadService._oportunidadBo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacion = oportunidadService._oportunidadBo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacionValidacion = oportunidadService._oportunidadBo.IdTiempoCapacitacionValidacion;
                        oportunidad.IdSubCategoriaDato = oportunidadService._oportunidadBo.IdSubCategoriaDato;
                        oportunidad.IdInteraccionFormulario = oportunidadService._oportunidadBo.IdInteraccionFormulario;
                        oportunidad.UrlOrigen = oportunidadService._oportunidadBo.UrlOrigen;
                        oportunidad.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidad.IdPersonalAreaTrabajo = oportunidadService._oportunidadBo.IdPersonalAreaTrabajo;

                        oportunidadService.FinalizarActividadAsignacionManualOperaciones(false, oportunidad);

                        oportunidadService._oportunidadBo.OportunidadLogNueva.IdAsesorAnt = idant;
                        //oportunidadService._oportunidadBo.UltimaFechaProgramada = DateTime.Now;

                        oportunidadService._oportunidadBo.UltimaFechaProgramada = oportunidadService._oportunidadBo.UltimaFechaProgramada ?? DateTime.Now;

                        oportunidadService.ProgramaActividadAsignacionManualOperaciones(idactCabant);
                        oportunidadService._oportunidadBo.ActividadNueva.IdActividadCabecera = idactCabant;

                        var mapeoOportunidad = oportunidadService.MapeoBoDTO(oportunidadService._oportunidadBo);
                        //oportunidadService.Update(mapeoOportunidad);
                        var respOprtunidad = _unitOfWork.OportunidadRepository.Update(mapeoOportunidad);
                        _unitOfWork.Commit();
                        var oportunidadTemp = _mapper.Map<Oportunidad>(respOprtunidad);
                      

                        var oco = oportunidadClasificacionOperacionesService.ObtenerPorIdOportunidad(idOportunidad);
                        var mat = matriculaCabeceraService.ObtenerPorId(oco.IdMatriculaCabecera);
                        var user = integraAspNetUsersService.ObtenerPorId(objeto.IdPersonal);

                        if (user.UserName.Equals("esanchez1"))
                        {
                            user.UserName = "esanchez";
                        }
                        mat.UsuarioCoordinadorAcademico = user.UserName;
                        mat.FechaModificacion = DateTime.Now;
                        matriculaCabeceraService.Update(mat);
                        _unitOfWork.DetachAll();
                        matriculaCabeceraService.ModificarGestorDeCobranza(objeto.Usuario, mat.UsuarioCoordinadorAcademico, oco.IdMatriculaCabecera);
                    }
                    scope.Complete();
                }
                foreach (int idOportunidad in objeto.ListaOportunidades)
                {
                    Oportunidad oportunidad = new Oportunidad();
                    OportunidadLog oportunidadLog = new OportunidadLog();
                    OportunidadService oportunidadService2 = new OportunidadService(_unitOfWork);
                    OportunidadLogService oportunidadServiceLog2 = new OportunidadLogService(_unitOfWork);
                    ActividadDetalleService repActividadDetalle2 = new ActividadDetalleService(_unitOfWork);
                    //var actividadDetalle = repActividadDetalle2.ObtenerEntidadActividadDetallePorId(idOportunidad);
                    var actividadDetalle = _unitOfWork.ActividadDetalleRepository
                      .GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();
                    if (actividadDetalle != null)
                    {
                        oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                        oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
                        //oportunidadService2.Update(oportunidad);
                        _unitOfWork.OportunidadRepository.Update(oportunidad);
                        _unitOfWork.Commit();
                        _unitOfWork.DetachAll();
                    }
                    //try
                    //{
                    //    if (objeto.IdPersonal != null)
                    //    {
                    //        AgendaSocket.getInstance().NuevaActividadParaEjecutar(objeto.ListaOportunidades[0], objeto.IdPersonal);
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //}
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/01/2023
        /// Version: 1.0
        /// <summary>
        /// Asigna Oportunidad Operaciones
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> bool: true </returns>
        public bool AsignarOportunidadOperaciones(AsignarOportunidadOperacionesFiltroDTO objeto)
        {
            try
            {
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                AsignacionOportunidadService asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                OportunidadClasificacionOperacionesService oportunidadClasificacionOperacionesService = new OportunidadClasificacionOperacionesService(_unitOfWork);
                IntegraAspNetUserService integraAspNetUsersService = new IntegraAspNetUserService(_unitOfWork);
                oportunidadService._oportunidadBo = new OportunidadBoDTO()
                {
                    AsignacionOportunidad = new AsignacionOportunidad()
                    {
                        AsignacionOportunidadLogs = new List<AsignacionOportunidadLog>()
                    }
                };

                TransactionOptions options = new TransactionOptions();
                options.Timeout = new TimeSpan(0, 15, 0);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options.Timeout))
                {
                    foreach (int idOportunidad in objeto.ListaOportunidades)
                    {
                        oportunidadService._oportunidadBo = oportunidadService.MapeoOportunidadBaseObjetDesdeEntidad(oportunidadService.ObtenerPorId(idOportunidad));
                        oportunidadService._oportunidadBo.Usuario = objeto.Usuario;
                        AsignacionOportunidadLog asignacionLog = new AsignacionOportunidadLog();

                        int idant = oportunidadService._oportunidadBo.IdPersonalAsignado.Value;
                        int idactCabant = oportunidadService._oportunidadBo.IdActividadCabeceraUltima.Value;
                        asignacionLog.FechaLog = DateTime.Now;
                        asignacionLog.IdPersonalAnterior = oportunidadService._oportunidadBo.IdPersonalAsignado;
                        asignacionLog.IdCentroCostoAnt = oportunidadService._oportunidadBo.IdCentroCosto;
                        asignacionLog.IdOportunidad = oportunidadService._oportunidadBo.Id;

                        oportunidadService._oportunidadBo.Id = idOportunidad;
                        oportunidadService._oportunidadBo.IdPersonalAsignado = objeto.IdPersonal;

                        oportunidadService._oportunidadBo.AsignacionOportunidad = asignacionOportunidadService.ObtenerPorIdOportunidad(idOportunidad);
                        AsignacionOportunidad asignacion = new AsignacionOportunidad();

                        if (oportunidadService._oportunidadBo.AsignacionOportunidad == null)
                        {
                            //Creamos un nuevo registro en asignacionOportunidad
                            asignacion.FechaAsignacion = DateTime.Now;
                            asignacion.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                            asignacion.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                            asignacion.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado;
                            asignacion.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto.Value;
                            asignacion.IdOportunidad = idOportunidad;
                            asignacion.IdTipoDato = oportunidadService._oportunidadBo.IdTipoDato;
                            asignacion.IdFaseOportunidad = oportunidadService._oportunidadBo.IdFaseOportunidad;
                            asignacion.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                            asignacion.Estado = true;
                            asignacion.FechaCreacion = DateTime.Now;
                            asignacion.FechaModificacion = DateTime.Now;
                            asignacion.UsuarioCreacion = objeto.Usuario;
                            asignacion.UsuarioModificacion = objeto.Usuario;
                            oportunidadService._oportunidadBo.AsignacionOportunidad = asignacion;
                        }
                        oportunidadService._oportunidadBo.AsignacionOportunidad.FechaAsignacion = DateTime.Now;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal : oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto : oportunidadService._oportunidadBo.IdCentroCosto.Value;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno = oportunidadService._oportunidadBo.IdAlumno == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno : oportunidadService._oportunidadBo.IdAlumno;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona : oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal = oportunidadService._oportunidadBo.IdPersonalAsignado == 0 ? oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal : oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.FechaModificacion = DateTime.Now;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.UsuarioModificacion = objeto.Usuario;

                        asignacionLog.IdTipoDato = oportunidadService._oportunidadBo.AsignacionOportunidad.IdTipoDato;
                        asignacionLog.IdPersonal = oportunidadService._oportunidadBo.AsignacionOportunidad.IdPersonal;
                        asignacionLog.IdFaseOportunidad = oportunidadService._oportunidadBo.AsignacionOportunidad.IdFaseOportunidad;
                        asignacionLog.IdAlumno = oportunidadService._oportunidadBo.AsignacionOportunidad.IdAlumno;
                        asignacionLog.IdClasificacionPersona = oportunidadService._oportunidadBo.AsignacionOportunidad.IdClasificacionPersona;
                        asignacionLog.Estado = true;
                        asignacionLog.FechaCreacion = DateTime.Now;
                        asignacionLog.FechaModificacion = DateTime.Now;
                        asignacionLog.UsuarioCreacion = objeto.Usuario;
                        asignacionLog.UsuarioModificacion = objeto.Usuario;
                        asignacionLog.IdCentroCosto = oportunidadService._oportunidadBo.AsignacionOportunidad.IdCentroCosto;
                        asignacionLog.IdAsignacionOportunidad = oportunidadService._oportunidadBo.AsignacionOportunidad.Id;
                        oportunidadService._oportunidadBo.AsignacionOportunidad.AsignacionOportunidadLogs.Add(asignacionLog);


                        //Finalizar Actividad
                        ActividadDetalleService actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                        oportunidadService._oportunidadBo.ActividadAntigua = actividadDetalleService.ObtenerPorId(oportunidadService._oportunidadBo.IdActividadDetalleUltima.Value);
                        oportunidadService._oportunidadBo.ActividadAntigua.Comentario = "Asignacion Manual";
                        oportunidadService._oportunidadBo.ActividadAntigua.IdOcurrenciaActividad = null;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdOportunidad = oportunidadService._oportunidadBo.Id;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdCentralLlamada = 0;
                        oportunidadService._oportunidadBo.ActividadAntigua.IdActividadCabecera = oportunidadService._oportunidadBo.IdActividadCabeceraUltima;


                        OportunidadDTO oportunidad = new OportunidadDTO();

                        oportunidad.Id = oportunidadService._oportunidadBo.Id;
                        oportunidad.IdCentroCosto = oportunidadService._oportunidadBo.IdCentroCosto.Value;
                        oportunidad.IdPersonalAsignado = oportunidadService._oportunidadBo.IdPersonalAsignado;
                        oportunidad.IdTipoDato = oportunidadService._oportunidadBo.IdTipoDato;
                        oportunidad.IdFaseOportunidad = oportunidadService._oportunidadBo.IdFaseOportunidad;
                        oportunidad.IdOrigen = oportunidadService._oportunidadBo.IdOrigen;
                        oportunidad.IdAlumno = oportunidadService._oportunidadBo.IdAlumno;
                        oportunidad.UltimoComentario = oportunidadService._oportunidadBo.UltimoComentario;
                        oportunidad.IdActividadDetalleUltima = oportunidadService._oportunidadBo.IdActividadDetalleUltima;
                        oportunidad.IdActividadCabeceraUltima = oportunidadService._oportunidadBo.IdActividadCabeceraUltima;
                        oportunidad.IdEstadoActividadDetalleUltimoEstado = oportunidadService._oportunidadBo.IdEstadoActividadDetalleUltimoEstado;
                        oportunidad.UltimaFechaProgramada = oportunidadService._oportunidadBo.UltimaFechaProgramada.ToString();
                        oportunidad.IdEstadoOportunidad = oportunidadService._oportunidadBo.IdEstadoOportunidad;
                        oportunidad.IdEstadoOcurrenciaUltimo = oportunidadService._oportunidadBo.IdEstadoOcurrenciaUltimo;
                        oportunidad.IdFaseOportunidadMaxima = oportunidadService._oportunidadBo.IdFaseOportunidadMaxima;
                        oportunidad.IdFaseOportunidadInicial = oportunidadService._oportunidadBo.IdFaseOportunidadInicial;
                        oportunidad.IdCategoriaOrigen = oportunidadService._oportunidadBo.IdCategoriaOrigen;
                        oportunidad.IdConjuntoAnuncio = oportunidadService._oportunidadBo.IdConjuntoAnuncio;
                        oportunidad.IdCampaniaScoring = oportunidadService._oportunidadBo.IdCampaniaScoring;
                        oportunidad.IdFaseOportunidadIp = oportunidadService._oportunidadBo.IdFaseOportunidadIp;
                        oportunidad.IdFaseOportunidadIc = oportunidadService._oportunidadBo.IdFaseOportunidadIc;
                        oportunidad.FechaEnvioFaseOportunidadPf = oportunidadService._oportunidadBo.FechaEnvioFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadPf = oportunidadService._oportunidadBo.FechaPagoFaseOportunidadPf;
                        oportunidad.FechaPagoFaseOportunidadIc = oportunidadService._oportunidadBo.FechaPagoFaseOportunidadIc;
                        oportunidad.FechaRegistroCampania = oportunidadService._oportunidadBo.FechaRegistroCampania;
                        oportunidad.IdFaseOportunidadPortal = oportunidadService._oportunidadBo.IdFaseOportunidadPortal;
                        oportunidad.IdFaseOportunidadPf = oportunidadService._oportunidadBo.IdFaseOportunidadPf;
                        oportunidad.CodigoPagoIc = oportunidadService._oportunidadBo.CodigoPagoIc;
                        oportunidad.FlagVentaCruzada = oportunidadService._oportunidadBo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacion = oportunidadService._oportunidadBo.IdTiempoCapacitacion;
                        oportunidad.IdTiempoCapacitacionValidacion = oportunidadService._oportunidadBo.IdTiempoCapacitacionValidacion;
                        oportunidad.IdSubCategoriaDato = oportunidadService._oportunidadBo.IdSubCategoriaDato;
                        oportunidad.IdInteraccionFormulario = oportunidadService._oportunidadBo.IdInteraccionFormulario;
                        oportunidad.UrlOrigen = oportunidadService._oportunidadBo.UrlOrigen;
                        oportunidad.IdClasificacionPersona = oportunidadService._oportunidadBo.IdClasificacionPersona;
                        oportunidad.IdPersonalAreaTrabajo = oportunidadService._oportunidadBo.IdPersonalAreaTrabajo;

                        oportunidadService.FinalizarActividadAsignacionManualOperaciones(false, oportunidad);

                        oportunidadService._oportunidadBo.OportunidadLogNueva.IdAsesorAnt = idant;
                        oportunidadService._oportunidadBo.UltimaFechaProgramada = DateTime.Now;
                        oportunidadService.ProgramaActividadAsignacionManualOperaciones(63);
                        oportunidadService._oportunidadBo.ActividadNueva.IdActividadCabecera = idactCabant;

                        var mapeoOportunidad = oportunidadService.MapeoBoDTO(oportunidadService._oportunidadBo);
                        oportunidadService.Update(mapeoOportunidad);

                        var oco = oportunidadClasificacionOperacionesService.ObtenerPorIdOportunidad(idOportunidad);
                        var mat = matriculaCabeceraService.ObtenerPorId(oco.IdMatriculaCabecera);
                        var user = integraAspNetUsersService.ObtenerPorId(objeto.IdPersonal);

                        mat.UsuarioCoordinadorAcademico = user.UserName;
                        mat.FechaModificacion = DateTime.Now;
                        matriculaCabeceraService.Update(mat);
                        _unitOfWork.DetachAll();
                        matriculaCabeceraService.ModificarGestorDeCobranza(objeto.Usuario, mat.UsuarioCoordinadorAcademico, oco.IdMatriculaCabecera);
                    }
                    scope.Complete();
                }
                foreach (int idOportunidad in objeto.ListaOportunidades)
                {
                    Oportunidad oportunidad = new Oportunidad();
                    OportunidadLog oportunidadLog = new OportunidadLog();
                    OportunidadService oportunidadService2 = new OportunidadService(_unitOfWork);
                    OportunidadLogService oportunidadLogService2 = new OportunidadLogService(_unitOfWork);
                    ActividadDetalleService actividadDetalleService2 = new ActividadDetalleService(_unitOfWork);

                    //var actividadDetalle = actividadDetalleService2.ObtenerEntidadActividadDetallePorId(idOportunidad);
                    var actividadDetalle = _unitOfWork.ActividadDetalleRepository
                        .GetBy(w => w.IdOportunidad == idOportunidad, w => new { w.Id, w.FechaCreacion }).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();

                    if (actividadDetalle != null)
                    {
                        oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                        oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
                        //oportunidadService2.Update(oportunidad);
                        _unitOfWork.OportunidadRepository.Update(oportunidad);
                        _unitOfWork.Commit();
                        _unitOfWork.DetachAll();
                    }
                    //try
                    //{
                    //    if (objeto.IdPersonal != null)
                    //    {
                    //        AgendaSocket.getInstance().NuevaActividadParaEjecutar(objeto.ListaOportunidades[0], objeto.IdPersonal);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
