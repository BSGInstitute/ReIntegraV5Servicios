using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICronogramaPagoService
    {
        #region Metodos Base
        CronogramaPago Add(CronogramaPago entidad);
        CronogramaPago Update(CronogramaPago entidad);
        bool Delete(int id, string usuario);

        List<CronogramaPago> Add(List<CronogramaPago> listadoEntidad);
        List<CronogramaPago> Update(List<CronogramaPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CronogramaPagoDTO> ObtenerCronogramaPago();
        IEnumerable<CronogramaPagoComboDTO> ObtenerCombo();
        ProgramaCuotasDTO ObtenerProgramaCuotasPorIdMatriculaCabecera(int idMatriculaCabecera);
        object ObtenerCronogramaPagoPorCodigoMatricula(string CodigoMatricula);
        public object ActualizarCronogramaPago(CronogramaPagoAlumnoDTO CronogramaPago);
    }
}
