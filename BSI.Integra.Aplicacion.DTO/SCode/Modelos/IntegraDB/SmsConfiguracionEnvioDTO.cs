namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SmsConfiguracionEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SmsEnvioAnexoDTO
    {
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int? IdCodigoPais { get; set; }
        public string Celular { get; set; }
        public string Servidor { get; set; }
        public string Tipo { get; set; }
        public string Puerto { get; set; }
    }
    public class OportunidadDiasSinContactoDTO
    {
        public int IdOportunidad { get; set; }
        public int DiasSinContacto { get; set; }
    }
}
