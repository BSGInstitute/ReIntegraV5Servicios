using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPreguntaFrecuentePGeneralService
    {
        #region Metodos Base
        PreguntaFrecuentePGeneral Add(PreguntaFrecuentePGeneral entidad);
        PreguntaFrecuentePGeneral Update(PreguntaFrecuentePGeneral entidad);
        bool Delete(int id, string usuario);

        List<PreguntaFrecuentePGeneral> Add(List<PreguntaFrecuentePGeneral> listadoEntidad);
        List<PreguntaFrecuentePGeneral> Update(List<PreguntaFrecuentePGeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<PreguntaFrecuentePGeneralDTO> ObtenerCombo();
        IEnumerable<PreguntaFrecuentePorCentroCostoDTO> ObtenerPreguntaFrecuentePorIdCentroCosto(int idCentroCosto);
        IEnumerable<PreguntaFrecuenteDetallePorCentroCostoDTO> ObtenerPreguntaFrecuenteDetallePorIdCentroCosto(int idCentroCosto);
        List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuente(ProgramaCentroCostoDTO data);
    }
}
