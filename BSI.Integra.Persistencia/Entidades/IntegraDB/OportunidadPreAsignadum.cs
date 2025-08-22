using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadPreAsignadum : BaseIntegraEntity
    {
        public int IdOportunidadLog { get; set; }

        [StringLength(25)]
        public string NombreModeloPredictivoProbabilidadEntrante { get; set; }

        public int IdModeloPredictivoProbabilidad { get; set; }

        public bool Asignado { get; set; }

        public int IdOportunidadAsignado { get; set; }

        public bool EnProcesoAsignacionAutomatizada { get; set; }

        public bool Errado { get; set; }

        public int IdOportunidadErrado { get; set; }

        public bool Configurado { get; set; }

        public int IdOportunidadConfiguracion { get; set; }



    }
}
