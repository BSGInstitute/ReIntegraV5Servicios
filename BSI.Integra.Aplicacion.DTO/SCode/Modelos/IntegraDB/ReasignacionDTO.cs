namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    internal class ReasignacionDTO
    {
    }
    public class PersonalMinReasignacionDTO
    {
        public int IdAsesor { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public int IdJefe { get; set; }
        public string NombreCompletoJefe { get; set; }
        public string EmailJefe { get; set; }
    }
    public class FaseOportunidadReasignacionDTO
    {
        public string Codigo { get; set; }
    }
    public class AlumnoReasignacionDTO
    {
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
}
