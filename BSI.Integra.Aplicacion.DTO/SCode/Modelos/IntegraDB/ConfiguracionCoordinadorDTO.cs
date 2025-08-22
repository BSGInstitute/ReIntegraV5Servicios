namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionCoordinadorDTO
    {
        public int Id { get; set; }
        public int[] ListaPersonal { get; set; }
        public int[] ListaCentroCosto { get; set; }
        public int[] ListaEstadoMatricula { get; set; }
        public int[] ListaSubEstadoMatricula { get; set; }
        public string Usuario { get; set; }
    }
}
