using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaFacebook
    {
        public TCampaniaFacebook()
        {
            TConjuntoAnuncioFacebooks = new HashSet<TConjuntoAnuncioFacebook>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id original de Facebook de la campania
        /// </summary>
        public string? FacebookIdCampania { get; set; }
        /// <summary>
        /// Nombre original de Facebook de la campania
        /// </summary>
        public string? FacebookNombreCampania { get; set; }
        /// <summary>
        /// Id de la cuenta de facebook
        /// </summary>
        public string? FacebookIdCuenta { get; set; }
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
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de V3
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TConjuntoAnuncioFacebook> TConjuntoAnuncioFacebooks { get; set; }
    }
}
