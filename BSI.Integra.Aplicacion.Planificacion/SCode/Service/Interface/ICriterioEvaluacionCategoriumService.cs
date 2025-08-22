using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICriterioEvaluacionCategoriumService
    {
        List<ComboDTO> ObtenerCombo(); 
        CriterioEvaluacionCategoriumDTO Actualizar(CriterioEvaluacionCategoriumDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        CriterioEvaluacionCategoriumDTO Insertar(CriterioEvaluacionCategoriumDTO dto, string usuario);
        CriterioEvaluacionCategoriumDTO ObtenerPorId(int id);
        List<CriterioEvaluacionCategoriumDTO> Obtener();
    }
}
