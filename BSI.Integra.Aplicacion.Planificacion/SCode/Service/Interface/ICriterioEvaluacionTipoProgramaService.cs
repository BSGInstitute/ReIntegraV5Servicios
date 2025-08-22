using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICriterioEvaluacionTipoProgramaService
    {
        bool EliminacionLogicaPorCriterioEvaluacion(int idCriterioEvaluacion, string usuario, List<int> idsCriterioEvaluacionTipoProgramaNuevos);
    }
}
