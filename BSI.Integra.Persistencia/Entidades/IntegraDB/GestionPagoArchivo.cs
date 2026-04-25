using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GestionPagoArchivo : BaseIntegraEntity
    {
        public int IdGestionPago { get; set; }
        public int? IdGestionPagoCronograma { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string ContentTypeArchivo { get; set; } = null!;
    }
}
