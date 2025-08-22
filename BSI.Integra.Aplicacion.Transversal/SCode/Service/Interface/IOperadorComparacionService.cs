using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOperadorComparacionService
    {
        #region Metodos Base
        OperadorComparacion Add(OperadorComparacion entidad);
        OperadorComparacion Update(OperadorComparacion entidad);
        bool Delete(int id, string usuario);

        List<OperadorComparacion> Add(List<OperadorComparacion> listadoEntidad);
        List<OperadorComparacion> Update(List<OperadorComparacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<OperadorComparacionDTO> ObtenerOperadorComparacion();
        IEnumerable<ComboDTO> ObtenerCombo(); 
        public List<OperadoresComparacionDTO> ObtenerListado();
    
        IEnumerable<DTO.ComboDTO> ObtenerComboParaFilroSegmento();
    }
}
