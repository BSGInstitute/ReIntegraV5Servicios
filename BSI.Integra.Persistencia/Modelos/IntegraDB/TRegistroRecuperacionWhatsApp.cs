using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRegistroRecuperacionWhatsApp
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_CampaniaGeneral
        /// </summary>
        public int IdCampaniaGeneral { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_CampaniaGeneralDetalle
        /// </summary>
        public int IdCampaniaGeneralDetalle { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_CampaniaGeneralDetalleResponsable
        /// </summary>
        public int IdCampaniaGeneralDetalleResponsable { get; set; }
        /// <summary>
        /// Llave foranea de la tabla gp.T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_WhatsAppConfiguracionEnvio
        /// </summary>
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        /// <summary>
        /// Dia actual
        /// </summary>
        public int Dia { get; set; }
        /// <summary>
        /// Cantidad del dia 1
        /// </summary>
        public int Dia1 { get; set; }
        /// <summary>
        /// Cantidad del dia 2
        /// </summary>
        public int Dia2 { get; set; }
        /// <summary>
        /// Cantidad del dia 3
        /// </summary>
        public int Dia3 { get; set; }
        /// <summary>
        /// Cantidad del dia 4
        /// </summary>
        public int Dia4 { get; set; }
        /// <summary>
        /// Cantidad del dia 5
        /// </summary>
        public int Dia5 { get; set; }
        /// <summary>
        /// Fecha de inicio de envio de WhatsApp
        /// </summary>
        public DateTime FechaInicioEnvioWhatsapp { get; set; }
        /// <summary>
        /// Fecha de fin de envio de WhatsApp
        /// </summary>
        public DateTime FechaFinEnvioWhatsapp { get; set; }
        /// <summary>
        /// Hora de envio de WhatsApp
        /// </summary>
        public TimeSpan HoraEnvio { get; set; }
        /// <summary>
        /// Flag para determinar si se concreto exitosamente el envio
        /// </summary>
        public bool Completado { get; set; }
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
        /// Flag para detectar si un envio ha fallado
        /// </summary>
        public bool? Fallido { get; set; }
        /// <summary>
        /// Flag para detectar si se encuentra en recuperacion un envio
        /// </summary>
        public bool? RecuperacionEnProceso { get; set; }
    }
}
