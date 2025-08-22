using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralAsubPgeneralVersionPrograma
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foranea de la tabla pla.T_PGeneralASubPGeneral
        /// </summary>
        public int IdPgeneralAsubPgeneral { get; set; }
        /// <summary>
        /// Clave Foranea de la Tabla pla.T_VersionPrograma
        /// </summary>
        public int? IdVersionPrograma { get; set; }
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
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TPgeneralAsubPgeneral IdPgeneralAsubPgeneralNavigation { get; set; } = null!;
        public virtual TVersionPrograma? IdVersionProgramaNavigation { get; set; }
    }
}
