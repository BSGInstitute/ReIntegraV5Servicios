using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTipoCambioCol
    {
        public TTipoCambioCol()
        {
            TTipoCambioMoneda = new HashSet<TTipoCambioMonedum>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de cambio de pesos a dolares
        /// </summary>
        public double PesosDolares { get; set; }
        /// <summary>
        /// Tipo de cambio de dolares a pesos
        /// </summary>
        public double DolaresPesos { get; set; }
        /// <summary>
        /// fecha del tipo de cambio
        /// </summary>
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
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

        public virtual ICollection<TTipoCambioMonedum> TTipoCambioMoneda { get; set; }
    }
}
