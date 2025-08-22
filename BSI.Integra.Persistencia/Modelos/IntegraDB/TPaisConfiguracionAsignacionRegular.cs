using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPaisConfiguracionAsignacionRegular
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Configuraciones a nivel de pais que se considera para un registro de asignacion regular
        /// </summary>
        public int IdAsignacionRegular { get; set; }
        /// <summary>
        /// Id del pais que se va considerar para hacer los reportes
        /// </summary>
        public int IdPaisAsignacionRegular { get; set; }
        /// <summary>
        /// Id de las categoria origen asociadas al proveedor
        /// </summary>
        public bool EsProporcionManual { get; set; }
        /// <summary>
        /// orden en el que los sectores se van a organizar
        /// </summary>
        public int ProporcionManual { get; set; }
        /// <summary>
        /// Se va agrupar La categoria origen de los proveedores
        /// </summary>
        public int ProporcionPorPais { get; set; }
        /// <summary>
        /// estado del sector
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion del sector
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion del sector
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion del sector
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion del sector
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// IdMigracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
