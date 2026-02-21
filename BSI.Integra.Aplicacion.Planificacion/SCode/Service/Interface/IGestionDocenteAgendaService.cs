using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteAgendaService
    {
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo);
        (Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ActividadesAgenda, int Cantidad) CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, int idAsesor);
    }
}
