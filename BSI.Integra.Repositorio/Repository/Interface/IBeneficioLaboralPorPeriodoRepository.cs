using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBeneficioLaboralPorPeriodoRepository : IGenericRepository<TBeneficioLaboralPorPeriodo>
    {
        #region Metodos Base
        TBeneficioLaboralPorPeriodo Add(BeneficioLaboralPorPeriodo entidad);
        TBeneficioLaboralPorPeriodo Update(BeneficioLaboralPorPeriodo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TBeneficioLaboralPorPeriodo> Add(IEnumerable<BeneficioLaboralPorPeriodo> listadoEntidad);
        IEnumerable<TBeneficioLaboralPorPeriodo> Update(IEnumerable<BeneficioLaboralPorPeriodo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<BeneficioLaboralVentasDTO> ObtenerBeneficioLaboralVentasPorPeriodo(int IdPeriodo);
    }
}
