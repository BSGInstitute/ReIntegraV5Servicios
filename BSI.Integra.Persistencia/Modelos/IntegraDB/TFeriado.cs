using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFeriado
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? Tipo { get; set; }
        /// <summary>
        /// Fecha del feriado
        /// </summary>
        public DateTime Dia { get; set; }
        /// <summary>
        /// Motivo del feriado
        /// </summary>
        public string Motivo { get; set; } = null!;
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int Frecuencia { get; set; }
        /// <summary>
        /// Es foreing key T_TroncalCiudad
        /// </summary>
        public int IdTroncalCiudad { get; set; }
        /// <summary>
        /// Estado del registro (creado, eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que hizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla original
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
