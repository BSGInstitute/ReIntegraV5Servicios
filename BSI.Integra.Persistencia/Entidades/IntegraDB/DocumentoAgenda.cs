using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoAgenda : BaseIntegraEntity
    {
        [StringLength(200)]
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string MensajeDetalle { get; set; } = null!;
        public bool Generado { get; set; }
    }
}
