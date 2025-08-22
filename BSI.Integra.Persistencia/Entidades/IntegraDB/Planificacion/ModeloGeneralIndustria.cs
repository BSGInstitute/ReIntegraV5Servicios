using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ModeloGeneralIndustria : BaseIntegraEntity
    {
        public int IdModeloGeneral { get; set; }
        public int IdIndustria { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
