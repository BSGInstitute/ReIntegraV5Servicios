using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCrucigramaProgramaCapacitacion
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_PGeneral
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// FK T_PEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Orden fila en la que se encuentra el capitulo
        /// </summary>
        public int? OrdenFilaCapitulo { get; set; }
        /// <summary>
        /// Orden de fila en la que se encuentra la sesion
        /// </summary>
        public int? OrdenFilaSesion { get; set; }
        /// <summary>
        /// Codigo que se establece para el crucigrama
        /// </summary>
        public string CodigoCrucigrama { get; set; } = null!;
        /// <summary>
        /// Fk T_TipoMarcador
        /// </summary>
        public int IdTipoMarcador { get; set; }
        /// <summary>
        /// Valor del marcador seleccionado
        /// </summary>
        public decimal ValorMarcador { get; set; }
        /// <summary>
        /// Define el nro de filas del crucigrama
        /// </summary>
        public int CantidadFila { get; set; }
        /// <summary>
        /// define el nro de columnas del crucigrama
        /// </summary>
        public int CantidadColumna { get; set; }
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
