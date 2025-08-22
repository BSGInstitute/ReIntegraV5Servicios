namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ParametroSeoDTO
    {
    }
    public class ParametroSeoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ParametroSeoContenidoArticuloDTO
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public int NumeroCaracteres { get; set; }
        public string Descripcion { get; set; }
    }
    public class DatosInsertarParametroSeoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdArticulo { get; set; }
        public int NumeroCaracteres { get; set; }
        public string Descripcion { get; set; }
    }
}
