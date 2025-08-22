namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DataCreditoInformacionDTO
    {
        public DateTime FechaUltimaActualizacion { get; set; }
        public string DNI { get; set; }
        public string NombreAlterno { get; set; }

    }
    public class DataCreditoTarjetaCreditoDTO
    {
        public string CnsEntNomRazLn { get; set; }
        public decimal LineaCredito { get; set; }
        public decimal LineaCreditoNoUtil { get; set; }
        public decimal LineaUtil { get; set; }

    }
    public class DataCreditoCreditoVigenteDTO
    {
        public string NombreRazonSocial { get; set; }
        public decimal MontoDeuda { get; set; }
        public int DiasVencidos { get; set; }

    }
    public class DataCreditoRespuestaDTO
    {
        public DataCreditoInformacionDTO Informacion { get; set; }
        public List<DataCreditoTarjetaCreditoDTO> Tarjeta { get; set; }
        public List<DataCreditoCreditoVigenteDTO> Credito { get; set; }

    }
}
