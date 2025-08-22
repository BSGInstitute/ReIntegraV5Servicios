using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoFrecuencium
    {
        /// <summary>
        /// Es primary key 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico 
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Fecha de inicio del programa
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Frecuencia del programa
        /// </summary>
        public int Frecuencia { get; set; }
        /// <summary>
        /// Numero de sesiones
        /// </summary>
        public int NroSesiones { get; set; }
        /// <summary>
        /// Es foreing key tPLA_Frecuencia 
        /// </summary>
        public int? IdFrecuencia { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Fecha de fin del programa
        /// </summary>
        public DateTime? FechaFin { get; set; }
    }
}
