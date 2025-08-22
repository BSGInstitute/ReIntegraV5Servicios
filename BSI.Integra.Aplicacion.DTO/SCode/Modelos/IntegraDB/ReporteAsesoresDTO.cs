namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteMetasTotalISDTO
    {
        public int Cantidad { get; set; }
    }
    public class ReporteContactabilidadAsesorDTO
    {
        public int Hora { get; set; }
        public int IdAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public double TC { get; set; }
        public string Clave { get; set; }
        public int AT { get; set; }
        public int TE { get; set; }
    }
}
