using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoDetalleRepository : IGenericRepository<TCronogramaPagoDetalle>
    {
        #region Metodos Base
        TCronogramaPagoDetalle Add(CronogramaPagoDetalle entidad);
        TCronogramaPagoDetalle Update(CronogramaPagoDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPagoDetalle> Add(IEnumerable<CronogramaPagoDetalle> listadoEntidad);
        IEnumerable<TCronogramaPagoDetalle> Update(IEnumerable<CronogramaPagoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<CronogramaDetallePagoDTO> ObtenerListaDeCronogramaDetallePagoporIdMatricula(int IdMatriculaCabecera);
        List<int> ObtenerListaDeIdCronogramaDetallePagoporIdCabecera(int IdMatriculaCabecera);




    }
}
