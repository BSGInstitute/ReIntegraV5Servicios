namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion
{
    public class AccesoIpConfiguracionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IpPublica { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaExpira { get; set; }
    }
}