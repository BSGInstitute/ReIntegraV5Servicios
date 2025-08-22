using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICourierRepository : IGenericRepository<TCourier>
    {
        #region Metodos Base
        TCourier Add(Courier entidad);
        TCourier Update(Courier entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCourier> Add(IEnumerable<Courier> listadoEntidad);
        IEnumerable<TCourier> Update(IEnumerable<Courier> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CourierDTO> ObtenerCourier();
        Courier ObtenerPorId(int id);
        List<Courier> ObtenerPorIds(List<int> id);
    }
}
