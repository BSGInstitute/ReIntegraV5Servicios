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
    /// Repositorio: GestionDocenteAgendaRepository
    /// Autor: Jose Vega
    /// Fecha: 21/02/2026
    /// <summary>
    /// Gestión de tabs de agenda para planificación docente.
    /// Lee la configuración de T_AgendaTab + T_AgendaTabConfiguracionPlanificacion
    /// y ejecuta los SPs almacenados en el campo VistaBaseDatos.
    /// </summary>
    public class GestionDocenteAgendaRepository : GenericRepository<TProveedor>, IGestionDocenteAgendaRepository
    {
        public GestionDocenteAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones de tabs de agenda para planificación,
        /// uniendo T_AgendaTab con T_AgendaTabConfiguracionPlanificacion.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <returns>Lista de AgendaTabConfiguracionPlanificacionAlternoDTO</returns>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                List<AgendaTabConfiguracionPlanificacionAlternoDTO> rpta = new();
                var query = @"
                    SELECT
                        ATC.Id,
                        AT.Nombre,
                        AT.VisualizarActividad,
                        AT.CargarInformacionInicial,
                        ATC.VistaBaseDatos,
                        ATC.CamposVista,
                        ATC.IdFaseGestionContacto,
                        ATC.IdEstadoGestionContacto,
                        AT.CodigoAreaTrabajo,
                        AT.Numeracion,
                        AT.ValidarFecha
                    FROM com.T_AgendaTabConfiguracionPlanificacion ATC
                    INNER JOIN com.T_AgendaTab AT ON ATC.IdAgendaTab = AT.Id
                    WHERE AT.Estado = 1
                        AND ATC.Estado = 1
                        AND AT.CodigoAreaTrabajo = @codigoAreaTrabajo";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionPlanificacionAlternoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerTabsConfigurados(), {ex.Message}");
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de un tab específico por IdAgendaTab.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <param name="idTab">Id del AgendaTab</param>
        /// <returns>Lista de AgendaTabConfiguracionPlanificacionAlternoDTO</returns>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab)
        {
            try
            {
                List<AgendaTabConfiguracionPlanificacionAlternoDTO> rpta = new();
                var query = @"
                    SELECT
                        ATC.Id,
                        AT.Nombre,
                        AT.VisualizarActividad,
                        AT.CargarInformacionInicial,
                        ATC.VistaBaseDatos,
                        ATC.CamposVista,
                        ATC.IdFaseGestionContacto,
                        ATC.IdEstadoGestionContacto,
                        AT.CodigoAreaTrabajo,
                        AT.Numeracion,
                        AT.ValidarFecha
                    FROM com.T_AgendaTabConfiguracionPlanificacion ATC
                    INNER JOIN com.T_AgendaTab AT ON ATC.IdAgendaTab = AT.Id
                    WHERE AT.Estado = 1
                        AND ATC.Estado = 1
                        AND AT.CodigoAreaTrabajo = @codigoAreaTrabajo
                        AND AT.Id = @idTab";
                var resultado = _dapperRepository.QueryDapper(query, new { codigoAreaTrabajo, idTab });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionPlanificacionAlternoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerTabsConfiguradosPorIdTab(), {ex.Message}");
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el SP almacenado en VistaBaseDatos del tab configurado
        /// y retorna las actividades. Si se envía idAsesor > 0 filtra por IdPersonalAsignado.
        /// </summary>
        /// <param name="tabAgenda">Configuración del tab</param>
        /// <param name="idAsesor">Id del personal asignado (0 para no filtrar)</param>
        /// <returns>Lista de ActividadAgendaPlanificacionDTO</returns>
        public List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tabAgenda, int idAsesor)
        {
            try
            {
                List<ActividadAgendaPlanificacionDTO> rpta = new();
                var resultado = _dapperRepository.QuerySPDapper(tabAgenda.VistaBaseDatos, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAgendaPlanificacionDTO>>(resultado);
                }
                if (idAsesor > 0)
                {
                    rpta = rpta.Where(x => x.IdPersonalAsignado == idAsesor).ToList();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerActividades(), {ex.Message}");
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantidad de actividades por tab.
        /// Ejecuta el SP del tab y cuenta los resultados, filtrando por idAsesor si se envía.
        /// </summary>
        /// <param name="tabAgenda">Configuración del tab</param>
        /// <param name="idAsesor">Id del personal asignado (0 para no filtrar)</param>
        /// <returns>Cantidad de actividades</returns>
        public int CantidadActividadesPorTab(AgendaTabConfiguracionPlanificacionAlternoDTO tabAgenda, int idAsesor)
        {
            try
            {
                var actividades = ObtenerActividades(tabAgenda, idAsesor);
                return actividades.Count;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CantidadActividadesPorTab(), {ex.Message}");
            }
        }
    }
}
