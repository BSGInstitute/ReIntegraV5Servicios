using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCaja
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo de caja
        /// </summary>
        public string CodigoCaja { get; set; } = null!;
        /// <summary>
        /// Identificador numerico de la moneda, Lave foranea con T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Identiifcador numerico de la empresa, llave foranea con la tabla T_EmpresaAutorizada
        /// </summary>
        public int IdEmpresaAutorizada { get; set; }
        /// <summary>
        /// Identiifcador numerico de la entidad financiera, llave foranea con la tabla T_EntidadFinanciera
        /// </summary>
        public int IdEntidadFinanciera { get; set; }
        /// <summary>
        /// Numero de la cuenta corriente, llave foranea con la tabla T_CuentaCorriente
        /// </summary>
        public int IdCuentaCorriente { get; set; }
        /// <summary>
        /// Es Foreing Key T_Ciudad
        /// </summary>
        public int IdCiudad { get; set; }
        /// <summary>
        /// Usuario Reponsable de la caja, Es Foreing Key T_Personal
        /// </summary>
        public int IdPersonalResponsable { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Activo { get; set; }
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
        /// Sistema Automatico Fecha de creacion
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
    }
}
