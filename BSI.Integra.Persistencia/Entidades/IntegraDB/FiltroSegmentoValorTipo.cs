using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FiltroSegmentoValorTipo : BaseIntegraEntity
    {
        public int IdFiltroSegmento { get; set; }
        public int Valor { get; set; }
        public int? IdMigracion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }


    }
}
