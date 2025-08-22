using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteFormacionLog
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Fk T_PostulanteFormacion
        /// </summary>
        public int IdPostulanteFormacion { get; set; }
        /// <summary>
        /// Fk T_CentroEstudio
        /// </summary>
        public int? IdCentroEstudio { get; set; }
        /// <summary>
        /// Fk T_TipoEstudio
        /// </summary>
        public int? IdTipoEstudio { get; set; }
        /// <summary>
        /// Fk T_AreaFormacion
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// Fk T_EstadoEstudio
        /// </summary>
        public int? IdEstadoEstudio { get; set; }
        /// <summary>
        /// Fecha inicio estudios
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha fin estudios
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Nombre otra institutcion
        /// </summary>
        public string? OtraInstitucion { get; set; }
        /// <summary>
        /// Nombre optra carrera
        /// </summary>
        public string? OtraCarrera { get; set; }
        /// <summary>
        /// define si se estudia hasta la actualidad
        /// </summary>
        public bool? AlaActualidad { get; set; }
        /// <summary>
        /// Determina el turno en el que estudia el postulante
        /// </summary>
        public string? TurnoEstudio { get; set; }
        /// <summary>
        /// FK T_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Tipo de modificacion hecha
        /// </summary>
        public string TipoActualizacion { get; set; } = null!;
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
