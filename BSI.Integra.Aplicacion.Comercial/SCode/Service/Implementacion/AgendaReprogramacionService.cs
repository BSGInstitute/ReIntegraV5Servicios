using AutoMapper;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Globalization;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: AgendaComercialService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_AgendaComercial
    /// </summary>
    public class AgendaReprogramacionService : IAgendaReprogramacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private ReprogramacionDTO? _reprogramacion = new ReprogramacionDTO();
        public bool FlagValidacionVentaCruzada = true;

        public AgendaReprogramacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidad, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<THoraBloqueadum, HoraBloqueada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TComprobantePagoOportunidad, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCalidadProcesamientoAlterno, CalidadProcesamientoAlterno>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatosOportunidadDTO, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TActividadDetalleGestionContacto, ActividadDetalleGestionContacto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGestionContacto, GestionContacto>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task<bool> FinalizarYProgramarGestionAsync(FinalizarProgramarGestionPlaDTO dto)
        {
            if (dto.ActividadAntigua.IdGestionContacto == 0)
                throw new ArgumentException("IdGestionContacto es requerido");
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // 1. Obtener Gestión (Oportunidad Docente)
                    var gestionBO = await _unitOfWork.GestionContactoRepository.ObtenerPorIdAsync(dto.ActividadAntigua.IdGestionContacto);

                    if (gestionBO == null) throw new Exception("Gestión no encontrada");

                    // 2. Cerrar Actividad Antigua
                    var actividadAntigua = await _unitOfWork.ActividadDetalleGestionContactoRepository.ObtenerPorIdAsync(dto.ActividadAntigua.Id);

                    if (actividadAntigua != null)
                    {
                        actividadAntigua.FechaReal = DateTime.Now;
                        actividadAntigua.DuracionReal = 0;
                        actividadAntigua.Estado = true;
                        //actividadAntigua.IdOcurrencia = dto.ActividadAntigua.IdOcurrencia; -- PENDIENTE DE AJUSTE
                        actividadAntigua.Comentario = dto.ActividadAntigua.Comentario;
                        actividadAntigua.UsuarioModificacion = dto.Filtro.Usuario;
                        actividadAntigua.FechaModificacion = DateTime.Now;

                        _unitOfWork.ActividadDetalleGestionContactoRepository.Update(actividadAntigua);
                    }

                    // 3. Programar Nueva Actividad
                    if (DateTime.TryParse(dto.DatosGestion.UltimaFechaProgramada, out DateTime fechaProgramada))
                    {
                        var actividadNueva = new ActividadDetalleGestionContacto
                        {
                            IdGestionContacto = dto.ActividadAntigua.IdGestionContacto,
                            FechaProgramada = fechaProgramada,
                            Estado = true,
                            IdActividadCabecera = 1,
                            Comentario = "Reprogramación",
                            UsuarioCreacion = dto.Filtro.Usuario,
                            UsuarioModificacion = dto.Filtro.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        //await _unitOfWork.ActividadDetalleGestionContactoRepository.AddAsync(actividadNueva); -- PENDIENTE DE AJUSTE

                        // 4. Bloqueo de Agenda
                        var horaBloqueada = new HoraBloqueada
                        {
                            IdPersonal = dto.DatosGestion.IdPersonalAsignado,
                            Fecha = fechaProgramada.Date,
                            Hora = fechaProgramada,
                            Estado = true,
                            UsuarioCreacion = dto.Filtro.Usuario,
                            UsuarioModificacion = dto.Filtro.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        //await _unitOfWork.HoraBloqueadaRepository.AddAsync(horaBloqueada); -- PENDIENTE DE AJUSTE
                    }

                    // 5. Actualizar Gestión
                    // Lógica de cambio de fase
                    if (dto.DatosGestion.IdFaseGestionContacto.HasValue && dto.DatosGestion.IdFaseGestionContacto > 0)
                    {
                        gestionBO.IdFaseGestionContacto = dto.DatosGestion.IdFaseGestionContacto.Value;
                    }

                    gestionBO.UltimoComentario = dto.DatosGestion.UltimoComentario;
                    gestionBO.UsuarioModificacion = dto.Filtro.Usuario;
                    gestionBO.FechaModificacion = DateTime.Now;

                    _unitOfWork.GestionContactoRepository.Update(gestionBO);

                    await _unitOfWork.CommitAsync();
                    scope.Complete();

                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de OportunidadDTO a Entidad Oportunidad
        /// </summary>
        /// <returns> Oportunidad </returns>
        public Oportunidad CargarOportunidad(ReprogramacionDTO reprogramacion)
        {
            try
            {
                Oportunidad entidad = reprogramacion.Oportunidad;
                if (entidad == null) throw new BadRequestException("Oportunidad Vacia");
                entidad.Estado = true;
                entidad.ActividadDetalles = new List<ActividadDetalle>();
                entidad.AsignacionOportunidads = new List<AsignacionOportunidad>();
                entidad.CalidadProcesamientos = new List<CalidadProcesamiento>();
                entidad.CalidadProcesamientoAlternos = new List<CalidadProcesamientoAlterno>();
                entidad.ComprobantePagoOportunidads = new List<ComprobantePagoOportunidad>();
                entidad.ModeloDataMinings = new List<ModeloDataMining>();
                entidad.OportunidadClasificacionOperaciones = new List<OportunidadClasificacionOperaciones>();
                entidad.OportunidadCompetidors = new List<OportunidadCompetidor>();
                entidad.OportunidadLogs = new List<OportunidadLog>();
                entidad.SolucionClienteByActividads = new List<SolucionClienteByActividad>();

                if (reprogramacion.ActividadTemp != null) entidad.ActividadDetalles.Add(reprogramacion.ActividadTemp);
                if (reprogramacion.ActividadNuevaProgramada != null) entidad.ActividadDetalles.Add(reprogramacion.ActividadNuevaProgramada);
                if (reprogramacion.ActividadTrabajada != null) entidad.ActividadDetalles.Add(reprogramacion.ActividadTrabajada);
                if (reprogramacion.Actividades != null && reprogramacion.Actividades.Count() > 0)
                {
                    foreach (var item in reprogramacion.Actividades)
                    {
                        entidad.ActividadDetalles.Add(item);
                    }
                }
                if (reprogramacion.OportunidadLogTemp != null) entidad.OportunidadLogs.Add(reprogramacion.OportunidadLogTemp);
                if (reprogramacion.OportunidadLogNueva != null) entidad.OportunidadLogs.Add(reprogramacion.OportunidadLogNueva);
                if (reprogramacion.ComprobantePago != null) entidad.ComprobantePagoOportunidads.Add(reprogramacion.ComprobantePago);
                if (reprogramacion.OportunidadCompetidor != null) entidad.OportunidadCompetidors.Add(reprogramacion.OportunidadCompetidor);
                if (reprogramacion.CalidadProcesamiento != null) entidad.CalidadProcesamientos.Add(reprogramacion.CalidadProcesamiento);
                if (reprogramacion.CalidadProcesamientoAlterno != null) entidad.CalidadProcesamientoAlternos.Add(reprogramacion.CalidadProcesamientoAlterno);
                if (reprogramacion.ListaSolucionClienteByActividad != null && reprogramacion.ListaSolucionClienteByActividad.Count() > 0)
                {
                    foreach (var item in reprogramacion.ListaSolucionClienteByActividad)
                    {
                        entidad.SolucionClienteByActividads.Add(item);
                    }
                }
                if (reprogramacion.ModeloDataMining != null) entidad.ModeloDataMinings.Add(reprogramacion.ModeloDataMining);
                if (reprogramacion.AsignacionOportunidad != null) entidad.AsignacionOportunidads.Add(reprogramacion.AsignacionOportunidad);

                return entidad;
            }
            catch
            {
                throw;
            }
        }
        private async Task CargarOportunidadCompetidorReprogramacionAsync(OportunidadCompetidorFinalizarActividadDTO oportunidadCompetidor, List<int> listaCompetidor, string usuario)
        {
            if (oportunidadCompetidor.Id == 0)
            {
                _reprogramacion.OportunidadCompetidor = new OportunidadCompetidor
                {
                    Id = 0,
                    IdOportunidad = oportunidadCompetidor.IdOportunidad,
                    OtroBeneficio = oportunidadCompetidor.OtroBeneficio,
                    Respuesta = oportunidadCompetidor.Respuesta,
                    Completado = oportunidadCompetidor.Completado,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    Estado = true
                };
            }
            else
            {
                _reprogramacion!.OportunidadCompetidor = await _unitOfWork.OportunidadCompetidorRepository.ObtenerPorIdAsync(oportunidadCompetidor.Id);
                if (_reprogramacion.OportunidadCompetidor != null && _reprogramacion.OportunidadCompetidor.Id != 0)
                {
                    _reprogramacion.OportunidadCompetidor.IdOportunidad = oportunidadCompetidor.IdOportunidad;
                    _reprogramacion.OportunidadCompetidor.OtroBeneficio = oportunidadCompetidor.OtroBeneficio;
                    _reprogramacion.OportunidadCompetidor.Respuesta = oportunidadCompetidor.Respuesta;
                    _reprogramacion.OportunidadCompetidor.Completado = oportunidadCompetidor.Completado;
                }
                else
                {
                    _reprogramacion.OportunidadCompetidor = new OportunidadCompetidor
                    {
                        Id = 0,
                        IdOportunidad = oportunidadCompetidor.IdOportunidad,
                        OtroBeneficio = oportunidadCompetidor.OtroBeneficio,
                        Respuesta = oportunidadCompetidor.Respuesta,
                        Completado = oportunidadCompetidor.Completado,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        Estado = true
                    };
                }
            }

            if (_reprogramacion.OportunidadCompetidor.Id != 0 && listaCompetidor.Count() > 0)
            {
                var detalleOportunidadCompetidorService = new DetalleOportunidadCompetidorService(_unitOfWork);
                await detalleOportunidadCompetidorService.EliminarPorOportunidadCompetidorAsync(_reprogramacion.OportunidadCompetidor.Id, usuario, listaCompetidor);
            }

            _reprogramacion.OportunidadCompetidor.DetalleOportunidadCompetidors = new List<DetalleOportunidadCompetidor>();
            List<DetalleOportunidadCompetidor> listaDetalleOportunidadCompetidor = await _unitOfWork.DetalleOportunidadCompetidorRepository.ObtenerPorIdOportunidaCompetidorIdsCompetidorAsync(_reprogramacion.OportunidadCompetidor.Id, listaCompetidor);
            foreach (var idCompetidor in listaCompetidor)
            {
                DetalleOportunidadCompetidor detalleOportunidadCompetidor = new();
                if (idCompetidor != 0 && listaDetalleOportunidadCompetidor.Count() > 0)
                    detalleOportunidadCompetidor = listaDetalleOportunidadCompetidor.FirstOrDefault(x => x.IdCompetidor == idCompetidor);
                if (detalleOportunidadCompetidor == null || detalleOportunidadCompetidor.Id == 0)
                {
                    detalleOportunidadCompetidor = new DetalleOportunidadCompetidor
                    {
                        IdOportunidadCompetidor = _reprogramacion.OportunidadCompetidor.Id,
                        IdCompetidor = idCompetidor,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = usuario
                    };
                }
                detalleOportunidadCompetidor.FechaModificacion = DateTime.Now;
                detalleOportunidadCompetidor.UsuarioModificacion = usuario;
                _reprogramacion.OportunidadCompetidor.DetalleOportunidadCompetidors.Add(detalleOportunidadCompetidor);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/02/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza la reprogramacion de actividades
        /// </summary>
        /// <param name="idAsesor">Id del asesor</param>
        /// /// <param name="codigoAreaTrabajo">Codigo-Área de trabajo</param>
        /// <returns> AgendaDTO2 </returns>
        public async Task<RespuestaFinalizarYProgramarActividadAlterno> FinalizarYProgramarActividadAlternoAsync(FinalizarProgramarActividadAlternoDTO dto)
        {
            string parametros = "";
            try
            {
                if (dto.ActividadAntigua.IdOportunidad != 0)
                {
                    //Desactivar oportunidad remarketing
                    try
                    {
                        await _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnteriorAsync(dto.ActividadAntigua.IdOportunidad);
                    }
                    catch { }

                    var validacion = _unitOfWork.OportunidadRepository.ValidarFaseOportunidad(dto.ActividadAntigua.IdOportunidad, dto.DatosOportunidad.IdFaseOportunidad.Value, dto.ActividadAntigua.Id);
                    if (!validacion.Existe)
                    {
                        throw new ConflictException($"La Actividad ya fue trabajada: IdActividad: {dto.ActividadAntigua.Id}, IdOportunidad {dto.ActividadAntigua.IdOportunidad}, FaseActual: {validacion.CodigoFaseOportunidad}");
                    }
                }
                else
                {
                    throw new BadRequestException("Id Oportunidad 0");
                }
                //Cargar Valores estaticos
                //new ValorEstatico(_unitOfWork);

                int idReprogramacionCabecera = 0;
                HoraBloqueada horaBloqueada = new()
                {
                    IdPersonal = dto.filtro.IdPersonal,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = dto.filtro.Usuario,
                    UsuarioModificacion = dto.filtro.Usuario,
                    Estado = true
                };

                if (dto.DatosOportunidad.UltimaFechaProgramada != null)
                {
                    horaBloqueada.Fecha = DateTime.Parse(dto.DatosOportunidad.UltimaFechaProgramada);
                    horaBloqueada.Hora = DateTime.Parse(dto.DatosOportunidad.UltimaFechaProgramada);
                }

                _reprogramacion.Oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync(dto.ActividadAntigua.IdOportunidad);
                _reprogramacion.Oportunidad.IdFaseOportunidadIp = dto.DatosOportunidad.IdFaseOportunidadIp;
                _reprogramacion.Oportunidad.IdFaseOportunidadIc = dto.DatosOportunidad.IdFaseOportunidadIc;
                _reprogramacion.Oportunidad.FechaEnvioFaseOportunidadPf = dto.DatosOportunidad.FechaEnvioFaseOportunidadPf;
                _reprogramacion.Oportunidad.FechaPagoFaseOportunidadPf = dto.DatosOportunidad.FechaPagoFaseOportunidadPf;
                _reprogramacion.Oportunidad.FechaPagoFaseOportunidadIc = dto.DatosOportunidad.FechaPagoFaseOportunidadIc;
                _reprogramacion.Oportunidad.IdFaseOportunidadPf = dto.DatosOportunidad.IdFaseOportunidadPf;
                _reprogramacion.Oportunidad.CodigoPagoIc = dto.DatosOportunidad.CodigoPagoIc;
                _reprogramacion.Oportunidad.ValidacionCorrecta = true;
                _reprogramacion.Usuario = dto.filtro.Usuario;

                int idOcurrenciaAlterno = dto.ActividadAntigua.IdOcurrencia;
                _reprogramacion.ActividadTemp = new ActividadDetalle
                {
                    Id = dto.ActividadAntigua.Id,
                    FechaProgramada = DateTime.Parse(dto.DatosOportunidad.UltimaFechaProgramada),
                    Comentario = dto.ActividadAntigua.Comentario ?? "",
                    IdAlumno = dto.ActividadAntigua.IdAlumno,
                    IdOportunidad = dto.ActividadAntigua.IdOportunidad,
                    IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona,
                    IdOcurrenciaAlterno = dto.ActividadAntigua.IdOcurrencia,
                    IdOcurrenciaActividadAlterno = dto.ActividadAntigua.IdOcurrenciaActividad
                };

                _reprogramacion.ActividadTrabajada = await _unitOfWork.ActividadDetalleRepository.ObtenerPorIdAsync(dto.ActividadAntigua.Id);

                await CargarOportunidadCompetidorReprogramacionAsync(dto.OportunidadCompetidor, dto.ListaCompetidor, dto.Usuario);

                if (dto.CalidadProcesamientoAlterno != null)
                {
                    var calidadPA = dto.CalidadProcesamientoAlterno;
                    _reprogramacion.CalidadProcesamientoAlterno = new CalidadProcesamientoAlterno()
                    {
                        IdOportunidad = calidadPA.IdOportunidad,
                        PerfilCamposLlenos = calidadPA.PerfilCamposLlenos,
                        PerfilCamposTotal = calidadPA.PerfilCamposTotal,
                        TieneDni = calidadPA.TieneDni,
                        SentinelVerificado = calidadPA.SentinelVerificado,
                        PgeneralMotivacionValidado = calidadPA.PgeneralMotivacionValidado,
                        PgeneralMotivacionTotal = calidadPA.PgeneralMotivacionTotal,
                        PublicoObjetivoValidado = calidadPA.PublicoObjetivoValidado,
                        PublicoObjetivoTotal = calidadPA.PublicoObjetivoTotal,
                        PrerequisitoProgramaValidado = calidadPA.PrerequisitoProgramaValidado,
                        PrerequisitoProgramaTotal = calidadPA.PrerequisitoProgramaTotal,
                        RequisitoCertificacionValidado = calidadPA.RequisitoCertificacionValidado,
                        RequisitoCertificacionTotal = calidadPA.RequisitoCertificacionTotal,
                        BeneficiosValidados = calidadPA.BeneficiosValidados,
                        BeneficiosTotales = calidadPA.BeneficiosTotales,
                        InicioProgramaVerificado = calidadPA.InicioProgramaVerificado,
                        CompetidoresVerificacion = calidadPA.CompetidoresVerificacion,
                        CantidadCompetidores = calidadPA.CantidadCompetidores,
                        ProblemaSeleccionados = calidadPA.ProblemaSeleccionados,
                        ProblemaSolucionados = calidadPA.ProblemaSolucionados,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = dto.Usuario,
                        UsuarioModificacion = dto.Usuario,
                        Estado = true,
                    };
                }

                if (_reprogramacion.Oportunidad != null && _reprogramacion.Oportunidad.Id != 0)
                {
                    await FinalizarActividadAlternoAsync(false, dto.DatosOportunidad, dto.ActividadAntigua.IdOcurrenciaActividad);
                    await ProgramaActividadAlternoAsync();
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            if (_reprogramacion.PreCalculadaCambioFase != null)
                            {
                                _reprogramacion.PreCalculadaCambioFase.Contador = _unitOfWork.PreCalculadaCambioFaseRepository.ExistePreCalculadaCambioFase(_reprogramacion.PreCalculadaCambioFase);
                                _unitOfWork.PreCalculadaCambioFaseRepository.AddAsync(_reprogramacion.PreCalculadaCambioFase);
                                await _unitOfWork.CommitAsync();
                            }
                            var faseCierreOportunidad = new List<int>()
                            {
                                ValorEstatico.IdFaseOportunidadD,
                                ValorEstatico.IdFaseOportunidadRN4,
                                ValorEstatico.IdFaseOportunidadNI,
                                ValorEstatico.IdFaseOportunidadIS,
                                ValorEstatico.IdFaseOportunidadRN3,
                                ValorEstatico.IdFaseOportunidadRN2,
                                ValorEstatico.IdFaseOportunidadRN2A
                            };

                            try
                            {

                                var faseCierreEliminarCronograma = new List<int>()
                                {
                                    ValorEstatico.IdFaseOportunidadRN4,
                                    ValorEstatico.IdFaseOportunidadRN3,
                                    ValorEstatico.IdFaseOportunidadRN2,
                                    ValorEstatico.IdFaseOportunidadRN2A,
                                    ValorEstatico.IdFaseOportunidadBIC,
                                    ValorEstatico.IdFaseOportunidadE,
                                    9//RN1
                                };
                                if (faseCierreEliminarCronograma.Contains(_reprogramacion.Oportunidad.IdFaseOportunidad))
                                {
                                    await _unitOfWork.OportunidadRepository.EliminarCronogramaOportunidad(_reprogramacion.Oportunidad.Id);
                                }

                            }
                            catch (Exception e)
                            { }


                            //_unitOfWork.FaseOportunidadRepository.ValidarFaseCierreOportunidad(_reprogramacion.Oportunidad.IdFaseOportunidad)
                            if (faseCierreOportunidad.Contains(_reprogramacion.Oportunidad.IdFaseOportunidad))
                            {
                                var estadoISOM = 1;
                                //_unitOfWork.FaseOportunidadRepository.ValidarFaseIS(_reprogramacion.Oportunidad.IdFaseOportunidad)
                                if (_reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                                    estadoISOM = 0;
                                await _unitOfWork.OportunidadMaximaPorCategoriaRepository.ActualizarDatosEstaticosPantalla2Async(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault(), _reprogramacion.Oportunidad.IdCategoriaOrigen.GetValueOrDefault(), estadoISOM);
                            }
                            var resHoraBloqueda = _unitOfWork.HoraBloqueadaRepository.AddAsync(horaBloqueada);
                            await _unitOfWork.CommitAsync();
                            horaBloqueada = _mapper.Map<HoraBloqueada>(resHoraBloqueda);

                            var actividadDetelleCreada = _unitOfWork.ActividadDetalleRepository.AddAsync(_reprogramacion.ActividadNuevaProgramada);
                            await _unitOfWork.CommitAsync();
                            _reprogramacion.Oportunidad.IdActividadDetalleUltima = actividadDetelleCreada.Id;
                            _reprogramacion.ActividadNuevaProgramada = null;

                            var oportunidadActualizada = _unitOfWork.OportunidadRepository.Update(CargarOportunidad(_reprogramacion));
                            await _unitOfWork.CommitAsync();

                            _reprogramacion.Oportunidad = _mapper.Map<Oportunidad>(oportunidadActualizada);

                            if (dto.filtro.Tipo.ToLower() == "manual")
                            {
                                if (dto.DatosOportunidad.IdFaseOportunidad != _reprogramacion.Oportunidad.IdFaseOportunidad)
                                {
                                    bool validarFaceCierreOportunidad = await _unitOfWork.FaseOportunidadRepository.ValidarFaseCierreOportunidadAsync(_reprogramacion.Oportunidad.IdFaseOportunidad);
                                    if (validarFaceCierreOportunidad)
                                    {
                                        bool validarFaseIs = await _unitOfWork.FaseOportunidadRepository.ValidarFaseISAsync(_reprogramacion.Oportunidad.IdFaseOportunidad);
                                        if (validarFaseIs)
                                        {
                                            await EnviarPlantillaCondicionesAsync(_reprogramacion.Oportunidad);
                                            await GenerarComprobantePagoAsync(dto.ComprobantePago, idOcurrenciaAlterno);
                                            try
                                            {
                                                await _unitOfWork.OportunidadRepository.ActualizarIdPersonalCoordinadorSeguimiento(oportunidadActualizada.Id);
                                            }
                                            catch (Exception exp)
                                            { }
                                        }
                                    }
                                }
                            }
                            else if (dto.filtro.Tipo.ToLower() == "automatica")
                            {
                                var reprogramacionCabecera = await _unitOfWork.ReprogramacionCabeceraRepository.ObtenerPorIdCabeceraIdCategoriaOrigenAsync(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria);
                                if (reprogramacionCabecera != null)
                                {
                                    idReprogramacionCabecera = reprogramacionCabecera.Id;
                                }
                                ReprogramacionCabeceraPersonal reprogramacionCabeceraPersonal = await _unitOfWork.ReprogramacionCabeceraPersonalRepository.ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonalAsync(dto.filtro.IdActividadCabecera, dto.filtro.IdCategoria, dto.filtro.IdPersonal.Value);

                                if (reprogramacionCabeceraPersonal == null || reprogramacionCabeceraPersonal.Id == 0)
                                {
                                    reprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonal
                                    {
                                        IdActividadCabecera = dto.filtro.IdActividadCabecera,
                                        IdCategoriaOrigen = dto.filtro.IdCategoria,
                                        IdPersonal = dto.filtro.IdPersonal.Value,
                                        ReproDia = 1,
                                        FechaReprogramacion = DateTime.Now.Date,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = dto.filtro.Usuario,
                                        UsuarioModificacion = dto.filtro.Usuario,
                                        Estado = true
                                    };
                                    _unitOfWork.ReprogramacionCabeceraPersonalRepository.AddAsync(reprogramacionCabeceraPersonal);
                                    await _unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    if (reprogramacionCabecera == null)
                                        return null;
                                    else
                                    {
                                        if (reprogramacionCabecera.MaxReproPorDia > reprogramacionCabeceraPersonal.ReproDia)
                                        {
                                            reprogramacionCabeceraPersonal.ReproDia++;
                                            reprogramacionCabeceraPersonal.FechaReprogramacion = DateTime.Now.Date;
                                            reprogramacionCabeceraPersonal.FechaModificacion = DateTime.Now;
                                            reprogramacionCabeceraPersonal.UsuarioModificacion = dto.filtro.Usuario;
                                            reprogramacionCabeceraPersonal.Estado = true;
                                            _unitOfWork.ReprogramacionCabeceraPersonalRepository.Update(reprogramacionCabeceraPersonal);
                                            await _unitOfWork.CommitAsync();
                                        }
                                    }
                                }
                            }
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>
                            {
                                "sistemas@bsginstitute.com"
                            };

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "jcayo@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error FinalizarYProgramarActividad Transaction",
                                Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            try
                            {
                                var mailService = new TMK_MailService();
                                mailService.SetData(mailData);
                                mailService.SendMessageTask();
                            }
                            catch { }
                            throw;
                        }
                    }
                    try
                    {
                        //Para poder medir la Calidad de la llamada
                        var calidadLlamadaLog = new CalidadLlamadaLog
                        {
                            IdCalidadLlamada = dto.CalidadLlamada.IdCalidadLlamada,
                            IdProblemaLlamada = dto.CalidadLlamada.IdProblemaLlamada,
                            IdActividadDetalle = dto.ActividadAntigua.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _reprogramacion.Usuario,
                            UsuarioModificacion = _reprogramacion.Usuario,
                            Estado = true
                        };
                        _unitOfWork.CalidadLlamadaLogRepository.AddAsync(calidadLlamadaLog);
                        await _unitOfWork.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>
                        {
                            "sistemas@bsginstitute.com"
                        };

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "jcayo@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error FinalizarYProgramarActividad CalidadLlamada",
                            Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + _reprogramacion.Usuario == null ? "" : _reprogramacion.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };
                        try
                        {
                            TMK_MailService mailService = new TMK_MailService();
                            mailService.SetData(mailData);
                            mailService.SendMessageTask();
                        }
                        catch { }
                    }
                }
                else
                    throw new BadRequestException("No se definio la oportunidad");


                _unitOfWork.OportunidadRepository.ActualizarSeguimientoWhatsAppOportunidad(dto.ActividadAntigua.IdOportunidad, dto.ActividadAntigua.Id, dto.EstadoSeguimientoWhatsApp.GetValueOrDefault());

                CompuestoActividadEjecutadaDTO realizada = new CompuestoActividadEjecutadaDTO();
                try
                {
                    var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                    realizada = await actividadDetalleService.ObtenerAgendaRealizadaRegistroTiempoRealAsync(dto.ActividadAntigua.Id);
                }
                catch (Exception ex)
                {
                    realizada = new CompuestoActividadEjecutadaDTO();

                    List<string> correos = new List<string>
                    {
                        "sistemas@bsginstitute.com"
                    };

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Jojan 2",
                        Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.filtro.Usuario == null ? "" : dto.filtro.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString() + "</br><b> Parametros de entrada </b>" + parametros,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    try
                    {
                        TMK_MailService mailService = new TMK_MailService();
                        mailService.SetData(mailData);
                        mailService.SendMessageTask();
                    }
                    catch { }
                }

                return new RespuestaFinalizarYProgramarActividadAlterno()
                {
                    Realizada = realizada,
                    IdHoraBloqueada = horaBloqueada.Id,
                    IdOportunidad = _reprogramacion.Oportunidad.Id,
                    IdReprogramacionCabecera = idReprogramacionCabecera
                };
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 17/02/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="mantenerEstadoOportunidad"> Confirmación para mantener el estado de oportunidadNuevaEntidad </param>
        /// <param name="datosOportunidad"> Datos de Oportunidad </param>
        /// <returns> FinalizarActividadRespuestaDTO </returns> // ActividadAntigua
        public void FinalizarActividadAlterno(bool mantenerEstadoOportunidad, DatosOportunidadDTO datosOportunidad, int? idOcurrenciaActividadAlterno)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                _reprogramacion.OportunidadLogTemp = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog((int)_reprogramacion.ActividadTemp.IdOportunidad);

                var fechaFinLLamada = DateTime.Now;

                if (_reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno == 0)
                    throw new ArgumentException("Debe seleccionar una Ocurrencia");

                _reprogramacion.ActividadTemp.DuracionReal = 13;
                _reprogramacion.ActividadTemp.IdCentralLlamada = 3;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _unitOfWork.OcurrenciaAlternoRepository.ObtenerPorId(_reprogramacion.ActividadTemp.IdOcurrenciaAlterno.GetValueOrDefault());
                var ocurrenciaActividad = _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadPorId(idOcurrenciaActividadAlterno);
                if (ocurrencia != null && ocurrencia.Id != 0)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado; //2
                }

                //MEXICO
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault());
                _reprogramacion.ActividadTemp.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado; //2
                // Actualizar Actividad Detalle
                if (!_unitOfWork.ActividadDetalleRepository.Exist(_reprogramacion.ActividadTemp.Id))
                {
                    flagError = "";
                    throw new BadRequestException("La oportunidad no tiene Actividad Detalle");
                }

                _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_reprogramacion.ActividadTemp.Id);

                var _fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    _fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.ActividadTrabajada.FechaReal = _fechareal;
                _reprogramacion.ActividadTrabajada.DuracionReal = _reprogramacion.ActividadTemp.DuracionReal;
                _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle = _reprogramacion.ActividadTemp.IdEstadoActividadDetalle;
                _reprogramacion.ActividadTrabajada.Comentario = _reprogramacion.ActividadTemp.Comentario;
                _reprogramacion.ActividadTrabajada.IdCentralLlamada = _reprogramacion.ActividadTemp.IdCentralLlamada;
                _reprogramacion.ActividadTrabajada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadTrabajada.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.ActividadTrabajada.IdClasificacionPersona = _reprogramacion.ActividadTemp.IdClasificacionPersona;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;

                if (ocurrenciaActividad.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo) //31
                {
                    _reprogramacion.Oportunidad.IdFaseOportunidad = ocurrenciaActividad.IdFaseOportunidad.GetValueOrDefault();
                }

                _reprogramacion.Oportunidad.UltimoComentario = _reprogramacion.ActividadTemp.Comentario;
                _reprogramacion.Oportunidad.UltimaFechaProgramada = _reprogramacion.ActividadTemp.FechaProgramada == null ? DateTime.Now : _reprogramacion.ActividadTemp.FechaProgramada;

                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTemp.Id;

                if (datosOportunidad.IdEstadoOportunidad != 0
                    && datosOportunidad.IdEstadoOportunidad == ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada
                    && mantenerEstadoOportunidad)
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.ReasignadaVentaCruzada;
                else
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Ejecutada;

                _reprogramacion.Oportunidad.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (_reprogramacion.Oportunidad.IdFaseOportunidad != 0
                    && datosOportunidad.IdFaseOportunidad != _reprogramacion.Oportunidad.IdFaseOportunidad)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    _reprogramacion.Oportunidad.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria((int)datosOportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdFaseOportunidad);
                }
                _reprogramacion.Oportunidad.FechaModificacion = DateTime.Now;
                _reprogramacion.Oportunidad.UsuarioModificacion = _reprogramacion.Usuario;

                //Crear Log Nuevo
                _reprogramacion.OportunidadLogNueva = new OportunidadLog();
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.OportunidadLogTemp.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.OportunidadLogTemp.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdCentroCosto = _reprogramacion.Oportunidad.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdPersonalAsignado = _reprogramacion.Oportunidad.IdPersonalAsignado;
                _reprogramacion.OportunidadLogNueva.IdTipoDato = _reprogramacion.Oportunidad.IdTipoDato;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidad = _reprogramacion.Oportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdOrigen = _reprogramacion.Oportunidad.IdOrigen;
                _reprogramacion.OportunidadLogNueva.IdContacto = _reprogramacion.Oportunidad.IdAlumno;
                _reprogramacion.OportunidadLogNueva.FechaFinLog = _reprogramacion.OportunidadLogTemp.FechaLog;
                _reprogramacion.OportunidadLogNueva.IdCentroCostoAnt = _reprogramacion.OportunidadLogTemp.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdAsesorAnt = _reprogramacion.OportunidadLogTemp.IdPersonalAsignado;

                var _fechalog = DateTime.Now;

                //MEXICO
                if (diferenciaHoraria != null)
                {
                    _fechalog = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.OportunidadLogNueva.FechaLog = _fechalog;
                _reprogramacion.OportunidadLogNueva.IdActividadDetalle = _reprogramacion.ActividadTemp.Id;
                _reprogramacion.OportunidadLogNueva.Comentario = _reprogramacion.Oportunidad.UltimoComentario;
                _reprogramacion.OportunidadLogNueva.IdOportunidad = _reprogramacion.Oportunidad.Id;
                _reprogramacion.OportunidadLogNueva.IdCategoriaOrigen = _reprogramacion.Oportunidad.IdCategoriaOrigen;
                _reprogramacion.OportunidadLogNueva.IdSubCategoriaDato = _reprogramacion.Oportunidad.IdSubCategoriaDato;
                _reprogramacion.OportunidadLogNueva.FechaRegistroCampania = _reprogramacion.Oportunidad.FechaRegistroCampania;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                _reprogramacion.OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.Oportunidad.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;

                if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != _reprogramacion.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _reprogramacion.OportunidadLogNueva.CambioFase = true;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFase = _reprogramacion.OportunidadLogNueva.FechaLog;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseAnt = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.CambioFaseAsesor = 1;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesor = _reprogramacion.OportunidadLogNueva.FechaLog;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesorAnt = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;

                    if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad != 0
                        && _reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                    {
                        _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogNueva.FechaLog;
                        _reprogramacion.OportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs;
                        _reprogramacion.OportunidadLogNueva.CambioFaseIs = false;
                    }

                    if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad != 0
                        && _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != 0
                        && (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 || _reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2A)
                        && (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2 || _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2A))
                        _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2 + 1;
                    else
                        _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2;
                }
                else
                {
                    _reprogramacion.OportunidadLogNueva.CambioFase = false;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFase = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseAnt = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogTemp.FechaCambioFaseIs;
                    _reprogramacion.OportunidadLogNueva.CambioFaseIs = false;
                    _reprogramacion.OportunidadLogNueva.CambioFaseAsesor = 0;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesor = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesorAnt = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                    _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2;
                }
                _reprogramacion.OportunidadLogNueva.FechaCreacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.FechaModificacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.UsuarioCreacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.Estado = true;

                if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != _reprogramacion.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _reprogramacion.PreCalculadaCambioFase = new PreCalculadaCambioFase()
                    {
                        IdPersonal = _reprogramacion.OportunidadLogNueva.IdPersonalAsignado,
                        Fecha = DateTime.Now,
                        IdCategoriaOrigen = _reprogramacion.OportunidadLogNueva.IdCategoriaOrigen,
                        IdCentroCosto = _reprogramacion.OportunidadLogNueva.IdCentroCosto,
                        IdFaseOportunidadDestino = _reprogramacion.OportunidadLogNueva.IdFaseOportunidad,
                        IdFaseOportunidadOrigen = _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt,
                        IdOrigen = _reprogramacion.Oportunidad.IdOrigen,
                        IdTipoDato = _reprogramacion.Oportunidad.IdTipoDato,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _reprogramacion.Usuario,
                        UsuarioModificacion = _reprogramacion.Usuario,
                        Estado = true
                    };
                }

                if (_reprogramacion.OportunidadCompetidor != null && _reprogramacion.OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    _reprogramacion.OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    _reprogramacion.OportunidadCompetidor.UsuarioModificacion = _reprogramacion.Usuario;
                }
                _reprogramacion.ActividadTemp = null;
                _reprogramacion.OportunidadLogTemp = null;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message + "-" + flagError);
            }
        }

        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 17/02/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="mantenerEstadoOportunidad"> Confirmación para mantener el estado de oportunidadNuevaEntidad </param>
        /// <param name="datosOportunidad"> Datos de Oportunidad </param>
        /// <returns> FinalizarActividadRespuestaDTO </returns> // ActividadAntigua
        private async Task FinalizarActividadAlternoAsync(bool mantenerEstadoOportunidad, DatosOportunidadDTO datosOportunidad, int? idOcurrenciaActividadAlterno)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                var taskOportunidadLogTemp = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLogAsync((int)_reprogramacion.ActividadTemp.IdOportunidad);
                var ocurrencia = await _unitOfWork.OcurrenciaAlternoRepository.ObtenerPorIdAsync(_reprogramacion.ActividadTemp.IdOcurrenciaAlterno.GetValueOrDefault());
                var ocurrenciaActividad = await _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadPorIdAsync(idOcurrenciaActividadAlterno);

                var fechaFinLLamada = DateTime.Now;

                if (_reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno == 0)
                    throw new ArgumentException("Debe seleccionar una Ocurrencia");

                _reprogramacion.ActividadTemp.DuracionReal = 13;
                _reprogramacion.ActividadTemp.IdCentralLlamada = 3;

                flagError = "ObtenerOcurrenciaPorActividad";

                if (ocurrencia != null && ocurrencia.Id != 0)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    bool flagValidarEstadoOcurrencia = await _unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrenciaAsync(ocurrencia.Id);
                    if (flagValidarEstadoOcurrencia)
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                }

                //MEXICO
                var diferenciaHoraria = await _unitOfWork.PersonalRepository.ObtenerDiferenciaHorariaAsync(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault());
                _reprogramacion.ActividadTemp.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                // Actualizar Actividad Detalle
                if (!_unitOfWork.ActividadDetalleRepository.Exist(_reprogramacion.ActividadTemp.Id))
                {
                    flagError = "";
                    throw new BadRequestException("La oportunidad no tiene Actividad Detalle");
                }

                _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_reprogramacion.ActividadTemp.Id);
                var _fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    _fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.ActividadTrabajada.FechaReal = _fechareal;
                _reprogramacion.ActividadTrabajada.DuracionReal = _reprogramacion.ActividadTemp.DuracionReal;
                _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle = _reprogramacion.ActividadTemp.IdEstadoActividadDetalle;
                _reprogramacion.ActividadTrabajada.Comentario = _reprogramacion.ActividadTemp.Comentario;
                _reprogramacion.ActividadTrabajada.IdCentralLlamada = _reprogramacion.ActividadTemp.IdCentralLlamada;
                _reprogramacion.ActividadTrabajada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadTrabajada.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.ActividadTrabajada.IdClasificacionPersona = _reprogramacion.ActividadTemp.IdClasificacionPersona;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;

                if (ocurrenciaActividad.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo) //31
                {
                    _reprogramacion.Oportunidad.IdFaseOportunidad = ocurrenciaActividad.IdFaseOportunidad.GetValueOrDefault();
                }

                _reprogramacion.Oportunidad.UltimoComentario = _reprogramacion.ActividadTemp.Comentario;
                _reprogramacion.Oportunidad.UltimaFechaProgramada = _reprogramacion.ActividadTemp.FechaProgramada == null ? DateTime.Now : _reprogramacion.ActividadTemp.FechaProgramada;

                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTemp.Id;

                if (datosOportunidad.IdEstadoOportunidad != 0
                    && datosOportunidad.IdEstadoOportunidad == ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada
                    && mantenerEstadoOportunidad)
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.ReasignadaVentaCruzada;
                else
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Ejecutada;

                _reprogramacion.Oportunidad.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (_reprogramacion.Oportunidad.IdFaseOportunidad != 0
                    && datosOportunidad.IdFaseOportunidad != _reprogramacion.Oportunidad.IdFaseOportunidad && _reprogramacion.Oportunidad.IdFaseOportunidad != 23)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    _reprogramacion.Oportunidad.IdFaseOportunidadMaxima = await _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoriaAsync((int)datosOportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdFaseOportunidad);
                }
                _reprogramacion.Oportunidad.FechaModificacion = DateTime.Now;
                _reprogramacion.Oportunidad.UsuarioModificacion = _reprogramacion.Usuario;

                _reprogramacion.OportunidadLogTemp = await taskOportunidadLogTemp;
                //Crear Log Nuevo
                _reprogramacion.OportunidadLogNueva = new OportunidadLog();
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.OportunidadLogTemp.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.OportunidadLogTemp.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdCentroCosto = _reprogramacion.Oportunidad.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdPersonalAsignado = _reprogramacion.Oportunidad.IdPersonalAsignado;
                _reprogramacion.OportunidadLogNueva.IdTipoDato = _reprogramacion.Oportunidad.IdTipoDato;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidad = _reprogramacion.Oportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdOrigen = _reprogramacion.Oportunidad.IdOrigen;
                _reprogramacion.OportunidadLogNueva.IdContacto = _reprogramacion.Oportunidad.IdAlumno;
                _reprogramacion.OportunidadLogNueva.FechaFinLog = _reprogramacion.OportunidadLogTemp.FechaLog;
                _reprogramacion.OportunidadLogNueva.IdCentroCostoAnt = _reprogramacion.OportunidadLogTemp.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdAsesorAnt = _reprogramacion.OportunidadLogTemp.IdPersonalAsignado;

                var _fechalog = DateTime.Now;

                //MEXICO
                if (diferenciaHoraria != null)
                {
                    _fechalog = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.OportunidadLogNueva.FechaLog = _fechalog;
                _reprogramacion.OportunidadLogNueva.IdActividadDetalle = _reprogramacion.ActividadTemp.Id;
                _reprogramacion.OportunidadLogNueva.Comentario = _reprogramacion.Oportunidad.UltimoComentario;
                _reprogramacion.OportunidadLogNueva.IdOportunidad = _reprogramacion.Oportunidad.Id;
                _reprogramacion.OportunidadLogNueva.IdCategoriaOrigen = _reprogramacion.Oportunidad.IdCategoriaOrigen;
                _reprogramacion.OportunidadLogNueva.IdSubCategoriaDato = _reprogramacion.Oportunidad.IdSubCategoriaDato;
                _reprogramacion.OportunidadLogNueva.FechaRegistroCampania = _reprogramacion.Oportunidad.FechaRegistroCampania;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                _reprogramacion.OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.Oportunidad.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;

                if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != _reprogramacion.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _reprogramacion.OportunidadLogNueva.CambioFase = true;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFase = _reprogramacion.OportunidadLogNueva.FechaLog;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseAnt = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.CambioFaseAsesor = 1;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesor = _reprogramacion.OportunidadLogNueva.FechaLog;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesorAnt = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;

                    if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad != 0
                        && _reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                    {
                        _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogNueva.FechaLog;
                        _reprogramacion.OportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs;
                        _reprogramacion.OportunidadLogNueva.CambioFaseIs = false;
                    }

                    if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad != 0
                        && _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != 0
                        && (_reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 || _reprogramacion.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2A)
                        && (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2 || _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2A))
                        _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2 + 1;
                    else
                        _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2;
                }
                else
                {
                    _reprogramacion.OportunidadLogNueva.CambioFase = false;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFase = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseAnt = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                    _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogTemp.FechaCambioFaseIs;
                    _reprogramacion.OportunidadLogNueva.CambioFaseIs = false;
                    _reprogramacion.OportunidadLogNueva.CambioFaseAsesor = 0;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesor = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                    _reprogramacion.OportunidadLogNueva.FechaCambioAsesorAnt = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                    _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2;
                }
                _reprogramacion.OportunidadLogNueva.FechaCreacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.FechaModificacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.UsuarioCreacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.Estado = true;

                if (_reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt != _reprogramacion.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _reprogramacion.PreCalculadaCambioFase = new PreCalculadaCambioFase()
                    {
                        IdPersonal = _reprogramacion.OportunidadLogNueva.IdPersonalAsignado,
                        Fecha = DateTime.Now,
                        IdCategoriaOrigen = _reprogramacion.OportunidadLogNueva.IdCategoriaOrigen,
                        IdCentroCosto = _reprogramacion.OportunidadLogNueva.IdCentroCosto,
                        IdFaseOportunidadDestino = _reprogramacion.OportunidadLogNueva.IdFaseOportunidad,
                        IdFaseOportunidadOrigen = _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt,
                        IdOrigen = _reprogramacion.Oportunidad.IdOrigen,
                        IdTipoDato = _reprogramacion.Oportunidad.IdTipoDato,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _reprogramacion.Usuario,
                        UsuarioModificacion = _reprogramacion.Usuario,
                        Estado = true
                    };
                }
                else
                {
                    _reprogramacion.PreCalculadaCambioFase = null;
                }

                if (_reprogramacion.OportunidadCompetidor != null && _reprogramacion.OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    _reprogramacion.OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    _reprogramacion.OportunidadCompetidor.UsuarioModificacion = _reprogramacion.Usuario;
                }
                _reprogramacion.ActividadTemp = null;
                _reprogramacion.OportunidadLogTemp = null;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message + "-" + flagError);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/02/2021
        /// Version: 1.0
        /// <summary>
        /// Programar Actividad alternos
        /// </summary>
        /// <param name="actividadNueva"> Datos de Nueva Actividad </param>
        /// <param name="oportunidadNuevaEntidad"> Datos de Oportunidad </param>
        /// <returns>  </returns>
        private void ProgramaActividadAlterno()
        {
            try
            {
                var ocurrencia = _unitOfWork.OcurrenciaAlternoRepository.ObtenerPorId((int)_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno.Value);
                var ocurrenciaActividad = _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadPorId((int)_reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno);
                if (_reprogramacion.ActividadTrabajada.IdOportunidad == 0)
                {
                    throw new ArgumentException("Debe tener una oportunidad");
                }

                if (_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == 0 || _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == null)
                {
                    _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                            ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;

                        if (ocurrenciaActividad.IdActividadCabeceraProgramada == null)
                        {
                            if (!(ocurrenciaActividad.IdActividadCabeceraProgramada == null))
                            {
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ocurrenciaActividad.IdActividadCabeceraProgramada;
                            }
                            else
                            {
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                            }
                        }
                        else
                        {
                            if (_reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161)
                            {
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                            }
                            else
                            {
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ocurrenciaActividad.IdActividadCabeceraProgramada;
                            }
                        }
                    }
                }
                _reprogramacion.ActividadNuevaProgramada = new ActividadDetalle();
                _reprogramacion.ActividadNuevaProgramada.IdOportunidad = _reprogramacion.ActividadTrabajada.IdOportunidad;
                _reprogramacion.ActividadNuevaProgramada.IdAlumno = _reprogramacion.ActividadTrabajada.IdAlumno;
                _reprogramacion.ActividadNuevaProgramada.Actor = "A";
                _reprogramacion.ActividadNuevaProgramada.FechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ? _reprogramacion.Oportunidad.UltimaFechaProgramada.Value : default;
                _reprogramacion.ActividadNuevaProgramada.IdEstadoActividadDetalle = EstadoActividadDetalle.NoEjecutado;
                _reprogramacion.ActividadNuevaProgramada.FechaCreacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.UsuarioCreacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.UsuarioModificacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.IdActividadCabecera = _reprogramacion.ActividadTrabajada.IdActividadCabecera;
                _reprogramacion.ActividadNuevaProgramada.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno;
                _reprogramacion.ActividadNuevaProgramada.Estado = true;
                //Actualiza Oportunidad

                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTrabajada.Id;
                _reprogramacion.Oportunidad.IdActividadCabeceraUltima = _reprogramacion.ActividadTrabajada.IdActividadCabecera.Value;
                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle;
                _reprogramacion.Oportunidad.UltimaFechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ? _reprogramacion.Oportunidad.UltimaFechaProgramada.Value : default(DateTime);
                _reprogramacion.Oportunidad.UltimoComentario = _reprogramacion.ActividadTrabajada.Comentario;

                if (_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == 35) //OCURRENCIA_ASIGNACION_MANUAL
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Reasignada;
                else
                {
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ?
                                                        EstadoOportunidad.Programada :
                                                        EstadoOportunidad.NoProgramada;
                }

                //var grupoPrelanzamiento = _unitOfWork.OcurrenciaRepository.ValidarGrupoPreLanzamiento(Reprogramacion.Oportunidad.IdCategoriaOrigen.Value);

                //if (Reprogramacion.Oportunidad.IdEstadoOportunidad == EstadoOportunidad.NoProgramada
                //    && ocurrencia.IdEstadoOcurrencia == EstadoOcurrencia.NoEjecutado
                //    && grupoPrelanzamiento == 1
                //    && Reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC
                //)
                //{
                //    Reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.NoProgramada;
                //}

                //if (Reprogramacion.Oportunidad.IdEstadoOportunidad != 0
                //    && Reprogramacion.Oportunidad.IdEstadoOportunidad.Equals(EstadoOportunidad.ReasignadaVentaCruzada)
                //    && false)
                //{
                //    Reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.ReasignadaVentaCruzada;
                //}
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/02/2021
        /// Version: 1.0
        /// <summary>
        /// Programar Actividad alternos
        /// </summary>
        /// <param name="actividadNueva"> Datos de Nueva Actividad </param>
        /// <param name="oportunidadNuevaEntidad"> Datos de Oportunidad </param>
        /// <returns>  </returns>
        private async Task ProgramaActividadAlternoAsync()
        {
            try
            {
                OcurrenciaAlterno ocurrencia = await _unitOfWork.OcurrenciaAlternoRepository.ObtenerPorIdAsync((int)_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno.Value);
                OcurenciaActividadCompletoDTO ocurrenciaActividad = await _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadPorIdAsync((int)_reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno.Value);

                if (_reprogramacion.ActividadTrabajada.IdOportunidad == 0)
                    throw new ArgumentException("Debe tener una oportunidad");

                if (_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == 0 || _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == null)
                {
                    _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        bool validarEstadoOcurrencia = await _unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrenciaAsync(ocurrencia.Id);
                        if (validarEstadoOcurrencia)
                        {
                            ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                        }
                        bool flagObtenerActividadCabecera = false;

                        if (ocurrenciaActividad.IdActividadCabeceraProgramada == null)
                        {
                            _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                        }
                        else
                        {
                            if (_reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161)
                            {
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(_reprogramacion.Oportunidad.IdFaseOportunidad, _reprogramacion.Oportunidad.IdTipoDato, _reprogramacion.Oportunidad.IdPersonalAreaTrabajo, _reprogramacion.Oportunidad.IdActividadCabeceraUltima);
                            }
                            else
                                _reprogramacion.ActividadTrabajada.IdActividadCabecera = ocurrenciaActividad.IdActividadCabeceraProgramada;
                        }
                    }
                }
                _reprogramacion.ActividadNuevaProgramada = new ActividadDetalle();
                _reprogramacion.ActividadNuevaProgramada.IdOportunidad = _reprogramacion.ActividadTrabajada.IdOportunidad;
                _reprogramacion.ActividadNuevaProgramada.IdAlumno = _reprogramacion.ActividadTrabajada.IdAlumno;
                _reprogramacion.ActividadNuevaProgramada.Actor = "A";
                _reprogramacion.ActividadNuevaProgramada.FechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ? _reprogramacion.Oportunidad.UltimaFechaProgramada.Value : default;
                _reprogramacion.ActividadNuevaProgramada.IdEstadoActividadDetalle = EstadoActividadDetalle.NoEjecutado;
                _reprogramacion.ActividadNuevaProgramada.FechaCreacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.UsuarioCreacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.UsuarioModificacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.IdActividadCabecera = _reprogramacion.ActividadTrabajada.IdActividadCabecera;
                _reprogramacion.ActividadNuevaProgramada.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno;
                _reprogramacion.ActividadNuevaProgramada.Estado = true;
                //Actualiza Oportunidad

                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTrabajada.Id;
                _reprogramacion.Oportunidad.IdActividadCabeceraUltima = _reprogramacion.ActividadTrabajada.IdActividadCabecera.Value;
                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle;
                _reprogramacion.Oportunidad.UltimaFechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ? _reprogramacion.Oportunidad.UltimaFechaProgramada.Value : default;
                _reprogramacion.Oportunidad.UltimoComentario = _reprogramacion.ActividadTrabajada.Comentario;

                if (_reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno == 35) //OCURRENCIA_ASIGNACION_MANUAL
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Reasignada;
                else
                {
                    _reprogramacion.Oportunidad.IdEstadoOportunidad = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ?
                                                        EstadoOportunidad.Programada :
                                                        EstadoOportunidad.NoProgramada;
                }

                //var grupoPrelanzamiento = await _unitOfWork.OcurrenciaRepository.ValidarGrupoPreLanzamientoAsync(_reprogramacion.Oportunidad.IdCategoriaOrigen.Value);

                //if (_reprogramacion.Oportunidad.IdEstadoOportunidad == EstadoOportunidad.NoProgramada
                //    && ocurrencia.IdEstadoOcurrencia == EstadoOcurrencia.NoEjecutado
                //    && grupoPrelanzamiento == 1
                //    && _reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC
                //)
                //    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.NoProgramada;

                //if (_reprogramacion.Oportunidad.IdEstadoOportunidad != 0
                //    && _reprogramacion.Oportunidad.IdEstadoOportunidad.Equals(EstadoOportunidad.ReasignadaVentaCruzada)
                //    && false)
                //    _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.ReasignadaVentaCruzada;
            }
            catch
            {
                throw;
            }
        }

        public void GenerarComprobantePago(ComprobantePagoOportunidadDTO comprobantePago, int idOcurrenciaAlterno)
        {
            if (idOcurrenciaAlterno == ValorEstatico.IdOcurrenciaConfirmaPagoIs || idOcurrenciaAlterno == ValorEstatico.IdOcurrenciaIsSinLlamada)
            {
                var comprobantePagoOportunidad = new ComprobantePagoOportunidad
                {
                    IdContacto = comprobantePago.IdContacto,
                    Nombres = comprobantePago.Nombres,
                    Apellidos = comprobantePago.Apellidos,
                    Celular = comprobantePago.Celular,
                    Dni = comprobantePago.Dni == null ? "" : comprobantePago.Dni,
                    Correo = comprobantePago.Correo,
                    NombrePais = comprobantePago.NombrePais,
                    IdPais = comprobantePago.IdPais,
                    NombreCiudad = comprobantePago.NombreCiudad == null ? "" : comprobantePago.NombreCiudad,
                    TipoComprobante = comprobantePago.TipoComprobante,
                    NroDocumento = comprobantePago.NroDocumento != null ? comprobantePago.NroDocumento : "",
                    NombreRazonSocial = comprobantePago.NombreRazonSocial ?? "",
                    Direccion = comprobantePago.Direccion != null ? comprobantePago.Direccion : "",
                    BitComprobante = comprobantePago.BitComprobante,
                    IdOcurrencia = comprobantePago.IdOcurrencia,
                    IdAsesor = comprobantePago.IdAsesor,
                    IdOportunidad = comprobantePago.IdOportunidad,
                    Comentario = comprobantePago.Comentario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _reprogramacion.Usuario,
                    UsuarioModificacion = _reprogramacion.Usuario,
                    Estado = true
                };

                var comprobantePagoOportunidadService = new ComprobantePagoOportunidadService(_unitOfWork);
                var comprobanteNuevo = comprobantePagoOportunidadService.Add(comprobantePagoOportunidad);
                if (comprobanteNuevo != null && comprobanteNuevo.Id != 0)
                {
                    //Enviar Correo Finanzas  Boleta = 0 && Factura = 1
                    List<string> correos = new List<string>
                    {
                        "lcalla@bsginstitute.com",
                        //"rchauca@bsginstitute.com",
                        //"mzegarraj@bsginstitute.com",
                        "ccrispin@bsginstitute.com",
                        "dpacheco@bsginstitute.com"
                    };
                    string mensaje = comprobantePagoOportunidadService.MensajeEmailComprobantePago(comprobanteNuevo);

                    TMK_MailService mailService = new TMK_MailService();
                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "BSG INSTITUTE - Datos Alumno en IS ",
                        Message = mensaje,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null,
                        RemitenteC = comprobantePago.Nombres
                    };
                    try
                    {
                        mailService.SetData(mailData);
                        mailService.SendMessageTask();
                    }
                    catch { }
                }
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/04/2023
        /// Versión: 2.0
        /// Autor Modificacion: Gilmer Quispe
        /// Fecha Modificacion: 26/09/2024
        /// Versión: 2.1
        /// Modificacion: Se agrego una funcion (ObtenerComprobantePagoNotificacionCorreo) para obtener los correos configurados de manera dinámica.
        /// <summary>
        /// Obtiene Los Valores Para Etiquetas Lista de Programas
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idAreaEtiqueta">Id del area de la etiqueta (PK de la tabla mkt.T_AreaCampoEtiqueta)</param>
        /// <returns>String</returns>
        public async Task GenerarComprobantePagoAsync(ComprobantePagoOportunidadDTO comprobantePago, int idOcurrenciaAlterno)
        {
            if (idOcurrenciaAlterno == ValorEstatico.IdOcurrenciaConfirmaPagoIs || idOcurrenciaAlterno == ValorEstatico.IdOcurrenciaIsSinLlamada)
            {
                var comprobantePagoOportunidad = new ComprobantePagoOportunidad
                {
                    IdContacto = comprobantePago.IdContacto,
                    Nombres = comprobantePago.Nombres,
                    Apellidos = comprobantePago.Apellidos,
                    Celular = comprobantePago.Celular,
                    Dni = comprobantePago.Dni == null ? "" : comprobantePago.Dni,
                    Correo = comprobantePago.Correo,
                    NombrePais = comprobantePago.NombrePais,
                    IdPais = comprobantePago.IdPais,
                    NombreCiudad = comprobantePago.NombreCiudad == null ? "" : comprobantePago.NombreCiudad,
                    TipoComprobante = comprobantePago.TipoComprobante,
                    NroDocumento = comprobantePago.NroDocumento != null ? comprobantePago.NroDocumento : "",
                    NombreRazonSocial = comprobantePago.NombreRazonSocial ?? "",
                    Direccion = comprobantePago.Direccion != null ? comprobantePago.Direccion : "",
                    BitComprobante = comprobantePago.BitComprobante,
                    IdOcurrencia = comprobantePago.IdOcurrencia,
                    IdAsesor = comprobantePago.IdAsesor,
                    IdOportunidad = comprobantePago.IdOportunidad,
                    Comentario = comprobantePago.Comentario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = _reprogramacion.Usuario,
                    UsuarioModificacion = _reprogramacion.Usuario,
                    Estado = true
                };

                var comprobanteNuevo = _unitOfWork.ComprobantePagoOportunidadRepository.AddAsync(comprobantePagoOportunidad);
                await _unitOfWork.CommitAsync();
                if (comprobanteNuevo != null && comprobanteNuevo.Id != 0)
                {
                    //Enviar Correo Finanzas Boleta = 0 && Factura = 1
                    List<CorreoNotificacionDTO> comprobantePagoNotificacionCorreos = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.ComprobantePago).ToList();

                    List<string> correos = new List<string>();
                    correos = comprobantePagoNotificacionCorreos.Where(x => x.IdPais == null).ToList().Select(y => y.Email).ToList();
                    if (comprobantePago.IdPais == 51)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 51).ToList().Select(y => y.Email).ToList());
                    if (comprobantePago.IdPais == 52)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 52).ToList().Select(y => y.Email).ToList());
                    if (comprobantePago.IdPais == 56)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 56).ToList().Select(y => y.Email).ToList());
                    if (comprobantePago.IdPais == 57)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 57).ToList().Select(y => y.Email).ToList());

                    var comprobantePagoOportunidadService = new ComprobantePagoOportunidadService(_unitOfWork);
                    string mensaje = comprobantePagoOportunidadService.MensajeEmailComprobantePago(_mapper.Map<ComprobantePagoOportunidad>(comprobanteNuevo));

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "jcayo@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "BSG INSTITUTE - Datos Alumno en IS ",
                        Message = mensaje,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null,
                        RemitenteC = comprobantePago.Nombres
                    };
                    try
                    {
                        TMK_MailService mailService = new TMK_MailService();
                        mailService.SetData(mailData);
                        mailService.SendMessageTask();
                    }
                    catch { }
                }
            }
        }

        public void EnviarPlantillaCondiciones(Oportunidad oportunidad)
        {
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);
                var oportunidadService = new OportunidadService(_unitOfWork);
                var pGeneralTipoDescuentoService = new PGeneralTipoDescuentoService(_unitOfWork);
                var datosCompuestos = oportunidadService.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                var promocion = pGeneralTipoDescuentoService.ObtenerFlagPromocion(datosCompuestos.IdPgeneral.Value, 143).Valor;

                var montoPagoCronogramaService = new MontoPagoCronogramaService(_unitOfWork);
                string costoTotalDescuento = montoPagoCronogramaService.ObtenerCostoTotalConDescuento(oportunidad.Id);

                datosCompuestos.Promocion25 = promocion;
                datosCompuestos.CostoTotalConDescuento = costoTotalDescuento;

                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);
                var idPlantilla = 0;
                var idPlantilla2 = 0;

                if (alumno.IdCodigoPais == 57)//Colombia
                    idPlantilla = 1247; //Nuevas Condiciones y Características -Colombia
                else if (alumno.IdCodigoPais == 51)//Peru
                    idPlantilla = 1246; //Nuevas Condiciones y Características -Perú
                else if (alumno.IdCodigoPais == 52)//Mexico
                {
                    idPlantilla = 1401; //Condiciones y Características - Mexico
                    idPlantilla2 = 1402; //Contrato de uso de datos biométricos por voz  - México
                }

                if (idPlantilla != 0)
                {
                    string fechaInicioPrograma = plantillaPwService.ObtenerFechaInicioPrograma(datosCompuestos.IdPgeneral.Value, datosCompuestos.IdCentroCosto.Value);

                    plantillaPwService.ObtenerValorEtiqueta(oportunidad.IdCentroCosto.Value, oportunidad.Id);
                    plantillaPwService.ObtenerDatosProgramaGeneral(datosCompuestos.IdPgeneral.Value);

                    var emailCalculado = plantillaPwService.ReemplazarEtiquetas(idPlantilla, oportunidad.Id);

                    List<string> correosPersonalizadosCopia = new List<string>
                    {
                        //"ccrispin@bsginstitute.com"
                        "grabaciones@bsginstitute.com",
                        //"mzegarraj@bsginstitute.com",
                        //"rchauca@bsginstitute.com",
                        "dpacheco@bsginstitute.com",
                        "bamontoya@bsginstitute.com",
                        "mcabanan@bsginstitute.com",
                        "validacionenvios@bsginstitute.com",
                        "aportillavi@bsginstitute.com",
                        "jcacerest@bsginstitute.com"
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                        plantillaPwService.DatosOportunidadAlumno().OportunidadAlumno.Email1
                    };

                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "matriculas@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = "",
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();
                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    var documentoEnviadoWebPwBO = new DocumentoEnviadoWebPw()
                    {
                        IdAlumno = oportunidad.IdAlumno.Value,
                        Nombre = "BSG Institute - Condiciones y Características",
                        IdPespecifico = plantillaPwService.DatosOportunidadAlumno().IdPEspecifico,
                        FechaEnvio = DateTime.Now,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _reprogramacion.Usuario,
                        UsuarioModificacion = _reprogramacion.Usuario,
                        Estado = true
                    };
                    if (documentoEnviadoWebPwBO != null)
                    {
                        _unitOfWork.DocumentoEnviadoWebPwRepository.Add(documentoEnviadoWebPwBO);
                        _unitOfWork.Commit();
                    }

                    if (idPlantilla2 != 0)
                    {
                        //reemplazo 2

                        var emailCalculado2 = plantillaPwService.ReemplazarEtiquetas(idPlantilla2, oportunidad.Id);

                        var mailDataPersonalizado2 = new TMKMailDataDTO
                        {
                            Sender = "matriculas@bsginstitute.com",
                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                            Subject = emailCalculado2.Asunto,
                            Message = emailCalculado2.CuerpoHTML,
                            Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                            Bcc = "ccrispin@bsginstitute.com",
                            AttachedFiles = emailCalculado2.ListaArchivosAdjuntos
                        };
                        var mailServie2 = new TMK_MailService();
                        mailServie2.SetData(mailDataPersonalizado2);
                        mailServie2.SendMessageTask();
                    }
                }

            }
            catch { }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/04/2022
        /// Versión: 2.0
        /// <summary>
        /// Envio de Correos Nuevas condiciones y Caracteristicas
        /// </summary>
        /// <param name="oportunidad">Registro de Oportunidad</param>
        /// <returns>String</returns>
        public async Task EnviarPlantillaCondicionesAsync(Oportunidad oportunidad)
        {
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);
                var datosCompuestos = await _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidadAsync(oportunidad.Id);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);

                var idPlantilla = 0;
                var idPlantilla2 = 0;
                IntDTO idCodigoPais = await _unitOfWork.AlumnoRepository.ObtenerIdPaisPorIdAlumnoAsync(oportunidad.IdAlumno.Value);

                if (idCodigoPais.Valor == 57)//Colombia
                    idPlantilla = 1247; //Nuevas Condiciones y Características -Colombia
                else if (idCodigoPais.Valor == 51)//Peru
                    idPlantilla = 1246; //Nuevas Condiciones y Características -Perú
                else if (idCodigoPais.Valor == 52)//Mexico
                {
                    idPlantilla = 1401; //Condiciones y Características - Mexico
                    idPlantilla2 = 1402; //Contrato de uso de datos biométricos por voz  - México
                }
                else if (idCodigoPais.Valor == 56)//Chile
                    idPlantilla = 1623; //Nuevas Condiciones y Características -Perú

                if (idPlantilla != 0)
                {
                    await plantillaPwService.CargarDatosOportunidadAlumnoReprogramacion(datosCompuestos.IdPgeneral.Value, oportunidad.IdCentroCosto.Value, oportunidad.Id);

                    List<CorreoNotificacionDTO> comprobantePagoNotificacionCorreos = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.CondicionesCaracteristicas).ToList();

                    List<string> correos = new List<string>();
                    correos = comprobantePagoNotificacionCorreos.Where(x => x.IdPais == null).ToList().Select(y => y.Email).ToList();
                    if (idCodigoPais.Valor == 51)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 51).ToList().Select(y => y.Email).ToList());
                    if (idCodigoPais.Valor == 52)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 52).ToList().Select(y => y.Email).ToList());
                    if (idCodigoPais.Valor == 56)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 56).ToList().Select(y => y.Email).ToList());
                    if (idCodigoPais.Valor == 57)
                        correos.AddRange(comprobantePagoNotificacionCorreos.Where(x => x.IdPais == 57).ToList().Select(y => y.Email).ToList());

                    List<string> correosPersonalizadosCopia = new List<string>
                    {
                        "grabaciones@bsginstitute.com",
                        "validacionenvios@bsginstitute.com"
                    };
                    correosPersonalizadosCopia.AddRange(correos);
                    List<string> correosPersonalizados = new List<string>
                    {
                        plantillaPwService.DatosOportunidadAlumno().OportunidadAlumno.Email1
                    };

                    PlantillaEmailMandrillDTO emailCalculado = await plantillaPwService.ReemplazarEtiquetasAsync(idPlantilla, oportunidad.Id);
                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "matriculas@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = "",
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    try
                    {
                        var mailService = new TMK_MailService();
                        mailService.SetData(mailDataPersonalizado);
                        mailService.SendMessageTask();
                    }
                    catch { }


                    //se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda
                    mailDataPersonalizado.Sender = personal.Email;
                    mailDataPersonalizado.Cc = "";
                    try
                    {
                        var mailService_1 = new TMK_MailService();
                        var listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();
                        mailService_1.SetData(mailDataPersonalizado);
                        List<TMKMensajeIdDTO> MensajeIdDTO = mailService_1.SendMessageTask();

                        foreach (var mensaje in MensajeIdDTO)
                        {
                            var validarEmail = _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(mensaje.Email);
                            int idAlumno = validarEmail == null ? 0 : validarEmail.Id;
                            var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                            {
                                IdOportunidad = oportunidad.Id,
                                IdPersonal = oportunidad.IdPersonalAsignado,
                                IdAlumno = idAlumno,
                                IdCentroCosto = oportunidad.IdCentroCosto,
                                IdMandrilTipoAsignacion = oportunidad.Id == 0 ? 4 : 0, //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                                EstadoEnvio = 1,
                                IdMandrilTipoEnvio = 2, //Manual = 2
                                FechaEnvio = DateTime.Now,
                                Asunto = emailCalculado.Asunto,
                                FkMandril = mensaje.MensajeId,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "EnvioAutomaticoCondiciones",
                                UsuarioModificacion = "EnvioAutomaticoCondiciones",
                                EsEnvioMasivo = false
                            };
                            listaMandrilEnvioCorreo.Add(mandrilEnvioCorreoEntidad);
                        }

                        //Logica Guardar Correo
                        GmailCorreo gmailCorreo = new GmailCorreo
                        {
                            IdEtiqueta = 1,//sent:1 , inbox:2
                            Asunto = emailCalculado.Asunto,
                            Fecha = DateTime.Now,
                            EmailBody = emailCalculado.CuerpoHTML,
                            Seen = false,
                            Remitente = mailDataPersonalizado.Sender,
                            Cc = mailDataPersonalizado.Cc,
                            Bcc = mailDataPersonalizado.Bcc,
                            Destinatarios = mailDataPersonalizado.Recipient,
                            IdPersonal = oportunidad.IdPersonalAsignado,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "EnvioAutomaticoCondiciones",
                            UsuarioModificacion = "EnvioAutomaticoCondiciones",
                            IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                            Estado = true
                        };

                        _unitOfWork.GmailCorreoRepository.AddSync(gmailCorreo);
                        _unitOfWork.MandrilEnvioCorreoRepository.AddSync(listaMandrilEnvioCorreo);

                        await _unitOfWork.CommitAsync();

                    }
                    catch { }
                    //fin se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda

                    var documentoEnviadoWebPw = new DocumentoEnviadoWebPw()
                    {
                        IdAlumno = oportunidad.IdAlumno.Value,
                        Nombre = "BSG Institute - Condiciones y Características",
                        IdPespecifico = plantillaPwService.DatosOportunidadAlumno().IdPEspecifico,
                        FechaEnvio = DateTime.Now,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = _reprogramacion.Usuario,
                        UsuarioModificacion = _reprogramacion.Usuario,
                        Estado = true
                    };
                    _unitOfWork.DocumentoEnviadoWebPwRepository.AddAsync(documentoEnviadoWebPw);
                    await _unitOfWork.CommitAsync();
                    if (idPlantilla2 != 0)
                    {
                        //reemplazo 2
                        var emailCalculado2 = await plantillaPwService.ReemplazarEtiquetasAsync(idPlantilla2, oportunidad.Id);
                        var mailDataPersonalizado2 = new TMKMailDataDTO
                        {
                            Sender = "matriculas@bsginstitute.com",
                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                            Subject = emailCalculado2.Asunto,
                            Message = emailCalculado2.CuerpoHTML,
                            Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                            Bcc = "ccrispin@bsginstitute.com",
                            AttachedFiles = null
                        };
                        try
                        {
                            var mailService2 = new TMK_MailService();
                            mailService2.SetData(mailDataPersonalizado2);
                            mailService2.SendMessageTask();
                        }
                        catch { }

                        //se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda
                        mailDataPersonalizado2.Sender = personal.Email;
                        mailDataPersonalizado2.Cc = "";
                        try
                        {
                            var mailService_2 = new TMK_MailService();
                            var listaMandrilEnvioCorreo2 = new List<MandrilEnvioCorreo>();
                            mailService_2.SetData(mailDataPersonalizado2);
                            List<TMKMensajeIdDTO> MensajeIdDTO2 = mailService_2.SendMessageTask();

                            foreach (var mensaje in MensajeIdDTO2)
                            {
                                var validarEmail = _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(mensaje.Email);
                                int idAlumno = validarEmail == null ? 0 : validarEmail.Id;
                                var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                                {
                                    IdOportunidad = oportunidad.Id,
                                    IdPersonal = oportunidad.IdPersonalAsignado,
                                    IdAlumno = idAlumno,
                                    IdCentroCosto = oportunidad.IdCentroCosto,
                                    IdMandrilTipoAsignacion = oportunidad.Id == 0 ? 4 : 0, //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                                    EstadoEnvio = 1,
                                    IdMandrilTipoEnvio = 2, //Manual = 2
                                    FechaEnvio = DateTime.Now,
                                    Asunto = emailCalculado2.Asunto,
                                    FkMandril = mensaje.MensajeId,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "EnvioAutomaticoCondiciones",
                                    UsuarioModificacion = "EnvioAutomaticoCondiciones",
                                    EsEnvioMasivo = false
                                };
                                listaMandrilEnvioCorreo2.Add(mandrilEnvioCorreoEntidad);
                            }

                            //Logica Guardar Correo
                            GmailCorreo gmailCorreo2 = new GmailCorreo
                            {
                                IdEtiqueta = 1,//sent:1 , inbox:2
                                Asunto = emailCalculado2.Asunto,
                                Fecha = DateTime.Now,
                                EmailBody = emailCalculado2.CuerpoHTML,
                                Seen = false,
                                Remitente = mailDataPersonalizado2.Sender,
                                Cc = mailDataPersonalizado2.Cc,
                                Bcc = mailDataPersonalizado2.Bcc,
                                Destinatarios = mailDataPersonalizado2.Recipient,
                                IdPersonal = oportunidad.IdPersonalAsignado,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "EnvioAutomaticoCondiciones",
                                UsuarioModificacion = "EnvioAutomaticoCondiciones",
                                IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                                Estado = true
                            };

                            _unitOfWork.GmailCorreoRepository.AddSync(gmailCorreo2);
                            _unitOfWork.MandrilEnvioCorreoRepository.AddSync(listaMandrilEnvioCorreo2);
                            await _unitOfWork.CommitAsync();

                        }
                        catch { }
                        //fin se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda
                    }
                }
            }
            catch { }
        }

        /// Autor: Carlos Crispin Riquelme
        /// Fecha: 22/04/2025
        /// Versión: 2.0
        /// <summary>
        /// Envio de Correos al asignar la oportunidad
        /// </summary>
        /// <param name="oportunidad">Registro de Oportunidad</param>
        /// <returns>String</returns>
        public async Task EnviarPlantillaAsync(List<OportunidadWhatsappEnvioDTO> lista)
        {
            try
            {
                var plantillaPwService = new PlantillaPwService(_unitOfWork);

                foreach (var item in lista)
                {
                    var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(item.IdOportunidad);
                    var datosCompuestos = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(oportunidad.IdPersonalAsignado == null ? 0 : oportunidad.IdPersonalAsignado.Value);

                    IntDTO idCodigoPais = await _unitOfWork.AlumnoRepository.ObtenerIdPaisPorIdAlumnoAsync(oportunidad.IdAlumno.Value);
                    var idPlantilla = 0;
                    idPlantilla = 827; //Correo Informacion del Curso Completo


                    if (idPlantilla != 0)
                    {
                        await plantillaPwService.CargarDatosOportunidadAlumnoReprogramacion(datosCompuestos.IdPgeneral.Value, oportunidad.IdCentroCosto.Value, oportunidad.Id);

                        List<CorreoNotificacionDTO> comprobantePagoNotificacionCorreos = _unitOfWork.OportunidadRepository.ObtenerCorreoNotificacion().Where(x => x.IdCorreoNotificacionTipo == (int)CorreoNotificacionTipo.CondicionesCaracteristicas).ToList();

                        List<string> correosPersonalizados = new List<string>
                        {
                            plantillaPwService.DatosOportunidadAlumno().OportunidadAlumno.Email1
                        };

                        PlantillaEmailMandrillDTO emailCalculado = await plantillaPwService.ReemplazarEtiquetasAsync(idPlantilla, oportunidad.Id);
                        var mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = "matriculas@bsginstitute.com",
                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                            Subject = emailCalculado.Asunto,
                            Message = emailCalculado.CuerpoHTML,
                            Bcc = "",
                            AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                        };
                        try
                        {
                            var mailService = new TMK_MailService();
                            mailService.SetData(mailDataPersonalizado);
                            mailService.SendMessageTask();
                        }
                        catch { }


                        //se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda
                        mailDataPersonalizado.Sender = personal.Email;
                        mailDataPersonalizado.Cc = "";
                        try
                        {
                            var mailService_1 = new TMK_MailService();
                            var listaMandrilEnvioCorreo = new List<MandrilEnvioCorreo>();
                            mailService_1.SetData(mailDataPersonalizado);
                            List<TMKMensajeIdDTO> MensajeIdDTO = mailService_1.SendMessageTask();

                            foreach (var mensaje in MensajeIdDTO)
                            {
                                var validarEmail = _unitOfWork.AlumnoRepository.ValidarEmail1Alumno(mensaje.Email);
                                int idAlumno = validarEmail == null ? 0 : validarEmail.Id;
                                var mandrilEnvioCorreoEntidad = new MandrilEnvioCorreo
                                {
                                    IdOportunidad = oportunidad.Id,
                                    IdPersonal = oportunidad.IdPersonalAsignado,
                                    IdAlumno = idAlumno,
                                    IdCentroCosto = oportunidad.IdCentroCosto,
                                    IdMandrilTipoAsignacion = oportunidad.Id == 0 ? 4 : 0, //Si la oportunidad es null significa que viene desde la bandeja de entrada de la agenda
                                    EstadoEnvio = 1,
                                    IdMandrilTipoEnvio = 2, //Manual = 2
                                    FechaEnvio = DateTime.Now,
                                    Asunto = emailCalculado.Asunto,
                                    FkMandril = mensaje.MensajeId,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "EnvioAutomaticoCondiciones",
                                    UsuarioModificacion = "EnvioAutomaticoCondiciones",
                                    EsEnvioMasivo = false
                                };
                                listaMandrilEnvioCorreo.Add(mandrilEnvioCorreoEntidad);
                            }

                            //Logica Guardar Correo
                            GmailCorreo gmailCorreo = new GmailCorreo
                            {
                                IdEtiqueta = 1,//sent:1 , inbox:2
                                Asunto = emailCalculado.Asunto,
                                Fecha = DateTime.Now,
                                EmailBody = emailCalculado.CuerpoHTML,
                                Seen = false,
                                Remitente = mailDataPersonalizado.Sender,
                                Cc = mailDataPersonalizado.Cc,
                                Bcc = mailDataPersonalizado.Bcc,
                                Destinatarios = mailDataPersonalizado.Recipient,
                                IdPersonal = oportunidad.IdPersonalAsignado,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "EnvioAutomaticoCondiciones",
                                UsuarioModificacion = "EnvioAutomaticoCondiciones",
                                IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                                Estado = true
                            };

                            _unitOfWork.GmailCorreoRepository.AddSync(gmailCorreo);
                            _unitOfWork.MandrilEnvioCorreoRepository.AddSync(listaMandrilEnvioCorreo);

                            await _unitOfWork.CommitAsync();

                        }
                        catch { }
                        //fin se envia nuevamente al correo del alumno desde el correo del asesor para que se muestre en correos enviados de la agenda
                    }
                }
            }
            catch { }
        }

        public async Task<(CompuestoActividadEjecutadaDTO realizada, int idOportunidad)> CerrarActividadAsync(CerrarActividadDTO jsonDTO)
        {
            try
            {
                // Desactivado de redireccion
                if (jsonDTO.ActividadAntigua.IdOportunidad != 0)
                {
                    try
                    {
                        await _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnteriorAsync(jsonDTO.ActividadAntigua.IdOportunidad);
                    }
                    catch (Exception e)
                    {
                    }
                    var validacion = _unitOfWork.OportunidadRepository.ValidarFaseOportunidad(jsonDTO.ActividadAntigua.IdOportunidad, jsonDTO.DatosOportunidad.IdFaseOportunidad.Value, jsonDTO.ActividadAntigua.Id);
                    if (!validacion.Existe)
                    {
                        throw new ConflictException($"La Actividad ya fue trabajada: IdActividad {jsonDTO.ActividadAntigua.Id}, IdOportunidad: {jsonDTO.ActividadAntigua.Id} Fase Actual: {validacion.CodigoFaseOportunidad}");
                    }
                }
                //new ValorEstatico(_unitOfWork);
                _reprogramacion = new ReprogramacionDTO();
                _reprogramacion.Oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync(jsonDTO.ActividadAntigua.IdOportunidad);
                _reprogramacion.Usuario = jsonDTO.Usuario;

                _reprogramacion.ActividadTemp = new ActividadDetalle()
                {
                    Id = jsonDTO.ActividadAntigua.Id,
                    Comentario = jsonDTO.ActividadAntigua.Comentario,
                    IdAlumno = jsonDTO.ActividadAntigua.IdAlumno,
                    IdOportunidad = jsonDTO.ActividadAntigua.IdOportunidad,
                    IdOcurrenciaAlterno = jsonDTO.ActividadAntigua.IdOcurrencia,
                    IdOcurrenciaActividadAlterno = jsonDTO.ActividadAntigua.IdOcurrenciaActividad,
                };

                await CargarOportunidadCompetidorReprogramacionAsync(jsonDTO.OportunidadCompetidor, jsonDTO.ListaCompetidor, jsonDTO.Usuario);

                if (jsonDTO.CalidadProcesamientoAlterno != null)
                {
                    var calidadPA = jsonDTO.CalidadProcesamientoAlterno;
                    _reprogramacion.CalidadProcesamientoAlterno = new CalidadProcesamientoAlterno()
                    {
                        IdOportunidad = calidadPA.IdOportunidad,
                        PerfilCamposLlenos = calidadPA.PerfilCamposLlenos,
                        PerfilCamposTotal = calidadPA.PerfilCamposTotal,
                        TieneDni = calidadPA.TieneDni,
                        SentinelVerificado = calidadPA.SentinelVerificado,
                        PgeneralMotivacionValidado = calidadPA.PgeneralMotivacionValidado,
                        PgeneralMotivacionTotal = calidadPA.PgeneralMotivacionTotal,
                        PublicoObjetivoValidado = calidadPA.PublicoObjetivoValidado,
                        PublicoObjetivoTotal = calidadPA.PublicoObjetivoTotal,
                        PrerequisitoProgramaValidado = calidadPA.PrerequisitoProgramaValidado,
                        PrerequisitoProgramaTotal = calidadPA.PrerequisitoProgramaTotal,
                        RequisitoCertificacionValidado = calidadPA.RequisitoCertificacionValidado,
                        RequisitoCertificacionTotal = calidadPA.RequisitoCertificacionTotal,
                        BeneficiosValidados = calidadPA.BeneficiosValidados,
                        BeneficiosTotales = calidadPA.BeneficiosTotales,
                        InicioProgramaVerificado = calidadPA.InicioProgramaVerificado,
                        CompetidoresVerificacion = calidadPA.CompetidoresVerificacion,
                        CantidadCompetidores = calidadPA.CantidadCompetidores,
                        ProblemaSeleccionados = calidadPA.ProblemaSeleccionados,
                        ProblemaSolucionados = calidadPA.ProblemaSolucionados,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = jsonDTO.Usuario,
                        UsuarioModificacion = jsonDTO.Usuario,
                        Estado = true,
                    };
                }

                if (_reprogramacion.Oportunidad != null && _reprogramacion.Oportunidad.Id != 0)
                {
                    _reprogramacion.ActividadTrabajada = await _unitOfWork.ActividadDetalleRepository.ObtenerPorIdAsync(jsonDTO.ActividadAntigua.Id);
                    await FinalizarActividadAlternoAsync(false, jsonDTO.DatosOportunidad, jsonDTO.ActividadAntigua.IdOcurrenciaActividad);
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            var faseCierreOportunidad = new List<int>()
                            {
                                ValorEstatico.IdFaseOportunidadD,
                                ValorEstatico.IdFaseOportunidadRN4,
                                ValorEstatico.IdFaseOportunidadNI,
                                ValorEstatico.IdFaseOportunidadIS,
                                ValorEstatico.IdFaseOportunidadRN3,
                                ValorEstatico.IdFaseOportunidadRN2,
                                ValorEstatico.IdFaseOportunidadRN2A
                            };
                            if (faseCierreOportunidad.Contains(_reprogramacion.Oportunidad.IdFaseOportunidad))
                            {
                                int estadoISoM;
                                if (_reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                                    estadoISoM = 0;
                                else
                                    estadoISoM = 1;

                                await _unitOfWork.OportunidadMaximaPorCategoriaRepository.ActualizarDatosEstaticosPantalla2Async(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault(), _reprogramacion.Oportunidad.IdCategoriaOrigen.GetValueOrDefault(), estadoISoM);
                            }
                            _unitOfWork.OportunidadRepository.Update(CargarOportunidad(_reprogramacion));
                            await _unitOfWork.CommitAsync();
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>
                            {
                                "sistemas@bsginstitute.com"
                            };

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + jsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + jsonDTO.Usuario == null ? "" : jsonDTO.Usuario + "<br/>" + jsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;
                            try
                            {
                                TMK_MailService Mailservice = new TMK_MailService();
                                Mailservice.SetData(mailData);
                                Mailservice.SendMessageTask();
                            }
                            catch { }
                            throw;
                        }
                    }
                    var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                    var realizadas = await actividadDetalleService.ObtenerAgendaRealizadaRegistroTiempoRealAsync(_reprogramacion.ActividadTrabajada.Id);
                    return (realizadas, _reprogramacion.Oportunidad.Id);
                }
                else
                {
                    throw new ArgumentException("No existe la Oportunidad");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(CompuestoActividadEjecutadaDTO realizada, int idOportunidad)> FinalizarActividadCrearOportunidadAlternoAsync(VentaCruzadaDTO ventaCruzada)
        {
            try
            {
                ReprogramacionDTO oportunidadReprogramacionNueva = new ReprogramacionDTO();
                oportunidadReprogramacionNueva.Oportunidad = new Oportunidad();
                _reprogramacion = new ReprogramacionDTO();
                _reprogramacion.Oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync(ventaCruzada.ActividadAntigua.IdOportunidad);
                _reprogramacion.Usuario = ventaCruzada.Usuario;
                #region Desactivado de redireccion anterior
                try
                {
                    await _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnteriorAsync(_reprogramacion.Oportunidad.Id);
                }
                catch (Exception)
                {
                }
                #endregion

                //new ValorEstatico(_unitOfWork);
                _reprogramacion.Oportunidad.IdPersonalAsignado = ventaCruzada.DatosOportunidad.IdPersonalAsignado;

                _reprogramacion.Oportunidad.IdAlumno = ventaCruzada.DatosOportunidad.IdAlumno;
                if (ventaCruzada.DatosOportunidad.UltimaFechaProgramada != null)
                {
                    _reprogramacion.Oportunidad.UltimaFechaProgramada = DateTime.Parse(ventaCruzada.DatosOportunidad.UltimaFechaProgramada);
                }
                _reprogramacion.Oportunidad.UltimoComentario = ventaCruzada.DatosOportunidad.UltimoComentario;
                _reprogramacion.Oportunidad.IdSubCategoriaDato = ventaCruzada.DatosOportunidad.IdSubCategoriaDato;
                _reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Programada;
                int faseNueva = ventaCruzada.DatosOportunidad.IdFaseOportunidad.Value;
                ventaCruzada.DatosOportunidad.IdFaseOportunidad = _reprogramacion.Oportunidad.IdFaseOportunidad;
                // Finalizar Actividad
                ActividadDetalle actividadAntigua = new ActividadDetalle()
                {
                    Id = ventaCruzada.ActividadAntigua.Id,
                    Comentario = ventaCruzada.ActividadAntigua.Comentario,
                    IdAlumno = ventaCruzada.ActividadAntigua.IdAlumno,
                    IdOportunidad = ventaCruzada.ActividadAntigua.IdOportunidad,
                    IdCentralLlamada = ventaCruzada.ActividadAntigua.IdCentralLlamada,
                    IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona,
                    IdOcurrenciaAlterno = ventaCruzada.ActividadAntigua.IdOcurrencia,
                    IdOcurrenciaActividadAlterno = ventaCruzada.ActividadAntigua.IdOcurrenciaActividad,
                };

                _reprogramacion.ActividadTemp = actividadAntigua;
                await CargarOportunidadCompetidorReprogramacionAsync(ventaCruzada.OportunidadCompetidor, ventaCruzada.ListaCompetidor, ventaCruzada.Usuario);

                if (ventaCruzada.CalidadProcesamientoAlterno != null)
                {
                    var calidadPA = ventaCruzada.CalidadProcesamientoAlterno;
                    _reprogramacion.CalidadProcesamientoAlterno = new CalidadProcesamientoAlterno()
                    {
                        IdOportunidad = calidadPA.IdOportunidad,
                        PerfilCamposLlenos = calidadPA.PerfilCamposLlenos,
                        PerfilCamposTotal = calidadPA.PerfilCamposTotal,
                        TieneDni = calidadPA.TieneDni,
                        SentinelVerificado = calidadPA.SentinelVerificado,
                        PgeneralMotivacionValidado = calidadPA.PgeneralMotivacionValidado,
                        PgeneralMotivacionTotal = calidadPA.PgeneralMotivacionTotal,
                        PublicoObjetivoValidado = calidadPA.PublicoObjetivoValidado,
                        PublicoObjetivoTotal = calidadPA.PublicoObjetivoTotal,
                        PrerequisitoProgramaValidado = calidadPA.PrerequisitoProgramaValidado,
                        PrerequisitoProgramaTotal = calidadPA.PrerequisitoProgramaTotal,
                        RequisitoCertificacionValidado = calidadPA.RequisitoCertificacionValidado,
                        RequisitoCertificacionTotal = calidadPA.RequisitoCertificacionTotal,
                        BeneficiosValidados = calidadPA.BeneficiosValidados,
                        BeneficiosTotales = calidadPA.BeneficiosTotales,
                        InicioProgramaVerificado = calidadPA.InicioProgramaVerificado,
                        CompetidoresVerificacion = calidadPA.CompetidoresVerificacion,
                        CantidadCompetidores = calidadPA.CantidadCompetidores,
                        ProblemaSeleccionados = calidadPA.ProblemaSeleccionados,
                        ProblemaSolucionados = calidadPA.ProblemaSolucionados,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = ventaCruzada.Usuario,
                        UsuarioModificacion = ventaCruzada.Usuario,
                        Estado = true,
                    };
                }

                if (_reprogramacion.Oportunidad.Id != 0)
                {
                    _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(ventaCruzada.ActividadAntigua.Id);
                    await FinalizarActividadAlternoAsync(false, ventaCruzada.DatosOportunidad, ventaCruzada.ActividadAntigua.IdOcurrenciaActividad);
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            if (_reprogramacion.PreCalculadaCambioFase != null)
                            {
                                _reprogramacion.PreCalculadaCambioFase.Contador = _unitOfWork.PreCalculadaCambioFaseRepository.ExistePreCalculadaCambioFase(_reprogramacion.PreCalculadaCambioFase);
                                _unitOfWork.PreCalculadaCambioFaseRepository.AddAsync(_reprogramacion.PreCalculadaCambioFase);
                                await _unitOfWork.CommitAsync();
                            }
                            var faseCierreOportunidad = new List<int>()
                            {
                                ValorEstatico.IdFaseOportunidadD,
                                ValorEstatico.IdFaseOportunidadRN4,
                                ValorEstatico.IdFaseOportunidadNI,
                                ValorEstatico.IdFaseOportunidadIS,
                                ValorEstatico.IdFaseOportunidadRN3,
                                ValorEstatico.IdFaseOportunidadRN2,
                                ValorEstatico.IdFaseOportunidadRN2A
                            };
                            if (faseCierreOportunidad.Contains(_reprogramacion.Oportunidad.IdFaseOportunidad))
                            {
                                int estadoISoM;
                                if (_reprogramacion.Oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                                    estadoISoM = 0;
                                else
                                    estadoISoM = 1;
                                await _unitOfWork.OportunidadMaximaPorCategoriaRepository.ActualizarDatosEstaticosPantalla2Async(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault(), _reprogramacion.Oportunidad.IdCategoriaOrigen.GetValueOrDefault(), estadoISoM);
                            }
                            //ACTUALIZA LA OPORTUNIDAD

                            //OportunidadReprogramacion.IdFaseOportunidad = OportunidadReprogramacion.OportunidadLogNueva.IdFaseOportunidad.Value;
                            _unitOfWork.OportunidadRepository.Update(CargarOportunidad(_reprogramacion));
                            await _unitOfWork.CommitAsync();

                            //Logica Nueva Oportunidad Actividad
                            oportunidadReprogramacionNueva.Oportunidad.IdFaseOportunidad = faseNueva;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAsignado = ventaCruzada.DatosOportunidad.IdPersonalAsignado;
                            oportunidadReprogramacionNueva.Oportunidad.IdTipoDato = ventaCruzada.DatosOportunidad.IdTipoDato.Value;
                            oportunidadReprogramacionNueva.Oportunidad.IdOrigen = ventaCruzada.DatosOportunidad.IdOrigen;
                            oportunidadReprogramacionNueva.Oportunidad.IdAlumno = ventaCruzada.DatosOportunidad.IdAlumno;
                            if (ventaCruzada.DatosOportunidad.UltimaFechaProgramada != null)
                            {
                                oportunidadReprogramacionNueva.Oportunidad.UltimaFechaProgramada = DateTime.Parse(ventaCruzada.DatosOportunidad.UltimaFechaProgramada);
                            }
                            oportunidadReprogramacionNueva.Oportunidad.UltimoComentario = ventaCruzada.DatosOportunidad.UltimoComentario;
                            oportunidadReprogramacionNueva.IdTipoInteraccion = ventaCruzada.DatosOportunidad.IdTipoInteraccion.GetValueOrDefault();
                            oportunidadReprogramacionNueva.Oportunidad.IdSubCategoriaDato = ventaCruzada.DatosOportunidad.IdSubCategoriaDato;
                            oportunidadReprogramacionNueva.Oportunidad.IdCentroCosto = ventaCruzada.DatosOportunidad.IdCentroCosto;
                            oportunidadReprogramacionNueva.Oportunidad.FechaRegistroCampania = _reprogramacion.Oportunidad.FechaRegistroCampania;
                            oportunidadReprogramacionNueva.Oportunidad.Estado = true;
                            oportunidadReprogramacionNueva.Oportunidad.FechaCreacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.FechaModificacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioCreacion = ventaCruzada.Usuario;
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioModificacion = ventaCruzada.Usuario;
                            oportunidadReprogramacionNueva.Oportunidad.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAreaTrabajo = _reprogramacion.Oportunidad.IdPersonalAreaTrabajo;

                            //SE CREA UNA NUEVA OPORTUNIDAD
                            CrearOportunidad(ref oportunidadReprogramacionNueva, false, TipoPersona.Alumno);

                            IAgendaService agendaService = new AgendaService(_unitOfWork);

                            //// Lo comento porque se ve que en la funcion Crear Oportunidad ya envia el correo
                            //// 1967 Correo Informacion del Curso Completo
                            //agendaService.EnviarCorreoOportunidadAutomatico(oportunidadReprogramacionNueva.Oportunidad!.Id, 1967, "Automatico1967");

                            _unitOfWork.ProcedenciaVentaCruzadumRepository.InsertarProcedenciaVentaCruzada(_reprogramacion.Oportunidad.Id, oportunidadReprogramacionNueva.Oportunidad.Id);
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
                            mailData.Subject = "Error VentaCruzada Transaction";
                            mailData.Message = "IdOportunidad: " + ventaCruzada.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + ventaCruzada.Usuario == null ? "" : ventaCruzada.Usuario + "<br/>" + ventaCruzada.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
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

                    ///01/02/2021
                    ///Calculo nuevo modelo predictivo
                    ///Carlos Crispin Riquelme
                    try
                    {
                        var nuevaProbabilidad = _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivo(oportunidadReprogramacionNueva.Oportunidad.Id);
                    }
                    catch (Exception e)
                    {
                    }
                }
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var realizadas = actividadDetalleService.ObtenerAgendaRealizadaRegistroTiempoReal(ventaCruzada.ActividadAntigua.Id);
                return (realizadas, ventaCruzada.ActividadAntigua.IdOportunidad);
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>()
                {
                    "sistemas@bsginstitute.com"
                };

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error VentaCruzada General";
                mailData.Message = "IdOportunidad: " + ventaCruzada.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + ventaCruzada.Usuario == null ? "" : ventaCruzada.Usuario + "<br/>" + ventaCruzada.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Llena los datos necesarios de la oportunidadNuevaEntidad y generar sus hijos
        /// </summary>
        /// <param name="oportunidadNuevaEntidad">Entidad oportunidadNuevaEntidad</param>
        private void LlenarOportunidadHijos(ref ReprogramacionDTO reprogramacion)
        {
            if (reprogramacion.Oportunidad == null)
                throw new BadRequestException("Oportunidad Vacia");

            reprogramacion.Oportunidad.IdPagina = 1;
            if (reprogramacion.Oportunidad.IdOrigen == 0 && reprogramacion.Oportunidad.IdOrigen != null)
                throw new BadRequestException("La Oportunidad no tiene Origen.");

            var categoriaOrigen = _unitOfWork.OrigenRepository.ObtenerIdCategoriaOrigenPorOrigen(reprogramacion.Oportunidad.IdOrigen.GetValueOrDefault());

            if (categoriaOrigen.IdCategoriaOrigen == 0)
                throw new BadRequestException("No se encontro origen no se puede crear categoria.");

            if (reprogramacion.Oportunidad.IdCategoriaOrigen == null || reprogramacion.Oportunidad.IdCategoriaOrigen == 0)
                reprogramacion.Oportunidad.IdCategoriaOrigen = categoriaOrigen.IdCategoriaOrigen;

            if (reprogramacion.Oportunidad.IdSubCategoriaDato == null || reprogramacion.Oportunidad.IdSubCategoriaDato == 0)
            {
                if (reprogramacion.IdTipoInteraccion == null)
                    reprogramacion.IdTipoInteraccion = 18;

                var categoriadato = _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenSubCategoriaDato(reprogramacion.Oportunidad.IdCategoriaOrigen.Value, reprogramacion.IdTipoInteraccion.Value);
                if (categoriadato != null)
                    reprogramacion.Oportunidad.IdSubCategoriaDato = categoriadato.IdSubCategoriaDato;
            }
            if (!string.IsNullOrEmpty(reprogramacion.Oportunidad.CodMailing))
            {
                var campaniaMailing = _unitOfWork.CampaniaMailingDetalleRepository.ObtenerIdCampaniaMailing(reprogramacion.Oportunidad.CodMailing);
                if (campaniaMailing != null)
                {
                    reprogramacion.Oportunidad.IdConjuntoAnuncio = campaniaMailing.Valor;
                }
            }
            reprogramacion.Oportunidad.IdFaseOportunidadInicial = reprogramacion.Oportunidad.IdFaseOportunidad;
            reprogramacion.Oportunidad.IdFaseOportunidadMaxima = reprogramacion.Oportunidad.IdFaseOportunidad;
            if (reprogramacion.Oportunidad.UltimaFechaProgramada == null
                || reprogramacion.Oportunidad.UltimaFechaProgramada.Equals("              ")
                || reprogramacion.Oportunidad.UltimaFechaProgramada.Equals("00000000000000"))
            {
                reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.NoProgramada;
            }
            else
                reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.Programada;

            reprogramacion.OportunidadLogTemp = new OportunidadLog()
            {
                IdPersonalAsignado = reprogramacion.Oportunidad.IdPersonalAsignado,
                IdAsesorAnt = reprogramacion.Oportunidad.IdPersonalAsignado,
                IdContacto = reprogramacion.Oportunidad.IdAlumno,
                IdFaseOportunidad = reprogramacion.Oportunidad.IdFaseOportunidad,
                IdFaseOportunidadAnt = reprogramacion.Oportunidad.IdFaseOportunidad,
                IdOportunidad = reprogramacion.Oportunidad.Id,
                IdCentroCosto = reprogramacion.Oportunidad.IdCentroCosto,
                IdCentroCostoAnt = reprogramacion.Oportunidad.IdCentroCosto,
                IdOrigen = reprogramacion.Oportunidad.IdOrigen,
                IdTipoDato = reprogramacion.Oportunidad.IdTipoDato,
                FechaLog = DateTime.Now,
                FechaFinLog = DateTime.Now,
                FechaCambioFase = DateTime.Now,
                FechaCambioFaseAnt = DateTime.Now,
                CambioFase = true,
                CambioFaseIs = false,
                Comentario = reprogramacion.Oportunidad.UltimoComentario,
                IdConjuntoAnuncio = reprogramacion.Oportunidad.IdConjuntoAnuncio,
                FechaRegistroCampania = reprogramacion.Oportunidad.FechaRegistroCampania,
                CicloRn2 = 1,
                CambioFaseAsesor = 1,
                FechaCambioAsesor = DateTime.Now,
                FechaCambioAsesorAnt = DateTime.Now,
                IdCategoriaOrigen = reprogramacion.Oportunidad.IdCategoriaOrigen,
                IdSubCategoriaDato = reprogramacion.Oportunidad.IdSubCategoriaDato,
                UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion,
                UsuarioModificacion = reprogramacion.Oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona,
                IdPersonalAreaTrabajo = reprogramacion.Oportunidad.IdPersonalAreaTrabajo
            };

            var idActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(reprogramacion.Oportunidad.IdFaseOportunidad, reprogramacion.Oportunidad.IdTipoDato, reprogramacion.Oportunidad.IdPersonalAreaTrabajo, reprogramacion.Oportunidad.IdActividadCabeceraUltima);

            ActividadDetalle actividadDetalle = new ActividadDetalle()
            {
                FechaProgramada = reprogramacion.Oportunidad.UltimaFechaProgramada,
                Actor = "A",
                Comentario = "Sin Comentario",
                IdActividadCabecera = idActividadCabecera,
                IdAlumno = reprogramacion.Oportunidad.IdAlumno,
                IdEstadoActividadDetalle = EstadoActividadDetalle.NoEjecutado,
                IdOportunidad = 1,//oportunidad.Id
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion,
                UsuarioModificacion = reprogramacion.Oportunidad.UsuarioCreacion,
                Estado = true,
                IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona
            };
            if (reprogramacion.Oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookPreLanC2FormularioPropio)
            {
                actividadDetalle.IdActividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProgPrelan;
            }
            var actividadNueva = _unitOfWork.ActividadDetalleRepository.Add(actividadDetalle);
            _unitOfWork.Commit();
            reprogramacion.Oportunidad.IdActividadDetalleUltima = actividadNueva.Id;

            reprogramacion.ActividadNueva = _mapper.Map<ActividadDetalle>(actividadNueva);
            reprogramacion.ActividadTemp = null;

            reprogramacion.Oportunidad.UltimoComentario = reprogramacion.Oportunidad.UltimoComentario ?? reprogramacion.ActividadNueva.Comentario;
            reprogramacion.Oportunidad.IdActividadCabeceraUltima = reprogramacion.ActividadNueva.IdActividadCabecera ?? 0;
            reprogramacion.Oportunidad.IdActividadDetalleUltima = reprogramacion.ActividadNueva.Id;
            reprogramacion.Oportunidad.UltimaFechaProgramada = reprogramacion.ActividadNueva.FechaProgramada;
            reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = reprogramacion.ActividadNueva.IdEstadoActividadDetalle;
            //Registramos la asignacion de oportunidad
            AsignacionOportunidad? asignacionOportunidad = null;
            if (_unitOfWork.AsignacionOportunidadRepository.Exist(reprogramacion.Oportunidad.Id))
                asignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(reprogramacion.Oportunidad.Id);
            if (asignacionOportunidad == null)
            {
                asignacionOportunidad = new AsignacionOportunidad
                {
                    FechaAsignacion = DateTime.Now,
                    IdAlumno = reprogramacion.Oportunidad.IdAlumno,
                    IdPersonal = reprogramacion.Oportunidad.IdPersonalAsignado,
                    IdCentroCosto = reprogramacion.Oportunidad.IdCentroCosto is null ? default : reprogramacion.Oportunidad.IdCentroCosto.Value,
                    IdOportunidad = reprogramacion.Oportunidad.Id,
                    IdTipoDato = reprogramacion.Oportunidad.IdTipoDato,
                    IdFaseOportunidad = reprogramacion.Oportunidad.IdFaseOportunidad,
                    UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion,
                    UsuarioModificacion = reprogramacion.Oportunidad.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona,
                };
            }
            else
            {
                asignacionOportunidad.IdPersonal = reprogramacion.Oportunidad.IdPersonalAsignado == 0 ? asignacionOportunidad.IdPersonal : reprogramacion.Oportunidad.IdPersonalAsignado;
                if (reprogramacion.Oportunidad.IdCentroCosto != 0 && reprogramacion.Oportunidad.IdCentroCosto != null)
                    asignacionOportunidad.IdCentroCosto = reprogramacion.Oportunidad.IdCentroCosto;
                if (reprogramacion.Oportunidad.IdAlumno != 0 && reprogramacion.Oportunidad.IdAlumno != null)
                    asignacionOportunidad.IdAlumno = reprogramacion.Oportunidad.IdAlumno;
                asignacionOportunidad.FechaAsignacion = DateTime.Now;
                asignacionOportunidad.FechaModificacion = DateTime.Now;
                asignacionOportunidad.UsuarioModificacion = reprogramacion.Oportunidad.UsuarioCreacion;
                asignacionOportunidad.UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion;
                asignacionOportunidad.Estado = true;
                asignacionOportunidad.IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona;
            }

            AsignacionOportunidadLog asignacionOportunidadLog = new AsignacionOportunidadLog
            {
                FechaLog = DateTime.Now,
                IdPersonalAnterior = asignacionOportunidad.IdPersonal,
                IdAsignacionOportunidad = asignacionOportunidad.Id,
                IdCentroCostoAnt = asignacionOportunidad.IdCentroCosto,
                IdOportunidad = asignacionOportunidad.Id,
                IdTipoDato = asignacionOportunidad.IdTipoDato,
                IdFaseOportunidad = asignacionOportunidad.IdFaseOportunidad,
                IdAlumno = asignacionOportunidad.IdAlumno,
                IdPersonal = asignacionOportunidad.IdPersonal,
                IdCentroCosto = asignacionOportunidad.IdCentroCosto,
                UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion,
                UsuarioModificacion = reprogramacion.Oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona
            };
            asignacionOportunidad.AsignacionOportunidadLogs = new List<AsignacionOportunidadLog>()
            {
                asignacionOportunidadLog
            };
            reprogramacion.AsignacionOportunidad = asignacionOportunidad;
        }
        public void CrearOportunidad(ref ReprogramacionDTO reprogramacion, bool flagVentaCruzada, TipoPersona tipoPersona)
        {
            // Llenamos valores oportunidad/hijos
            LlenarOportunidadHijos(ref reprogramacion);
            if (reprogramacion.Oportunidad != null)
            {
                if (reprogramacion.ActividadNueva == null)
                    throw new BadRequestException("Activida Nueva Vacia");
                ActividadDetalle actividadNuevaTemp = reprogramacion.ActividadNueva;
                reprogramacion.ActividadNueva = null;
                if (reprogramacion.AsignacionOportunidad == null)
                    throw new BadRequestException("Asignacion Oportunidad Vacia");
                AsignacionOportunidad asignacionOportunidadTemp = reprogramacion.AsignacionOportunidad;
                asignacionOportunidadTemp.IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona;
                if (asignacionOportunidadTemp.AsignacionOportunidadLogs == null)
                    throw new BadRequestException("Asignacion Oportunidad Logs Vacia");
                asignacionOportunidadTemp.AsignacionOportunidadLogs.FirstOrDefault().IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona;

                reprogramacion.AsignacionOportunidad = null;
                var oportunidadCreada = _unitOfWork.OportunidadRepository.Add(CargarOportunidad(reprogramacion));
                _unitOfWork.Commit();
                reprogramacion.Oportunidad = _mapper.Map<Oportunidad>(oportunidadCreada);

                asignacionOportunidadTemp.IdOportunidad = reprogramacion.Oportunidad.Id;
                asignacionOportunidadTemp.AsignacionOportunidadLogs.FirstOrDefault().IdOportunidad = reprogramacion.Oportunidad.Id;

                if (asignacionOportunidadTemp.Id != 0)
                    _unitOfWork.AsignacionOportunidadRepository.Update(asignacionOportunidadTemp);
                else
                    _unitOfWork.AsignacionOportunidadRepository.Add(asignacionOportunidadTemp);
                _unitOfWork.Commit();

                bool flagValidaActividadDetalle = false;
                int nroIntentos = 0;

                while (!flagValidaActividadDetalle && nroIntentos < 5)
                {
                    try
                    {
                        _unitOfWork.DetachAll();
                        var actividadDetalle = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(actividadNuevaTemp.Id);
                        actividadDetalle.IdOportunidad = reprogramacion.Oportunidad.Id;
                        actividadDetalle.IdClasificacionPersona = reprogramacion.Oportunidad.IdClasificacionPersona;
                        _unitOfWork.ActividadDetalleRepository.Update(actividadDetalle);
                        _unitOfWork.Commit();
                        //_unitOfWork.ActividadDetalleRepository.ActualizarOportunidadYClasificacionPersona(reprogramacion.Oportunidad.Id, reprogramacion.Oportunidad.IdClasificacionPersona.Value, actividadNuevaTemp.Id);
                        flagValidaActividadDetalle = true;
                    }
                    catch (Exception ex)
                    {
                        nroIntentos++;
                        if (nroIntentos == 4)
                        {
                            _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarActividadDetalle", Parametros = $"idActividadDetalle={actividadNuevaTemp.Id}&idOportunidad={reprogramacion.Oportunidad.Id}&idClasificacionPersona={reprogramacion.Oportunidad.IdClasificacionPersona}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                            throw new Exception(ex.Message);
                        }
                        Thread.Sleep(2000);
                    }
                }
                IModeloDataMiningService modeloDataMiningService = new ModeloDataMiningService(_unitOfWork);
                var cantidad = modeloDataMiningService.ListaPorOportunidad(reprogramacion.Oportunidad.Id);
                ModeloDataMining modeloDataMining;

                if (tipoPersona == TipoPersona.Alumno)
                {
                    if (cantidad != null && cantidad.Count() > 0)
                    {
                        modeloDataMining = modeloDataMiningService.ObtenerPorId(cantidad.FirstOrDefault().Id);
                        modeloDataMiningService.ObtenerProbabilidad(reprogramacion.Oportunidad.Id, ref modeloDataMining);
                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new BadRequestException("No se pudo crear Probabilidad Inicial con OportunidadId: " + reprogramacion.Oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = reprogramacion.Oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = reprogramacion.Oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = reprogramacion.Oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = reprogramacion.Oportunidad.IdAlumno;
                        modeloDataMining.UsuarioModificacion = reprogramacion.Oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.Estado = true;
                        modeloDataMiningService.Update(modeloDataMining);
                        reprogramacion.ModeloDataMining = modeloDataMining;
                    }
                    else
                    {
                        modeloDataMining = new ModeloDataMining();
                        modeloDataMining.IdOportunidad = reprogramacion.Oportunidad.Id;
                        modeloDataMiningService.ObtenerProbabilidad(reprogramacion.Oportunidad.Id, ref modeloDataMining);

                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new BadRequestException("No se pudo crear Probabilidad Inicial con OportunidadId: " + reprogramacion.Oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = reprogramacion.Oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = reprogramacion.Oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = reprogramacion.Oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = reprogramacion.Oportunidad.IdAlumno;
                        modeloDataMining.UsuarioCreacion = reprogramacion.Oportunidad.UsuarioCreacion;
                        modeloDataMining.UsuarioModificacion = reprogramacion.Oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaCreacion = DateTime.Now;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.FechaCreacionContacto = DateTime.Now;
                        modeloDataMining.FechaCreacionOportunidad = reprogramacion.Oportunidad.FechaCreacion;
                        modeloDataMining.Estado = true;
                        var repuestaDataMining = modeloDataMiningService.Add(modeloDataMining);
                        modeloDataMining.Id = repuestaDataMining.Id;
                        reprogramacion.ModeloDataMining = modeloDataMining;
                    }

                    //decimal? puntoCorte = null;
                    //modeloDataMining.PuntoCorte
                    //oportunidad.ValorProbabilidad = modeloDataMining.ProbabilidadActual < puntoCorte ? -1 : Convert.ToDecimal(modeloDataMining.IdProbabilidadRegistroPwActual);
                }
            }
            //validamos venta cruzada
            if (FlagValidacionVentaCruzada)
            {
                OportunidadVentaCruzada(ref reprogramacion, flagVentaCruzada, tipoPersona);
            }
            IAgendaService agendaService = new AgendaService(_unitOfWork);

            // 1967 Correo Informacion del Curso Completo
            agendaService.EnviarCorreoOportunidadAutomatico(reprogramacion.Oportunidad!.Id, 1967, "Automatico1967");
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Hace la logica de validaciones por venta cruzada, reasignar las oportunidadNuevaEntidades medias y altas    
        /// </summary>
        /// <param name="oportunidadNuevaEntidad"> Confirmación para mantener el estado de oportunidadNuevaEntidad </param>
        /// <param name="flagVentaCruzada"> Datos de Oportunidad </param>
        private void OportunidadVentaCruzada(ref ReprogramacionDTO reprogramacion, bool flagVentaCruzada, Enums.TipoPersona tipoPersona)
        {
            if (tipoPersona == TipoPersona.Alumno)
            {
                var idAsesorVentaCruzada = ObtenerAsesorVentaCruzada(reprogramacion.Oportunidad.IdAlumno.GetValueOrDefault());
                if (reprogramacion.Oportunidad.IdPersonalAsignado == ValorEstatico.IdPersonalAsignacionAutomatica
                    || reprogramacion.Oportunidad.IdPersonalAsignado == idAsesorVentaCruzada
                    || flagVentaCruzada == true)
                {
                    bool validarProbabilidad = _unitOfWork.OportunidadRepository.ValidarProbabilidadVentaCruzada(reprogramacion.ModeloDataMining.IdProbabilidadRegistroPwActual);
                    if (reprogramacion.ModeloDataMining != null && validarProbabilidad)
                    {
                        try
                        {
                            //Si encontramos almenos un programa en lanzamiento por venta cruzada, reasignamos la oportunidad a un asesor que tenga meta en ese programa
                            if (idAsesorVentaCruzada != 0 && idAsesorVentaCruzada != -1)
                            {
                                //NO ENVIA CORREO PORQUE NO HAY OTRAS CON CUAL COMPARAR
                                ActualizarOportunidadVentaCruzada(ref reprogramacion, idAsesorVentaCruzada, "UsuarioVentaCruzada", false, false);
                                _unitOfWork.OportunidadRepository.Update(CargarOportunidad(reprogramacion));
                                _unitOfWork.Commit();
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error al reasignar la oportunidad por por venta cruzada " + e.Message);
                        }
                    }
                }
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualizar la oportunidadNuevaEntidad de la venta cruzada
        /// </summary>
        /// <param name="oportunidadNuevaEntidad"> entidad oportunidadNuevaEntidad </param>
        /// <param name="idAsesorReasignacion"> id del asesor de reasignacion </param>
        /// <param name="usuario"> usuario </param>
        /// <param name="enviaCorreo"> true o false </param>
        /// <param name="permaneceEstado"> true o false </param>
        /// <returns> Vacio </returns>
        private void ActualizarOportunidadVentaCruzada(ref ReprogramacionDTO reprogramacion, int idAsesorReasignacion, string usuario, bool enviaCorreo, bool permaneceEstado)
        {
            try
            {
                //obtenemos los datos de la oportunidadNuevaEntidad
                var datosOportunidadReasignacion = _unitOfWork.OportunidadRepository.ObtenerDatosOportunidadReasignacion(reprogramacion.Oportunidad.Id);
                PersonalMinReasignacionDTO asesorAntiguo = new PersonalMinReasignacionDTO
                {
                    IdAsesor = datosOportunidadReasignacion.IdAsesor,
                    EmailAsesor = datosOportunidadReasignacion.EmailAsesor,
                    EmailJefe = datosOportunidadReasignacion.EmailAsesor,
                    IdJefe = datosOportunidadReasignacion.IdJefe,
                    NombreCompletoAsesor = datosOportunidadReasignacion.NombreCompletoAsesor,
                    NombreCompletoJefe = datosOportunidadReasignacion.NombreCompletoJefe
                };
                FaseOportunidadReasignacionDTO faseOportunidad = new FaseOportunidadReasignacionDTO()
                {
                    Codigo = datosOportunidadReasignacion.CodigoFaseOportunidad
                };
                var datosAsesorNuevo = _unitOfWork.PersonalRepository.ObtenerPersonalReasignacion(idAsesorReasignacion);
                AlumnoReasignacionDTO alumnoReasignacion = new AlumnoReasignacionDTO()
                {
                    Nombre1 = datosOportunidadReasignacion.Nombre1,
                    Nombre2 = datosOportunidadReasignacion.Nombre1,
                    ApellidoPaterno = datosOportunidadReasignacion.ApellidoPaterno,
                    ApellidoMaterno = datosOportunidadReasignacion.ApellidoMaterno
                };
                //fin obtenemos los datos de la oportunidadNuevaEntidad 200418
                using (TransactionScope scope = new TransactionScope())
                {
                    //actualizamos la oportunidadNuevaEntidad
                    ActualizarOportunidadVentaCruzadaAsesor(ref reprogramacion, idAsesorReasignacion, permaneceEstado, usuario);
                    //Actualizamos en nuevo log
                    reprogramacion.OportunidadLogNueva.IdPersonalAsignado = idAsesorReasignacion;
                    reprogramacion.OportunidadLogNueva.FechaLog = DateTime.Now;
                    _unitOfWork.OportunidadRepository.Update(CargarOportunidad(reprogramacion));
                    _unitOfWork.Commit();
                    //Registramos la asignacion con los nuevos datos
                    IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                    oportunidadService.ActualizarAsignacionOportunidad(reprogramacion.Oportunidad.Id, idAsesorReasignacion, reprogramacion.Oportunidad.IdCentroCosto.Value, reprogramacion.Oportunidad.IdAlumno.Value, usuario);
                    //actualizamos la asignacion 
                    scope.Complete();
                }
                try
                {
                    //enviamos correo de la reasignacion 200418
                    if (enviaCorreo == true)
                    {
                        EnvioCorreoReasignacion(asesorAntiguo, datosAsesorNuevo, alumnoReasignacion, faseOportunidad);
                    }
                }
                catch (Exception)
                {
                    //si no se envio el correopar que igual se crre la oportunidadNuevaEntidad
                }
                //fin enviamos correo de la reasignacion 200418
            }
            catch (Exception e)
            {
                throw new BadRequestException("Me cai al actualizar el asesor por venta cruzada con envio correo");
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Envia un correo, cuando se reasigna la oportunidadNuevaEntidad
        /// </summary> 
        /// <param name="PersonalAntiguo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="PersonalNuevo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="Alumno">Objeto de clase AlumnoReasignacionDTO</param>
        /// <param name="FaseOportunidad">Objeto de clase FaseOportunidadReasignacionDTO</param>
        private void EnvioCorreoReasignacion(PersonalMinReasignacionDTO PersonalAntiguo, PersonalMinReasignacionDTO PersonalNuevo, AlumnoReasignacionDTO Alumno, FaseOportunidadReasignacionDTO FaseOportunidad)
        {
            string cuerpoMensaje, asuntoMensaje;
            try
            {
                cuerpoMensaje = "<p>" +
                    "Estimado " + PersonalNuevo.NombreCompletoAsesor + "<br/><br/>" +
                    "Se te ha reasignado el contacto " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno) + " en fase <b>" + FaseOportunidad.Codigo + "</b> que estaba asignado al asesor " + PersonalAntiguo.NombreCompletoAsesor + "<br/>" +
                    "Verifica las llamadas y la informacion previa registradas en el sistema para que puedas continuar de manera adecuada el proceso de venta.<br/><br/>" +
                    "Saludos <br/>" +
                    "Integra Reasignacion" +
                    "</p>";
                asuntoMensaje = "Reasignacion de Oportunidad " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno).ToUpper(CultureInfo.InvariantCulture);
                var servicioMail = new TMK_MailService();
                var data = new TMKMailDataDTO
                {
                    Sender = "reasignacion@bsginstitute.com",
                    Recipient = PersonalNuevo.EmailAsesor,
                    Cc = PersonalNuevo.EmailJefe,
                    Subject = asuntoMensaje,
                    Message = cuerpoMensaje,
                    RemitenteC = "Integra - Reasignacion Automatica "
                };
                servicioMail.SetData(data);
                var resultado = servicioMail.VerifyData();
                servicioMail.SendMessageTask();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el flag de venta cruzada y actualizar el asesor al que pertenece la oportunidadNuevaEntidad
        /// </summary>
        /// <param name="oportunidadEntidad">Referencia a entidad Oportunidad </param>
        /// <param name="idAsesorReasignacion">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="PermaneceEstado">Flag para validar si permanece el estado anterior</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        private void ActualizarOportunidadVentaCruzadaAsesor(ref ReprogramacionDTO reprogramacion, int idAsesorReasignacion, bool PermaneceEstado, string Usuario)
        {
            if (PermaneceEstado == true)//viene de reasignacion OD
            {
                //marcamos esta oportunidadNuevaEntidad para poder darles seguimiento y mostrarlas en el reporte de metas
                reprogramacion.Oportunidad.FlagVentaCruzada = null;
            }
            else //Logica Normal
            {
                //marcamos esta oportunidadNuevaEntidad para poder darles seguimiento y mostrarlas en el reporte de metas
                reprogramacion.Oportunidad.FlagVentaCruzada = 1;
                reprogramacion.Oportunidad.IdEstadoOportunidad = EstadoOportunidad.ReasignadaVentaCruzada;
            }
            if (idAsesorReasignacion != 0)
                reprogramacion.Oportunidad.IdPersonalAsignado = idAsesorReasignacion;

            reprogramacion.Oportunidad.FechaModificacion = DateTime.Now;
            reprogramacion.Oportunidad.UsuarioModificacion = Usuario;
        }

        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el asesor de venta cruzada por el idAlumno
        /// </summary>
        /// <param name="idAlumno"> id del alumno </param>
        /// <returns> Vacio </returns>
        public int ObtenerAsesorVentaCruzada(int idAlumno)
        {
            try
            {
                var servicioAlumno = new AlumnoService(_unitOfWork);
                var servicioAsignacionOportunidad = new AsignacionOportunidadService(_unitOfWork);
                var servicioPEspecifico = new PEspecificoService(_unitOfWork);
                var fechaActual = DateTime.Now;
                int _idPEspecifico;
                string _modalidad;
                int _idAsesor;
                int? idCiudad;
                int idPais;
                int idPEspecifico = 0;
                int asesorActual = 0;//obtiene el valor de asesor
                int cantidadOportunidades = 0;
                int maxOportunidades = 0;

                var alumnoCiudadPais = servicioAlumno.ObtenerCiudadPaisPorIdAlumno(idAlumno);
                idCiudad = alumnoCiudadPais.IdCiudad;
                idPais = Convert.ToInt16(alumnoCiudadPais.IdCodigoPais);
                var ventaCruzada = _unitOfWork.OportunidadRepository.ObtenerCentroCostoProbable(idAlumno, fechaActual).OrderByDescending(x => x.Precio).ToList();
                foreach (var item in ventaCruzada)
                {
                    _idPEspecifico = item.IdPEspecifico;
                    _modalidad = item.Tipo;
                    _idAsesor = item.IdPersonal;
                    if (_modalidad == "Presencial")
                    {
                        if (idCiudad == _unitOfWork.PEspecificoRepository.ObtenerIdCiudad(_idPEspecifico).Valor)
                        {
                            asesorActual = _idAsesor;
                            cantidadOportunidades = servicioAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Valor.Value;
                            maxOportunidades = servicioAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).Valor.Value;
                            if (cantidadOportunidades <= maxOportunidades)
                            {
                                idPEspecifico = _idPEspecifico;
                                break;
                            }
                        }
                    }
                    else
                    {
                        asesorActual = _idAsesor;
                        cantidadOportunidades = servicioAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Valor.Value;
                        maxOportunidades = servicioAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).Valor.Value;
                        if (cantidadOportunidades <= maxOportunidades)
                        {
                            idPEspecifico = _idPEspecifico;
                            break;
                        }
                    }
                    if (idPEspecifico != 0)
                    {
                        break;
                    }
                }
                if (idPEspecifico != 0)
                {
                    return asesorActual;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el asesor de venta cruzada por el idAlumno
        /// </summary>
        /// <param name="idAlumno"> id del alumno </param>
        /// <returns> Vacio </returns>
        public ActividadAgendaDTO RealizarCambioCentroCosto(int idOportunidad, int idCentroCosto, string usuario)
        {
            try
            {
                if (idOportunidad == 0)
                {
                    throw new BadRequestException("Id Oportunidad no valido");
                }
                if (idCentroCosto == 0)
                {
                    throw new BadRequestException("Id Centro Costo no valido");
                }
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);

                if (oportunidad == null || oportunidad.Id == 0)
                {
                    throw new BadRequestException("Oportunidad no existente");
                }

                if (oportunidad.IdCentroCosto == idCentroCosto)
                {
                    throw new BadRequestException("La oportunidad ya cuenta con el centro de costo solicitado");
                }

                var pEspecificoActual = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto.Value);

                var estadoMatricula = _unitOfWork.MatriculaCabeceraRepository.Exist(x => x.IdAlumno == oportunidad.IdAlumno && x.IdPespecifico == pEspecificoActual.Id);
                if (estadoMatricula)
                {
                    throw new BadRequestException("El alumno ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Sistemas");
                }

                var cronograma = _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidad(oportunidad.Id);
                if (cronograma != null && cronograma.Id != 0)
                {
                    var estadoMatricula2 = _unitOfWork.MatriculaCabeceraRepository.Exist(x => x.IdCronograma == cronograma.Id);
                    if (estadoMatricula2)
                    {
                        throw new BadRequestException("El alumno ya tiene una Matricula Cabecera Registrada, si desea hacer el cambio de Centro de Costo comunicarse con Sistemas");
                    }
                }

                var pEspecificoCambio = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto);
                if (pEspecificoCambio.EstadoP != "Lanzamiento" && pEspecificoCambio.EstadoP != "Por Ejecucion")
                {
                    throw new BadRequestException("El centro de costo no se encuentra en estado de 'Lanzamiento' o 'Por Ejecucion'");
                }

                _reprogramacion = new ReprogramacionDTO();
                _reprogramacion.Oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad)!;
                _reprogramacion.Oportunidad.IdCentroCosto = idCentroCosto;
                _reprogramacion.Usuario = usuario;


                _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_reprogramacion.Oportunidad.IdActividadDetalleUltima.Value);

                _reprogramacion.ActividadTrabajada.Comentario = "Cambio de Centro Costo";
                _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno = 35;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaActividad = null;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno = null;

                _reprogramacion.ActividadTemp = new ActividadDetalle
                {
                    Id = _reprogramacion.Oportunidad.IdActividadDetalleUltima.Value,
                    FechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada,
                    Comentario = "Cambio de Centro Costo",
                    IdAlumno = _reprogramacion.Oportunidad.IdAlumno,
                    IdOportunidad = _reprogramacion.Oportunidad.Id,
                    IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona,
                };
                _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_reprogramacion.Oportunidad.IdActividadDetalleUltima.Value);



                var taskOportunidadLogTemp = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog((int)_reprogramacion.ActividadTemp.IdOportunidad);

                _reprogramacion.ActividadTemp.DuracionReal = 13;
                _reprogramacion.ActividadTemp.IdCentralLlamada = 3;

                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(_reprogramacion.Oportunidad.IdPersonalAsignado.GetValueOrDefault());
                _reprogramacion.ActividadTemp.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;

                _reprogramacion.ActividadTrabajada = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_reprogramacion.ActividadTemp.Id);
                var fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.ActividadTrabajada.FechaReal = fechareal;
                _reprogramacion.ActividadTrabajada.DuracionReal = _reprogramacion.ActividadTemp.DuracionReal;
                _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle = _reprogramacion.ActividadTemp.IdEstadoActividadDetalle;
                _reprogramacion.ActividadTrabajada.Comentario = _reprogramacion.ActividadTemp.Comentario;
                _reprogramacion.ActividadTrabajada.IdCentralLlamada = _reprogramacion.ActividadTemp.IdCentralLlamada;
                _reprogramacion.ActividadTrabajada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadTrabajada.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.ActividadTrabajada.IdClasificacionPersona = _reprogramacion.ActividadTemp.IdClasificacionPersona;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;

                _reprogramacion.Oportunidad.UltimoComentario = "Cambio Centro Costo";
                _reprogramacion.Oportunidad.UltimaFechaProgramada = _reprogramacion.ActividadTemp.FechaProgramada == null ? DateTime.Now : _reprogramacion.ActividadTemp.FechaProgramada;

                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTemp.Id;

                _reprogramacion.Oportunidad.FechaModificacion = DateTime.Now;
                _reprogramacion.Oportunidad.UsuarioModificacion = _reprogramacion.Usuario;

                _reprogramacion.OportunidadLogTemp = taskOportunidadLogTemp;
                //Crear Log Nuevo
                _reprogramacion.OportunidadLogNueva = new OportunidadLog();
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.OportunidadLogTemp.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.OportunidadLogTemp.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdCentroCosto = _reprogramacion.Oportunidad.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdPersonalAsignado = _reprogramacion.Oportunidad.IdPersonalAsignado;
                _reprogramacion.OportunidadLogNueva.IdTipoDato = _reprogramacion.Oportunidad.IdTipoDato;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadAnt = _reprogramacion.Oportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidad = _reprogramacion.Oportunidad.IdFaseOportunidad;
                _reprogramacion.OportunidadLogNueva.IdOrigen = _reprogramacion.Oportunidad.IdOrigen;
                _reprogramacion.OportunidadLogNueva.IdContacto = _reprogramacion.Oportunidad.IdAlumno;
                _reprogramacion.OportunidadLogNueva.FechaFinLog = _reprogramacion.OportunidadLogTemp.FechaLog;
                _reprogramacion.OportunidadLogNueva.IdCentroCostoAnt = _reprogramacion.OportunidadLogTemp.IdCentroCosto;
                _reprogramacion.OportunidadLogNueva.IdAsesorAnt = _reprogramacion.OportunidadLogTemp.IdPersonalAsignado;

                var fechalog = DateTime.Now;
                //MEXICO
                if (diferenciaHoraria != null)
                {
                    fechalog = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                _reprogramacion.OportunidadLogNueva.FechaLog = fechalog;
                _reprogramacion.OportunidadLogNueva.IdActividadDetalle = _reprogramacion.ActividadTemp.Id;
                _reprogramacion.OportunidadLogNueva.Comentario = "Cambio de Centro Costo";
                _reprogramacion.OportunidadLogNueva.IdOportunidad = _reprogramacion.Oportunidad.Id;
                _reprogramacion.OportunidadLogNueva.IdCategoriaOrigen = _reprogramacion.Oportunidad.IdCategoriaOrigen;
                _reprogramacion.OportunidadLogNueva.IdSubCategoriaDato = _reprogramacion.Oportunidad.IdSubCategoriaDato;
                _reprogramacion.OportunidadLogNueva.FechaRegistroCampania = _reprogramacion.Oportunidad.FechaRegistroCampania;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIc = _reprogramacion.OportunidadLogTemp.IdFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadIp = _reprogramacion.OportunidadLogTemp.IdFaseOportunidadIp;
                _reprogramacion.OportunidadLogNueva.IdFaseOportunidadPf = _reprogramacion.OportunidadLogTemp.IdFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaEnvioFaseOportunidadPf = _reprogramacion.OportunidadLogTemp.FechaEnvioFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadIc = _reprogramacion.OportunidadLogTemp.FechaPagoFaseOportunidadIc;
                _reprogramacion.OportunidadLogNueva.FechaPagoFaseOportunidadPf = _reprogramacion.OportunidadLogTemp.FechaPagoFaseOportunidadPf;
                _reprogramacion.OportunidadLogNueva.FasesActivas = _reprogramacion.OportunidadLogTemp.FasesActivas;
                _reprogramacion.OportunidadLogNueva.CodigoPagoIc = _reprogramacion.OportunidadLogTemp.CodigoPagoIc;
                _reprogramacion.OportunidadLogNueva.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.OportunidadLogNueva.IdPersonalAreaTrabajo = _reprogramacion.Oportunidad.IdPersonalAreaTrabajo;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaAlterno;
                _reprogramacion.OportunidadLogNueva.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTemp.IdOcurrenciaActividadAlterno;


                _reprogramacion.OportunidadLogNueva.CambioFase = false;
                _reprogramacion.OportunidadLogNueva.FechaCambioFase = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                _reprogramacion.OportunidadLogNueva.FechaCambioFaseAnt = _reprogramacion.OportunidadLogTemp.FechaCambioFase;
                _reprogramacion.OportunidadLogNueva.FechaCambioFaseIs = _reprogramacion.OportunidadLogTemp.FechaCambioFaseIs;
                _reprogramacion.OportunidadLogNueva.CambioFaseIs = false;
                _reprogramacion.OportunidadLogNueva.CambioFaseAsesor = 0;
                _reprogramacion.OportunidadLogNueva.FechaCambioAsesor = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                _reprogramacion.OportunidadLogNueva.FechaCambioAsesorAnt = _reprogramacion.OportunidadLogTemp.FechaCambioAsesor;
                _reprogramacion.OportunidadLogNueva.CicloRn2 = _reprogramacion.OportunidadLogTemp.CicloRn2;

                _reprogramacion.OportunidadLogNueva.FechaCreacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.FechaModificacion = DateTime.Now;
                _reprogramacion.OportunidadLogNueva.UsuarioCreacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.UsuarioModificacion = _reprogramacion.Usuario;
                _reprogramacion.OportunidadLogNueva.Estado = true;

                _reprogramacion.ActividadTemp = null;
                _reprogramacion.OportunidadLogTemp = null;

                _reprogramacion.ActividadNuevaProgramada = new ActividadDetalle();
                _reprogramacion.ActividadNuevaProgramada.IdOportunidad = _reprogramacion.ActividadTrabajada.IdOportunidad;
                _reprogramacion.ActividadNuevaProgramada.IdAlumno = _reprogramacion.ActividadTrabajada.IdAlumno;
                _reprogramacion.ActividadNuevaProgramada.Actor = "A";
                _reprogramacion.ActividadNuevaProgramada.FechaProgramada = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ? _reprogramacion.Oportunidad.UltimaFechaProgramada.Value : default;
                _reprogramacion.ActividadNuevaProgramada.IdEstadoActividadDetalle = EstadoActividadDetalle.NoEjecutado;
                _reprogramacion.ActividadNuevaProgramada.FechaCreacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.FechaModificacion = DateTime.Now;
                _reprogramacion.ActividadNuevaProgramada.UsuarioCreacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.UsuarioModificacion = _reprogramacion.Oportunidad.UsuarioModificacion;
                _reprogramacion.ActividadNuevaProgramada.IdActividadCabecera = _reprogramacion.ActividadTrabajada.IdActividadCabecera;
                _reprogramacion.ActividadNuevaProgramada.IdClasificacionPersona = _reprogramacion.Oportunidad.IdClasificacionPersona;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaAlterno;
                _reprogramacion.ActividadNuevaProgramada.IdOcurrenciaActividadAlterno = _reprogramacion.ActividadTrabajada.IdOcurrenciaActividadAlterno;
                _reprogramacion.ActividadNuevaProgramada.Estado = true;
                //Actualiza Oportunidad

                _reprogramacion.Oportunidad.IdActividadDetalleUltima = _reprogramacion.ActividadTrabajada.Id;
                _reprogramacion.Oportunidad.IdActividadCabeceraUltima = _reprogramacion.ActividadTrabajada.IdActividadCabecera.Value;
                _reprogramacion.Oportunidad.IdEstadoActividadDetalleUltimoEstado = _reprogramacion.ActividadTrabajada.IdEstadoActividadDetalle;
                _reprogramacion.Oportunidad.IdEstadoOportunidad = _reprogramacion.Oportunidad.UltimaFechaProgramada.HasValue ?
                                                    EstadoOportunidad.Programada :
                                                    EstadoOportunidad.NoProgramada;

                var actividadDetelleCreada = _unitOfWork.ActividadDetalleRepository.Add(_reprogramacion.ActividadNuevaProgramada);
                _unitOfWork.Commit();
                _reprogramacion.Oportunidad.IdActividadDetalleUltima = actividadDetelleCreada.Id;
                _reprogramacion.ActividadNuevaProgramada = null;

                var oportunidadActualizada = _unitOfWork.OportunidadRepository.Update(CargarOportunidad(_reprogramacion));
                _unitOfWork.Commit();
                return _unitOfWork.OportunidadRepository.ObtenerDatosOportunidad(oportunidadActualizada.Id)!;
            }
            catch
            {
                throw;
            }
        }
    }
}

