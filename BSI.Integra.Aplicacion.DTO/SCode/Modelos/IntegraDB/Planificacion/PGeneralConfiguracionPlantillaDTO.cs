

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralConfiguracionPlantillaDTO
    {
        public int Id { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int? IdPlantillaBase { get; set; }
        public DateTime? UltimaFechaRemplazarCertificado { get; set; }
        public bool? RemplazarCertificados { get; set; }
        public List<PgeneralConfiguracionPlantillaDetalleDTO> Detalle { get; set; }
    }
}
