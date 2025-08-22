using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionBeneficioProgramaGeneralService
    {
        #region Metodos Base
        ConfiguracionBeneficioProgramaGeneral Add(ConfiguracionBeneficioProgramaGeneral entidad);
        ConfiguracionBeneficioProgramaGeneral Update(ConfiguracionBeneficioProgramaGeneral entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionBeneficioProgramaGeneral> Add(List<ConfiguracionBeneficioProgramaGeneral> listadoEntidad);
        List<ConfiguracionBeneficioProgramaGeneral> Update(List<ConfiguracionBeneficioProgramaGeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<string> ObtenerDescripcionPGeneralConfiguracionBeneficios(int idPgeneral, int? idPais, int? idPaquete);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2Internacional(int idPGeneral);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2(int idPGeneral, int codigoPais);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais);
        BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral);
        BeneficioDetalleRequisitoDTO BeneficioDetalleRequisitoPorPGeneralYBeneficio(int idBeneficio, int idPGeneral);
    }
}
