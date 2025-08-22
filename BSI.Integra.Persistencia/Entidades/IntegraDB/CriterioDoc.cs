using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CriterioDoc : BaseIntegraEntity
    {
        public bool ModalidadPresencial { get; set; }
       
        public bool ModalidadAonline { get; set; }
     
        public bool ModalidadOnline { get; set; }
     
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
