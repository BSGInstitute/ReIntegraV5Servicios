
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteAccesoTemporalAulaVirtualDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
    }
    public class InformacionAccesoPostulanteDTO
    {
        public int? IdAlumno { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool ValidacionRespuesta { get; set; }
    }
    public class RespuestaAccesosPostulanteDTO
    {
        public int? IdAlumno { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
    }
}
