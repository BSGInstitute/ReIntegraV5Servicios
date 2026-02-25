using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Servicio de agenda de docentes. Orquesta las consultas del repositorio
    /// para construir las respuestas de los endpoints de GestionDocenteAgenda.
    /// </summary>
    public class GestionDocenteAgendaService : IGestionDocenteAgendaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteAgendaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de docentes que tienen cursos asignados con su personal asignado.
        /// </summary>
        /// <returns>Lista de DocenteConCursoDTO.</returns>
        public List<DocenteConCursoDTO> ObtenerDocentesConCursos()
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerDocentesConCursos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle completo de un docente: cabecera con datos personales, flujo asignado
        /// y todos sus cronogramas con sesiones, priorizando el curso indicado.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente/proveedor.</param>
        /// <param name="idPEspecifico">Identificador del curso a priorizar en la lista.</param>
        /// <param name="idGestionContacto">Identificador opcional del GestionContacto para obtener el flujo.</param>
        /// <returns>DocenteAgendaDetalleDTO con toda la información.</returns>
        public DocenteAgendaDetalleDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto)
        {
            try
            {
                var cabecera = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCabeceraDocente(idProveedor);
                if (cabecera == null) return null;

                DocenteAgendaFlujoDTO flujo = null;
                if (idGestionContacto.HasValue)
                {
                    flujo = _unitOfWork.GestionDocenteAgendaRepository.ObtenerFlujoDocente(idGestionContacto.Value);
                }

                var cronogramas = _unitOfWork.GestionDocenteAgendaRepository.ObtenerCronogramasDocente(idProveedor, idPEspecifico);

                foreach (var cronograma in cronogramas)
                {
                    cronograma.Sesiones = _unitOfWork.GestionDocenteAgendaRepository.ObtenerSesionesPorCursoYDocente(idProveedor, cronograma.IdPEspecifico);
                }

                return new DocenteAgendaDetalleDTO
                {
                    Cabecera = cabecera,
                    Flujo = flujo,
                    Cronogramas = cronogramas
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de todos los tabs activos para un área de trabajo.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo (ej: "PLA").</param>
        /// <returns>Lista de AgendaTabConfiguracionPlanificacionAlternoDTO.</returns>
        public List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                return _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades de todos los tabs que tengan VisualizarActividad = true
        /// y CargarInformacionInicial = true, agrupadas por nombre de tab.
        /// El filtro por asesor se aplica en repositorio si idAsesor > 0.
        /// </summary>
        /// <param name="idAsesor">ID del personal asignado; 0 para no filtrar.</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <returns>Diccionario con clave = NombreTab y valor = lista de actividades.</returns>
        public Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo)
        {
            try
            {
                var resultado = new Dictionary<string, List<ActividadAgendaPlanificacionDTO>>();
                var tabs = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfigurados(codigoAreaTrabajo);

                foreach (var tab in tabs.Where(t => t.VisualizarActividad && t.CargarInformacionInicial))
                {
                    var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(tab, idAsesor);
                    resultado[tab.Nombre] = actividades;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico por su ID y retorna también la cantidad total.
        /// </summary>
        /// <param name="idTab">ID del tab (T_AgendaTabConfiguracionPlanificacion.Id).</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para no filtrar.</param>
        /// <returns>CargarActividadPorTabResultadoDTO con actividades agrupadas y cantidad.</returns>
        public CargarActividadPorTabResultadoDTO CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var actividadesAgenda = new Dictionary<string, List<ActividadAgendaPlanificacionDTO>>();
                var tabs = _unitOfWork.GestionDocenteAgendaRepository.ObtenerTabsConfiguradosPorIdTab(codigoAreaTrabajo, idTab);

                foreach (var tab in tabs)
                {
                    var actividades = _unitOfWork.GestionDocenteAgendaRepository.ObtenerActividades(tab, idAsesor);
                    actividadesAgenda[tab.Nombre] = actividades;
                }

                int cantidad = actividadesAgenda.Values.Sum(v => v.Count);

                return new CargarActividadPorTabResultadoDTO
                {
                    ActividadesAgenda = actividadesAgenda,
                    Cantidad = cantidad
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
