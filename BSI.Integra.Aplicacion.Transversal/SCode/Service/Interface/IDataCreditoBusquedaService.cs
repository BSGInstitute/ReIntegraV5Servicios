using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoBusquedumService
    {
        #region Metodos Base
        DataCreditoBusquedum Add(DataCreditoBusquedum entidad);
        DataCreditoBusquedum Update(DataCreditoBusquedum entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoBusquedum> Add(List<DataCreditoBusquedum> listadoEntidad);
        List<DataCreditoBusquedum> Update(List<DataCreditoBusquedum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        DataCreditoDataDTO ObtenerIdDataCreditoDeAlumnoPorId(int idAlumno);
        DataCreditoInformacionDTO ObtenerInformacionDataCreditoPorId(int idDataCredito);
        List<DataCreditoTarjetaCreditoDTO> ObtenerHistorialTarjetasDataCreditoPorId(int idDataCredito);
        List<DataCreditoCreditoVigenteDTO> ObtenerHistorialDeudasDataCreditoPorId(int idDataCredito);
    }
}
