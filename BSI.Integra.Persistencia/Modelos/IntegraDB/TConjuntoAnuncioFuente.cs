using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConjuntoAnuncioFuente
    {
        public TConjuntoAnuncioFuente()
        {
            TConjuntoAnuncioTipoObjetivos = new HashSet<TConjuntoAnuncioTipoObjetivo>();
        }

        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la fuente del conjunto Anuncio
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Eliminacion Logica (Campo de Auditoria)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de Creacion del Registro (Campo de Auditoria)
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion de registro (Campo de Auditoria)
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// usuario de creacion del registro  (Campo de Auditoria)
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario de modificacion del registro  (Campo de Auditoria)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id del registro origen de la migracion
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TConjuntoAnuncioTipoObjetivo> TConjuntoAnuncioTipoObjetivos { get; set; }
    }
}
