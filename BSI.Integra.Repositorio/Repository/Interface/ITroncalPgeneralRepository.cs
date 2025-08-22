using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITroncalPgeneralRepository : IGenericRepository<TTroncalPgeneral>
    {
        #region Metodos Base
        TTroncalPgeneral Add(TroncalPgeneral entidad);
        TTroncalPgeneral Update(TroncalPgeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTroncalPgeneral> Add(IEnumerable<TroncalPgeneral> listadoEntidad);
        IEnumerable<TTroncalPgeneral> Update(IEnumerable<TroncalPgeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<TroncalPgeneralFiltroDTO> ObtenerTroncalPgeneralFiltro();
        IEnumerable<LocacionTroncalDTO> ObtenerLocacionTroncal();
        Task<IEnumerable<LocacionTroncalDTO>> ObtenerLocacionTroncalAsync();
        Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerPGeneralPorIdSubArea(int idSubArea);
        Task<IEnumerable<TroncalPGeneralSubAreaCodigoDTO>> ObtenerTroncalPGeneral();

        Task<IEnumerable<ComboDTO>> ObtenerTroncalCiudadAsync();
        TroncalPgeneral? ObtenerPorId(int id);
    }
}
