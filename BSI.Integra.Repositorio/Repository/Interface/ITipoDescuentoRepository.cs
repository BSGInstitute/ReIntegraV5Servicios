using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoDescuentoRepository : IGenericRepository<TTipoDescuento>
    {
        #region Metodos Base
        TTipoDescuento Add(TipoDescuento entidad);
        TTipoDescuento Update(TipoDescuento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoDescuento> Add(IEnumerable<TipoDescuento> listadoEntidad);
        IEnumerable<TTipoDescuento> Update(IEnumerable<TipoDescuento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoDescuentoDTO> ObtenerTipoDescuento();
        TipoDescuento ObtenerPorId(int idTipoDescuento);
        Task<TipoDescuento> ObtenerPorIdAsync(int idTipoDescuento);
        IEnumerable<TipoDescuentoComboDTO> ObtenerCombo();
        Task<IEnumerable<TipoDescuentoComboDTO>> ObtenerComboAsync();
        IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoOportunidad(int idOportunidad, string tipoPersonal);
        IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoSolicitudOportunidad(int idOportunidad);
        IEnumerable<TipoDescuento> ObtenerPorIds(int idTipoDescuento);
        IEnumerable<TipoDescuentoConNivelAprobacionDTO> ObtenerTipoDescuentoConNivelAprobacion();
        IEnumerable<TipoDescuentoNivelAprobacionDTO> ObtenerNivelesAprobacion();
    }
}