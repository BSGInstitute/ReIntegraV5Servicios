using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralPuntoCorteDetalleRepository : IGenericRepository<TProgramaGeneralPuntoCorteDetalle>
    {
        #region Metodos Base
        TProgramaGeneralPuntoCorteDetalle Add(ProgramaGeneralPuntoCorteDetalle entidad);
        TProgramaGeneralPuntoCorteDetalle Update(ProgramaGeneralPuntoCorteDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPuntoCorteDetalle> Add(IEnumerable<ProgramaGeneralPuntoCorteDetalle> listadoEntidad);
        IEnumerable<TProgramaGeneralPuntoCorteDetalle> Update(IEnumerable<ProgramaGeneralPuntoCorteDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerPorIdProgramaGeneralPuntoCorte(int idProgramaGeneralPuntoCorte);
        List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerPorIdProgramaGeneralPuntoCorteIdPuntoCorte(int idProgramaGeneralPuntoCorte, int idPuntoCorte);
    }
}
