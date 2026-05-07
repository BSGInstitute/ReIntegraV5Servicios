using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena la configuracion de cuenta de empleador en Computrabajo con rating general, total de evaluaciones y URL del perfil. La captura es manual y periodica (quincenal) debido a que Computrabajo no dispone de API publica
    /// </summary>
    public partial class TComputrabajoConfiguracion
    {
        public TComputrabajoConfiguracion()
        {
            TComputrabajoResenas = new HashSet<TComputrabajoResena>();
        }

        /// <summary>
        /// Identificador unico de la configuracion de Computrabajo (PK)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la empresa tal como se muestra en el perfil de Computrabajo
        /// </summary>
        public string NombreEmpresa { get; set; } = null!;
        /// <summary>
        /// Rating general de la empresa en escala 0.00 a 5.00 estrellas
        /// </summary>
        public decimal Valoracion { get; set; }
        /// <summary>
        /// Total de evaluaciones acumuladas en el perfil de empleador de Computrabajo. Se congela en cada sincronización de reseñas.
        /// </summary>
        public int ResenaTotal { get; set; }
        /// <summary>
        /// URL publica del perfil de empleador en Computrabajo
        /// </summary>
        public string? UrlPerfil { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima actualizacion manual de los datos desde Computrabajo
        /// </summary>
        public DateTime? FechaSincronizacion { get; set; }
        /// <summary>
        /// Estado logico del registro (1=Activo, 0=Eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico por ultima vez el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la ultima modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de control de versiones (timestamp automatico)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TComputrabajoResena> TComputrabajoResenas { get; set; }
    }
}
