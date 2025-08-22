using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPartnerContactoPw
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// identificador del partner
        /// </summary>
        public int IdPartner { get; set; }
        /// <summary>
        /// Nombres completos del contacto para el parner
        /// </summary>
        public string Nombres { get; set; } = null!;
        /// <summary>
        /// Apllidos completos del contacto para el parner
        /// </summary>
        public string Apellidos { get; set; } = null!;
        /// <summary>
        /// Correo principal electronico del contacto para el parner
        /// </summary>
        public string Email1 { get; set; } = null!;
        /// <summary>
        /// Correo alternativo electronico del contacto para el parner
        /// </summary>
        public string? Email2 { get; set; }
        /// <summary>
        /// Telefono pricipal para el contacto para el parner
        /// </summary>
        public string Telefono1 { get; set; } = null!;
        /// <summary>
        /// Telefono alternativo para el contacto para el parner
        /// </summary>
        public string? Telefono2 { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
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
