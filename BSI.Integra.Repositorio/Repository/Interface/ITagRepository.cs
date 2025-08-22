using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITagRepository : IGenericRepository<TTag>
    {
        #region Metodos Base
        TTag Add(Tag entidad);
        TTag Update(Tag entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTag> Add(IEnumerable<Tag> listadoEntidad);
        IEnumerable<TTag> Update(IEnumerable<Tag> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TagComboDTO> ObtenerCombo();
    }
}
