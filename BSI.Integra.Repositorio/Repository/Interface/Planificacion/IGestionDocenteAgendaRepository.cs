using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteAgendaRepository : IGenericRepository<TProveedor>
    {
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfiguradosPorIdTab(string codigoAreaTrabajo, int idTab);
        List<ActividadAgendaPlanificacionDTO> ObtenerActividades(AgendaTabConfiguracionPlanificacionAlternoDTO tabAgenda, int idAsesor);
        int CantidadActividadesPorTab(AgendaTabConfiguracionPlanificacionAlternoDTO tabAgenda, int idAsesor);
    }
}
