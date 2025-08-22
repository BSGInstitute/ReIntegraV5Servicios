using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LogProyeccionFur : BaseIntegraEntity
    {
        public string TipoProyectado { get; set; } = null!; 
        public DateTime FechaSemilla { get; set; }
        public DateTime FechaInicioProyeccion { get; set; }
        public DateTime FechaFinProyeccion { get; set; }
        public string ResultadoProceso { get; set; } = null!;
        public string DetalleDeError { get; set; } = null!;
    }
}
