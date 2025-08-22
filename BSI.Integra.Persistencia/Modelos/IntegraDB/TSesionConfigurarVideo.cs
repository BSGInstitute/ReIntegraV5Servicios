using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSesionConfigurarVideo
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_ConfigurarVideoPrograma
        /// </summary>
        public int IdConfigurarVideoPrograma { get; set; }
        /// <summary>
        /// Minuto en el que se platea asignar un examen o vista de documento
        /// </summary>
        public int Minuto { get; set; }
        /// <summary>
        /// Iden tificador del tipo de la vista en el video de la tabla  pla.T_TipoVista
        /// </summary>
        public int IdTipoVista { get; set; }
        /// <summary>
        /// Numero de la diapositiva o de documento a visualizar
        /// </summary>
        public int? NroDiapositiva { get; set; }
        /// <summary>
        /// Identificador de la evaluacion configurada
        /// </summary>
        public int? IdEvaluacion { get; set; }
        /// <summary>
        /// Creado o eliminado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico  Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }
        /// <summary>
        /// Visualizar el logo configurado en el video
        /// </summary>
        public bool? ConLogoVideo { get; set; }
        /// <summary>
        /// Visualizar el logo configurado en la diapositiva
        /// </summary>
        public bool? ConLogoDiapositiva { get; set; }

        public virtual TConfigurarVideoPrograma IdConfigurarVideoProgramaNavigation { get; set; } = null!;
        public virtual TTipoVistum IdTipoVistaNavigation { get; set; } = null!;
    }
}
