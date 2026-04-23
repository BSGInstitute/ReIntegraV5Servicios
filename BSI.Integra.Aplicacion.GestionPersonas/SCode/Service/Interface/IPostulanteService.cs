using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPostulanteService
    {
        IEnumerable<ComboDTO> ObtenerPostulanteFiltroAutocomplete(string valor);

        ResultadoDatosPostulanteDTO ObtenerPostulantesInscritos(PaginadorDTO paginador);

        //julius
        IEnumerable<PostulanteInformacionProcesoDTO> ObtenerPostulantesInscritosConProcesos();
        IEnumerable<PostulanteProcesoFormatDTO> ObtenerPostulantesInscritosConProcesosExamenes(PostulanteProcesoDTO DataPostulante);


        ResultadoDatosPostulanteDTO ObtenerFiltroDatosPostulanteManual(
            DatosPostulanteDTO datosPostulanteDTO,
            FiltroKendoGridDTO filtroKendoGridDTO,
            IEnumerable<OperadorComparacionDTO> operadoresComparacionDTO);


        Boolean ObtenerPostulantePorEmail(string email);

        //Temp
        ResultadoInsertarPostulante InsertarPostulante(InsertarPostulanteDTO informacionPostulante);
        ResultadoInsertarPostulante ActualizarPostulante(InsertarPostulanteDTO informacionPostulante);
        Object EliminarPostulante(EliminarDTO PostulanteEliminar);
        List<PostulanteLogHistorialDTO> ObtenerHistorialPostulante(int IdPostulante, string Clave);
        IEnumerable<PostulanteExperienciaDTO> ObtenerPostulanteExperiencia(int IdPostulante);

        Object InsertarPostulanteExperiencia(PostulanteExperienciaFormularioDTO postulanteExperienciaFormulario);
        Object ActualizarPostulanteExperiencia(PostulanteExperienciaFormularioDTO postulanteExperienciaFormulario);
        Object EliminarPostulanteExperiencia(EliminarDTO postulanteExperiencia);
        IEnumerable<PostulanteExperienciaLogV2DTO> ObtenerHistorialPostulanteExperiencia(int id);
        IEnumerable<PostulanteFormacionDTO> ObtenerPostulanteFormacion(int IdPostulante);
        Object InsertarPostulanteFormacion(PostulanteFormacionFormularioDTO formacionPostulante);
        Object ActualizarPostulanteFormacion(PostulanteFormacionFormularioDTO formacionPostulante);
        Object EliminarPostulanteFormacion(EliminarDTO postulanteFormacion);
        IEnumerable<PostulanteFormacionLogDTO> ObtenerHistorialPostulanteFormacion(int idPostulante);
        IEnumerable<ResultadoFinalTextoDTO> ValidarCorreoPostulante(int idPostulante);
        Object EnviarPlantillaEmailMasivo(EnvioPlantillaPostulanteDTO Postulantes);
        Object EnviarMensajeEmailPostulante(PostulanteProcesoSeleccionIdDTO PostulanteProcesoSeleccion);
        Object ActualizarProcesoPostulanteSinNota(PostulanteProcesoNuevoDTO Informacion);
        List<ComparacionProcesosSeleccionDTO> CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino);
        Object ActualizarProcesoPostulante(PostulanteProcesoNuevoDTO Informacion);
        Object CambiarProcesoSeleccionPostulanteAlterno(PostulanteProcesoNuevoDTO Informacion);
        Object EnviarMensajeWhatsAppPostulante(EnvioPlantillaPostulanteDTO Postulantes);
        string GenerarClave(int longitud);
        ImportacionPostulanteResultadoDTO ImportarExcel(IFormFile files);
        //ImportacionPostulanteResultadoDTO ImportarExcelV2(IFormFile files);
        ResultadoInsertarPostulante InsertarPostulantePorImportacion(PostulanteProcesoSeleccionConsolidadoDTO lista);
        Postulante ObtenerPostulanteInformacion(int IdPostulante);

        IEnumerable<PostulanteProcesoFormatDTO> HabilitarExamenesEvaluaciones(PostulanteExamenesDTO parametros);

        InformacionPostulanteDTO ObtenerPostulantesInformacionV2(int idPostulante);

    }
}
