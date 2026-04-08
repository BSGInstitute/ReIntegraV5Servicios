using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteRepository : IGenericRepository<TPostulante>
    {
        Postulante? ObtenerPorId(int idPostulante);
        IEnumerable<ComboDTO> ObtenerPostulanteFiltroAutocomplete(string nombre);
        List<PostulanteUltimoProcesoSeleccionDTO> ObtenerPostulantesUltimoProcesoSeleccion(EvaluacionPostulanteFiltroReporteDTO filtro);
        List<PostulanteClasificacionNeoDTO> ObtenerPostulantesUltimoProcesoSeleccion(List<int> idsPostulantes);
        int ObtenerIdMatriculaPorPostulante(int idPostulante);
        List<ReportePostulanteMatriculaDTO> ObtenerNotasMatriculaReporte(List<int> idsPostulantes);
        bool RestablecerNotas(EnvioDatosReestablecerDTO dto, string usuario);
        int? ObtenerIdPostulanteProcesoSeleccionPorIdPostulante(int IdPostulante);
        DatosMatriculaPostulanteDTO? ObtenerDatosMatriculaIdPostulante(int idPostulante);
        ProcesoSeleccionInscritoDTO? ObtenerProcesoSeleccionInscrito(int idPostulanteProcesoSeleccion);
        ResultadoDatosPostulanteDTO ObtenerPostulantesInscritos(PaginadorDTO paginador);

        //julius
        IEnumerable<PostulanteInformacionProcesoDTO> ObtenerPostulantesInscritosConProcesos();
        IEnumerable<PostulanteProcesoEvaluacionesDTO> ObtenerPostulantesInscritosConProcesosExamenes(PostulanteProcesoDTO DataPostulante);

        ResultadoDatosPostulanteDTO ObtenerFiltroDatosPostulanteManual(
            DatosPostulanteDTO datosPostulanteDTO,
            FiltroKendoGridDTO filtroKendoGridDTO,
            IEnumerable<OperadorComparacionDTO> operadoresComparacionDTO);

        Boolean ObtenerPostulantePorEmail(string email);
        IEnumerable<ResultadoFinalTextoDTO> ValidarCorreoPostulante(int IdPostulante);
        PostulanteAlumnoDTO ObtenerIdAlumnoDesdeidPostulanteSinMatricula(int IdPostulante);
        ResultadoFinalTextoDTO CreacionAlumnoDesdePostulante(int idPostulante, string Usuario);
        ValorIntDTO ObtenerMatriculaconIdAlumno(int IdAlumno);
        CuentaPortalDevuelvePostulanteDTO CreacionCuentaPortaldePostulante(int idAlumno, string Email, string NombrePostulante, string ApellidoPostulante, int? IdPais, int? IdCiudad, string Celular);
        List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulante(PostulanteProcesoNuevoDTO Informacion);
        List<ComparacionProcesosSeleccionDTO> CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino);
        List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulanteSinNota(PostulanteProcesoNuevoDTO Informacion);
        bool CambiarProcesoSeleccionPostulanteAlterno(PostulanteProcesoNuevoDTO Informacion);
        IEnumerable<PostulanteProcesoEvaluacionesDTO> HabilitarExamenesEvaluaciones(PostulanteExamenesDTO parametros);

        PostulanteInformacionVisualDTO ObtenerInformacionPostulanteVisual(int idPostulante);
        List<PostulanteFormacionDTOV2> ObtenerPostulanteFormacion(int idPostulante);
        List<PostulanteIdiomaDTOV2> ObtenerPostulanteIdioma(int idPostulante);
        PostulanteEquipoComputoDTOV2 ObtenerPostulanteEquipoComputo(int idPostulante);
        List<PostulanteExperienciaDTOV2> ObtenerPostulanteExperiencia(int idPostulante);
        PostulanteConexionInternetDTOV2 ObtenerPostulanteConexionInternet(int idPostulante);
    }
}
