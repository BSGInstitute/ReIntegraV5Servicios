using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralExpositor
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key T_PGeneral 
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Es foreing key T_Expositor
        /// </summary>
        public int IdExpositor { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int Posicion { get; set; }
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
        /// <summary>
        /// Es foreign key que hace referencia a la tabla T_ModalidadCurso
        /// </summary>
        public int? IdModalidadCurso { get; set; }

        public virtual TModalidadCurso? IdModalidadCursoNavigation { get; set; }
        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
    }
}
