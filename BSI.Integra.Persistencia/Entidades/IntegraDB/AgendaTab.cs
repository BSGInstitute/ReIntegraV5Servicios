using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AgendaTab : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
        [StringLength(4)]
        public string? CodigoAreaTrabajo { get; set; }
    }
}
