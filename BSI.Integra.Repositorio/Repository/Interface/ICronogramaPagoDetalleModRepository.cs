using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoDetalleModRepository : IGenericRepository<TCronogramaPagoDetalleMod>
    {
        #region Metodos Base
        TCronogramaPagoDetalleMod Add(CronogramaPagoDetalleMod entidad);
        TCronogramaPagoDetalleMod Update(CronogramaPagoDetalleMod entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPagoDetalleMod> Add(IEnumerable<CronogramaPagoDetalleMod> listadoEntidad);
        IEnumerable<TCronogramaPagoDetalleMod> Update(IEnumerable<CronogramaPagoDetalleMod> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
       
    }
}
