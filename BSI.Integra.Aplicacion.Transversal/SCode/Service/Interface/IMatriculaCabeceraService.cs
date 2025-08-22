using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMatriculaCabeceraService
    {
        #region Metodos Base
        MatriculaCabecera Add(MatriculaCabecera entidad);
        MatriculaCabecera Update(MatriculaCabecera entidad);
        bool Delete(int id, string usuario);

        List<MatriculaCabecera> Add(List<MatriculaCabecera> listadoEntidad);
        List<MatriculaCabecera> Update(List<MatriculaCabecera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MatriculaCabeceraDTO> ObtenerMatriculaCabecera();
        IEnumerable<MatriculaCabeceraComboDTO> ObtenerCombo();
        int ObtenerIdMatriculaCabeceraPorAlumnoCentroCosto(int idAlumno, int idCentroCosto);
        int ObtenerAlumnoProgramaEspecifico(string codigoMatricula);
        int ObtenerProgramaGeneral(int pespecifico);
        string ObtenerFechaFinalizacion(int idMatriculaCabecera);
        bool ActualizarEstadoMatricula(int idMatriculaCabecera, int idEstadoMatricula, string codigoMatricula);
        List<EstadoMatriculadoDTO> EstadoMatriculadoPorAlumno(int idAlumno);
        List<MatriculaDTO> MatriculaPorAlumno(int idAlumno);
        List<BeneficioSolicitadoReporteDTO> ObtenerTodoBeneficioSolicitado(FiltroBeneficiosSolicitadosPorAlumnos FiltroReporteSolcitud);
        List<DatoAdicionalPWDTO> ObtenerDatosAdicionalesPorCodigo(int idMatriculaCabeceraBeneficios);
        int EntregarBeneficio(int idMatriculaCabeceraBeneficio, string usuario);
        EstadoMatriculadoDTO EstadoEvaluacionPorCodMatricula(string codigoMatricula);
        MatriculaCabeceraComboDTO obtenerIdMatriculaporCodigo(string codigo);
        bool ActualizarTMatriculaCabecera(string codigoMatricula, string usuario);
        MatriculaCabeceraDTO ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera);
        DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula(int id);
        string ObtenerUrlConfirmacionParticipacionSesionWebinar(int id, int cantidadDias);
        string ObtenerPresentacionTrabajoNDias(int id, int cantidadDias);
        string ObtenerPresentacionTrabajoFinalNDias(int id, int cantidadDias);
        string ObtenerSesionesWebinarNDias(int id, int cantidadDias, bool mostrarUrlAcceso);
        string ObtenerSesionesWebinarConfirmadasNDias(int id, int cantidadDias, bool mostrarUrlAcceso);
        List<SesionWebinarDTO> ObtenerSesionesConfirmadasWebinarNDias(int id, int cantidadDias);
        string ObtenerCronogramaAutoEvaluacionCompleto(int id);
        string ObtenerAutoEvaluacionesVencidas(int id);
        string ObtenerAutoEvaluacionesCompletas(int id);
        int ObtenerCantidadAutoEvaluacionesPendientes(int id);
        List<AutoEvaluacionCronogramaDetalleDTO> ObtenerDetalleAutoEvaluacionesVencidaDiaExacto(int id, int cantidadDias, bool esFechaExacta);
        AutoEvaluacionCronogramaDetalleDTO ObtenerDetalleProximaAutoEvaluacion(int id);
        int ObtenerCantidadAutoEvaluacionesVencidas(int id);
        string ObtenerCronogramaPagoCompleto(int id, FormatoHTMLMostrar formatoHTMLMostrar);
        string ObtenerMontoTotal(int id);
        string ObtenerCronogramaPagoCompletoCuotasVencidas(int id);
        int ObtenerCantidadCuotasPendientes(int id);
        int ObtenerCantidadCuotasVencidas(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase);
        List<CuotaCronogramaDetalleDTO> ObtenerDetalleCuotasVencidas(int id, int cantidadDias, bool esFechaExacta);
        CuotaCronogramaDetalleDTO ObtenerDetalleProximaCuota(int id);
        string ObtenerDescuentoCuotasPendientesPorPorcentaje(int idMatriculaCabecera, decimal porcentaje);
        List<DetalleCursoActualAulaVirtualDTO> ObtenerCursoActualAlumnoMoodle(int id);
        DetalleAccesoAulaVirtualDTO ObtenerDetalleAccesoAulaVirtual(int id);
        DetalleAccesoPortalWebDTO ObtenerDetalleAccesoDocentePortalWeb(int idProveedor);
        DetalleAccesoPortalWebDTO ObtenerDetalleAccesoPortalWeb(int id);
        int ObtenerProximaSesion(int id, int cantidadDias);
        string ObtenerMaterialesPorMaterialPEspecificoDetalle(List<int> listaIdMaterialPEspecificoDetalle);
        List<SesionWebinarDTO> ObtenerUrlSesionesWebinarNDias(int id, int cantidadDias);
        string ObtenerAutoEvaluacionesVencidasDiasExacto(int id, int cantidadDias, bool esFechaExacta, int idPlantillaBase);
        int ObtenerTodoCantidadCuotasVencidas(int id);
        MatriculaCabecera MapeoEntidadDesdeDTO(MatriculaCabeceraDTO dto);
        string ObtenerUrlMaterialesPorMaterialPEspecificoDetalle(int idMaterialPEspecificoDetalle);
        CambioCentroCostoDTO ObtenerRegistrosParaActualizar(int idSolicitudOperaciones);
        bool ActualizarCentroCosto(CambioCentroCostoDTO solicitudOperacion);
        CambioCentroCostoDTO ObtenerRegistrosParaActualizarVersion(int idSolicitudOperaciones);
        StringDTO EliminarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera);
        StringDTO InsertarBeneficiosMatriculaCabeceraIdMatricula(int idMatriculaCabecera, int nuevoPaquete, int idCronograma);
        List<SubEstadoMatriculaFiltroConfiguracionCoordinadoraDTO> ObtenerSubEstadoMatriculaConfiguracionCoordinadora();
        List<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatricula();
        List<InformacionBeneficioSolicitadoDTO> ObtenerBeneficiosSolicitadosPorMatricula(string codigoMatricula);
        List<MatriculaCabeceraBeneficioDTO> ObtenerBeneficiosCongeladosPorMatricula(string codigoMatricula);
        List<MatriculaCabeceraComboDTO> ObtenerEstadoPgeneralBeneficio(int idPGeneral);
        List<MatriculaCabeceraComboDTO> ObtenerSubEstadoPgeneralBeneficio(int idPGeneral);
        int ObtenerEstadoAlumno(string codigoMatricula);
        int ObtenerSubestadoAlumno(string codigoMatricula);
        List<CursoMoodleDTO> ObtenerCursoMoodle(string codigoMatricula);
        List<CostosAdministrativosDTO> ObtenerCostosAdministrativos(int idMatriculaCabecera);
        List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumno(int idAlumno);
        List<AlumnoProgramaEspecificoDTO> ObtenerAlumnoProgramaEspecificoLista(int idCabeceraMatricula);
        MatriculaCabeceraCodigoFechaDTO CodigoMatriculaPorIdOportunidad(int idOportunidad);
        MatriculaTemporalDTO ObtenerMatriculaPorOportunidad(int idOportunidad);
        IEnumerable<MatriculaCabeceraComboDTO> ObtenerCodigoMatriculaAutocompleto(string nombre);
        IdMatriculaCelularDTO ObtenerIdMatriculaPorCelular(string celular);
        int ModificarGestorDeCobranza(string Usuario, string Gestor, int IdMatriculaCabecera);
        bool ActualizarEstadoMatriculaPorSolicitud(int idSolicitudOperaciones, string nombreEstado);
        DatosAlumnoCoordinadorMatriculaCabeceraDTO ObtenerIdAlumnoCoordinadorAcademico(int idMatriculaCabecera);
        MatriculaCabecera ObtenerPorCodigoMatricula(string codigoMatricula);
        bool ExistePorId(int id);
        List<FiltroMatriculaAlumnoDTO> ObtenerAlumnosMatriculados(string codMatricula, string dni);
        MatriculaCabecera ObtenerPorId(int id);
        List<EstadosMatriculaDTO> ObtenerEstadosMatricula();
        ProgramaGeneralMatriculaDTO ObtenerProgramaGeneralPorIdMatricula(int idMatriculaCabecera);
        List<ProyectoPresentadoPorAlumnoDTO> GenerarReporteProyectoPresentadoPorAlumno(ProyectoPresentadoPorAlumnoFiltroDTO filtroReporte);
        List<VersionMatriculaDisponibleDTO> ObtenerVersionDisponibleMatricula(int idMatriculaCabecera);
        VersionMatriculaDTO ObtenerVersionMatricula(int idMatriculaCabecera);
        PaisMatriculaDTO ObtenerPaisMatricula(string IdMatriculaCabecera);
        string GenerarMatriculaCabeceraPorPostulante(int idPostulante, string Usuario);
        int ActualizarCronogramaPagoPorPostulante(int idPostulante, string Usuario);
    }
}