using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IReporteEncuestasSincronicoRepository
    {
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaInicialSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaIntermediaSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaFinalSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro);
        public IEnumerable<ComboDTO> ObtenerComboDocentes();
        public List<ReporteEncuestasDocenteDTO>? GenerarReporteEncuestaDocente(ReporteEncuestaFiltroSincronicoDTO filtro);
		public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioSincronico(filtroTestimonioDTO filtro);
		public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioASincronico(filtroTestimonioDTO filtro);
        public List<ComboDTO> ObtenerRespuestaEncuestaCombo(FiltroRespuestaCombo filtro);
        TestimonioEncuestaObtenerDTO ObtenerDatosTestimonioSincronicoASinEstado(int id, int modalidad);
        void ActualizarTestimonio(TestimonioEntidadActualizarDTO dto, string usuario);
        void GuardarTestimonio(TestimonioEntidadDTO dto, string usuario);
        void ActualizarEstadoTestimonioEstado(TestimonioEntidadActualizarDTO dto, string usuario);
        public List<ReportePGeneralValoracion>? GenerarReporteValoracionTotal(filtroValoracionDTO filtro);
        void ActualizarVisibleValoracionEncuesta(int id, int modalidad, string usuario);
    }
}
