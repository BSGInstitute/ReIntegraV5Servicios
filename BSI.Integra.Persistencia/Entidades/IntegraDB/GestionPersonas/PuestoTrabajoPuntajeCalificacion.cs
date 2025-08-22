using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoPuntajeCalificacion : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int? IdExamen { get; set; }
        public bool CalificaPorCentil { get; set; }
        public decimal? PuntajeMinimo { get; set; }
        public int? IdProcesoSeleccionRango { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsCalificable { get; set; }
    }
}
