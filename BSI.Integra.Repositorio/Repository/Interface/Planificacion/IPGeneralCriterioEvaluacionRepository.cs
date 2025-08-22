using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPGeneralCriterioEvaluacionRepository : IGenericRepository<TPgeneralCriterioEvaluacion>
    {
        #region Metodos Base
        TPgeneralCriterioEvaluacion Add(PgeneralCriterioEvaluacion entidad);
        TPgeneralCriterioEvaluacion Update(PgeneralCriterioEvaluacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralCriterioEvaluacion> Add(IEnumerable<PgeneralCriterioEvaluacion> listadoEntidad);
        IEnumerable<TPgeneralCriterioEvaluacion> Update(IEnumerable<PgeneralCriterioEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralCriterioEvaluacion? ObtenerPorId(int id);
        List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionAOnline(int idPgeneral);
        List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionOnline(int idPgeneral);
        List<PGeneralCriterioEvaluacionDTO> ObtenerPGcriteriosEvaluacionPresencial(int idPgeneral);
        IEnumerable<PGeneralModalidadDTO> ObtenerModalidadesPorIdPGeneral(int idPGeneral);
        void EliminarPorIdPGeneralIdModalidad(int idPGeneral, int idModalidadCurso, string usuario);
        IEnumerable<PgeneralCriterioEvaluacion> ObtenerPorIdModalidadCurso(int idModalidadCurso);
    }
}
