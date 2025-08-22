using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenAsignadoEvaluadorRepository : IGenericRepository<TExamenAsignadoEvaluador>
    {
        #region Metodos Base
        TExamenAsignadoEvaluador Add(ExamenAsignadoEvaluador entidad);
        TExamenAsignadoEvaluador Update(ExamenAsignadoEvaluador entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TExamenAsignadoEvaluador> Add(IEnumerable<ExamenAsignadoEvaluador> listadoEntidad);
        IEnumerable<TExamenAsignadoEvaluador> Update(IEnumerable<ExamenAsignadoEvaluador> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ExamenAsignadoEvaluador? ObtenerPorId(int id);
        List<EvaluacionesAsignadasEvaluadorDTO> ObtenerListaEvaluacionEvaluador(int idPostulante, int idProcesoSeleccion);
        List<PreguntaTestDTO> ObtenerPreguntasTest(TestInformacionDTO test);
        List<RespuestasTestDTO> ObtenerListaPreguntasRespuestaTest(int IdExamen, int IdPregunta);
        List<EvaluacionPortalPostulante> ObtenerEvaluacionesPortalPostulante(EvaluacionPostulanteFiltroReporteDTO filtro);
        ExamenAsignadoEvaluador? ObtenerPorIdPostulanteIdExamen(int idPostulante, int idExamen);
        List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion);
    }
}
