using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEmpresaRepository : IGenericRepository<TEmpresa>
    {
        #region Metodos Base
        TEmpresa Add(Empresa entidad);
        TEmpresa Update(Empresa entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEmpresa> Add(IEnumerable<Empresa> listadoEntidad);
        IEnumerable<TEmpresa> Update(IEnumerable<Empresa> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EmpresaDTO> ObtenerEmpresa();
        IEnumerable<EmpresaDTO> ObtenerEmpresa2();
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerAutocomplete(string nombreParcial);
        ValorIntDTO ObtenerIdTamanioEmpresaPorIdEmpresa(int idEmpresa);
        List<ComboDTO> ObtenerTodoCompetidores();
        IEnumerable<TipoIdentificadorComboDTO> ObtenerComboTipoIdentificador();
        IEnumerable<Aplicacion.DTO.ComboDTO> ObtenerComboTamanioEmpresa();
        IEnumerable<CodigoCiiuIndustriaComboDTO> ObtenerComboCodigoCiiuIndustria();
        ComboDTO ObtenerEmpresaPorId(int id);
        List<ComboDTO> CargarEmpresaAutoComplete(string nombre);
        Empresa ObtenerPorId(int id);
        List<EmpresaObtenerDTO> ObtenerEmpresas();
        EmpresaFiltroDTO ObtenerEmpresaFiltro(FiltroKendoGridDTO gridState);
        IEnumerable<ComboDTO> ObtenerTodoEmpresasFiltro();
    }
}