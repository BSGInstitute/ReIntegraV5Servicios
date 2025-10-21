using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSolucion : BaseIntegraEntity
    {
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
    }
}
