using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentoAgendum
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string MensajeDetalle { get; set; } = null!;
        public bool Generado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
