using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAccionFormulario
    {
        public TAccionFormulario()
        {
            TAccionFormularioPorCampoContactos = new HashSet<TAccionFormularioPorCampoContacto>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Ultima llamada Ejecutada
        /// </summary>
        public int UltimaLlamadaEjecutada { get; set; }
        /// <summary>
        /// Indica Campo sin valores
        /// </summary>
        public bool CamposSinValores { get; set; }
        /// <summary>
        /// Tiempo Redireccionamiento
        /// </summary>
        public int TiempoRedirecionamiento { get; set; }
        /// <summary>
        /// Campos Segun Probabilidad
        /// </summary>
        public bool CamposSegunProbabilidad { get; set; }
        /// <summary>
        /// Todos Campos
        /// </summary>
        public bool TodosCampos { get; set; }
        /// <summary>
        /// Numero Clics
        /// </summary>
        public int? NumeroClics { get; set; }
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

        public virtual ICollection<TAccionFormularioPorCampoContacto> TAccionFormularioPorCampoContactos { get; set; }
    }
}
