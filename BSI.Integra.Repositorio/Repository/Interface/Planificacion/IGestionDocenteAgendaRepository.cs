using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteAgendaRepository : IGenericRepository<TProveedor>
    {
        List<DocenteConCursoDTO> ObtenerDocentesConCursos();
        DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor);
        DocenteAgendaFlujoDTO ObtenerFlujoDocente(int idGestionContacto);
        List<DocenteAgendaCronogramaDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPrioridad);
        List<DocenteAgendaSesionDTO> ObtenerSesionesPorCursoYDocente(int idProveedor, int idPEspecifico);
        /// <summary>Obtiene la configuración de todos los tabs activos para un área de trabajo.</summary>
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        /// <summary>Obtiene la configuración de un tab específico por su ID y área de trabajo.</summary>
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab);
        /// <summary>
        /// Ejecuta el SP dinámico del tab y retorna las actividades.
        /// Si idAsesor > 0 filtra por IdPersonalAsignado en memoria.
        /// </summary>
        List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tab, int idAsesor);
    }
}
