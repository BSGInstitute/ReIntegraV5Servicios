using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaGeneralDetalleArea
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de la tabla t_areacapacitacion
        /// </summary>
        public int IdAreaCapacitacion { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniageneraldetalle
        /// </summary>
        public int IdCampaniaGeneralDetalle { get; set; }
        /// <summary>
        /// Flag que indica el estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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

        public virtual TAreaCapacitacion IdAreaCapacitacionNavigation { get; set; } = null!;
        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; } = null!;
    }
}
