

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITagParametroSeoPwRepository : IGenericRepository<TTagParametroSeoPw>
    {
        #region Metodos Base
        TTagParametroSeoPw Add(TagParametroSeoPw entidad);
        TTagParametroSeoPw Update(TagParametroSeoPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTagParametroSeoPw> Add(IEnumerable<TagParametroSeoPw> listadoEntidad);
        IEnumerable<TTagParametroSeoPw> Update(IEnumerable<TagParametroSeoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TagParametroSeoPw? ObtenerPorId(int id);
        IEnumerable<ParametroContenidoDTO> ObtenerTodoParametroPorIdTag(int idTag);
        TagParametroSeoPw? ObtenerPorIdParametroSEOyIdTag(int idParametroSeopw, int idTagPw);
        IEnumerable<TagParametroSeoPw> ObtenerPorIdTag(int idTag);
    }
}
