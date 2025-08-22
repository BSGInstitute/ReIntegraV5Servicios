using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAmbiente
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Detalle del ambiente
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es foreing key TPLA_Locaciones
        /// </summary>
        public int IdLocacion { get; set; }
        /// <summary>
        /// Es foreing key TPLA_TipoAmbiente
        /// </summary>
        public int IdTipoAmbiente { get; set; }
        /// <summary>
        /// Capacidad de personas en el ambiente
        /// </summary>
        public int Capacidad { get; set; }
        /// <summary>
        /// Indica si el ambiente es virtual o no
        /// </summary>
        public bool Virtual { get; set; }
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
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
