using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoPagoRepository : IGenericRepository<TDocumentoPago>
    {
        #region Metodos Base
        TDocumentoPago Add(DocumentoPago entidad);
        TDocumentoPago Update(DocumentoPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoPago> Add(IEnumerable<DocumentoPago> listadoEntidad);
        IEnumerable<TDocumentoPago> Update(IEnumerable<DocumentoPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
}
