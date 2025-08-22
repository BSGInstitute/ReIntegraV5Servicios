using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CompuestoBeneficioModalidadDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public List<ComboDTO> BeneficiosArgumentos { get; set; }
        public List<ModalidadCursoProblemaDTO> Modalidades { get; set; }
    }
    public class CompuestoMotivacionModalidadDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public List<ComboDTO> MotivacionesArgumentos { get; set; }
        public List<ModalidadCursoProblemaDTO> Modalidades { get; set; }
    }
    public class CompuestoModeloCertificadoModalidadDTO
    {
        public int IdModeloCertificado { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreModeloCertificado { get; set; }
        public string Modalidades { get; set; }
        public string UrlAnterior { get; set; }
        public IList<IFormFile>? Files { get; set; }
    }
    public class ProblemaArgumentoDTO
    {
        public int? Id { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
    }

}
