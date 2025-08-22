using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RegionCiudad : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int idPais { get; set; }
        public int? CodigoBS { get; set; }
        public int? DenominacionBS { get; set; }
        public string? NombreCorto { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }
}
