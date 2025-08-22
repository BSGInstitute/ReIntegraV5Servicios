using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOrigenDatoCalidadDetalle
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id de la tabla mkt.T_OrigenDatoCalidad
        /// </summary>
        public int IdOrigenDatoCalidad { get; set; }
        /// <summary>
        /// bandera de dato de calidad
        /// </summary>
        public bool DatosCalidad { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad muy alta de asignacion regular
        /// </summary>
        public bool MuyAltaAr { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad muy alta de asignacion directa
        /// </summary>
        public bool MuyAltaAd { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad alta de asignacion directa
        /// </summary>
        public bool AltaAd { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad alta de asignacion regular
        /// </summary>
        public bool AltaAr { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad media asignacion directa
        /// </summary>
        public bool MediaAd { get; set; }
        /// <summary>
        /// bandera de datos de probabilidad media asignacion regular
        /// </summary>
        public bool MediaAr { get; set; }
        /// <summary>
        /// estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
