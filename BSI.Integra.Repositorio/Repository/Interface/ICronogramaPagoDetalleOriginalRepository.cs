using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoDetalleOriginalRepository : IGenericRepository<TCronogramaPagoDetalleOriginal>
    {
        #region Metodos Base
        TCronogramaPagoDetalleOriginal Add(CronogramaPagoDetalleOriginal entidad);
        TCronogramaPagoDetalleOriginal Update(CronogramaPagoDetalleOriginal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPagoDetalleOriginal> Add(IEnumerable<CronogramaPagoDetalleOriginal> listadoEntidad);
        IEnumerable<TCronogramaPagoDetalleOriginal> Update(IEnumerable<CronogramaPagoDetalleOriginal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
}
