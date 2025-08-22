using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MoodleCategoria : BaseIntegraEntity
    {
        public int IdMoodleCategoriaTipo { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public bool AplicaProyecto { get; set; }
    }
}
