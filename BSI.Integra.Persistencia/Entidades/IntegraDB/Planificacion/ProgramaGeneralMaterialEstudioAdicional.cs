
using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralMaterialEstudioAdicional: BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string NombreArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public string? EnlaceArchivo { get; set; }
        public string? NombreConfiguracion { get; set; }
    }
}
