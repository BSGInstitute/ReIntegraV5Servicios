using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfigurarEvaluacionTrabajo
    {
        public TConfigurarEvaluacionTrabajo()
        {
            TPreguntaEvaluacionTrabajos = new HashSet<TPreguntaEvaluacionTrabajo>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_TipoEvaluacionTrabajo
        /// </summary>
        public int IdTipoEvaluacionTrabajo { get; set; }
        /// <summary>
        /// Nombre del registro de evaluacion a configurar para el capitulo del programa
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del registro de evaluacion
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Identificador de la tabla pla.T_DocumentoPw
        /// </summary>
        public int? IdDocumentoPw { get; set; }
        /// <summary>
        /// Nombre del Archivo que se asignara al registro de evaluacion
        /// </summary>
        public string? ArchivoNombre { get; set; }
        /// <summary>
        /// Carpeta donde se alojara el archivo a registrar
        /// </summary>
        public string? ArchivoCarpeta { get; set; }
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
        /// Identificador de migracion
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Identificador de pla.T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Identificador de pla.T_Seccion_PW
        /// </summary>
        public int? IdSeccion { get; set; }
        /// <summary>
        /// Numero de fila en la cual se encuentra el registro
        /// </summary>
        public int? Fila { get; set; }
        /// <summary>
        /// Descripcion para resolver las preguntas por parte del usuario
        /// </summary>
        public string? DescripcionPregunta { get; set; }
        /// <summary>
        /// Registro del numero de orden del capitulo asociado segun la estructura curricular
        /// </summary>
        public int? OrdenCapitulo { get; set; }
        /// <summary>
        /// Estado para validar si se habiliata las instrucciones
        /// </summary>
        public bool? HabilitarInstrucciones { get; set; }
        /// <summary>
        /// Estado para validar si se habiliata subir archivo
        /// </summary>
        public bool? HabilitarArchivo { get; set; }
        /// <summary>
        /// Estado para validar si se habiliata visualizar preguntas 
        /// </summary>
        public bool? HabilitarPreguntas { get; set; }
        /// <summary>
        /// Registrop del numero de orden de de la evaluacion configurada
        /// </summary>
        public int? OrdenEvaluacion { get; set; }

        public virtual TDocumentoPw? IdDocumentoPwNavigation { get; set; }
        public virtual TPgeneral? IdPgeneralNavigation { get; set; }
        public virtual TTipoEvaluacionTrabajo IdTipoEvaluacionTrabajoNavigation { get; set; } = null!;
        public virtual ICollection<TPreguntaEvaluacionTrabajo> TPreguntaEvaluacionTrabajos { get; set; }
    }
}
