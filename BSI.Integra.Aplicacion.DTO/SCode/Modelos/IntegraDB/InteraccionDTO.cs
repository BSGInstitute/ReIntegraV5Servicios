namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class InteraccionDTO
    {
        public int? IdActividadDetalle { get; set; }
        public int? IdTipoInteraccionGeneral { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
