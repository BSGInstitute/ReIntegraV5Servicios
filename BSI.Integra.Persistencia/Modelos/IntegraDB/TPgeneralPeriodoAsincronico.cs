using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPgeneralPeriodoAsincronico
    {
        public TPgeneralPeriodoAsincronico()
        {
            TPgenerals = new HashSet<TPgeneral>();
        }

        /// <summary>
        /// Id con valor unico
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del periodo
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Cantidad de meses(CM - 1)
        /// </summary>
        public int Periodo { get; set; }
        /// <summary>
        /// Activo / Inactivo
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creador de la tabla
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Ultimo usuario modificador
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de la creacion de la tabla
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de la modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Rowversion de la tabla
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Idmigracion
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TPgeneral> TPgenerals { get; set; }
    }
}
