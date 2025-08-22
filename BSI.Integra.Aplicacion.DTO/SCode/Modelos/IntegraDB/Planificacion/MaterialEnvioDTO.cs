using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MaterialEnvioDTO
    {
        public int Id { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public ICollection<MaterialEnvioDetalleDTO> Detalles { get; set; }
    }
}