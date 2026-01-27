using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AgendaTabRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_AgendaTab
    /// </summary>
    public class AgendaTabRepository : GenericRepository<TAgendaTab>, IAgendaTabRepository
    {
        private Mapper _mapper;

        public AgendaTabRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAgendaTab MapeoEntidad(AgendaTab entidad)
        {
            try
            {
                TAgendaTab modelo = _mapper.Map<TAgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAgendaTab Add(AgendaTab entidad)
        {
            try
            {
                var AgendaTab = MapeoEntidad(entidad);
                base.Insert(AgendaTab);
                return AgendaTab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAgendaTab Update(AgendaTab entidad)
        {
            try
            {
                var AgendaTab = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AgendaTab.RowVersion = entidadExistente.RowVersion;

                base.Update(AgendaTab);
                return AgendaTab;
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
        public IEnumerable<TAgendaTab> Add(IEnumerable<AgendaTab> listadoEntidad)
        {
            try
            {
                List<TAgendaTab> listado = new List<TAgendaTab>();
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
        public IEnumerable<TAgendaTab> Update(IEnumerable<AgendaTab> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAgendaTab> listado = new List<TAgendaTab>();
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AgendaTab.
        /// </summary>
        /// <returns> List<AgendaTabDTO> </returns>
        public IEnumerable<AgendaTabDTO> Obtener()
        {
            try
            {
                List<AgendaTabDTO> rpta = new List<AgendaTabDTO>();
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        VisualizarActividad,
                        CargarInformacionInicial,
                        Numeracion,
                        ValidarFecha,
                        CodigoAreaTrabajo,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion
                    FROM com.T_AgendaTab WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OTR-O-001@Error en Obtener, {ex.Message}");
            }
        }
        

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de las configuraciones de los tabs.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Codigo del Area de Trabajo</param>
        /// <returns> List<AgendaTabConfiguradoDTO> </returns>
        public IEnumerable<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> rpta = new List<AgendaTabConfiguracionAlternoDTO>();
                var query = @"
                    SELECT Id,Nombre,VisualizarActividad,CargarInformacionInicial,VistaBaseDatos,CamposVista,IdTipoCategoriaOrigen,IdCategoriaOrigen,
	                    IdTipoDato,IdFaseOportunidad,IdEstadoOportunidad,Probabilidad
                    FROM com.V_ObtenerTabsAgendaConfigurado
                    WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 and CodigoAreaTrabajo = @codigoAreaTrabajo";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OTC-001@Error en ObtenerTabsConfigurados, {ex.Message}");
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de las configuraciones de los tabs por el idtab. 
        /// </summary>
        /// <param name="codigoAreaTrabajo">Codigo del Area de Trabajo</param>
        /// <param name="idTab">Id Agenda Tab</param>
        /// <returns> List<AgendaTabConfiguradoDTO> </returns>
        public IEnumerable<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> rpta = new();
                var query = @"
                            SELECT
	                            Id,
	                            Nombre,
	                            VisualizarActividad,
	                            CargarInformacionInicial,
	                            VistaBaseDatos,
	                            CamposVista,
	                            IdTipoCategoriaOrigen,
	                            IdCategoriaOrigen,
	                            IdTipoDato,
	                            IdFaseOportunidad,
	                            IdEstadoOportunidad,
	                            Probabilidad
                            FROM com.V_ObtenerTabsAgendaConfigurado
                            WHERE
	                            EstadoAgendaTab = 1
	                            AND EstadoTabConfiguracion = 1
	                            AND CodigoAreaTrabajo = @codigoAreaTrabajo
	                            AND Id = @idTab;";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo, idTab });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerTabsConfiguradosPorIdTab()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerActividadesProgramada(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaDTO> rpta = new();
                string filtro = ObtenerFiltro(filtros);
                filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                string condicionEstadoOcurrencia = string.Empty;
                string condicionPersonalAsignado = string.Empty;
                int idOportunidadRemarketingAgenda;
                bool condicionWhatsaap = false;

                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        condicionWhatsaap = valor == "SI";
                    }
                }
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
                condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor})" : string.Empty;

                string query = $@"
                    SELECT {tabAgenda.CamposVista}
                    FROM (
                        SELECT *
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            {condicionEstadoOcurrencia} {condicionPersonalAsignado}
                        UNION
                        SELECT *
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda} 
                            {condicionPersonalAsignado}
                    ) AS T0
                    
                    ";
                if (condicionWhatsaap)
                {
                    query += string.IsNullOrEmpty(filtro) ? "WHERE EstadoSeguimientoWhatsApp <> 1" : $" WHERE EstadoSeguimientoWhatsApp <> 1 AND {filtro.Trim()}";
                }
                else
                {
                    query += string.IsNullOrEmpty(filtro) ? string.Empty : $"WHERE {filtro.Trim()}";
                }

                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerActividadesProgramada()", ex);
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 10/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaV2DTO></returns>
        public List<ActividadAgendaV2DTO> ObtenerActividadesProgramadaV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaV2DTO> rpta = new();
                string filtro = ObtenerFiltro(filtros);
                filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                string condicionEstadoOcurrencia = string.Empty;
                string condicionPersonalAsignado = string.Empty;
                int idOportunidadRemarketingAgenda;
                bool condicionWhatsaap = false;

                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        condicionWhatsaap = valor == "SI";
                    }
                }
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
                condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor})" : string.Empty;

                string query = $@"
                    SELECT {tabAgenda.CamposVista}
                    FROM (
                        SELECT *
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            AND IdOportunidadRemarketingAgenda IS NULL
                            {condicionEstadoOcurrencia} {condicionPersonalAsignado}
                        UNION
                        SELECT *
                        FROM {tabAgenda.VistaBaseDatos}
                        WHERE IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda} 
                            {condicionPersonalAsignado}
                    ) AS T0
                    
                    ";
                if (condicionWhatsaap)
                {
                    query += string.IsNullOrEmpty(filtro) ? "WHERE EstadoSeguimientoWhatsApp <> 1" : $" WHERE EstadoSeguimientoWhatsApp <> 1 AND {filtro.Trim()}";
                }
                else
                {
                    query += string.IsNullOrEmpty(filtro) ? string.Empty : $"WHERE {filtro.Trim()}";
                }

                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaV2DTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerActividadesProgramada()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene actividades no programadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerActividadesNoProgramada(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaDTO> rpta = new();
                string filtro = ObtenerFiltro(filtros);
                filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                string condicion = string.Empty;
                string condicionPersonalAsignado = string.Empty;
                string queryAdicional = string.Empty;
                int idOportunidadRemarketingAgenda;
                condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor}) " : string.Empty;


                bool condicionWhatsaap = false;
                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        condicionWhatsaap = valor == "SI";
                    }
                }

                if (tabAgenda.Nombre.Contains("1 Solicitud"))
                {
                    idOportunidadRemarketingAgenda = 2;
                    condicion = " AND Total = 1 AND IdEstadoOportunidad != 8";
                    //condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor})" : string.Empty;
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
                            AND FechaCreacion > FechaMenos_30 {condicionPersonalAsignado}";
                }

                string query = $@"
                    SELECT {tabAgenda.CamposVista}
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
                                {condicionPersonalAsignado}
                            UNION
                            SELECT *
                            FROM {tabAgenda.VistaBaseDatos}
                            WHERE
                                IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda}
                                AND FechaCreacion > FechaMenos_30
                                {condicionPersonalAsignado}
                            {queryAdicional}  
                        ) AS T0
                        ";
                if (condicionWhatsaap)
                {
                    query += string.IsNullOrEmpty(filtro) ? "WHERE EstadoSeguimientoWhatsApp <> 1" : $" WHERE EstadoSeguimientoWhatsApp <> 1 AND {filtro.Trim()} ORDER BY Orden ASC";
                }
                else
                {
                    query += string.IsNullOrEmpty(filtro) ? string.Empty : $" WHERE {filtro.Trim()} ORDER BY Orden ASC";
                }


                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerActividadesNoProgramada() {ex.Message}", ex);
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 10/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene actividades no programadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaV2DTO></returns>
        public List<ActividadAgendaV2DTO> ObtenerActividadesNoProgramadaV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaV2DTO> rpta = new();
                string filtro = ObtenerFiltro(filtros);
                filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                string condicion = string.Empty;
                string condicionPersonalAsignado = string.Empty;
                string queryAdicional = string.Empty;
                int idOportunidadRemarketingAgenda;
                condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor}) " : string.Empty;


                bool condicionWhatsaap = false;
                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        condicionWhatsaap = valor == "SI";
                    }
                }

                if (tabAgenda.Nombre.Contains("1 Solicitud"))
                {
                    idOportunidadRemarketingAgenda = 2;
                    condicion = " AND Total = 1 AND IdEstadoOportunidad != 8";
                    //condicionPersonalAsignado = idAsesor > 0 ? $" AND IdPersonal_Asignado IN ({idAsesor})" : string.Empty;
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
                            AND FechaCreacion > FechaMenos_30 {condicionPersonalAsignado}";
                }

                string query = $@"
                    SELECT {tabAgenda.CamposVista}
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
                                {condicionPersonalAsignado}
                            UNION
                            SELECT *
                            FROM {tabAgenda.VistaBaseDatos}
                            WHERE
                                IdOportunidadRemarketingAgenda = {idOportunidadRemarketingAgenda}
                                AND FechaCreacion > FechaMenos_30
                                {condicionPersonalAsignado}
                            {queryAdicional}  
                        ) AS T0
                        ";
                if (condicionWhatsaap)
                {
                    query += string.IsNullOrEmpty(filtro) ? "WHERE EstadoSeguimientoWhatsApp <> 1" : $" WHERE EstadoSeguimientoWhatsApp <> 1 AND {filtro.Trim()} ORDER BY Orden ASC";
                }
                else
                {
                    query += string.IsNullOrEmpty(filtro) ? string.Empty : $" WHERE {filtro.Trim()} ORDER BY Orden ASC";
                }


                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaV2DTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerActividadesNoProgramada() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades con mensajes recibidos
        /// </summary>
        /// <param name="idPersonal">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerMensajesRecibidosComercial(int idPersonal)
        {
            try
            {
                List<ActividadAgendaDTO> rpta = new();
                string resultado = _dapperRepository.QuerySPDapper("[com].[SP_Agenda_MensajesRecibidos]", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerMensajesRecibidosComercial()", ex);
            }
        }



        /// Autor: Joseph Llanque
        /// Fecha: 30/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades con chats pendientes del portal
        /// </summary>
        /// <param name="idPersonal">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerMensajesRecibidosChatPortal(int idPersonal)
        {
            try
            {
                List<ActividadAgendaDTO> rpta = new();
                string resultado = _dapperRepository.QuerySPDapper("[com].[SP_Agenda_MensajesRecibidosChatPortal]", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerMensajesRecibidosComercial()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/12/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener las actividades con mensajes recibidos
        /// </summary>
        /// <param name="idPersonal">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerCorreosAgendaComercial(int idPersonal)
        {
            try
            {
                List<ActividadAgendaDTO> rpta = new();
                string resultado = _dapperRepository.QuerySPDapper("[com].[SP_Agenda_CorreosRecibidos]", new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerCorreosAgendaComercial()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerActividadesOperaciones(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = tabAgenda.Nombre == "Solicitud Cambio" ? obtenerFiltroSolicitudes(filtros) : this.ObtenerFiltro(filtros);
                //var filtro = this.ObtenerFiltro(filtros);

                var queryConIdAsesor = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                var queryConFiltros = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                var actividadesDB = _dapperRepository.QueryDapper(query, new { });
                actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);

                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Alexis Arroyo
        /// Fecha: 26/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene solicitudes agrupadas por asesor (Derivadas y Resueltas)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerActividadesSolicitudesAgrupadas(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                const int ID_AREA_ATENCION_CLIENTE = 3;
                var estadosResueltos = new List<int> { 7, 8 };

                // 1. Construir parámetros para el SP
                int? idPersonalRevision = idAsesor > 0 ? idAsesor : (int?)null;
                //string? idEstadoSolicitud = null;
                //DateTime? fechaInicio = filtros != null && filtros.ContainsKey("FechaInicio") && !string.IsNullOrEmpty(filtros["FechaInicio"])
                //    ? DateTime.Parse(filtros["FechaInicio"]).Date
                //    : (DateTime?)null;
                //DateTime? fechaFin = filtros != null && filtros.ContainsKey("FechaFin") && !string.IsNullOrEmpty(filtros["FechaFin"])
                //    ? DateTime.Parse(filtros["FechaFin"]).Date.AddDays(1).AddSeconds(-1)
                //    : (DateTime?)null;

                // 2. Llamar al SP de SolicitudAlumno
                List<SolicitudAlumnoFiltradaDTO> todasLasSolicitudes = new();
                var resultado = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudAlumnoPorAsesor",
                    new { IdPersonalRevision = idPersonalRevision});

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    todasLasSolicitudes = JsonConvert.DeserializeObject<List<SolicitudAlumnoFiltradaDTO>>(resultado);
                }

                // 3. Filtrar y agrupar solicitudes
                var solicitudesDerivadas = todasLasSolicitudes
                    .Where(s => !estadosResueltos.Contains(s.IdEstadoSolicitud) && s.IdAreaSolucion == ID_AREA_ATENCION_CLIENTE)
                    .ToList();

                var solicitudesResueltas = todasLasSolicitudes
                    .Where(s => estadosResueltos.Contains(s.IdEstadoSolicitud) && s.IdAreaSolucion != ID_AREA_ATENCION_CLIENTE)
                    .ToList();

                // 4. Mapear SolicitudAlumnoFiltradaDTO → ActividadAgendaDTO
                List<ActividadAgendaDTO> actividades = new();

                actividades.AddRange(solicitudesDerivadas.Select(s => MapearSolicitudAActividad(s, "Derivada")));
                actividades.AddRange(solicitudesResueltas.Select(s => MapearSolicitudAActividad(s, "Resuelta")));

                return actividades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Mapea un objeto SolicitudAlumnoFiltradaDTO a ActividadAgendaDTO
        /// </summary>
        private ActividadAgendaDTO MapearSolicitudAActividad(SolicitudAlumnoFiltradaDTO solicitud, string tipoSolicitud)
        {
            return new ActividadAgendaDTO
            {
                // Campos base existentes
                Id = solicitud.id,
                IdMatriculaCabecera = solicitud.IdMatriculaCabecera,
                CodigoMatricula = solicitud.CodigoMatricula,
                IdEstadoMatricula = solicitud.IdEstadoMatricula,
                EstadoMatricula = solicitud.EstadoMatricula,
                SubEstadoMatricula = solicitud.SubEstadoMatricula,
                Contacto = solicitud.NombreAlumno,
                IdAlumno = solicitud.IdAlumno ?? 0,
                IdOportunidad = solicitud.IdOportunidad ?? 0,
                IdFaseOportunidad = solicitud.IdFaseOportunidad,
                IdPadre = solicitud.IdPadre,
                IdActividadCabecera = solicitud.IdActividadCabecera ?? 0,
                UltimaFechaProgramada = !string.IsNullOrEmpty(solicitud.UltimaFechaProgramada)
                    ? DateTime.Parse(solicitud.UltimaFechaProgramada)
                    : (DateTime?)null,
                IdClasificacionPersona = solicitud.IdClasificacionPersona ?? 0,
                PEspecifico = solicitud.NombrePEspecifico,
                IdCentroCosto = solicitud.IdCentroCosto,
                CentroCosto = solicitud.CentroCosto,
                UltimoComentario = solicitud.DetalleSolicitud,
                TipoSolicitudOperaciones = tipoSolicitud,
                Email1 = solicitud.Email,
                Asesor = solicitud.PersonalRevision,
                IdPersonal_Asignado = solicitud.IdPersonalRevision,
                EstadoHoja = solicitud.EstadoSolicitud,
                Origen = solicitud.TipoSolicitud,
                CategoriaNombre = solicitud.NombreSolicitudCategoria,
                CategoriaDescripcion = solicitud.NombreSubCategoria,
                FechaSolicitud = !string.IsNullOrEmpty(solicitud.FechaRegistro)
                    ? DateTime.Parse(solicitud.FechaRegistro)
                    : (DateTime?)null,

                // Campos de Programa
                IdPEspecifico = solicitud.IdPEspecifico,
                IdPGeneral = solicitud.IdPGeneral,
                PGeneral = solicitud.PGeneral,

                // Campos de Solicitud
                Prioridad = solicitud.Prioridad,
                NombreSolicitud = solicitud.NombreSolicitud,
                IdTipoReporte = solicitud.IdTipoReporte,
                TipoReporte = solicitud.Tipo,
                IdSolicitudCategoria = solicitud.IdSolicitudCategoria,
                IdSubCategoria = solicitud.IdSubCategoria,
                IdEstadoSolicitud = solicitud.IdEstadoSolicitud,

                // Campos de Solicitante
                IdSolicitante = solicitud.IdSolicitante,
                NombreSolicitante = solicitud.NombreSolicitante,
                IdAreaSolicitante = solicitud.IdAreaSolicitante,
                AreaSolicitante = solicitud.AreaSolicitante,

                // Campos de Revisión
                IdAreaRevision = solicitud.IdAreaRevision,
                AreaRevision = solicitud.AreaRevision,
                NombreArchivoSolicitante = solicitud.NombreArchivoSolicitante,

                // Campos de Solución
                IdAreaSolucion = solicitud.IdAreaSolucion,
                AreaSolucion = solicitud.AreaSolucion,
                IdPersonalSolucion = solicitud.IdPersonalSolucion,
                PersonalSolucion = solicitud.PersonalSolucion,
                ComentarioSolucion = solicitud.ComentarioSolucion,
                NombreArchivoSolucion = solicitud.NombreArchivoSolucion,

                // Otros campos
                FechaModificacionSolicitud = !string.IsNullOrEmpty(solicitud.FechaModificacion)
                    ? DateTime.Parse(solicitud.FechaModificacion)
                    : (DateTime?)null,
                IdControlSolicitudOrigen = solicitud.IdControlSolicitudOrigen,
                ControlSolicitudOrigen = solicitud.ControlSolicitudOrigen
            };
        }
        /// Autor: Jose Vega
        /// Fecha: 10/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaV2DTO></returns>
        public List<ActividadAgendaV2DTO> ObtenerActividadesOperacionesV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaV2DTO> actividadesAgenda = new List<ActividadAgendaV2DTO>();
                var query = string.Empty;
                var filtro = tabAgenda.Nombre == "Solicitud Cambio" ? obtenerFiltroSolicitudes(filtros) : this.ObtenerFiltro(filtros);
                //var filtro = this.ObtenerFiltro(filtros);

                var queryConIdAsesor = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                var queryConFiltros = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                var actividadesDB = _dapperRepository.QueryDapper(query, new { });
                actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaV2DTO>>(actividadesDB);

                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de la matricula enviada.
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>List<ActividadAgendaDTO></returns>
        public List<ActividadAgendaDTO> ObtenerActividadesOperacionesFichaChat(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, int idMatriculaCabecera)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();

                var query = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") AND IdMatriculaCabecera IN (" + idMatriculaCabecera+")";
                var actividadesDB = _dapperRepository.QueryDapper(query, new { });
                actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);

                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de actividades por tab de operaciones
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>int</returns>
        public int CantidadActividadesPorTabOperaciones(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                IntDTO rpta = new();
                string filtro = ObtenerFiltroCantidad(filtros);
                string condicion = idAsesor != 0 ? $" AND IdPersonal_Asignado IN ({idAsesor})" : string.Empty;
                string query = $@"SELECT COUNT(*) AS Valor FROM {tabAgenda.VistaBaseDatos} WHERE IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad}) {condicion} {filtro}";
                string resultado = _dapperRepository.FirstOrDefault(query, new { });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                }
                return rpta.Valor.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método CantidadActividadesPorTabOperaciones()", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>string</returns>
        public List<ActividadAgendaDTO> ObtenerActividades(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new();
                var condicionProbabilidad = string.Empty;
                var condicionAsesor = idAsesor == 0 ? string.Empty : $" AND IdPersonal_Asignado IN ({idAsesor})";
                var filtro = this.ObtenerFiltro(filtros);
                string condicionWhatsaap = string.Empty;

                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        if (valor == "SI")
                        {
                            condicionWhatsaap = "EstadoSeguimientoWhatsApp <> 1 AND";
                        }
                    }
                }
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                var query = @$"SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                        WHERE 
                            {condicionWhatsaap} 
                            IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            {condicionProbabilidad}
                            {condicionAsesor}
                            {filtro}
                        ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(resultado)!;
                }
                return actividadesAgenda;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OA-001@Error en ObtenerActividades {ex.Message}");
            }
        }
        /// Autor: Jose Vega
        /// Fecha: 10/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>string</returns>
        public List<ActividadAgendaV2DTO> ObtenerActividadesV2(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string>? filtros)
        {
            try
            {
                List<ActividadAgendaV2DTO> actividadesAgenda = new();
                var condicionProbabilidad = string.Empty;
                var condicionAsesor = idAsesor == 0 ? string.Empty : $" AND IdPersonal_Asignado IN ({idAsesor})";
                var filtro = this.ObtenerFiltro(filtros);
                string condicionWhatsaap = string.Empty;

                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        if (valor == "SI")
                        {
                            condicionWhatsaap = "EstadoSeguimientoWhatsApp <> 1 AND";
                        }
                    }
                }
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad}) ";
                }
                var query = @$"SELECT {tabAgenda.CamposVista} FROM {tabAgenda.VistaBaseDatos}
                        WHERE 
                            {condicionWhatsaap} 
                            IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            {condicionProbabilidad}
                            {condicionAsesor}
                            {filtro}
                        ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaV2DTO>>(resultado)!;
                }
                return actividadesAgenda;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OA-001@Error en ObtenerActividades {ex.Message}");
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cantidad de actividades por tab
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo AgendaTabConfiguracionAlternoDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>string</returns>
        public int CantidadActividadesPorTab(AgendaTabConfiguracionAlternoDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                var cantidad = new Dictionary<string, int>();
                var filtro = this.ObtenerFiltroCantidad(filtros);

                string condicionWhatsaap = string.Empty;

                if (filtros != null)
                {
                    KeyValuePair<string, string>? filtroTemp = filtros.FirstOrDefault(x => x.Key.ToLower() == "eswhatsapp");
                    if (filtroTemp.HasValue)
                    {
                        string valor = filtroTemp.Value.Value;
                        if (valor == "SI")
                        {
                            condicionWhatsaap = "EstadoSeguimientoWhatsApp <> 1 AND ";
                        }
                    }
                }

                var condicionAsesor = idAsesor == 0 ? string.Empty : $" AND IdPersonal_Asignado IN ({idAsesor}) ";
                var condicionProbabilidad = string.Empty;
                if (!tabAgenda.Probabilidad.Contains("0"))
                {
                    condicionProbabilidad = $" AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})";
                }

                var query = @$"
                        SELECT COUNT(Id) AS Cantidad 
                        FROM {tabAgenda.VistaBaseDatos} 
                        WHERE 
                            {condicionWhatsaap} 
                            IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen}) 
                            AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                            AND IdTipoDato IN ({tabAgenda.IdTipoDato}) 
                            AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                            AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                            {condicionProbabilidad} 
                            {condicionAsesor} 
                            {filtro}";
                var resultado = _dapperRepository.FirstOrDefault(query, new { });
                cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado)!;

                return cantidad.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades realizadas con los filtros enviados
        /// </summary>
        /// <param name="idsAsesor">Id de los asesores (PK de la tabla gp.T_Personal)</param>
        /// <param name="fecha">Cadena con la fecha para el SP</param>
        /// <param name="idCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idFaseOportunidad">Id de la fase de la oportunidad(PK de la tabla pla.T_FaseOportunidad)</param>
        /// <param name="idTipoDato">Id del tipo de dato (PK de la tabla mkt.T_TipoDato)</param>
        /// <param name="idOrigen">Id del origen del dato (PK de la tabla mkt.T_Origen)</param>
        /// <param name="take">Cantidad de datos para la paginacion</param>
        /// <param name="skip">Limite minimo para la paginacion</param>
        /// <param name="idsCategoriaOrigen">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="idProbabilidad">Id de la probilidad (PK de la tabla mkt.T_ProbabilidadRegistro_PW)</param>
        /// <param name="idEstado">Id del estado de la oportunidad(PK de la tabla com.T_EstadoOportunidad)</param>
        /// <returns>List<PruebaActividadRealizadaDTO></returns>
        public List<PruebaActividadRealizadaDTO> ObtenerActividadesRealizadasSP(string idsAsesor, string fecha, int idCentroCosto, int idAlumno, int idFaseOportunidad, int idTipoDato, int idOrigen, int take, int skip, string idsCategoriaOrigen, int idProbabilidad, int idEstado)
        {
            try
            {
                List<PruebaActividadRealizadaDTO> actividadesRealizadas = new List<PruebaActividadRealizadaDTO>();
                take = take == 0 ? 20000 : take;
                idsCategoriaOrigen = idsCategoriaOrigen ?? "_";
                var actividadesDB = _dapperRepository.QuerySPDapper("com.SP_ObtenerRealizadasV2NuevoModelo", new { IdAsesor = idsAsesor, Fecha = fecha, IdCentroCosto = idCentroCosto, IdAlumno = idAlumno, IdFaseOportunidad = idFaseOportunidad, IdTipoDato = idTipoDato, IdOrigen = idOrigen, Take = take, Skip = skip, IdCategoriaOrigen = idsCategoriaOrigen, IdProbabilidad = idProbabilidad, EstadoFilter = idEstado });
                actividadesRealizadas = JsonConvert.DeserializeObject<List<PruebaActividadRealizadaDTO>>(actividadesDB);
                return actividadesRealizadas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Transforma los el diccionario en filtros para Querys de Base de Datos
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>string</returns>
        private string ObtenerFiltro(Dictionary<string, string>? filtros)
        {
            try
            {
                string filtroVista = string.Empty;
                string skip = "";
                string take = "";
                if (filtros != null && filtros.Count() > 0)
                {
                    Dictionary<string, string> filtroTemp = filtros.Where(x => x.Key.ToLower() != "eswhatsapp").ToDictionary(s => s.Key, s => s.Value);

                    foreach (var prop in filtroTemp)
                    {
                        if (prop.Key.ToLower().Equals("take") || prop.Key.ToLower().Equals("page") || prop.Value == null || prop.Value.Equals("") || prop.Value.Length <= 0)
                        {
                            continue;
                        }
                        if (prop.Key.Equals("skip"))
                        {
                            skip = prop.Value;
                            continue;
                        }
                        if (prop.Key.ToLower().Equals("pageSize".ToLower()))
                        {
                            take = prop.Value;
                            continue;
                        }
                        if (prop.Key.ToLower().Equals("IdProbabilidadRegistroPW".ToLower()))
                        {
                            filtroVista += $" AND {prop.Key} = {prop.Value}";
                            continue;
                        }
                        if (prop.Key.ToLower().Equals("codigoMatricula".ToLower()) || prop.Key.ToLower().Equals("dni"))
                        {
                            filtroVista += $" AND {prop.Key} LIKE  '%{prop.Value}%'";
                            continue;
                        }
                        if (prop.Key.ToLower().Equals("FechaLlamada".ToLower()))
                        {
                            filtroVista += $" AND {prop.Key} >= CONVERT(DATETIME, '{prop.Value}', 101) AND {prop.Key} <= CONVERT(DATETIME, DATEADD(DAY, 1, CONVERT(DATE, '{prop.Value}')), 101)";
                            continue;
                        }
                        if (prop.Value.Contains(","))
                        {
                            filtroVista += $" AND {prop.Key} IN ({prop.Value})";
                        }
                        else
                        {
                            filtroVista += $" AND {prop.Key} = {prop.Value}";
                        }
                    }
                    if (skip != "" && take != "")
                    {
                        filtroVista += $" ORDER BY UltimaFechaProgramada ASC OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;";
                    }
                }
                return filtroVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string obtenerFiltroSolicitudes(Dictionary<string, string> filtros)
        {
            try
            {
                var filtroVista = string.Empty;
                var skip = "";
                var take = "";
                foreach (var prop in filtros)
                {
                    if (prop.Key.Equals("take") || prop.Key.Equals("page") || prop.Key.Equals("sort") || prop.Value.Equals("") || prop.Value == null || prop.Value.Length <= 0)
                    {
                        continue;
                    }
                    if (prop.Key.Equals("skip"))
                    {
                        skip = prop.Value;
                        continue;
                    }
                    if (prop.Key.Equals("pageSize"))
                    {
                        take = prop.Value;
                        continue;
                    }
                    if (prop.Key.Equals("IdProbabilidadRegistroPW"))
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                        continue;
                    }
                    if (prop.Key.Equals("codigoMatricula") || prop.Key.Equals("dni"))
                    {
                        filtroVista += " AND " + prop.Key + " Like  '%" + prop.Value + "%'";
                        continue;
                    }
                    if (prop.Key.Equals("FechaLlamada"))
                    {
                        filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "   <= convert(datetime, DATEADD(DAY, 1, Convert(date, '" + prop.Value + "')), 101)";
                        continue;
                    }
                    if (prop.Value.Contains(","))
                    {
                        filtroVista += " AND " + prop.Key + " IN (" + prop.Value + ")";
                    }
                    else
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                    }
                }
                if (skip != "" && take != "")
                {
                    filtros.TryGetValue("sort", out var aux);
                    if (aux != null)
                    {
                        filtroVista += " ORDER BY FechaSolicitud " + aux + " OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY;";
                    }
                    else
                    {
                        filtroVista += " ORDER BY FechaSolicitud ASC OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY;";
                    }
                }
                return filtroVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la query para la peticion a la DB
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>string</returns>
        private string ObtenerFiltroCantidad(Dictionary<string, string> filtros)
        {
            try
            {
                var filtroVista = string.Empty;
                Dictionary<string, string> filtroTemp = filtros.Where(x => x.Key.ToLower() != "eswhatsapp").ToDictionary(s => s.Key, s => s.Value);

                foreach (var prop in filtroTemp)
                {
                    if (prop.Key.Equals("skip") || prop.Key.Equals("pageSize") || prop.Key.Equals("take") || prop.Key.Equals("sort") || prop.Key.Equals("page") || prop.Value.Equals("") || prop.Value == null || prop.Value.Length <= 0)
                    {
                        continue;
                    }
                    if (prop.Key.Equals("IdProbabilidadRegistroPW"))
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                        continue;
                    }
                    if (prop.Key.Equals("codigoMatricula") || prop.Key.Equals("dni"))
                    {
                        filtroVista += " AND " + prop.Key + " Like '%" + prop.Value + "%'";
                        continue;
                    }
                    if (prop.Key.Equals("FechaLlamada"))
                    {
                        filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "   <= convert(datetime, DATEADD(DAY, 1, Convert(date, '" + prop.Value + "')), 101)";
                        continue;
                    }
                    if (prop.Value.Contains(","))
                    {
                        filtroVista += " AND " + prop.Key + " IN (" + prop.Value + ")";
                    }
                    else
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                    }
                }
                return filtroVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los tabs de la agenda que no requieren validacion
        /// </summary>
        /// <param name="codigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo AgendaTabConfiguradoDTO</returns>
        public List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosSinValidacion(string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> rpta = new List<AgendaTabConfiguracionAlternoDTO>();
                var query = @"SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos,
                            CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad,
                            IdEstadoOportunidad, Probabilidad, Numeracion, ValidarFecha 
                            FROM com.V_ObtenerTabsAgendaConfigurado 
                            WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 AND Numeracion = 0 
                            and CodigoAreaTrabajo=@CodigoAreaTrabajo";
                var resultado = _dapperRepository.QueryDapper(query, new { CodigoAreaTrabajo = codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OTCSV-001@Error en ObtenerTabsConfiguradosSinValidacion(), {ex.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tabs de la agenda que requieren validacion
        /// </summary>
        /// <param name="codigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo AgendaTabConfiguradoDTO</returns>
        public List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosConValidacion(string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> rpta = new List<AgendaTabConfiguracionAlternoDTO>();
                var query = @"SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos,
                            CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad,
                            IdEstadoOportunidad, Probabilidad, Numeracion, ValidarFecha 
                            FROM com.V_ObtenerTabsAgendaConfigurado WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 AND Numeracion > 0 
                            and CodigoAreaTrabajo=@CodigoAreaTrabajo";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OTCCV-001@Error en ObtenerTabsConfiguradosConValidacion(), {ex.Message}");
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 08/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tabs de la agenda que requieren validacion
        /// </summary>
        /// <param name="codigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo AgendaTabConfiguradoDTO</returns>
        public List<AgendaTabConfiguracionAlternoDTO> ObtenerTabsConfiguradosConValidacionMarcador(string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionAlternoDTO> rpta = new List<AgendaTabConfiguracionAlternoDTO>();
                var query = @"SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos,
                            CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad,
                            IdEstadoOportunidad, Probabilidad, Numeracion, ValidarFecha 
                            FROM com.V_ObtenerTabsAgendaConfigurado 
                            WHERE  EstadoAgendaTab = 1 
                                AND EstadoTabConfiguracion = 1 
                                AND Numeracion > 0 
                                AND CodigoAreaTrabajo=@CodigoAreaTrabajo 
                                AND AplicaMarcadorPredictivo=1";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionAlternoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#ATR-OTCCV-001@Error en ObtenerTabsConfiguradosConValidacionMarcador(), {ex.Message}");
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tabs de la agenda de atencion al cliente
        /// </summary>
        /// <returns> List<AgendaTabComboDTO> </returns>
        public List<ComboDTO> CombosTabsAtencionAlCliente()
        {
            try
            {
                List<ComboDTO> tabsAgenda = new List<ComboDTO>();
                var query = @"
                            SELECT Id, Nombre FROM com.T_AgendaTab WHERE Estado = 1 AND CodigoAreaTrabajo = 'OP'";
                var tabsAgendaDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(tabsAgendaDB) && !tabsAgendaDB.Contains("[]"))
                {
                    tabsAgenda = JsonConvert.DeserializeObject<List<ComboDTO>>(tabsAgendaDB)!;
                }
                return tabsAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de la tabla T_AgendaTab asociados a un Id.
        /// </summary>
        /// <param name="id"> id de AgentaTab </param>
        /// <returns> Entidad AgendaTab </returns>
        public AgendaTab ObtenerPorId(int id)
        {
            try
            {
                var rpta = new AgendaTab();
                var query = @"SELECT Id,
                                       Nombre,
                                       VisualizarActividad,
                                       CargarInformacionInicial,
                                       Numeracion,
                                       ValidarFecha,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       CodigoAreaTrabajo,
                                       Ponderacion,
                                       AplicaMarcadorPredictivo
                                FROM com.T_AgendaTab
                                WHERE Estado = 1
                                      AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<AgendaTab>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
