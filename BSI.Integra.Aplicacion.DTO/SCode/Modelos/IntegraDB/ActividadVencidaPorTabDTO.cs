namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadVencidaPorTabDTO
    {
        public string Dia { get; set; }
        public string Estado { get; set; }
        public string Total { get; set; }

    }
    public class ActividadVencidaporTabPorDiaAgrupadoDTO
    {
        public string Dia { get; set; }
        public List<ActividadVencidaPorTabDTO> Detalle { get; set; }
    }
}
