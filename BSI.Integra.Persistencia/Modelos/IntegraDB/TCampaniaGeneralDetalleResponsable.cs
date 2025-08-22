using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaGeneralDetalleResponsable
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniageneraldetalle
        /// </summary>
        public int IdCampaniaGeneralDetalle { get; set; }
        /// <summary>
        /// FK de la tabla t_personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Valor generado para el dia 1
        /// </summary>
        public int Dia1 { get; set; }
        /// <summary>
        /// Valor generado para el dia 2
        /// </summary>
        public int Dia2 { get; set; }
        /// <summary>
        /// Valor generado para el dia 3
        /// </summary>
        public int Dia3 { get; set; }
        /// <summary>
        /// Valor generado para el dia 4
        /// </summary>
        public int Dia4 { get; set; }
        /// <summary>
        /// Valor generado para el dia 5
        /// </summary>
        public int Dia5 { get; set; }
        /// <summary>
        /// Valor total generado
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Flag que indica el estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
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
        public Guid? IdMigracion { get; set; }

        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; } = null!;
    }
}
