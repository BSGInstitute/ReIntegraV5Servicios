using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPostulanteConexionInternet
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int IdPostulante { get; set; }
        /// <summary>
        /// Tipo de conexion a internet del postulante
        /// </summary>
        public string TipoConexion { get; set; } = null!;
        /// <summary>
        /// Medio de conexion del postulante
        /// </summary>
        public string MedioConexion { get; set; } = null!;
        /// <summary>
        /// Velocidad de internet del postulante
        /// </summary>
        public string VelocidadInternet { get; set; } = null!;
        /// <summary>
        /// Proveedor de internet del postulante
        /// </summary>
        public string ProveedorInternet { get; set; } = null!;
        /// <summary>
        /// Costo de internet del postulante
        /// </summary>
        public decimal CostoInternet { get; set; }
        /// <summary>
        /// Conexion compartida del postulante
        /// </summary>
        public string ConexionCompartida { get; set; } = null!;
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
    }
}
