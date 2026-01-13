using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPEspecificoService
    {
        IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorIdPGeneral(int idPGeneral);
        string FechaInicioProgramaV2(int idPGeneral, int idPespecifico);
        List<PEspecificoPorIdPGeneral> ObtenerFechaInicioProgramaTodos(int idPGeneral);
        string ObtenerPeriodoDuracion(int idPEspecifico, int idMatriculaCabecera);
        string ObtenerConjuntoSesion(int idPEspecifico);
        string ObtenerProximoConjuntoSesion(int idPEspecifico, int cantidadDias);
        string ObtenerProximoConjuntoSesionWebex(int idPEspecifico, int cantidadDias, int incrementoZonaHoraria, string nombrePais, bool incluirNombreCurso);
        List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPGeneral(int idPEspecifico, int idMatriculaCabecera);
        List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoIrca(int idPEspecifico, int idMatriculaCabecera, bool esCursoDSig);
        List<ComboDTO> ObtenerPorNombreAutocomplete(string valor);
        IEnumerable<ComboDTO> ObtenerProgramaEspecifico();
        IEnumerable<ComboDTO> ObtenerCombosPEpecificoPorProgramaGeneral(List<int> idPGeneral);
        PEspecificoModuloComboDTO ObtenerCombosModulo();
        Task<PEspecificoModuloComboDTO> ObtenerCombosModuloAsync();
        DatosConfiguracionProgramasWebexDTO ObtenerConfiguracionWebinarPEspecifico(int idPEspecifico);
        string GenerarCronogramaGrupal(int idPespecifico, string usuario);
        (bool Estado, int IdProgramaEspecifico) ActualizarEstadoPrograma(int idPespecifico, int idEstadoPrograma, string usuario);
        CentroCostoGeneradoDTO GenerarCentroCostoCodigoNombre(PEspecificoGeneracionAutomaticaDTO dto);
        PEspecificoDTO ActualizarPespecifico(PEspecificoDTO dto, string usuario);
        int GetAnio(string centroCostoNombre);
        RegistroProgramaEspecificoDTO InsertarCrearCursosConCentroCosto(FiltroInsertarPEspecificoDTO dto, string usuario);
        IEnumerable<InformacionPespecificoHijoDTO> ObtenerTodoPespecificosRelacionados(int idPespecifico);
        bool ActualizarInsertarModuloWebinar(InsertarActualizarModuloWebinaDTO dto, string usuario);
        bool ActualizarConfigurarWebinar(List<ConfigurarWebinarDTO> dto, string usuario);
        bool EliminarConfiguracionWebinar(List<int> ids, string usuario);
        (bool Estado, string Nombre) VerificarSiTienePadrePEspecifico(int idPespecifico);
        public bool VerificarEsPespecificoIndividual(int idPespecifico);
        string ObtenerCronogramaParaModulo(int idPespecifico, string usuario);
        IEnumerable<ComboDTO> ObtenerNumeroGrupos(int pEspecificoId, bool cursoIndividual);
        ConfigurarWebinarDTO InsertarConfiguracionWebinar(ConfigurarWebinarDTO dto, string usuario);
        IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecifico(FiltroObtenerSesionesDTO dto);
        bool VerificarDuracionPorIdPespecificoPadre(int idPespecificoPadre);
        bool ActualizarFechaPorSesion(ActualizarFechaPorSesionDTO dto, string usuario);
        byte[]? GenerarReporteAmbienteExcel(FiltroReporteAmbienteDTO filtro);
        string ObtenerCronogramaParaModuloAlterno(int idPespecifico);
        bool ModificarFrecuencia(ParametrosInsertaFrecuenciaDTO dto, string usuario);
        bool EliminarCronogramaDuplicado(int PEspecificoId, int NumeroGrupo, string Usuario);
        bool InsertarEventoEspecial(FiltroSesionEspecialDTO dto, string usuario);
        RptaActualizarDuracionInsertarSesionDTO ActualizarDuracionInsertarSesion(InformacionPespecificoSesionDTO dto, string usuario);
        (bool EstadoCruce, List<CruceSesionPEspecificoDTO>? Cruces) ActualizarDocenteAmbienteProgramaEspecifico(DocenteAmbientePEspecificoDTO dto, string usuario);
        string GenerarPDFCronogramaModulo(FiltroObtenerPDFDTO dto, string usuario);
        string GenerarPDFCronogramaSemanal(FiltroObtenerPDFDTO dto, string usuario);
        bool InsertarFrecuencia(ParametrosInsertaFrecuenciaDTO dto, string usuario);
        bool ClonarSesiones(int PEspecificoId, string Usuario, int IdPespecificoHijo);
        List<ProgramaEspecificoMaterialDTO> ObtenerPorFiltro(ProgramaEspecificoMaterialFiltroDTO filtro);
        IEnumerable<ComboDTO> ObtenerProgramasEspecificosAdicional();
        IEnumerable<PEspecificoDetalleFechaByPGeneral> ObtenerFiltroV2PorIdPGeneral(int idPGeneral);
		IEnumerable<PEspecificoByPGeneral> ObtenerPEspecificoByPGeneral(int idPGeneral);
	}
}
