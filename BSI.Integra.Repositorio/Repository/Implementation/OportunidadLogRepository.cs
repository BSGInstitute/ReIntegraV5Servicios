using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadLogRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_OportunidadLog
    /// </summary>
    public class OportunidadLogRepository : GenericRepository<TOportunidadLog>, IOportunidadLogRepository
    {
        private Mapper _mapper;

        public OportunidadLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadLog, OportunidadLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadLog MapeoEntidad(OportunidadLog entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadLog modelo = _mapper.Map<TOportunidadLog>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadLog Add(OportunidadLog entidad)
        {
            try
            {
                var OportunidadLog = MapeoEntidad(entidad);
                base.Insert(OportunidadLog);
                return OportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadLog Update(OportunidadLog entidad)
        {
            try
            {
                var OportunidadLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadLog.RowVersion = entidadExistente.RowVersion;

                base.Update(OportunidadLog);
                return OportunidadLog;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TOportunidadLog> Add(IEnumerable<OportunidadLog> listadoEntidad)
        {
            try
            {
                List<TOportunidadLog> listado = new List<TOportunidadLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TOportunidadLog> Update(IEnumerable<OportunidadLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadLog> listado = new List<TOportunidadLog>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadLog.
        /// </summary>
        /// <returns> List<OportunidadLogDTO> </returns>
        public IEnumerable<OportunidadLog> ObtenerOportunidadLog()
        {
            try
            {
                List<OportunidadLog> rpta = new List<OportunidadLog>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdCentroCosto,
	                    IdPersonal_Asignado AS IdPersonalAsignado,
	                    IdTipoDato,
	                    IdFaseOportunidad_Ant AS IdFaseOportunidadAnt,
	                    IdFaseOportunidad,
	                    IdOrigen,
	                    IdContacto,
	                    Fecha_Log AS FechaLog,
	                    IdActividadDetalle,
	                    IdOcurrencia,
	                    IdOcurrenciaActividad,
	                    Comentario,
	                    IdCategoriaOrigen,
	                    IdConjuntoAnuncio,
	                    IdFaseOportunidad_IP AS IdFaseOportunidadIP,
	                    IdFaseOportunidad_IC AS IdFaseOportunidadIC,
	                    FechaEnvioFaseOportunidadPF,
	                    FechaPagoFaseOportunidadPF,
	                    FechaPagoFaseOportunidadIC,
	                    FasesActivas,
	                    FechaRegistroCampania,
	                    IdFaseOportunidad_PF AS IdFaseOportunidadPF,
	                    CodigoPagoIC,
	                    IdAsesor_Ant AS IdAsesorAnt,
	                    IdCentroCosto_Ant AS IdCentroCostoAnt,
	                    FechaFinLog,
	                    FechaCambioFase,
	                    CambioFase,
	                    FechaCambioFaseIS,
	                    CambioFaseIS,
	                    FechaCambioFaseAnt,
	                    FechaCambioAsesor,
	                    FechaCambioAsesorAnt,
	                    CambioFaseAsesor,
	                    CicloRN2,
	                    IdSubCategoriaDato,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    IdClasificacionPersona,
	                    IdPersonalAreaTrabajo,
	                    IdOcurrenciaAlterno,
	                    IdOcurrenciaActividadAlterno
                    FROM com.T_OportunidadLog
                        WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadLog>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OportunidadLog para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadLogComboDTO> </returns>
        public IEnumerable<OportunidadLogComboDTO> ObtenerCombo()
        {
            try
            {
                List<OportunidadLogComboDTO> rpta = new List<OportunidadLogComboDTO>();
                var query = @"
                    SELECT
	                    OLOG.Id,
	                    OLOG.IdOportunidad,
	                    FANT.Codigo AS FaseAnterior,
	                    FO.Codigo AS FaseActual,
	                    OLOG.Fecha_Log
                    FROM com.T_OportunidadLog AS OLOG
                    INNER JOIN pla.T_FaseOportunidad AS FANT
	                    ON OLOG.IdFaseOportunidad_Ant = FANT.Id
	                    AND FANT.Estado = 1
                    INNER JOIN pla.T_FaseOportunidad AS FO
	                    ON OLOG.IdFaseOportunidad = FO.Id
	                    AND FO.Estado = 1
                    WHERE OLOG.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadLogComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las fechas donde no se tuvo contacto con el cliente 
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad Relacionada</param>
        /// <returns> List<ValorDateTimeDTO> </returns>
        public IEnumerable<FechaLogDatetimeDTO> ObtenerFechasSinContactoPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<FechaLogDatetimeDTO> fechasSinContacto = new List<FechaLogDatetimeDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("com.SP_TOportunidadLog_SinContacto", new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    fechasSinContacto = JsonConvert.DeserializeObject<List<FechaLogDatetimeDTO>>(resultadoQuery);
                }
                return fechasSinContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log y el nombre del Personal asociados a una Oportunidad para Reporte de Seguimiento
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogReporteSeguimientoYPersonalDTO> </returns>
        public IEnumerable<OportunidadLogReporteSeguimientoYPersonalDTO> ObtenerReporteSeguimientoYPersonalPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<OportunidadLogReporteSeguimientoYPersonalDTO> oportunidadLog = new List<OportunidadLogReporteSeguimientoYPersonalDTO>();
                var query = @"
                    SELECT *
                    FROM com.V_OportunidadLogReporteSeguimientoNombrePersonal
                    WHERE IdOportunidad = @idOportunidad
                    ORDER BY FechaModificacion";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteSeguimientoYPersonalDTO>>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log asociado a una Oportunidad para Reporte de Seguimiento
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadLogReporteSeguimientoYPersonalDTO> </returns>
        public IEnumerable<OportunidadLogReporteSeguimientoNWDTO> ObtenerOportunidadLogReporteSeguimientoNWPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<OportunidadLogReporteSeguimientoNWDTO> oportunidadLog = new List<OportunidadLogReporteSeguimientoNWDTO>();
                var query = @"
                    SELECT FaseInicio,FaseDestino,FechaModificacion,FechaSiguienteLlamada,IdFaseOportunidad,IdFaseOportunidadIP,IdFaseOportunidadPF,
	                    IdFaseOportunidadIC,FechaEnvioFaseOportunidadPF,FechaPagoFaseOportunidadPF,FechaPagoFaseOportunidadIC,IdOcurrencia,
	                    IdEstadoOcurrencia,TiempoDuracion,TiempoDuracionMinutos,TiempoDuracion3CX,IdCentralLLamada,IdTresCX,IdOportunidadLog,
	                    FechaIncioLlamadaIntegra,EstadoLlamadaIntegra,EstadoLlamadaTresCX,FechaIncioLlamadaTresCX,NombreActividad,NombreOcurrencia,
	                    ComentarioActividad,FechaFinLlamadaIntegra,FechaFinLlamadaTresCX,SubEstadoLlamadaTresCX,SubEstadoLlamadaIntegra,
	                    IdFaseOportunidadInicial,NombreGrabacionIntegra,NombreGrabacionTresCX,Webphone
                    FROM com.V_ObtenerOportunidadLogReporteSeguimientoNW
                    WHERE IdOportunidad = @idOportunidad
	                    AND EstadoOportunidadLog = 1
	                    AND (ComentarioActividad <> 'Asignacion Manual' OR ComentarioActividad IS NULL)
	                    AND IdActividadDetalle IS NOT NULL
                    ORDER BY FechaModificacion";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteSeguimientoNWDTO>>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 27/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log asociado a una Oportunidad para Reporte de Seguimiento (3CX)
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Lista de OportunidadLogReporteSeguimientoAlternoDTO </returns>
        public IEnumerable<OportunidadLogReporteSeguimientoAlternoDTO> ObtenerOportunidadLogReporteSeguimientoNWAlterno3cx(int idOportunidad)
        {
            try
            {
                List<OportunidadLogReporteSeguimientoAlternoDTO> oportunidadLog = new List<OportunidadLogReporteSeguimientoAlternoDTO>();
                var query = @"
                    	SELECT 
                            FaseInicio,
		                    FaseDestino,
		                    IdFaseOportunidadInicial,
		                    IdActividadDetalle,
		                    FechaModificacion,
		                    FechaSiguienteLlamada,
		                    IdFaseOportunidad,
		                    IdFaseOportunidadIP,
		                    IdFaseOportunidadPF,
		                    IdFaseOportunidadIC,
		                    FechaEnvioFaseOportunidadPF,
		                    FechaPagoFaseOportunidadPF,
		                    FechaPagoFaseOportunidadIC,
		                    IdOcurrencia,
		                    IdEstadoOcurrencia,
		                    IdOportunidadLog,
		                    IdOportunidad,
		                    EstadoOportunidadLog,
		                    NombreActividad,
		                    NombreOcurrencia,
		                    ComentarioActividad,
		                    IdContacto,
		                    IdLW,
		                    IdLWCC,
		                    IdTresCxCC,
		                    IdRingover,
		                    DuracionTimbradoLW,
		                    DuracionContestoLW,
		                    DuracionTimbradoCentralLWCC,
		                    DuracionContestoCentralLWCC,
		                    DuracionTimbradoCentralTresCx,
		                    DuracionContestoCentralTresCx,
		                    DuracionTimbradoCentralRingover,
		                    DuracionContestoCentralRingover,
		                    IdLlamadaCentralLWCC,
		                    IdLlamadaCentralTresCx,
		                    IdLlamadaCentralRingover,
		                    FechaInicioLlamadaIntegra,
		                    FechaFinLlamadaIntegra,
		                    FechaInicioLlamadaTresCXIntegra,
		                    FechaFinLlamadaTresCXIntegra,
		                    FechaInicioLlamadaTresCX,
		                    FechaFinLlamadaTresCX,
                            FechaInicioLlamadaRingover,
		                    FechaFinLlamadaRingover,
		                    NombreGrabacionIntegra,
		                    NombreGrabacionTresCXIntegra,
		                    NombreGrabacionTresCX,
		                    NombreGrabacionRingover,
		                    EstadoLlamadaIntegra,
		                    SubEstadoLlamadaIntegra,
		                    EstadoLlamadaTresCXIntegra,
		                    SubEstadoLlamadaTresCXIntegra,
		                    EstadoLlamadaTresCX,
		                    SubEstadoLlamadaTresCX,
                            EstadoLlamadaRingover,
		                    SubEstadoLlamadaRingover,
		                    Webphone,
		                    WebphoneTresCx,
		                    WebphoneRingover,
                            OtroMedio,
                            EstadoSeguimientoWhatsApp
	                    FROM com.V_ObtenerOportunidadLogReporteSeguimientoNWAlterno3cx
	                    WHERE 
		                    IdOportunidad = @idOportunidad
		                    AND EstadoOportunidadLog = 1
		                    AND (ComentarioActividad <> 'Asignacion Manual' OR ComentarioActividad IS NULL)
		                    AND IdActividadDetalle IS NOT NULL
	                    ORDER BY FechaModificacion";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteSeguimientoAlternoDTO>>(resultadoQuery)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OLR-OOLRSA3cx-001@Error en ObtenerOportunidadLogReporteSeguimientoNWAlterno3cx, {ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Detalle del Log asociado a una Oportunidad para Reporte de Seguimiento (3CX)
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Lista de OportunidadLogReporteDTO </returns>
        public IEnumerable<OportunidadLogReporteDTO> ObtenerOportunidadLogReporteSeguimientoV5(int idOportunidad,int diferenciaHoraria)
        {
            try
            {
                List<OportunidadLogReporteDTO> oportunidadLog = new List<OportunidadLogReporteDTO>();
                var query = @"
                    	SELECT 
                            FaseInicio,
	                        FaseDestino,
	                        IdFaseOportunidadInicial,
	                        IdActividadDetalle,
	                        FechaModificacion,
	                        FechaSiguienteLlamada,
	                        IdFaseOportunidad,
	                        IdFaseOportunidadIP,
	                        IdFaseOportunidadPF,
	                        IdFaseOportunidadIC,
	                        FechaEnvioFaseOportunidadPF,
	                        FechaPagoFaseOportunidadPF,
	                        FechaPagoFaseOportunidadIC,
	                        IdOcurrencia,
	                        IdEstadoOcurrencia,
	                        IdOportunidadLog,
	                        IdOportunidad,
	                        EstadoOportunidadLog,
	                        NombreActividad,
	                        NombreOcurrencia,
	                        ComentarioActividad,
	                        IdContacto,
	                        OtroMedio,
	                        EstadoSeguimientoWhatsApp,
	                        IdRegistroLlamada,
                            DATEADD(HOUR, @diferenciaHoraria,FechaInicioLlamada) FechaInicioLlamada,
                            DATEADD(HOUR, @diferenciaHoraria, FechaFinLlamada) FechaFinLlamada,
	                        DuracionTimbrado,
	                        DuracionContesto,
	                        EstadoLlamada,
	                        SubEstadoLlamada,
	                        NombreGrabacion,
	                        UrlGrabacion,
                            UrlGrabacion2,
	                        WebphoneGrabacion,
	                        TelefonoDestinoReal,
	                        TelefonoDestino,
	                        OrigenLlamada,
	                        AnexoCentral,
                            esLlamadaCalificada,
                            esLlamadaTranscrita
	                    FROM com.V_ObtenerOportunidadLogReporteSeguimientoV5
	                    WHERE 
		                    IdOportunidad = @idOportunidad
		                    AND EstadoOportunidadLog = 1
		                    AND ((ComentarioActividad <> 'Asignacion Manual'  AND ComentarioActividad <> 'Cambio de Centro Costo') OR ComentarioActividad IS NULL)
		                    AND IdActividadDetalle IS NOT NULL
	                    ORDER BY FechaModificacion";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad, diferenciaHoraria });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<OportunidadLogReporteDTO>>(resultadoQuery)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OLR-OOLRSA3cx-001@Error en ObtenerOportunidadLogReporteSeguimientoV5, {ex.Message}");
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Reporte de Actividades y Ocurrencias relacionado a una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public List<ReporteActividadOcurrenciaDTO> ObtenerReporteActividadOcurrenciaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> rpta = new List<ReporteActividadOcurrenciaDTO>();
                var query = @"
                    SELECT IdOportunidad,IdEstadoOcurrencia,IdFaseOportunidadAnterior,IdFaseActual,FechaReal
                    FROM com.V_NumeroActividadesEstadoOcurrencia
                    WHERE IdOportunidad = @idOportunidad";
                var resultado = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public IEnumerable<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrencia(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> oportunidadLog = new List<ReporteActividadOcurrenciaDTO>();
                var query = @"
                    SELECT
	                    IdOportunidad,
	                    IdEstadoOcurrencia,
	                    IdFaseOportunidadAnterior,
	                    IdFaseActual,
	                    FechaReal
                    FROM com.V_NumeroActividadesEstadoOcurrencia
                    WHERE IdOportunidad = @IdOportunidad";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Ultimo Oportunidad Log
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> UltimoOportunidadLogDTO </returns>
        public OportunidadLog ObtenerUltimoOportunidadLog(int idOportunidad)
        {
            try
            {
                OportunidadLog oportunidadLog = new OportunidadLog();
                var query = @"
                        SELECT TOP 1  
                                Id,
                                Fecha_Log AS FechaLog,
                                FechaCambioFase,
                                FechaCambioFaseIS,
                                IdPersonal_Asignado AS IdPersonalAsignado,
                                IdCentroCosto,
                                FechaCambioFaseAnt,
                                FechaCambioAsesor,
                                FechaCambioAsesorAnt,
                                CambioFaseAsesor,
                                CicloRN2,
                                IdClasificacionPersona,
                                IdPersonalAreaTrabajo
                        FROM com.T_OportunidadLog
                        WHERE Estado = 1 AND IdOportunidad = @idOportunidad
                        ORDER BY Fecha_Log DESC";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLog>(resultadoQuery)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Ultimo Oportunidad Log
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> UltimoOportunidadLogDTO </returns>
        public async Task<OportunidadLog> ObtenerUltimoOportunidadLogAsync(int idOportunidad)
        {
            try
            {
                OportunidadLog oportunidadLog = new OportunidadLog();
                var query = @"
                        SELECT TOP 1  
                                Id,
                                Fecha_Log AS FechaLog,
                                FechaCambioFase,
                                FechaCambioFaseIS,
                                IdPersonal_Asignado AS IdPersonalAsignado,
                                IdCentroCosto,
                                FechaCambioFaseAnt,
                                FechaCambioAsesor,
                                FechaCambioAsesorAnt,
                                CambioFaseAsesor,
                                CicloRN2,
                                IdClasificacionPersona,
                                IdPersonalAreaTrabajo
                        FROM com.T_OportunidadLog
                        WHERE Estado = 1 AND IdOportunidad = @idOportunidad
                        ORDER BY Fecha_Log DESC";
                var resultadoQuery = await _dapperRepository.FirstOrDefaultAsync(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLog>(resultadoQuery)!;
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public OportunidadLog ObtenerPorId(int idOportunidadLog)
        {
            try
            {
                OportunidadLog oportunidadLog = new OportunidadLog();
                var query = @"SELECT 
                                Id, IdOportunidad, IdCentroCosto, IdPersonal_Asignado AS IdPersonalAsignado, IdTipoDato, IdFaseOportunidad_Ant AS IdFaseOportunidadAnt, IdFaseOportunidad, IdOrigen,
                                IdContacto, Fecha_Log AS FechaLog, IdActividadDetalle, IdOcurrencia, IdOcurrenciaActividad, Comentario, IdCategoriaOrigen, IdConjuntoAnuncio, IdOcurrenciaAlterno,
                                IdFaseOportunidad_IC AS IdFaseOportunidadIc, FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, FasesActivas, FechaRegistroCampania,
                                IdFaseOportunidad_PF AS IdFaseOportunidadPf, CodigoPagoIC, IdAsesor_Ant AS IdAsesorAnt, IdCentroCosto_Ant AS IdCentroCostoAnt, FechaFinLog, FechaCambioFase, CambioFase,
                                FechaCambioFaseIS, CambioFaseIS, FechaCambioFaseAnt, FechaCambioAsesor, FechaCambioAsesorAnt, CambioFaseAsesor, CicloRN2, IdSubCategoriaDato, Estado, UsuarioCreacion,
                                UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion, IdClasificacionPersona, IdPersonalAreaTrabajo, IdFaseOportunidad_IP AS IdFaseOportunidadIp,
                                IdOcurrenciaActividadAlterno
                    FROM 
                        com.T_OportunidadLog
                    WHERE 
                        Estado = 1 AND IdOportunidad = @IdOportunidadLog ORDER BY Fecha_Log DESC";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdOportunidadLog = idOportunidadLog });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLog>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 16/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Ultimo Oportunidad Log
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> UltimoOportunidadLogDTO </returns>
        public OportunidadLog ObtenerUltimoOportunidadLog2(int idOportunidad)
        {
            try
            {
                OportunidadLog oportunidadLog = new OportunidadLog();
                var query = @"
                    SELECT TOP 1 Id, Fecha_Log,FechaCambioFase,FechaCambioFaseIS,IdPersonal_Asignado,IdCentroCosto,FechaCambioFaseAnt,FechaCambioAsesor,
	                    FechaCambioAsesorAnt,CambioFaseAsesor,CicloRN2,IdClasificacionPersona,IdPersonalAreaTrabajo
                    FROM com.T_OportunidadLog
                    WHERE Estado = 1 AND IdOportunidad = @idOportunidad
                    ORDER BY Fecha_Log DESC";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLog>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadLog ObtenerOportunidadLogPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var oportunidadLog = new OportunidadLog();
                var query = @"
                            SELECT 
                                Id, IdOportunidad, IdCentroCosto, IdPersonal_Asignado AS IdPersonalAsignado, IdTipoDato, IdFaseOportunidad_Ant AS IdFaseOportunidadAnt, IdFaseOportunidad, IdOrigen, 
                                IdActividadDetalle, IdOcurrencia, IdOcurrenciaActividad, Comentario, IdCategoriaOrigen, IdConjuntoAnuncio, IdFaseOportunidad_IP AS IdFaseOportunidadIp, Estado,
                                FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, FasesActivas, FechaRegistroCampania, IdFaseOportunidad_PF AS IdFaseOportunidadPf,
                                CodigoPagoIC, IdAsesor_Ant AS IdAsesorAnt, IdCentroCosto_Ant AS IdCentroCostoAnt, FechaFinLog, FechaCambioFase, CambioFase, FechaCambioFaseIS, CambioFaseIS,
                                FechaCambioAsesor, FechaCambioAsesorAnt, CambioFaseAsesor, CicloRN2, IdSubCategoriaDato, IdOcurrenciaActividadAlterno, UsuarioCreacion, Fecha_Log AS FechaLog,
                                FechaCreacion, FechaModificacion, RowVersion, IdMigracion, IdClasificacionPersona, IdPersonalAreaTrabajo, IdOcurrenciaAlterno, UsuarioModificacion, IdContacto,
                                IdFaseOportunidad_IC AS IdFaseOportunidadIc, FechaCambioFaseAnt
                            FROM 
                                com.T_OportunidadLog
                            WHERE 
                                Estado = 1 AND IdOportunidad = @IdOportunidad ORDER BY FechaCreacion DESC";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLog = JsonConvert.DeserializeObject<OportunidadLog>(resultadoQuery);
                }
                return oportunidadLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OportunidadLog> ObtenerOportunidadLogsPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var oportunidadLogs = new List<OportunidadLog>();
                var query = @"
                            SELECT 
                                Id, IdOportunidad, IdCentroCosto, IdPersonal_Asignado AS IdPersonalAsignado, IdTipoDato, IdFaseOportunidad_Ant AS IdFaseOportunidadAnt, IdFaseOportunidad, IdOrigen, 
                                IdActividadDetalle, IdOcurrencia, IdOcurrenciaActividad, Comentario, IdCategoriaOrigen, IdConjuntoAnuncio, IdFaseOportunidad_IP AS IdFaseOportunidadIp, Estado,
                                FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, FasesActivas, FechaRegistroCampania, IdFaseOportunidad_PF AS IdFaseOportunidadPf,
                                CodigoPagoIC, IdAsesor_Ant AS IdAsesorAnt, IdCentroCosto_Ant AS IdCentroCostoAnt, FechaFinLog, FechaCambioFase, CambioFase, FechaCambioFaseIS, CambioFaseIS,
                                FechaCambioAsesor, FechaCambioAsesorAnt, CambioFaseAsesor, CicloRN2, IdSubCategoriaDato, IdOcurrenciaActividadAlterno, UsuarioCreacion, Fecha_Log AS FechaLog,
                                FechaCreacion, FechaModificacion, RowVersion, IdMigracion, IdClasificacionPersona, IdPersonalAreaTrabajo, IdOcurrenciaAlterno, UsuarioModificacion, IdContacto,
                                IdFaseOportunidad_IC AS IdFaseOportunidadIc, FechaCambioFaseAnt
                            FROM 
                                com.T_OportunidadLog
                            WHERE 
                                Estado = 1 AND IdOportunidad = @IdOportunidad ORDER BY FechaCreacion DESC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "[]")
                {
                    oportunidadLogs = JsonConvert.DeserializeObject<List<OportunidadLog>>(resultadoQuery);
                }
                return oportunidadLogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory  Ramirez
        /// Fecha:  06/12/2022
        /// Version: 2.0
        /// <summary>
        /// Elimina logicamente la ultima oportunidad log por el idOportunidad para retroceder a la anterior
        /// </summary>
        /// <returns></returns>

        public OportunidadLogRevertirDTO RevertirFaseOportunidad(int idOportunidad, DateTime? FechaProgramada, string usuario)
        {
            try
            {
                var result = _dapperRepository.QuerySPDapper("[com].[SP_RevertirUltimoCambioFaseEliminarOportunidadLog]", new { IdOportunidad = idOportunidad, Usuario = usuario });
                var oportunidadLog = this.GetBy(x => x.IdOportunidad == idOportunidad && x.Estado == true, x => new OportunidadLogRevertirDTO { Id = x.Id, IdOportunidad = x.IdOportunidad, IdCentroCosto = x.IdCentroCosto, IdPersonalAsignado = x.IdPersonalAsignado, IdFaseOportunidad = x.IdFaseOportunidad, IdTipoDato = x.IdTipoDato, IdContacto = x.IdContacto, FechaLog = x.FechaLog, IdClasificacionPersona = x.IdClasificacionPersona }).OrderByDescending(x => x.FechaLog).FirstOrDefault();

                return oportunidadLog;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<ObtenerDetalleOportunidadDTO> ObtenerDetalleOportunidad(int idOportunidad)
        {
            try
            {
                var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, Estado FROM com.ObtenerDetalleOportunidad WHERE Est = 1 AND IdOportunidad = @idOportunidad ORDER BY Fecha DESC";
                var res = _dapperRepository.QueryDapper(query, new { idOportunidad });
                return JsonConvert.DeserializeObject<List<ObtenerDetalleOportunidadDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<IntDTO> ObtenerDetallePersonalAsignado(int idOportunidad)
        {
            try
            {
                var query = @"SELECT IdPersonal_Asignado AS Valor
                                FROM com.T_OportunidadLog 
                                WHERE IdOportunidad = @IdOportunidad AND Estado  = 1 AND IdPersonal_Asignado IS NOT NULL
                                ORDER BY Fecha_Log ASC";
                var res = _dapperRepository.QueryDapper(query, new { IdOportunidad = idOportunidad });
                return JsonConvert.DeserializeObject<List<IntDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
