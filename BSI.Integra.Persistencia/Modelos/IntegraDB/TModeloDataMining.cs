using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModeloDataMining
    {
        public int Id { get; set; }
        /// <summary>
        /// Probalidad inicial del contacto
        /// </summary>
        public decimal? ProbabilidadInicial { get; set; }
        public int? IdProbabilidadRegistroPwInicial { get; set; }
        /// <summary>
        /// Probalidad actual del contacto
        /// </summary>
        public decimal? ProbabilidadActual { get; set; }
        /// <summary>
        /// Relacion a la tabla ProbabilidadRegistro_PW
        /// </summary>
        public int? IdProbabilidadRegistroPwActual { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Fecha de creacion del contacto
        /// </summary>
        public DateTime? FechaCreacionContacto { get; set; }
        /// <summary>
        /// Fecha de creacion de la oportunidad
        /// </summary>
        public DateTime? FechaCreacionOportunidad { get; set; }
        /// <summary>
        /// Dias transcurridos entre la creación de la oportunidad y la creación del contacto
        /// </summary>
        public int? DiasEntreCreacionContactoOportunidad { get; set; }
        /// <summary>
        /// Numero de nombres
        /// </summary>
        public int? Nombres { get; set; }
        /// <summary>
        /// Numero de apellidos
        /// </summary>
        public int? Apellidos { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdPais { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CARGO
        /// </summary>
        public int? IdCargo { get; set; }
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
        /// Validacion del registro email por condicion registrada (valido, no valido y sin valor)
        /// </summary>
        public int? Email1 { get; set; }
        /// <summary>
        /// Validacion del registro telefono fijo por condicion registrada (valido, no valido y sin valor)
        /// </summary>
        public int? TelefonoFijo { get; set; }
        /// <summary>
        /// Validacion del registro telefono movil por condicion registrada (valido, no valido y sin valor)
        /// </summary>
        public int? TelefonoMovil { get; set; }
        /// <summary>
        /// Razon Social Empresa
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Tamanho de la empresa
        /// </summary>
        public int? IdTamanioEmpresa { get; set; }
        /// <summary>
        /// Codigo CIIU de la empresa - TCRM_Empresas campo em_ciiu
        /// </summary>
        public int? Ciiuempresa { get; set; }
        /// <summary>
        /// Validacion del registro telefono de la empresa por condicion registrada (valido, no valido y sin valor)
        /// </summary>
        public int? TelefonoEmpresa { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ciudad
        /// </summary>
        public int? IdCiudadEmpresa { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Pais
        /// </summary>
        public int? IdPaisEmpresa { get; set; }
        /// <summary>
        /// Es foreign key pla.T_CentroCosto Oportunidad actual del contacto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Es Foreing Key tArea
        /// </summary>
        public int? IdArea { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_AreaCapacitacion
        /// </summary>
        public int? IdAreaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key tSubArea
        /// </summary>
        public int? IdSubArea { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_SubAreaCapacitacion
        /// </summary>
        public int? IdSubAreaCapacitacion { get; set; }
        /// <summary>
        /// Es Foreing Key tPLA_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Es Foreing Key tCategoriaP (T_CategoriaPrograma)
        /// </summary>
        public int? IdCategoriaPrograma { get; set; }
        /// <summary>
        /// Duracion del programa
        /// </summary>
        public string? ProgramaGeneralDuracion { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_Partner
        /// </summary>
        public int? IdPartner { get; set; }
        /// <summary>
        /// Es Foreing Key tPEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Si el programa es presencial u online
        /// </summary>
        public int? Modalidad { get; set; }
        /// <summary>
        /// Monto del programa especifico
        /// </summary>
        public int? PrecioProgramaEspecifico { get; set; }
        /// <summary>
        /// Monto en dolares del programa especifico
        /// </summary>
        public int? PrecioProgramaEspecificoDolares { get; set; }
        /// <summary>
        /// Es Foreing Key TPW_Moneda
        /// </summary>
        public int? MonedaPrecioProgramaEspecifico { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Origen
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Fase Maxima Alcanzada (actual)
        /// </summary>
        public string? FaseMaximaAlcanzada { get; set; }
        /// <summary>
        /// Fase Actual (actual)
        /// </summary>
        public string? FaseActual { get; set; }
        /// <summary>
        /// Es Foreign key de  T_ActividadDetalle. Actividad final (actual)
        /// </summary>
        public int? IdActividadFinal { get; set; }
        /// <summary>
        /// Es foreign key de T_Ocurrencia. Ocurrencia final (actual)
        /// </summary>
        public int? IdOcurrenciaFinal { get; set; }
        /// <summary>
        /// Asesor (actual)
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// T_SubCategoriaDato
        /// </summary>
        public int? IdSubCategoriaDato { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[]? RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAlumno? IdAlumnoNavigation { get; set; }
        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
    }
}
