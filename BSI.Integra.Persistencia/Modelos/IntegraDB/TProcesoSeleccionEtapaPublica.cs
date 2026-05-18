using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catalogo de etapas publicas del proceso de seleccion. Se usa para mostrar al postulante externo en que fase del proceso se encuentra dentro de la bolsa de trabajo.
    /// </summary>
    public partial class TProcesoSeleccionEtapaPublica
    {
        public TProcesoSeleccionEtapaPublica()
        {
            TProcesoSeleccionEtapas = new HashSet<TProcesoSeleccionEtapa>();
        }

        /// <summary>
        /// Identificador unico de la etapa publica del proceso de seleccion
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la etapa publica mostrado al postulante externo (ej: Evaluacion, Entrevista, Finalizado)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Indicador de registro activo: 1 = activo, 0 = eliminado logicamente
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que inserto el registro por primera vez
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista, autogenerado por SQL Server
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TProcesoSeleccionEtapa> TProcesoSeleccionEtapas { get; set; }
    }
}
