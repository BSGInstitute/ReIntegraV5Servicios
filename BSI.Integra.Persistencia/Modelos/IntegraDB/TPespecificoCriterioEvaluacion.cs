using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoCriterioEvaluacion
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foránea de la tabla pla.T_PEspecificoEsquema
        /// </summary>
        public int IdPespecificoEsquema { get; set; }
        /// <summary>
        /// Clave foránea de la tabla pla.T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Clave foránea de la tabla pla.T_CriterioEvaluacion
        /// </summary>
        public int IdCriterioEvaluacion { get; set; }
        /// <summary>
        /// Porcentaje de ponderación para el criterio de evaluación
        /// </summary>
        public int Ponderacion { get; set; }
        /// <summary>
        /// Estado del registro (creado, eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que hizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
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
    }
}
