using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICriterioEvaluacionModalidadCursoService
    {
        bool EliminacionLogicaPorCriterioEvaluacion(int idCriterioEvaluacion, string usuario, List<int> idsCriterioEvaluacionTipoProgramaNuevos);
        List<CriterioEvaluacionModalidadDTO> ListarCriteriosEvaluacionModalidad();

    }
}
