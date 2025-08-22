using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDetalleOportunidadCompetidor
    {
        /// <summary>
        /// es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// es foreign key T_OportunidadCompetidor
        /// </summary>
        public int IdOportunidadCompetidor { get; set; }
        /// <summary>
        /// es foreign key de T_Empresa
        /// </summary>
        public int IdCompetidor { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }

        public virtual TOportunidadCompetidor IdOportunidadCompetidorNavigation { get; set; } = null!;
    }
}
