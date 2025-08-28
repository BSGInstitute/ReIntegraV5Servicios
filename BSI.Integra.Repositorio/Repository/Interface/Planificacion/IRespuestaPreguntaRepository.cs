using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IRespuestaPreguntaRepository : IGenericRepository<TRespuestaPreguntum>
    {
        #region Metodos Base
        TRespuestaPreguntum Add(RespuestaPregunta entidad);
        TRespuestaPreguntum Update(RespuestaPregunta entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRespuestaPreguntum> Add(IEnumerable<RespuestaPregunta> listadoEntidad);
        IEnumerable<TRespuestaPreguntum> Update(IEnumerable<RespuestaPregunta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        RespuestaPregunta ObtenerPorId(int id);
        List<RespuestaPregunta> ObtenerRespuestaPorIdPregunta(int id);
        IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO> ObtenerFactorDesaprovatorio();
        List<PreguntaRespuestaAsincronicaDTO> ObtenerRespuestaPregunta(int idPregunta);
        IEnumerable<PreguntaRespuestaAsincronicaDTO> ObtenerRespuesta(int idPregunta);
    }
}
