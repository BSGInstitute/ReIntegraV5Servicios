using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCrucigramaProgramaCapacitacionDetalle
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_CrucigramaProgramaCapacitacion
        /// </summary>
        public int IdCrucigramaProgramaCapacitacionDetalle { get; set; }
        /// <summary>
        /// Nro de orden de la palabra
        /// </summary>
        public int NumeroPalabra { get; set; }
        /// <summary>
        /// Palabra del crucigrama
        /// </summary>
        public string Palabra { get; set; } = null!;
        /// <summary>
        /// Definicion de la palabra
        /// </summary>
        public string Definicion { get; set; } = null!;
        /// <summary>
        /// Determina la orientacion: Vertica u Horizontal
        /// </summary>
        public int Tipo { get; set; }
        /// <summary>
        /// Establece la columna de inicio de la palabra
        /// </summary>
        public int ColumnaInicio { get; set; }
        /// <summary>
        /// Establece la fila de inicio de la palabra
        /// </summary>
        public int FilaInicio { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
