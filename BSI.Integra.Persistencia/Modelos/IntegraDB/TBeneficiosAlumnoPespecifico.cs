using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TBeneficiosAlumnoPespecifico
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Alumno
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Llave foranea de la T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Indica los beneficios propiamente de cada beneficio
        /// </summary>
        public string? Beneficios { get; set; }
        /// <summary>
        /// Indica si esta eliminado o creado
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
        /// Indica la fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Indica la fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Indica el row version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica el idmigracion de la tabla origen
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
