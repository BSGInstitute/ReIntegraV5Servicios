using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface ICriterioTareaService
    {
        bool Insertar(CriterioTareaDTO criterioDTO, string usuario);
        bool Actualizar(CriterioTareaDTO criterioDTO, string usuario);
        bool Eliminar(int id, string usuario);
        CriterioTareaDTO ObtenerPorId(int idCriterio);
        List<CriterioTareaDTO> ListarCriterios();
    }
}
