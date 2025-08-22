using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SolucionClienteByActividad : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int IdCausa { get; set; }
        public int? IdPersonal { get; set; }
        public bool Solucionado { get; set; }
        public int IdProblemaCliente { get; set; }
        [StringLength(500)]
        public string? OtroProblema { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
