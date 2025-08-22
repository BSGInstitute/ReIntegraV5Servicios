using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCongelamientoProyeccionFur
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de fin.T_CabeceraFurConfiguracionAutomatica
        /// </summary>
        public int IdCabeceraFurConfiguracionAutomatica { get; set; }
        /// <summary>
        /// FK de Area
        /// </summary>
        public int IdArea { get; set; }
        /// <summary>
        /// Configuración de Congelamiento
        /// </summary>
        public string Configuracion { get; set; } = null!;
        /// <summary>
        /// detalle de configuración de Congelamiento
        /// </summary>
        public string DetalleFurConfiguracionAutomatica { get; set; } = null!;
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
