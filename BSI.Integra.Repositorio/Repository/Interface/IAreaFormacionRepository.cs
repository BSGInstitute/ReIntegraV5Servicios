using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAreaFormacionRepository : IGenericRepository<TAreaFormacion>
    {
        #region Metodos Base
        TAreaFormacion Add(AreaFormacion entidad);
        TAreaFormacion Update(AreaFormacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAreaFormacion> Add(IEnumerable<AreaFormacion> listadoEntidad);
        IEnumerable<TAreaFormacion> Update(IEnumerable<AreaFormacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AreaFormacionDTO> ObtenerAreaFormacion();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ComboDTO> ObtenerAreaFormacionFiltro();
        AreaFormacion? ObtenerPorId(int id);
    }
}