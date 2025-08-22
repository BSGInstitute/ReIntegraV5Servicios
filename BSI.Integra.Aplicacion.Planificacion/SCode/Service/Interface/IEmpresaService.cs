using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IEmpresaService
    {
        #region Metodos Base
        Empresa Add(Empresa entidad);
        Empresa Update(Empresa entidad);
        bool Delete(int id, string usuario);

        List<Empresa> Add(List<Empresa> listadoEntidad);
        List<Empresa> Update(List<Empresa> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<EmpresaDTO> ObtenerEmpresa();
        IEnumerable<ComboDTO> ObtenerCombo();
        List<EmpresaDTO> ObtenerEmpresa2();
        IEnumerable<ComboDTO> ObtenerAutocomplete(string nombreParcial);
        ValorIntDTO ObtenerIdTamanioEmpresaPorIdEmpresa(int idEmpresa);
        List<ComboDTO> ObtenerTodoCompetidores();
        IEnumerable<TipoIdentificadorComboDTO> ObtenerComboTipoIdentificador();
        IEnumerable<DTO.ComboDTO> ObtenerComboTamanioEmpresa();
        IEnumerable<CodigoCiiuIndustriaComboDTO> ObtenerComboCodigoCiiuIndustria();
        ComboDTO ObtenerEmpresaPorId(int id);
        List<ComboDTO> CargarEmpresaAutoComplete(string nombre);
        Empresa ObtenerPorId(int id);
        List<EmpresaObtenerDTO> ObtenerEmpresas();
    }
}
