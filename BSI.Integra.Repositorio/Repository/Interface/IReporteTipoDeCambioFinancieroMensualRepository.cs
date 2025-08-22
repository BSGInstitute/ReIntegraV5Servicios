using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteTipoDeCambioFinancieroMensualRepository : IGenericRepository<TReporteTipoDeCambioFinancieroMensual>
    {
        #region Metodos Base
        TReporteTipoDeCambioFinancieroMensual Add(ReporteTipoDeCambioFinancieroMensual entidad);
        TReporteTipoDeCambioFinancieroMensual Update(ReporteTipoDeCambioFinancieroMensual entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TReporteTipoDeCambioFinancieroMensual> Add(IEnumerable<ReporteTipoDeCambioFinancieroMensual> listadoEntidad);
        IEnumerable<TReporteTipoDeCambioFinancieroMensual> Update(IEnumerable<ReporteTipoDeCambioFinancieroMensual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<ReporteTipoDeCambioFinancieroMensualEnvioDTO> ObtenerTipoPagoPorAnioYMes(ReporteTipoDeCambioFinanzcieroMensualGrillaDTO entidad);

        IEnumerable<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO> ObtenerTipoCambioTotal();



    }
}