using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICajaRepository : IGenericRepository<TCaja>
    {
        #region Metodos Base
        TCaja Add(Caja entidad);
        TCaja Update(Caja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCaja> Add(IEnumerable<Caja> listadoEntidad);
        IEnumerable<TCaja> Update(IEnumerable<Caja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CajaDTO> ObtenerCaja();
        IEnumerable<CajaComboDTO> ObtenerCombo();
        IEnumerable<CajaResponsableComboDTO> ObtenerListaCajaResponsable();
        int obtenerIdCuentaCorriente(int IdCaja);
        IEnumerable<ResumenCajaDTO> ObtenerResumenCaja();

    }
}
