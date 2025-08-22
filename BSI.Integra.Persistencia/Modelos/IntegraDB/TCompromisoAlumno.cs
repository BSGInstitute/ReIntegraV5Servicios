using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCompromisoAlumno
    {
        /// <summary>
        /// Es llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es clave foranea de fin.T_CronogramaPagoDetalleFinal
        /// </summary>
        public int IdCronogramaPagoDetalleFinal { get; set; }
        /// <summary>
        /// Fecha donde se compromete el alumno a pagar
        /// </summary>
        public DateTime FechaCompromiso { get; set; }
        /// <summary>
        /// Fecha donde se genera el compromiso del alumno
        /// </summary>
        public DateTime FechaGeneracionCompromiso { get; set; }
        /// <summary>
        /// Monto el cual se compromete el alumno a pagar
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Clave foranea de pla.T_Moneda
        /// </summary>
        public int? IdMoneda { get; set; }
        /// <summary>
        /// Numero de compromiso
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Estado del registro
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
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
