namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ParametroSeoPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int NumeroCaracteres { get; set; }
    }
   
    public class ParametroSeoAsociadosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Contenido { get; set; }
    }
}
