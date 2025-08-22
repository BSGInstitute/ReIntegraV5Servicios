using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProcedenciaFormularioService
    {
        #region Metodos Base
        public ProcedenciaFormulario Add(ProcedenciaFormularioDTO entidad, string Usuario);
        public ProcedenciaFormulario Update(ProcedenciaFormularioDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<ProcedenciaFormulario> Add(List<ProcedenciaFormulario> listadoEntidad);
        List<ProcedenciaFormulario> Update(List<ProcedenciaFormulario> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<ProcedenciaFormularioDTO> ObtenerProcedenciaFormulario();
        IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioFiltro();
        IEnumerable<ProcedenciaFormularioFiltroDTO> ObtenerProcedenciaFormularioTodo();
    }
}
