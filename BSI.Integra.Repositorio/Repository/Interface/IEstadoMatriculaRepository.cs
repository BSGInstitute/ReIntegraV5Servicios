using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoMatriculaRepository : IGenericRepository<TEstadoMatricula>
    {
        #region Metodos Base
        TEstadoMatricula Add(EstadoMatricula entidad);
        TEstadoMatricula Update(EstadoMatricula entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoMatricula> Add(IEnumerable<EstadoMatricula> listadoEntidad);
        IEnumerable<TEstadoMatricula> Update(IEnumerable<EstadoMatricula> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstadoMatriculaDTO> ObtenerEstadoMatricula();
        IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaCombo();
        EstadoMatriculaListDTO InsertarEstadoSubestado(CRUDEstadoMatriculaDTO data);
        bool EliminarEstadoSubEstado(int id, string usuario);
        IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaParaMatriculados();
        List<FiltroConfiguracionCoordinadoraEstadoMatriculaDTO> ObtenerTodoFiltroConfiguracionCoordinadora();
        List<SubEstadoMatriculaFiltroDTO> ObtenerComboOficialSubEstadoMatricula();
        SubEstadoMatriculaDTO ObtenerSubEstadoIndividual(int IdEstadoMatricula);
        EstadoMatriculaListDTO EditarEstado(CRUDEstadoMatriculaDTO data);
        List<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        List<ObtenerEstadoMatriculaDTO> ObtenerEstadosMatricula();
        List<TCRM_SubEstadoMatriculaDTO> ObtenerFiltroSubEstadoMatricula(int idEstadoMatricula);
    }
}
