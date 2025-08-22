using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPgeneralCriterioEvaluacionHijoRepository : IGenericRepository<TPgeneralCriterioEvaluacionHijo>
    {
        #region Metodos Base
        TPgeneralCriterioEvaluacionHijo Add(PgeneralCriterioEvaluacionHijo entidad);
        TPgeneralCriterioEvaluacionHijo Update(PgeneralCriterioEvaluacionHijo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralCriterioEvaluacionHijo> Add(IEnumerable<PgeneralCriterioEvaluacionHijo> listadoEntidad);
        IEnumerable<TPgeneralCriterioEvaluacionHijo> Update(IEnumerable<PgeneralCriterioEvaluacionHijo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public void InsertarModalidadPGHIjo(PgeneralModalidad pgeneralModalidad);
        List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosAonline(int idPgeneral);
        List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosPresencial(int idPgeneral);
        PgeneralCriterioEvaluacionHijo? ObtenerPorId(int id);
        List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosOnline(int idPgeneral);
        IEnumerable<PGeneralModalidadDTO> ObtenerModalidadesPorIdPGeneral(int idPGeneral);
        void EliminarPorIdPGeneralYIdModalidad(int idPGeneral, int idModalidadCurso, string usuario);
    }
}
