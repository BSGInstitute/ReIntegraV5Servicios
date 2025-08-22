namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class BeneficioPreRequisitoDTO
    {
        public List<CompuestoBeneficioModalidadAlternoDTO> Beneficios { get; set; }
        public List<CompuestoPreRequisitoModalidadDTO> PreRequisitos { get; set; }
        public List<CompuestoMotivacionModalidadAlternoDTO> Motivaciones { get; set; }
        public List<CompuestoCertificacionModalidadDTO> Certificaciones { get; set; }
        public List<CompuestoProblemaModalidadAlternoDTO> Problemas { get; set; }
        public List<CompuestoProblemaModeloCertificadoDTO> Modelos { get; set; }
        public List<CompuestoPresentacionArgumentoModalidadAlternoDTO>? Argumentos { get; set; }

    }
}