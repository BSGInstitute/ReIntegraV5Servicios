using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionPeriodoMatricula
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la agrupación
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha inicio de la agrupación
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha fin de la agrupación
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Estado de eliminado o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
