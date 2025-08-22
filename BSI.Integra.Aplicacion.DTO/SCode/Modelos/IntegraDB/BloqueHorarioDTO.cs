namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BloqueHorarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string NombreUsuario { get; set; }
    }
    public class BloqueHorarioProcesarBicDTO
    {
        public string Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int? Contador { get; set; }
    }
}
