
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteLogDTO
    {
        public int? idPostulante { get; set; }
        public string? Clave { get; set; }
        public string? Valor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }

    public class PostulanteLogHistorialDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public string FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
