using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAnuncioFacebookRepository : IGenericRepository<TAnuncioFacebook>
    {
        #region Metodos Base
        TAnuncioFacebook Add(AnuncioFacebook entidad);
        TAnuncioFacebook Update(AnuncioFacebook entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAnuncioFacebook> Add(IEnumerable<AnuncioFacebook> listadoEntidad);
        IEnumerable<TAnuncioFacebook> Update(IEnumerable<AnuncioFacebook> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
}
