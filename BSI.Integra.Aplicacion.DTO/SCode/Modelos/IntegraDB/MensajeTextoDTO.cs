namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MensajeTextoDTO
    {
        public int IdOportunidad { get; set; }
        public string IdMatriculaCabecera { get; set; }
        public int CodigoPais { get; set; }
        public string IdSeguimientoTwilio { get; set; }

        public string Numero { get; set; }
        public string Mensaje { get; set; }
        public bool origenAgenda { get; set; } = false;
        public string UserName { get; set; }
        public string Clave { get; set; }
    }
    public class AgendaMensajeTextoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public string Usuario { get; set; }
    }
}
