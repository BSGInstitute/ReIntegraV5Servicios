using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCabeceraFurConfiguracionAutomatica
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de Cabecera
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo de Cabecera
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Id de T_ConfiguracionProyeccionFur
        /// </summary>
        public int? IdConfiguracionProyeccionFur { get; set; }
        /// <summary>
        /// Id de T_EstadoProyeccionFur
        /// </summary>
        public int IdEstadoProyeccionFur { get; set; }
        /// <summary>
        /// Id Area Solicitante de Proyección
        /// </summary>
        public int IdArea { get; set; }
        /// <summary>
        /// Observaciones de la Proyección
        /// </summary>
        public string? Observacion { get; set; }
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
