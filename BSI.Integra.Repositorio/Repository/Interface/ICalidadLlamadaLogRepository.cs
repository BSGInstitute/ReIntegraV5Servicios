using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICalidadLlamadaLogRepository : IGenericRepository<TCalidadLlamadaLog>
    {
        #region Metodos Base
        TCalidadLlamadaLog Add(CalidadLlamadaLog entidad);
        TCalidadLlamadaLog AddAsync(CalidadLlamadaLog entidad);
        TCalidadLlamadaLog Update(CalidadLlamadaLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCalidadLlamadaLog> Add(IEnumerable<CalidadLlamadaLog> listadoEntidad);
        IEnumerable<TCalidadLlamadaLog> Update(IEnumerable<CalidadLlamadaLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CalidadLlamadaLogDTO> ObtenerCalidadLlamadaLog();
    }
}