using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMontoPagoSuscripcionRepository : IGenericRepository<TMontoPagoSuscripcion>
    {
        #region Metodos Base
        TMontoPagoSuscripcion Add(MontoPagoSuscripcion entidad);
        TMontoPagoSuscripcion Update(MontoPagoSuscripcion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPagoSuscripcion> Add(IEnumerable<MontoPagoSuscripcion> listadoEntidad);
        IEnumerable<TMontoPagoSuscripcion> Update(IEnumerable<MontoPagoSuscripcion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ValorIntDTO> ObtenerPorIdMontoPago(int idMontoPago);
        IEnumerable<ValorIntDTO> ObtenerPorIdsMontoPago(IEnumerable<int> idsMontoPago);
    }
}
