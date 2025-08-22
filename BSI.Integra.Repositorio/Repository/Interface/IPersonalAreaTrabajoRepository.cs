using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPersonalAreaTrabajoRepository : IGenericRepository<TPersonalAreaTrabajo>
    {
        #region Metodos Base
        TPersonalAreaTrabajo Add(PersonalAreaTrabajo entidad);
        TPersonalAreaTrabajo Update(PersonalAreaTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalAreaTrabajo> Add(IEnumerable<PersonalAreaTrabajo> listadoEntidad);
        IEnumerable<TPersonalAreaTrabajo> Update(IEnumerable<PersonalAreaTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalAreaTrabajoDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        bool ExistePorId(int id);
        PersonalAreaTrabajo? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerTodoFiltroAreaTrabajo();
    }
}