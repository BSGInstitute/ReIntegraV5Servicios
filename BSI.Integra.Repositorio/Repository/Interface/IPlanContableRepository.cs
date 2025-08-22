using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPlanContableRepository : IGenericRepository<TPlanContable>
    {
        #region Metodos Base
        TPlanContable Add(PlanContable entidad);
        TPlanContable Update(PlanContable entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPlanContable> Add(IEnumerable<PlanContable> listadoEntidad);
        IEnumerable<TPlanContable> Update(IEnumerable<PlanContable> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PlanContableDTO> ObtenerPlanContable();
        IEnumerable<PlanContableComboDTO> ObtenerCombo();
        IEnumerable<PlanContableDTO> ObteneCuentasHijo(long cuenta);
        IEnumerable<PlanContableCuentasDTO> ObtenerPlanContableAutoComplete();
        IEnumerable<PlanContableComboDTO> ObtenerPlanContableFiltro(string NombreParcial);
        IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro();
        IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro(int IdPlanContable);
        IEnumerable<ComboDTO> ObtenerPlanContableTipoCuenta();
        public List<PlanContableFiltroDTO> ObtenerPlanContableAutoComplete(string NombreParcial);
        PlanContable? ObtenerPlanContablePorCuenta(long cuenta);
        public bool ActualizarRubroPlanContable(long Padre, string Usuario, int? IdFurTipoSolicitud = 0);
    }
}