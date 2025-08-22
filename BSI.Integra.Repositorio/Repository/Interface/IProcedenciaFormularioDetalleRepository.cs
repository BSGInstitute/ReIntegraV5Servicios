using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProcedenciaFormularioDetalleRepository : IGenericRepository<TProcedenciaFormularioDetalle>
    {
        #region Metodos Base
        TProcedenciaFormularioDetalle Add(ProcedenciaFormularioDetalle entidad);
        TProcedenciaFormularioDetalle Update(ProcedenciaFormularioDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProcedenciaFormularioDetalle> Add(IEnumerable<ProcedenciaFormularioDetalle> listadoEntidad);
        IEnumerable<TProcedenciaFormularioDetalle> Update(IEnumerable<ProcedenciaFormularioDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ProcedenciaFormularioDetalleDTO> ObtenerProcedenciaFormularioDetalle();
        IEnumerable<ProcedenciaFormularioDetalleInteraccionDTO> ObtenerProcedenciaFormularioDetallePorIdProcedenciaFormulario(int IdProcedenciaFormulario);
    }
}
