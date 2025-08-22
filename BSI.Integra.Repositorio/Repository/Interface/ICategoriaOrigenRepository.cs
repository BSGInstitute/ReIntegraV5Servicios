using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICategoriaOrigenRepository : IGenericRepository<TCategoriaOrigen>
    {
        #region Metodos Base
        TCategoriaOrigen Add(CategoriaOrigen entidad);
        TCategoriaOrigen Update(CategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCategoriaOrigen> Add(IEnumerable<CategoriaOrigen> listadoEntidad);
        IEnumerable<TCategoriaOrigen> Update(IEnumerable<CategoriaOrigen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<CategoriaOrigenDTO> ObtenerCategoriaOrigen();
        IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro();
        IEnumerable<TipoCategoriaOrigenFiltroDTO> TipoCategoriaOrigenFiltroTodo();
        IEnumerable<ComboFiltroDTO> ObtenerCateoriaOrigenFiltro();
        IEnumerable<ComboFiltroDTO> ObtenerFiltroCategoriaOrigen();
        List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ObtenerRemarketingCategoriaOrigen();
        CategoriaOrigenSubCategoriaDatoDTO? ObtenerCategoriaOrigenSubCategoriaDato(int idCategoriaOrigen, int idTipoFormulario);
        List<ComboFiltroDTO> ObtenerCategoriaOrigenPorNombre(string nombre);
        public IEnumerable<ComboFiltroDTO> ObtenerCategoriaFiltro();
        CategoriaOrigen ObtenerPorId(int id);
        public List<CategoriaOrigenAdwordsDTO> ObtenerCategoriaOrigenAdwords();
        int ObtenerTipoCategoriaOrigenPorId(int id);

        List<CategoriaOrigenFiltroGrupoDTO> ObtenerCategoriaFiltroGrupo();
        List<CategoriaOrigeCombonDTO> ObtenerCategoriaPorTipoCategoria(string TipoDato);
        Task<IEnumerable<CompuestoCategoriaOrigenConHijosDTO>> ObtenerCategoriaConHijosAsync();
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorCampania(string Nombre);
    }
}
