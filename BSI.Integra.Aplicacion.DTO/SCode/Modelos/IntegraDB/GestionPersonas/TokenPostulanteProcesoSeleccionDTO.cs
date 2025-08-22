
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class TokenPostulanteProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public int IdPostulanteProcesoSeleccion { get; set; }
        public string Token { get; set; } = null!;
        public string TokenHash { get; set; } = null!;
        public Guid GuidAccess { get; set; }
        public bool Activo { get; set; }
        public Boolean Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
