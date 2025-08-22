using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICriterioEvaluacionService
    {
        CriterioEvaluacionDTO InsertarCriterio(CriterioEvaluacionDTO criterioEvaluacionDTO, string usuario);
        CriterioEvaluacionDTO ActualizarCriterio(CriterioEvaluacionDTO criterioEvaluacionDTO, string usuario);
        bool EliminarCriterio(int id, string usuario);
        List<CriterioEvaluacionDTO> ObtenerCriteriosEvaluacion();
        CriterioEvaluacionDTO ObtenerCriterioEvaluacionPorId(int idCriterioEvaluacion);
        public Task<CriterioEvaluacionComboDTO> ObtenerCombosModulo();
        public IEnumerable<ComboDTO> ObtenerCombo();
        List<ComboDTO> ObtenerCriterio(int tipoprograma, int modalidadprograma);

    }
}
