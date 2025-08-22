
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalAccesoTemporalAulaVirtualDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EvaluacionHabilitada { get; set; }
    }
    public class MaestroPersonalGrupoAccesoTemporalPersonalDTO
    {
        public int IdPersonal { get; set; }
        public string NombreProgramaPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public List<int> IdPEspecificoHijo { get; set; }
        public double Avance { get; set; }
        public double Nota { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public int CantidadPreguntaConfigurada { get; set; }
        public int CantidadCrucigramaConfigurado { get; set; }
        public int CantidadPreguntaResuelta { get; set; }
        public int CantidadCrucigramaResuelta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class DatosBasicosPortalContactoDTO
    {
        public string IdUsuarioPortalWeb { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int IdContacto { get; set; }
        public int IdAlumno { get; set; }
    }
}
