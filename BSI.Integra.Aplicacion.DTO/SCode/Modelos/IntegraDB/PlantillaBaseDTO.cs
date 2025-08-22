namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaBaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class PlantillaBaseComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
    public class SpeechBienvenidaDespedidaDTO
    {
        public int IdPlantillaBienvenida { get; set; }
        public int IdPlantillaDespedida { get; set; }
    }
    public class PlantillaWhatsAppDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int TipoPlantilla { get; set; }
    }
}
