namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoCategoriaOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Meta { get; set; }
        public int? Orden { get; set; }
        public int OportunidadMaxima { get; set; }

    }
    public class TipoCategoriaOrigenFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Meta { get; set; }
        public int OportunidadMaxima { get; set; }

    }
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO
    {
        public int IdTipoCategoriaOrigen { get; set; }
        public string NombreTipoCategoriaOrigen { get; set; }
    }
}

