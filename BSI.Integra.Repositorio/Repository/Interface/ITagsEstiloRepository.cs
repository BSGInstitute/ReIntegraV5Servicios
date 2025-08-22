using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITagsEstiloRepository : IGenericRepository<TTagEstilo>
    {
        #region Metodos Base
        TTagEstilo Add(TagsEstilo entidad);
        TTagEstilo Update(TagsEstilo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTagEstilo> Add(IEnumerable<TagsEstilo> listadoEntidad);
        IEnumerable<TTagEstilo> Update(IEnumerable<TagsEstilo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        //IEnumerable<ObtenerFiltro> ObtenerFiltro();
        IEnumerable<TagsEstilo> ObtenerTagsEstilo();

        IEnumerable<EstiloValor> ObtenerEstiloValor(int id);

    }
}
