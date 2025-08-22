using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IPlanContableService
    {
        #region Metodos Base
        PlanContable Add(PlanContableDatosDTO entidad, string Usuario);
        PlanContable Update(PlanContableDatosDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<PlanContable> Add(List<PlanContable> listadoEntidad);
        List<PlanContable> Update(List<PlanContable> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PlanContableDTO> ObtenerPlanContable();
        IEnumerable<PlanContableComboDTO> ObtenerCombo();
        IEnumerable<PlanContableDTO> ObteneCuentasHijo(long cuenta);
        IEnumerable<PlanContableCuentasDTO> ObtenerPlanContableAutoComplete();
        IEnumerable<PlanContableComboDTO> ObtenerPlanContableFiltro(string NombreParcial);
        IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro();
        IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro(int IdPlanContable);

        PlanContable InsertarCuentaContable(PlanContableDatosDTO Json, string Usuario);
        PlanContable ActualizarCuentaContable(PlanContableDatosDTO Json, string Usuario);
        bool EliminarCuentaContable(int id, string usuario);
        IEnumerable<DTO.ComboDTO> ObtenerPlanContableTipoCuenta();
        public List<PlanContableFiltroDTO> ObtenerPlanContableAutoComplete(string NombreParcial);
    }
}
