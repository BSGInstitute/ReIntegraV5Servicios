using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Autor: Jose Vega
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Implementación Dapper del repositorio de agenda de docentes.
    /// Ejecuta consultas de solo lectura sobre múltiples tablas del esquema pla, fin, gp y conf.
    /// NOTA: Verificar nombres de columna en T_Proveedor, T_Personal, T_PEspecifico
    /// contra el esquema real de la base de datos antes de ejecutar en producción.
    /// </summary>
    public class GestionDocenteAgendaRepository : GenericRepository<TProveedor>, IGestionDocenteAgendaRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDapperRepository _dapperRepository;

        public GestionDocenteAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _connectionFactory = connectionFactory;
            _dapperRepository = dapperRepository;
        }

        public DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor)
        {
            try
            {
                DocenteAgendaCabeceraDTO cabecera = null;
                string query = @"
                        SELECT
                            P.Id AS IdProveedor,
                            CASE
                                WHEN LEN(CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)) = 0
                                    THEN P.RazonSocial
                                ELSE CONCAT(P.Nombre1, ' ', P.Nombre2, ' ', P.ApePaterno, ' ', P.ApeMaterno)
                            END AS NombreCompleto,
                            P.Celular1 AS Celular,
                            P.Email,
                            P.IdPersonal_Asignado AS IdPersonalAsignado,
                            ISNULL(CONCAT(PER.Apellidos, ', ', PER.Nombres), '') AS PersonalAsignado,
                            PA.Id AS IdPais,
                            ISNULL(PA.NombrePais, '') AS Pais,
                            ISNULL(C.Nombre, '') AS Ciudad
                        FROM fin.T_Proveedor P
                        LEFT JOIN gp.T_Personal PER ON P.IdPersonal_Asignado = PER.Id
                        LEFT JOIN conf.T_Ciudad C ON P.IdCiudad = C.Id
                        LEFT JOIN conf.T_Pais PA ON C.IdPais = PA.Id
                        WHERE P.Id = @IdProveedor AND P.Estado = 1";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<DocenteAgendaCabeceraDTO>>(resultadoDB);
                    cabecera = lista?.FirstOrDefault();
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocenteAgendaCronogramaDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPrioridad)
        {
            try
            {
                List<DocenteAgendaCronogramaDTO> lista = new List<DocenteAgendaCronogramaDTO>();
                string query = @"
                        SELECT DISTINCT
                            PE.Id AS IdPEspecifico,
                            PE.Nombre AS NombreCurso,
                            PE.Codigo AS CodigoCurso,
                            PE.EstadoP AS EstadoCurso,
                            PE.Tipo AS TipoCurso,
                            PE.Categoria AS CategoriaCurso,
                            PE.Ciudad AS CiudadCurso,
                            PE.FechaInicio,
                            PE.FechaTermino,
                            CASE WHEN PE.Id = @IdPEspecificoPrioridad THEN 1 ELSE 0 END AS EsPriorizado
                        FROM pla.T_PEspecificoSesion S
                        INNER JOIN pla.T_PEspecifico PE ON S.IdPEspecifico = PE.Id
                        WHERE S.IdProveedor = @IdProveedor AND S.Estado = 1 AND PE.Estado = 1
                        ORDER BY EsPriorizado DESC, PE.FechaInicio DESC";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor, IdPEspecificoPrioridad = idPEspecificoPrioridad });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteAgendaCronogramaDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocenteAgendaSesionDTO> ObtenerSesionesPorCursoYDocente(int idProveedor, int idPEspecifico)
        {
            try
            {
                List<DocenteAgendaSesionDTO> lista = new List<DocenteAgendaSesionDTO>();
                string query = @"
                        SELECT
                            S.Id AS IdSesion,
                            S.FechaHoraInicio,
                            S.Duracion,
                            S.Grupo,
                            S.Comentario
                        FROM pla.T_PEspecificoSesion S
                        WHERE S.IdProveedor = @IdProveedor
                            AND S.IdPEspecifico = @IdPEspecifico
                            AND S.Estado = 1
                        ORDER BY S.FechaHoraInicio";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteAgendaSesionDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de todos los tabs activos para un área de trabajo,
        /// ordenados por Numeracion.
        /// </summary>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                var lista = new List<AgendaTabConfiguracionPlanificacionAlternoDTO>();
                string query = @"
                        SELECT
                            ATC.Id,
                            AT.Nombre,
                            AT.VisualizarActividad,
                            AT.CargarInformacionInicial,
                            ATC.VistaBaseDatos,
                            ATC.VistaCampos,
                            ATC.IdFaseGestionContacto,
                            ATC.IdEstadoGestionContacto,
                            AT.CodigoAreaTrabajo,
                            AT.Numeracion,
                            AT.ValidarFecha
                        FROM com.T_AgendaTabConfiguracionPlanificacion ATC
                        INNER JOIN com.T_AgendaTab AT ON ATC.IdAgendaTab = AT.Id
                        WHERE AT.Estado = 1 AND ATC.Estado = 1
                            AND AT.CodigoAreaTrabajo = @CodigoAreaTrabajo
                        ORDER BY AT.Numeracion";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { CodigoAreaTrabajo = codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionPlanificacionAlternoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de un tab específico filtrado por su ID y área de trabajo.
        /// </summary>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab)
        {
            try
            {
                var lista = new List<AgendaTabConfiguracionPlanificacionAlternoDTO>();
                string query = @"
                        SELECT
                            ATC.Id,
                            AT.Nombre,
                            AT.VisualizarActividad,
                            AT.CargarInformacionInicial,
                            ATC.VistaBaseDatos,
                            ATC.VistaCampos,
                            ATC.IdFaseGestionContacto,
                            ATC.IdEstadoGestionContacto,
                            AT.CodigoAreaTrabajo,
                            AT.Numeracion,
                            AT.ValidarFecha
                        FROM com.T_AgendaTabConfiguracionPlanificacion ATC
                        INNER JOIN com.T_AgendaTab AT ON ATC.IdAgendaTab = AT.Id
                        WHERE AT.Estado = 1 AND ATC.Estado = 1
                            AND AT.Id = @IdTab
                            AND AT.CodigoAreaTrabajo = @CodigoAreaTrabajo
                        ORDER BY AT.Numeracion";

                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdTab = idTab, CodigoAreaTrabajo = codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionPlanificacionAlternoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el SP dinámico almacenado en tab.VistaBaseDatos y retorna la lista de actividades.
        /// Si idAsesor > 0 filtra los resultados por IdPersonalAsignado en memoria.
        /// </summary>
        public List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tab, int idAsesor)
        {
            try
            {
                var lista = new List<ActividadAgendaPlanificacionDTO>();
                var resultadoDB = _dapperRepository.QuerySPDapper(tab.VistaBaseDatos, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ActividadAgendaPlanificacionDTO>>(resultadoDB);
                }
                //comentado para probar con asesora generica, descomentar en produccion con criteiro de aiginaciones
                //if (idAsesor > 0)
                //{
                //    lista = lista.Where(a => a.IdPersonalAsignado == idAsesor).ToList();
                //}

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de docentes que comparten el mismo centro de costo que la gestión de contacto proporcionada.
        /// </summary>
        public List<DocenteConCursoDTO> ObtenerDocentesPorGestionContacto(int idGestionContacto)
        {
            try
            {
                List<DocenteConCursoDTO> lista = new List<DocenteConCursoDTO>();
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteCursoPorGestionContactoObtener", new { IdGestionContacto = idGestionContacto });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<DocenteConCursoDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información adicional del docente: email, historial completo de WhatsApp,
        /// historial de correos, resumen de última comunicación, encuestas y alumnos únicos.
        /// </summary>
        public InformacionFaltanteDocenteDTO ObtenerInformacionFaltanteDocente(int idProveedor, int idPEspecifico)
        {
            try
            {
                InformacionFaltanteDocenteDTO info = new InformacionFaltanteDocenteDTO();

                // Información base (email, encuestas, cantidad estudiantes)
                var resultadoBaseDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteInformacionBaseObtener", new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoBaseDB) && !resultadoBaseDB.Contains("[]"))
                {
                    info = JsonConvert.DeserializeObject<List<InformacionFaltanteDocenteDTO>>(resultadoBaseDB).FirstOrDefault();
                }

                // Historial WhatsApp (enviados y recibidos), ordenado en C#
                var resultadoWhatsAppDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteWhatsAppHistorialObtener", new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoWhatsAppDB) && !resultadoWhatsAppDB.Contains("[]"))
                {
                    info.HistorialWhatsApp = JsonConvert.DeserializeObject<List<WhatsAppHistorialDocenteDTO>>(resultadoWhatsAppDB)
                        .OrderByDescending(x => x.FechaCreacion)
                        .ToList();
                }

                // Última comunicación (MAX por canal), selección del más reciente en C#
                var resultadoUltimaComDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteUltimaComunicacionObtener", new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoUltimaComDB) && !resultadoUltimaComDB.Contains("[]"))
                {
                    info.UltimaComunicacion = JsonConvert.DeserializeObject<List<UltimaComunicacionResumenDTO>>(resultadoUltimaComDB)
                        .Where(x => x.Fecha.HasValue)
                        .OrderByDescending(x => x.Fecha)
                        .FirstOrDefault();
                }

                // Historial de correos del docente, ordenado en C#
                var resultadoCorreosDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteCorreoHistorialObtener", new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoCorreosDB) && !resultadoCorreosDB.Contains("[]"))
                {
                    info.HistorialCorreos = JsonConvert.DeserializeObject<List<CorreoResumenDocenteDTO>>(resultadoCorreosDB)
                        .OrderByDescending(x => x.FechaEnvio)
                        .ToList();
                }

                // Puntaje global (promedio de encuestas de todos los cursos del docente)
                var resultadoPuntajeDB = _dapperRepository.QuerySPDapper("pla.SP_DocentePuntajeGlobalObtener", new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoPuntajeDB) && !resultadoPuntajeDB.Contains("[]"))
                {
                    var puntajeResult = JsonConvert.DeserializeObject<List<InformacionFaltanteDocenteDTO>>(resultadoPuntajeDB).FirstOrDefault();
                    if (puntajeResult != null)
                    {
                        info.PuntajeGlobal = puntajeResult.PuntajeGlobal;
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 03/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de un correo enviado al docente por su ID de mkt.T_GmailCorreo.
        /// </summary>
        public CorreoDetalleDocenteDTO ObtenerDetalleCorreo(int idCorreo)
        {
            try
            {
                CorreoDetalleDocenteDTO detalle = null;
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteCorreoDetalleObtener", new { IdCorreo = idCorreo });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    detalle = JsonConvert.DeserializeObject<List<CorreoDetalleDocenteDTO>>(resultadoDB).FirstOrDefault();
                }
                return detalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
