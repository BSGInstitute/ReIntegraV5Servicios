using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TEvaluacionEscalaCalificacion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ModalidadCurso
        /// </summary>
        public int IdModalidadCurso { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Ciudad
        /// </summary>
        public string CodigoCiudad { get; set; } = null!;
        /// <summary>
        /// Indica la escala de calificacion
        /// </summary>
        public decimal EscalaCalificacion { get; set; }
        /// <summary>
        /// Indica la nota aprobatoria
        /// </summary>
        public decimal NotaAprobatoria { get; set; }
        /// <summary>
        /// Indica la el redondeo a decimales a utilizar
        /// </summary>
        public int RedondeoDecimales { get; set; }
        /// <summary>
        /// Escala de calificacion en texto
        /// </summary>
        public string EscalaTexto { get; set; } = null!;
        /// <summary>
        /// Nota Aprobatoria en texto
        /// </summary>
        public string NotaAprobatoriaTexto { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
