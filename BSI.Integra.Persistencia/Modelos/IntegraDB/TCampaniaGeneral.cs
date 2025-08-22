using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaGeneral
    {
        public TCampaniaGeneral()
        {
            TCampaniaGeneralDetalles = new HashSet<TCampaniaGeneralDetalle>();
        }

        /// <summary>
        /// Identificar de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la Campania General
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// FK a la tabla t_categoriaorigen
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Fecha Envio Mailing
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// FK de la tabla t_categoriaobjetofiltro para definir el nivel de segmentacion
        /// </summary>
        public int? IdCategoriaObjetoFiltro { get; set; }
        /// <summary>
        /// FK de la tabla t_hora  para definir la hora de envio mailing
        /// </summary>
        public int? IdHoraEnvioMailing { get; set; }
        /// <summary>
        /// Fk con la tabla T_TipoAsociacion (1,2)
        /// </summary>
        public int? IdTipoAsociacion { get; set; }
        /// <summary>
        /// FK de la tabla t_probabilidadregistro_pw  para definir la probabilidad seleccionada en la configuracion
        /// </summary>
        public int? IdProbabilidadRegistroPw { get; set; }
        /// <summary>
        /// Numero maximo de segmentos en los que puede estar un contacto
        /// </summary>
        public int? NroMaximoSegmentos { get; set; }
        /// <summary>
        /// Numero de periodos sin recibir corrreo de la misma Area/SubArea/Programa
        /// </summary>
        public int? CantidadPeriodoSinCorreo { get; set; }
        /// <summary>
        /// FK de la tabla t_tiempofrecuencia
        /// </summary>
        public int? IdTiempoFrecuencia { get; set; }
        /// <summary>
        /// FK de la tabla t_filtrosegmento
        /// </summary>
        public int? IdFiltroSegmento { get; set; }
        /// <summary>
        /// FK de la tabla t_plantilla
        /// </summary>
        public int? IdPlantillaMailing { get; set; }
        /// <summary>
        /// FK de la tabla t_remitentemailing
        /// </summary>
        public int? IdRemitenteMailing { get; set; }
        /// <summary>
        /// Flag que indica si aplicara para whatsapp
        /// </summary>
        public bool? IncluyeWhatsapp { get; set; }
        /// <summary>
        /// Fecha de inicio del envio de whatsapp
        /// </summary>
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        /// <summary>
        /// Fecha fin del envio de whatsapp
        /// </summary>
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        /// <summary>
        /// Numero de minutos luego de enviar el 1er correo
        /// </summary>
        public int? NumeroMinutosPrimerEnvio { get; set; }
        /// <summary>
        /// FK de la tabla t_hora  para definir la hora de envio whatsapp
        /// </summary>
        public int? IdHoraEnvioWhatsapp { get; set; }
        /// <summary>
        /// Numero de dias sin whatsapp
        /// </summary>
        public int? DiasSinWhatsapp { get; set; }
        /// <summary>
        /// FK a la tabla t_plantilla
        /// </summary>
        public int? IdPlantillaWhatsapp { get; set; }
        /// <summary>
        /// Flag de estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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
        /// Flag para indicar si la campaña incluira los rebotes
        /// </summary>
        public bool? IncluirRebotes { get; set; }
        /// <summary>
        /// FK de la tabla t_hora para mailing
        /// </summary>
        public int IdEstadoEnvioMailing { get; set; }
        /// <summary>
        /// FK de la tabla t_hora para whatsapp
        /// </summary>
        public int IdEstadoEnvioWhatsapp { get; set; }

        public virtual TCategoriaObjetoFiltro? IdCategoriaObjetoFiltroNavigation { get; set; }
        public virtual TCategoriaOrigen? IdCategoriaOrigenNavigation { get; set; }
        public virtual TFiltroSegmento? IdFiltroSegmentoNavigation { get; set; }
        public virtual TProbabilidadRegistroPw? IdProbabilidadRegistroPwNavigation { get; set; }
        public virtual TRemitenteMailing? IdRemitenteMailingNavigation { get; set; }
        public virtual TTiempoFrecuencium? IdTiempoFrecuenciaNavigation { get; set; }
        public virtual TTipoAsociacion? IdTipoAsociacionNavigation { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalles { get; set; }
    }
}
