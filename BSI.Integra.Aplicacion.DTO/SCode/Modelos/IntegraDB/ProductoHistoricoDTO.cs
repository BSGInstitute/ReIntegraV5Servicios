
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProductoHistoricoDTO
    {
        public HistoricoProductoProveedorVersionDTO Historico { get; set; }
        public Producto Productos { get; set; }

    }

    public class ActualizarHistoricoDTO
    {
        public int Id { get; set; }
        public int IdTipoPago { get; set; }
        public int IdCondicionPago { get; set; }
        public string Observaciones { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
