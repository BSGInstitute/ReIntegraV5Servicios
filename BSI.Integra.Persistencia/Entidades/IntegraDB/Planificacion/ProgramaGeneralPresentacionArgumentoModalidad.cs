using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoModalidad : BaseIntegraEntity
    {
        public int IdProgramaGeneralPresentacionArgumento { get; set; }
        public int IdModalidadCurso { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
}
