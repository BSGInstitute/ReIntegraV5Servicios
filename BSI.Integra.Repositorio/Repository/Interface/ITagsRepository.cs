using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITagsRepository : IGenericRepository<TTag>
    {
        #region Metodos Base
        TTag Add(Tags entidad);
        TTag Update(Tags entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTag> Add(IEnumerable<Tags> listadoEntidad);
        IEnumerable<TTag> Update(IEnumerable<Tags> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboTag> ObtenerCombo();
        IEnumerable<Tags> ObtenerTags();

    }
}
