using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsappAgenteLlamadaEstado
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPais { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public byte EstadoDisponibilidad { get; set; }
        public int? IdLlamadaActual { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public string? SignalRconnectionId { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
