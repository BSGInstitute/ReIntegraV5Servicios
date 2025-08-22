using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionAutomaticaTemp
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombres completos
        /// </summary>
        public string? Nombres { get; set; }
        /// <summary>
        /// Apeliidos completos
        /// </summary>
        public string? Apellidos { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string? Correo { get; set; }
        /// <summary>
        /// Telefono fijo
        /// </summary>
        public string? Fijo { get; set; }
        /// <summary>
        /// Telefono movil
        /// </summary>
        public string? Movil { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREAFORMACION
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CARGO
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREATRABAJO
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_INDUSTRIA
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Nombre del programa general
        /// </summary>
        public string? NombrePrograma { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Codigo del centro de costo
        /// </summary>
        public string? CentroCosto { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int? IdFaseOportunidad { get; set; }
        /// <summary>
        /// Origen del registro
        /// </summary>
        public string? Origen { get; set; }
        /// <summary>
        /// Estado del registro si ha sido procesado es 1 si no es 0
        /// </summary>
        public bool? Procesado { get; set; }
        /// <summary>
        /// Es Foreing Key TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// POR DEFINIR
        /// </summary>
        public Guid? IdFaseOportunidadPortal { get; set; }
        /// <summary>
        /// Fechad e creacion del registro
        /// </summary>
        public DateTime? FechaRegistroCampania { get; set; }
        /// <summary>
        /// Es foreignkey de T_TiempoCapacitacion
        /// </summary>
        public int? IdTiempoCapacitacion { get; set; }
        /// <summary>
        /// Id de categoria dato
        /// </summary>
        public int? IdCategoriaDato { get; set; }
        /// <summary>
        /// Es Foreign Key de TTipoInteraccion
        /// </summary>
        public int? IdTipoInteraccion { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        /// <summary>
        /// Url de origen
        /// </summary>
        public string? UrlOrigen { get; set; }
        /// <summary>
        /// id de pagina
        /// </summary>
        public int? IdPagina { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
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
        /// <summary>
        /// Llave foranea de la tabla mkt.T_AnuncioFacebook
        /// </summary>
        public int? IdAnuncioFacebook { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_FacebookFormularioLeadgen
        /// </summary>
        public int? IdFacebookFormularioLeadgen { get; set; }
        public bool? AptoProcesamiento { get; set; }
    }
}
