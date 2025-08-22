using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFurConfiguracionAutomatica
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_FurTipoSolicitud
        /// </summary>
        public int IdFurTipoSolicitud { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Sede
        /// </summary>
        public int IdSede { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_FurTipoPedido
        /// </summary>
        public int IdFurTipoPedido { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_PersonalAreaTrabajo
        /// </summary>
        public int IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Indica la cantidad
        /// </summary>
        public decimal Cantidad { get; set; }
        public int IdMonedaPagoReal { get; set; }
        public int AjusteNumeroSemana { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_HistoridoProductoProveedor
        /// </summary>
        public int IdHistoricoProductoProveedor { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Frecuencia
        /// </summary>
        public int IdFrecuencia { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_CentroCosto
        /// </summary>
        public int IdCentroCosto { get; set; }
        /// <summary>
        /// Descripcion del elemento
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Indica la fecha en el cual se realizara el pago
        /// </summary>
        public DateTime FechaGeneracionFur { get; set; }
        /// <summary>
        /// Indica la fecha de inicio para la generacion de fur 
        /// </summary>
        public DateTime FechaInicioConfiguracion { get; set; }
        /// <summary>
        /// Indica la fecha de fin para la generacion de fur 
        /// </summary>
        public DateTime FechaFinConfiguracion { get; set; }
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
        /// <summary>
        /// Id de la empresa viene de la tabla fin.t_EmpresaAutorizada
        /// </summary>
        public int? IdEmpresa { get; set; }
        /// <summary>
        /// Activo para la proyeccion
        /// </summary>
        public bool? Activo { get; set; }
    }
}
