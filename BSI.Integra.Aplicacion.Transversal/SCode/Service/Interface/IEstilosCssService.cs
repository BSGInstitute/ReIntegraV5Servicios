using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstilosCssService
    {

        #region Metodos Base
        EstilosCss Add(EstilosCssEnvio entidad);
        EstilosCss Update(EstilosCssEnvio entidad);
        bool Delete(int id, string usuario);

        List<EstilosCss> Add(List<EstilosCss> listadoEntidad);
        List<EstilosCss> Update(List<EstilosCss> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstiloCombo> ObtenerCombo();
        IEnumerable<EstilosCss> ObtenerEstilosCss();

        IEnumerable<EstiloCombo> ObtenerComboTagEstilo(int id);


    }
}
