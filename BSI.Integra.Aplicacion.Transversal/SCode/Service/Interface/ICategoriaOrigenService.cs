using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICategoriaOrigenService
    {
        #region Metodos Base
        CategoriaOrigen Add(CategoriasOrigenDTO entidad, string Usuario);
        CategoriaOrigen Update(CategoriasOrigenDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<CategoriaOrigen> Add(List<CategoriaOrigen> listadoEntidad);
        List<CategoriaOrigen> Update(List<CategoriaOrigen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<CategoriaOrigenDTO> ObtenerCategoriaOrigen();
        IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro();
        IEnumerable<TipoCategoriaOrigenFiltroDTO> TipoCategoriaOrigenFiltroTodo();
        IEnumerable<ComboFiltroDTO> ObtenerCateoriaOrigenFiltro();
        IEnumerable<ComboFiltroDTO> ObtenerFiltroCategoriaOrigen();
        List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ObtenerRemarketingCategoriaOrigen();


        CategoriaOrigenSubCategoriaDatoDTO CategoriaOrigenSubCategoriaDato(int idCategoriaOrigen, int idTipoFormulario);
        List<ComboFiltroDTO> ObtenerCategoriaOrigenPorNombre(string nombre);
        public IEnumerable<ComboFiltroDTO> ObtenerCategoriaFiltro();
        List<CategoriaOrigenFiltroGrupoDTO> ObtenerCategoriaFiltroGrupo();
        List<CategoriaOrigeCombonDTO> ObtenerCategoriaPorTipoCategoria(string TipoDato);
        public CategoriaOrigen ObtenerPorId(int id);


    }
}
