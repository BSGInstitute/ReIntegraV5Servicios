using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IReporteEncuestasSincronicoService
    {
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaInicialSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaIntermediaSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaFinalSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public IEnumerable<ComboDTO> ObtenerComboDocentes();
        public List<ReporteEncuestasDocenteDTO>? GenerarReporteEncuestaDocente(ReporteEncuestaFiltroSincronicoDTO filtro);
		public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioPorModalidad(filtroTestimonioDTO filtro);
        public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioASincronico(filtroTestimonioDTO filtro);
        public List<ReportePGeneralValoracion>? GenerarReporteValoracionTotal(filtroValoracionDTO filtro);
        List<ComboDTO> ObtenerRespuestaEncuestaCombo(FiltroRespuestaCombo filtro);
        bool GuardarTestimonio(TestimonioInsertarDTO dto, string usuario);
        bool ActualizarValoracionVisiblePw(ValoracionesActualizarDTO dto, string usuario);

    }
}
