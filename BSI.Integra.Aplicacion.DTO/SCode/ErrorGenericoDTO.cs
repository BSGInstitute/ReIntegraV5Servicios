namespace BSI.Integra.Aplicacion.DTO
{
    public class ErrorGenericoDTO
    {
        public string typeRequest { get; set; }
        public string title { get; set; }
        public string error { get; set; }
        public string stackTrace { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
    public class DetailErrorDTO
    {
        public string codigo { get; set; }
        public string Message { get; set; }
        public string descripcion { get; set; }
    }
}
