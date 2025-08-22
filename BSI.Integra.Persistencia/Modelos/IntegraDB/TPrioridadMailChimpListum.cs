using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPrioridadMailChimpListum
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniamailing
        /// </summary>
        public int IdCampaniaMailing { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniamailingdetalle
        /// </summary>
        public int? IdCampaniaMailingDetalle { get; set; }
        /// <summary>
        /// Asunto que ira en el correo
        /// </summary>
        public string AsuntoLista { get; set; } = null!;
        /// <summary>
        /// Contenido de correo
        /// </summary>
        public string Contenido { get; set; } = null!;
        /// <summary>
        /// Asunto con el que se enviara el correo
        /// </summary>
        public string Asunto { get; set; } = null!;
        /// <summary>
        /// Id del asesor del que saldra el correo
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Nombre del asesor del que saldra el correo
        /// </summary>
        public string NombreAsesor { get; set; } = null!;
        /// <summary>
        /// Alias del correo del asesor
        /// </summary>
        public string Alias { get; set; } = null!;
        /// <summary>
        /// Etiquetas a reemplazar en el envio
        /// </summary>
        public string Etiquetas { get; set; } = null!;
        /// <summary>
        /// FK de la tabla t_campaniamailchimp
        /// </summary>
        public string? IdCampaniaMailchimp { get; set; }
        /// <summary>
        /// FK de la tabla t_listamailchimp
        /// </summary>
        public string? IdListaMailchimp { get; set; }
        /// <summary>
        /// Flag que indica si se envio el correo
        /// </summary>
        public bool? Enviado { get; set; }
        /// <summary>
        /// Fecha del envio
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
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
        /// Indica la cantidad de veces que se intento subir a mailchimp
        /// </summary>
        public int? NroIntentos { get; set; }
        /// <summary>
        /// Indica si se termino de subir la lista correctamente
        /// </summary>
        public bool? EsSubidoCorrectamente { get; set; }
        /// <summary>
        /// FK de la tabla t_campaniageneraldetalle
        /// </summary>
        public int? IdCampaniaGeneralDetalle { get; set; }
        /// <summary>
        /// Cantidad enviados correctos MailChimp
        /// </summary>
        public int? CantidadEnviadosMailChimp { get; set; }
        /// <summary>
        /// Cantidad de aperturas unicas segun API de MailChimp
        /// </summary>
        public int? CantidadAperturaUnica { get; set; }
        /// <summary>
        /// Cantidad de rebotes suaves segun API de MailChimp
        /// </summary>
        public int? CantidadReboteSuave { get; set; }
        /// <summary>
        /// Cantidad de rebotes duros segun API de MailChimp
        /// </summary>
        public int? CantidadReboteDuro { get; set; }
        /// <summary>
        /// Cantidad de rebotes por sintaxis segun API de MailChimp
        /// </summary>
        public int? CantidadReboteSintaxis { get; set; }
        /// <summary>
        /// Tasa de apertura segun API de MailChimp
        /// </summary>
        public decimal? TasaApertura { get; set; }
        /// <summary>
        /// Cantidad de clics unicos segun API de MailChimp
        /// </summary>
        public int? CantidadClicUnico { get; set; }
        /// <summary>
        /// Cantidad total de clics segun API de MailChimp
        /// </summary>
        public int? CantidadTotalClic { get; set; }
        /// <summary>
        /// Cantidad de reportes por abuso segun API de MailChimp
        /// </summary>
        public int? CantidadReporteAbuso { get; set; }
        /// <summary>
        /// Cantidad de desuscritos segun API de MailChimp
        /// </summary>
        public int? CantidadDesuscritos { get; set; }
        /// <summary>
        /// Tasa de clicks segun API de MailChimp
        /// </summary>
        public decimal? TasaClic { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
    }
}
