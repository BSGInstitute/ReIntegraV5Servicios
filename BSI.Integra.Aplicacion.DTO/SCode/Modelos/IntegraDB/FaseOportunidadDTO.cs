namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FaseOportunidadDTO
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? NroMinutos { get; set; }
        public int? IdActividad { get; set; }
        public int? MaxNumDias { get; set; }
        public int? MinNumDias { get; set; }
        public int? TasaConversionEsperada { get; set; }
        public int? Meta { get; set; }
        public bool? Final { get; set; }
        public bool? ReporteMeta { get; set; }
        public bool? EnSeguimiento { get; set; }
        public bool? EsCierre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? VisibleEnReporte { get; set; }
    }

    public class FaseOportunidadComboDTO
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
    }
    public class FaseOportunidadInteraccionDTO
    {
        public int Id { get; set; }
        public string IdFaseOportunidadPortal { get; set; }
    }
}
