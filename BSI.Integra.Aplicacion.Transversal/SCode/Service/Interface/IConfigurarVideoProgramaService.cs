using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfigurarVideoProgramaService
    {
        List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEvaluaciones(int IdPGeneral, int NumeroFila);
        List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEncuestas(int idPGeneral, int numeroFila);
        (int cantidadCorrecto, int cantidadIncorrecto) ImportarExcelConfigurarSecuenciaVideo(IFormFile ArchivoExcel, string usuario);
        List<EstructuraCapituloProgramaAlternoDTO> ObtenerConfiguracionVideoPrograma(int idPGeneral);
        List<EstructuraCapituloProgramaAlternoDTO> ObtenerConfiguracionExamenPrograma(int idPGeneral);
        List<PreguntaPorProgramaDTO> ObtenerEnunciadoPreguntaPorIdPGeneral(int idPGeneral);
        List<ComboDTO> ObtenerDocumentoProgramaGeneral(int idPGeneral);
        List<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConfigurarProyecto(int idPGeneral);
        ConfigurarVideoProgramaDTO ObtenerConfiguracionSesionPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila);
        List<GrupoPreguntaProgramaCapacitacionDTO> ObtenerConfiguracionPreguntasEstructura(int idPGeneral, int seccion, int fila);
        IntDTO EliminarConfiguracionPrograma(int idPGeneral, string usuario);
        byte[] ObtenerPlantillaExcelConfigurarSecuenciaVideo(int idPGeneral);
        List<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConfigurarEvaluacionTrabajoPorConfiguracion(int idPGeneral, int idSeccion, int fila);
        bool Insertar(ConfigurarVideoProgramaDTO configurarVideoProgramaDTO, string usuario);
        bool Actualizar(ConfigurarVideoProgramaDTO configurarVideoProgramaDTO, string usuario);
        bool EliminarSesionConfigurarVideoPorVideoId(string videoId, string usuario);
        byte[] ObtenerPlantillaExcelConfiguracionDeVideo(int idPGeneral);
        (int cantidadCorrecto, int cantidadIncorrecto) ImportarExcel(IFormFile archivoExcel, string usuario);
        (List<EstructuraProgramaCapituloDTO> estructuraProgramaCapitulo, List<SesionSubSesionPreguntaInteractivaDTO> estructuraProgramaSesion) ObtenerCombosModulo();
        bool ActualizarDescargaReproduccionVideo(ActualizarDescargaReproduccionDTO dto, string usuario);
        ConfigurarConteodeVideosPorTipo ObtenerConteosdeVideosTipo(int idPGeneral);
    }
}
