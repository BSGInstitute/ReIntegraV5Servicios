using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMontoPagoPlataformaRepository : IGenericRepository<TMontoPagoPlataforma>
    {
        #region Metodos Base
        TMontoPagoPlataforma Add(MontoPagoPlataforma entidad);
        TMontoPagoPlataforma Update(MontoPagoPlataforma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPagoPlataforma> Add(IEnumerable<MontoPagoPlataforma> listadoEntidad);
        IEnumerable<TMontoPagoPlataforma> Update(IEnumerable<MontoPagoPlataforma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ValorIntDTO> ObtenerPorIdMontoPago(int idMontoPago);
        IEnumerable<ValorIntDTO> ObtenerPorIdsMontoPago(IEnumerable<int> idsMontoPago);
        MontoPagoPlataforma ObtenerPorIdPlataformaPagoYIdMontoPago(int idPlataformaPago, int idMontoPago);
    }
}
