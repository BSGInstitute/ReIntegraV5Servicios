using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoCriterioEvaluacionRepository : IGenericRepository<TPespecificoCriterioEvaluacion>
    {
        #region Metodos Base
        TPespecificoCriterioEvaluacion Add(PEspecificoCriterioEvaluacion entidad);
        TPespecificoCriterioEvaluacion Update(PEspecificoCriterioEvaluacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoCriterioEvaluacion> Add(IEnumerable<PEspecificoCriterioEvaluacion> listadoEntidad);
        IEnumerable<TPespecificoCriterioEvaluacion> Update(IEnumerable<PEspecificoCriterioEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
