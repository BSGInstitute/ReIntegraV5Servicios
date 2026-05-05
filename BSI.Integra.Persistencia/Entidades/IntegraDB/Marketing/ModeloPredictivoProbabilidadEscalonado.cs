using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    /// Entidad: ModeloPredictivoProbabilidadEscalonado
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Entidad de dominio de mkt.T_ModeloPredictivoProbabilidadEscalonado.
    /// </summary>
    public class ModeloPredictivoProbabilidadEscalonado : BaseIntegraEntity
    {
        public int IdModeloPredictivoProbabilidad { get; set; }
        public int IdModeloPredictivoEscalonadoClasificacion { get; set; }
        public decimal ProbabilidadPerfil { get; set; }
        public decimal ProbabilidadPerfilTasaConversion { get; set; }
        public decimal? ProbabilidadPerfilTasaConversionInteraccion { get; set; }
        public decimal? ProbabilidadPerfilTasaConversionInteraccionOriginal { get; set; }

        public virtual TModeloPredictivoProbabilidad IdModeloPredictivoProbabilidadNavigation { get; set; } = null!;
    }
}
