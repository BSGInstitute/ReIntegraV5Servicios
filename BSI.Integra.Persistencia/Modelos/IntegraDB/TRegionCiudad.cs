using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRegionCiudad
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la ciudad
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Codigo BS
        /// </summary>
        public int? CodigoBs { get; set; }
        /// <summary>
        /// Denominación BS
        /// </summary>
        public string? DenominacionBs { get; set; }
        /// <summary>
        /// Nombre corto
        /// </summary>
        public string? NombreCorto { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha de creacion del registro
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
