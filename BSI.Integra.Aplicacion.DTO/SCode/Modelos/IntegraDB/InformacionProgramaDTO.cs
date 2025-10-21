using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ObtenerMontos2RespuestaDTO
    {
        public List<MontoPagoModalidadDTO> MontosPorPais { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
    }
    public class ObtenerMontos2RespuestaV2DTO
    {
        public List<MontoPagoModalidadV2DTO> MontosPorPais { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
    }
    public class CargarInformacionProgramaRespuestaDTO
    {
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string InformacionPrograma { get; set; }
        public string EtiquetaTarifarios { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
    }

    public class ProgramasPorCodigoPaisComboDTO
    {
        public int IdPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public int IdCategoriaPGeneral { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoISOPais { get; set; }
        public string SimboloMoneda { get; set; }
        public string CodigoMoneda { get; set; }
    }

    public class CargarInformacionProgramaRespuestaOperacionesAtcDTO
    {
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string InformacionPrograma { get; set; }
        public List<ProgramaGeneralSeccionDocumentoDTO> InformacionProgramaV2 { get; set; }
        public List<TarifarioDetalleAgendaDTO> ListaTarifarios { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
        public List<listaBeneficiosDTO> listaBeneficiosAtC { get; set; }
        public List<MontoPagoModalidadDTO> inversion { get; set; }
        public List<MontoPagadoDTO> montopagado { get; set; }
        public List<VersionprogramaDTO> versionAlumno { get; set; }
    }

    public class ResumenProgramaV2DTO
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public string Inversion { get; set; }
        public string Certificacion { get; set; }
    }
    public class ResumenProgramaV3DTO
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public List<object> Inversion { get; set; }
        public object Certificacion { get; set; }
    }
    public class CargarInformacionProgramaAutomaticoRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string InformacionPrograma { get; set; }
        public List<ResumenProgramaV2DTO> ResumenProgramasV2 { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaTarifarios { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
    }


    public class CargarInformacionProgramaAutomaticoRespuestaOperacionesAtcDTO
    {
        public int IdPGeneral { get; set; }
        public string InformacionPrograma { get; set; }
        public List<ProgramaGeneralSeccionDocumentoDTO> InformacionProgramaV2 { get; set; }
        public List<ResumenProgramaV2DTO> ResumenProgramasV2 { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaFormasPago { get; set; }
        public List<TarifarioDetalleAgendaDTO> ListaTarifarios { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
        public List<listaBeneficiosDTO> listaBeneficiosAtC { get; set; }
        public List<MontoPagoModalidadDTO> inversion { get; set; }
        public List<MontoPagadoDTO> montopagado { get; set; }
        public List<VersionprogramaDTO> versionAlumno { get; set; }
    }

    public class CapitulosSesionesProgramaDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string Nombre { get; set; }
        public string Capitulo { get; set; }
        public int OrdenFila { get; set; }
        public List<EstructuraCapituloProgramaAlternoDTO> ListaSesiones { get; set; }
    }
    public class PreguntaFrecuenteSeccionesDTO
    {
        public int IdPrograma { get; set; }
        public int IdSeccion { get; set; }
        public string Nombre { get; set; }
        public List<PreguntaFrecuenteRespuestasDTO> Contenido { get; set; }
    }
    public class PreguntaFrecuenteSeccionesV2DTO
    {
        public int IdPrograma { get; set; }
        public int IdSeccion { get; set; }
        public string Nombre { get; set; }
        public List<PreguntaFrecuenteRespuestasV2DTO> Contenido { get; set; }
    }
    public class PreguntaFrecuenteRespuestasDTO
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
    }
    public class PreguntaFrecuenteRespuestasV2DTO
    {
        public string Pregunta { get; set; }
        public object Respuesta { get; set; }
    }
    public class listaBeneficiosDTO
    {
        public string version { get; set; }
        public string beneficio { get; set; }
    }

    public class CargarInformacionProgramaEndpointsDTO
    {
        // Configuración de beneficios
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficios { get; set; }
        public IEnumerable<DocumentoPwVersionesPGeneralDTO> IntroduccionesBeneficio { get; set; }

        // Inversión
        public IEnumerable<MontoPagoModalidadDTO> Inversion { get; set; }

        //Modalidades 

        public IEnumerable<ModalidadProgramaDTO> Modalidades { get; set; }

        // Flags
        public bool EsProgramaPadre { get; set; }
        public bool EsProgramaTecnico { get; set; }

        // Estructura técnica por hijo
        public List<ListaCursosPorProgramaDTO> CursosHijo { get; set; } = new();                                   // PGeneralRepository.ListaCursosHijoPorIdPGeneral
        public Dictionary<int, object> DuracionCursoHijoPorId { get; set; } = new();                                // PGeneralRepository.ObtenerDuracionCursoHijo(IdHijo) (si conoces el tipo, reemplaza object por int/string/DTO)
        public Dictionary<int, List<ContenidoHijoDTO>> ContenidoEstructuraPorHijoId { get; set; } = new();          // PGeneralRepository.ContenidoEstructuraHijoPadre(IdHijo)

        // Secciones de agenda (raw)
        public List<ProgramaGeneralSeccionDocumentoDTO> SeccionesPrograma { get; set; } = new();                    // DocumentoAgendaService.ObtenerInformacionProgramaGeneral

        // PEspecífico / PGeneral
        public List<PEspecificoPorIdPGeneral> FechasInicioPrograma { get; set; } = new();                           // PEspecificoService.ObtenerFechaInicioProgramaTodos
        public PGeneralAtributosPrincipalesDTO ProgramaGeneral { get; set; }                                         // PGeneralService.ObtenerPGeneralAtributosPrincipalesPorId

        // Área
        public PGeneralAreaSubAreaDTO AreaSubArea { get; set; }                                                      // PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral
        public AreaCapacitacion AreaCapacitacion { get; set; }                                                       // AreaCapacitacionRepository.ObtenerPorId

        // Pagos y tarifarios
        public List<MontoPagadoDTO> MontoPagado { get; set; } = new();                                               // MontoPagoCronogramaRepository.ObtenerMontoPagado
        public List<TarifarioDetalleAgendaDTO> Tarifarios { get; set; } = new();                                     // OrigenService.ObtenerTarifariosDetallesAgenda

        // Beneficios filtrados por país
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficiosFiltradosPorPais { get; set; } = new();
    }
    public class CargarInformacionProgramaEndpointsV2DTO
    {
        // Configuración de beneficios
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficios { get; set; }
        public IEnumerable<DocumentoPwVersionesPGeneralDTO> IntroduccionesBeneficio { get; set; }

        // Inversión
        public IEnumerable<MontoPagoModalidadV2DTO> Inversion { get; set; }

        //Modalidades 

        public IEnumerable<ModalidadProgramaDTO> Modalidades { get; set; }

        // Flags
        public bool EsProgramaPadre { get; set; }
        public bool EsProgramaTecnico { get; set; }

        // Estructura técnica por hijo
        public List<ListaCursosPorProgramaDTO> CursosHijo { get; set; } = new();                                   // PGeneralRepository.ListaCursosHijoPorIdPGeneral
        public Dictionary<int, object> DuracionCursoHijoPorId { get; set; } = new();                                // PGeneralRepository.ObtenerDuracionCursoHijo(IdHijo) (si conoces el tipo, reemplaza object por int/string/DTO)
        public Dictionary<int, List<ContenidoHijoDTO>> ContenidoEstructuraPorHijoId { get; set; } = new();          // PGeneralRepository.ContenidoEstructuraHijoPadre(IdHijo)

        // Secciones de agenda (raw)
        public List<ProgramaGeneralSeccionDocumentoV2DTO> SeccionesPrograma { get; set; } = new();                    // DocumentoAgendaService.ObtenerInformacionProgramaGeneral

        // PEspecífico / PGeneral
        public List<PEspecificoPorIdPGeneral> FechasInicioPrograma { get; set; } = new();                           // PEspecificoService.ObtenerFechaInicioProgramaTodos
        public PGeneralAtributosPrincipalesDTO ProgramaGeneral { get; set; }                                         // PGeneralService.ObtenerPGeneralAtributosPrincipalesPorId

        // Área
        public PGeneralAreaSubAreaDTO AreaSubArea { get; set; }                                                      // PGeneralRepository.ObtenerAreaSubAreaPorIdPGeneral
        public AreaCapacitacion AreaCapacitacion { get; set; }                                                       // AreaCapacitacionRepository.ObtenerPorId

        // Pagos y tarifarios
        public List<MontoPagadoDTO> MontoPagado { get; set; } = new();                                               // MontoPagoCronogramaRepository.ObtenerMontoPagado
        public List<TarifarioDetalleAgendaDTO> Tarifarios { get; set; } = new();                                     // OrigenService.ObtenerTarifariosDetallesAgenda

        // Beneficios filtrados por país
        public List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficiosFiltradosPorPais { get; set; } = new();
    }
    public class InformacionProgramaSpeechDTO
    {
        public List<PresentacionProgramadto> RefuerzodeConfianza { get; set; }
        public List<PresentacionProgramadto> Limitaciones { get; set; }
        public List<PresentacionProgramadto> Demostracióndevalor { get; set; }
        public List<PresentacionProgramadto> Aspectosdiferenciadores { get; set; }
        public List<PresentacionProgramadto> Garantiadeprograma { get; set; }
        public List<PEspecificoPorIdPGeneral> Modalidad { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Objetivos { get; set; }
        public ObtenerMontos2RespuestaDTO Montos { get; set; }
        public List<ProgramaGeneralSeccionDocumentoDTO> DatosAdicionales { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> DuracionHorario { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> PublicoObjetivo { get; set; }

        public List<RegistroListaSeccionesDocumentoDTO> Metodologia { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Presentacion { get; set; }
        public List<ProgramaExpositoresDTO> Expositores { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Prerrequisitos { get; set; }
    }

    public class InformacionProgramaSpeechV2DTO
    {
        public List<PresentacionProgramadto> RefuerzodeConfianza { get; set; }
        public List<PresentacionProgramadto> Limitaciones { get; set; }
        public List<PresentacionProgramadto> Demostracióndevalor { get; set; }
        public List<PresentacionProgramadto> Aspectosdiferenciadores { get; set; }
        public List<PresentacionProgramadto> Garantiadeprograma { get; set; }
        public List<PEspecificoPorIdPGeneral> Modalidad { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Objetivos { get; set; }
        public ObtenerMontos2RespuestaDTO Montos { get; set; }
        public PGeneralAlternoDTO General { get; set; }
        public List<ProgramaGeneralSeccionDocumentoDTO> DatosAdicionales { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> DuracionHorario { get; set; }
        public List<RegistroListaSeccionesDocumentoV2DTO> DuracionHorarioETL { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> PublicoObjetivo { get; set; }

        public List<RegistroListaSeccionesDocumentoDTO> Metodologia { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Presentacion { get; set; }
        public List<ProgramaExpositoresDTO> Expositores { get; set; }
        public List<RegistroListaSeccionesDocumentoDTO> Prerrequisitos { get; set; }
    }


}
