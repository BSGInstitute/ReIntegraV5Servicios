using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TActividadCabecera
    {
        public TActividadCabecera()
        {
            TActividadDetalleGestionContactos = new HashSet<TActividadDetalleGestionContacto>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la actividad cabecera
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la cabecera
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime? FechaCreacion2 { get; set; }
        /// <summary>
        /// Tiempo estimado de la duracion
        /// </summary>
        public int DuracionEstimada { get; set; }
        /// <summary>
        /// Reprogramacion manual
        /// </summary>
        public bool ReproManual { get; set; }
        /// <summary>
        /// Reprogramacion automatica
        /// </summary>
        public bool ReproAutomatica { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_PLANTILLA
        /// </summary>
        public int Idplantilla { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_ActividadBase
        /// </summary>
        public int IdActividadBase { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime? FechaModificacion2 { get; set; }
        /// <summary>
        /// Llamada valida
        /// </summary>
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
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
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Clave foranea de la tabla T_ConjuntoLista
        /// </summary>
        public int? IdConjuntoLista { get; set; }
        /// <summary>
        /// ClaveForanea de la tabla T_Frecuencia
        /// </summary>
        public int? IdFrecuencia { get; set; }
        /// <summary>
        /// Indica Fecha en la que se Iniciará la Actividad
        /// </summary>
        public DateTime? FechaInicioActividad { get; set; }
        /// <summary>
        /// Indica el dia de mes en el que se ejecutara la actividad
        /// </summary>
        public byte? DiaFrecuenciaMensual { get; set; }
        /// <summary>
        /// define si la frecuencia de ejecucion es repetitivo
        /// </summary>
        public bool? EsRepetitivo { get; set; }
        /// <summary>
        /// define la hora de envio
        /// </summary>
        public TimeSpan? HoraInicio { get; set; }
        /// <summary>
        /// define la fecha de fin si es repetitivo
        /// </summary>
        public TimeSpan? HoraFin { get; set; }
        /// <summary>
        ///  numero del intervalo de tiempo
        /// </summary>
        public byte? CantidadIntevaloTiempo { get; set; }
        /// <summary>
        /// clave foranea de la tabla T_TiempoIntervalo
        /// </summary>
        public int? IdTiempoIntervalo { get; set; }
        /// <summary>
        /// Define si la Actividad sera Ejecutada o no
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Fecha de Finalización de la actividad
        /// </summary>
        public DateTime? FechaFinActividad { get; set; }
        /// <summary>
        /// FK tabla gp.T_PersonalAreaTrabajo, Permite diferenciar a que area pertenece las actividades cabecera
        /// </summary>
        public int IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Es clave foranea de t_FacebookCampanha
        /// </summary>
        public int? IdFacebookCampanha { get; set; }
        /// <summary>
        /// Es clave foránea de T_FacebookCuentaPublicitaria
        /// </summary>
        public int? IdFacebookCuentaPublicitaria { get; set; }
        /// <summary>
        /// Flag que indica si la actividad es una actividad de envio masivo
        /// </summary>
        public bool? EsEnvioMasivo { get; set; }
        /// <summary>
        /// FK de T_TipoDato
        /// </summary>
        public int? IdTipoDato { get; set; }

        public virtual ICollection<TActividadDetalleGestionContacto> TActividadDetalleGestionContactos { get; set; }
    }
}
