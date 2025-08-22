namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteControlDocumentosDTO
    {
        public string ProgramaEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMatriculaObservacion { get; set; }
        public string EstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string PeriodoMatricula { get; set; }
        public decimal PagoAcumulado { get; set; }
        public string CriterioCalificacion { get; set; }
        public string QuienEntregoDoc { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string Coordinador { get; set; }
        public string Asesor { get; set; }
        public DateTime? FechaPrimerPago { get; set; }
        public string Documentos { get; set; }
        public Nullable<int> Cronograma { get; set; }
        public Nullable<int> Convenio { get; set; }
        public Nullable<int> Pagare { get; set; }
        public Nullable<int> Carta_Autorizacion { get; set; }
        public Nullable<int> Hoja_Requisitos { get; set; }
        public Nullable<int> Orden_compra { get; set; }
        public Nullable<int> Carta_compromiso { get; set; }
        public Nullable<int> DNI { get; set; }

    }

    public class ReporteControlDocumentosFiltroDTO
    {
        public DateTime? FechaInicio { get; set; } = null;
        public DateTime? FechaFin { get; set; } = null;
        public int? IdCoordinador { get; set; } = null;
        public int? IdAsesor { get; set; } = null;
        public int? IdCentroCosto { get; set; } = null;
        public int? IdAlumno { get; set; } = null;
        public int? IdMatricula { get; set; } = null;
        public int? IdEstadoPagoMatricula { get; set; } = null;
    }
}
