using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPuestoTrabajoFuncion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_PerfilPuestoTrabajo
        /// </summary>
        public int IdPerfilPuestoTrabajo { get; set; }
        /// <summary>
        /// Numero de Orden de la funcion
        /// </summary>
        public int NroOrden { get; set; }
        /// <summary>
        /// Descripcion de la funcion
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fk T_PersonalTipoFuncion
        /// </summary>
        public int IdPersonalTipoFuncion { get; set; }
        /// <summary>
        /// Fk T_FrecuenciaPuestoTrabajo
        /// </summary>
        public int IdFrecuenciaPuestoTrabajo { get; set; }
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

        public virtual TFrecuenciaPuestoTrabajo IdFrecuenciaPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TPersonalTipoFuncion IdPersonalTipoFuncionNavigation { get; set; } = null!;
    }
}
