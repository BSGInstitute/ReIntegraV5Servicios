using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCuentaCorriente
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; set; } = null!;
        /// <summary>
        /// Es foreing key tCiudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// sucursal
        /// </summary>
        public string Sucursal { get; set; } = null!;
        /// <summary>
        /// Llave Foranea con T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Numero de cuenta corta
        /// </summary>
        public string Cuenta { get; set; } = null!;
        /// <summary>
        /// Es foreing key tEntidadFinanciera
        /// </summary>
        public int IdBanco { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
