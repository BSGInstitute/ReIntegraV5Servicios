using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaFrecuentePGeneralRepository : IGenericRepository<TPreguntaFrecuentePgeneral>
    {
        #region Metodos Base
        TPreguntaFrecuentePgeneral Add(PreguntaFrecuentePGeneral entidad);
        TPreguntaFrecuentePgeneral Update(PreguntaFrecuentePGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaFrecuentePgeneral> Add(IEnumerable<PreguntaFrecuentePGeneral> listadoEntidad);
        IEnumerable<TPreguntaFrecuentePgeneral> Update(IEnumerable<PreguntaFrecuentePGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<PreguntaFrecuentePGeneralDTO> Obtener();
        IEnumerable<PreguntaFrecuentePorCentroCostoDTO> ObtenerPreguntaFrecuentePorIdCentroCosto(int idCentroCosto);
        List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuenteCambio(int idPGeneral, int idArea, int idSubArea, int idTipo);
        List<PreguntaFrecuentePGeneralRespuestaDTO> ObtenerPreguntaFrecuente(ProgramaCentroCostoDTO data);
        IEnumerable<PreguntaFrecuentePGeneral> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente);
        PreguntaFrecuentePGeneral ObtenerPorIdPreguntaFrecuenteYIdPGeneral(int idPreguntaFrecuente, int idPGeneral);
    }
}