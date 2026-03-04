using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteAgendaService
    {
        List<DocenteConCursoDTO> ObtenerDocentesConCursos();
        DocenteAgendaDetalleDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto);
        List<AgendaTabConfiguracionPlanificacionAlternoDTO> ObtenerTabsConfigurados(string codigoAreaTrabajo);
        Dictionary<string, List<ActividadAgendaPlanificacionDTO>> ObtenerActividades(int idAsesor, string codigoAreaTrabajo);
        CargarActividadPorTabResultadoDTO CargarActividadSeleccionadaPorFiltro(int idTab, string codigoAreaTrabajo, int idAsesor);
        List<DocenteConCursoDTO> ObtenerDocentesPorGestionContacto(int idGestionContacto);
        InformacionFaltanteDocenteDTO ObtenerInformacionFaltanteDocente(int idProveedor, int idPEspecifico);
        CorreoDetalleDocenteDTO ObtenerDetalleCorreo(int idCorreo);
    }
}
