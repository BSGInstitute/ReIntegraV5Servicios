using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las sub-categorias a las que pertencen las solicitudes.
    /// </summary>
    public partial class TSolicitudTisubCategorium
    {
        public TSolicitudTisubCategorium()
        {
            TSolicitudTis = new HashSet<TSolicitudTi>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la categoria a la que pertenece
        /// </summary>
        public int IdSolicitudTicategoria { get; set; }
        /// <summary>
        /// Nombre de la subcategoria
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Numero de minutos de duración
        /// </summary>
        public int DuracionMinutos { get; set; }
        /// <summary>
        /// Flag que valida el estado de la subcategoria
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico por ultima vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
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

        public virtual TSolicitudTicategorium IdSolicitudTicategoriaNavigation { get; set; } = null!;
        public virtual ICollection<TSolicitudTi> TSolicitudTis { get; set; }
    }
}
