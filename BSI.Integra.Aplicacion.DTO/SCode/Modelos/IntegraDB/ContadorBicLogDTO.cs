namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ContadorBicLogDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int SinContactoManhana { get; set; }
        public int SinContactoTarde { get; set; }
        public int IdFaseOportunidad { get; set; }
        public DateTime FechaCalculo { get; set; }
    }
}
