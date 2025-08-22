using BSI.Integra.Aplicacion.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ControlDoc : BaseIntegraEntity
    {
   
        public int IdMatriculaCabecera { get; set; }
      
        public int IdCriterioDoc { get; set; }
     
        public bool EstadoDocumento { get; set; }
        
        public string? IdMigracion { get; set; }
    }
}
