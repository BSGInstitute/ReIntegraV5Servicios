namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolucionClienteByActividadAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProblema { get; set; }
        public bool Seleccionado { get; set; }
        public bool Solucionado { get; set; }
    }
}
