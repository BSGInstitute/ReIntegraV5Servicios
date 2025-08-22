namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppPlantillaPorOcurrenciaActividadDTO
    {
        public int Id { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public int IdPlantilla { get; set; }
        public int NumeroDiasSinContacto { get; set; }
    }
    public class WhatsAppPlantillaPorOcurrenciaActividadComboDTO
    {
        public int Id { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public string Plantilla { get; set; } = null!;
    }
    public class DatoPlantillaWhatsAppDTO
    {
        public string Codigo { get; set; } = "";
        public string Texto { get; set; } = "";
    }
    public class WhatsAppResultadoConjuntoListaDTO
    {
        public int IdPre { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public List<DatoPlantillaWhatsAppDTO> objetoplantilla { get; set; }
    }
}
