namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Respuesta del endpoint de score P0 por oportunidad.
    /// </summary>
    public class ScorePrimeraOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public decimal P0 { get; set; }
    }
}
