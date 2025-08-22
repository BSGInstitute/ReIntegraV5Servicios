using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class Examen : BaseIntegraEntity
    {

        public string Nombre { get; set; } = null!;
        public string? Titulo { get; set; }
        public bool? RequiereTiempo { get; set; }
        public int? DuracionMinutos { get; set; }
        public string? Instrucciones { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdExamenConfiguracionFormato { get; set; }
        public int? IdExamenComportamiento { get; set; }
        public int? IdExamenConfigurarResultado { get; set; }
        public int? IdCriterioEvaluacionProceso { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public bool? RequiereCentil { get; set; }
        public int? IdFormulaPuntaje { get; set; }
        public decimal? Factor { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadDiasAcceso { get; set; }
    }
}
