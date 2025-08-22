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
    public interface ICriterioEvaluacionTipoProgramaRepository : IGenericRepository<TCriterioEvaluacionTipoPrograma>
    {
        #region Metodos Base
        TCriterioEvaluacionTipoPrograma Add(CriterioEvaluacionTipoPrograma entidad);
        TCriterioEvaluacionTipoPrograma Update(CriterioEvaluacionTipoPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacionTipoPrograma> Add(IEnumerable<CriterioEvaluacionTipoPrograma> listadoEntidad);
        IEnumerable<TCriterioEvaluacionTipoPrograma> Update(IEnumerable<CriterioEvaluacionTipoPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CriterioEvaluacionTipoProgramaDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion);
        CriterioEvaluacionTipoPrograma ObtenerPorIdTipoProgramaYIdCriterioEvaluacion(int idTipoPrograma, int idCriterioEvaluacion);
    }
}
