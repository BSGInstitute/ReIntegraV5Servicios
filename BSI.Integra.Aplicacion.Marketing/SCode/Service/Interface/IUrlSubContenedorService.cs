using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IUrlSubContenedorService
    {
        #region Metodos Base
        UrlSubContenedor Add(UrlSubContenedor entidad);
        UrlSubContenedor Update(UrlSubContenedor entidad);
        bool Delete(int id, string usuario);

        List<UrlSubContenedor> Add(List<UrlSubContenedor> listadoEntidad);
        List<UrlSubContenedor> Update(List<UrlSubContenedor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<UrlSubContenedorDTO> ObtenerRutaSubContenedor(int idSubContenedor);


    }
}
