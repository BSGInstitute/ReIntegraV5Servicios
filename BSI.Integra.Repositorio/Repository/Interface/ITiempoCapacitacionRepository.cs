using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITiempoCapacitacionRepository : IGenericRepository<TTiempoCapacitacion>
    {
        #region Metodos Base
        TTiempoCapacitacion Add(TiempoCapacitacion entidad);
        TTiempoCapacitacion Update(TiempoCapacitacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTiempoCapacitacion> Add(IEnumerable<TiempoCapacitacion> listadoEntidad);
        IEnumerable<TTiempoCapacitacion> Update(IEnumerable<TiempoCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TiempoCapacitacionDTO> ObtenerTiempoCapacitacion();
        List<TiempoCapacitacionComboDTO> ObtenerCombo();
        Task<List<TiempoCapacitacionComboDTO>> ObtenerComboAsync();
    }
}