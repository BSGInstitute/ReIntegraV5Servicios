using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IDetalleOportunidadCompetidorService
    {
        #region Metodos Base
        DetalleOportunidadCompetidor Add(DetalleOportunidadCompetidor entidad);
        DetalleOportunidadCompetidor Update(DetalleOportunidadCompetidor entidad);
        bool Delete(int id, string usuario);

        List<DetalleOportunidadCompetidor> Add(List<DetalleOportunidadCompetidor> listadoEntidad);
        List<DetalleOportunidadCompetidor> Update(List<DetalleOportunidadCompetidor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetalleOportunidadCompetidor();
        IEnumerable<DetalleOportunidadCompetidorComboDTO> ObtenerCombo();
        IEnumerable<DetalleOportunidadCompetidorEmpresaDTO> ObtenerEmpresaCompetidoraPorIdOportunidadCompetidor(int idOportunidadCompetidor);
        //void EliminarPorOportunidadCompetidor(int idOportunidadCompetidor, string usuario, List<int> nuevos);
        IEnumerable<DetalleOportunidadCompetidorDTO> ObtenerDetallePorIdOportunidadCompetidor(int idOportunidadCompetidor);
        DetalleOportunidadCompetidorDTO ObtenerDetallePorDatosCompetidor(int idOportunidadCompetidor, int idCompetidor);
        DetalleOportunidadCompetidor ObtenerPorIdOportunidaCompetidorIdCompetidor(int idOportunidadCompetidor, int idCompetidor);
    }
}
