using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoProyeccionFurRepository : IGenericRepository<TEstadoProyeccionFur>
    {
        #region Metodos Base
        TEstadoProyeccionFur Add(EstadoProyeccionFur entidad);
        TEstadoProyeccionFur Update(EstadoProyeccionFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoProyeccionFur> Add(IEnumerable<EstadoProyeccionFur> listadoEntidad);
        IEnumerable<TEstadoProyeccionFur> Update(IEnumerable<EstadoProyeccionFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public EstadoProyeccionFur ObtenerEstadoProyeccionFurById(int Id);
        public IEnumerable<EstadoProyeccionFurDTO> ObtenerComboEstadoProyeccionFur();

    }
}
