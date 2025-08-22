using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfigurarWebinar
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Modalidad del Programa Especifico
        /// </summary>
        public string Modalidad { get; set; } = null!;
        /// <summary>
        /// Codigo Programa Especifico
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Fk T_OperadorComparacion
        /// </summary>
        public int IdOperadorComparacionAvance { get; set; }
        /// <summary>
        /// Valor del avance del alumno
        /// </summary>
        public int ValorAvance { get; set; }
        /// <summary>
        /// Valor del avance del alumno opcional
        /// </summary>
        public int? ValorAvanceOpc { get; set; }
        /// <summary>
        /// Fk T_OperadorComparacion
        /// </summary>
        public int IdOperadorComparacionPromedio { get; set; }
        /// <summary>
        /// Valor del promedio del alumno
        /// </summary>
        public int ValorPromedio { get; set; }
        /// <summary>
        /// Valor del promedio del alumno opcional
        /// </summary>
        public int? ValorPromedioOpc { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
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
        /// Llave foranea con la tabla T_PEspecifico Padre
        /// </summary>
        public int IdPespecificoPadre { get; set; }

        public virtual TPespecifico IdPespecificoNavigation { get; set; } = null!;
    }
}
