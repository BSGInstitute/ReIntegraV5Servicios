using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoBusquedumRepository : IGenericRepository<TDataCreditoBusquedum>
    {
        #region Metodos Base
        TDataCreditoBusquedum Add(DataCreditoBusquedum entidad);
        TDataCreditoBusquedum Update(DataCreditoBusquedum entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoBusquedum> Add(IEnumerable<DataCreditoBusquedum> listadoEntidad);
        IEnumerable<TDataCreditoBusquedum> Update(IEnumerable<DataCreditoBusquedum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        DataCreditoDataDTO ObtenerIdDataCreditoDeAlumnoPorId(int idAlumno);
        DataCreditoInformacionDTO ObtenerInformacionDataCreditoPorId(int idDataCredito);
        List<DataCreditoTarjetaCreditoDTO> ObtenerHistorialTarjetasDataCreditoPorId(int idDataCredito);
        List<DataCreditoCreditoVigenteDTO> ObtenerHistorialDeudasDataCreditoPorId(int idDataCredito);
    }
}
