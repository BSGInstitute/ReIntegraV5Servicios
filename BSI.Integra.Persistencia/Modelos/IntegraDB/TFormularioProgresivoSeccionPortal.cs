using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las secciones del portal para formularios progresivos
    /// </summary>
    public partial class TFormularioProgresivoSeccionPortal
    {
        public TFormularioProgresivoSeccionPortal()
        {
            TFormularioProgresivoConfiguracionBotons = new HashSet<TFormularioProgresivoConfiguracionBoton>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del registro
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TFormularioProgresivoConfiguracionBoton> TFormularioProgresivoConfiguracionBotons { get; set; }
    }
}
