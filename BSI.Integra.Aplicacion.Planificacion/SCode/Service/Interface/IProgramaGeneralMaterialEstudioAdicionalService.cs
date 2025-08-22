
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralMaterialEstudioAdicionalService
    {
        IEnumerable<ComboDTO> ObtenerProgramaGeneralMaterialEstudio();
        ProgramaGeneralMaterialAgrupadoDTO ObtenerProgramaGeneralMaterialEstudioDetalle(int idPgeneral);
        IEnumerable<ComboDTO> InsertarActualizarProgramaGeneralMaterialEstudio(ProgramaGeneralMaterialEstudioAdicionalEntidadDTO dto, string usuario);
        bool EliminarProgramaGeneralMaterialEstudio(int idPgeneral, string usuario);
    }
}
