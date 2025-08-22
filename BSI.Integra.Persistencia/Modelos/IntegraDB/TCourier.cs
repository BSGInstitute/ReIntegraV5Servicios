using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCourier
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del courier
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Id de pais remitente
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Id ciudad remitente
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// Direccion
        /// </summary>
        public string Direccion { get; set; } = null!;
        /// <summary>
        /// Telefono
        /// </summary>
        public string Telefono { get; set; } = null!;
        /// <summary>
        /// Pagina Web
        /// </summary>
        public string Url { get; set; } = null!;
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
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
