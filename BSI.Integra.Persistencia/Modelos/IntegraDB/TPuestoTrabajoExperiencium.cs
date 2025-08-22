using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajoExperiencium
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave primaria de gp.T_PerfilPuestoTrabajo
        /// </summary>
        public int IdPerfilPuestoTrabajo { get; set; }
        /// <summary>
        /// FK T_Experiencia
        /// </summary>
        public int IdExperiencia { get; set; }
        /// <summary>
        /// FK T_TipoExperiencia
        /// </summary>
        public int IdTipoExperiencia { get; set; }
        /// <summary>
        /// Numero minimo de periodo
        /// </summary>
        public int NumeroMinimo { get; set; }
        /// <summary>
        /// dia, mes , año
        /// </summary>
        public string Periodo { get; set; } = null!;
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

        public virtual TExperiencium IdExperienciaNavigation { get; set; } = null!;
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TTipoExperiencium IdTipoExperienciaNavigation { get; set; } = null!;
    }
}
