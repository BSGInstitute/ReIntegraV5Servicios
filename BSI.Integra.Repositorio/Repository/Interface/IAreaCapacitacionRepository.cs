using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAreaCapacitacionRepository : IGenericRepository<TAreaCapacitacion>
    {
        #region Metodos Base
        TAreaCapacitacion Add(AreaCapacitacion entidad);
        TAreaCapacitacion Update(AreaCapacitacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAreaCapacitacion> Add(IEnumerable<AreaCapacitacion> listadoEntidad);
        IEnumerable<TAreaCapacitacion> Update(IEnumerable<AreaCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AreaCapacitacionDTO> Obtener();
        AreaCapacitacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        public int ObtenerAreaCapacitacionAnterior(int areaActual);
        IEnumerable<AreaCapacitacionFiltroDTO> ObtenerFiltro();
        Task<IEnumerable<AreaCapacitacionFiltroDTO>> ObtenerFiltroAsync();
    }
}