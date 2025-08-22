namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroSegmentoDetalleDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int IdOperadorComparacion { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public int CantidadTiempoFrecuencia { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
    }
}
