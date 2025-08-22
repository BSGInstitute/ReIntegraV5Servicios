using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAgendaTab
    {
        public TAgendaTab()
        {
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperacione>();
        }

        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de los tabs de la Agenda
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Validacion para Visualizar o no los Tabs de la Agenda
        /// </summary>
        public bool VisualizarActividad { get; set; }
        /// <summary>
        /// Validadcion para cargar o no informacion de las actvidades en los Tabs de la Agenda
        /// </summary>
        public bool CargarInformacionInicial { get; set; }
        /// <summary>
        /// Indica el orden de priorizacion de los Tab
        /// </summary>
        public int Numeracion { get; set; }
        /// <summary>
        /// Indica si requiere la validacion de la fecha de programacion
        /// </summary>
        public bool ValidarFecha { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Campo CodigoAreaTrabajo Que define a que área de trabajo pertene el tab
        /// </summary>
        public string? CodigoAreaTrabajo { get; set; }
        /// <summary>
        /// Ponderacion del tab para el marcador predictivo
        /// </summary>
        public int? Ponderacion { get; set; }
        /// <summary>
        /// Indica si el tab aplica o no para marcador predictivo
        /// </summary>
        public bool? AplicaMarcadorPredictivo { get; set; }

        public virtual ICollection<TOportunidadClasificacionOperacione> TOportunidadClasificacionOperaciones { get; set; }
    }
}
