using System.ComponentModel.DataAnnotations.Schema;

namespace BSI.Integra.Aplicacion.Base
{
    public class BaseIntegraEntity
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public byte[]? RowVersion { get; set; } = null!;
    }
}
