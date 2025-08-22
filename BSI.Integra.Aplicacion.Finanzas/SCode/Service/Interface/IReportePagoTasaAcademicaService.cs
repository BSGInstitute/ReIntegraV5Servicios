using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePagoTasaAcademicaService
    {
        public List<ComboConceptoDTO> ObtenercomboConcepto(string nombre);
        public List<ReporteTasasAcademicasDTO> ObtenerReportePagosTasasAcademicas(filtroReporteTasaAcademicaDTO filtro);
    }
}
