using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
  
    public class PreguntaEncuestaCategoria : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string? Descripcion { get; set; }=null!;
      
    }
}
