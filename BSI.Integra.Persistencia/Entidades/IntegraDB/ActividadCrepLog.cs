using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ActividadCrepLog : BaseIntegraEntity
    {

        public int Id { get; set; }
  
        public string TipoOperacion { get; set; } = null!;

        public string TipoActividad { get; set; } = null!;

        public int EstadoOperacion { get; set; }

        public string? ExcepcionProceso { get; set; }

        public string Crep { get; set; } = null!;

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;
 
        public DateTime FechaCreacion { get; set; }
     
        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }
    }
}
