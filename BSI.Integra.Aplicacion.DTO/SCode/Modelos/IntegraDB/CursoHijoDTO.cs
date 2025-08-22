namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CursoHijoDTO
    {
    }
    public class CursoHijoDuracionDTO
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
    }
    public class CursoHijoIdDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Duracion { get; set; }
        public int? IdPEspecifico { get; set; }
    }
}
