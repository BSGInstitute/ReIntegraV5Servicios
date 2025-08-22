using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IUrlSubContenedorRepository : IGenericRepository<TUrlSubContenedor>
    {
        #region Metodos Base
        TUrlSubContenedor Add(UrlSubContenedor entidad);
        TUrlSubContenedor Update(UrlSubContenedor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TUrlSubContenedor> Add(IEnumerable<UrlSubContenedor> listadoEntidad);
        IEnumerable<TUrlSubContenedor> Update(IEnumerable<UrlSubContenedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        IEnumerable<UrlSubContenedorDTO> ObtenerRutaSubContenedor(int IdUrlSubContenedor);

    }
}
