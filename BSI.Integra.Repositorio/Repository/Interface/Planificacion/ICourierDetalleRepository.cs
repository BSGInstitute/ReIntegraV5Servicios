using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICourierDetalleRepository : IGenericRepository<TCourierDetalle>
    {
        #region Metodos Base
        TCourierDetalle Add(CourierDetalle entidad);
        TCourierDetalle Update(CourierDetalle entidad);
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CourierDetalleDTO> ObtenerPorIdCourier(int IdCourier);
        CourierDetalle ObtenerPorId(int id);
        List<CourierDetalle> ObtenerPorIds(List<int> id);
    }

}
