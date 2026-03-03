using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICriterioTareaRepository
    {
        bool Insertar(CriterioTareaDTO criterioDTO, string usuario);
        bool Actualizar(CriterioTareaDTO criterioDTO, string usuario);
        bool Eliminar(int id, string usuario);
        CriterioTareaDTO ObtenerPorId(int idCriterio);
        List<CriterioTareaDTO> ListarCriterios();
        List<SubCriterioTareaDTO> ListarSubCriteriosPorCriterio(int idCriterio);
        bool AsignarSubCriterio(int idCriterio, int idSubCriterio, string usuario);
        bool DesasignarSubCriterio(int idCriterio, int idSubCriterio, string usuario);
    }
}
