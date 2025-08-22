using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ICriterioEvaluacionProcesoService 
    {
        IEnumerable<CriterioEvaluacionProcesoExamenDTO> Obtener();
        CriterioEvaluacionProcesoDTO Insertar(CriterioEvaluacionProcesoDTO dto, string usuario);
        CriterioEvaluacionProcesoDTO Actualizar(CriterioEvaluacionProcesoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
