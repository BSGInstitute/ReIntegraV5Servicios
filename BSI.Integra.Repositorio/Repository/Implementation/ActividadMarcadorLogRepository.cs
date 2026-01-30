using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ActividadMarcadorLogRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ActividadMarcadorLog
    /// </summary>
    public class ActividadMarcadorLogRepository : GenericRepository<TActividadMarcadorLog>, IActividadMarcadorLogRepository
    {
        private Mapper _mapper;

        public ActividadMarcadorLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadMarcadorLog, ActividadMarcadorLog>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TActividadMarcadorLog MapeoEntidad(ActividadMarcadorLog entidad)
        {
            try
            {
                TActividadMarcadorLog modelo = _mapper.Map<TActividadMarcadorLog>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<TActividadMarcadorLog> MapeoEntidad(IEnumerable<ActividadMarcadorLog> entidad)
        {
            try
            {
                IEnumerable<TActividadMarcadorLog> modelo = _mapper.Map<IEnumerable<TActividadMarcadorLog>>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadMarcadorLog Add(ActividadMarcadorLog entidad)
        {
            try
            {
                var ActividadMarcadorLog = MapeoEntidad(entidad);
                Insert(ActividadMarcadorLog);
                return ActividadMarcadorLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadMarcadorLog AddAsync(ActividadMarcadorLog entidad)
        {
            try
            {
                var ActividadMarcadorLog = MapeoEntidad(entidad);
                base.InsertAsync(ActividadMarcadorLog);
                return ActividadMarcadorLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadMarcadorLog Update(ActividadMarcadorLog entidad)
        {
            try
            {
                var ActividadMarcadorLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ActividadMarcadorLog.RowVersion = entidadExistente.RowVersion;

                Update(ActividadMarcadorLog);
                return ActividadMarcadorLog;
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

        public IEnumerable<TActividadMarcadorLog> Add(IEnumerable<ActividadMarcadorLog> listadoEntidad)
        {
            try
            {
                IEnumerable<TActividadMarcadorLog> listado = MapeoEntidad(listadoEntidad);
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TActividadMarcadorLog> Update(IEnumerable<ActividadMarcadorLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                IEnumerable<TActividadMarcadorLog> listado = MapeoEntidad(listadoEntidad);

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ActividadMarcadorLog por idActividadDetalle, idOportunidad
        /// </summary>
        /// <param name="idActividadDetalle">Id Actividad Detalle</param>
        /// <param name="idOportunidad">Id Oportunidad</param>
        /// <returns>ActividadMarcadorLog</returns>
        public ActividadMarcadorLog? ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id, 
                        IdOportunidad, 
                        IdActividadDetalle, 
                        FechaProgramada, 
                        TotalIntento, 
                        Contestado, 
                        NoContestado, 
                        IdAgendaTab, 
                        Estado, 
                        UsuarioCreacion, 
                        UsuarioModificacion, 
                        FechaCreacion, 
                        FechaModificacion, 
                        RowVersion, 
                        IdMigracion 
                        FROM com.T_ActividadMarcadorLog
                    WHERE IdActividadDetalle=@idActividadDetalle 
                        AND IdOportunidad=@idOportunidad 
                        AND Estado=1";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idActividadDetalle, idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ActividadMarcadorLog>(resultado)!;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"#@Error en ObtenerPorIdActividadDetalleIdOportunidad(), {ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public List<ActividadAgendaMarcadorDTO>? ObtenerActividadesProgramadaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor)
        {
            try
            {
                string condicionEstadoOcurrencia = string.Empty;
                string condicionMarcador = string.Empty;
                string condicionPersonalAsignado = idAsesor > 0 ? $"" : string.Empty;
                int idOportunidadRemarketingAgenda;
                string condicionActividades = string.Empty;
                if (tabAgenda.Nombre.Contains("Automatica"))
                {
                    condicionEstadoOcurrencia = " AND (IdEstadoOcurrencia = 2 OR IdEstadoOcurrencia IS NULL)";
                    idOportunidadRemarketingAgenda = 14;
                }
                else
                {
                    condicionEstadoOcurrencia = " AND (IdEstadoOcurrencia = 1 OR IdEstadoOcurrencia = 7)";
                    idOportunidadRemarketingAgenda = 1;
                }
                string query = $@"
                    SELECT T0.*,
	                    ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            {condicionEstadoOcurrencia}  AND IdPersonal_Asignado=@idAsesor
                        UNION
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda} 
                            AND IdPersonal_Asignado=@idAsesor
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                    AND AML.IdOportunidad = T0.IdOportunidad
	                    AND AML.Estado = 1
                    WHERE IdOcurrencia NOT IN (238,371,413)";
                string resultado = _dapperRepository.QueryDapper(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ActividadAgendaMarcadorDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAPM-001@Error en ObtenerActividadesProgramadaMarcador()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene actividades no programadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public List<ActividadAgendaMarcadorDTO>? ObtenerActividadesNoProgramadaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor)
        {
            try
            {
                string condicion = string.Empty;
                string queryAdicional = string.Empty;
                int idOportunidadRemarketingAgenda;
                if (tabAgenda.Nombre.Contains("1 Solicitud"))
                {
                    idOportunidadRemarketingAgenda = 2;
                    condicion = " AND Total = 1 ";
                }
                else
                {
                    idOportunidadRemarketingAgenda = 11;
                    condicion = " AND Total > 1 ";
                    queryAdicional = $@"
                        UNION
                        SELECT *
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdEstadoOportunidad = 8
							AND IdOportunidadRemarketingAgenda IS NULL
                            AND FechaCreacion > FechaMenos_30 AND IdPersonal_Asignado IN ({idAsesor})";
                }
                string query = $@"
                    SELECT T0.*,
	                    AML.FechaProgramada AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                        FROM (
                            SELECT *
                            FROM {tabAgenda.VistaBaseDatos}
                            WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})
                                AND IdOportunidadRemarketingAgenda IS NULL
                                AND FechaCreacion > FechaMenos_30
                                {condicion}
                                AND IdPersonal_Asignado IN ({idAsesor}) 
                            UNION
                            SELECT *
                            FROM {tabAgenda.VistaBaseDatos}
                            WHERE
                                IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda}
                                AND FechaCreacion > FechaMenos_30
                                AND IdPersonal_Asignado IN ({idAsesor})
                            {queryAdicional}  
                        ) AS T0
                        LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ActividadAgendaMarcadorDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OANPM-001@Error en ObtenerActividadesNoProgramadaMarcador() {ex.Message}", ex);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public List<ActividadAgendaMarcadorDTO>? ObtenerActividadesMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                var query = @$"
                    SELECT T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                        {condicionProbabilidad}
                        AND IdPersonal_Asignado IN ({idAsesor})

                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ActividadAgendaMarcadorDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAM-001@Error en ObtenerActividadesMarcador {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesIpIcPf(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                if (validarIntento == true)
                {
                    condicionMarcador = @" WHERE (ISNULL(AML.TotalIntento, 0) > 0 
                            OR ISNULL(AML.Contestado, 0) > 0 
                            OR ISNULL(AML.NoContestado, 0) > 0)";
                    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                else
                {
                    condicionMarcador = @" WHERE ISNULL(AML.TotalIntento, 0) = 0";
                    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                        {condicionProbabilidad}
                        AND IdPersonal_Asignado IN ({idAsesor})

                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                    {condicionMarcador} {condicionActividades}";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }


        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de no prog 1 solicituds y no prog mas de una solicitud 
        /// PRIORIDAD 1
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesUnaMasdeUna(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                //if (validarIntento == true)
                //{
                //    condicionMarcador = @" WHERE (ISNULL(AML.TotalIntento, 0) > 0 
                //            OR ISNULL(AML.Contestado, 0) > 0 
                //            OR ISNULL(AML.NoContestado, 0) > 0)";
                //    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                //}
                //else
                //{
                //    condicionMarcador = @" WHERE ISNULL(AML.TotalIntento, 0) = 0";
                //    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                //}
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                   WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                AND IdOportunidadRemarketingAgenda IS NULL
                                AND FechaCreacion > FechaMenos_30
                                AND Total > 0  
                                {condicionProbabilidad}
                                AND IdPersonal_Asignado IN ({idAsesor})
                            UNION
                            SELECT *
                            FROM {tabAgenda.VistaBaseDatos}
                            WHERE
                                IdOportunidadRemarketingAgenda in (2,11)
                                AND FechaCreacion > FechaMenos_30
                                AND IdPersonal_Asignado IN ({idAsesor})
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                   ";
                //AND IdPersonal_Asignado IN({ idAsesor})
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesUnaMAsdeUNaSolicitud {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesIpIcPfWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento,DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                //if (validarIntento == true)
                //{
                //    condicionMarcador = @" WHERE (ISNULL(AML.TotalIntento, 0) > 0 
                //            OR ISNULL(AML.Contestado, 0) > 0 
                //            OR ISNULL(AML.NoContestado, 0) > 0)";
                //    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                //}
                //else
                //{
                //    condicionMarcador = @" WHERE ISNULL(AML.TotalIntento, 0) = 0";
                //    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                //}
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                        {condicionProbabilidad}
                        AND IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada <=  @fechaActual

                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }



        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesAutomaticaMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento)
        {
            try
            {
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                string condicionPersonalAsignado = idAsesor > 0 ? $"" : string.Empty;
                if (validarIntento == true)
                {
                    condicionMarcador = @" AND (ISNULL(AML.TotalIntento, 0) > 0 
                            OR ISNULL(AML.Contestado, 0) > 0 
                            OR ISNULL(AML.NoContestado, 0) > 0)";
                    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                else
                {
                    condicionMarcador = @" AND ISNULL(AML.TotalIntento, 0) = 0";
                    var horaActual = DateTime.Now;
                    var horaCorte = DateTime.Today.AddHours(14);
                    if (horaActual < horaCorte.AddMinutes(1))
                    {
                        condicionMarcador += " AND ActividadesManhana = 0";
                    }
                    else
                    {
                        condicionMarcador += " AND ActividadesTarde = 0";
                    }
                    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }

                string query = $@"
                    SELECT TOP 1 T0.*,
	                    ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            AND (IdEstadoOcurrencia = 2 OR IdEstadoOcurrencia IS NULL)
                            AND IdPersonal_Asignado=@idAsesor
                        UNION
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = 14 
                            AND IdPersonal_Asignado=@idAsesor
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                    AND AML.IdOportunidad = T0.IdOportunidad
	                    AND AML.Estado = 1
                    WHERE IdOcurrencia NOT IN (238,371,413)
                    {condicionMarcador} {condicionActividades}
                    ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAAM-001@Error en ObtenerActividadesAutomaticaMarcador()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesAutomaticaMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                string condicionPersonalAsignado = idAsesor > 0 ? $"" : string.Empty;
                if (validarIntento == true)
                {
                    condicionMarcador = @"(ISNULL(AML.TotalIntento, 0) > 0 
                            OR ISNULL(AML.Contestado, 0) > 0 
                            OR ISNULL(AML.NoContestado, 0) > 0)";
                    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                else
                {
                    condicionMarcador = @"ISNULL(AML.TotalIntento, 0) = 0";
                    var horaActual = DateTime.Now;
                    var horaCorte = DateTime.Today.AddHours(14);
                    if (horaActual < horaCorte.AddMinutes(1))
                    {
                        condicionMarcador += " AND ActividadesManhana = 0";
                    }
                    else
                    {
                        condicionMarcador += " AND ActividadesTarde = 0";
                    }
                    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }

                string query = $@"
                    SELECT TOP 1 T0.*,
	                    ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            AND (IdEstadoOcurrencia = 2 OR IdEstadoOcurrencia IS NULL)
                            AND UltimaFechaProgramada<=@fechaActual
                            AND IdPersonal_Asignado=@idAsesor
                        UNION
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = 14 
                            AND IdPersonal_Asignado=@idAsesor
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                    AND AML.IdOportunidad = T0.IdOportunidad
	                    AND AML.Estado = 1
                    {condicionActividades}
                    ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor, fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAAM-001@Error en ObtenerActividadesAutomaticaMarcador()", ex);
            }
        }


        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesProgramadasManualesMarcador(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool validarIntento)
        {
            try
            {
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                if (validarIntento == true)
                {
                    condicionMarcador = @" AND (ISNULL(AML.TotalIntento, 0) > 0 
                            OR ISNULL(AML.Contestado, 0) > 0 
                            OR ISNULL(AML.NoContestado, 0) > 0)";
                    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                else
                {
                    condicionMarcador = @" AND ISNULL(AML.TotalIntento, 0) = 0";
                    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }

                string query = $@"
                    SELECT TOP 1 T0.*,
	                    ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            AND (IdEstadoOcurrencia = 1 OR IdEstadoOcurrencia = 7)
                            AND IdPersonal_Asignado=@idAsesor
                        UNION
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = 14 
                            AND IdPersonal_Asignado=@idAsesor
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                    AND AML.IdOportunidad = T0.IdOportunidad
	                    AND AML.Estado = 1
                    WHERE IdOcurrencia NOT IN (238,371,413)
                    {condicionMarcador} {condicionActividades}
                    ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAPMM-001@Error en ObtenerActividadesProgramadasManualesMarcador()", ex);
            }
        }



        /// Autor: Joseph Llanque
        /// Fecha: 30/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas manuales
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesProgramadasManualesMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento,DateTime? fechaActual)
        {
            try
            {
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                if (validarIntento == true)
                {
                    condicionMarcador = @"(ISNULL(AML.TotalIntento, 0) > 0 
                            OR ISNULL(AML.Contestado, 0) > 0 
                            OR ISNULL(AML.NoContestado, 0) > 0)";
                    condicionActividades = " ORDER BY ISNULL(AML.TotalIntento, 0) DESC, ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }
                else
                {
                    condicionMarcador = @"ISNULL(AML.TotalIntento, 0) = 0";
                    condicionActividades = " ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC";
                }

                string query = $@"
                    SELECT TOP 1 T0.*,
	                    ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            AND (IdEstadoOcurrencia = 1 OR IdEstadoOcurrencia = 7)
                            AND IdPersonal_Asignado=@idAsesor
                            AND UltimaFechaProgramada<=@fechaActual
                        UNION
                        SELECT {tabAgenda.CamposVista}
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = 14 
                            AND IdPersonal_Asignado=@idAsesor
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                    AND AML.IdOportunidad = T0.IdOportunidad
	                    AND AML.Estado = 1
                    WHERE 
                    {condicionMarcador} {condicionActividades}
                    ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor, fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAPMM-001@Error en ObtenerActividadesProgramadasManualesMarcador()", ex);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 20/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (RN2B)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesRN2BMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {

                List<ActividadAgendaDTO> actividadesAgenda = new();
                var condicionProbabilidad = string.Empty;
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                string condicionPersonalAsignado = idAsesor > 0 ? $"" : string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                string query = $@"SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                                 WHERE 
                                     IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                     AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                     AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                     AND IdFaseOportunidad IN (10,41)
                                     AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                     AND IdPersonal_Asignado=@idAsesor
                                     AND UltimaFechaProgramada<=@fechaActual
                                     {condicionProbabilidad}
                                     {condicionMarcador}
                                     ORDER BY UltimaFechaProgramada ASC
                     ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor, fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAAM-001@Error en ObtenerActividadesAutomaticaMarcador()", ex);
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 20/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (RN2B)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>ActividadAgendaMarcadorDTO</returns>
        public ActividadAgendaMarcadorDTO? ObtenerActividadesRN2AMarcadorWavix(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento)
        {
            try
            {

                List<ActividadAgendaDTO> actividadesAgenda = new();
                var condicionProbabilidad = string.Empty;
                string condicionMarcador = string.Empty;
                string condicionActividades = string.Empty;
                string condicionPersonalAsignado = idAsesor > 0 ? $"" : string.Empty;
                string query = $@"SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                                 WHERE 
                                     IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                     AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                     AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                     AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                     AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                     AND IdPersonal_Asignado=@idAsesor
                                     {condicionProbabilidad}
                     ";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorDTO>(resultado)!;
                    rpta.IdAgendaTabMarcador = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("#AMLR-OAAM-001@Error en ObtenerActividadesAutomaticaMarcador()", ex);
            }
        }







        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad1ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 2
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad2ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new {fechaActual});
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 2
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesClasesOnlineATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 2
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesCompromisosATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAtrasadoMyPATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAtrasadoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoDelDiaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPagoAlDiaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesSeguimientoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPorAbandonarATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPreReporteATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesReporteATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesSinContactoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var horaActual = DateTime.Now;
                var horaCorte = DateTime.Today.AddHours(14);
                if (horaActual < horaCorte.AddMinutes(1))
                {
                    condicionMarcador += " AND ActividadesManhana = 0";
                }
                else
                {
                    condicionMarcador += " AND ActividadesTarde = 0";
                }
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        {condicionMarcador}
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesReservaConDeudaATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 4
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesCulminadoATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public ActividadAgendaMarcadorATCDTO? ObtenerActividadesPrioridad6ATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT TOP 1 T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.FirstOrDefault(query, new { fechaActual});
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<ActividadAgendaMarcadorATCDTO>(resultado)!;
                    rpta.IdTabAgenda = tabAgenda.Id;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 3
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public List<ActividadAgendaMarcadorATCDTO>? ObtenerActividadesMasdeUnaTabATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                        AND UltimaFechaProgramada<=@fechaActual
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.QueryDapper(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<List<ActividadAgendaMarcadorATCDTO>>(resultado)!;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de prioridad 3
        /// </summary>
        /// <param name="tabAgenda">Tab Agenda</param>
        /// <param name="idAsesor">Id filtro asesor</param>
        /// <returns>ActividadAgendaMarcadorATCDTO</returns>
        public List<ActividadAgendaMarcadorATCDTO>? ObtenerActividadesCuotaVenceHoyATC(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, bool? validarIntento, DateTime? fechaActual)
        {
            try
            {
                var condicionProbabilidad = string.Empty;
                var condicionMarcador = string.Empty;
                var condicionActividades = string.Empty;
                var query = @$"
                    SELECT T0.*,
                        ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) AS FechaProgramadaMarcador,
	                    ISNULL(AML.TotalIntento, 0) AS TotalIntento,
	                    ISNULL(AML.Contestado, 0) AS Contestado,
	                    ISNULL(AML.NoContestado, 0) AS NoContestado,
                        AML.IdAgendaTab AS IdAgendaTabMarcador
                    FROM (
                    SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                    WHERE
                        IdPersonal_Asignado IN ({idAsesor})
                    ) AS T0
                    LEFT JOIN com.T_ActividadMarcadorLog AS AML ON AML.IdActividadDetalle = T0.Id
	                        AND AML.IdOportunidad = T0.IdOportunidad
	                        AND AML.Estado = 1
                            ORDER BY ISNULL(AML.FechaProgramada, T0.UltimaFechaProgramada) ASC
                   ";
                //{ condicionMarcador}{ condicionActividades}
                var resultado = _dapperRepository.QueryDapper(query, new { fechaActual });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<List<ActividadAgendaMarcadorATCDTO>>(resultado)!;
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#AMLR-OAIPICPF-001@Error en ObtenerActividadesIpIcPf {ex.Message}");
            }
        }
    }
}

