using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena las categorias de las solicitudes.
    /// </summary>
    public partial class TSolicitudTicategorium
    {
        public TSolicitudTicategorium()
        {
            TSolicitudTis = new HashSet<TSolicitudTi>();
            TSolicitudTisubCategoria = new HashSet<TSolicitudTisubCategorium>();
        }

        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de solicitud
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// Nombre de la solicitud 
        /// </summary>
        public string Nombre { get; set; } = null!;
        public bool TipoSistemas { get; set; }
        /// <summary>
        /// Flag que valida el estado de la solicitud
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
        /// Fecha de la ultima modificación del registro
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

        public virtual ICollection<TSolicitudTi> TSolicitudTis { get; set; }
        public virtual ICollection<TSolicitudTisubCategorium> TSolicitudTisubCategoria { get; set; }
    }
}
