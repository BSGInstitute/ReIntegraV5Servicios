using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteCursoPortalNotasHistorico
    {
        /// <summary>
        /// PK de Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_PostulanteProcesoSeleccion
        /// </summary>
        public int IdPostulanteProcesoSeleccion { get; set; }
        /// <summary>
        /// FK de T_Pgeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Orden de Fila Capítulo
        /// </summary>
        public int? OrdenFilaCapitulo { get; set; }
        /// <summary>
        /// Orden de Fila Sesion
        /// </summary>
        public int? OrdenFilaSesion { get; set; }
        /// <summary>
        /// Grupo de Preguntas
        /// </summary>
        public string? GrupoPregunta { get; set; }
        /// <summary>
        /// Calificación de Evaluacion
        /// </summary>
        public decimal? Calificacion { get; set; }
        /// <summary>
        /// FK de AspNetUsers
        /// </summary>
        public string? IdUsuario { get; set; }
        /// <summary>
        /// FK de T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// FK de Pespecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Validación Acceso de Prueba
        /// </summary>
        public bool? AccesoPrueba { get; set; }
        /// <summary>
        /// Estado de Registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de Creación de Registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación de registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de Creación de Registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de Modificación de registro
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
