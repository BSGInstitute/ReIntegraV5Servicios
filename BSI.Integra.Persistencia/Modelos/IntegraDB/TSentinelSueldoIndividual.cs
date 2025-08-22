using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSueldoIndividual
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero de DNI
        /// </summary>
        public string? Dni { get; set; }
        /// <summary>
        /// Nombres
        /// </summary>
        public string? Nombres { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Nombre de la industria
        /// </summary>
        public string? Industria { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_INDUSTRIA
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string? TamanioEmpresa { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TamanioEmpresas
        /// </summary>
        public int? IdTamanioEmpresa { get; set; }
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        public string? EmpresaNombre { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Empresas
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Nombre del cargo
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CARGO
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Nombre del area de trabajo
        /// </summary>
        public string? AreaTrabajo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREATRABAJO
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Nombre del area de formacion
        /// </summary>
        public string? AreaFormacion { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREAFORMACION
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// Nombre de la Ciudad
        /// </summary>
        public string? Ciudad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int? IdCodigoCiudad { get; set; }
        /// <summary>
        /// Nombre del pais
        /// </summary>
        public string? Pais { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdCodigoPais { get; set; }
        /// <summary>
        /// Limite Inferior
        /// </summary>
        public int? SeLimiteInferior { get; set; }
        /// <summary>
        /// Limite Superior
        /// </summary>
        public int? SeLimiteSuperior { get; set; }
        /// <summary>
        /// Promedio
        /// </summary>
        public double? SePromedio { get; set; }
        /// <summary>
        /// Origen de la informacion
        /// </summary>
        public string? OrigenInformacion { get; set; }
        /// <summary>
        /// Flag de incluir
        /// </summary>
        public bool? Incluir { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
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
        /// Sistema Automatico Usuario de creacion
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
