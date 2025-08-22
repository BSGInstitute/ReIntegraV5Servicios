using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoMatriculaAlumno
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foránea de T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Es clave foránea de T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Es clave foránea de T_PEspecificoTipoMatricula
        /// </summary>
        public int IdPespecificoTipoMatricula { get; set; }
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
        /// Id del usuario en moodle
        /// </summary>
        public int? IdUsuarioMoodle { get; set; }
        /// <summary>
        /// Id de curso en moodle
        /// </summary>
        public int? IdCursoMoodle { get; set; }
        /// <summary>
        /// Grupo
        /// </summary>
        public int Grupo { get; set; }
        /// <summary>
        /// Duracion en horas del curso
        /// </summary>
        public int? Duracion { get; set; }
        public bool? EsAonline { get; set; }
        public bool? Regularizado { get; set; }
        public string? ErrorCongelamiento { get; set; }
        /// <summary>
        /// Indica si aplica el nuevo aula virtual
        /// </summary>
        public bool? AplicaNuevaAulaVirtual { get; set; }
        /// <summary>
        /// Contiene la nota obtenida en el aula virtual anterior al momento de migrar al alumno
        /// </summary>
        public decimal? NotaAulaVirtualAnterior { get; set; }
        /// <summary>
        /// Contiene la conversion de la la nota obtenida en el aula virtual anterior a la escala de calificacion correspondiente
        /// </summary>
        public int? IdEscalaCalificacionDetalle { get; set; }
        /// <summary>
        /// Indica si la nota del aula virtual anterior se recuperara en la nueva aula virtual
        /// </summary>
        public bool? RecuperaNuevaAulaVirtual { get; set; }
        /// <summary>
        /// Es la fecha que debio terminar el curso
        /// </summary>
        public DateTime? FechaFinCronograma { get; set; }
        /// <summary>
        /// Es la feha real que termino el curso
        /// </summary>
        public DateTime? FechaFinReal { get; set; }
    }
}
