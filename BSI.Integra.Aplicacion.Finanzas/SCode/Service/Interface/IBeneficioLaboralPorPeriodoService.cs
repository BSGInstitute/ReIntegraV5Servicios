using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IBeneficioLaboralPorPeriodoService
    {
        #region Metodos Base
        BeneficioLaboralPorPeriodo Add(BeneficioLaboralPorPeriodo entidad);
        BeneficioLaboralPorPeriodo Update(BeneficioLaboralPorPeriodo entidad);
        bool Delete(int id, string usuario);

        List<BeneficioLaboralPorPeriodo> Add(List<BeneficioLaboralPorPeriodo> listadoEntidad);
        List<BeneficioLaboralPorPeriodo> Update(List<BeneficioLaboralPorPeriodo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        bool InsertarBeneficioLaboralPorPeriodo(ListaBeneficioLaboralDTO ObjetoDTO);
        IEnumerable<BeneficioLaboralVentasDTO> ObtenerBeneficioLaboralSegunPeriodo(int IdPeriodo);
    }
}
