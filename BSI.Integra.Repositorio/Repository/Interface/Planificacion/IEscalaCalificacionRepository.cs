using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEscalaCalificacionRepository : IGenericRepository<TEscalaCalificacion>
    {
        #region Metodos Base
        TEscalaCalificacion Add(EscalaCalificacion entidad);
        TEscalaCalificacion Update(EscalaCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TEscalaCalificacion> Add(IEnumerable<EscalaCalificacion> listadoEntidad);
        IEnumerable<TEscalaCalificacion> Update(IEnumerable<EscalaCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EscalaCalificacionDTO> ObtenerTodo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        EscalaCalificacion ObtenerPorId(int id);
        List<EscalaCalificacion> ObtenerPorIds(List<int> id);
    }
}
