using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPerfilAtrabajoCoeficiente : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }
        public int IdSelect { get; set; }
        public int Columna { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
