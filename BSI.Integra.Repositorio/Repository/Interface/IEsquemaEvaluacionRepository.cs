using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEsquemaEvaluacionRepository : IGenericRepository<TEsquemaEvaluacion>
    {
        #region Metodos Base
        TEsquemaEvaluacion Add(EsquemaEvaluacion entidad);
        TEsquemaEvaluacion Update(EsquemaEvaluacion entidad);
        bool Delete(int id, string usuario);       
        IEnumerable<TEsquemaEvaluacion> Add(IEnumerable<EsquemaEvaluacion> listadoEntidad);
        IEnumerable<TEsquemaEvaluacion> Update(IEnumerable<EsquemaEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        EsquemaEvaluacion? ObtenerPorId(int id);
        string ObtenerNombreCongelamientoEsquemaPorMatricula(int idMatriculaCabecera);
        List<EsquemaEvaluacionItemEvaluableAlumnoDTO> ListadoDetalladoItemEvaluablePorAlumno(int idMatriculaCabecera, int idPEspecifico, int grupo);
        bool ExisteNuevaAulaVirtual(int idPEspecifico);
        int EliminarMatriculaCabecera(int IdMatriculaCabecera);
        int InsertarMatriculaNueva(int IdMatriculaCabecera);
        int InsertarMatriculaAntigua(int IdMatriculaCabecera);
        string ObtenerNombreCongelamientoEsquemaPorMatricula2(int idMatriculaCabecera);
        List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera);
        bool ActualizarCongelamientoEsquemaPorMatricula(EditarCongelamientoPEspecificoMatriculaAlumnoDTO json);
        List<CriterioEvaluacionCursoDTO> ListadoCriteriosEvaluacionPorCurso(int IdMatriculaCabecera, int IdPEspecifico, int Grupo);
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        public List<EsquemaEvaluacionComboDTO> ObtenerComboEsquemaEvaluacion();
        public List<EsquemaCriterioEvaluacionDTO> ObtenerCriterioEvaluacionPorIdEsquema(int IdEsquemaEvaluacion);
        bool ModificarEsquemaEvaluacionPredefinido(int idEsquemaEvaluacionPGeneral);
        IEnumerable<EsquemaEvaluacionDTO> ObtenerTodo();
     
    }
}
