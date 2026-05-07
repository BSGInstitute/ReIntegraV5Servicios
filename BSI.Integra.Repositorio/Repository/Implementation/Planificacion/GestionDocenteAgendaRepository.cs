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

        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2026
        /// Versión: 2.0
        /// <summary>
        /// Obtiene la cabecera del docente usando pla.SP_GestionDocenteCabeceraObtener.
        /// </summary>
        public DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor)
        {
            try
            {
                DocenteAgendaCabeceraDTO cabecera = null;
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_GestionDocenteCabeceraObtener", new { IdProveedor = idProveedor });
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

        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene cronogramas con sesiones en una sola query flat (fix N+1).
        /// Usa pla.SP_GestionDocenteCronogramaSesionesObtener.
        /// Se agrupa por IdPEspecifico en el service para armar DocenteAgendaCronogramaDTO.
        /// </summary>
        public List<CronogramaSesionFlatDTO> ObtenerCronogramaSesionesFlat(int idProveedor, int idPEspecificoPrioridad)
        {
            try
            {
                var lista = new List<CronogramaSesionFlatDTO>();
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_GestionDocenteCronogramaSesionesObtener", new { IdProveedor = idProveedor, IdPEspecificoPrioridad = idPEspecificoPrioridad });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CronogramaSesionFlatDTO>>(resultadoDB);
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

        /// <summary>
        /// SPs de tab que aceptan filtro opcional por @IdPersonalAsignado.
        /// Convención: el SP debe declarar el parámetro como NULL por default
        /// para mantener retrocompatibilidad con llamadas sin filtro.
        /// Cuando se agregue un nuevo SP de tab que soporte el filtro, sumarlo aquí.
        /// </summary>
        private static readonly HashSet<string> SpsConFiltroPersonalAsignado = new(StringComparer.OrdinalIgnoreCase)
        {
            "pla.SP_GestionDocenteMensajesRecibidosObtener",
        };

        /// Autor: Jose Vega
        /// Fecha: 24/02/2026
        /// Versión: 2.0 — 2026-05-06: pasa @IdPersonalAsignado al SP cuando éste lo soporta
        ///                            (whitelist SpsConFiltroPersonalAsignado), reduciendo
        ///                            el universo de búsqueda en BD en lugar de filtrar en memoria.
        /// <summary>
        /// Ejecuta el SP dinámico almacenado en tab.VistaBaseDatos y retorna la lista de actividades.
        /// Si idAsesor > 0 y el SP está en la whitelist de filtro por personal, le pasa el
        /// parámetro @IdPersonalAsignado al SP. Para el resto de SPs, se mantiene la llamada sin filtro
        /// (compatibilidad con tabs existentes que no aceptan parámetros).
        /// </summary>
        public List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tab, int idAsesor)
        {
            try
            {
                var lista = new List<ActividadAgendaPlanificacionDTO>();

                object parametros = null;
                if (idAsesor > 0 && SpsConFiltroPersonalAsignado.Contains(tab.VistaBaseDatos))
                {
                    parametros = new { IdPersonalAsignado = idAsesor };
                }

                var resultadoDB = _dapperRepository.QuerySPDapper(tab.VistaBaseDatos, parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ActividadAgendaPlanificacionDTO>>(resultadoDB);
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
        /// Versión: 2.0
        /// <summary>
        /// Obtiene información base del docente: email, encuestas, última comunicación y puntaje global.
        /// El historial de WhatsApp y correos se obtienen por separado via ObtenerHistorialWhatsApp
        /// y ObtenerHistorialCorreos para evitar N+1 queries en la carga inicial de la lista.
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

                // Última comunicación (MAX por canal), selección del más reciente en C#
                var resultadoUltimaComDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteUltimaComunicacionObtener", new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoUltimaComDB) && !resultadoUltimaComDB.Contains("[]"))
                {
                    info.UltimaComunicacion = JsonConvert.DeserializeObject<List<UltimaComunicacionResumenDTO>>(resultadoUltimaComDB)
                        .Where(x => x.Fecha.HasValue)
                        .OrderByDescending(x => x.Fecha)
                        .FirstOrDefault();
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

        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial completo de WhatsApp del docente (enviados y recibidos),
        /// ordenado por fecha descendente. Separado de ObtenerInformacionFaltanteDocente
        /// para carga bajo demanda al abrir el tab de WhatsApp.
        /// </summary>
        public List<WhatsAppHistorialDocenteDTO> ObtenerHistorialWhatsApp(int idProveedor)
        {
            try
            {
                var lista = new List<WhatsAppHistorialDocenteDTO>();
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteWhatsAppHistorialObtener", new { IdProveedor = idProveedor });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<WhatsAppHistorialDocenteDTO>>(resultadoDB)
                        .OrderByDescending(x => x.FechaCreacion)
                        .ToList();
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de correos enviados al docente para un curso específico,
        /// ordenado por fecha de envío descendente. Separado de ObtenerInformacionFaltanteDocente
        /// para carga bajo demanda al abrir el tab de correo.
        /// </summary>
        public List<CorreoResumenDocenteDTO> ObtenerHistorialCorreos(int idProveedor, int idPEspecifico)
        {
            try
            {
                var lista = new List<CorreoResumenDocenteDTO>();
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_DocenteCorreoHistorialObtener", new { IdProveedor = idProveedor, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CorreoResumenDocenteDTO>>(resultadoDB)
                        .OrderByDescending(x => x.FechaEnvio)
                        .ToList();
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

        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2026
        /// Versión: 2.0
        /// <summary>
        /// Obtiene el flujo asignado al docente usando pla.SP_GestionDocenteFlujoObtener.
        /// </summary>
        public DocenteAgendaFlujoDTO ObtenerFlujoDocente(int idGestionContacto)
        {
            try
            {
                DocenteAgendaFlujoDTO flujo = null;
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_GestionDocenteFlujoObtener", new { IdGestionContacto = idGestionContacto });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    flujo = JsonConvert.DeserializeObject<List<DocenteAgendaFlujoDTO>>(resultadoDB).FirstOrDefault();
                }
                return flujo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 19/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el contador de alertas del docente ejecutando pla.SP_GestionDocenteAlertasContador.
        /// </summary>
        public ContadorAlertasDTO ObtenerContadorAlertas()
        {
            try
            {
                ContadorAlertasDTO contador = new ContadorAlertasDTO();
                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_GestionDocenteAlertasContador", null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    contador = JsonConvert.DeserializeObject<List<ContadorAlertasDTO>>(resultadoDB).FirstOrDefault() ?? new ContadorAlertasDTO();
                }
                return contador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 11/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el body HTML y archivos adjuntos de un correo desde mkt.T_GmailCorreo (BD),
        /// sin conectarse a IMAP. El IdCorreo es el PK de T_GmailCorreo.
        /// </summary>
        public CorreoBodyDTO ObtenerCorreoBodyDB(int idCorreo)
        {
            try
            {
                var resultado = new CorreoBodyDTO();

                // Obtener el body del correo
                string queryCorreo = @"
                    SELECT ISNULL(EmailBody, '') AS EmailBody
                    FROM mkt.T_GmailCorreo WITH(NOLOCK)
                    WHERE Id = @IdCorreo AND Estado = 1";

                var resultadoCorreoDB = _dapperRepository.QueryDapper(queryCorreo, new { IdCorreo = idCorreo });
                if (!string.IsNullOrEmpty(resultadoCorreoDB) && !resultadoCorreoDB.Contains("[]"))
                {
                    var correo = JsonConvert.DeserializeObject<List<CorreoBodyDTO>>(resultadoCorreoDB).FirstOrDefault();
                    if (correo != null)
                    {
                        resultado.EmailBody = correo.EmailBody;
                    }
                }

                // Obtener archivos adjuntos
                string queryAdjuntos = @"
                    SELECT
                        Id,
                        IdGmailCorreo AS IdCorreo,
                        Nombre AS NombreArchivo,
                        ISNULL(UrlArchivoRepositorio, '') AS UrlArchivoRepositorio
                    FROM mkt.T_GmailCorreoArchivoAdjunto WITH(NOLOCK)
                    WHERE IdGmailCorreo = @IdCorreo AND Estado = 1";

                var resultadoAdjuntosDB = _dapperRepository.QueryDapper(queryAdjuntos, new { IdCorreo = idCorreo });
                if (!string.IsNullOrEmpty(resultadoAdjuntosDB) && !resultadoAdjuntosDB.Contains("[]"))
                {
                    resultado.ArchivosAdjuntos = JsonConvert.DeserializeObject<List<CorreoArchivoAdjuntoDTO>>(resultadoAdjuntosDB);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 07/05/2026
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta pla.SP_GestionDocenteMensajesRecibidosObtener pasando el filtro
        /// opcional @IdPersonalAsignado. Endpoint dedicado al tab "Mensajes Recibidos"
        /// (no pasa por la pipeline genérica de tabs).
        /// </summary>
        public List<MensajeRecibidoAgendaDTO> ObtenerMensajesRecibidos(int idPersonalAsignado)
        {
            try
            {
                var lista = new List<MensajeRecibidoAgendaDTO>();
                object parametros = idPersonalAsignado > 0
                    ? new { IdPersonalAsignado = idPersonalAsignado }
                    : null;

                var resultadoDB = _dapperRepository.QuerySPDapper("pla.SP_GestionDocenteMensajesRecibidosObtener", parametros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MensajeRecibidoAgendaDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
