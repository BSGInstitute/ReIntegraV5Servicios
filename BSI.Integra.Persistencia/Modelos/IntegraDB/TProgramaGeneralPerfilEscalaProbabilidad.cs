using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProgramaGeneralPerfilEscalaProbabilidad
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tPGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Descripcion de la probabilidad
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Probalididad en primera instancia
        /// </summary>
        public double ProbabilidadInicial { get; set; }
        /// <summary>
        /// Probabilidad actual
        /// </summary>
        public double ProbabilidadActual { get; set; }
        /// <summary>
        /// Id estatico descripcion probabilidad
        /// </summary>
        public int Orden { get; set; }
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
        public Guid? IdMigracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
    }
}
