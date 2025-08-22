namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ContadorBicDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
    }
    public class OportunidadAgrupadaDTO
    {
        public int IdOportunidad { get; set; }
        public List<DateTime> ListaFechas { get; set; }
        public int? IdPais { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdFaseOportunidad { get; set; }
    }
    public class OportunidadContadorBicDTO
    {
        public int IdOportunidad { get; set; }
        public int CantidadDias { get; set; }

    }
}
