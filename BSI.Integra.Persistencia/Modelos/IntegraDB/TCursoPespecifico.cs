using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCursoPespecifico
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key T_PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Nombre del curso
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// duracion en horas del curso
        /// </summary>
        public int Duracion { get; set; }
        /// <summary>
        /// Orden de ejecucion del curso en el programa
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Es foreing key T_Expositor
        /// </summary>
        public int? IdExpositor { get; set; }
        /// <summary>
        /// Numero de sesiones que tiene el curso
        /// </summary>
        public int? NroSesiones { get; set; }
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

        public virtual TPespecifico? IdPespecificoNavigation { get; set; }
    }
}
