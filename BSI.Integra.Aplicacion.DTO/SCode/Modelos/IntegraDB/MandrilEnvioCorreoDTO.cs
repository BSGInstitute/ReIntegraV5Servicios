namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MandrilEnvioCorreoDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdMandrilTipoAsignacion { get; set; }
        public int? EstadoEnvio { get; set; }
        public int IdMandrilTipoEnvio { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string FkMandril { get; set; }
        public bool EsEnvioMasivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

}
