using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registra las reglas de transicion permitidas entre fases de oportunidad dentro de los procesos comerciales.
    /// </summary>
    public partial class TTransicionCalificacionFase
    {
        public TTransicionCalificacionFase()
        {
            TCriterioCalificacionFaseOportunidads = new HashSet<TCriterioCalificacionFaseOportunidad>();
        }

        /// <summary>
        /// Clave primaria de la transicion.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fase de oportunidad (origen).
        /// </summary>
        public int IdFaseOportunidadOrigen { get; set; }
        /// <summary>
        /// Fase de oportunidad (destino).
        /// </summary>
        public int IdFaseOportunidadDestino { get; set; }
        /// <summary>
        /// Campo de auditoria Estado (eliminacion logica) del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoria Usuario Creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria Usuario Modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria Fecha Creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo de auditoria Fecha Modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoria RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria IdMigracion del registro
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TFaseOportunidad IdFaseOportunidadDestinoNavigation { get; set; } = null!;
        public virtual TFaseOportunidad IdFaseOportunidadOrigenNavigation { get; set; } = null!;
        public virtual ICollection<TCriterioCalificacionFaseOportunidad> TCriterioCalificacionFaseOportunidads { get; set; }
    }
}
