using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TContenidoCertificadoIrca
    {
        /// <summary>
        /// clave Primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Identificados del Curso de Irca
        /// </summary>
        public int CursoIrcaId { get; set; }
        /// <summary>
        /// Nombre de Curso de Irca
        /// </summary>
        public string NombreCurso { get; set; } = null!;
        /// <summary>
        /// Codigo de Curso de Irca
        /// </summary>
        public string CodigoCurso { get; set; } = null!;
        /// <summary>
        /// Fecha Inicio Capacacitacion de Irca
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin de Capacitacion de Irca
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Duracion del Curso de Irca
        /// </summary>
        public int DuracionCurso { get; set; }
        /// <summary>
        /// Resultado Curso Irca indica si es aprobado o desaprobado
        /// </summary>
        public string ResultadoCurso { get; set; } = null!;
        /// <summary>
        /// clave foranea de la tabla pla.T_CentroCosto
        /// </summary>
        public int IdCentroCostoIrca { get; set; }
        /// <summary>
        /// si el Contenido ha sido procesado exitosamente
        /// </summary>
        public bool Procesado { get; set; }
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
