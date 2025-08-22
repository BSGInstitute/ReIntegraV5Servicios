using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionDeEnvioParaWhatsApp : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int? IdPlantilla { get; set; }
        public DateTime FechaDeEnvio { get; set; }
        public DateTime FechaFinDeEnvio { get; set; }
        public int? HoraDeEnvio { get; set; }
        public string Nombre { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }

    }
}
