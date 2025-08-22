using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITagsService
    {

        #region Metodos Base
        Tags Add(TagsEnvio entidad);
        Tags Update(TagsEnvio entidad);
        bool Delete(int id, string usuario);

        List<Tags> Add(List<Tags> listadoEntidad);
        List<Tags> Update(List<Tags> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboTag> ObtenerCombo();
        IEnumerable<Tags> ObtenerTags();

    }
}
