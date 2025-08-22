using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralMaterialEstudioAdicionalEspecifico
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla ProgramaGeneralMaterialEstudioAdicional pla.T_ProgramaGeneralMaterialEstudioAdicionalEspecificos
        /// </summary>
        public int MaterialEstudioAdicionalPorPgeneralId { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
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
        /// Identificador de migracion
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TPespecifico? IdPespecificoNavigation { get; set; }
    }
}
