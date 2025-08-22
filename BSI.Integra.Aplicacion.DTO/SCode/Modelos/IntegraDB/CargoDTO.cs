namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CargoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public int? Orden { get; set; }
        //public string UsuarioCreacion { get; set; } = null!;
        //public string UsuarioModificacion { get; set; } = null!;
        //public DateTime FechaCreacion { get; set; }
        //public DateTime FechaModificacion { get; set; }
    } 
}
