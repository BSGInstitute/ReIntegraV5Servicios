using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEstadoPespecificoRepository : IGenericRepository<TEstadoPespecifico>
    {
        #region Metodos Base
        TEstadoPespecifico Add(EstadoPespecifico entidad);
        TEstadoPespecifico Update(EstadoPespecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TEstadoPespecifico> Add(IEnumerable<EstadoPespecifico> listadoEntidad);
        IEnumerable<TEstadoPespecifico> Update(IEnumerable<EstadoPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstadoPespecifico> ObtenerTodo();
        EstadoPespecifico ObtenerPorId(int id);
        List<EstadoPespecifico> ObtenerPorIds(List<int> ids);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
