using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoFrecuenciaDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key T_PEspecificoFrecuencia
        /// </summary>
        public int? IdPespecificoFrecuencia { get; set; }
        /// <summary>
        /// Numero de dia de la semana, en 0 domingo 1 lunes ...
        /// </summary>
        public byte DiaSemana { get; set; }
        /// <summary>
        /// Hora del dia
        /// </summary>
        public TimeSpan HoraDia { get; set; }
        /// <summary>
        /// Duracion en horas
        /// </summary>
        public decimal Duracion { get; set; }
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
        /// Id de la tabla anterior
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
