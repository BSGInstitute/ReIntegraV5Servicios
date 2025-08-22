using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPEspecificoCriterioEvaluacionService
    {
        #region Metodos Base
        PEspecificoCriterioEvaluacion Add(PEspecificoCriterioEvaluacion entidad);
        PEspecificoCriterioEvaluacion Update(PEspecificoCriterioEvaluacion entidad);
        bool Delete(int id, string usuario);

        List<PEspecificoCriterioEvaluacion> Add(List<PEspecificoCriterioEvaluacion> listadoEntidad);
        List<PEspecificoCriterioEvaluacion> Update(List<PEspecificoCriterioEvaluacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}

