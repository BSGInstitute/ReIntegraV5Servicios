using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class FiltroSegmentoValorTipoDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
    }

    public class ListaFiltroSegmentoValorTipoDTO
    {
        public List<FiltroSegmentoValorTipoDTO> ListaAreas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
        public List<FiltroSegmentoValorTipoDTO> ListaSubAreas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
        public List<FiltroSegmentoValorTipoDTO> ListaProgramas { get; set; } = new List<FiltroSegmentoValorTipoDTO>();
    }

}
