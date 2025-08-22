using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoListaDetalleRepository : IGenericRepository<TConjuntoListaDetalle>
    {

        #region Metodos Base
        TConjuntoListaDetalle Add(ConjuntoListaDetalleDTO entidad);
        TConjuntoListaDetalle Update(ConjuntoListaDetalleDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConjuntoListaDetalle> Add(IEnumerable<ConjuntoListaDetalleDTO> listadoEntidad);
        IEnumerable<TConjuntoListaDetalle> Update(IEnumerable<ConjuntoListaDetalleDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ConjuntoListaDetalleDTO> Obtener(int idConjuntoLista);
        List<ConjuntoListaDetalleMailingMasivoDTO> ObtenerListasMailingMasivo(int idConjuntoLista);

    }
}