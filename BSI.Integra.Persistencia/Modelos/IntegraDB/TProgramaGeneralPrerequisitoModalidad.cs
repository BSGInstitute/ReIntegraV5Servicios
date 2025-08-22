using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralPrerequisitoModalidad
    {
        /// <summary>
        /// int
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_ProgramaGeneralPrerequisito
        /// </summary>
        public int IdProgramaGeneralPrerequisito { get; set; }
        /// <summary>
        /// Identificador de la modalidad (sera Fk de la tabla nueva T_ModalidadCurso)
        /// </summary>
        public int IdModalidadCurso { get; set; }
        /// <summary>
        /// Nombre de modalidad
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual TProgramaGeneralPrerequisito IdProgramaGeneralPrerequisitoNavigation { get; set; } = null!;
    }
}
