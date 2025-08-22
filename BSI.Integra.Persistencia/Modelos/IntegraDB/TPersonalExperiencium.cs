using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalExperiencium
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Fk T_AreaTrabajo
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Fk T_Cargo
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Fecha ingreso al trabajo
        /// </summary>
        public DateTime FechaIngreso { get; set; }
        /// <summary>
        /// Fecha retiro al trabajo
        /// </summary>
        public DateTime FechaRetiro { get; set; }
        /// <summary>
        /// Motivo de retiro del trabajo
        /// </summary>
        public string? MotivoRetiro { get; set; }
        /// <summary>
        /// Nombre jefe inmediato trabajo
        /// </summary>
        public string? NombreJefeInmediato { get; set; }
        /// <summary>
        /// Telefono jefe inmediato
        /// </summary>
        public string? TelefonoJefeInmediato { get; set; }
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
        /// <summary>
        /// FK T_Industria
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// FK de T_PersonalArchivo
        /// </summary>
        public int? IdPersonalArchivo { get; set; }
    }
}
