using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICronogramaCabeceraCambioService
    {
        #region Metodos Base
        CronogramaCabeceraCambio Add(CronogramaCabeceraCambio entidad);
        CronogramaCabeceraCambio Update(CronogramaCabeceraCambio entidad);
        bool Delete(int id, string usuario);

        List<CronogramaCabeceraCambio> Add(List<CronogramaCabeceraCambio> listadoEntidad);
        List<CronogramaCabeceraCambio> Update(List<CronogramaCabeceraCambio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


        public object ObtenerSolicitudesCambios(int idPersonal);
        public object Aprobar(CronogramaCabeceraCambioAprobarDTO cronogramaDTO);
        public object Rechazar(CronogramaCabeceraCambioAprobarDTO cronogramaDTO);

    }
}
