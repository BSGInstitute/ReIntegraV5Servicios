namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ContadorBicLogDetalleDTO
    {
        public int Id { get; set; }
        public int IdContadorBicLog { get; set; }
        public bool EstadoContactoManhana { get; set; }
        public bool EstadoContactoTarde { get; set; }
        public DateTime FechaLogContacto { get; set; }
    }
    public class ContadorBicLogReporteDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int SinContactoManhana { get; set; }
        public int SinContactoTarde { get; set; }
        public DateTime FechaCalculo { get; set; }
        public int IdPersonal { get; set; }
        public string DatosPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public bool EstadoContactoManhana { get; set; }
        public bool EstadoContactoTarde { get; set; }
        public DateTime FechaLogContacto { get; set; }
    }
}
