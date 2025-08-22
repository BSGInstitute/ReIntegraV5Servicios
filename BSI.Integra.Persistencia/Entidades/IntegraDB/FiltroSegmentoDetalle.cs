using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FiltroSegmentoDetalle : BaseIntegraEntity
    {


        public int IdFiltroSegmento { get; set; }
     
        public int IdOperadorComparacion { get; set; }
     
        public int IdTiempoFrecuencia { get; set; }
 
        public int CantidadTiempoFrecuencia { get; set; }
    
        public int IdCategoriaObjetoFiltro { get; set; }

        public int? IdMigracion { get; set; }

        public int Valor { get; set; }



    }
}
