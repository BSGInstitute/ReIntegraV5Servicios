using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalHorario
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public TimeSpan? Lunes1 { get; set; }
        public TimeSpan? Lunes2 { get; set; }
        public TimeSpan? Lunes3 { get; set; }
        public TimeSpan? Lunes4 { get; set; }
        public TimeSpan? Martes1 { get; set; }
        public TimeSpan? Martes2 { get; set; }
        public TimeSpan? Martes3 { get; set; }
        public TimeSpan? Martes4 { get; set; }
        public TimeSpan? Miercoles1 { get; set; }
        public TimeSpan? Miercoles2 { get; set; }
        public TimeSpan? Miercoles3 { get; set; }
        public TimeSpan? Miercoles4 { get; set; }
        public TimeSpan? Jueves1 { get; set; }
        public TimeSpan? Jueves2 { get; set; }
        public TimeSpan? Jueves3 { get; set; }
        public TimeSpan? Jueves4 { get; set; }
        public TimeSpan? Viernes1 { get; set; }
        public TimeSpan? Viernes2 { get; set; }
        public TimeSpan? Viernes3 { get; set; }
        public TimeSpan? Viernes4 { get; set; }
        public TimeSpan? Sabado1 { get; set; }
        public TimeSpan? Sabado2 { get; set; }
        public TimeSpan? Sabado3 { get; set; }
        public TimeSpan? Sabado4 { get; set; }
        public TimeSpan? Domingo1 { get; set; }
        public TimeSpan? Domingo2 { get; set; }
        public TimeSpan? Domingo3 { get; set; }
        public TimeSpan? Domingo4 { get; set; }
        /// <summary>
        /// Indica si el horario esta activo o inactivo
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Fecha en la que se inicia con ese horario
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha en la que se termina de usar ese horario
        /// </summary>
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
