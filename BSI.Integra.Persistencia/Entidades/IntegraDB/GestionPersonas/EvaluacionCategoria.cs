using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class EvaluacionCategoria: BaseIntegraEntity
    {

        [StringLength(100)]
        public string Nombre { get; set; } = null!;
    
    }
}
