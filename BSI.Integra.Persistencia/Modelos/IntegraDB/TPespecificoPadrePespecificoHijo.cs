using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoPadrePespecificoHijo
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico (identifica al programa especifico padre)
        /// </summary>
        public int PespecificoPadreId { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico (identifica al programa especifico hijo)
        /// </summary>
        public int PespecificoHijoId { get; set; }
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
        public Guid? IdMigracion { get; set; }
    }
}
