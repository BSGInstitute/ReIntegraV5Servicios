namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CodigoMatriculaV2DTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdAlumno { get; set; }
    }
}
