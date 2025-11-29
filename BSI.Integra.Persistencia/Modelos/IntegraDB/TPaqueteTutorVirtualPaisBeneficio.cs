using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPaqueteTutorVirtualPaisBeneficio
    {
        /// <summary>
        /// Identificador autoincremental del beneficio
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del detalle país-paquete al que pertenece el beneficio
        /// </summary>
        public int IdPaqueteTutorVirtualPais { get; set; }
        /// <summary>
        /// Nombre o descripción del beneficio incluido
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado lógico del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro por última vez
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
        /// Control de concurrencia (timestamp)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPaqueteTutorVirtualPai IdPaqueteTutorVirtualPaisNavigation { get; set; } = null!;
    }
}
