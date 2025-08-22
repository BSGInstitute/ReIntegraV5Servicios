using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaDetalleCambioRepository : IGenericRepository<TCronogramaDetalleCambio>
    {
        #region Metodos Base
        TCronogramaDetalleCambio Add(CronogramaDetalleCambio entidad);
        TCronogramaDetalleCambio Update(CronogramaDetalleCambio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaDetalleCambio> Add(IEnumerable<CronogramaDetalleCambio> listadoEntidad);
        IEnumerable<TCronogramaDetalleCambio> Update(IEnumerable<CronogramaDetalleCambio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CambioCronogramaDTO> ObtenerCambiosPendientes(int idMatriculaCabecera, int version);
    }
}
