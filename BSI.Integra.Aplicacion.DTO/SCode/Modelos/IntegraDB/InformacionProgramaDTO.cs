namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ObtenerMontos2RespuestaDTO
    {
        public List<MontoPagoModalidadDTO> MontosPorPais { get; set; }
        public List<BeneficioDTO> ListaBeneficios { get; set; }
    }

    public class ObtenerMontos2RespuestaDTOjson
    {
        public List<MontoPagoModalidadDTO> MontosPorPais { get; set; }
        public List<BeneficioDTOjson> ListaBeneficios { get; set; }
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

    public class CargarInformacionProgramaRespuestaDTOjson
    {
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string InformacionPrograma { get; set; }
        public string EtiquetaTarifarios { get; set; }
        public List<BeneficioDTOjson> ListaBeneficios { get; set; }
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
    public class ResumenProgramaDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class ResumenPrograma2DTO
    {
            public string EsProgramaOCurso { get; set; }
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

    public class ResumenProgramaV2DTOjson
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public string Inversion { get; set; }
        public string Certificacion { get; set; }
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

    public class CargarInformacionProgramaAutomaticoRespuestaDTOjson
    {
        public int IdPGeneral { get; set; }
        public string InformacionPrograma { get; set; }
        public List<ResumenProgramaV2DTOjson> ResumenProgramasV2 { get; set; }
        public string EtiquetaDuracionHorarios { get; set; }
        public string EtiquetaExpositores { get; set; }
        public string EtiquetaBeneficiosInversion { get; set; }
        public string EtiquetaFormasPago { get; set; }
        public string EtiquetaTarifarios { get; set; }
        public List<BeneficioDTOjson> ListaBeneficios { get; set; }
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
    public class PreguntaFrecuenteRespuestasDTO
    {
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
    }
    public class listaBeneficiosDTO
    {
        public string version { get; set; }
        public string beneficio { get; set; }
    }

}
