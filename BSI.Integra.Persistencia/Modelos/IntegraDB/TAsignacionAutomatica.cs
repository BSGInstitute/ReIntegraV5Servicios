using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionAutomatica
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Pimer nombre
        /// </summary>
        public string? Nombre1 { get; set; }
        /// <summary>
        /// Segundo nombre
        /// </summary>
        public string? Nombre2 { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Telefono fijo
        /// </summary>
        public string? Telefono { get; set; }
        /// <summary>
        /// Telefono movil
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Nombre completo del programa
        /// </summary>
        public string? NombrePrograma { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Origen
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int? IdFaseOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREAFORMACION
        /// </summary>
        public int? IdAreaFormacion { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AREATRABAJO
        /// </summary>
        public int? IdAreaTrabajo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_INDUSTRIA
        /// </summary>
        public int? IdIndustria { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CARGO
        /// </summary>
        public int? IdCargo { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Si se ha balidado el registro
        /// </summary>
        public bool? Validado { get; set; }
        /// <summary>
        /// Si ha sido corregido
        /// </summary>
        public bool? Corregido { get; set; }
        /// <summary>
        /// De donde viene el registro
        /// </summary>
        public string? OrigenCampania { get; set; }
        /// <summary>
        /// Es Foreing Key TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// TCRM_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_AsignacionAutomaticaOrigen
        /// </summary>
        public int? IdAsignacionAutomaticaOrigen { get; set; }
        /// <summary>
        /// DESCRIPCION
        /// </summary>
        public int? IdCampaniaScoring { get; set; }
        /// <summary>
        /// Fecha de registro de la campanha
        /// </summary>
        public DateTime? FechaRegistroCampania { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidadPortal
        /// </summary>
        public Guid? IdFaseOportunidadPortal { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Tpersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// FK de TiempoCapacitacion
        /// </summary>
        public int? IdTiempoCapacitacion { get; set; }
        /// <summary>
        /// TCRM_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaDato { get; set; }
        /// <summary>
        /// TMK_TipoInteraccion
        /// </summary>
        public int? IdTipoInteraccion { get; set; }
        /// <summary>
        /// TCRM_Subcategoriadato
        /// </summary>
        public int? IdSubCategoriaDato { get; set; }
        /// <summary>
        /// TMK_InteraccionFormulario
        /// </summary>
        public int? IdInteraccionFormulario { get; set; }
        /// <summary>
        /// URL de Origen
        /// </summary>
        public string? UrlOrigen { get; set; }
        /// <summary>
        /// Probabilidad Actual
        /// </summary>
        public double? ProbabilidadActual { get; set; }
        /// <summary>
        /// Decripcion de probabilidad actual
        /// </summary>
        public string? ProbabilidadActualDesc { get; set; }
        /// <summary>
        /// TPW_PaginasWeb
        /// </summary>
        public int? IdPagina { get; set; }
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
        /// <summary>
        /// Fecha Programada
        /// </summary>
        public DateTime? FechaProgramada { get; set; }
        /// <summary>
        /// Clave foranea de TAlumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_AnuncioFacebook
        /// </summary>
        public int? IdAnuncioFacebook { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_AsignacionAutomatica_Temp
        /// </summary>
        public int? IdAsignacionAutomaticaTemp { get; set; }
        public bool? AptoProcesamiento { get; set; }
    }
}
