using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCategoriaOrigenRepository : IGenericRepository<TTipoCategoriaOrigen>
    {
        #region Metodos Base
        TTipoCategoriaOrigen Add(TipoCategoriaOrigen entidad);
        TTipoCategoriaOrigen Update(TipoCategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCategoriaOrigen> Add(IEnumerable<TipoCategoriaOrigen> listadoEntidad);
        IEnumerable<TTipoCategoriaOrigen> Update(IEnumerable<TipoCategoriaOrigen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<TipoCategoriaOrigenDTO> ObtenerTipoCategoriaOrigen();
        IEnumerable<TipoCategoriaOrigenFiltroDTO> ObtenerFiltroTipoCategoriaOrigen();
        List<ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO> ObtenerRemarketingTipoCategoriaOrigen();
        public IEnumerable<FiltroDTO> ObtenerTodoFiltro();
        public int ObtenerTipoCategoriaOrigenID(int id);




    }
}