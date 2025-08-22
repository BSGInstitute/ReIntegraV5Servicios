
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralMaterialEstudioAdicionalRepository
    {
        #region Metodos Base
        TProgramaGeneralMaterialEstudioAdicional Add(ProgramaGeneralMaterialEstudioAdicional entidad);
        TProgramaGeneralMaterialEstudioAdicional Update(ProgramaGeneralMaterialEstudioAdicional entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralMaterialEstudioAdicional> Add(IEnumerable<ProgramaGeneralMaterialEstudioAdicional> listadoEntidad);
        IEnumerable<TProgramaGeneralMaterialEstudioAdicional> Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicional> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralMaterialEstudioAdicional> ObtenerPorIdPgeneral(int idPGeneral);
        ProgramaGeneralMaterialEstudioAdicional ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerProgramaGeneralMaterialEstudioAdicional();
    }
}
