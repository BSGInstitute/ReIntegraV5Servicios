using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITagService
    {
        #region Metodos Base
        Tag Add(Tag entidad);
        Tag Update(Tag entidad);
        bool Delete(int id, string usuario);
        List<Tag> Add(List<Tag> listadoEntidad);
        List<Tag> Update(List<Tag> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<TagComboDTO> ObtenerCombo();
    }
}
