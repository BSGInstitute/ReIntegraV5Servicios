
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteFormacionLogDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string Pais { get; set; }
        public string CentroEstudio { get; set; }
        public string OtraInstitucion { get; set; }
        public string TipoEstudio { get; set; }
        public string EstadoEstudio { get; set; }
        public string AreaFormacion { get; set; }
        public string OtraCarrera { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string AlaActualidad { get; set; }
        public string TurnoEstudio { get; set; }
        public string TipoActualizacion { get; set; }
        public string FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
