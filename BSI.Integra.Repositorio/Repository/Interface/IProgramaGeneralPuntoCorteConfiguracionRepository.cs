using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralPuntoCorteConfiguracionRepository : IGenericRepository<TProgramaGeneralPuntoCorteConfiguracion>
    {
        #region Metodos Base
        TProgramaGeneralPuntoCorteConfiguracion Add(ProgramaGeneralPuntoCorteConfiguracion entidad);
        TProgramaGeneralPuntoCorteConfiguracion Update(ProgramaGeneralPuntoCorteConfiguracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPuntoCorteConfiguracion> Add(IEnumerable<ProgramaGeneralPuntoCorteConfiguracion> listadoEntidad);
        IEnumerable<TProgramaGeneralPuntoCorteConfiguracion> Update(IEnumerable<ProgramaGeneralPuntoCorteConfiguracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ProgramaGeneralPuntoCorteConfiguracionDTO> Obtener();
    }
}
