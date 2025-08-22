using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadRemarketingAgendum
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla com.T_Oportunidad
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Llave foranea de la tabla com.T_AgendaTab
        /// </summary>
        public int IdAgendaTab { get; set; }
        /// <summary>
        /// Flag para aplicar la redireccion forzada al tab configurado
        /// </summary>
        public bool AplicaRedireccion { get; set; }
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
        /// <summary>
        /// Id de migracion de V3
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
