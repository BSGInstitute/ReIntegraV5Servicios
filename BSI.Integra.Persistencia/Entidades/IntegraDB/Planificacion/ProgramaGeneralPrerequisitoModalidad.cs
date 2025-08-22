using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPrerequisitoModalidad : BaseIntegraEntity
    {
        public int IdProgramaGeneralPrerequisito { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
