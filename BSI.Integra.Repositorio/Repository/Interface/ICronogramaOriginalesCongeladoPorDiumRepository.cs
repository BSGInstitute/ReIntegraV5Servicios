using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaOriginalesCongeladoPorDiumRepository : IGenericRepository<TCronogramaOriginalesCongeladoPorDium>
    {
        #region Metodos Base
        TCronogramaOriginalesCongeladoPorDium Add(CronogramaOriginalesCongeladoPorDium entidad);
        TCronogramaOriginalesCongeladoPorDium Update(CronogramaOriginalesCongeladoPorDium entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCronogramaOriginalesCongeladoPorDium> Add(IEnumerable<CronogramaOriginalesCongeladoPorDium> listadoEntidad);
        IEnumerable<TCronogramaOriginalesCongeladoPorDium> Update(IEnumerable<CronogramaOriginalesCongeladoPorDium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public bool ActualizarCronogramaCongeladosOriginales(ActualizarCronogramaCongeladoOriginalesDTO datos, string usuario);

    }
}
