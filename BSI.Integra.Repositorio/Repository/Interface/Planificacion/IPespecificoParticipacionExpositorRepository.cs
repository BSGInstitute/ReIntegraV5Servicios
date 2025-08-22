using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoParticipacionExpositorRepository : IGenericRepository<TPespecificoParticipacionExpositor>
    {
        #region Metodos Base
        TPespecificoParticipacionExpositor Add(PespecificoParticipacionExpositor entidad);
        TPespecificoParticipacionExpositor Update(PespecificoParticipacionExpositor entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoParticipacionExpositor> Add(IEnumerable<PespecificoParticipacionExpositor> listadoEntidad);
        IEnumerable<TPespecificoParticipacionExpositor> Update(IEnumerable<PespecificoParticipacionExpositor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoParticipacionExpositor? ObtenerPorId(int id);
        IEnumerable<PespecificoParticipacionExpositor>? ObtenerPorIds(IEnumerable<int> id);
        PespecificoParticipacionExpositor? ObtenerPorIdPespecificoYGrupo(int idPespecifico, int grupo);
        IEnumerable<PEspecificoHistorialParticipacionDocenteDTO> ObtenerHistorialParticipacion(ParticipacionExpositorFiltroDTO dto);
        bool ActualizarRegistroAsistencias(int idCursoActual, string nombreUsuario);
        bool ActualizarRegistroNotas(int idCursoActual, string nombreUsuario);
    }
}
