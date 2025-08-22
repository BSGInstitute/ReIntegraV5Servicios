namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolucionClienteByActividadDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdCausa { get; set; }
        public int IdPersonal { get; set; }
        public bool Solucionado { get; set; }
        public int IdProblemaCliente { get; set; }
        public string OtroProblema { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
