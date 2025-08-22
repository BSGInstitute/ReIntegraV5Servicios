using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoPuntajeCalificacionDTO
    {
        public int ? Id { get; set; }
        public int? IdPerfilPuestoTrabajo { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public bool? CalificaPorCentil { get; set; }
        public int? PuntajeMinimo { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public bool? EsCalificable { get; set; }
    }
    public class PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
    {
        public bool CalificacionTotal { get; set; }
        public int? IdEvaluacion { get; set; }
        public string? NombreEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public int? IdComponente { get; set; }
        public string? NombreComponente { get; set; }
        public decimal? Puntaje { get; set; }
        public bool CalificaPorCentil { get; set; }
        public bool CalificaAgrupadoNoIndependiente { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public bool EsCalificable { get; set; }
    }

    public class PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool CalificacionTotal { get; set; }
        public bool CalificaAgrupadoNoIndependiente { get; set; }
    }
}
