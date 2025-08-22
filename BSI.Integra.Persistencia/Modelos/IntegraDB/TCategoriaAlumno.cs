using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCategoriaAlumno
    {
        /// <summary>
        /// PK de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la categoria
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion de la categoria
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Se configurará si la categoría estará activa o inactiva
        /// </summary>
        public bool EstadoCategoria { get; set; }
        /// <summary>
        /// número del porcentaje de descuento que le corresponde a un alumno
        /// </summary>
        public int Descuento { get; set; }
        /// <summary>
        /// número de dias que se ampliacion de la categoria
        /// </summary>
        public int AmpliacionFechaFinPrograma { get; set; }
        /// <summary>
        /// se ingresa la cantidad mínima de días en los que el alumno paga su cuota
        /// </summary>
        public int CantidadDiasVencimiento { get; set; }
        /// <summary>
        /// Estado del registro
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
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
