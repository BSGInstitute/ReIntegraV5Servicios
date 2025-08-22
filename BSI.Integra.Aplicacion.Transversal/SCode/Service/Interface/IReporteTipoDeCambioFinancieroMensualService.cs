using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReporteTipoDeCambioFinancieroMensualService
    {
        #region Metodos Base
        ReporteTipoDeCambioFinancieroMensual Add(ReporteTipoDeCambioFinancieroMensualDTO entidad, string usuario);
        ReporteTipoDeCambioFinancieroMensual Update(ReporteTipoDeCambioFinancieroMensualDTO entidad, string usuario);
        bool Delete(int id, string usuario);

        List<ReporteTipoDeCambioFinancieroMensual> Add(List<ReporteTipoDeCambioFinancieroMensual> listadoEntidad);
        List<ReporteTipoDeCambioFinancieroMensual> Update(List<ReporteTipoDeCambioFinancieroMensual> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<ReporteTipoDeCambioFinancieroMensualEnvioDTO> ObtenerTipoPagoPorAnioYMes(ReporteTipoDeCambioFinanzcieroMensualGrillaDTO entidad);



    }
}
