using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralVersionPrograma
    {
        /// <summary>
        /// Clave Primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Foranea de la tabla pla.T_Pgeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Clave Foranea de la Tabla pla.T_VersionPrograma
        /// </summary>
        public int? IdVersionPrograma { get; set; }
        /// <summary>
        /// Duracion en horas de programa por version
        /// </summary>
        public int? Duracion { get; set; }
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
        /// <summary>
        /// Cantidad de Creditos disponibles para el tutor virtual
        /// </summary>
        public int? CreditoDisponibleTutorVirtual { get; set; }
        /// <summary>
        /// Cantidad de webinars asignados a la version del programa
        /// </summary>
        public int? CantidadWebinarAsignado { get; set; }
        /// <summary>
        /// Cantidad de meses adicionales a los webinar por version de programa
        /// </summary>
        public int? CantidadMesAccesoAdicionalWebinar { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
    }
}
