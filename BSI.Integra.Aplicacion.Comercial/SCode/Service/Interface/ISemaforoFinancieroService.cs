using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISemaforoFinancieroService
    {
        #region Metodos Base
        SemaforoFinanciero Add(SemaforoFinanciero entidad);
        SemaforoFinanciero Update(SemaforoFinanciero entidad);
        bool Delete(int id, string usuario);
        List<SemaforoFinanciero> Add(List<SemaforoFinanciero> listadoEntidad);
        List<SemaforoFinanciero> Update(List<SemaforoFinanciero> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SemaforoFinancieroDTO> ObtenerSemaforoFinanciero();
        IEnumerable<SemaforoFinancieroComboDTO> ObtenerCombo();
        bool InsertarNuevoSemaforo(SemaforoFinanciero objeto);
        SemaforoFinanciero ObtenerSemaforoPorId(int id);
    }
}
