namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DataCreditoErrorDTO : Exception
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
    }
}
