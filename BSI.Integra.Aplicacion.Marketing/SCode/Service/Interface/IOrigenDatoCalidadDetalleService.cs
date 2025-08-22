using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOrigenDatoCalidadDetalleService
    {
        #region Metodos Base
        OrigenDatoCalidadDetalle Add(OrigenDatoCalidadDetalle entidad);
        OrigenDatoCalidadDetalle Update(OrigenDatoCalidadDetalle entidad);
        bool Delete(int id, string usuario);

        List<OrigenDatoCalidadDetalle> Add(List<OrigenDatoCalidadDetalle> listadoEntidad);
        List<OrigenDatoCalidadDetalle> Update(List<OrigenDatoCalidadDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        OrigenDatoCalidadDetalleConfiguracionDTO ObtenerOrigenSectorConfigurado(int IdOrigenSector);
    }
}
