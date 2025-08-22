namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EtiquetaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public int? IdTipoEtiqueta { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class EtiquetaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
