using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIndustriaRepository : IGenericRepository<TIndustrium>
    {
        #region Metodos Base
        TIndustrium Add(Industria entidad);
        TIndustrium Update(Industria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TIndustrium> Add(IEnumerable<Industria> listadoEntidad);
        IEnumerable<TIndustrium> Update(IEnumerable<Industria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();

        IEnumerable<ComboDTO> ObtenerComboTiempoExperiencia();

        IEnumerable<ComboDTO> ObtenerComboTamanioEmpresa();
    }
}