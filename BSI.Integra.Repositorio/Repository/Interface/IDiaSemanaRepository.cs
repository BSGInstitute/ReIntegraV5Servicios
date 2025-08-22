using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDiaSemanaRepository : IGenericRepository<TDiaSemana>
    {
        #region Metodos Base
        TDiaSemana Add(DiaSemana entidad);
        TDiaSemana Update(DiaSemana entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDiaSemana> Add(IEnumerable<DiaSemana> listadoEntidad);
        IEnumerable<TDiaSemana> Update(IEnumerable<DiaSemana> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync(); 
    }
}
