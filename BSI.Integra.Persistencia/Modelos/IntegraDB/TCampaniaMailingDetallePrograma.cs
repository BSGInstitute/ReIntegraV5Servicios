using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaMailingDetallePrograma
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreign key de TPLA_CampaniasMailingDetalle
        /// </summary>
        public int IdCampaniaMailingDetalle { get; set; }
        /// <summary>
        /// Es foreign key de tPLA_PGeneral en la columna IdPGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Tipo(Principal, Secundario)
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Orden de Programas
        /// </summary>
        public int Orden { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }

        public virtual TCampaniaMailingDetalle IdCampaniaMailingDetalleNavigation { get; set; } = null!;
        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
    }
}
