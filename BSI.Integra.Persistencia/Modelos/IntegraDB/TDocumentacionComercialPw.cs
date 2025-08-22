using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDocumentacionComercialPw
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// título de la documentación
        /// </summary>
        public string Titulo { get; set; } = null!;
        /// <summary>
        /// contenido de la documentación
        /// </summary>
        public string Contenido { get; set; } = null!;
        /// <summary>
        /// tipo  de la documentacion
        /// </summary>
        public string Tipo { get; set; } = null!;
        /// <summary>
        /// modalidad de la documentacion
        /// </summary>
        public string Modalidad { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key hacia la tabla TCRM_Pais para el campo CodigoPais
        /// </summary>
        public int? IdPais { get; set; }
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
    }
}
