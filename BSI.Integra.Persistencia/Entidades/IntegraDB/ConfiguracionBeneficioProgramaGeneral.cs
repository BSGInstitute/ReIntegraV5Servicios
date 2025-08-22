using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionBeneficioProgramaGeneral : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int Tipo { get; set; }
        public bool? Asosiar { get; set; }
        public int Entrega { get; set; }
        public int? IdMigracion { get; set; }
        public int? AvanceAcademico { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
        public string? Requisitos { get; set; }
        public string? ProcesoSolicitud { get; set; }
        public string? DetallesAdicionales { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> ConfiguracionBeneficioProgramaGeneralEstadoMatriculas { get; set; }
        public List<ConfiguracionBeneficioProgramaGeneralSubEstado> ConfiguracionBeneficioProgramaGeneralSubEstados { get; set; }
    }
}
