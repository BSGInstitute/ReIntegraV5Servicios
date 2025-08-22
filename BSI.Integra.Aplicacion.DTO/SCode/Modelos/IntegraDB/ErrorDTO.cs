namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ErrorDTO
    {
        public int Codigo { get; set; }
        public int IdErrorTipo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionPersonalizada { get; set; }
        public bool Estado { get; set; }
    }
}
