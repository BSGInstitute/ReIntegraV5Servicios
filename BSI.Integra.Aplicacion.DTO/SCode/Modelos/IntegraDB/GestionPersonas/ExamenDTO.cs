using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ExamenDTO
    {
        public int Id { get; set; }
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
    public class ConfiguracionExamenTestDTO
    {
        public int IdExamenTest { get; set; }
        public int IdExamen { get; set; }
        public int? IdGrupo { get; set; }
        public int CantidadPreguntas { get; set; }
    }
    public class FactorComponenteDTO
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public string? Nombre { get; set; }
        public decimal? Factor { get; set; }
        public string? Usuario { get; set; }
    }
    public class AsignacionComponenteEvaluacionDTO
    {
        public List<ComponenteAsignacionDTO>? ListaComponenteAsignado { get; set; }
        public List<ComponenteAsignacionDTO>? ListaComponenteNoAsignado { get; set; }
        public int? IdEvaluacion { get; set; }
        public string? Usuario { get; set; }
    }
    public class ComponenteAsignacionDTO
    {
        public int Id { get; set; }
        public string? NombreComponente { get; set; }
    }

    public class ExamenVDTO
    {
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public decimal? FactorComponente { get; set; }
        public int IdEvaluacion { get; set; }
    }
}
