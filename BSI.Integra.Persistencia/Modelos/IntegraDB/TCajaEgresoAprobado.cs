using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCajaEgresoAprobado
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
        /// Codigo REC
        /// </summary>
        public string CodigoRec { get; set; } = null!;
        /// <summary>
        /// Anhio de registro
        /// </summary>
        public string Anho { get; set; } = null!;
        /// <summary>
        /// Descripcion del registro solicitado
        /// </summary>
        public string Detalle { get; set; } = null!;
        /// <summary>
        /// Observaciones a la solicitud
        /// </summary>
        public string Observacion { get; set; } = null!;
        /// <summary>
        /// Origen de la solicitud
        /// </summary>
        public string Origen { get; set; } = null!;
        /// <summary>
        /// Fecha de la creacion del registro
        /// </summary>
        public DateTime FechaCreacionRegistro { get; set; }
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
