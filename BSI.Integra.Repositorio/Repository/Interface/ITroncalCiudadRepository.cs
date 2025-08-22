using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITroncalCiudadRepository : IGenericRepository<TTroncalCiudad>
    {
        #region Metodos Base
        TTroncalCiudad Add(TroncalCiudad entidad);
        TTroncalCiudad Update(TroncalCiudad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTroncalCiudad> Add(IEnumerable<TroncalCiudad> listadoEntidad);
        IEnumerable<TTroncalCiudad> Update(IEnumerable<TroncalCiudad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Task<IEnumerable<ComboDTO>> ObtenerComboTroncalCiudadAsync();
    }
}
