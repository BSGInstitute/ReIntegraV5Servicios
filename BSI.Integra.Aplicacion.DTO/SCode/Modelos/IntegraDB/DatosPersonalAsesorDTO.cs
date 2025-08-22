namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DatosPersonalAsesorDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Gmail { get; set; }
        public string NombreCompleto { get; set; }
        public bool asignado { get; set; }
        public int? IdAsesor { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public bool Estado { get; set; }
    }



}
