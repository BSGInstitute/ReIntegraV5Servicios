using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProcedenciaFormularioRepository : IGenericRepository<TProcedenciaFormulario>
    {
        #region Metodos Base
        TProcedenciaFormulario Add(ProcedenciaFormulario entidad);
        TProcedenciaFormulario Update(ProcedenciaFormulario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProcedenciaFormulario> Add(IEnumerable<ProcedenciaFormulario> listadoEntidad);
        IEnumerable<TProcedenciaFormulario> Update(IEnumerable<ProcedenciaFormulario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ProcedenciaFormularioDTO> ObtenerProcedenciaFormulario();
        IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioFiltro();
        IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioTodo();
    }
}
