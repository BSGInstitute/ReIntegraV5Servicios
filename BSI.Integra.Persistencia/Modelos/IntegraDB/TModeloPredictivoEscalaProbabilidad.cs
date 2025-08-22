using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModeloPredictivoEscalaProbabilidad
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tPLA_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Numero de orden
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Nombre e la escala medida
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Valor calculada definida inicial
        /// </summary>
        public decimal ProbabilidaIinicial { get; set; }
        /// <summary>
        /// Valor calculada definida actual
        /// </summary>
        public decimal ProbabilidadActual { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
    }
}
