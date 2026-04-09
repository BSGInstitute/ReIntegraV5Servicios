using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGestionPagoRepository : IGenericRepository<TGestionPago>
    {
        #region Metodos Base
        TGestionPago Add(GestionPago entidad);
        TGestionPago Update(GestionPago entidad);
        bool Delete(int idGestionPago, string usuario);

        IEnumerable<TGestionPago> Add(IEnumerable<GestionPago> listadoEntidad);
        IEnumerable<TGestionPago> Update(IEnumerable<GestionPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GestionPagoDTO> ObtenerGestionesPago(FiltroGestionPagoDTO filtro);
        GestionPagoDTO? ObtenerGestionPagoPorId(int idGestionPago);
        GestionPagoDTO? ObtenerGestionPagoPorComprobante(int idComprobantePago);
    }
}
