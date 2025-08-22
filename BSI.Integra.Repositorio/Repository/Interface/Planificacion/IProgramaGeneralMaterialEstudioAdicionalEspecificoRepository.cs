
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralMaterialEstudioAdicionalEspecificoRepository
    {
        #region Metodos Base
        TProgramaGeneralMaterialEstudioAdicionalEspecifico Add(ProgramaGeneralMaterialEstudioAdicionalEspecifico entidad);
        TProgramaGeneralMaterialEstudioAdicionalEspecifico Update(ProgramaGeneralMaterialEstudioAdicionalEspecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralMaterialEstudioAdicionalEspecifico> Add(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listadoEntidad);
        IEnumerable<TProgramaGeneralMaterialEstudioAdicionalEspecifico> Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ValorDTO> ObtenerIdsPorIdPgeneral(int idPGeneral);
        ProgramaGeneralMaterialEstudioAdicionalEspecifico ObtenerPorIdyIdPgeneral(int idPGeneral, int id);
    }
}
