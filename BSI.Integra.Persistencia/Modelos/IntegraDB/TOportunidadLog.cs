using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadLog
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OportunidadNew
        /// </summary>
        public int? IdOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key tCentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Es Foreing Key tPersonal
        /// </summary>
        public int? IdPersonalAsignado { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad anterior
        /// </summary>
        public int? IdFaseOportunidadAnt { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_FaseOportunidad
        /// </summary>
        public int? IdFaseOportunidad { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Origen
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key tAlumnos
        /// </summary>
        public int? IdContacto { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime? FechaLog { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadesDetalleNew
        /// </summary>
        public int? IdActividadDetalle { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_Ocurrencia
        /// </summary>
        public int? IdOcurrencia { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_OcurrenciaActividad
        /// </summary>
        public int? IdOcurrenciaActividad { get; set; }
        /// <summary>
        /// Descripcion del log
        /// </summary>
        public string? Comentario { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_CategoriaOrigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Es Foreing Key TFM_ConjuntoAnuncios
        /// </summary>
        public int? IdConjuntoAnuncio { get; set; }
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
        /// Si una de las fases oportunidades se llena
        /// </summary>
        public bool? FasesActivas { get; set; }
        /// <summary>
        /// Fecha del registro campaña
        /// </summary>
        public DateTime? FechaRegistroCampania { get; set; }
        /// <summary>
        /// Id estatico 
        /// </summary>
        public int? IdFaseOportunidadPf { get; set; }
        /// <summary>
        /// Id pais Interesado Concreto
        /// </summary>
        public string? CodigoPagoIc { get; set; }
        /// <summary>
        /// Id asesor anterior
        /// </summary>
        public int? IdAsesorAnt { get; set; }
        /// <summary>
        /// Id centro costo
        /// </summary>
        public int? IdCentroCostoAnt { get; set; }
        /// <summary>
        /// Fecha final log
        /// </summary>
        public DateTime? FechaFinLog { get; set; }
        /// <summary>
        /// Fecha del cambio de fase
        /// </summary>
        public DateTime? FechaCambioFase { get; set; }
        /// <summary>
        /// Cambio de fase
        /// </summary>
        public bool? CambioFase { get; set; }
        /// <summary>
        /// Fecha del cambio de fase Inscrito
        /// </summary>
        public DateTime? FechaCambioFaseIs { get; set; }
        /// <summary>
        /// Cambio de fase Inscrito
        /// </summary>
        public bool? CambioFaseIs { get; set; }
        /// <summary>
        /// Fecha cambio fase anterior
        /// </summary>
        public DateTime? FechaCambioFaseAnt { get; set; }
        /// <summary>
        /// Fecha cambio asesor 
        /// </summary>
        public DateTime? FechaCambioAsesor { get; set; }
        /// <summary>
        /// Fecha cambio asesor anterior
        /// </summary>
        public DateTime? FechaCambioAsesorAnt { get; set; }
        /// <summary>
        /// Cambio fase asesor
        /// </summary>
        public int? CambioFaseAsesor { get; set; }
        /// <summary>
        /// Cuando se cambia de asesor
        /// </summary>
        public int? CicloRn2 { get; set; }
        public int? IdSubCategoriaDato { get; set; }
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
        /// <summary>
        /// Llave foranea con la tabla T_ClasificacionPersona
        /// </summary>
        public int? IdClasificacionPersona { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PersonalAreaTrabajo
        /// </summary>
        public int? IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// FK de T_OcurrenciaAlterno
        /// </summary>
        public int? IdOcurrenciaAlterno { get; set; }
        /// <summary>
        /// FK de T_OcurrenciaActividadAlterno
        /// </summary>
        public int? IdOcurrenciaActividadAlterno { get; set; }
        /// <summary>
        /// Indica si la oportunidad está relacionada con venta cruzada de marketing
        /// </summary>
        public bool? VentaCruzadaMarketing { get; set; }

        public virtual TOportunidad? IdOportunidadNavigation { get; set; }
    }
}
