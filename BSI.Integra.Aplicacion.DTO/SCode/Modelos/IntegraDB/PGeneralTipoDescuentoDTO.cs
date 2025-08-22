namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PGeneralTipoDescuentoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool? FlagPromocion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class PGeneralTipoDescuentoComboDTO
    {
        public int Id { get; set; }
        public string PGeneral { get; set; } = null!;
        public string TipoDescuento { get; set; } = null!;
    }
}
