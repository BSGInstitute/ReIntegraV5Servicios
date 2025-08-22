using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GmailCliente : BaseIntegraEntity
    {
        public int? IdAsesor { get; set; }
        [StringLength(350)]
        public string EmailAsesor { get; set; } = null!;
        [StringLength(350)]
        public string PasswordCorreo { get; set; } = null!;
        [StringLength(300)]
        public string NombreAsesor { get; set; } = null!;
        [StringLength(100)]
        public string IdClient { get; set; } = null!;
        [StringLength(50)]
        public string ClientSecret { get; set; } = null!;
        [StringLength(150)]
        public string? AliasEmailAsesor { get; set; }
    }
}
