using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoInteraccionService
    {
        #region Metodos Base
        TipoInteraccion Add(TipoInteraccionesDTO entidad, string Usuario);
        TipoInteraccion Update(TipoInteraccionesDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<TipoInteraccion> Add(List<TipoInteraccion> listadoEntidad);
        List<TipoInteraccion> Update(List<TipoInteraccion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<TipoInteraccionDTO> ObtenerTipoInteraccion();
        IEnumerable<TipoInteraccionCanalDTO> ObtenerTipoInteraccionCanalCombo();
        List<FiltroDTO> ObtenerPorTipoInteraccionGeneralFormulario();
        
        }
}
