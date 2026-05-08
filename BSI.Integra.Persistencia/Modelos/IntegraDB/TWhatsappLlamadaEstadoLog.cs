using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsappLlamadaEstadoLog
    {
        public int IdLog { get; set; }
        public int IdWhatsappLlamada { get; set; }
        public byte? EstadoAnterior { get; set; }
        public byte EstadoNuevo { get; set; }
        public string? Motivo { get; set; }
        public string? Origen { get; set; }
        public DateTime FechaTransicion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
