using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalPuestoSedeHistorico
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PuestoTrabajo
        /// </summary>
        public int IdPuestoTrabajo { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_SedeTrabajo
        /// </summary>
        public int IdSedeTrabajo { get; set; }
        /// <summary>
        /// Estado de Puesto de trabajo o sede a la actualidad
        /// </summary>
        public bool Actual { get; set; }
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

        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual TPuestoTrabajo IdPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TSedeTrabajo IdSedeTrabajoNavigation { get; set; } = null!;
    }
}
