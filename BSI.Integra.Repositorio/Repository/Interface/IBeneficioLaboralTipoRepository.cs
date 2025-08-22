using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBeneficioLaboralTipoRepository : IGenericRepository<TBeneficioLaboralTipo>
    {
        #region Metodos Base
        TBeneficioLaboralTipo Add(BeneficioLaboralTipo entidad);
        TBeneficioLaboralTipo Update(BeneficioLaboralTipo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TBeneficioLaboralTipo> Add(IEnumerable<BeneficioLaboralTipo> listadoEntidad);
        IEnumerable<TBeneficioLaboralTipo> Update(IEnumerable<BeneficioLaboralTipo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


    }
}
