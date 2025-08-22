using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDetalleOportunidadCompetidorRepository : IGenericRepository<TDetalleOportunidadCompetidor>
    {
        #region Metodos Base
        TDetalleOportunidadCompetidor Add(DetalleOportunidadCompetidor entidad);
        TDetalleOportunidadCompetidor Update(DetalleOportunidadCompetidor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDetalleOportunidadCompetidor> Add(IEnumerable<DetalleOportunidadCompetidor> listadoEntidad);
        IEnumerable<TDetalleOportunidadCompetidor> Update(IEnumerable<DetalleOportunidadCompetidor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetalleOportunidadCompetidor();
        IEnumerable<DetalleOportunidadCompetidorComboDTO> ObtenerCombo();
        IEnumerable<DetalleOportunidadCompetidorEmpresaDTO> ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(int idOportunidadCompetidor);
        IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetallePorIdOportunidadCompetidor(int idOportunidadCompetidor);
        Task<IEnumerable<DetalleOportunidadCompetidorDTO>> ObtenerDetallePorIdOportunidadCompetidorAsync(int idOportunidadCompetidor);
        DetalleOportunidadCompetidorDTO ObtenerDetallePorDatosCompetidor(int idOportunidadCompetidor, int idCompetidor);
        DetalleOportunidadCompetidor ObtenerPorIdOportunidaCompetidorIdCompetidor(int idOportunidadCompetidor, int idCompetidor);
        Task<DetalleOportunidadCompetidor> ObtenerPorIdOportunidaCompetidorIdCompetidorAsync(int idOportunidadCompetidor, int idCompetidor);
        Task<List<DetalleOportunidadCompetidor>> ObtenerPorIdOportunidaCompetidorIdsCompetidorAsync(int idOportunidadCompetidor, List<int> idCompetidor);
    }
}