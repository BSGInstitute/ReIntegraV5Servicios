using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoMatriculaService
    {
        #region Metodos Base
        EstadoMatricula Add(EstadoMatricula entidad);
        EstadoMatricula Update(EstadoMatricula entidad);
        bool Delete(int id, string usuario);

        List<EstadoMatricula> Add(List<EstadoMatricula> listadoEntidad);
        List<EstadoMatricula> Update(List<EstadoMatricula> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaCombo();
        IEnumerable<EstadoMatriculaDTO> ObtenerEstadoMatricula();
        bool EliminarEstadoSubEstado(int id, string usuario);
        IEnumerable<EstadoMatriculaComboDTO> ObtenerEstadoMatriculaParaMatriculados();
        SubEstadoMatriculaDTO ObtenerSubEstadoIndividual(int IdEstadoMatricula);
        EstadoMatriculaListDTO InsertarEstadoSubestado(CRUDEstadoMatriculaDTO data);
        EstadoMatriculaListDTO EditarEstado(CRUDEstadoMatriculaDTO data);
        List<ComboDTO> ObtenerCombo();
        List<ObtenerEstadoMatriculaDTO> ObtenerEstadosMatricula();
        List<TCRM_SubEstadoMatriculaDTO> ObtenerFiltroSubEstadoMatricula(int idEstadoMatricula);
    }
}
