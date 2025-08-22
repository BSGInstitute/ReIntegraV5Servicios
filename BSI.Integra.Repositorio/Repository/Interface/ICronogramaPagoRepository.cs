using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICronogramaPagoRepository : IGenericRepository<TCronogramaPago>
    {
        #region Metodos Base
        TCronogramaPago Add(CronogramaPago entidad);
        TCronogramaPago Update(CronogramaPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCronogramaPago> Add(IEnumerable<CronogramaPago> listadoEntidad);
        IEnumerable<TCronogramaPago> Update(IEnumerable<CronogramaPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CronogramaPagoDTO> ObtenerCronogramaPago();
        IEnumerable<CronogramaPagoComboDTO> ObtenerCombo();
        ProgramaCuotasDTO ObtenerProgramaCuotasPorIdMatriculaCabecera(int idMatriculaCabecera);
        public ComprobanteRecienteDTO ObtenerComprobanteReciente(int idMatricula = 0);
        int ObtenerIdporCodigo(string codmat);
        public bool ActualizarCompromisoPago(int IdMatriculaCabecera, string Usuario);
        PrecioFinalProgramaAlumnoDTO ObtenerPrecioFinalProgramaAlumno(string codigoMatricula);
    }
}