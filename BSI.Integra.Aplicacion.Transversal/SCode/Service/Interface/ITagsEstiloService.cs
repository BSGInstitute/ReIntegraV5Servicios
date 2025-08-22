using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITagsEstiloService
    {

        #region Metodos Base
        TagsEstilo Add(TagsEstiloEnvio entidad);
        TagsEstilo Update(TagsEstiloEnvio entidad);
        bool Delete(int id, string usuario);

        List<TagsEstilo> Add(List<TagsEstilo> listadoEntidad);
        List<TagsEstilo> Update(List<TagsEstilo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        //IEnumerable<ObtenerFiltro> ObtenerFiltro();
        IEnumerable<TagsEstilo> ObtenerTagsEstilo();
        IEnumerable<EstiloValor> ObtenerEstiloValor(int id);


    }
}
