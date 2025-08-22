using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoAnuncioFacebookRepository : IGenericRepository<TConjuntoAnuncioFacebook>
    {
        #region Metodos Base
        TConjuntoAnuncioFacebook Add(ConjuntoAnuncioFacebook entidad);
        TConjuntoAnuncioFacebook Update(ConjuntoAnuncioFacebook entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConjuntoAnuncioFacebook> Add(IEnumerable<ConjuntoAnuncioFacebook> listadoEntidad);
        IEnumerable<TConjuntoAnuncioFacebook> Update(IEnumerable<ConjuntoAnuncioFacebook> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
      
    }
}
