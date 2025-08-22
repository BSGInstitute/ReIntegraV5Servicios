namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class ReporteDocumentosDTO
    {
        public DateTime FechaCierre { get; set; }
        public string NombrePersonalAsesor { get; set; }
        public string NombrePersonalCoordinador { get; set; }
        public int NumeroIS { get; set; }
        public int ContratoVoz { get; set; }
        public int ContratoFirmado { get; set; }
        public int Empresa { get; set; }
        public int SinDocumentacion { get; set; }
        public decimal Convenio { get; set; }
        public decimal SinDocumentacionP { get; set; }
        public int Observacion { get; set; }
        public int PagoContado { get; set; }
        public int Deuda { get; set; }

    }

    public class ReporteDocumentosFiltroDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Asesor { get; set; }
        public string Coordinador { get; set; }
        public int Desglose { get; set; }
    }


    public class ReporteDocuemntosAgrupadoDTO
    {
        public string Fecha { get; set; }
        public List<ReporteDocumentosVistaDTO> DetalleFecha { get; set; }
    }

    public class ReporteDocumentosVistaDTO
    {
        public DateTime FechaCierre { get; set; }
        public string NombrePersonalAsesor { get; set; }
        public string Coordinador { get; set; }
        public int NumeroIS { get; set; }
        public int ContratoVoz { get; set; }
        public int ContratoFirmado { get; set; }
        public int Empresa { get; set; }
        public int SinDocumentacion { get; set; }
        public decimal Convenio { get; set; }
        public decimal SinDocumentacionP { get; set; }
        public int Observacion { get; set; }
        public int PagoContado { get; set; }
    }

    public class ReporteDocumentosCompuestoDTO
    {
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosAsesor { get; set; }
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosEquipo { get; set; }
        public List<ReporteDocuemntosAgrupadoDTO> ReporteDocumentosCoordinador { get; set; }

    }

}
