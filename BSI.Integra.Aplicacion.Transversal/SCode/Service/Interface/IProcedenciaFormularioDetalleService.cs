using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProcedenciaFormularioDetalleService
    {
        #region Metodos Base
        ProcedenciaFormularioDetalle Add(ProcedenciaFormularioDetalle entidad);
        ProcedenciaFormularioDetalle Update(ProcedenciaFormularioDetalle entidad);
        bool Delete(int id, string usuario);

        List<ProcedenciaFormularioDetalle> Add(List<ProcedenciaFormularioDetalleDTO> listadoEntidad, string Usuario);
        List<ProcedenciaFormularioDetalle> Update(List<ProcedenciaFormularioDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<ProcedenciaFormularioDetalleDTO> ObtenerProcedenciaFormularioDetalle();
        IEnumerable<ProcedenciaFormularioDetalleInteraccionDTO> ObtenerProcedenciaFormularioDetallePorIdProcedenciaFormulario(int IdProcedenciaFormulario);
    }
}
