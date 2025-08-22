using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGrupoFiltroProgramaCriticoRepository : IGenericRepository<TGrupoFiltroProgramaCritico>
    {
        #region Metodos Base
        TGrupoFiltroProgramaCritico Add(GrupoFiltroProgramaCritico entidad);
        TGrupoFiltroProgramaCritico Update(GrupoFiltroProgramaCritico entidad);
        bool gru(int id, string usuario);

        IEnumerable<TGrupoFiltroProgramaCritico> Add(IEnumerable<GrupoFiltroProgramaCritico> listadoEntidad);
        IEnumerable<TGrupoFiltroProgramaCritico> Update(IEnumerable<GrupoFiltroProgramaCritico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        public List<DatosPersonalAsesorDTO> ObtenerTodoPersonalAsesoresFiltro();
        public List<GrupoFiltroProgramaCriticoDTO> ObtenerTodoGrid();
        public List<PGeneralSubAreaDTO> ObtenerPorIdGrupo(int idGrupo);
        public List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO> ObtenerReporteProgramasCriticosAsignacion(ReporteProgramasCriticosFiltroDTO filtros);
        public List<FiltroIdNombreDTO> ObtenerFiltro();
        public List<ReporteProgramasCriticosDTO> ObtenerReporteProgramasCriticos(ReporteProgramasCriticosFiltroDTO filtros);



    }
}
