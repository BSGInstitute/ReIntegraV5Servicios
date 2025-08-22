using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLocacion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la locacion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Pais de la locacion (Identificador de la columna CodigoPais de la tabla TCRM_Pais)
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Ciudad de la locacion (identificador de la tabla TCRM_Ciudad)
        /// </summary>
        public int IdRegion { get; set; }
        /// <summary>
        /// Es foreing key TCRM_RegionCiudades
        /// </summary>
        public int IdCiudad { get; set; }
        public string? Direccion { get; set; }
        /// <summary>
        /// Estado del registro (creado, eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que hizo la ultima modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
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
