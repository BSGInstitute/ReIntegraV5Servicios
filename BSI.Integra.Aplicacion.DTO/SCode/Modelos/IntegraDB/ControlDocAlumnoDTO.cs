namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ControlDocAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public string ComisionableEditable { get; set; }
        public decimal? MontoComisionable { get; set; }
        public string ObservacionesComisionable { get; set; }
        public decimal? PagadoComisionable { get; set; }
        public int? IdMatriculaObservacion { get; set; }
    }
    public class ListaControlDocumentosDTO
    {
        public List<CambiarEntregaDocumentosDTO> ListaDocumentos { get; set; }
        public string matricula { get; set; }
    }
    public class CambiarEntregaDocumentosDTO
    {
        public int IdCriterioDocs { get; set; }
        public bool Ingresar { get; set; }
        public string usuario { get; set; }
    }
    public class CriterioObservacionDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public int? IdMatriculaObservacion { get; set; }
    }
}
