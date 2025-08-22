using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReportePagoTasaAcademicaRepository
    {
        public List<ComboConceptoDTO> ObtenercomboConcepto(string nombre);
        public List<ReporteTasasAcademicasDTO> ObtenerReportePagosTasasAcademicas(filtroReporteTasaAcademicaDTO filtro);
    }
}
