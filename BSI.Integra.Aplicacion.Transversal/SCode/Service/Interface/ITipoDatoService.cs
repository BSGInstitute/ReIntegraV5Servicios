using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoDatoService
    {
        #region Metodos Base
        TipoDato Add(TipoDato entidad);
        TipoDato Update(TipoDato entidad);
        bool Delete(int id, string usuario);

        List<TipoDato> Add(List<TipoDato> listadoEntidad);
        List<TipoDato> Update(List<TipoDato> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<TipoDatoDTO> ObtenerTipoDato();
        IEnumerable<TipoDatoFiltroDTO> ObtenerFiltroTipoDato();
        IEnumerable<ComboDTO> CargarTipoDatoChat();
        TipoDato InsertarTipoDato(TipoDatosDTO entidad, string Usuario);
        TipoDato ActualizarTipoDato(TipoDatosDTO entidad, string Usuario);
    }
}
