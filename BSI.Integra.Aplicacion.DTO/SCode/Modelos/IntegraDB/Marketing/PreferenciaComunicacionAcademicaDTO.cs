
namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    public class PreferenciaComunicacionAcademicaDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdMedioComunicacion { get; set; }
    }

    public class VPreferenciaComunicacionAcademicaMedioComunicacionDTO
    {
        public int IdPreferenciaComunicacionAcademica { get; set; }
        public int IdAlumno { get; set; }
        public int IdMedioComunicacion { get; set; }
        public string Nombre { get; set; }
    }
}
