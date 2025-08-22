using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoDetalleModLogFinalRepository : IGenericRepository<TCronogramaPagoDetalleModLogFinal>
    {
        #region Metodos Base
        TCronogramaPagoDetalleModLogFinal Add(CronogramaPagoDetalleModLogFinal entidad);
        TCronogramaPagoDetalleModLogFinal Update(CronogramaPagoDetalleModLogFinal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPagoDetalleModLogFinal> Add(IEnumerable<CronogramaPagoDetalleModLogFinal> listadoEntidad);
        IEnumerable<TCronogramaPagoDetalleModLogFinal> Update(IEnumerable<CronogramaPagoDetalleModLogFinal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        
    }
}
