using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SmsConfiguracionEnvio : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string? Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public bool Activo { get; set; }
    }
}
