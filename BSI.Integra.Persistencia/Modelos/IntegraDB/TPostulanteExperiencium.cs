using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteExperiencium
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Fk T_Empresa
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Campo que se llena en caso la empresa del postulante no se encuentre en la lista de empresas
        /// </summary>
        public string? OtraEmpresa { get; set; }
        /// <summary>
        /// Fk T_Cargo
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Fk T_AreaTrabajo
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Fk T_Industria
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Fecha inicio trabajo
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha fin trabajo
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Nombre jefe inmediato
        /// </summary>
        public string? NombreJefe { get; set; }
        /// <summary>
        /// Numero celular jefe inmediato
        /// </summary>
        public string? NumeroJefe { get; set; }
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
        /// Define si el trabajo es hasta la actualidad
        /// </summary>
        public bool? AlaActualidad { get; set; }
        /// <summary>
        /// Valida si es su ultima experiencia laboral
        /// </summary>
        public bool? EsUltimoEmpleo { get; set; }
        /// <summary>
        /// Salario de trabajo
        /// </summary>
        public decimal? Salario { get; set; }
        /// <summary>
        /// Funciones desarrolladas por el postulante
        /// </summary>
        public string? Funcion { get; set; }
        /// <summary>
        /// Comisión adicional recibida relacionada a experiencia de Postulante
        /// </summary>
        public decimal? SalarioComision { get; set; }
        /// <summary>
        /// Fk de T_Moneda para especificar la moneda en la cual el postulante recibía su salario y salario comisión
        /// </summary>
        public int? IdMoneda { get; set; }
    }
}
