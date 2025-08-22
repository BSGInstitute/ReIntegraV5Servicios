namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class IvrPlantillaDTO
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public bool MenuOpcion { get; set; }
        public string TextoMenu { get; set; }
        public bool Activo { get; set; }
    }
}
