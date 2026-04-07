using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ISubCriterioTareaRepository
    {
        bool Insertar(SubCriterioTareaDTO subCriterioDTO, string usuario);
        bool Actualizar(SubCriterioTareaDTO subCriterioDTO, string usuario);
        bool Eliminar(int id, string usuario);
        SubCriterioTareaDTO ObtenerPorId(int idSubCriterio);
        List<SubCriterioTareaDTO> ListarSubCriterios();
    }
}
