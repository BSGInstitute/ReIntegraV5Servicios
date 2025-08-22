using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracionPreEnvio
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla mkt.T_WhatsappMensajePublicidad
        /// </summary>
        public int IdWhatsappMensajePublicidad { get; set; }
        /// <summary>
        /// Identificador de la tabla mkt.T_ConjuntoListaResultado
        /// </summary>
        public int IdConjuntoListaResultado { get; set; }
        /// <summary>
        /// Identificador de la tabla mkt.T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Numero de celular
        /// </summary>
        public string Celular { get; set; } = null!;
        /// <summary>
        /// Identificador de la tabla conf.T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Numero de ejecucion del registro
        /// </summary>
        public int NroEjecucion { get; set; }
        /// <summary>
        /// Si el numero es valido o no para envio de mensaje
        /// </summary>
        public bool Validado { get; set; }
        /// <summary>
        /// Mensaje propio de la Plantilla
        /// </summary>
        public string? Plantilla { get; set; }
        /// <summary>
        /// Identificador de la tabla gp.T_personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_Plantilla
        /// </summary>
        public int? IdPlantilla { get; set; }
        /// <summary>
        /// Identificador de la tabla mkt.T_WhatsAppEstadoValidacion
        /// </summary>
        public int IdWhatsAppEstadoValidacion { get; set; }
        /// <summary>
        /// Objeto en json de los parametros de la plantilla
        /// </summary>
        public string? ObjetoPlantilla { get; set; }
        /// <summary>
        /// Indica si el mensaje fue realizado y procesado con whatsapp
        /// </summary>
        public bool Procesado { get; set; }
        /// <summary>
        /// Mensaje del proceso por whatsapp
        /// </summary>
        public string MensajeProceso { get; set; } = null!;
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
        /// Identificador de la tabla mkt.T_ConjuntoListaDetalle
        /// </summary>
        public int IdConjuntoListaDetalle { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }
}
