namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteIngresoCongelamientoDTO
    {
        public int Id { get; set; }
        public string NombreFiltro { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string DetalleCongelado { get; set; }

    }
    public class ReporteIngresoCongelamientoRecibidoDTO
    {
        public int? Id { get; set; } = 0;
        public string NombreFiltro { get; set; }
        public string DetalleCongelado { get; set; }

    }
}
