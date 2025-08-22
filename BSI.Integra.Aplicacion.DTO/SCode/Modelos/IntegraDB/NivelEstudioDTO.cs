namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class NivelEstudioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoFormacion { get; set; }
        public string TipoFormacion { get; set; }
    }
    public class NivelEstudioComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipoFormacion { get; set; }
    }
    
}
