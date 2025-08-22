using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;


namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPresentacionArgumentoRepository
    {
        #region Metodos Base
        TProgramaGeneralPresentacionArgumento Add(ProgramaGeneralPresentacionArgumento entidad);
        TProgramaGeneralPresentacionArgumento Update(ProgramaGeneralPresentacionArgumento entidad);
        bool Delete(int id, string usuario);
        bool Exist(int id);

        IEnumerable<TProgramaGeneralPresentacionArgumento> Add(IEnumerable<ProgramaGeneralPresentacionArgumento> listadoEntidad);
        IEnumerable<TProgramaGeneralPresentacionArgumento> Update(IEnumerable<ProgramaGeneralPresentacionArgumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        IEnumerable<ProgramaGeneralPresentacionArgumentoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        ProgramaGeneralPresentacionArgumento? ObtenerPorId(int id);
        List<CompuestoPresentacionArgumentoModalidadAlternoDTO> ObtenePresentacionArgumentoPorModalidades(int idPGeneral);
        List<ProgramaGeneralPresentacionArgumentoAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(int idOportunidad);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerProgramaGeneralPresentacionArgumentoHtml(int idPGeneral);
    }
}
