namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FlujoOcurrenciaDTO
    {
        public int Id { get; set; }
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public bool CerrarSeguimiento { get; set; }
        public int? IdFaseDestino { get; set; }
        public int? IdFlujoActividadSiguiente { get; set; }
    }
    public class FlujoOcurrenciaDetalleDTO
    {
        public int Id { get; set; }
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public bool CerrarSeguimiento { get; set; }
        public int? IdFase_Destino { get; set; }
        public int? IdFlujoActividad_Siguiente { get; set; }
        public string? FaseDestino { get; set; }
        public string? ActividadSiguiente { get; set; }
    }
}