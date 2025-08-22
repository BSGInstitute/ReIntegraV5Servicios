using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface ITipoCategoriaOrigenService
    {
        #region Metodos Base
        TipoCategoriaOrigen Add(TipoCategoriaOrigenDTO entidad, string Usuario);
        TipoCategoriaOrigen Update(TipoCategoriaOrigenDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<TipoCategoriaOrigen> Add(List<TipoCategoriaOrigen> listadoEntidad);
        List<TipoCategoriaOrigen> Update(List<TipoCategoriaOrigen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoCategoriaOrigenDTO> ObtenerTipoCategoriaOrigen();
        IEnumerable<ComboDTO> ObtenerCombo();

        IEnumerable<TipoCategoriaOrigenFiltroDTO> ObtenerFiltroTipoCategoriaOrigen();
        public IEnumerable<FiltroDTO> ObtenerTodoFiltro();


    }
}