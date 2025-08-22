using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TIntentoMarcadorLog
    {
        /// <summary>
        /// (PK) Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id Oportunidad
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Id Actividad Detalle
        /// </summary>
        public int IdActividadDetalle { get; set; }
        /// <summary>
        /// Id Personal intento
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Id Tab Origen del dato
        /// </summary>
        public int? IdAgendaTab { get; set; }
        /// <summary>
        /// Orden del Intento
        /// </summary>
        public int OrdenIntento { get; set; }
        /// <summary>
        /// Fecha Programada del Intento
        /// </summary>
        public DateTime? FechaProgramadaIntento { get; set; }
        /// <summary>
        /// Fecha Real Intento
        /// </summary>
        public DateTime? FechaRealIntento { get; set; }
        /// <summary>
        /// Fecha Hora Contesto
        /// </summary>
        public DateTime? FechaHoraContesto { get; set; }
        /// <summary>
        /// Fecha Hora Colgar Llamada
        /// </summary>
        public DateTime? FechaHoraColgarLlamada { get; set; }
        /// <summary>
        /// Fecha Hora Estado Intento
        /// </summary>
        public DateTime? FechaHoraEstadoIntento { get; set; }
        /// <summary>
        /// Detalle
        /// </summary>
        public string? Detalle { get; set; }
        /// <summary>
        /// Origen Colgar Llamada
        /// </summary>
        public string? OrigenColgarLlamada { get; set; }
        /// <summary>
        /// Origen Sistema
        /// </summary>
        public string? OrigenSistema { get; set; }
        /// <summary>
        /// Origen Intento
        /// </summary>
        public string? OrigenIntento { get; set; }
        /// <summary>
        /// Estado Intento
        /// </summary>
        public int? EstadoIntento { get; set; }
        /// <summary>
        /// Estado Marcador
        /// </summary>
        public bool? EstadoMarcador { get; set; }
        /// <summary>
        /// Estado Crm
        /// </summary>
        public bool? EstadoCrm { get; set; }
        /// <summary>
        /// Duracion Timbrado Integra
        /// </summary>
        public int? DuracionTimbradoIntegra { get; set; }
        /// <summary>
        /// Duracion Contesto Integra
        /// </summary>
        public int? DuracionContestoIntegra { get; set; }
        /// <summary>
        /// Origen Telefono
        /// </summary>
        public string? OrigenTelefono { get; set; }
        /// <summary>
        /// Numero Destino
        /// </summary>
        public string? NumeroDestino { get; set; }
        /// <summary>
        /// Anexo
        /// </summary>
        public string? Anexo { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
