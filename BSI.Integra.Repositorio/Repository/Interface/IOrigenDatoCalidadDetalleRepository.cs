using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOrigenDatoCalidadDetalleRepository : IGenericRepository<TOrigenDatoCalidadDetalle>
    {
        #region Metodos Base
        TOrigenDatoCalidadDetalle Add(OrigenDatoCalidadDetalle entidad);
        TOrigenDatoCalidadDetalle Update(OrigenDatoCalidadDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOrigenDatoCalidadDetalle> Add(IEnumerable<OrigenDatoCalidadDetalle> listadoEntidad);
        IEnumerable<TOrigenDatoCalidadDetalle> Update(IEnumerable<OrigenDatoCalidadDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<OrigenDatoCalidadDetalleDTO> ObtenerOrigenSectorConfigurado(int IdOrigenSector);

        NombreCantidadAgrupadoDTO ObtenerNombreOrigenDatoCalidadDetalleAgrupado(int IdOrigenSector);
        origenDatoCalidadDetalleConfiguracionAgrupadoDTO ObtenerOrigenSectorConfiguradoCategoriaAgrupado(int IdOrigenSector);
    }
}
