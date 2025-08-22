using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConjuntoListaDetalleValor : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
    

    }
}
