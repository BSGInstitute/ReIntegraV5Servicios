namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorCalificacionDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdProveedorSubCriterioCalificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
    }
    public class FiltroProveedorCalificacionDTO
    {
        public int IdProveedor { get; set; }
        public int[] ListaIdSubCriterioCalificacion { get; set; }
        public int IdPrestacionRegistro { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
