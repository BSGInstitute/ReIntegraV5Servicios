using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TRecordAreaComercial
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del record
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Monto del record
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Es foreign key de T_Moneda
        /// </summary>
        public int IdMonedaRecord { get; set; }
        /// <summary>
        /// Es foreign key de T_TableroComercialUnidad
        /// </summary>
        public int IdTableroComercialUnidad { get; set; }
        /// <summary>
        /// Monto del bono
        /// </summary>
        public decimal Bono { get; set; }
        /// <summary>
        /// Es foreign key de T_Moneda
        /// </summary>
        public int IdMonedaBono { get; set; }
        /// <summary>
        /// Si se visualizara en moneda local
        /// </summary>
        public bool VisualizarMonedaLocal { get; set; }
        /// <summary>
        /// Si es un record vigente
        /// </summary>
        public bool EsVigente { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modifico el registro
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
