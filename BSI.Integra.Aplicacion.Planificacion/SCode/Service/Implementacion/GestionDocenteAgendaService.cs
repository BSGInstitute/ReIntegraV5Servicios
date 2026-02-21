using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Service: GestionDocenteAgendaService
    /// Autor: Jose Vega
    /// Fecha: 21/02/2026
    /// <summary>
    /// Orquesta la carga de tabs y actividades para la agenda de planificación docente.
    /// Lee la configuración de tabs desde T_AgendaTab + T_AgendaTabConfiguracionPlanificacion
    /// y ejecuta los SPs almacenados en VistaBaseDatos.
    /// </summary>
    public class GestionDocenteAgendaService : IGestionDocenteAgendaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones de tabs para un área de trabajo.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <returns>Lista de configuraciones de tabs</returns>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las actividades por cada tab configurado para el asesor.
        /// Itera los tabs y ejecuta el SP de cada uno.
        /// </summary>
        /// <param name="idAsesor">Id del personal asignado (0 para no filtrar)</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <returns>Diccionario con nombre del tab como clave y lista de actividades como valor</returns>
        public Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo)
        {
            try
            {
                var tabsAgenda = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);
                Dictionary<string, List<ActividadAgendaPlanificacionDTO>> actividadesAgenda = new();

                foreach (var item in tabsAgenda)
                {
                    if (item.VisualizarActividad && item.CargarInformacionInicial)
                    {
                        var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(item, idAsesor);

                        if (!actividadesAgenda.ContainsKey(item.Nombre))
                            actividadesAgenda.Add(item.Nombre, actividades);
                        else
                            actividadesAgenda[item.Nombre].AddRange(actividades);
                    }
                }

                return actividadesAgenda;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 21/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico filtrado por idTab.
        /// </summary>
        /// <param name="idTab">Id del AgendaTab</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo</param>
        /// <param name="idAsesor">Id del personal asignado (0 para no filtrar)</param>
        /// <returns>Tupla con diccionario de actividades y cantidad total</returns>
        public (Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ActividadesAgenda, int Cantidad) CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var tabsActividad = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab);
                Dictionary<string, List<ActividadAgendaPlanificacionDTO>> actividadesAgenda = new();
                int cantidad = 0;

                if (tabsActividad != null && tabsActividad.Count > 0)
                {
                    foreach (var item in tabsActividad)
                    {
                        var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(item, idAsesor);
                        cantidad = actividades.Count;

                        if (!actividadesAgenda.ContainsKey(item.Nombre))
                            actividadesAgenda.Add(item.Nombre, actividades);
                        else
                            actividadesAgenda[item.Nombre].AddRange(actividades);
                    }
                }

                return (actividadesAgenda, cantidad);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
