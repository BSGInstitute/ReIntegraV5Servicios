using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFormularioRespuestaService
    {
        #region Metodos Base
        FormularioRespuesta Add(FormularioRespuestaDatoDTO entidad);
        FormularioRespuesta Update(FormularioRespuestaDatoDTO entidad);
        bool Delete(int id, string usuario);

        List<FormularioRespuesta> Add(List<FormularioRespuesta> listadoEntidad);
        List<FormularioRespuesta> Update(List<FormularioRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DTO.ComboDTO> ObtenerCombo();
        IEnumerable<FormularioRespuestaDTO> ObtenerFormularioRespuesta();

        IEnumerable<FormularioRespuestaFiltroDTO> ObtenerFiltroFormularioRespuestum();
        IEnumerable<ProgramaGeneralDatoDTO> ObtenerComboDato();

    }
}
