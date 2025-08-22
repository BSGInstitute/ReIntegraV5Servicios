namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CajaEgresoAprobadoDTO
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string CodigoRec { get; set; }
        public string Anho { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string Origen { get; set; }
        public DateTime FechaCreacionRegistro { get; set; }
        public int IdPersonalResponsable { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
