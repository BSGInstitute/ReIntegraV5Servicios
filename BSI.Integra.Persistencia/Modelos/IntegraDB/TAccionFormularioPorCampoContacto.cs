using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAccionFormularioPorCampoContacto
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_AccionFormulario
        /// </summary>
        public int? IdAccionFormulario { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CampoContacto
        /// </summary>
        public int IdCampoContacto { get; set; }
        /// <summary>
        /// Orden
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Nombre del Campo
        /// </summary>
        public string Campo { get; set; } = null!;
        /// <summary>
        /// Indica que el Campo es siempre visible
        /// </summary>
        public bool EsSiempreVisible { get; set; }
        /// <summary>
        /// Indica que el Campo es inteligente
        /// </summary>
        public bool EsInteligente { get; set; }
        /// <summary>
        /// Indica que el Campo tiene Probabilidad
        /// </summary>
        public bool TieneProbabilidad { get; set; }
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
        public Guid IdMigracion { get; set; }

        public virtual TAccionFormulario? IdAccionFormularioNavigation { get; set; }
        public virtual TCampoContacto IdCampoContactoNavigation { get; set; } = null!;
    }
}
