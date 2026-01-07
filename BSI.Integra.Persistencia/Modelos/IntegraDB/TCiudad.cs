using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCiudad
    {
        public TCiudad()
        {
            TDocentePostulantes = new HashSet<TDocentePostulante>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo de la region o ciudad
        /// </summary>
        public int Codigo { get; set; }
        /// <summary>
        /// Nombre de la ciudad
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Codigo del pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Longitud total del numero de telefono movil
        /// </summary>
        public int LongCelular { get; set; }
        /// <summary>
        /// Longitud total del numero de telefono fijo
        /// </summary>
        public int LongTelefono { get; set; }
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
        /// Sistema Automatico Fecha de modificacion
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
        public int? LongCelularAlterno { get; set; }

        public virtual ICollection<TDocentePostulante> TDocentePostulantes { get; set; }
    }
}
