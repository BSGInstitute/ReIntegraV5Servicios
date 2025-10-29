using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena la motivacion por la que el alumno toma el programa
    /// </summary>
    public partial class TProgramaMotivacion
    {
        public TProgramaMotivacion()
        {
            TOportunidadProgramaMotivacionSeleccions = new HashSet<TOportunidadProgramaMotivacionSeleccion>();
        }

        /// <summary>
        /// Identificador único de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción del programa de motivación
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Estado lógico (1: activo, 0: inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó la creación del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo para control de concurrencia (timestamp)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TOportunidadProgramaMotivacionSeleccion> TOportunidadProgramaMotivacionSeleccions { get; set; }
    }
}
