using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPespecificoCursoAdicional
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico (identifica al programa especifico)
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico (identifica al programa especifico adicional)
        /// </summary>
        public int IdPespecificoAdicional { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
