using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCajaPorRendirCabecera
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tCajas
        /// </summary>
        public int IdCaja { get; set; }
        /// <summary>
        /// Codigo de la cabecera del registro
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// El anho en que se realiza
        /// </summary>
        public int Anho { get; set; }
        /// <summary>
        /// El usuario que lo aprueba
        /// </summary>
        public int IdPersonalAprobacion { get; set; }
        /// <summary>
        /// El usuario que lo solicita
        /// </summary>
        public int IdPersonalSolicitante { get; set; }
        /// <summary>
        /// Descripcion de lo solicitado
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Observaciones a la solicitud
        /// </summary>
        public string Observacion { get; set; } = null!;
        /// <summary>
        /// Estado si ha sido aceptado
        /// </summary>
        public bool EsRendido { get; set; }
        /// <summary>
        /// Devolucion del monto registrado
        /// </summary>
        public decimal MontoDevolucion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
