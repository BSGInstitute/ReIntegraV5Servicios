using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCentroCosto
    {
        public TCentroCosto()
        {
            TCampaniaGeneralDetalles = new HashSet<TCampaniaGeneralDetalle>();
            TPespecificos = new HashSet<TPespecifico>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tArea
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Es Foreing Key tSubArea
        /// </summary>
        public int? IdSubArea { get; set; }
        /// <summary>
        /// Codigo del programa general
        /// </summary>
        public string IdPgeneral { get; set; } = null!;
        /// <summary>
        /// Codigo del centro de costos
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo numero del centro de costos
        /// </summary>
        public string Codigo { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key tAreaCC
        /// </summary>
        public string? IdAreaCc { get; set; }
        /// <summary>
        /// Numero de registro
        /// </summary>
        public int? Ismtotales { get; set; }
        /// <summary>
        /// Numero de registro
        /// </summary>
        public int? Icpftotales { get; set; }
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
        /// Sistema Automatico Fecha creacion
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

        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalles { get; set; }
        public virtual ICollection<TPespecifico> TPespecificos { get; set; }
    }
}
