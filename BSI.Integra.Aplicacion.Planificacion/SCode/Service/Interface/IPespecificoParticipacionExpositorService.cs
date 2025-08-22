
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPespecificoParticipacionExpositorService
    {
        CombosPEspecificoExpositorDTO ObtenerCombosProgramaEspecificoProveedor();
        IEnumerable<PEspecificoHistorialParticipacionDocenteDTO> GenerarReporteParticipacionExpositor(ParticipacionExpositorFiltroDTO dto);
        bool ActualizarProveedor(ParticipacionExpositorDTO dto, string usuario);
        bool ActualizarProveedorConfirmacion(PEE_ProveedorOperacionesGrupoConfirmadoDTO dto, string usuario);
        bool ActualizarRegistroAsistencia(int idCursoActual, string usuario);
        bool ActualizarRegistroNotas(int idCursoActual, string usuario);
    }
}
