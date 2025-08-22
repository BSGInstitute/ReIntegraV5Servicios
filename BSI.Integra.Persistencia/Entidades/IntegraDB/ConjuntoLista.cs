using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConjuntoLista : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public int? ConsiderarYaEnviados { get; set; }
        public List<ConjuntoListaDetalle> ListaConjuntoListaDetalle { get; set; }

    }
}
