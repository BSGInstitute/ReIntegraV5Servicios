using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajoFormacionAcademica
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
        /// FK T_TipoFormacion - Multiselect
        /// </summary>
        public string IdTipoFormacion { get; set; } = null!;
        /// <summary>
        /// FK T_NivelEstudio - Multiselect
        /// </summary>
        public string IdNivelEstudio { get; set; } = null!;
        /// <summary>
        /// FK T_AreaFormacion - Multiselect
        /// </summary>
        public string IdAreaFormacion { get; set; } = null!;
        /// <summary>
        /// FK T_CentroEstudio - Multiselect
        /// </summary>
        public string IdCentroEstudio { get; set; } = null!;
        /// <summary>
        /// FK T_GradoEstudio - Multiselect
        /// </summary>
        public string IdGradoEstudio { get; set; } = null!;
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

        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; } = null!;
    }
}
