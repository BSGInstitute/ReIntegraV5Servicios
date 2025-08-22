using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IEntidadFinancieraService
    {
        #region Metodos Base
        EntidadFinanciera Add(EntidadFinancieraRecibidoDTO data, string Usuario);
        EntidadFinanciera Update(EntidadFinancieraRecibidoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<EntidadFinanciera> Add(List<EntidadFinanciera> listadoEntidad);
        List<EntidadFinanciera> Update(List<EntidadFinanciera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<EntidadFinancieraDTO> ObtenerEntidadFinanciera();
        IEnumerable<EntidadFinancieraComboDTO> ObtenerCombo();

    }
}
