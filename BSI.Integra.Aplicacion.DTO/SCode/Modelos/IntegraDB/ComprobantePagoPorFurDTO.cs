namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComprobantePorFurDTO
    {
        public int Id { get; set; }
        public string Comprobante { get; set; }
        public string Proveedor { get; set; }
        public int IdAsociacion { get; set; }
        public string NombreAsociacion { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoAsociado { get; set; }
        public decimal MontoAmortizar { get; set; }
    }

    public class AsociarComprobateDTO
    {
        public int IdComprobantePago { get; set; }
        public decimal Monto { get; set; }
        public string Usuario { get; set; }
        public int IdFur { get; set; }

    }
}
