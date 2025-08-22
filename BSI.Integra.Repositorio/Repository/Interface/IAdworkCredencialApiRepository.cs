using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAdworkCredencialApiRepository : IGenericRepository<TAdworkCredencialApi>
    {
        #region Metodos Base
        TAdworkCredencialApi Add(AdworkCredencialApi entidad);
        TAdworkCredencialApi Update(AdworkCredencialApi entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAdworkCredencialApi> Add(IEnumerable<AdworkCredencialApi> listadoEntidad);
        IEnumerable<TAdworkCredencialApi> Update(IEnumerable<AdworkCredencialApi> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public AdworkCredencialApiDTO ObtenerCredencial();
    }
}
