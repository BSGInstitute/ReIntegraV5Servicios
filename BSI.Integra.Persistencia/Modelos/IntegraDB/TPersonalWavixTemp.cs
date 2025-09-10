using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los id de sip trunk creado para cada usuario
    /// </summary>
    public partial class TPersonalWavixTemp
    {
        public TPersonalWavixTemp()
        {
            TConfiguracionPersonalWavixTemps = new HashSet<TConfiguracionPersonalWavixTemp>();
        }

        /// <summary>
        /// Identificador único de la tabla (autoincremental)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea que referencia al personal asignado
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Código o identificador del SIP Trunk asociado al personal
        /// </summary>
        public string IdSipTrunk { get; set; } = null!;
        /// <summary>
        /// URL o dirección del servidor SIP asociado
        /// </summary>
        public string UrlServer { get; set; } = null!;
        /// <summary>
        /// Estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo utilizado para control de concurrencia (versión de fila)
        /// </summary>
        public byte[]? RowVersion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
        public virtual ICollection<TConfiguracionPersonalWavixTemp> TConfiguracionPersonalWavixTemps { get; set; }
    }
}
