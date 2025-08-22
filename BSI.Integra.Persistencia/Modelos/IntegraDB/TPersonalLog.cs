using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalLog
    {
        /// <summary>
        /// Llave principal de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Indica el rol que tiene el personal
        /// </summary>
        public string? Rol { get; set; }
        /// <summary>
        /// Indica el tipo personal que tiene el personal
        /// </summary>
        public string? TipoPersonal { get; set; }
        /// <summary>
        /// Indica el idjefe que tiene el personal
        /// </summary>
        public int? IdJefe { get; set; }
        /// <summary>
        /// Indica el estado (modificado o no modificado) del rol del personal
        /// </summary>
        public bool EstadoRol { get; set; }
        /// <summary>
        /// Indica el estado (modificado o no modificado) del tipo personal del personal
        /// </summary>
        public bool EstadoTipoPersonal { get; set; }
        /// <summary>
        /// Indica el estado (modificado o no modificado) del idjefe del personal
        /// </summary>
        public bool EstadoIdJefe { get; set; }
        /// <summary>
        /// Indica la fecha de inicio de la modificacion del personal
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Indica la fecha de fin de la modificacion del personal
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Estado (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Indica el usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Indica el usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Indica el fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Indica el fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Indica el row version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica el id de la tabla migrada
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Id de Personal de tipo Asesor Cerrador
        /// </summary>
        public int? IdCerrador { get; set; }
        /// <summary>
        /// Confirmacion de Personal de Tipo Asesor Cerrador
        /// </summary>
        public bool? EsCerrador { get; set; }
        /// <summary>
        /// Validacion de cambio de Tipo de Asesor
        /// </summary>
        public bool? EstadoCerrador { get; set; }
        /// <summary>
        /// FK de T_PuestoTrabajoNivel
        /// </summary>
        public int? IdPuestoTrabajoNivel { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
    }
}
