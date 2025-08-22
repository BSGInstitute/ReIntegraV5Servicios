namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DetalleFurDTO
    {
        public DateTime FechaSolicitud { get; set; }
        public string CondicionTipoPago { get; set; }
        public string FurTipoPedido { get; set; }
        public int IdFurTipoPedido { get; set; }
        public string NumeroCuenta { get; set; }
        public string CuentaDescripcion { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaLimite { get; set; }
        public int IdMoneda { get; set; }
        public int IdProductoPresentacion { get; set; }
        public decimal PrecioOrigen { get; set; }
        public decimal PrecioDolares { get; set; }
        public decimal Precio { get; set; }
        public int IdMonedaPago { get; set; }


    }

}
