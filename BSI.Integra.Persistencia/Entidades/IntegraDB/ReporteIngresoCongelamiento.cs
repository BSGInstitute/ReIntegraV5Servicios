using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ReporteIngresoCongelamiento : BaseIntegraEntity
    {
        public string? NombreFiltro { get; set; }
        public string? DetalleCongelado { get; set; }
        public DateTime FechaCongelamiento { get; set; }
    }
}
