using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TiempoFrecuencia : BaseIntegraEntity
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int NroCampos { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;
   
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
 
        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }
}
