using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOportunidadPreAsignadum
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id oportunidad log de com.T_OportunidadLog
        /// </summary>
        public int IdOportunidadLog { get; set; }
        /// <summary>
        /// Nombre de la probabilidad de la oportunidad
        /// </summary>
        public string NombreModeloPredictivoProbabilidadEntrante { get; set; } = null!;
        public int IdModeloPredictivoProbabilidad { get; set; }
        /// <summary>
        /// Flag de si la oportunidad esta asignada
        /// </summary>
        public bool Asignado { get; set; }
        /// <summary>
        /// Id de la oportunidad sobre la que se asignó
        /// </summary>
        public int IdOportunidadAsignado { get; set; }
        /// <summary>
        /// flag de si la oportunidad esta en proceso de asignacion automatizada
        /// </summary>
        public bool EnProcesoAsignacionAutomatizada { get; set; }
        /// <summary>
        /// flag de erroneo en caso de que la oportunidad al momento de entrar al proceso presento incogruencias con las configuraciones
        /// </summary>
        public bool Errado { get; set; }
        /// <summary>
        /// Id del error que presento
        /// </summary>
        public int IdOportunidadErrado { get; set; }
        /// <summary>
        /// Flag si la oportunidad ya se pudo configurar con exito
        /// </summary>
        public bool Configurado { get; set; }
        /// <summary>
        /// Id de la configuracion de la oportunidad
        /// </summary>
        public int IdOportunidadConfiguracion { get; set; }
        /// <summary>
        /// estado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
