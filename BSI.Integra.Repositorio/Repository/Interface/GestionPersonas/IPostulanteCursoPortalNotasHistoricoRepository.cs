using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteCursoPortalNotasHistoricoRepository : IGenericRepository<TCriterioEvaluacionProceso>
    {
        #region Metodos Base
        TCriterioEvaluacionProceso Add(PostulanteCursoPortalNotasHistorico entidad);
        TCriterioEvaluacionProceso Update(PostulanteCursoPortalNotasHistorico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<PostulanteCursoPortalNotasHistorico> listadoEntidad);
        IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<PostulanteCursoPortalNotasHistorico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteCursoPortalNotasHistorico? ObtenerPorId(int id);
        List<PostulanteCursoPortalNotasHistoricoDTO> ObtenerNotasAnteriores(int idAlumno, int idPespecifico);
        List<PostulanteVideoVisualizacionDTO> ObtenerVisualizacionVideoAnteriores(string idUsuario, int idPGeneral);
        bool EliminarFisicamenteAnterioresNotas(string idUsuario, int idPGeneral, List<int> listaIdNota, List<int> listaIdVideo);
    }
}
