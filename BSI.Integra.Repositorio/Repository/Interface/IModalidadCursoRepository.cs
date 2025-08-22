using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IModalidadCursoRepository : IGenericRepository<TModalidadCurso>
    {
        #region Metodos Base
        TModalidadCurso Add(ModalidadCurso entidad);
        TModalidadCurso Update(ModalidadCurso entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TModalidadCurso> Add(IEnumerable<ModalidadCurso> listadoEntidad);
        IEnumerable<TModalidadCurso> Update(IEnumerable<ModalidadCurso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModalidadCurso? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ModalidadCurso>? ObtenerPorIds(List<int> ids);
        IEnumerable<ModalidadCursoDTO> Obtener();
    }
}