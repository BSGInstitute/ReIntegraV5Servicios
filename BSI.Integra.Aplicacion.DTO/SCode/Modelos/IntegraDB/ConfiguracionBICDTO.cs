namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionBICDTO
    {
        public int Id { get; set; }
        public int Dias { get; set; }
        public int Llamadas { get; set; }
        public bool Aplica { get; set; }
    }
    public class PersonalBicConfiguracionDTO
    {
        public int DiasBic { get; set; }
        public string IdAsesores { get; set; }
        public int? IdPais { get; set; }
    }
}
