using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOperadorComparacionRepository : IGenericRepository<TOperadorComparacion>
    {
        #region Metodos Base
        TOperadorComparacion Add(OperadorComparacion entidad);
        TOperadorComparacion Update(OperadorComparacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOperadorComparacion> Add(IEnumerable<OperadorComparacion> listadoEntidad);
        IEnumerable<TOperadorComparacion> Update(IEnumerable<OperadorComparacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OperadorComparacionDTO> ObtenerOperadorComparacion();
        IEnumerable<ComboDTO> ObtenerCombo(); 
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();  
        public List<OperadoresComparacionDTO> ObtenerListado(); 
        IEnumerable<ComboDTO> ObtenerComboParaFilroSegmento(); 
    }
}