using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISubEstadoMatriculaService
    {
        #region Metodos Base
        SubEstadoMatricula Add(SubEStadoRecibidoDTO entidad,string usuario);
        SubEstadoMatricula Update(SubEStadoRecibidoDTO entidad, string usuario);
        bool Delete(int id, string usuario);

        List<SubEstadoMatricula> Add(List<SubEstadoMatricula> listadoEntidad);
        List<SubEstadoMatricula> Update(List<SubEstadoMatricula> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        IEnumerable<TCRM_SubEstadoMatriculaDTO> ObtenerSubEstadoMatricula();
        IEnumerable<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatriculaFiltro();
    }
}
