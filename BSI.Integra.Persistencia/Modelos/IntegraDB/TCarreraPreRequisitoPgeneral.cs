using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCarreraPreRequisitoPgeneral
    {
        /// <summary>
        /// id Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id foreign key PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Id foreign key PGeneral
        /// </summary>
        public int? IdPgeneralPrerequisito { get; set; }
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
        public int? IdMigracion { get; set; }

        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
    }
}
