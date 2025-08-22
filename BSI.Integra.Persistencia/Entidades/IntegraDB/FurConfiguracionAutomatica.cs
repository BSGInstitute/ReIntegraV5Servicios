using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FurConfiguracionAutomatica : BaseIntegraEntity
    {
        public int IdFurTipoSolicitud { get; set; }
        public int IdSede { get; set; }
        public int IdFurTipoPedido { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public decimal Cantidad { get; set; }
        public int IdMonedaPagoReal { get; set; }
        public int AjusteNumeroSemana { get; set; }
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdFrecuencia { get; set; }
        public int IdCentroCosto { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime FechaGeneracionFur { get; set; }
        public DateTime FechaInicioConfiguracion { get; set; }
        public DateTime FechaFinConfiguracion { get; set; }
        public int? IdEmpresa { get; set; }
        public bool? Activo { get; set; }
    }
}
