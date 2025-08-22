using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConjuntoListaDetalle : BaseIntegraEntity
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int IdConjuntoLista { get; set; }

        public byte Prioridad { get; set; }
        public List<ConjuntoListaDetalleValor> ListaConjuntoListaDetalleValor { get; set; }
    }

}
