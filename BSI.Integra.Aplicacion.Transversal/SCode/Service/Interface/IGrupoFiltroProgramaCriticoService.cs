using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IGrupoFiltroProgramaCriticoService
    {
        #region Metodos Base
        GrupoFiltroProgramaCritico Add(GrupoFiltroProgramaCritico entidad);
        GrupoFiltroProgramaCritico Update(GrupoFiltroProgramaCritico entidad);
        bool Delete(int id, string usuario);

        List<GrupoFiltroProgramaCritico> Add(List<GrupoFiltroProgramaCritico> listadoEntidad);
        List<GrupoFiltroProgramaCritico> Update(List<GrupoFiltroProgramaCritico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        public List<DatosPersonalAsesorDTO> ObtenerTodoPersonalAsesoresFiltro();
        public List<GrupoFiltroProgramaCriticoDTO> ObtenerTodoGrid();
        public List<PGeneralSubAreaDTO> ObtenerPorIdGrupo(int idGrupo);
        public GrupoFiltroProgramaCritico Insertar(CompuestoGrupoFiltroProgramaCriticoDTO Json);
        public GrupoFiltroProgramaCritico Actualizar(CompuestoGrupoFiltroProgramaCriticoDTO Json);
        public List<ReporteEstructuradoAsignacionProgramasCriticosDTO> ObtenerReporteProgramasCriticosAsignacion(ReporteProgramasCriticosFiltroDTO filtros);
        public List<FiltroIdNombreDTO> ObtenerFiltro();
        public List<ReporteProgramasCriticosDTO> ObtenerReporteProgramasCriticos(ReporteProgramasCriticosFiltroDTO filtros);

    }
}
