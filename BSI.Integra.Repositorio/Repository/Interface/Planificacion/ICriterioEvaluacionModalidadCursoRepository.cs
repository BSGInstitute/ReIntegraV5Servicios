using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICriterioEvaluacionModalidadCursoRepository : IGenericRepository<TCriterioEvaluacionModalidadCurso>
    {
        #region Metodos Base
        TCriterioEvaluacionModalidadCurso Add(CriterioEvaluacionModalidadCurso entidad);
        TCriterioEvaluacionModalidadCurso Update(CriterioEvaluacionModalidadCurso entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacionModalidadCurso> Add(IEnumerable<CriterioEvaluacionModalidadCurso> listadoEntidad);
        IEnumerable<TCriterioEvaluacionModalidadCurso> Update(IEnumerable<CriterioEvaluacionModalidadCurso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CriterioEvaluacionModalidadCursoDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion);
        CriterioEvaluacionModalidadCurso ObtenerPorIdModalidadCursoYIdCriterioEvaluacion(int idCriterioEvaluacion, int idModalidadCurso);
        List<CriterioEvaluacionModalidadDTO> ListarCriteriosEvaluacionModalidad();
    }
}
