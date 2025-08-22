using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSdtInfGen
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Sentinel
        /// </summary>
        public int? IdSentinel { get; set; }
        /// <summary>
        /// Numero de DNI
        /// </summary>
        public string? Dni { get; set; }
        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }
        /// <summary>
        /// Sexo
        /// </summary>
        public string? Sexo { get; set; }
        /// <summary>
        /// Digito
        /// </summary>
        public string? Digito { get; set; }
        /// <summary>
        /// Digito anterior
        /// </summary>
        public string? DigitoAnterior { get; set; }
        /// <summary>
        /// Numero de RUC
        /// </summary>
        public string? Ruc { get; set; }
        /// <summary>
        /// razon Social 
        /// </summary>
        public string? RazonSocial { get; set; }
        /// <summary>
        /// Nombre comercial
        /// </summary>
        public string? NombreComercial { get; set; }
        /// <summary>
        /// Fecha de dado de baja
        /// </summary>
        public DateTime? FechaBaja { get; set; }
        /// <summary>
        /// Tipo de contribuyente
        /// </summary>
        public string? TipoContribuyente { get; set; }
        /// <summary>
        /// Codigo del tipo de contribuyente
        /// </summary>
        public string? CodigoTipoContribuyente { get; set; }
        /// <summary>
        /// Estado del contribuyente
        /// </summary>
        public string? EstadoContribuyente { get; set; }
        /// <summary>
        /// Codigo del estado del contribuyente
        /// </summary>
        public string? CodigoEstadoContribuyente { get; set; }
        /// <summary>
        /// Condicion del contribuyente
        /// </summary>
        public string? CondicionContribuyente { get; set; }
        /// <summary>
        /// Codigo de la condicion del contribuyente
        /// </summary>
        public string? CodigoCondicionContribuyente { get; set; }
        /// <summary>
        /// Actividad economica
        /// </summary>
        public string? ActividadEconomica { get; set; }
        /// <summary>
        /// Clasificación uniforme de las actividades económicas por procesos productivos
        /// </summary>
        public string? Ciiu { get; set; }
        /// <summary>
        /// Actividad economica alternativa
        /// </summary>
        public string? ActividadEconomica2 { get; set; }
        /// <summary>
        /// Clasificación uniforme de las actividades económicas por procesos productivos alternativa
        /// </summary>
        public string? Ciiu2 { get; set; }
        /// <summary>
        /// Actividad economica alternativa
        /// </summary>
        public string? ActividadEconomica3 { get; set; }
        /// <summary>
        /// Clasificación uniforme de las actividades económicas por procesos productivos alternativa
        /// </summary>
        public string? Ciiu3 { get; set; }
        /// <summary>
        /// Fecha inicial de la actividad
        /// </summary>
        public DateTime? FechaActividad { get; set; }
        /// <summary>
        /// Direccion
        /// </summary>
        public string? Direccion { get; set; }
        /// <summary>
        /// Referencia de la dirección
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Departamento donde se ubica
        /// </summary>
        public string? Departamento { get; set; }
        /// <summary>
        /// Provincia donde se ubica
        /// </summary>
        public string? Provincia { get; set; }
        /// <summary>
        /// Distrito donde se ubica
        /// </summary>
        public string? Distrito { get; set; }
        /// <summary>
        /// Codigo de ubicacion geografica
        /// </summary>
        public string? Ubigeo { get; set; }
        /// <summary>
        /// Fecha de constitucion
        /// </summary>
        public DateTime? FechaConstitucion { get; set; }
        /// <summary>
        /// Actividad de comercio exterior
        /// </summary>
        public string? ActvidadComercioExterior { get; set; }
        /// <summary>
        /// Codigo de actividad de comercio exterior
        /// </summary>
        public string? CodigoActividadComerExt { get; set; }
        /// <summary>
        /// Codigo de dependencia
        /// </summary>
        public string? CodigoDependencia { get; set; }
        /// <summary>
        /// Nombre de la dependencia
        /// </summary>
        public string? Dependencia { get; set; }
        /// <summary>
        /// Numero de folio
        /// </summary>
        public string? Folio { get; set; }
        /// <summary>
        /// Numero de asiento
        /// </summary>
        public string? Asiento { get; set; }
        /// <summary>
        /// Numero de tomo
        /// </summary>
        public string? Tomo { get; set; }
        /// <summary>
        /// Partida registral
        /// </summary>
        public string? PartidaReg { get; set; }
        /// <summary>
        /// Patron
        /// </summary>
        public string? Patron { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual TSentinel? IdSentinelNavigation { get; set; }
    }
}
