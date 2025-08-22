using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICargoRepository : IGenericRepository<TCargo>
    {
        #region Metodos Base
        TCargo Add(Cargo entidad);
        TCargo Update(Cargo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCargo> Add(IEnumerable<Cargo> listadoEntidad);
        IEnumerable<TCargo> Update(IEnumerable<Cargo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Cargo ObtenerPorId(int id);
        IEnumerable<Cargo> ObtenerTodo();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ComboDTO> ObtenerCargoFiltro();
    }
}