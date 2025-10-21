using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidad
    {
        public TOportunidad()
        {
            TActividadDetalles = new HashSet<TActividadDetalle>();
            TAsignacionOportunidads = new HashSet<TAsignacionOportunidad>();
            TCalidadProcesamientoAlternos = new HashSet<TCalidadProcesamientoAlterno>();
            TCalidadProcesamientos = new HashSet<TCalidadProcesamiento>();
            TComprobantePagoOportunidads = new HashSet<TComprobantePagoOportunidad>();
            TContadorBicLogs = new HashSet<TContadorBicLog>();
            TGoogleAdsConversionQueues = new HashSet<TGoogleAdsConversionQueue>();
            TModeloDataMinings = new HashSet<TModeloDataMining>();
            TModeloPredictivoProbabilidads = new HashSet<TModeloPredictivoProbabilidad>();
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperacione>();
            TOportunidadCompetidors = new HashSet<TOportunidadCompetidor>();
            TOportunidadLogs = new HashSet<TOportunidadLog>();
            TSolucionClienteByActividads = new HashSet<TSolucionClienteByActividad>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int IdFaseOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Origen
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Comentario del asesor
        /// </summary>
        public string? UltimoComentario { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesDetalleNew
        /// </summary>
        public int? IdActividadDetalleUltima { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesCabecera
        /// </summary>
        public int? IdActividadCabeceraUltima { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_EstadoActividadesdetalle
        /// </summary>
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        /// <summary>
        /// Fecha q se programo por ultima vez la actividad
        /// </summary>
        public DateTime? UltimaFechaProgramada { get; set; }
        /// <summary>
        /// Agenda a la a que pertenece TCRM_EstadoOportunidad
        /// </summary>
        public int IdEstadoOportunidad { get; set; }
        /// <summary>
        /// Ultimo estado de la ocurrencia
        /// </summary>
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        /// <summary>
        /// Fase maxima alcanzada de la oportunidad
        /// </summary>
        public int IdFaseOportunidadMaxima { get; set; }
        /// <summary>
        /// Fase inicial de la oportunidad
        /// </summary>
        public int? IdFaseOportunidadInicial { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
        /// <summary>
        /// En desarrollo
        /// </summary>
        public int? IdCampaniaScoring { get; set; }
        /// <summary>
        /// Id estatico
        /// </summary>
        public int? IdFaseOportunidadIp { get; set; }
        /// <summary>
        /// Id estatico
        /// </summary>
        public int? IdFaseOportunidadIc { get; set; }
        /// <summary>
        /// Fecha Envio Fase oportunidad Promesa Ficha
        /// </summary>
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        /// <summary>
        /// Fecha Pago Fase oportunidad Promesa Ficha
        /// </summary>
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        /// <summary>
        /// Fecha Pago Fase oportunidad Interesado Concreto
        /// </summary>
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        /// <summary>
        /// Fecha del registro campaña
        /// </summary>
        public DateTime FechaRegistroCampania { get; set; }
        /// <summary>
        /// Es Foreing Key de la tabla FaseOportunidadPortal - BD bsconsult7V2
        /// </summary>
        public Guid? IdFaseOportunidadPortal { get; set; }
        /// <summary>
        /// Id estatico 
        /// </summary>
        public int? IdFaseOportunidadPf { get; set; }
        /// <summary>
        /// Id pais Interesado Concreto
        /// </summary>
        public string? CodigoPagoIc { get; set; }
        /// <summary>
        /// Si paso por venta cruzada
        /// </summary>
        public int? FlagVentaCruzada { get; set; }
        /// <summary>
        /// FK de T_TiempoCapacitacion
        /// </summary>
        public int? IdTiempoCapacitacion { get; set; }
        /// <summary>
        /// FK de T_TiempoCapacitacion para validacion
        /// </summary>
        public int? IdTiempoCapacitacionValidacion { get; set; }
        /// <summary>
        /// FK de T_SubCategoriaDato
        /// </summary>
        public int? IdSubCategoriaDato { get; set; }
        /// <summary>
        /// FK de T_InteraccionFormulario
        /// </summary>
        public int? IdInteraccionFormulario { get; set; }
        /// <summary>
        /// URL de Origen
        /// </summary>
        public string? UrlOrigen { get; set; }
        /// <summary>
        /// Fecha para paso 2
        /// </summary>
        public DateTime? FechaPaso2 { get; set; }
        /// <summary>
        /// Si pasa por el paso 2 o no
        /// </summary>
        public bool? Paso2 { get; set; }
        /// <summary>
        /// Codigo de Mailing
        /// </summary>
        public string? CodMailing { get; set; }
        /// <summary>
        /// Id de pagina
        /// </summary>
        public int? IdPagina { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
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
        /// <summary>
        /// Almacena el numero de solicitud del contacto
        /// </summary>
        public int? NroSolicitud { get; set; }
        /// <summary>
        /// Almacena el numero de solicitud del contacto para un AreaCapacitacion
        /// </summary>
        public int? NroSolicitudPorArea { get; set; }
        /// <summary>
        /// Almacena el numero de solicitud del contacto para un SubAreaCapacitacion
        /// </summary>
        public int? NroSolicitudPorSubArea { get; set; }
        /// <summary>
        /// Almacena el numero de solicitud del contacto para un ProgramaGeneral
        /// </summary>
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        /// <summary>
        /// Almacena el numero de solicitud del contacto para un ProgramaEspecifico
        /// </summary>
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Indica el registro padre en la misma tabla T_Oportunidad
        /// </summary>
        public int? IdPadre { get; set; }
        /// <summary>
        /// Foreign Key de la tabla mkt.T_AnuncioFacebook
        /// </summary>
        public int? IdAnuncioFacebook { get; set; }
        /// <summary>
        /// Flag para determinar si el proceso de validacion de oportunidad finalizo correctamente
        /// </summary>
        public bool? ValidacionCorrecta { get; set; }
        /// <summary>
        /// Indica 
        /// </summary>
        public bool? EnLlamada { get; set; }
        /// <summary>
        /// Numero de intentos de llamada
        /// </summary>
        public int? NumeroIntentoLlamada { get; set; }

        public virtual TAnuncioFacebook? IdAnuncioFacebookNavigation { get; set; }
        public virtual TOportunidadGoogleLead TOportunidadGoogleLead { get; set; } = null!;
        public virtual ICollection<TActividadDetalle> TActividadDetalles { get; set; }
        public virtual ICollection<TAsignacionOportunidad> TAsignacionOportunidads { get; set; }
        public virtual ICollection<TCalidadProcesamientoAlterno> TCalidadProcesamientoAlternos { get; set; }
        public virtual ICollection<TCalidadProcesamiento> TCalidadProcesamientos { get; set; }
        public virtual ICollection<TComprobantePagoOportunidad> TComprobantePagoOportunidads { get; set; }
        public virtual ICollection<TContadorBicLog> TContadorBicLogs { get; set; }
        public virtual ICollection<TGoogleAdsConversionQueue> TGoogleAdsConversionQueues { get; set; }
        public virtual ICollection<TModeloDataMining> TModeloDataMinings { get; set; }
        public virtual ICollection<TModeloPredictivoProbabilidad> TModeloPredictivoProbabilidads { get; set; }
        public virtual ICollection<TOportunidadClasificacionOperacione> TOportunidadClasificacionOperaciones { get; set; }
        public virtual ICollection<TOportunidadCompetidor> TOportunidadCompetidors { get; set; }
        public virtual ICollection<TOportunidadLog> TOportunidadLogs { get; set; }
        public virtual ICollection<TSolucionClienteByActividad> TSolucionClienteByActividads { get; set; }
    }
}
