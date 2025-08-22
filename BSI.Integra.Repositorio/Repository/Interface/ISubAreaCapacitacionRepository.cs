using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISubAreaCapacitacionRepository : IGenericRepository<TSubAreaCapacitacion>
    {
        #region Metodos Base
        TSubAreaCapacitacion Add(SubAreaCapacitacion entidad);
        TSubAreaCapacitacion Update(SubAreaCapacitacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSubAreaCapacitacion> Add(IEnumerable<SubAreaCapacitacion> listadoEntidad);
        IEnumerable<TSubAreaCapacitacion> Update(IEnumerable<SubAreaCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SubAreaCapacitacionFiltroDTO> ObtenerFiltro();
        Task<IEnumerable<SubAreaCapacitacionFiltroDTO>> ObtenerFiltroAsync();
        IEnumerable<SubAreaCapacitacion> Obtener();
        IEnumerable<SubAreaCapacitacionAlternoDTO> ObtenerAlterno();
        List<SubAreaCapacitacionFiltroDTO> ObtenerPorIdAreaCapacitacion(int idAreaCapacitacion);
        int ObtenerSubAreaCapacitacionAnterior(int idActualSubArea);
        SubAreaCapacitacion ObtenerPorId(int id);
        bool ExistePorId(int id);
        IEnumerable<ComboDTO> ObtenerSubAreaPorIdDeAreaLista(List<int> IdAreaCapacitacion);

    }
}