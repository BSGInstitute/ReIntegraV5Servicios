using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFlujoOcurrenciaRepository : IGenericRepository<TFlujoOcurrencium>
    {
        #region Metodos Base
        TFlujoOcurrencium Add(FlujoOcurrencia entidad);
        TFlujoOcurrencium Update(FlujoOcurrencia entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFlujoOcurrencium> Add(IEnumerable<FlujoOcurrencia> listadoEntidad);
        IEnumerable<TFlujoOcurrencium> Update(IEnumerable<FlujoOcurrencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FlujoOcurrencia? ObtenerPorId(int id);
        IEnumerable<FlujoOcurrenciaDetalleDTO>? ObtenerPorIdFlujoActividad(int idFlujoActividad);
    }
}
