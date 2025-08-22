
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfigurarVideoProgramaRepository : IGenericRepository<TConfigurarVideoPrograma>
    {
        #region Metodos Base
        TConfigurarVideoPrograma Add(ConfigurarVideoPrograma entidad);
        TConfigurarVideoPrograma Update(ConfigurarVideoPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfigurarVideoPrograma> Add(IEnumerable<ConfigurarVideoPrograma> listadoEntidad);
        IEnumerable<TConfigurarVideoPrograma> Update(IEnumerable<ConfigurarVideoPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoPrograma(int idPGeneral);
        List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEvaluaciones(int idPGeneral, int numeroFila);
        List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEncuestas(int idPGeneral, int numeroFila);
        IEnumerable<ConfigurarVideoPrograma> ObtenerPorIdPGeneral(int idPGeneral);
        void EliminarConfiguracionVideo(int idProgramaGeneral);
        ConfigurarVideoProgramaDTO ObtenerConfigurarVideoPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila);
        ConfigurarVideoPrograma ObtenerPorId(int id);
        IEnumerable<ConfigurarVideoPrograma> ObtenerPorVideoId(string videoId);
        IEnumerable<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaDescargaSinDatos(int idPGeneral);
        IEnumerable<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaDescarga(int idPGeneral);
    }
}
