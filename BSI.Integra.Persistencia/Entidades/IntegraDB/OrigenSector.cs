using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OrigenSector : BaseIntegraEntity
    {
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [StringLength(100)]
        public string? Descripcion { get; set; }

        public int Orden { get; set; }


    }
}
