namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    internal class ReporteTasaConversionHistoricaDTO
    {
    }

    public class ReporteTasaConversionConsolidadaDTO
    {
        public List<AsesorFiltroDTO> Asesores { get; set; }
        public List<CoordinadorFiltroDTO> Coordinadores { get; set; }
    }

    public class AsesorFiltroDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public bool Asignado { get; set; }
    }

    public class CoordinadorFiltroDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Activo { get; set; }
        public bool? Estado { get; set; }
        public int? IdJefe { get; set; }
    }


}
