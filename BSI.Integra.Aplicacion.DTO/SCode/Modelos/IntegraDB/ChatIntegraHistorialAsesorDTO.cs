namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatHistorialAsesorDTO
    {
        public int IdInteraccionChat { get; set; }
        public int? IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public int? IdAsesor { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Tiempo { get; set; }
        public string Ubicacion { get; set; }
        public string Mensajes { get; set; }
        public Guid Chatsession { get; set; }
        public bool Leido { get; set; }
    }
}
