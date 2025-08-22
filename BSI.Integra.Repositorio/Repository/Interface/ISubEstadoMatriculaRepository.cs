using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISubEstadoMatriculaRepository : IGenericRepository<TSubEstadoMatricula>
    {
        #region Metodos Base
        TSubEstadoMatricula Add(SubEstadoMatricula entidad);
        TSubEstadoMatricula Update(SubEstadoMatricula entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSubEstadoMatricula> Add(IEnumerable<SubEstadoMatricula> listadoEntidad);
        IEnumerable<TSubEstadoMatricula> Update(IEnumerable<SubEstadoMatricula> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TCRM_SubEstadoMatriculaDTO> ObtenerSubEstadoMatricula();
        IEnumerable<SubEstadoMatriculaComboDTO> ObtenerCombo();
        IEnumerable<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatriculaFiltro();
        Task<IEnumerable<SubEstadoMatriculaFiltroDTO>> ObtenerSubEstadoMatriculaFiltroAsync();
    }
}
