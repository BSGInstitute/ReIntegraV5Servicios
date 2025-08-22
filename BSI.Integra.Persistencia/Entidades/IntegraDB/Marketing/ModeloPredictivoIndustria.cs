using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    public class ModeloPredictivoIndustria : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public bool Validar { get; set; }
        public int IdIndustria { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
