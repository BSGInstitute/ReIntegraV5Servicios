using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TBloqueHorarioProcesaOportunidad
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Estado activo o no activo
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Descripcion del dato
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Campo sede del dato
        /// </summary>
        public string Sede { get; set; } = null!;
        /// <summary>
        /// Campo Dia
        /// </summary>
        public string Dia { get; set; } = null!;
        /// <summary>
        /// Campo turno manhana del dato
        /// </summary>
        public bool TurnoM { get; set; }
        /// <summary>
        /// Campo hora de inicio turno manhana
        /// </summary>
        public TimeSpan HoraInicioM { get; set; }
        /// <summary>
        /// Campor hora final turno manhana
        /// </summary>
        public TimeSpan HoraFinM { get; set; }
        /// <summary>
        /// Campo turno tarde del dato
        /// </summary>
        public bool TurnoT { get; set; }
        /// <summary>
        /// Campo hora inicio de turno tarde
        /// </summary>
        public TimeSpan HoraInicioT { get; set; }
        /// <summary>
        /// Campo hora final de turno tarde
        /// </summary>
        public TimeSpan HoraFinT { get; set; }
        /// <summary>
        /// Campo probabilidad de la oportunidad
        /// </summary>
        public string ProbabilidadOportunidad { get; set; } = null!;
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Campo booleano de lanzamiento
        /// </summary>
        public bool Prelanzamiento { get; set; }
        /// <summary>
        /// Campo Id de la semana
        /// </summary>
        public int? IdDiaSemana { get; set; }
    }
}
